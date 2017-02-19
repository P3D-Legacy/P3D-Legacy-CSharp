Imports P3D.Legacy.Core.Pokemon
Namespace Items.MegaStones

    ''' <summary>
    ''' The Mega Stone for Latias.
    ''' </summary>
    <Item(542, "Latiasite")>
    Public Class Latiasite

        Inherits MegaStone

        Public Sub New()
            MyBase.New("Latias", 380)
            TextureRectangle = New Rectangle(168, 72, 24, 24)
        End Sub

    End Class

End Namespace
