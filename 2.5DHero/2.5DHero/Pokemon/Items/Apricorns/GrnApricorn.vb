Imports P3D.Legacy.Core.Pokemon

Namespace Items.Apricorns

    <Item(93, "Green Apricorn")>
    Public Class GrnApricorn

        Inherits Apricorn

        Public Overrides ReadOnly Property Description As String = "A green Apricorn. It has a mysterious, aromatic scent."

        Public Sub New()
            TextureRectangle = New Rectangle(408, 72, 24, 24)
        End Sub

    End Class

End Namespace
