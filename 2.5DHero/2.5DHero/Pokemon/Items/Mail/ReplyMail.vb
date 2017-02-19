Imports P3D.Legacy.Core.Pokemon
Namespace Items.Mail

    <Item(335, "Reply Mail")>
    Public Class ReplyMail

        Inherits MailItem

        Public Overrides ReadOnly Property Description As String = "Stationary designed for writing a reply. Let a Pokémon hold it for delivery."

        Public Sub New()
            TextureRectangle = New Rectangle(336, 480, 24, 24)
        End Sub

    End Class

End Namespace
