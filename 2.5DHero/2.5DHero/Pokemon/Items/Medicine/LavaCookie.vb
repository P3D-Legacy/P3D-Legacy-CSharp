Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Resources.Sound
Imports P3D.Legacy.Core.Screens

Namespace Items.Medicine

    <Item(7, "Lava Cookie")>
    Public Class LavaCookie

        Inherits Item

        Public Overrides ReadOnly Property Description As String = "Lavaridge Town's local specialty. It can be used once to heal all the status conditions of a Pokemon."
        Public Overrides ReadOnly Property PokeDollarPrice As Integer = 200

        Public Sub New()
            TextureRectangle = New Rectangle(192, 192, 24, 24)
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

                    Core.Player.Inventory.RemoveItem(Me.ID, 1)

                    Screen.TextBox.reDelay = 0.0F

                    Dim t As String = Pokemon.GetDisplayName() & "~eats the cookie...*" & Pokemon.GetDisplayName() & "~gets healed up!"
                    t &= RemoveItem()
                    PlayerStatistics.Track("[17]Medicine Items used", 1)

                    SoundManager.PlaySound("single_heal", False)
                    Screen.TextBox.Show(t, {})

                    Return True
                Else
                    Screen.TextBox.reDelay = 0.0F
                    Screen.TextBox.Show(Pokemon.GetDisplayName() & "~doesn't want to eat~the cookie...", {})

                    Return False
                End If
            End If
        End Function

    End Class

End Namespace
