Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.Resources.Managers
Imports P3D.Legacy.Core.Resources.Managers.Sound
Imports P3D.Legacy.Core.Resources.Sound
Imports P3D.Legacy.Core.Screens

Namespace Items.Medicine

    <Item(40, "Max Revive")>
    Public Class MaxRevive

        Inherits Item

        Public Overrides ReadOnly Property IsHealingItem As Boolean = True
        Public Overrides ReadOnly Property Description As String = "A medicine that can revive fainted Pokémon. It also fully restores a fainted Pokémon's maximum HP."
        Public Overrides ReadOnly Property PokeDollarPrice As Integer = 4000

        Public Sub New()
            TextureRectangle = New Rectangle(384, 24, 24, 24)
        End Sub

        Public Overrides Sub Use()
            If GameModeManager.GetActiveGameRuleValueOrDefault("CanUseHealItem", True) = False Then
                Screen.TextBox.Show("Cannot use heal items.", {}, False, False)
                Exit Sub
            End If
            Core.SetScreen(New ChoosePokemonScreen(Core.CurrentScreen, Me, AddressOf Me.UseOnPokemon, "Use " & Me.Name, True))
        End Sub

        Public Overrides Function UseOnPokemon(ByVal PokeIndex As Integer) As Boolean
            Dim Pokemon As Pokemon = Core.Player.Pokemons(PokeIndex)

            If Pokemon.Status = BasePokemon.StatusProblems.Fainted Then
                Pokemon.Status = BasePokemon.StatusProblems.None
                Pokemon.HP = Pokemon.MaxHP

                SoundEffectManager.PlaySound("single_heal", False)
                Screen.TextBox.Show(Pokemon.GetDisplayName() & "~is revitalized.", {}, False, False)
                PlayerStatistics.Track("[17]Medicine Items used", 1)

                RemoveItem()

                Return True
            Else
                Screen.TextBox.Show("Cannot use Max Revive~on this Pokémon.", {}, False, False)

                Return False
            End If
        End Function

    End Class

End Namespace
