Imports P3D.Legacy.Core.Pokemon

Namespace Items.KeyItems

    <Item(71, "Silver Wing")>
    Public Class SilverWing

        Inherits KeyItem

        Public Overrides ReadOnly Property Description As String = "A strange, silvery feather that sparkles."

        Public Sub New()
            TextureRectangle = New Rectangle(48, 72, 24, 24)
        End Sub

    End Class

End Namespace
