Imports P3D.Legacy.Core.Pokemon
Namespace Items.Mail

    <Item(304, "Glitter Mail")>
    Public Class GlitterMail

        Inherits MailItem

        Public Overrides ReadOnly Property Description As String = "A Pikachu-print Mail to be held by a Pokémon."

        Public Sub New()
            TextureRectangle = New Rectangle(72, 456, 24, 24)
        End Sub

    End Class

End Namespace
