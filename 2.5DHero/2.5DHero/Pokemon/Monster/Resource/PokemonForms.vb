﻿Public Class PokemonForms

    Private Shared _pokemonList As New List(Of PokemonForm)

    Public Shared Sub Initialize()
        _pokemonList.Clear()
        _pokemonList.AddRange({New Nidoran(),
                               New Pichu(), New Unown(), New Steelix(),
                               New Deoxys(),
                               New Burmy(), New Shellos(), New Gastrodon(), New Rotom(), New Dialga(), New Arceus(),
                               New Basculin(), New Deerling(), New Sawsbuck(), New Frillish(), New Jellicent(), New Tornadus(), New Thundurus(), New Landorus(), New Kyurem(),
                               New Aegislash()})
    End Sub

    ''' <summary>
    ''' Returns the initial Additional Data, if it needs to be set at generation time of the Pokémon.
    ''' </summary>
    ''' <param name="P"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetInitialAdditionalData(ByVal P As Pokemon) As String
        For Each listP In _pokemonList
            If listP.IsNumber(P.Number) = True Then
                Return listP.GetInitialAdditionalData(P)
            End If
        Next

        Return ""
    End Function

    ''' <summary>
    ''' Returns the Animation Name of the Pokémon, the path to its Sprite/Model files.
    ''' </summary>
    ''' <param name="P"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAnimationName(ByVal P As Pokemon) As String
        For Each listP In _pokemonList
            If listP.IsNumber(P.Number) = True Then
                Return listP.GetAnimationName(P)
            End If
        Next
       
        Return P.OriginalName
    End Function

    ''' <summary>
    ''' Returns the grid coordinates of the Pokémon's menu sprite.
    ''' </summary>
    ''' <param name="P"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetMenuImagePosition(ByVal P As Pokemon) As Vector2
        For Each listP In _pokemonList
            If listP.IsNumber(P.Number) = True Then
                Return listP.GetMenuImagePosition(P)
            End If
        Next

        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim n As Integer = P.Number
        While n > 32
            n -= 32
            y += 1
        End While
        x = n - 1
        Return New Vector2(x, y)
    End Function

    ''' <summary>
    ''' Returns the size of the Pokémon's menu sprite.
    ''' </summary>
    ''' <param name="P"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetMenuImageSize(ByVal P As Pokemon) As Size
        For Each listP In _pokemonList
            If listP.IsNumber(P.Number) = True Then
                Return listP.GetMenuImageSize(P)
            End If
        Next

        Return New Size(32, 32)
    End Function

    ''' <summary>
    ''' Returns the addition to the Pokémon's overworld sprite name.
    ''' </summary>
    ''' <param name="P"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetOverworldAddition(ByVal P As Pokemon) As String
        For Each listP In _pokemonList
            If listP.IsNumber(P.Number) = True Then
                Return listP.GetOverworldAddition(P)
            End If
        Next

        Return ""
    End Function

    ''' <summary>
    ''' Returns the path to the Pokémon's overworld sprite.
    ''' </summary>
    ''' <param name="P"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetOverworldSpriteName(ByVal P As Pokemon) As String
        Dim path As String = "Pokemon\Overworld\Normal\"
        If P.IsShiny = True Then
            path = "Pokemon\Overworld\Shiny\"
        End If
        path &= P.Number.ToString() & GetOverworldAddition(P)
        Return path
    End Function

    ''' <summary>
    ''' Returns the Pokémon's data file.
    ''' </summary>
    ''' <param name="Number">The number of the Pokémon.</param>
    ''' <param name="AdditionalData">The additional data of the Pokémon.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetPokemonDataFile(ByVal Number As Integer, ByVal AdditionalData As String) As String
        Dim FileName As String = GameModeManager.GetPokemonDataFilePath(Number.ToString() & ".dat")

        Dim Addition As String = ""

        For Each listP In _pokemonList
            If listP.IsNumber(Number) = True Then
                Addition = listP.GetDataFileAddition(AdditionalData)
            End If
        Next

        If Addition <> "" Then
            FileName = FileName.Remove(FileName.Length - 4, 4) & Addition & ".dat"
        End If

        If System.IO.File.Exists(FileName) = False Then
            Number = 10
            FileName = GameModeManager.GetPokemonDataFilePath(Number.ToString() & ".dat")
        End If

        Return FileName
    End Function

    Public Shared Function GetDefaultOverworldSpriteAddition(ByVal Number As Integer) As String
        Return ""
    End Function

    Public Shared Function GetDefaultImageAddition(ByVal Number As Integer) As String
        Return ""
    End Function

#Region "Classes"

    Private MustInherit Class PokemonForm

        Private _numbers As New List(Of Integer)

        Public Sub New(ByVal Number As Integer)
            Me._numbers.Add(Number)
        End Sub

        Public Sub New(ByVal Numbers() As Integer)
            Me._numbers.AddRange(Numbers)
        End Sub

        Public Overridable Function GetInitialAdditionalData(ByVal P As Pokemon) As String
            Return ""
        End Function

        Public Overridable Function GetAnimationName(ByVal P As Pokemon) As String
            Return P.OriginalName
        End Function

        Public Overridable Function GetMenuImagePosition(ByVal P As Pokemon) As Vector2
            Dim x As Integer = 0
            Dim y As Integer = 0
            Dim n As Integer = P.Number
            While n > 32
                n -= 32
                y += 1
            End While
            x = n - 1
            Return New Vector2(x, y)
        End Function

        Public Overridable Function GetMenuImageSize(ByVal P As Pokemon) As Size
            Return New Size(32, 32)
        End Function

        Public Overridable Function GetOverworldAddition(ByVal P As Pokemon) As String
            Return ""
        End Function

        Public Overridable Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Return ""
        End Function

        Public Function IsNumber(ByVal number As Integer) As Boolean
            Return Me._numbers.Contains(number)
        End Function

    End Class

    Private Class Nidoran

        Inherits PokemonForm

        Public Sub New()
            MyBase.New({29, 32})
        End Sub

        Public Overrides Function GetAnimationName(ByVal P As Pokemon) As String
            If P.Number = 29 Then
                Return "Nidoran_f"
            Else
                Return "Nidoran_m"
            End If
        End Function

    End Class

    Private Class Pichu

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(172)
        End Sub

        Public Overrides Function GetAnimationName(P As Pokemon) As String
            If P.AdditionalData.ToLower() = "spiky-eared" Then
                Return P.OriginalName & "_spiky-eared"
            Else
                Return MyBase.GetAnimationName(P)
            End If
        End Function

        Public Overrides Function GetOverworldAddition(P As Pokemon) As String
            If P.AdditionalData.ToLower() = "spiky-eared" Then
                Return "_spiky-eared"
            Else
                Return MyBase.GetOverworldAddition(P)
            End If
        End Function

        Public Overrides Function GetMenuImagePosition(P As Pokemon) As Vector2
            If P.AdditionalData.ToLower() = "spiky-eared" Then
                Return New Vector2(13, 26)
            Else
                Return MyBase.GetMenuImagePosition(P)
            End If
        End Function

    End Class

    Private Class Unown

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(201)
        End Sub

        Public Overrides Function GetInitialAdditionalData(ByVal P As Pokemon) As String
            Return CStr(Core.Random.Next(0, 28))
        End Function

        Public Overrides Function GetAnimationName(ByVal P As Pokemon) As String
            Dim AlphabetArray() As String = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "question", "exclamation"}
            If CInt(P.AdditionalData) > 0 Then
                Return "Unown_" & AlphabetArray(CInt(P.AdditionalData))
            End If
            Return "Unown"
        End Function

        Public Overrides Function GetMenuImagePosition(ByVal P As Pokemon) As Vector2
            Dim x As Integer = 8
            Dim y As Integer = 6
            If CInt(P.AdditionalData) > 0 Then
                y = 31
                x = CInt(P.AdditionalData) - 1
            End If
            Return New Vector2(x, y)
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As Pokemon) As String
            Dim alphabet() As String = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "question", "exclamation"}
            Return "-" & alphabet(CInt(P.AdditionalData))
        End Function

    End Class

    Private Class Steelix

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(208)
        End Sub

        Public Overrides Function GetMenuImagePosition(ByVal P As Pokemon) As Vector2
            Return New Vector2(11, 26)
        End Function

        Public Overrides Function GetMenuImageSize(ByVal P As Pokemon) As Size
            Return New Size(35, 32)
        End Function

    End Class

    Private Class Deoxys

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(386)
        End Sub

        Public Overrides Function GetAnimationName(ByVal P As Pokemon) As String
            Select Case P.AdditionalData.ToLower()
                Case "attack", "defense", "speed"
                    Return P.OriginalName & "_" & P.AdditionalData.ToLower()
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetMenuImagePosition(ByVal P As Pokemon) As Vector2
            Select Case P.AdditionalData.ToLower()
                Case "attack"
                    Return New Vector2(8, 28)
                Case "defense"
                    Return New Vector2(7, 28)
                Case "speed"
                    Return New Vector2(9, 28)
                Case Else
                    Return New Vector2(1, 12)
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As Pokemon) As String
            Select Case P.AdditionalData.ToLower()
                Case "attack", "defense", "speed"
                    Return "_" & P.AdditionalData.ToLower()
                Case Else
                    Return ""
            End Select
        End Function

        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "attack", "defense", "speed"
                    Return "_" & AdditionalData.ToLower()
                Case Else
                    Return ""
            End Select
        End Function

    End Class

    Private Class Burmy

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(412)
        End Sub

        'TODO: Finish forms.
        Public Overrides Function GetInitialAdditionalData(ByVal P As Pokemon) As String
            Select Case Screen.Level.EnvironmentType
                Case 0, 5 'Outside, Forest
                    Return "plant"
                Case 2, 3 'Cave, Underwater
                    Return "sandy"
                Case 1 'Inside, Dark
                    Return "trash"
            End Select

            Return "plant"
        End Function

    End Class

    Private Class Shellos

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(422)
        End Sub

        Public Overrides Function GetMenuImagePosition(ByVal P As Pokemon) As Vector2
            If P.AdditionalData = "1" Then
                Return New Vector2(8, 30)
            Else
                Return New Vector2(5, 13)
            End If
        End Function

        Public Overrides Function GetAnimationName(ByVal P As Pokemon) As String
            If P.AdditionalData = "1" Then
                Return P.OriginalName & "e"
            Else
                Return P.OriginalName & "w"
            End If
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As Pokemon) As String
            If P.AdditionalData = "1" Then
                Return "e"
            Else
                Return "w"
            End If
        End Function

    End Class

    Private Class Gastrodon

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(423)
        End Sub

        Public Overrides Function GetMenuImagePosition(ByVal P As Pokemon) As Vector2
            If P.AdditionalData = "1" Then
                Return New Vector2(9, 30)
            Else
                Return New Vector2(6, 13)
            End If
        End Function

        Public Overrides Function GetAnimationName(ByVal P As Pokemon) As String
            If P.AdditionalData = "1" Then
                Return P.OriginalName & "e"
            Else
                Return P.OriginalName & "w"
            End If
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As Pokemon) As String
            If P.AdditionalData = "1" Then
                Return "e"
            Else
                Return "w"
            End If
        End Function

    End Class

    Private Class Rotom

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(479)
        End Sub

        Public Overrides Function GetAnimationName(ByVal P As Pokemon) As String
            Select Case P.AdditionalData.ToLower()
                Case "fan", "frost", "heat", "mow", "wash"
                    Return P.OriginalName & "_" & P.AdditionalData.ToLower()
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetMenuImagePosition(ByVal P As Pokemon) As Vector2
            Select Case P.AdditionalData.ToLower()
                Case "fan"
                    Return New Vector2(13, 30)
                Case "frost"
                    Return New Vector2(14, 30)
                Case "heat"
                    Return New Vector2(15, 30)
                Case "mow"
                    Return New Vector2(16, 30)
                Case "wash"
                    Return New Vector2(17, 30)
                Case Else
                    Return New Vector2(30, 14)
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As Pokemon) As String
            Select Case P.AdditionalData.ToLower()
                Case "fan", "frost", "heat", "mow", "wash"
                    Return "_" & P.AdditionalData.ToLower()
                Case Else
                    Return ""
            End Select
        End Function

        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "fan", "frost", "heat", "mow", "wash"
                    Return "_" & AdditionalData.ToLower()
                Case Else
                    Return ""
            End Select
        End Function

    End Class

    Private Class Dialga

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(483)
        End Sub

        Public Overrides Function GetAnimationName(P As Pokemon) As String
            If P.AdditionalData.ToLower() = "primal" Then
                Return P.OriginalName & "_primal"
            Else
                Return MyBase.GetAnimationName(P)
            End If
        End Function

        Public Overrides Function GetMenuImagePosition(P As Pokemon) As Vector2
            If P.AdditionalData.ToLower() = "primal" Then
                Return New Vector2(14, 26)
            Else
                Return MyBase.GetMenuImagePosition(P)
            End If
        End Function

        Public Overrides Function GetOverworldAddition(P As Pokemon) As String
            If P.AdditionalData.ToLower() = "primal" Then
                Return "_primal"
            Else
                Return MyBase.GetOverworldAddition(P)
            End If
        End Function

    End Class

    Private Class Arceus

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(493)
        End Sub

        Private Function GetTypeAdditionFromPlate(ByVal p As Pokemon) As Tuple(Of String, Integer)
            If Not p.Item Is Nothing Then
                If p.Item.IsPlate = False Then
                    Return New Tuple(Of String, Integer)("", 0)
                Else
                    Select Case p.Item.ID
                        Case 267
                            Return New Tuple(Of String, Integer)("dragon", 15)
                        Case 268
                            Return New Tuple(Of String, Integer)("dark", 10)
                        Case 269
                            Return New Tuple(Of String, Integer)("ground", 9)
                        Case 270
                            Return New Tuple(Of String, Integer)("fighting", 13)
                        Case 271
                            Return New Tuple(Of String, Integer)("fire", 1)
                        Case 272
                            Return New Tuple(Of String, Integer)("ice", 14)
                        Case 273
                            Return New Tuple(Of String, Integer)("bug", 4)
                        Case 274
                            Return New Tuple(Of String, Integer)("steel", 12)
                        Case 275
                            Return New Tuple(Of String, Integer)("grass", 0)
                        Case 276
                            Return New Tuple(Of String, Integer)("psychic", 7)
                        Case 277
                            Return New Tuple(Of String, Integer)("fairy", 16)
                        Case 278
                            Return New Tuple(Of String, Integer)("flying", 3)
                        Case 279
                            Return New Tuple(Of String, Integer)("water", 2)
                        Case 280
                            Return New Tuple(Of String, Integer)("ghost", 11)
                        Case 281
                            Return New Tuple(Of String, Integer)("rock", 8)
                        Case 282
                            Return New Tuple(Of String, Integer)("poison", 5)
                        Case 283
                            Return New Tuple(Of String, Integer)("electric", 6)
                        Case Else
                            Return New Tuple(Of String, Integer)("", 0)
                    End Select
                End If
            Else
                Return New Tuple(Of String, Integer)("", 0)
            End If
        End Function

        Public Overrides Function GetAnimationName(ByVal P As Pokemon) As String
            Dim typeAddition As String = GetTypeAdditionFromPlate(P).Item1
            If typeAddition <> "" Then
                typeAddition = "_" & typeAddition
            End If

            Return "Arceus" & typeAddition
        End Function

        Public Overrides Function GetMenuImagePosition(ByVal P As Pokemon) As Vector2
            Dim data = GetTypeAdditionFromPlate(P)
            If data.Item1 = "" Then
                Return New Vector2(12, 15) 'Default Arceus sprite
            Else
                Return New Vector2(data.Item2, 27) 'Type Arceus sprite
            End If
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As Pokemon) As String
            Dim typeAddition As String = GetTypeAdditionFromPlate(P).Item1
            If typeAddition <> "" Then
                typeAddition = "_" & typeAddition
            End If
            Return typeAddition
        End Function

    End Class

    Private Class Basculin

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(550)
        End Sub

        Public Overrides Function GetDataFileAddition(AdditionalData As String) As String
            If AdditionalData.ToLower() = "blue" Then
                Return "_blue"
            Else
                Return ""
            End If
        End Function

        Public Overrides Function GetAnimationName(P As Pokemon) As String
            If P.AdditionalData.ToLower() = "blue" Then
                Return P.OriginalName & "_blue"
            Else
                Return MyBase.GetAnimationName(P)
            End If
        End Function

        Public Overrides Function GetMenuImagePosition(P As Pokemon) As Vector2
            If P.AdditionalData.ToLower() = "blue" Then
                Return New Vector2(0, 28)
            Else
                Return MyBase.GetMenuImagePosition(P)
            End If
        End Function

        Public Overrides Function GetOverworldAddition(P As Pokemon) As String
            If P.AdditionalData.ToLower() = "blue" Then
                Return "_blue"
            Else
                Return MyBase.GetOverworldAddition(P)
            End If
        End Function

    End Class

    Private Class Deerling

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(585)
        End Sub

        Public Overrides Function GetMenuImagePosition(ByVal P As Pokemon) As Vector2
            Select Case World.CurrentSeason
                Case World.Seasons.Fall
                    Return New Vector2(1, 30)
                Case World.Seasons.Spring
                    Return New Vector2(8, 18)
                Case World.Seasons.Summer
                    Return New Vector2(0, 30)
                Case World.Seasons.Winter
                    Return New Vector2(2, 30)
            End Select
        End Function

        Public Overrides Function GetAnimationName(ByVal P As Pokemon) As String
            Select Case World.CurrentSeason
                Case World.Seasons.Fall
                    Return P.OriginalName & "_fa"
                Case World.Seasons.Spring
                    Return P.OriginalName & "_sp"
                Case World.Seasons.Summer
                    Return P.OriginalName & "_su"
                Case World.Seasons.Winter
                    Return P.OriginalName & "_wi"
            End Select
            Return P.OriginalName & "_fa"
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As Pokemon) As String
            Select Case World.CurrentSeason
                Case World.Seasons.Fall
                    Return "_fa"
                Case World.Seasons.Spring
                    Return "_sp"
                Case World.Seasons.Summer
                    Return "_su"
                Case World.Seasons.Winter
                    Return "_wi"
            End Select
            Return "_fa"
        End Function

    End Class

    Private Class Sawsbuck

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(586)
        End Sub

        Public Overrides Function GetMenuImagePosition(ByVal P As Pokemon) As Vector2
            Select Case World.CurrentSeason
                Case World.Seasons.Fall
                    Return New Vector2(4, 30)
                Case World.Seasons.Spring
                    Return New Vector2(9, 18)
                Case World.Seasons.Summer
                    Return New Vector2(3, 30)
                Case World.Seasons.Winter
                    Return New Vector2(5, 30)
            End Select
        End Function

        Public Overrides Function GetAnimationName(ByVal P As Pokemon) As String
            Select Case World.CurrentSeason
                Case World.Seasons.Fall
                    Return P.OriginalName & "_fa"
                Case World.Seasons.Spring
                    Return P.OriginalName & "_sp"
                Case World.Seasons.Summer
                    Return P.OriginalName & "_su"
                Case World.Seasons.Winter
                    Return P.OriginalName & "_wi"
            End Select
            Return P.OriginalName & "_fa"
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As Pokemon) As String
            Select Case World.CurrentSeason
                Case World.Seasons.Fall
                    Return "_fa"
                Case World.Seasons.Spring
                    Return "_sp"
                Case World.Seasons.Summer
                    Return "_su"
                Case World.Seasons.Winter
                    Return "_wi"
            End Select
            Return "_fa"
        End Function

    End Class

    Private Class Frillish

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(592)
        End Sub

        Public Overrides Function GetMenuImagePosition(ByVal P As Pokemon) As Vector2
            Select Case P.Gender
                Case Pokemon.Genders.Female
                    Return New Vector2(6, 30)
                Case Else
                    Return New Vector2(15, 18)
            End Select
        End Function

        Public Overrides Function GetAnimationName(ByVal P As Pokemon) As String
            Select Case P.Gender
                Case Pokemon.Genders.Female
                    Return P.OriginalName & "_f"
                Case Else
                    Return P.OriginalName & "_m"
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As Pokemon) As String
            Select Case P.Gender
                Case Pokemon.Genders.Female
                    Return "_f"
                Case Else
                    Return "_m"
            End Select
        End Function

    End Class

    Private Class Jellicent

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(593)
        End Sub

        Public Overrides Function GetMenuImagePosition(ByVal P As Pokemon) As Vector2
            Select Case P.Gender
                Case Pokemon.Genders.Female
                    Return New Vector2(7, 30)
                Case Else
                    Return New Vector2(16, 18)
            End Select
        End Function

        Public Overrides Function GetAnimationName(ByVal P As Pokemon) As String
            Select Case P.Gender
                Case Pokemon.Genders.Female
                    Return P.OriginalName & "_f"
                Case Else
                    Return P.OriginalName & "_m"
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As Pokemon) As String
            Select Case P.Gender
                Case Pokemon.Genders.Female
                    Return "_f"
                Case Else
                    Return "_m"
            End Select
        End Function

    End Class

    Private Class Tornadus

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(641)
        End Sub

        Public Overrides Function GetAnimationName(P As Pokemon) As String
            If P.AdditionalData.ToLower() = "therian" Then
                Return P.OriginalName & "_therian"
            Else
                Return MyBase.GetAnimationName(P)
            End If
        End Function

        Public Overrides Function GetMenuImagePosition(P As Pokemon) As Vector2
            If P.AdditionalData.ToLower() = "therian" Then
                Return New Vector2(10, 30)
            Else
                Return MyBase.GetMenuImagePosition(P)
            End If
        End Function

        Public Overrides Function GetOverworldAddition(P As Pokemon) As String
            If P.AdditionalData.ToLower() = "therian" Then
                Return "_therian"
            Else
                Return MyBase.GetOverworldAddition(P)
            End If
        End Function

    End Class

    Private Class Thundurus

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(642)
        End Sub

        Public Overrides Function GetAnimationName(P As Pokemon) As String
            If P.AdditionalData.ToLower() = "therian" Then
                Return P.OriginalName & "_therian"
            Else
                Return MyBase.GetAnimationName(P)
            End If
        End Function

        Public Overrides Function GetMenuImagePosition(P As Pokemon) As Vector2
            If P.AdditionalData.ToLower() = "therian" Then
                Return New Vector2(11, 30)
            Else
                Return MyBase.GetMenuImagePosition(P)
            End If
        End Function

        Public Overrides Function GetOverworldAddition(P As Pokemon) As String
            If P.AdditionalData.ToLower() = "therian" Then
                Return "_therian"
            Else
                Return MyBase.GetOverworldAddition(P)
            End If
        End Function

    End Class

    Private Class Landorus

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(645)
        End Sub

        Public Overrides Function GetAnimationName(P As Pokemon) As String
            If P.AdditionalData.ToLower() = "therian" Then
                Return P.OriginalName & "_therian"
            Else
                Return MyBase.GetAnimationName(P)
            End If
        End Function

        Public Overrides Function GetMenuImagePosition(P As Pokemon) As Vector2
            If P.AdditionalData.ToLower() = "therian" Then
                Return New Vector2(12, 30)
            Else
                Return MyBase.GetMenuImagePosition(P)
            End If
        End Function

        Public Overrides Function GetOverworldAddition(P As Pokemon) As String
            If P.AdditionalData.ToLower() = "therian" Then
                Return "_therian"
            Else
                Return MyBase.GetOverworldAddition(P)
            End If
        End Function

    End Class

    Private Class Kyurem

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(646)
        End Sub

        Public Overrides Function GetAnimationName(ByVal P As Pokemon) As String
            Select Case P.AdditionalData.ToLower()
                Case "black"
                    Return P.OriginalName & "_black"
                Case "white"
                    Return P.OriginalName & "_white"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(P As Pokemon) As String
            Select Case P.AdditionalData.ToLower()
                Case "black"
                    Return "_black"
                Case "white"
                    Return "_white"
                Case Else
                    Return ""
            End Select
        End Function

        Public Overrides Function GetMenuImagePosition(ByVal P As Pokemon) As Vector2
            Select Case P.AdditionalData.ToLower()
                Case "black"
                    Return New Vector2(22, 30)
                Case "white"
                    Return New Vector2(24, 30)
                Case Else
                    Return New Vector2(5, 20)
            End Select
        End Function

        Public Overrides Function GetMenuImageSize(P As Pokemon) As Size
            Select Case P.AdditionalData.ToLower()
                Case "black"
                    Return New Size(37, 32)
                Case "white"
                    Return New Size(33, 32)
                Case Else
                    Return New Size(32, 32)
            End Select
        End Function

    End Class

    Private Class Aegislash

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(681)
        End Sub

        Public Overrides Function GetMenuImagePosition(ByVal P As Pokemon) As Vector2
            Select Case P.AdditionalData
                Case "blade"
                    Return New Vector2(20, 30)
                Case Else
                    Return New Vector2(8, 21)
            End Select
        End Function

        Public Overrides Function GetMenuImageSize(ByVal P As Pokemon) As Size
            Select Case P.AdditionalData
                Case "blade"
                    Return New Size(35, 32)
                Case Else
                    Return New Size(32, 32)
            End Select
        End Function

        Public Overrides Function GetAnimationName(ByVal P As Pokemon) As String
            Select Case P.AdditionalData
                Case "blade"
                    Return P.OriginalName & "_blade"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As Pokemon) As String
            Select Case P.AdditionalData
                Case "blade"
                    Return "_blade"
                Case Else
                    Return ""
            End Select
        End Function

        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "blade"
                    Return "_blade"
                Case Else
                    Return ""
            End Select
        End Function

    End Class

#End Region

End Class