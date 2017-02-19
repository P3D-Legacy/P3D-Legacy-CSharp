Imports P3D.Legacy.Core.Pokemon
Namespace Items.MegaStones

    ''' <summary>
    ''' The Mega Stone for Gardevoir.
    ''' </summary>
    <Item(519, "Gardevoirite")>
    Public Class Gardevoirite

        Inherits MegaStone

        Public Sub New()
            MyBase.New("Gardevoir", 282)
            TextureRectangle = New Rectangle(48, 24, 24, 24)
        End Sub

    End Class

End Namespace
