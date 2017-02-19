﻿Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Screens

Namespace BattleSystem.Moves.Flying

    Public Class Fly

        Inherits Attack

        Public Sub New()
            '#Definitions
            Me.Type = New Element(Element.Types.Flying)
            Me.ID = 19
            Me.OriginalPP = 15
            Me.CurrentPP = 15
            Me.MaxPP = 15
            Me.Power = 90
            Me.Accuracy = 95
            Me.Category = Categories.Physical
            Me.ContestCategory = ContestCategories.Smart
            Me.Name = "Fly"
            Me.Description = "The user soars, then strikes its target on the second turn. It can also be used for flying to any familiar town."
            Me.CriticalChance = 1
            Me.IsHMMove = False
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
            Me.KingsrockAffected = True
            Me.CounterAffected = True

            Me.DisabledWhileGravity = True
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

            Me.AIField1 = AIField.Damage
            Me.AIField2 = AIField.MultiTurn
        End Sub

        Public Overrides Function GetUseAccEvasion(own As Boolean, BattleScreen As Screen) As Boolean
            Dim screen As BattleScreen = BattleScreen
            Dim fly As Integer = screen.FieldEffects.OwnFlyCounter
            If own = False Then
                fly = screen.FieldEffects.OppFlyCounter
            End If

            If fly = 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Overrides Sub PreAttack(Own As Boolean, BattleScreen As Screen)
            Dim screen As BattleScreen = BattleScreen
            Dim fly As Integer = screen.FieldEffects.OwnFlyCounter
            If Own = False Then
                fly = screen.FieldEffects.OppFlyCounter
            End If

            If fly = 0 Then
                Me.FocusOppPokemon = False
            Else
                Me.FocusOppPokemon = True
            End If
        End Sub

        Public Overrides Function MoveFailBeforeAttack(Own As Boolean, BattleScreen As Screen) As Boolean
            Dim screen As BattleScreen = BattleScreen
            Dim p As Pokemon = screen.OwnPokemon
            Dim op As Pokemon = screen.OppPokemon
            If Own = False Then
                p = screen.OppPokemon
                op = screen.OwnPokemon
            End If

            Dim fly As Integer = screen.FieldEffects.OwnFlyCounter
            If Own = False Then
                fly = screen.FieldEffects.OppFlyCounter
            End If

            If Not p.Item Is Nothing Then
                If p.Item.Name.ToLower() = "power herb" And screen.FieldEffects.CanUseItem(Own) = True And screen.FieldEffects.CanUseOwnItem(Own, screen) = True Then
                    If screen.Battle.RemoveHeldItem(Own, Own, BattleScreen, "Power Herb pushed the use of Fly!", "move:fly") = True Then
                        fly = 1
                    End If
                End If
            End If

            If fly = 0 Then
                screen.BattleQuery.Add(New TextQueryObject(p.GetDisplayName() & " flew up high!"))
                If Own = True Then
                    screen.FieldEffects.OwnFlyCounter = 1
                Else
                    screen.FieldEffects.OppFlyCounter = 1
                End If
                Return True
            Else
                If Own = True Then
                    screen.FieldEffects.OwnFlyCounter = 0
                Else
                    screen.FieldEffects.OppFlyCounter = 0
                End If
                Return False
            End If
        End Function

        Public Overrides Function DeductPp(own As Boolean, BattleScreen As Screen) As Boolean
            Dim screen As BattleScreen = BattleScreen
            Dim fly As Integer = screen.FieldEffects.OwnFlyCounter
            If own = False Then
                fly = screen.FieldEffects.OppFlyCounter
            End If

            If fly = 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Private Sub MoveFails(own As Boolean, BattleScreen As BattleScreen)
            If own = True Then
                BattleScreen.FieldEffects.OwnFlyCounter = 0
            Else
                BattleScreen.FieldEffects.OppFlyCounter = 0
            End If
        End Sub

        Public Overloads Sub MoveMisses(own As Boolean, BattleScreen As BattleScreen)
            MoveFails(own, BattleScreen)
        End Sub

        Public Overrides Sub AbsorbedBySubstitute(own As Boolean, BattleScreen As Screen)
            MoveFails(own, BattleScreen)
        End Sub

        Public Overloads Sub MoveProtectedDetected(own As Boolean, BattleScreen As BattleScreen)
            MoveFails(own, BattleScreen)
        End Sub

    End Class

End Namespace