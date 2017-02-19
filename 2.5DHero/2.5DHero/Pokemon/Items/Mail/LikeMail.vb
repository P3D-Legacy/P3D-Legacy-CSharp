Imports P3D.Legacy.Core.Pokemon
Namespace Items.Mail

    <Item(334, "Like Mail")>
    Public Class LikeMail

        Inherits MailItem

        Public Overrides ReadOnly Property Description As String = "Stationary designed for writing recommendations. Let a Pokémon hold it for delivery."

        Public Sub New()
            TextureRectangle = New Rectangle(312, 480, 24, 24)
        End Sub

    End Class

End Namespace
