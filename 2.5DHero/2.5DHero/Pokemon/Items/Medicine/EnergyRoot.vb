Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.Screens

Namespace Items.Medicine

    <Item(122, "Energy Root")>
    Public Class EnergyRoot

        Inherits MedicineItem

        Public Overrides ReadOnly Property PokeDollarPrice As Integer = 800
        Public Overrides ReadOnly Property Description As String = "An extremely bitter medicinal root. When consumed, it restores 200 HP to an injured Pokémon."
        Public Overrides ReadOnly Property IsHealingItem As Boolean = True

        Public Sub New()
            TextureRectangle = New Rectangle(24, 120, 24, 24)
        End Sub

        Public Overrides Sub Use()
            If GameModeManager.GetActiveGameRuleValueOrDefault("CanUseHealItem", True) = False Then
                Screen.TextBox.Show("Cannot use heal items.", {}, False, False)
                Exit Sub
            End If
            Core.SetScreen(New ChoosePokemonScreen(Core.CurrentScreen, Me, AddressOf Me.UseOnPokemon, "Use " & Me.Name, True))
        End Sub

        Public Overrides Function UseOnPokemon(ByVal PokeIndex As Integer) As Boolean
            Dim r As Boolean = HealPokemon(PokeIndex, 200)
            If r = True Then
                Core.Player.Pokemons(PokeIndex).ChangeFriendShip(BasePokemon.FriendShipCauses.EnergyRoot)
            End If
            Return r
        End Function

    End Class

End Namespace
