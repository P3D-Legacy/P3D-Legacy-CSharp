Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.Resources.Sound
Imports P3D.Legacy.Core.Screens

Namespace Items.Medicine

    <Item(124, "Revival Herb")>
    Public Class RevivalHerb

        Inherits Item

        Public Overrides ReadOnly Property IsHealingItem As Boolean = True
        Public Overrides ReadOnly Property Description As String = "A terribly bitter medicinal herb. It revives a fainted Pokémon and fully restores its maximum HP."
        Public Overrides ReadOnly Property PokeDollarPrice As Integer = 2800

        Public Sub New()
            TextureRectangle = New Rectangle(72, 120, 24, 24)
        End Sub

        Public Overrides Sub Use()
            If CBool(GameModeManager.GetGameRuleValue("CanUseHealItem", "1")) = False Then
                Screen.TextBox.Show("Cannot use heal items.", {}, False, False)
                Exit Sub
            End If
            Core.SetScreen(New ChoosePokemonScreen(Core.CurrentScreen, Me, AddressOf Me.UseOnPokemon, "Use " & Me.Name, True))
        End Sub

        Public Overrides Function UseOnPokemon(ByVal PokeIndex As Integer) As Boolean
            Dim Pokemon As Pokemon = Core.Player.Pokemons(PokeIndex)

            If Pokemon.Status = BasePokemon.StatusProblems.Fainted Then
                Pokemon.Status = BasePokemon.StatusProblems.None
                Pokemon.HP = CInt(Math.Floor(Pokemon.MaxHP / 2))
                Pokemon.ChangeFriendShip(BasePokemon.FriendShipCauses.RevivalHerb)

                SoundManager.PlaySound("single_heal", False)
                Screen.TextBox.Show(Pokemon.GetDisplayName() & "~is revitalized.", {}, False, False)
                PlayerStatistics.Track("[17]Medicine Items used", 1)

                RemoveItem()

                Return True
            Else
                Screen.TextBox.Show("Cannot use revive~on this Pokémon.", {}, False, False)

                Return False
            End If
        End Function

    End Class

End Namespace
