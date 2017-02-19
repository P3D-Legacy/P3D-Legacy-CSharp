Imports P3D.Legacy.Core.Pokemon
Namespace Items.MegaStones

    ''' <summary>
    ''' The Mega Stone for Gallade.
    ''' </summary>
    <Item(540, "Galladite")>
    Public Class Galladite

        Inherits MegaStone

        Public Sub New()
            MyBase.New("Gallade", 475)
            TextureRectangle = New Rectangle(120, 72, 24, 24)
        End Sub

    End Class

End Namespace
