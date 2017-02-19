Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Screens

Namespace BattleSystem.Moves.Ice

    Public Class FreezeShock

        Inherits Attack

        Public Sub New()
            '#Definitions
            Me.Type = New Element(Element.Types.Ice)
            Me.ID = 553
            Me.OriginalPP = 5
            Me.CurrentPP = 5
            Me.MaxPP = 5
            Me.Power = 140
            Me.Accuracy = 90
            Me.Category = Categories.Special
            Me.ContestCategory = ContestCategories.Beauty
            Me.Name = "Freeze Shock"
            Me.Description = "On the second turn, the user hits the target with electrically charged ice. It may leave the target with paralysis."
            Me.CriticalChance = 1
            Me.IsHMMove = False
            Me.Target = Targets.AllAdjacentTargets
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
            Me.CounterAffected = False

            Me.DisabledWhileGravity = False
            Me.UseEffectiveness = True
            Me.ImmunityAffected = True
            Me.RemovesFrozen = False
            Me.HasSecondaryEffect = True

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
            Me.AIField3 = AIField.CanParalyse

            EffectChances.Add(30)
        End Sub

        Public Overrides Function MoveFailBeforeAttack(Own As Boolean, BattleScreen As Screen) As Boolean
            Dim screen As BattleScreen = BattleScreen
            Dim p As Pokemon = screen.OwnPokemon
            Dim op As Pokemon = screen.OppPokemon
            If Own = False Then
                p = screen.OppPokemon
                op = screen.OwnPokemon
            End If

            Dim freezeshock As Integer = screen.FieldEffects.OwnFreezeShockCounter
            If Own = False Then
                freezeshock = screen.FieldEffects.OppFreezeShockCounter
            End If

            If Not p.Item Is Nothing Then
                If p.Item.Name.ToLower() = "power herb" And screen.FieldEffects.CanUseItem(Own) = True And screen.FieldEffects.CanUseOwnItem(Own, screen) = True Then
                    If screen.Battle.RemoveHeldItem(Own, Own, screen, "Power Herb pushed the use of Ice Burn!", "move:iceburn") = True Then
                        freezeshock = 1
                    End If
                End If
            End If

            If freezeshock = 0 Then
                screen.BattleQuery.Add(New TextQueryObject(p.GetDisplayName() & " became cloaked in a harsh light!"))
                If Own = True Then
                    screen.FieldEffects.OwnFreezeShockCounter = 1
                Else
                    screen.FieldEffects.OppFreezeShockCounter = 1
                End If
                Return True
            Else
                If Own = True Then
                    screen.FieldEffects.OwnFreezeShockCounter = 0
                Else
                    screen.FieldEffects.OppFreezeShockCounter = 0
                End If
                Return False
            End If
        End Function

        Public Overrides Function DeductPp(own As Boolean, BattleScreen As Screen) As Boolean
            Dim screen As BattleScreen = BattleScreen
            Dim freezeshock As Integer = screen.FieldEffects.OwnFreezeShockCounter
            If own = False Then
                freezeshock = screen.FieldEffects.OppFreezeShockCounter
            End If

            If freezeshock = 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Overloads Sub MoveHits(own As Boolean, BattleScreen As BattleScreen)
            If Core.Random.Next(0, 100) < Me.GetEffectChance(0, own, BattleScreen) Then
                BattleScreen.Battle.InflictParalysis(Not own, own, BattleScreen, "", "move:freezeshock")
            End If
        End Sub

    End Class

End Namespace