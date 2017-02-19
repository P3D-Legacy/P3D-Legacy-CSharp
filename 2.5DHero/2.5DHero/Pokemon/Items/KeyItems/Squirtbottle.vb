Imports P3D.Legacy.Core.Pokemon

Namespace Items.KeyItems

    <Item(175, "SquirtBottle")>
    Public Class Squirtbottle

        Inherits KeyItem

        Public Overrides ReadOnly Property Description As String = "A bottle used for watering plants in Loamy Soil."

        Public Sub New()
            TextureRectangle = New Rectangle(360, 144, 24, 24)
        End Sub

    End Class

End Namespace
