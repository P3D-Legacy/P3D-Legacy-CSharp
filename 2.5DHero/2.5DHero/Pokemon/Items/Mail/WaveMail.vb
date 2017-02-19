Imports P3D.Legacy.Core.Pokemon
Namespace Items.Mail

    <Item(311, "Wave Mail")>
    Public Class WaveMail

        Inherits MailItem

        Public Overrides ReadOnly Property Description As String = "A Wailmer-print Mail to be held by a Pokémon."

        Public Sub New()
            TextureRectangle = New Rectangle(240, 456, 24, 24)
        End Sub

    End Class

End Namespace
