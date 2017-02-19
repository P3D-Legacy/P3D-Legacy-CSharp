Imports P3D.Legacy.Core.Pokemon
Namespace Items.MegaStones

    ''' <summary>
    ''' The Mega Stone for Medicham.
    ''' </summary>
    <Item(528, "Medichamite")>
    Public Class Medichamite

        Inherits MegaStone

        Public Sub New()
            MyBase.New("Medicham", 308)
            TextureRectangle = New Rectangle(24, 48, 24, 24)
        End Sub

    End Class

End Namespace
