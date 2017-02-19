Imports P3D.Legacy.Core.Pokemon
Namespace Items.Mail

    <Item(324, "BridgeMail T")>
    Public Class BridgeMailT

        Inherits MailItem

        Public Overrides ReadOnly Property Description As String = "Stationery featuring a print of a steel suspension bridge. Let a Pokémon hold it for use."

        Public Sub New()
            TextureRectangle = New Rectangle(72, 480, 24, 24)
        End Sub

    End Class

End Namespace
