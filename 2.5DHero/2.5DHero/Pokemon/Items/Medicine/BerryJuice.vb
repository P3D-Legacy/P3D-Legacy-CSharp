Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.Resources.Managers
Imports P3D.Legacy.Core.Screens

Namespace Items.Medicine

    <Item(139, "Berry Juice")>
    Public Class BerryJuice

        Inherits MedicineItem

        Public Overrides ReadOnly Property PokeDollarPrice As Integer = 100
        Public Overrides ReadOnly Property Description As String = "A 100 percent pure juice made of Berries. When consumed, it restores 20 HP to an injured Pokémon."
        Public Overrides ReadOnly Property IsHealingItem As Boolean = True

        Public Sub New()
            TextureRectangle = New Rectangle(360, 120, 24, 24)
        End Sub

        Public Overrides Sub Use()
            If GameModeManager.GetActiveGameRuleValueOrDefault("CanUseHealItem", True) = False Then
                Screen.TextBox.Show("Cannot use heal items.", {}, False, False)
                Exit Sub
            End If
            Core.SetScreen(New ChoosePokemonScreen(Core.CurrentScreen, Me, AddressOf Me.UseOnPokemon, "Use " & Me.Name, True))
        End Sub

        Public Overrides Function UseOnPokemon(ByVal PokeIndex As Integer) As Boolean
            Return HealPokemon(PokeIndex, 20)
        End Function

    End Class

End Namespace
