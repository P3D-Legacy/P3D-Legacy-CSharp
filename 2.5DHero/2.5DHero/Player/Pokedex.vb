Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.Security
Imports PCLExt.FileStorage

Public Class Pokedex
    Inherits BasePokedex

    '0 = undiscovered
    '1 = seen
    '2 = caught + seen
    '3 = shiny + caught + seen

#Region "PlayerData"

    Public Shared Sub Load()
        Core.Player.Pokedexes.Clear()

        Dim path As IFile = GameModeManager.GetContentFile("Data\pokedex.dat").Result
        FileValidation.CheckFileValid(path, False, "Pokedex.vb")

        Dim lines() As String = System.IO.File.ReadAllLines(path)
        For Each PokedexData As String In lines
            Core.Player.Pokedexes.Add(New Pokedex(PokedexData))
        Next
    End Sub

#End Region

#Region "PokedexHandler"

    'The Pokedex screen changes the PokemonList array to add Pokémon not in the array, so this will get used to count things when focussing on the Pokémon in this dex.
    Private _originalPokemonList As New Dictionary(Of Integer, Integer)

    'Fields:
    Public PokemonList As New Dictionary(Of Integer, Integer)
    Public Name As String = ""
    Public Activation As String = ""
    Public OriginalCount As Integer = 0
    Public IncludeExternalPokemon As Boolean = False 'for the pokedex screen, if true, this pokedex view will include all Pokémon seen/caught at the end.

    Public Sub New(ByVal input As String)
        Dim data() As String = input.Split(CChar("|"))

        Me.Name = data(0)
        Me.Activation = data(1)

        Dim pokemonData() As String = data(2).Split(CChar(","))

        Dim Place As Integer = 1

        For Each l As String In pokemonData
            l = l.Replace("[MAX]", POKEMONCOUNT.ToString())

            If l.Contains("-") = True Then
                Dim range() As String = l.Split(CChar("-"))
                Dim min As Integer = CInt(range(0))
                Dim max As Integer = CInt(range(1))

                For j = min To max
                    PokemonList.Add(Place, j)
                    _originalPokemonList.Add(Place, j)

                    Place += 1
                Next
            Else
                PokemonList.Add(Place, CInt(l))
                _originalPokemonList.Add(Place, CInt(l))

                Place += 1
            End If
        Next

        If data.Length >= 4 Then
            Me.IncludeExternalPokemon = CBool(data(3))
        End If

        Me.OriginalCount = Me.PokemonList.Count
    End Sub

    Dim TempPlaces As New Dictionary(Of Integer, Integer)

    Public Overrides Function GetPlace(ByVal PokemonNumber As Integer) As Integer
        If TempPlaces.ContainsKey(PokemonNumber) = True Then
            Return TempPlaces(PokemonNumber)
        End If

        If PokemonList.ContainsValue(PokemonNumber) = True Then
            For i = 0 To PokemonList.Values.Count - 1
                If PokemonList.Values(i) = PokemonNumber Then
                    TempPlaces.Add(PokemonNumber, PokemonList.Keys(i))
                    Return PokemonList.Keys(i)
                End If
            Next
        End If

        Return -1
    End Function

    Public Overrides Function GetPokemonNumber(ByVal Place As Integer) As Integer
        If PokemonList.ContainsKey(Place) = True Then
            Return PokemonList(Place)
        End If
        Return -1
    End Function

    Public Overrides ReadOnly Property IsActivated() As Boolean
        Get
            If Me.Activation = "0" Then
                Return True
            Else
                If ActionScript.IsRegistered(Me.Activation) = True Then
                    Return True
                End If
            End If
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property Obtained() As Integer
        Get
            Dim o As Integer = 0
            For Each v As Integer In _originalPokemonList.Values
                If GetEntryType(Core.Player.PokedexData, v) > 1 Then
                    o += 1
                End If
            Next
            Return o
        End Get
    End Property

    Public Overrides ReadOnly Property Seen() As Integer
        Get
            Dim o As Integer = 0
            For Each v As Integer In _originalPokemonList.Values
                If GetEntryType(Core.Player.PokedexData, v) = 1 Then
                    o += 1
                End If
            Next
            Return o
        End Get
    End Property

    Public Overrides ReadOnly Property Count() As Integer
        Get
            Return Me._originalPokemonList.Keys.Count
        End Get
    End Property

    Public Overrides Function HasPokemon(ByVal pokemonNumber As Integer, ByVal originalList As Boolean) As Boolean
        If originalList = True Then
            Return _originalPokemonList.ContainsValue(pokemonNumber)
        Else
            Return PokemonList.ContainsValue(pokemonNumber)
        End If
    End Function

#End Region

End Class