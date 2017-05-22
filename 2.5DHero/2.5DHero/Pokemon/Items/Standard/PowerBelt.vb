Imports P3D.Legacy.Core.Pokemon
Namespace Items.Standard

    <Item(584, "Power Belt")>
    Public Class PowerBelt

        Inherits Item

        Public Overrides ReadOnly Property Description As String = "An item to be held by a Pokémon. It reduces Speed but allows the holder's Defense stat to grow more after battling."
        Public Overrides ReadOnly Property PokeDollarPrice As Integer = 3000
        Public Overrides ReadOnly Property CanBeUsedInBattle As Boolean = False
        Public Overrides ReadOnly Property CanBeUsed As Boolean = False

        Public Sub New()
            TextureRectangle = New Rectangle(168, 288, 24, 24)
        End Sub

    End Class

End Namespace
