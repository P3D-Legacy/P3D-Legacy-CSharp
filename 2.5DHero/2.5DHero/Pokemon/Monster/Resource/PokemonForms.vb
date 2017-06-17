Imports System.Drawing
Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Pokemon.Resource
Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.Screens
Imports P3D.Legacy.Core.World

Public Class PokemonForms
    Inherits BasePokemonForms

    'Private Shared _pokemonList As New List(Of PokemonForm)

    Public Shared Sub Initialize()
        PokemonList.Clear()
        PokemonList.AddRange({New Venusaur(), New Charizard(), New Blastoise(), New Beedrill(), New Pidgeot(), New Nidoran(), New Alakazam(), New Slowbro(), New Gengar(), New Kangaskhan(), New Pinsir(), New Gyarados(), New Aerodactyl(), New Mewtwo(),
                               New Pichu(), New Unown(), New Ampharos(), New Steelix(), New Scizor(), New Heracross(), New Houndoom(), New Tyranitar(),
                               New Sceptile(), New Blaziken(), New Swampert(), New Gardevoir(), New Sableye(), New Mawile(), New Aggron(), New Medicham(), New Manectric(), New Sharpedo(), New Camerupt(), New Altaria(), New Banette(), New Absol(), New Glalie(), New Salamence(), New Metagross(), New Latias(), New Latios(), New Kyogre(), New Groudon(), New Rayquaza(), New Deoxys(),
                               New Burmy(), New Shellos(), New Gastrodon(), New Lopunny(), New Garchomp(), New Lucario(), New Abomasnow(), New Gallade(), New Rotom(), New Dialga(), New Arceus(),
                               New Audino(), New Basculin(), New Deerling(), New Sawsbuck(), New Frillish(), New Jellicent(), New Tornadus(), New Thundurus(), New Landorus(), New Kyurem(),
                               New Vivillon(), New Pyroar(), New Flabebe(), New Floette(), New Florges(), New Aegislash(), New Diancie(), New Hoopa()})

    End Sub

