Imports P3D.Legacy.Core.Pokemon
Namespace Items.Mail

    <Item(305, "Harbor Mail")>
    Public Class HarborMail

        Inherits MailItem

        Public Overrides ReadOnly Property Description As String = "A Wingull-print Mail to be held by a Pokémon."

        Public Sub New()
            TextureRectangle = New Rectangle(96, 456, 24, 24)
        End Sub

    End Class

End Namespace
