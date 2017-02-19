Imports P3D.Legacy.Core.Pokemon

Namespace Items.KeyItems

    <Item(78, "Running Shoes")>
    Public Class RunningShoes

        Inherits KeyItem

        Public Overrides ReadOnly Property Description As String = "Special high-quality shoes. Instructions: Hold SHIFT to run!"

        Public Sub New()
            TextureRectangle = New Rectangle(288, 216, 24, 24)
        End Sub

    End Class

End Namespace
