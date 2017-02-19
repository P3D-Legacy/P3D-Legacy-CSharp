Imports P3D.Legacy.Core.Pokemon
Namespace Items.MegaStones

    ''' <summary>
    ''' The Mega Stone for Sceptile.
    ''' </summary>
    <Item(549, "Sceptilite")>
    Public Class Sceptilite

        Inherits MegaStone

        Public Sub New()
            MyBase.New("Sceptile", 254)
            TextureRectangle = New Rectangle(96, 96, 24, 24)
        End Sub

    End Class

End Namespace
