Imports P3D.Legacy.Core.Pokemon
Namespace Items.MegaStones

    ''' <summary>
    ''' The Mega Stone for Latios.
    ''' </summary>
    <Item(543, "Latiosite")>
    Public Class Latiosite

        Inherits MegaStone

        Public Sub New()
            MyBase.New("Latios", 381)
            TextureRectangle = New Rectangle(192, 72, 24, 24)
        End Sub

    End Class

End Namespace
