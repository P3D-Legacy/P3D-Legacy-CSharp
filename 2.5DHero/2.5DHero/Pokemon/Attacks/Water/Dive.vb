﻿Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Screens

Namespace BattleSystem.Moves.Water

    Public Class Dive

        Inherits Attack

        Public Sub New()
            '#Definitions
            Me.Type = New Element(Element.Types.Water)
            Me.ID = 291
            Me.OriginalPP = 10
            Me.CurrentPP = 10
            Me.MaxPP = 10
            Me.Power = 80
            Me.Accuracy = 100
            Me.Category = Categories.Physical
            Me.ContestCategory = ContestCategories.Beauty
            Me.Name = "Dive"
            Me.Description = "Diving on the first turn, the user floats up and attacks on the next turn."
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
            Me.CanHitUnderwater = True
            Me.CanHitUnderground = False
            '#End

            Me.AIField1 = AIField.Damage
            Me.AIField2 = AIField.MultiTurn
        End Sub

        Public Overrides Function GetUseAccEvasion(own As Boolean, BattleScreen As Screen) As Boolean
            Dim screen As BattleScreen = BattleScreen
            Dim dive As Integer = screen.FieldEffects.OwnDiveCounter
            If own = False Then
                dive = screen.FieldEffects.OppDiveCounter
            End If

            If dive = 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Overrides Sub PreAttack(Own As Boolean, BattleScreen As Screen)
            Dim screen As BattleScreen = BattleScreen
            Dim dive As Integer = screen.FieldEffects.OwnDiveCounter
            If Own = False Then
                dive = screen.FieldEffects.OppDiveCounter
            End If

            If dive = 0 Then
                Me.FocusOppPokemon = False
            Else
                Me.FocusOppPokemon = True
            End If
        End Sub

        Public Overrides Function MoveFailBeforeAttack(Own As Boolean, BattleScreen As Screen) As Boolean
            Dim screen As BattleScreen = BattleScreen
            Dim diveCounter As Integer = screen.FieldEffects.OwnDiveCounter

            If Own = False Then
                diveCounter = screen.FieldEffects.OppDiveCounter
            End If

            Dim p As Pokemon = screen.OwnPokemon
            If Own = False Then
                p = screen.OppPokemon
            End If

            Dim hasToCharge As Boolean = True
            If Not p.Item Is Nothing Then
                If p.Item.Name.ToLower() = "power herb" And screen.FieldEffects.CanUseItem(Own) = True And screen.FieldEffects.CanUseOwnItem(Own, BattleScreen) = True Then
                    If screen.Battle.RemoveHeldItem(Own, Own, BattleScreen, "Power Herb pushed the use of Dive!", "move:dive") = True Then
                        hasToCharge = False
                    End If
                End If
            End If

            If diveCounter = 0 And hasToCharge = True Then
                If Own = True Then
                    screen.FieldEffects.OwnDiveCounter = 1
                Else
                    screen.FieldEffects.OppDiveCounter = 1
                End If

                screen.BattleQuery.Add(New TextQueryObject(p.GetDisplayName() & " burrowed its way underground!"))

                Return True
            Else
                If Own = True Then
                    screen.FieldEffects.OwnDiveCounter = 0
                Else
                    screen.FieldEffects.OppDiveCounter = 0
                End If

                Return False
            End If
        End Function

        Public Overrides Sub MoveSelected(own As Boolean, BattleScreen As Screen)
            Dim screen As BattleScreen = BattleScreen
            If own = True Then
                screen.FieldEffects.OwnDiveCounter = 0
            Else
                screen.FieldEffects.OppDiveCounter = 0
            End If
        End Sub

        Private Sub MoveFails(own As Boolean, BattleScreen As BattleScreen)
            If own = True Then
                BattleScreen.FieldEffects.OwnDiveCounter = 0
            Else
                BattleScreen.FieldEffects.OppDiveCounter = 0
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