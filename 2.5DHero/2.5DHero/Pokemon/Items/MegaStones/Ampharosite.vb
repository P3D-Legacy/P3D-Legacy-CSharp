Imports P3D.Legacy.Core.Pokemon
Namespace Items.MegaStones

    ''' <summary>
    ''' The Mega Stone for Ampharos.
    ''' </summary>
    <Item(512, "Ampharosite")>
    Public Class Ampharosite

        Inherits MegaStone

        Public Sub New()
            MyBase.New("Ampharos", 181)
            TextureRectangle = New Rectangle(120, 0, 24, 24)
        End Sub

    End Class

End Namespace
