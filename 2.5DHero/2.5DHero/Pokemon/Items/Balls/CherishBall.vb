Imports P3D.Legacy.Core.Pokemon

Namespace Items.Balls

    <Item(45, "Cherish Ball")>
    Public Class CherishBall

        Inherits BallItem

        Public Overrides ReadOnly Property Description As String = "A quite rare Pokéball that has been specially crafted to commemorate an occasion of some sort."

        Public Sub New()
            TextureRectangle = New Rectangle(216, 192, 24, 24)
        End Sub

    End Class

End Namespace
