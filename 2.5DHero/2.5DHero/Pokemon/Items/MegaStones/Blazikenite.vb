Imports P3D.Legacy.Core.Pokemon
Namespace Items.MegaStones

    ''' <summary>
    ''' The Mega Stone for Blaziken.
    ''' </summary>
    <Item(515, "Blazikenite")>
    Public Class Blazikenite

        Inherits MegaStone

        Public Sub New()
            MyBase.New("Blaziken", 257)
            TextureRectangle = New Rectangle(192, 0, 24, 24)
        End Sub

    End Class

End Namespace
