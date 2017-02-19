Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Resources.Sound
Imports P3D.Legacy.Core.Screens

Namespace Items.Medicine

    <Item(38, "Full Heal")>
    Public Class FullHeal

        Inherits Item

        Public Overrides ReadOnly Property Description As String = "A spray-type medicine that is broadly effective. It can be used once to heal all the status conditions of a Pokémon."
        Public Overrides ReadOnly Property PokeDollarPrice As Integer = 600

        Public Sub New()
            TextureRectangle = New Rectangle(336, 24, 24, 24)
        End Sub

        Public Overrides Sub Use()
            Core.SetScreen(New ChoosePokemonScreen(Core.CurrentScreen, Me, AddressOf Me.UseOnPokemon, "Use " & Me.Name, True))
        End Sub

        Public Overrides Function UseOnPokemon(ByVal PokeIndex As Integer) As Boolean
            Dim Pokemon As Pokemon = Core.Player.Pokemons(PokeIndex)

            If Pokemon.Status = BasePokemon.StatusProblems.Fainted Then
                Screen.TextBox.ReDelay = 0.0F
                Screen.TextBox.Show(Pokemon.GetDisplayName() & "~is fainted!", {})

                Return False
            Else
                If Pokemon.Status <> BasePokemon.StatusProblems.None Or Pokemon.HasVolatileStatus(BasePokemon.VolatileStatus.Confusion) = True Then
                    Pokemon.Status = BasePokemon.StatusProblems.None

                    If Pokemon.HasVolatileStatus(BasePokemon.VolatileStatus.Confusion) = True Then
                        Pokemon.RemoveVolatileStatus(BasePokemon.VolatileStatus.Confusion)
                    End If

                    Screen.TextBox.reDelay = 0.0F

                    Dim t As String = Pokemon.GetDisplayName() & "~gets healed up!"
                    t &= RemoveItem()

                    SoundManager.PlaySound("single_heal", False)
                    Screen.TextBox.Show(t, {})
                    PlayerStatistics.Track("[17]Medicine Items used", 1)

                    Return True
                Else
                    Screen.TextBox.reDelay = 0.0F
                    Screen.TextBox.Show(Pokemon.GetDisplayName() & "~is fully healed!", {}, True, True)

                    Return False
                End If
            End If
        End Function

    End Class

End Namespace
