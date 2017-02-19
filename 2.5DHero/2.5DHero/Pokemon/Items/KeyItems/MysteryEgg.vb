Imports P3D.Legacy.Core.Pokemon

Namespace Items.KeyItems

    <Item(69, "Mystery Egg")>
    Public Class MysteryEgg

        Inherits KeyItem

        Public Overrides ReadOnly Property Description As String = "A mysterious Egg obtained from Mr. Pokémon. What's in the Egg is unknown."

        Public Sub New()
            TextureRectangle = New Rectangle(0, 72, 24, 24)
        End Sub

    End Class

End Namespace
