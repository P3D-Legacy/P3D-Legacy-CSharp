Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Resources.Managers.Sound
Imports P3D.Legacy.Core.Resources.Sound
Imports P3D.Legacy.Core.Screens

Namespace Items.Vitamins

    <Item(31, "Calcium")>
    Public Class Calcium

        Inherits VitaminItem

        Public Overrides ReadOnly Property Description As String = "A nutritious drink for Pokémon. When consumed, it raises the base Sp. Atk stat of a single Pokémon."

        Public Sub New()
            TextureRectangle = New Rectangle(168, 24, 24, 24)
        End Sub

        Public Overrides Function UseOnPokemon(PokeIndex As Integer) As Boolean
            Dim p As Pokemon = Core.Player.Pokemons(PokeIndex)

            If CanUseVitamin(p.EVSpAttack, p) = True Then
                p.EVSpAttack += 10
                p.ChangeFriendShip(BasePokemon.FriendShipCauses.Vitamin)

                SoundEffectManager.PlaySound("single_heal", False)
                Screen.TextBox.Show("Raised " & p.GetDisplayName() & "'s~Special Attack.", {}, False, False)
                PlayerStatistics.Track("[25]Vitamins used", 1)

                p.CalculateStats()
                RemoveItem()
                Return True
            Else
                Screen.TextBox.Show("Cannot raise~" & p.GetDisplayName() & "'s Special~Attack.", {}, False, False)

                Return False
            End If
        End Function

    End Class

End Namespace
