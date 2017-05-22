Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Screens


Namespace BattleSystem.Moves.Dark

    Public Class BeatUp

        Inherits Attack

        Public Sub New()
            '#Definitions
            Me.Type = New Element(Element.Types.Dark)
            Me.Id = 251
            Me.OriginalPp = 10
            Me.CurrentPp = 10
            Me.MaxPp = 10
            Me.Power = 0
            Me.Accuracy = 100
            Me.Category = Categories.Physical
            Me.ContestCategory = ContestCategories.Smart
            Me.Name = "Beat Up"
            Me.Description = "The user gets all party Pokémon to attack the target. The more party Pokémon, the greater the number of attacks."
            Me.CriticalChance = 1
            Me.IsHmMove = False
            Me.Target = Targets.OneAdjacentTarget
            Me.Priority = 0
            Me.TimesToAttack = 1
            '#End

            '#SpecialDefinitions
            Me.MakesContact = False
            Me.ProtectAffected = True
            Me.MagicCoatAffected = False
            Me.SnatchAffected = False
            Me.MirrorMoveAffected = True
            Me.KingsrockAffected = True
            Me.CounterAffected = True

            Me.DisabledWhileGravity = False
            Me.UseEffectiveness = True
            Me.ImmunityAffected = False
            Me.RemovesFrozen = False
            Me.HasSecondaryEffect = False

            Me.IsHealingMove = False
            Me.IsRecoilMove = False
            Me.IsPunchingMove = False
            Me.IsDamagingMove = True
            Me.IsProtectMove = False
            Me.IsSoundMove = False

            Me.IsAffectedBySubstitute = True
            Me.IsOneHitKoMove = False
            Me.IsWonderGuardAffected = True
            '#End
        End Sub

        Public Overrides Function GetTimesToAttack(own As Boolean, screen As Screen) As Integer
            Dim BattleScreen As BattleScreen = CType(screen, BattleScreen)
            Dim i As Integer = 1
            If own = True Then
                For Each p As Pokemon In Core.Player.Pokemons
                    If p.Status = BasePokemon.StatusProblems.None Then
                        i += 1
                    End If
                Next
            ElseIf BattleScreen.IsTrainerBattle = True Then
                For Each p As Pokemon In BattleScreen.Trainer.Pokemons
                    If p.Status = BasePokemon.StatusProblems.None Then
                        i += 1
                    End If
                Next
            End If
            i = i - 1
            Return i
        End Function

        Public Overrides Function GetBasePower(own As Boolean, screen As Screen) As Integer
            Dim BattleScreen As BattleScreen = CType(screen, BattleScreen)
            Dim avgTeamBaseAttack As Double = 0.0D
            Dim pokemonCounter As Integer = 0
            If own Then
                For Each pokemon As Pokemon In Core.Player.Pokemons
                    If (Not pokemon.IsEgg) AndAlso pokemon.Status <> BasePokemon.StatusProblems.Fainted And pokemon.HP > 0 Then
                        avgTeamBaseAttack += (pokemon.BaseAttack / 10)
                        pokemonCounter += 1
                    End If
                Next
            Else
                For Each pokemon As Pokemon In BattleScreen.Trainer.Pokemons
                    If (Not pokemon.IsEgg) AndAlso pokemon.Status <> BasePokemon.StatusProblems.Fainted And pokemon.HP > 0 Then
                        avgTeamBaseAttack += (pokemon.BaseAttack / 10)
                        pokemonCounter += 1
                    End If
                Next
            End If
            If pokemonCounter <> 0 Then
                avgTeamBaseAttack = avgTeamBaseAttack / pokemonCounter
            Else
                avgTeamBaseAttack = 10 'should never meet this case.
            End If
            Return CInt(avgTeamBaseAttack) + 5
        End Function
    End Class

End Namespace