#Region "Classes"

    Private Class Venusaur
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(3)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(24, 29)
                Case Else
                    Return New Vector2(2, 0)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Charizard
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(6)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega_x"
                    Return New Vector2(7, 29)
                Case "mega_y"
                    Return New Vector2(18, 29)
                Case Else
                    Return New Vector2(5, 0)
            End Select
        End Function
        Public Overrides Function GetMenuImageSize(ByVal P As BasePokemon) As Size
            Select Case P.AdditionalData
                Case "mega_x"
                    Return New Size(40, 32)
                Case "mega_y"
                    Return New Size(38, 32)
                Case Else
                    Return New Size(32, 32)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega_x"
                    Return "_mega_x"
                Case "mega_y"
                    Return "_mega_y"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega_x"
                    Return P.OriginalName & "_mega_x"
                Case "mega_y"
                    Return P.OriginalName & "_mega_y"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega_x"
                    Return "_mega_x"
                Case "mega_y"
                    Return "_mega_y"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Blastoise
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(9)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(29, 29)
                Case Else
                    Return New Vector2(8, 0)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Beedrill
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(15)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(15, 26)
                Case Else
                    Return New Vector2(14, 0)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Pidgeot
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(18)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(16, 26)
                Case Else
                    Return New Vector2(17, 0)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Nidoran

        Inherits PokemonForm

        Public Sub New()
            MyBase.New({29, 32})
        End Sub

        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            If P.Number = 29 Then
                Return "Nidoran_f"
            Else
                Return "Nidoran_m"
            End If
        End Function

    End Class

    Private Class Alakazam
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(65)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(27, 29)
                Case Else
                    Return New Vector2(0, 2)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Slowbro
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(80)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(18, 26)
                Case Else
                    Return New Vector2(15, 2)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Gengar
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(94)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(21, 29)
                Case Else
                    Return New Vector2(29, 2)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Kangaskhan
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(115)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(26, 29)
                Case Else
                    Return New Vector2(18, 3)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Pinsir
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(127)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(0, 29)
                Case Else
                    Return New Vector2(30, 3)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Gyarados
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(130)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(31, 27)
                Case Else
                    Return New Vector2(1, 4)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Aerodactyl
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(142)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(20, 29)
                Case Else
                    Return New Vector2(13, 4)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Mewtwo
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(150)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega_x"
                    Return New Vector2(14, 29)
                Case "mega_y"
                    Return New Vector2(28, 29)
                Case Else
                    Return New Vector2(21, 4)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega_x"
                    Return "_mega_x"
                Case "mega_y"
                    Return "_mega_y"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega_x"
                    Return P.OriginalName & "_mega_x"
                Case "mega_y"
                    Return P.OriginalName & "_mega_y"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega_x"
                    Return "_mega_x"
                Case "mega_y"
                    Return "_mega_y"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Pichu

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(172)
        End Sub

        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            If P.AdditionalData.ToLower() = "spiky-eared" Then
                Return P.OriginalName & "_spiky-eared"
            Else
                Return MyBase.GetAnimationName(P)
            End If
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            If P.AdditionalData.ToLower() = "spiky-eared" Then
                Return "_spiky-eared"
            Else
                Return MyBase.GetOverworldAddition(P)
            End If
        End Function

        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            If P.AdditionalData.ToLower() = "spiky-eared" Then
                Return New Vector2(13, 26)
            Else
                Return MyBase.GetMenuImagePosition(P)
            End If
        End Function

    End Class

    Private Class Ampharos
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(181)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(9, 29)
                Case Else
                    Return New Vector2(20, 5)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Unown

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(201)
        End Sub

        Public Overrides Function GetInitialAdditionalData(ByVal P As BasePokemon) As String
            Return CStr(Core.Random.Next(0, 28))
        End Function

        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Dim AlphabetArray() As String = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "question", "exclamation"}
            If CInt(P.AdditionalData) > 0 Then
                Return "Unown_" & AlphabetArray(CInt(P.AdditionalData))
            End If
            Return "Unown"
        End Function

        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Dim x As Integer = 8
            Dim y As Integer = 6
            If CInt(P.AdditionalData) > 0 Then
                y = 31
                x = CInt(P.AdditionalData) - 1
            End If
            Return New Vector2(x, y)
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Dim alphabet() As String = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "question", "exclamation"}
            Return "-" & alphabet(CInt(P.AdditionalData))
        End Function

    End Class

    Private Class Steelix

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(208)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(29, 28)
                Case Else
                    Return New Vector2(11, 26)
            End Select
        End Function
        Public Overrides Function GetMenuImageSize(ByVal P As BasePokemon) As Size
            Select Case P.AdditionalData
                Case "mega"
                    Return New Size(39, 32)
                Case Else
                    Return New Size(35, 32)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function
        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Scizor
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(212)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(1, 29)
                Case Else
                    Return New Vector2(19, 6)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function
        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Heracross
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(214)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(2, 29)
                Case Else
                    Return New Vector2(21, 6)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Houndoom
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(229)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(4, 29)
                Case Else
                    Return New Vector2(4, 7)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Tyranitar
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(248)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(5, 29)
                Case Else
                    Return New Vector2(23, 7)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Sceptile
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(254)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(0, 26)
                Case Else
                    Return New Vector2(29, 7)
            End Select
        End Function
        Public Overrides Function GetMenuImageSize(ByVal P As BasePokemon) As Size
            Select Case P.AdditionalData
                Case "mega"
                    Return New Size(37, 32)
                Case Else
                    Return New Size(35, 32)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Blaziken
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(257)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(15, 29)
                Case Else
                    Return New Vector2(0, 8)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Swampert
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(260)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(31, 28)
                Case Else
                    Return New Vector2(3, 8)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Gardevoir
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(282)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(12, 29)
                Case Else
                    Return New Vector2(25, 8)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Sableye
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(302)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(19, 26)
                Case Else
                    Return New Vector2(13, 9)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Mawile
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(303)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(13, 29)
                Case Else
                    Return New Vector2(14, 9)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Aggron
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(306)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(30, 29)
                Case Else
                    Return New Vector2(17, 9)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Medicham
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(308)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(16, 29)
                Case Else
                    Return New Vector2(19, 9)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Manectric
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(310)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(11, 29)
                Case Else
                    Return New Vector2(21, 9)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Sharpedo
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(319)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(20, 26)
                Case Else
                    Return New Vector2(30, 9)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Camerupt
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(323)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(21, 26)
                Case Else
                    Return New Vector2(2, 10)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Altaria
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(334)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(22, 26)
                Case Else
                    Return New Vector2(13, 10)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Banette
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(354)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(23, 29)
                Case Else
                    Return New Vector2(1, 11)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Absol
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(359)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(6, 29)
                Case Else
                    Return New Vector2(6, 11)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Glalie
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(362)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(29, 27)
                Case Else
                    Return New Vector2(9, 11)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Salamence
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(373)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(23, 26)
                Case Else
                    Return New Vector2(20, 11)
            End Select
        End Function
        Public Overrides Function GetMenuImageSize(ByVal P As BasePokemon) As Size
            Select Case P.AdditionalData
                Case "mega"
                    Return New Size(35, 32)
                Case Else
                    Return New Size(32, 32)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Metagross
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(376)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(30, 27)
                Case Else
                    Return New Vector2(23, 11)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Latias
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(380)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(28, 26)
                Case Else
                    Return New Vector2(27, 11)
            End Select
        End Function
        Public Overrides Function GetMenuImageSize(ByVal P As BasePokemon) As Size
            Select Case P.AdditionalData
                Case "mega"
                    Return New Size(35, 32)
                Case Else
                    Return New Size(32, 32)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function
        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Latios
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(381)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(30, 26)
                Case Else
                    Return New Vector2(28, 11)
            End Select
        End Function
        Public Overrides Function GetMenuImageSize(ByVal P As BasePokemon) As Size
            Select Case P.AdditionalData
                Case "mega"
                    Return New Size(35, 32)
                Case Else
                    Return New Size(32, 32)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function
        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Kyogre
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(382)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "primal"
                    Return New Vector2(30, 25)
                Case Else
                    Return New Vector2(29, 11)
            End Select
        End Function
        Public Overrides Function GetMenuImageSize(ByVal P As BasePokemon) As Size
            Select Case P.AdditionalData
                Case "primal"
                    Return New Size(36, 32)
                Case Else
                    Return New Size(32, 32)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "primal"
                    Return "_primal"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "primal"
                    Return P.OriginalName & "_primal"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "primal"
                    Return "_primal"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Groudon
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(383)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "primal"
                    Return New Vector2(28, 25)
                Case Else
                    Return New Vector2(30, 11)
            End Select
        End Function
        Public Overrides Function GetMenuImageSize(ByVal P As BasePokemon) As Size
            Select Case P.AdditionalData
                Case "primal"
                    Return New Size(36, 32)
                Case Else
                    Return New Size(35, 32)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "primal"
                    Return "_primal"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "primal"
                    Return P.OriginalName & "_primal"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "primal"
                    Return "_primal"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Rayquaza
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(384)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(26, 25)
                Case Else
                    Return New Vector2(31, 11)
            End Select
        End Function
        Public Overrides Function GetMenuImageSize(ByVal P As BasePokemon) As Size
            Select Case P.AdditionalData
                Case "mega"
                    Return New Size(38, 32)
                Case Else
                    Return New Size(32, 32)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function
        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Deoxys

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(386)
        End Sub

        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData.ToLower()
                Case "attack", "defense", "speed"
                    Return P.OriginalName & "_" & P.AdditionalData.ToLower()
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
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

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
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
        Public Overrides Function GetInitialAdditionalData(ByVal P As BasePokemon) As String
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

        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            If P.AdditionalData = "1" Then
                Return New Vector2(8, 30)
            Else
                Return New Vector2(5, 13)
            End If
        End Function

        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            If P.AdditionalData = "1" Then
                Return P.OriginalName & "e"
            Else
                Return P.OriginalName & "w"
            End If
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
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

        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            If P.AdditionalData = "1" Then
                Return New Vector2(9, 30)
            Else
                Return New Vector2(6, 13)
            End If
        End Function

        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            If P.AdditionalData = "1" Then
                Return P.OriginalName & "e"
            Else
                Return P.OriginalName & "w"
            End If
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            If P.AdditionalData = "1" Then
                Return "e"
            Else
                Return "w"
            End If
        End Function

    End Class

    Private Class Lopunny
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(428)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(25, 26)
                Case Else
                    Return New Vector2(11, 13)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Garchomp
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(445)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(10, 29)
                Case Else
                    Return New Vector2(28, 13)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Lucario
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(448)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(17, 29)
                Case Else
                    Return New Vector2(31, 13)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Abomasnow
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(460)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(25, 29)
                Case Else
                    Return New Vector2(11, 14)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Gallade
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(475)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(26, 26)
                Case Else
                    Return New Vector2(26, 14)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Rotom

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(479)
        End Sub

        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData.ToLower()
                Case "fan", "frost", "heat", "mow", "wash"
                    Return P.OriginalName & "_" & P.AdditionalData.ToLower()
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
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

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
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

        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            If P.AdditionalData.ToLower() = "primal" Then
                Return P.OriginalName & "_primal"
            Else
                Return MyBase.GetAnimationName(P)
            End If
        End Function

        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            If P.AdditionalData.ToLower() = "primal" Then
                Return New Vector2(14, 26)
            Else
                Return MyBase.GetMenuImagePosition(P)
            End If
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            If P.AdditionalData.ToLower() = "primal" Then
                Return "_primal"
            Else
                Return MyBase.GetOverworldAddition(P)
            End If
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "primal"
                    Return "_primal"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Arceus

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(493)
        End Sub

        Private Function GetTypeAdditionFromPlate(ByVal P As BasePokemon) As Tuple(Of String, Integer)
            If Not P.Item Is Nothing Then
                If P.Item.IsPlate = False Then
                    Return New Tuple(Of String, Integer)("", 0)
                Else
                    Select Case P.Item.Id
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

        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Dim typeAddition As String = GetTypeAdditionFromPlate(P).Item1
            If typeAddition <> "" Then
                typeAddition = "_" & typeAddition
            End If

            Return "Arceus" & typeAddition
        End Function

        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Dim data = GetTypeAdditionFromPlate(P)
            If data.Item1 = "" Then
                Return New Vector2(12, 15) 'Default Arceus sprite
            Else
                Return New Vector2(data.Item2, 27) 'Type Arceus sprite
            End If
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Dim typeAddition As String = GetTypeAdditionFromPlate(P).Item1
            If typeAddition <> "" Then
                typeAddition = "_" & typeAddition
            End If
            Return typeAddition
        End Function

    End Class

    Private Class Audino
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(531)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(27, 26)
                Case Else
                    Return New Vector2(18, 16)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
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

        Public Overrides Function GetAnimationName(P As BasePokemon) As String
            If P.AdditionalData.ToLower() = "blue" Then
                Return P.OriginalName & "_blue"
            Else
                Return MyBase.GetAnimationName(P)
            End If
        End Function

        Public Overrides Function GetMenuImagePosition(P As BasePokemon) As Vector2
            If P.AdditionalData.ToLower() = "blue" Then
                Return New Vector2(0, 28)
            Else
                Return MyBase.GetMenuImagePosition(P)
            End If
        End Function

        Public Overrides Function GetOverworldAddition(P As BasePokemon) As String
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

        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case World.CurrentSeason
                Case SeasonEnum.Fall
                    Return New Vector2(1, 30)
                Case SeasonEnum.Spring
                    Return New Vector2(8, 18)
                Case SeasonEnum.Summer
                    Return New Vector2(0, 30)
                Case SeasonEnum.Winter
                    Return New Vector2(2, 30)
            End Select
        End Function

        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case World.CurrentSeason
                Case SeasonEnum.Fall
                    Return P.OriginalName & "_fa"
                Case SeasonEnum.Spring
                    Return P.OriginalName & "_sp"
                Case SeasonEnum.Summer
                    Return P.OriginalName & "_su"
                Case SeasonEnum.Winter
                    Return P.OriginalName & "_wi"
            End Select
            Return P.OriginalName & "_fa"
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case World.CurrentSeason
                Case SeasonEnum.Fall
                    Return "_fa"
                Case SeasonEnum.Spring
                    Return "_sp"
                Case SeasonEnum.Summer
                    Return "_su"
                Case SeasonEnum.Winter
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

        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case World.CurrentSeason
                Case SeasonEnum.Fall
                    Return New Vector2(4, 30)
                Case SeasonEnum.Spring
                    Return New Vector2(9, 18)
                Case SeasonEnum.Summer
                    Return New Vector2(3, 30)
                Case SeasonEnum.Winter
                    Return New Vector2(5, 30)
            End Select
        End Function

        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case World.CurrentSeason
                Case SeasonEnum.Fall
                    Return P.OriginalName & "_fa"
                Case SeasonEnum.Spring
                    Return P.OriginalName & "_sp"
                Case SeasonEnum.Summer
                    Return P.OriginalName & "_su"
                Case SeasonEnum.Winter
                    Return P.OriginalName & "_wi"
            End Select
            Return P.OriginalName & "_fa"
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case World.CurrentSeason
                Case SeasonEnum.Fall
                    Return "_fa"
                Case SeasonEnum.Spring
                    Return "_sp"
                Case SeasonEnum.Summer
                    Return "_su"
                Case SeasonEnum.Winter
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

        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.Gender
                Case Pokemon.Genders.Female
                    Return New Vector2(6, 30)
                Case Else
                    Return New Vector2(15, 18)
            End Select
        End Function

        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.Gender
                Case Pokemon.Genders.Female
                    Return P.OriginalName & "_f"
                Case Else
                    Return P.OriginalName & "_m"
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
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

        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.Gender
                Case Pokemon.Genders.Female
                    Return New Vector2(7, 30)
                Case Else
                    Return New Vector2(16, 18)
            End Select
        End Function

        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.Gender
                Case Pokemon.Genders.Female
                    Return P.OriginalName & "_f"
                Case Else
                    Return P.OriginalName & "_m"
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
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

        Public Overrides Function GetAnimationName(P As BasePokemon) As String
            If P.AdditionalData.ToLower() = "therian" Then
                Return P.OriginalName & "_therian"
            Else
                Return MyBase.GetAnimationName(P)
            End If
        End Function

        Public Overrides Function GetMenuImagePosition(P As BasePokemon) As Vector2
            If P.AdditionalData.ToLower() = "therian" Then
                Return New Vector2(10, 30)
            Else
                Return MyBase.GetMenuImagePosition(P)
            End If
        End Function

        Public Overrides Function GetOverworldAddition(P As BasePokemon) As String
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

        Public Overrides Function GetAnimationName(P As BasePokemon) As String
            If P.AdditionalData.ToLower() = "therian" Then
                Return P.OriginalName & "_therian"
            Else
                Return MyBase.GetAnimationName(P)
            End If
        End Function

        Public Overrides Function GetMenuImagePosition(P As BasePokemon) As Vector2
            If P.AdditionalData.ToLower() = "therian" Then
                Return New Vector2(11, 30)
            Else
                Return MyBase.GetMenuImagePosition(P)
            End If
        End Function

        Public Overrides Function GetOverworldAddition(P As BasePokemon) As String
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

        Public Overrides Function GetAnimationName(P As BasePokemon) As String
            If P.AdditionalData.ToLower() = "therian" Then
                Return P.OriginalName & "_therian"
            Else
                Return MyBase.GetAnimationName(P)
            End If
        End Function

        Public Overrides Function GetMenuImagePosition(P As BasePokemon) As Vector2
            If P.AdditionalData.ToLower() = "therian" Then
                Return New Vector2(12, 30)
            Else
                Return MyBase.GetMenuImagePosition(P)
            End If
        End Function

        Public Overrides Function GetOverworldAddition(P As BasePokemon) As String
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

        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData.ToLower()
                Case "black"
                    Return P.OriginalName & "_black"
                Case "white"
                    Return P.OriginalName & "_white"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(P As BasePokemon) As String
            Select Case P.AdditionalData.ToLower()
                Case "black"
                    Return "_black"
                Case "white"
                    Return "_white"
                Case Else
                    Return ""
            End Select
        End Function

        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData.ToLower()
                Case "black"
                    Return New Vector2(22, 30)
                Case "white"
                    Return New Vector2(24, 30)
                Case Else
                    Return New Vector2(5, 20)
            End Select
        End Function

        Public Overrides Function GetMenuImageSize(P As BasePokemon) As Size
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

    Private Class Vivillon
        Inherits PokemonForm

        Public Sub New()
            MyBase.New(666)
        End Sub

        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "archipelago"
                    Return New Vector2(18, 28)
                Case "continental"
                    Return New Vector2(12, 28)
                Case "elegant"
                    Return New Vector2(14, 28)
                Case "fancy"
                    Return New Vector2(28, 28)
                Case "garden"
                    Return New Vector2(13, 28)
                Case "high_planes"
                    Return New Vector2(19, 28)
                Case "icy_snow"
                    Return New Vector2(15, 28)
                Case "jungle"
                    Return New Vector2(26, 28)
                Case "marine"
                    Return New Vector2(17, 28)
                Case "meadow"
                    Return New Vector2(25, 20)
                Case "modern"
                    Return New Vector2(16, 28)
                Case "monsoon"
                    Return New Vector2(22, 28)
                Case "ocean"
                    Return New Vector2(25, 28)
                Case "pokeball"
                    Return New Vector2(27, 28)
                Case "polar"
                    Return New Vector2(10, 28)
                Case "river"
                    Return New Vector2(21, 28)
                Case "sandstorm"
                    Return New Vector2(20, 28)
                Case "savanna"
                    Return New Vector2(23, 28)
                Case "sun"
                    Return New Vector2(24, 28)
                Case "tundra"
                    Return New Vector2(11, 28)
                Case Else
                    Return New Vector2(25, 20)
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "archipelago"
                    Return "_archipelago"
                Case "continental"
                    Return "_continental"
                Case "elegant"
                    Return "_elegant"
                Case "fancy"
                    Return "_fancy"
                Case "garden"
                    Return "_garden"
                Case "high_planes"
                    Return "_high_planes"
                Case "icy_snow"
                    Return "_icy_snow"
                Case "jungle"
                    Return "_jungle"
                Case "marine"
                    Return "_marine"
                Case "meadow"
                    Return "_meadow"
                Case "modern"
                    Return "_modern"
                Case "monsoon"
                    Return "_monsoon"
                Case "ocean"
                    Return "_ocean"
                Case "pokeball"
                    Return "_pokeball"
                Case "polar"
                    Return "_polar"
                Case "river"
                    Return "_river"
                Case "sandstorm"
                    Return "_sandstorm"
                Case "savanna"
                    Return "_savanna"
                Case "sun"
                    Return "_sun"
                Case "tundra"
                    Return "_tundra"
                Case Else
                    Return "_meadow"
            End Select
        End Function
    End Class

    Private Class Pyroar

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(668)
        End Sub

        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.Gender
                Case Pokemon.Genders.Male
                    Return New Vector2(27, 20)
                Case Else
                    Return New Vector2(27, 31)
            End Select
        End Function

        Public Overrides Function GetMenuImageSize(ByVal P As BasePokemon) As Size
            Select Case P.Gender
                Case Pokemon.Genders.Male
                    Return New Size(32, 32)
                Case Else
                    Return New Size(32, 32)
            End Select
        End Function

        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.Gender
                Case Pokemon.Genders.Male
                    Return P.OriginalName & "_male"
                Case Else
                    Return P.OriginalName & "_female"
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.Gender
                Case Pokemon.Genders.Male
                    Return "_male"
                Case Else
                    Return "_female"
            End Select
        End Function
    End Class

    Private Class Flabebe
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(669)
        End Sub

        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "yellow"
                    Return New Vector2(17, 27)
                Case "blue"
                    Return New Vector2(18, 27)
                Case "orange"
                    Return New Vector2(19, 27)
                Case "white"
                    Return New Vector2(20, 27)
                Case Else
                    Return MyBase.GetMenuImagePosition(P)
            End Select
        End Function

        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "yellow"
                    Return "flabebe_yellow"
                Case "blue"
                    Return "flabebe_blue"
                Case "orange"
                    Return "flabebe_orange"
                Case "white"
                    Return "flabebe_white"
                Case Else
                    Return "flabebe"
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "yellow"
                    Return "_yellow"
                Case "blue"
                    Return "_blue"
                Case "orange"
                    Return "_orange"
                Case "white"
                    Return "_white"
                Case Else
                    Return "_red"
            End Select
        End Function

    End Class
    Private Class Floette
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(670)
        End Sub

        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "yellow"
                    Return New Vector2(21, 27)
                Case "blue"
                    Return New Vector2(22, 27)
                Case "orange"
                    Return New Vector2(23, 27)
                Case "white"
                    Return New Vector2(24, 27)
                Case "eternal"
                    Return New Vector2(28, 31)
                Case Else
                    Return MyBase.GetMenuImagePosition(P)
            End Select
        End Function

        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "yellow"
                    Return P.OriginalName & "_yellow"
                Case "blue"
                    Return P.OriginalName & "_blue"
                Case "orange"
                    Return P.OriginalName & "_orange"
                Case "white"
                    Return P.OriginalName & "_white"
                Case "eternal"
                    Return P.OriginalName & "_eternal"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "yellow"
                    Return "_yellow"
                Case "blue"
                    Return "_blue"
                Case "orange"
                    Return "_orange"
                Case "white"
                    Return "_white"
                Case "eternal"
                    Return "_eternal"
                Case Else
                    Return "_red"
            End Select
        End Function

        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData
                Case "eternal"
                    Return "_eternal"
                Case Else
                    Return ""
            End Select
        End Function

    End Class
    Private Class Florges
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(671)
        End Sub

        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "yellow"
                    Return New Vector2(25, 27)
                Case "blue"
                    Return New Vector2(26, 27)
                Case "orange"
                    Return New Vector2(27, 27)
                Case "white"
                    Return New Vector2(28, 27)
                Case Else
                    Return MyBase.GetMenuImagePosition(P)
            End Select
        End Function

        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "yellow"
                    Return P.OriginalName & "_yellow"
                Case "blue"
                    Return P.OriginalName & "_blue"
                Case "orange"
                    Return P.OriginalName & "_orange"
                Case "white"
                    Return P.OriginalName & "_white"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "yellow"
                    Return "_yellow"
                Case "blue"
                    Return "_blue"
                Case "orange"
                    Return "_orange"
                Case "white"
                    Return "_white"
                Case Else
                    Return "_red"
            End Select
        End Function

    End Class

    Private Class Aegislash

        Inherits PokemonForm

        Public Sub New()
            MyBase.New(681)
        End Sub

        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "blade"
                    Return New Vector2(20, 30)
                Case Else
                    Return New Vector2(8, 21)
            End Select
        End Function

        Public Overrides Function GetMenuImageSize(ByVal P As BasePokemon) As Size
            Select Case P.AdditionalData
                Case "blade"
                    Return New Size(35, 32)
                Case Else
                    Return New Size(32, 32)
            End Select
        End Function

        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "blade"
                    Return P.OriginalName & "_blade"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
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

    Private Class Diancie
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(719)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "mega"
                    Return New Vector2(25, 25)
                Case Else
                    Return New Vector2(14, 22)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return P.OriginalName & "_mega"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "mega"
                    Return "_mega"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Private Class Hoopa
        Inherits PokemonForm
        Public Sub New()
            MyBase.New(720)
        End Sub
        Public Overrides Function GetMenuImagePosition(ByVal P As BasePokemon) As Vector2
            Select Case P.AdditionalData
                Case "unbound"
                    Return New Vector2(23, 25)
                Case Else
                    Return MyBase.GetMenuImagePosition(P)
            End Select
        End Function
        Public Overrides Function GetMenuImageSize(ByVal P As BasePokemon) As Size
            Select Case P.AdditionalData
                Case "unbound"
                    Return New Size(38, 32)
                Case Else
                    Return New Size(32, 32)
            End Select
        End Function
        Public Overrides Function GetDataFileAddition(ByVal AdditionalData As String) As String
            Select Case AdditionalData.ToLower()
                Case "unbound"
                    Return "_unbound"
                Case Else
                    Return ""
            End Select
        End Function
        Public Overrides Function GetAnimationName(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "unbound"
                    Return P.OriginalName & "_unbound"
                Case Else
                    Return P.OriginalName
            End Select
        End Function

        Public Overrides Function GetOverworldAddition(ByVal P As BasePokemon) As String
            Select Case P.AdditionalData
                Case "unbound"
                    Return "_unbound"
                Case Else
                    Return ""
            End Select
        End Function
    End Class
#End Region

End Class
