﻿Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Screens

Namespace BattleSystem.Moves.Normal

    Public Class Bide

        Inherits Attack

        Public Sub New()
            '#Definitions
            Me.Type = New Element(Element.Types.Normal)
            Me.ID = 117
            Me.OriginalPP = 10
            Me.CurrentPP = 10
            Me.MaxPP = 10
            Me.Power = 0
            Me.Accuracy = 0
            Me.Category = Categories.Physical
            Me.ContestCategory = ContestCategories.Tough
            Me.Name = "Bide"
            Me.Description = "The user endures attacks for two turns, then strikes back to cause double the damage taken."
            Me.CriticalChance = 0
            Me.IsHMMove = False
            Me.Target = Targets.OneAdjacentTarget
            Me.Priority = 1
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
            Me.UseEffectiveness = False
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
            Me.IsOneHitKOMove = False
            Me.IsWonderGuardAffected = True

            Me.UseAccEvasion = False
            Me.CanHitInMidAir = True
            Me.CanHitSleeping = True
            Me.CanHitUnderground = True
            Me.CanHitUnderwater = True
            '#End

            Me.AIField1 = AIField.Damage
            Me.AIField2 = AIField.MultiTurn
        End Sub

        Public Overrides Function MoveFailBeforeAttack(Own As Boolean, BattleScreen As Screen) As Boolean
            Dim screen as BattleScreen = BattleScreen
            If Own = True Then
                If screen.FieldEffects.OwnBideCounter < 3 Then
                    screen.FieldEffects.OwnBideCounter += 1
                    Return True
                Else
                    Return False
                End If
            Else
                If screen.FieldEffects.OppBideCounter < 3 Then
                    screen.FieldEffects.OppBideCounter += 1
                    Return True
                Else
                    Return False
                End If
            End If
        End Function

        Public Overrides Function GetDamage(Critical As Boolean, Own As Boolean, targetPokemon As Boolean, BattleScreen As Screen) As Integer
            Dim screen as BattleScreen = BattleScreen
            If Own = True Then
                Dim damage As Integer = screen.FieldEffects.OwnBideDamage * 2
                screen.FieldEffects.OwnBideDamage = 0
                screen.FieldEffects.OwnBideCounter = 0
                Return damage
            Else
                Dim damage As Integer = screen.FieldEffects.OppBideDamage * 2
                screen.FieldEffects.OppBideDamage = 0
                screen.FieldEffects.OppBideCounter = 0
                Return damage
            End If
        End Function

    End Class

End Namespace