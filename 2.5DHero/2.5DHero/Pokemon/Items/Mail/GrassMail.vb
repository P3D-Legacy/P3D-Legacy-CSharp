Imports P3D.Legacy.Core.Pokemon
Namespace Items.Mail

    <Item(300, "Grass Mail")>
    Public Class GrassMail

        Inherits MailItem

        Public Overrides ReadOnly Property Description As String = "Let a Pokémon hold it for delivery."

        Public Sub New()
            TextureRectangle = New Rectangle(408, 456, 24, 24)
        End Sub

    End Class

End Namespace
