﻿Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Screens


Namespace BattleSystem.Moves.Flying

    Public Class SkyAttack

        Inherits Attack

        Public Sub New()
            '#Definitions
            Me.Type = New Element(Element.Types.Flying)
            Me.ID = 143
            Me.OriginalPP = 5
            Me.CurrentPP = 5
            Me.MaxPP = 5
            Me.Power = 140
            Me.Accuracy = 90
            Me.Category = Categories.Physical
            Me.ContestCategory = ContestCategories.Cool
            Me.Name = "Sky Attack"
            Me.Description = "A second-turn attack move where critical hits land more easily. It may also make the target flinch."
            Me.CriticalChance = 1
            Me.IsHMMove = False
            Me.Target = Targets.OneTarget
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
            Me.IsOneHitKOMove = False
            Me.IsWonderGuardAffected = True
            '#End

            Me.AIField1 = AIField.Damage
            Me.AIField2 = AIField.MultiTurn

            Me.EffectChances.Add(30)
        End Sub

        Public Overrides Function GetUseAccEvasion(own As Boolean, screen As Screen) As Boolean
            Dim BattleScreen As BattleScreen = CType(Screen, BattleScreen)
            Dim SkyAttack As Integer = BattleScreen.FieldEffects.OwnSkyAttackCounter
            If own = False Then
                SkyAttack = BattleScreen.FieldEffects.OppSkyAttackCounter
            End If

            If SkyAttack = 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Overrides Sub PreAttack(own As Boolean, screen As Screen)
            Dim BattleScreen As BattleScreen = CType(Screen, BattleScreen)
            Dim SkyAttack As Integer = BattleScreen.FieldEffects.OwnSkyAttackCounter
            If Own = False Then
                SkyAttack = BattleScreen.FieldEffects.OppSkyAttackCounter
            End If

            If SkyAttack = 0 Then
                Me.FocusOppPokemon = False
            Else
                Me.FocusOppPokemon = True
            End If
        End Sub

        Public Overrides Function MoveFailBeforeAttack(own As Boolean, screen As Screen) As Boolean
            Dim BattleScreen As BattleScreen = CType(Screen, BattleScreen)
            Dim p As Pokemon = BattleScreen.OwnPokemon
            Dim op As Pokemon = BattleScreen.OppPokemon
            If Own = False Then
                p = BattleScreen.OppPokemon
                op = BattleScreen.OwnPokemon
            End If

            Dim skyattack As Integer = BattleScreen.FieldEffects.OwnSkyAttackCounter
            If Own = False Then
                skyattack = BattleScreen.FieldEffects.OppSkyAttackCounter
            End If

            If Not p.Item Is Nothing Then
                If p.Item.Name.ToLower() = "power herb" And BattleScreen.FieldEffects.CanUseItem(Own) = True And BattleScreen.FieldEffects.CanUseOwnItem(Own, Screen) = True Then
                    If BattleScreen.Battle.RemoveHeldItem(Own, Own, Screen, "Power Herb pushed the use of Sky Attack!", "move:skyattack") = True Then
                        skyattack = 1
                    End If
                End If
            End If

            If skyattack = 0 Then
                BattleScreen.BattleQuery.Add(New TextQueryObject(p.GetDisplayName() & " became cloaked in a harsh light!"))
                If Own = True Then
                    BattleScreen.FieldEffects.OwnSkyAttackCounter = 1
                Else
                    BattleScreen.FieldEffects.OppSkyAttackCounter = 1
                End If
                Return True
            Else
                If Own = True Then
                    BattleScreen.FieldEffects.OwnSkyAttackCounter = 0
                Else
                    BattleScreen.FieldEffects.OppSkyAttackCounter = 0
                End If
                Return False
            End If
        End Function

        Public Overrides Sub MoveSelected(own As Boolean, screen As Screen)
            Dim BattleScreen As BattleScreen = CType(screen, BattleScreen)
            If own = True Then
                BattleScreen.FieldEffects.OwnSkyAttackCounter = 0
            Else
                BattleScreen.FieldEffects.OppSkyAttackCounter = 0
            End If
        End Sub

        Public Overrides Function DeductPp(own As Boolean, screen As Screen) As Boolean
            Dim BattleScreen As BattleScreen = CType(Screen, BattleScreen)
            Dim SkyAttack As Integer = BattleScreen.FieldEffects.OwnSkyAttackCounter
            If own = False Then
                SkyAttack = BattleScreen.FieldEffects.OppSkyAttackCounter
            End If

            If SkyAttack = 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Private Sub MoveFails(own As Boolean, BattleScreen As BattleScreen)
            If own = True Then
                BattleScreen.FieldEffects.OwnSkyAttackCounter = 0
            Else
                BattleScreen.FieldEffects.OppSkyAttackCounter = 0
            End If
        End Sub

        Public Overrides Sub MoveMisses(own As Boolean, screen As Screen)
            Dim BattleScreen As BattleScreen = CType(screen, BattleScreen)
            MoveFails(own, BattleScreen)
        End Sub

        Public Overrides Sub AbsorbedBySubstitute(own As Boolean, screen As Screen)
            Dim BattleScreen As BattleScreen = CType(screen, BattleScreen)
            MoveFails(own, BattleScreen)
        End Sub

        Public Overrides Sub MoveProtectedDetected(own As Boolean, screen As Screen)
            Dim BattleScreen As BattleScreen = CType(screen, BattleScreen)
            MoveFails(own, BattleScreen)
        End Sub

        Public Overrides Sub InflictedFlinch(own As Boolean, screen As Screen)
            Dim BattleScreen As BattleScreen = CType(screen, BattleScreen)
            MoveFails(own, BattleScreen)
        End Sub

        Public Overrides Sub IsSleeping(own As Boolean, screen As Screen)
            Dim BattleScreen As BattleScreen = CType(screen, BattleScreen)
            MoveFails(own, BattleScreen)
        End Sub

        Public Overrides Sub HurtItselfInConfusion(own As Boolean, screen As Screen)
            Dim BattleScreen As BattleScreen = CType(screen, BattleScreen)
            MoveFails(own, BattleScreen)
        End Sub

        Public Overrides Sub IsAttracted(own As Boolean, screen As Screen)
            Dim BattleScreen As BattleScreen = CType(screen, BattleScreen)
            MoveFails(own, BattleScreen)
        End Sub

        '''v0.55_update
        'Public Overrides Sub MoveHits(own As Boolean, screen As Screen)
        '    Dim BattleScreen As BattleScreen = CType(screen, BattleScreen)
        '    If Core.Random.Next(0, 100) < Me.GetEffectChance(0, own, BattleScreen) Then
        '        BattleScreen.Battle.InflictFlinch(Not own, own, BattleScreen, "", "move:skyattack")
        '    End If
        'End Sub

    End Class

End Namespace