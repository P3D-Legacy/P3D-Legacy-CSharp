Imports P3D.Legacy.Core.Pokemon
Namespace Items.MegaStones

    ''' <summary>
    ''' The Mega Stone for Sharpedo.
    ''' </summary>
    <Item(550, "Sharpedonite")>
    Public Class Sharpedonite

        Inherits MegaStone

        Public Sub New()
            MyBase.New("Sharpedo", 319)
            TextureRectangle = New Rectangle(120, 96, 24, 24)
        End Sub

    End Class

End Namespace
