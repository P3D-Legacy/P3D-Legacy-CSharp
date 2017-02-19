Imports P3D.Legacy.Core.Pokemon
Namespace Items.Mail

    <Item(322, "Steel Mail")>
    Public Class SteelMail

        Inherits MailItem

        Public Overrides ReadOnly Property Description As String = "Stationery featuring a print of cool mechanical designs. Let a Pokémon hold it for delivery."

        Public Sub New()
            TextureRectangle = New Rectangle(24, 480, 24, 24)
        End Sub

    End Class

End Namespace
