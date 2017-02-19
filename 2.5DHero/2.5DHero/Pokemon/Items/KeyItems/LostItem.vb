Imports P3D.Legacy.Core.Pokemon

Namespace Items.KeyItems

    <Item(130, "Lost Item")>
    Public Class LostItem

        Inherits KeyItem

        Public Overrides ReadOnly Property Description As String = "The Poké Doll lost by the Copycat."

        Public Sub New()
            TextureRectangle = New Rectangle(216, 120, 24, 24)
        End Sub

    End Class

End Namespace
