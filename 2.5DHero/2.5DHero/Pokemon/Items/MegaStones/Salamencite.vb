Imports P3D.Legacy.Core.Pokemon
Namespace Items.MegaStones

    ''' <summary>
    ''' The Mega Stone for Salamence.
    ''' </summary>
    <Item(548, "Salamencite")>
    Public Class Salamencite

        Inherits MegaStone

        Public Sub New()
            MyBase.New("Salamence", 373)
            TextureRectangle = New Rectangle(72, 96, 24, 24)
        End Sub

    End Class

End Namespace
