Imports P3D.Legacy.Core.Pokemon
Namespace Items.MegaStones

    ''' <summary>
    ''' The Mega Stone for Aerodactyl.
    ''' </summary>
    <Item(509, "Aerodactylite")>
    Public Class Aerodactylite

        Inherits MegaStone

        Public Sub New()
            MyBase.New("Aerodactyl", 142)
            TextureRectangle = New Rectangle(48, 0, 24, 24)
        End Sub

    End Class

End Namespace
