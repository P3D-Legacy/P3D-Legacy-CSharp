Imports P3D.Legacy.Core.Pokemon
Namespace Items.Mail

    <Item(318, "Heart Mail")>
    Public Class HeartMail

        Inherits MailItem

        Public Overrides ReadOnly Property Description As String = "Stationery featuring a print of giant heart patterns. Let a Pokémon hold it for delivery."

        Public Sub New()
            TextureRectangle = New Rectangle(432, 456, 24, 24)
        End Sub

    End Class

End Namespace
