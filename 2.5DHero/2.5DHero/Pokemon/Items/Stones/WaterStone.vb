Imports P3D.Legacy.Core.Pokemon

Namespace Items.Stones

    <Item(24, "Water Stone")>
    Public Class WaterStone

        Inherits StoneItem

        Public Overrides ReadOnly Property Description As String = "A peculiar stone that can make certain species of Pokémon evolve. It is the blue of a pool of clear water."

        Public Sub New()
            TextureRectangle = New Rectangle(24, 24, 24, 24)
        End Sub

    End Class

End Namespace
