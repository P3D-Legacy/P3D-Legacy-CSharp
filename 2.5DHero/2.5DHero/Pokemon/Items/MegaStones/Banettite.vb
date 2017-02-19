Imports P3D.Legacy.Core.Pokemon
Namespace Items.MegaStones

    ''' <summary>
    ''' The Mega Stone for Banette.
    ''' </summary>
    <Item(513, "Banettite")>
    Public Class Banettite

        Inherits MegaStone

        Public Sub New()
            MyBase.New("Banette", 354)
            TextureRectangle = New Rectangle(144, 0, 24, 24)
        End Sub

    End Class

End Namespace
