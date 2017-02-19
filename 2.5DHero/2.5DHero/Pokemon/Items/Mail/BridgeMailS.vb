Imports P3D.Legacy.Core.Pokemon
Namespace Items.Mail

    <Item(326, "BridgeMail S")>
    Public Class BridgeMailS

        Inherits MailItem

        Public Overrides ReadOnly Property Description As String = "Stationery featuring a print of a sky-piercing bridge. Let a Pokémon hold it for use."

        Public Sub New()
            TextureRectangle = New Rectangle(120, 480, 24, 24)
        End Sub

    End Class

End Namespace
