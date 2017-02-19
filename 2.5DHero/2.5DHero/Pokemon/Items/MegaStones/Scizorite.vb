Imports P3D.Legacy.Core.Pokemon
Namespace Items.MegaStones

    ''' <summary>
    ''' The Mega Stone for Scizor.
    ''' </summary>
    <Item(532, "Scizorite")>
    Public Class Scizorite

        Inherits MegaStone

        Public Sub New()
            MyBase.New("Scizor", 212)
            TextureRectangle = New Rectangle(120, 48, 24, 24)
        End Sub

    End Class

End Namespace
