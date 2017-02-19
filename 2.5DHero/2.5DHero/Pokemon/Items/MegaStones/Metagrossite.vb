Imports P3D.Legacy.Core.Pokemon
Namespace Items.MegaStones

    ''' <summary>
    ''' The Mega Stone for Metagross.
    ''' </summary>
    <Item(545, "Metagrossite")>
    Public Class Metagrossite

        Inherits MegaStone

        Public Sub New()
            MyBase.New("Metagross", 376)
            TextureRectangle = New Rectangle(0, 96, 24, 24)
        End Sub

    End Class

End Namespace
