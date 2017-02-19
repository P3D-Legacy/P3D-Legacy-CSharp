Imports P3D.Legacy.Core.Pokemon

Namespace Items.Apricorns

    <Item(97, "White Apricorn")>
    Public Class WhtApricorn

        Inherits Apricorn

        Public Overrides ReadOnly Property Description As String = "A white Apricorn. It doesn't smell like anything."

        Public Sub New()
            TextureRectangle = New Rectangle(0, 96, 24, 24)
        End Sub

    End Class

End Namespace
