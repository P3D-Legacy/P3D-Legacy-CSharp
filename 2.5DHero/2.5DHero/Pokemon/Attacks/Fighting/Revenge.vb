Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Screens

Namespace BattleSystem.Moves.Fighting

    Public Class Revenge

        Inherits Attack

        Public Sub New()
            '#Definitions
            Me.Type = New Element(Element.Types.Fighting)
            Me.ID = 279
            Me.OriginalPP = 10
            Me.CurrentPP = 10
            Me.MaxPP = 10
            Me.Power = 60
            Me.Accuracy = 100
            Me.Category = Categories.Physical
            Me.ContestCategory = ContestCategories.Tough
            Me.Name = "Revenge"
            Me.Description = "An attack move that inflicts double the damage if the user has been hurt by the opponent in the same turn."
            Me.CriticalChance = 1
            Me.IsHMMove = False
            Me.Target = Targets.OneAdjacentTarget
            Me.Priority = -4
            Me.TimesToAttack = 1
            '#End

            '#SpecialDefinitions
            Me.MakesContact = True
            Me.ProtectAffected = True
            Me.MagicCoatAffected = False
            Me.SnatchAffected = False
            Me.MirrorMoveAffected = True
            Me.KingsrockAffected = True
            Me.CounterAffected = True

            Me.DisabledWhileGravity = False
            Me.UseEffectiveness = True
            Me.ImmunityAffected = True
            Me.HasSecondaryEffect = False
            Me.RemovesFrozen = False

            Me.IsHealingMove = False
            Me.IsRecoilMove = False
            Me.IsPunchingMove = False
            Me.IsDamagingMove = True
            Me.IsProtectMove = False
            Me.IsSoundMove = False

            Me.IsAffectedBySubstitute = True
            Me.IsOneHitKOMove = False
            Me.IsWonderGuardAffected = True
            '#End
        End Sub

        Public Overrides Function GetBasePower(own As Boolean, BattleScreen As Screen) As Integer
            Dim screen As BattleScreen = BattleScreen
            If own = True Then
                If screen.FieldEffects.OppTurnCounts > screen.FieldEffects.OwnTurnCounts Then
                    If Not screen.FieldEffects.OppLastMove Is Nothing Then
                        If screen.FieldEffects.OppLastMove.IsDamagingMove = True Then
                            Return Me.Power * 2
                        End If
                    End If
                End If
            Else
                If screen.FieldEffects.OwnTurnCounts > screen.FieldEffects.OppTurnCounts Then
                    If Not screen.FieldEffects.OwnLastMove Is Nothing Then
                        If screen.FieldEffects.OwnLastMove.IsDamagingMove = True Then
                            Return Me.Power * 2
                        End If
                    End If
                End If
            End If
            Return Me.Power
        End Function

    End Class

End Namespace