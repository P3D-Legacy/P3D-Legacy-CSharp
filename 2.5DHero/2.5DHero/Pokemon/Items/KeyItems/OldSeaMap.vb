Imports P3D.Legacy.Core.Pokemon

Namespace Items.KeyItems

    <Item(285, "Old Sea Map")>
    Public Class OldSeaMap

        Inherits KeyItem

        Public Overrides ReadOnly Property Description As String = "A faded sea chart that shows the way to a certain island."

        Public Sub New()
            TextureRectangle = New Rectangle(168, 264, 24, 24)
        End Sub

    End Class

End Namespace
