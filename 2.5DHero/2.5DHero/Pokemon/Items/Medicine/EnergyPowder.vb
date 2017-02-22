Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.Screens

Namespace Items.Medicine

    <Item(121, "Energy Powder")>
    Public Class EnergyPowder

        Inherits MedicineItem

        Public Overrides ReadOnly Property PokeDollarPrice As Integer = 500
        Public Overrides ReadOnly Property Description As String = "A bitter medicine powder. When consumed, it restores 50 HP to an injured Pokémon."
        Public Overrides ReadOnly Property IsHealingItem As Boolean = True

        Public Sub New()
            TextureRectangle = New Rectangle(0, 120, 24, 24)
        End Sub

        Public Overrides Sub Use()
            If GameModeManager.GetActiveGameRuleValueOrDefault("CanUseHealItem", True) = False Then
                Screen.TextBox.Show("Cannot use heal items.", {}, False, False)
                Exit Sub
            End If
            Core.SetScreen(New ChoosePokemonScreen(Core.CurrentScreen, Me, AddressOf Me.UseOnPokemon, "Use " & Me.Name, True))
        End Sub

        Public Overrides Function UseOnPokemon(ByVal PokeIndex As Integer) As Boolean
            Dim success As Boolean = HealPokemon(PokeIndex, 50)
            If success Then
                Core.Player.Pokemons(PokeIndex).ChangeFriendShip(BasePokemon.FriendShipCauses.EnergyPowder)
            End If
            Return success
        End Function

    End Class

End Namespace
