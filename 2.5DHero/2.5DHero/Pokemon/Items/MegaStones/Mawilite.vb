Imports P3D.Legacy.Core.Pokemon
Namespace Items.MegaStones

    ''' <summary>
    ''' The Mega Stone for Mawile.
    ''' </summary>
    <Item(527, "Mawilite")>
    Public Class Mawilite

        Inherits MegaStone

        Public Sub New()
            MyBase.New("Mawile", 303)
            TextureRectangle = New Rectangle(0, 48, 24, 24)
        End Sub

    End Class

End Namespace
