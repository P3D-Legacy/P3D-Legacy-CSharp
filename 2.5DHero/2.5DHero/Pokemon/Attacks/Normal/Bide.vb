Imports P3D.Legacy.Core.Pokemon
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

        Public Overrides Function MoveFailBeforeAttack(own As Boolean, screen As Screen) As Boolean
            Dim BattleScreen As BattleScreen = CType(Screen, BattleScreen)
            If Own = True Then
                If BattleScreen.FieldEffects.OwnBideCounter < 3 Then
                    BattleScreen.FieldEffects.OwnBideCounter += 1
                    Return True
                Else
                    Return False
                End If
            Else
                If BattleScreen.FieldEffects.OppBideCounter < 3 Then
                    BattleScreen.FieldEffects.OppBideCounter += 1
                    Return True
                Else
                    Return False
                End If
            End If
        End Function

        Public Overrides Function GetDamage(Critical As Boolean, Own As Boolean, targetPokemon As Boolean, screen As Screen) As Integer
            Dim BattleScreen As BattleScreen = CType(screen, BattleScreen)
            If Own = True Then
                Dim damage As Integer = BattleScreen.FieldEffects.OwnBideDamage * 2
                BattleScreen.FieldEffects.OwnBideDamage = 0
                BattleScreen.FieldEffects.OwnBideCounter = 0
                Return damage
            Else
                Dim damage As Integer = BattleScreen.FieldEffects.OppBideDamage * 2
                BattleScreen.FieldEffects.OppBideDamage = 0
                BattleScreen.FieldEffects.OppBideCounter = 0
                Return damage
            End If
        End Function

        Public Overrides Sub MoveSelected(own As Boolean, screen As Screen)
            Dim BattleScreen As BattleScreen = CType(screen, BattleScreen)
            If own = True Then
                BattleScreen.FieldEffects.OwnBideCounter = 0
            Else
                BattleScreen.FieldEffects.OppBideCounter = 0
            End If
        End Sub
        Public Overrides Function DeductPP(own As Boolean, screen As Screen) As Boolean
            Dim BattleScreen As BattleScreen = CType(screen, BattleScreen)
            Dim Bide As Integer = BattleScreen.FieldEffects.OwnBideCounter
            If own = False Then
                Bide = BattleScreen.FieldEffects.OppBideCounter
            End If

            If Bide = 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Private Sub MoveFails(own As Boolean, BattleScreen As BattleScreen)
            If own = True Then
                BattleScreen.FieldEffects.OwnBideCounter = 0
            Else
                BattleScreen.FieldEffects.OppBideCounter = 0
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

    End Class

End Namespace