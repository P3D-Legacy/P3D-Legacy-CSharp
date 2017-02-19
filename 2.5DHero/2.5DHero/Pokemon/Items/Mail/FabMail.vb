Imports P3D.Legacy.Core.Pokemon
Namespace Items.Mail

    <Item(303, "Fab Mail")>
    Public Class FabMail

        Inherits MailItem

        Public Overrides ReadOnly Property Description As String = "A gorgeous-print Mail to be held by a Pokémon."

        Public Sub New()
            TextureRectangle = New Rectangle(48, 456, 24, 24)
        End Sub

    End Class

End Namespace
