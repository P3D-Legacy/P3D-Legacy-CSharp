Imports P3D.Legacy.Core.Pokemon
Namespace Items.MegaStones

    ''' <summary>
    ''' The Mega Stone for Houndoomin.
    ''' </summary>
    <Item(523, "Houndoominite")>
    Public Class Houndoominite

        Inherits MegaStone

        Public Sub New()
            MyBase.New("Houndoom", 229)
            TextureRectangle = New Rectangle(144, 24, 24, 24)
        End Sub

    End Class

End Namespace
