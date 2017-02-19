Imports P3D.Legacy.Core.Pokemon
Namespace Items.Standard

    <Item(297, "Light Clay")>
    Public Class LightClay

        Inherits Item

        Public Overrides ReadOnly Property Description As String = "An item to be held by a Pokémon. Protective moves like Light Screen and Reflect will be effective longer."
        Public Overrides ReadOnly Property PokeDollarPrice As Integer = 100
        Public Overrides ReadOnly Property CanBeUsedInBattle As Boolean = False
        Public Overrides ReadOnly Property CanBeUsed As Boolean = False

        Public Sub New()
            TextureRectangle = New Rectangle(456, 264, 24, 24)
        End Sub

    End Class

End Namespace
