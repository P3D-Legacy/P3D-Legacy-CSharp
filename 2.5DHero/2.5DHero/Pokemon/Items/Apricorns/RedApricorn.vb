Imports P3D.Legacy.Core.Pokemon

Namespace Items.Apricorns

    <Item(85, "Red Apricorn")>
    Public Class RedApricorn

        Inherits Apricorn

        Public Overrides ReadOnly Property Description As String = "A red Apricorn. It assails your nostrils."

        Public Sub New()
            TextureRectangle = New Rectangle(240, 72, 24, 24)
        End Sub

    End Class

End Namespace
