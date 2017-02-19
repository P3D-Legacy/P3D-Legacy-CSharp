Imports P3D.Legacy.Core.Pokemon

Namespace Items.Stones

    <Item(23, "Thunder Stone")>
    Public Class ThunderStone

        Inherits StoneItem

        Public Overrides ReadOnly Property Description As String = "A peculiar stone that can make certain species of Pokémon evolve. It has a distinct thunderbolt pattern."

        Public Sub New()
            TextureRectangle = New Rectangle(0, 24, 24, 24)
        End Sub

    End Class

End Namespace
