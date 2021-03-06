Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Screens


Namespace BattleSystem.Moves.Bug

    Public Class Steamroller

        Inherits Attack

        Public Sub New()
            '#Definitions
            Me.Type = New Element(Element.Types.Bug)
            Me.Id = 537
            Me.OriginalPp = 20
            Me.CurrentPp = 20
            Me.MaxPp = 20
            Me.Power = 65
            Me.Accuracy = 100
            Me.Category = Categories.Physical
            Me.ContestCategory = ContestCategories.Tough
            Me.Name = "Steamroller"
            Me.Description = "The user crushes its targets by rolling over them with its rolled-up body. This attack may make the target flinch."
            Me.CriticalChance = 1
            Me.IsHmMove = False
            Me.Target = Targets.OneAdjacentTarget
            Me.Priority = 0
            Me.TimesToAttack = 1
            '#End

            '#SpecialDefinitions
            Me.MakesContact = True
            Me.ProtectAffected = True
            Me.MagicCoatAffected = False
            Me.SnatchAffected = False
            Me.MirrorMoveAffected = True
            Me.KingsrockAffected = False
            Me.CounterAffected = True

            Me.DisabledWhileGravity = False
            Me.UseEffectiveness = True
            Me.ImmunityAffected = True
            Me.HasSecondaryEffect = True
            Me.RemovesFrozen = False

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

            Me.EffectChances.Add(30)

            Me.AiField1 = AIField.Damage
            Me.AiField2 = AIField.Flinch
        End Sub

        Public Overrides Function GetBasePower(own As Boolean, screen As Screen) As Integer
            Dim BattleScreen As BattleScreen = CType(screen, BattleScreen)
            Dim minimize As Integer = BattleScreen.FieldEffects.OppMinimize
            If own = False Then
                minimize = BattleScreen.FieldEffects.OwnMinimize
            End If
            If minimize > 0 Then
                Return Me.Power * 2
            Else
                Return Me.Power
            End If
        End Function

        Public Overrides Sub MoveHits(own As Boolean, screen As Screen)
            Dim BattleScreen As BattleScreen = CType(screen, BattleScreen)
            Dim chance As Integer = GetEffectChance(0, own, BattleScreen)

            If Core.Random.Next(0, 100) < chance Then
                BattleScreen.Battle.InflictFlinch(Not own, own, BattleScreen, "", "move:steamroller")
            End If
        End Sub

    End Class

End Namespace