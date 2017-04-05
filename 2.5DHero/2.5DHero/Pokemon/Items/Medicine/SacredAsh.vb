Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Resources.Managers.Sound
Imports P3D.Legacy.Core.Resources.Sound
Imports P3D.Legacy.Core.Screens

Namespace Items.Medicine

    <Item(156, "Sacred Ash")>
    Public Class SacredAsh

        Inherits Item

        Public Overrides ReadOnly Property Description As String = "It revives all fainted Pokémon. In doing so, it also fully restores their HP."
        Public Overrides ReadOnly Property PokeDollarPrice As Integer = 200

        Public Sub New()
            TextureRectangle = New Rectangle(24, 144, 24, 24)
        End Sub

        Public Overrides Sub Use()
            Dim hasFainted As Boolean = False

            For Each p As Pokemon In Core.Player.Pokemons
                If p.Status = BasePokemon.StatusProblems.Fainted Then
                    hasFainted = True
                    Exit For
                End If
            Next

            If hasFainted = True Then
                For Each p As Pokemon In Core.Player.Pokemons
                    If p.Status = BasePokemon.StatusProblems.Fainted Then
                        p.Status = BasePokemon.StatusProblems.None
                        p.HP = p.MaxHP
                    End If
                Next

                SoundEffectManager.PlaySound("single_heal", False)
                Screen.TextBox.Show("Your team has been~fully healed." & RemoveItem(), {})
                PlayerStatistics.Track("[17]Medicine Items used", 1)
            Else
                Screen.TextBox.Show("Cannot be used.", {})
            End If
        End Sub

    End Class

End Namespace
