Imports P3D.Legacy.Core.Pokemon
Namespace Items.MegaStones

    ''' <summary>
    ''' The Mega Stone for Pidgeot.
    ''' </summary>
    <Item(546, "Pidgeotite")>
    Public Class Pidgeotite

        Inherits MegaStone

        Public Sub New()
            MyBase.New("Pidgeot", 18)
            TextureRectangle = New Rectangle(24, 96, 24, 24)
        End Sub

    End Class

End Namespace
