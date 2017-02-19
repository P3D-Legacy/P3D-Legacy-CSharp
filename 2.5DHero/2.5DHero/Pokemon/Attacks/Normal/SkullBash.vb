Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Screens

Namespace BattleSystem.Moves.Normal

    Public Class SkullBash

        Inherits Attack

        Public Sub New()
            '#Definitions
            Me.Type = New Element(Element.Types.Normal)
            Me.ID = 130
            Me.OriginalPP = 10
            Me.CurrentPP = 10
            Me.MaxPP = 10
            Me.Power = 130
            Me.Accuracy = 100
            Me.Category = Categories.Physical
            Me.ContestCategory = ContestCategories.Tough
            Me.Name = "Skull Bash"
            Me.Description = "The user tucks in its head to raise its Defense in the first turn, then rams the target on the next turn."
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
            '#End

            Me.AIField1 = AIField.Damage
            Me.AIField2 = AIField.RaiseDefense
            Me.AIField3 = AIField.MultiTurn
        End Sub

        Public Overrides Function GetUseAccEvasion(own As Boolean, BattleScreen As Screen) As Boolean
            Dim screen as BattleScreen = BattleScreen
            Dim SkullBash As Integer = screen.FieldEffects.OwnSkullBashCounter
            If own = False Then
                SkullBash = screen.FieldEffects.OppSkullBashCounter
            End If

            If SkullBash = 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Overrides Sub PreAttack(Own As Boolean, BattleScreen As Screen)
            Dim screen as BattleScreen = BattleScreen
            Dim SkullBash As Integer = screen.FieldEffects.OwnSkullBashCounter
            If Own = False Then
                SkullBash = screen.FieldEffects.OppSkullBashCounter
            End If

            If SkullBash = 0 Then
                Me.FocusOppPokemon = False
            Else
                Me.FocusOppPokemon = True
            End If
        End Sub

        Public Overrides Function MoveFailBeforeAttack(ByVal Own As Boolean, ByVal BattleScreen As Screen) As Boolean
            Dim screen as BattleScreen = BattleScreen
            Dim p As Pokemon = screen.OwnPokemon
            Dim op As Pokemon = screen.OppPokemon
            If Own = False Then
                p = screen.OppPokemon
                op = screen.OwnPokemon
            End If

            Dim skullBash As Integer = screen.FieldEffects.OwnSkullBashCounter
            If Own = False Then
                skullBash = screen.FieldEffects.OppSkullBashCounter
            End If

            If Not p.Item Is Nothing Then
                If p.Item.Name.ToLower() = "power herb" And screen.FieldEffects.CanUseItem(Own) = True And screen.FieldEffects.CanUseOwnItem(Own, screen) = True Then
                    If screen.Battle.RemoveHeldItem(Own, Own, screen, "Power Herb pushed the use of Skull Bash!", "move:skullbash") = True Then
                        skullBash = 1
                    End If
                End If
            End If

            If skullBash = 0 Then
                screen.BattleQuery.Add(New TextQueryObject(p.GetDisplayName() & " tucked in its head!"))
                If Own = True Then
                    screen.FieldEffects.OwnSkullBashCounter = 1
                Else
                    screen.FieldEffects.OppSkullBashCounter = 1
                End If
                screen.Battle.RaiseStat(Own, Own, screen, "Defense", 1, "", "move:skullbash")
                Return True
            Else
                If Own = True Then
                    screen.FieldEffects.OwnSkullBashCounter = 0
                Else
                    screen.FieldEffects.OppSkullBashCounter = 0
                End If
                Return False
            End If
        End Function

        Public Overrides Function DeductPp(own As Boolean, BattleScreen As Screen) As Boolean
            Dim screen as BattleScreen = BattleScreen
            Dim skullBash As Integer = screen.FieldEffects.OwnSkullBashCounter
            If own = False Then
                skullBash = screen.FieldEffects.OppSkullBashCounter
            End If

            If skullBash = 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Private Sub MoveFails(own As Boolean, BattleScreen As BattleScreen)
            If own = True Then
                BattleScreen.FieldEffects.OwnSkullBashCounter = 0
            Else
                BattleScreen.FieldEffects.OppSkullBashCounter = 0
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