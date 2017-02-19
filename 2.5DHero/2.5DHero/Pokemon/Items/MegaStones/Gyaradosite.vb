Imports P3D.Legacy.Core.Pokemon
Namespace Items.MegaStones

    ''' <summary>
    ''' The Mega Stone for Gyarados.
    ''' </summary>
    <Item(521, "Gyaradosite")>
    Public Class Gyaradosite

        Inherits MegaStone

        Public Sub New()
            MyBase.New("Gyarados", 130)
            TextureRectangle = New Rectangle(96, 24, 24, 24)
        End Sub

    End Class

End Namespace
