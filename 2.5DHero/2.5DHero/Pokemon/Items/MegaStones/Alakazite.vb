Imports P3D.Legacy.Core.Pokemon
Namespace Items.MegaStones

    ''' <summary>
    ''' The Mega Stone for Alakazam.
    ''' </summary>
    <Item(511, "Alakazite")>
    Public Class Alakazite

        Inherits MegaStone

        Public Sub New()
            MyBase.New("Alakazam", 65)
            TextureRectangle = New Rectangle(96, 0, 24, 24)
        End Sub

    End Class

End Namespace
