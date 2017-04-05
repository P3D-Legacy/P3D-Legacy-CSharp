Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.Resources.Managers
Imports P3D.Legacy.Core.Resources.Managers.Sound
Imports P3D.Legacy.Core.Resources.Sound
Imports P3D.Legacy.Core.Screens

Namespace Items.Medicine

    <Item(14, "Full Restore")>
    Public Class FullRestore

        Inherits MedicineItem

        Public Overrides ReadOnly Property IsHealingItem As Boolean = True
        Public Overrides ReadOnly Property Description As String = "A medicine that can be used to fully restore the HP of a single Pokémon and heal any status conditions it has."
        Public Overrides ReadOnly Property PokeDollarPrice As Integer = 3000

        Public Sub New()
            TextureRectangle = New Rectangle(288, 0, 24, 24)
        End Sub

        Public Overrides Sub Use()
            If GameModeManager.GetActiveGameRuleValueOrDefault("CanUseHealItem", True) = False Then
                Screen.TextBox.Show("Cannot use heal items.", {}, False, False)
                Exit Sub
            End If
            Core.SetScreen(New ChoosePokemonScreen(Core.CurrentScreen, Me, AddressOf Me.UseOnPokemon, "Use " & Me.Name, True))
        End Sub

        Public Overrides Function UseOnPokemon(ByVal PokeIndex As Integer) As Boolean
            Dim p As Pokemon = Core.Player.Pokemons(PokeIndex)

            Dim v1 As Boolean = HealPokemon(PokeIndex, p.MaxHP)
            Dim v2 As Boolean = False

            If p.Status <> BasePokemon.StatusProblems.Fainted And p.Status <> BasePokemon.StatusProblems.None Or p.HasVolatileStatus(BasePokemon.VolatileStatus.Confusion) = True Then
                If p.HasVolatileStatus(BasePokemon.VolatileStatus.Confusion) = True Then
                    p.RemoveVolatileStatus(BasePokemon.VolatileStatus.Confusion)
                End If

                p.Status = BasePokemon.StatusProblems.None
                v2 = True
                If v1 = False Then
                    Dim t As String = "Healed " & p.GetDisplayName() & "!"
                    t &= RemoveItem()

                    SoundEffectManager.PlaySound("single_heal", False)
                    Screen.TextBox.Show(t, {})
                End If
            End If

            If v2 = True Or v1 = True Then
                PlayerStatistics.Track("[17]Medicine Items used", 1)
                Return True
            End If

            Return False
        End Function

    End Class

End Namespace
