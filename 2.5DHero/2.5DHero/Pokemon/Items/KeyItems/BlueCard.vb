Imports P3D.Legacy.Core.Pokemon

Namespace Items.KeyItems

    <Item(116, "Blue Card")>
    Public Class BlueCard

        Inherits KeyItem

        Public Overrides ReadOnly Property Description As String = "A card to save points for the Buena's Password show."

        Public Sub New()
            TextureRectangle = New Rectangle(408, 96, 24, 24)
        End Sub

    End Class

End Namespace
