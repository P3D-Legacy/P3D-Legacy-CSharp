Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Pokemon.Items
Imports P3D.Legacy.Core.Screens

Namespace Items.Berries

    <Item(2025, "Tamato")>
    Public Class TamatoBerry

        Inherits Berry

        Public Sub New()
            MyBase.New(21600, "A Berry to be consumed by Pokémon. Using it on a Pokémon makes it more friendly but lowers its base Speed.", "20.0m", "Soft", 2, 4)

            Me.Spicy = 20
            Me.Dry = 10
            Me.Sweet = 0
            Me.Bitter = 0
            Me.Sour = 0

            Me.Type = Element.Types.Psychic
            Me.Power = 70
        End Sub

        Public Overrides Sub Use()
            Core.SetScreen(New ChoosePokemonScreen(Core.CurrentScreen, Me, AddressOf Me.UseOnPokemon, "Use " & Me.Name, True))
        End Sub

        Public Overrides Function UseOnPokemon(ByVal PokeIndex As Integer) As Boolean
            Dim p As Pokemon = Core.Player.Pokemons(PokeIndex)

            If p.EVSpeed > 0 Then
                Dim reduce As Integer = 10
                If p.EVSpeed < reduce Then
                    reduce = p.EVSpeed
                End If

                p.ChangeFriendShip(BasePokemon.FriendShipCauses.EVBerry)
                p.EVSpeed -= reduce
                p.CalculateStats()

                Screen.TextBox.Show("Raised friendship of~" & p.GetDisplayName() & "." & RemoveItem(), {}, True, False)
                Return True
            Else
                Screen.TextBox.Show("Cannot raise the friendship~of " & p.GetDisplayName() & ".", {}, True, False)
                Return False
            End If
        End Function

    End Class

End Namespace
