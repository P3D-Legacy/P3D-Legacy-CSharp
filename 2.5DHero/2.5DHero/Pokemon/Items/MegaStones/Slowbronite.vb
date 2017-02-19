Imports P3D.Legacy.Core.Pokemon
Namespace Items.MegaStones

    ''' <summary>
    ''' The Mega Stone for Slowbro.
    ''' </summary>
    <Item(551, "Slowbronite")>
    Public Class Slowbronite

        Inherits MegaStone

        Public Sub New()
            MyBase.New("Slowbro", 80)
            TextureRectangle = New Rectangle(144, 96, 24, 24)
        End Sub

    End Class

End Namespace
