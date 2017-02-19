Imports P3D.Legacy.Core.Pokemon
Namespace Items.Mail

    <Item(329, "Favored Mail")>
    Public Class FavoredMail

        Inherits MailItem

        Public Overrides ReadOnly Property Description As String = "Stationary designed for writing about your favorite things. Let a Pokémon hold it for delivery."

        Public Sub New()
            TextureRectangle = New Rectangle(192, 480, 24, 24)
        End Sub

    End Class

End Namespace
