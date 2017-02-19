Imports P3D.Legacy.Core.Pokemon

Namespace Items.Apricorns

    <Item(92, "Yellow Apricorn")>
    Public Class YlwApricorn

        Inherits Apricorn

        Public Overrides ReadOnly Property Description As String = "A yellow Apricorn. It has an invigorating scent."

        Public Sub New()
            TextureRectangle = New Rectangle(384, 72, 24, 24)
        End Sub

    End Class

End Namespace
