Imports P3D.Legacy.Core.Pokemon
Namespace Items.Mail

    <Item(320, "Snow Mail")>
    Public Class SnowMail

        Inherits MailItem

        Public Overrides ReadOnly Property Description As String = "Stationery featuring a print of a chilly, snow-covered world. Let a Pokémon hold it for delivery."

        Public Sub New()
            TextureRectangle = New Rectangle(480, 456, 24, 24)
        End Sub

    End Class

End Namespace
