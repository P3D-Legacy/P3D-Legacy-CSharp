Imports P3D.Legacy.Core.Pokemon
Namespace Items.Standard

    <Item(585, "Power Lens")>
    Public Class PowerLens

        Inherits Item

        Public Overrides ReadOnly Property Description As String = "An item to be held by a Pokémon. It reduces Speed but allows the holder's Sp. Atk stat to grow more after battling."
        Public Overrides ReadOnly Property PokeDollarPrice As Integer = 3000
        Public Overrides ReadOnly Property CanBeUsedInBattle As Boolean = False
        Public Overrides ReadOnly Property CanBeUsed As Boolean = False

        Public Sub New()
            TextureRectangle = New Rectangle(192, 384, 24, 24)
        End Sub

    End Class

End Namespace
