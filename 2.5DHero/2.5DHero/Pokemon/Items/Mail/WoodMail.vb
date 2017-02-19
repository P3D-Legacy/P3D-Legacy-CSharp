Imports P3D.Legacy.Core.Pokemon
Namespace Items.Mail

    <Item(312, "Wood Mail")>
    Public Class WoodMail

        Inherits MailItem

        Public Overrides ReadOnly Property Description As String = "A Slakoth-print Mail to be held by a Pokémon."

        Public Sub New()
            TextureRectangle = New Rectangle(264, 456, 24, 24)
        End Sub

    End Class

End Namespace
