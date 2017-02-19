Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Screens

Namespace BattleSystem.Moves.Ground

    Public Class Dig

        Inherits Attack

        Public Sub New()
            '#Definitions
            Me.Type = New Element(Element.Types.Ground)
            Me.ID = 91
            Me.OriginalPP = 10
            Me.CurrentPP = 10
            Me.MaxPP = 10
            Me.Power = 80
            Me.Accuracy = 100
            Me.Category = Categories.Physical
            Me.ContestCategory = ContestCategories.Smart
            Me.Name = "Dig"
            Me.Description = "The user burrows, then attacks on the second turn. It can also be used to exit dungeons. "
            Me.CriticalChance = 1
            Me.IsHMMove = False
            Me.Target = Targets.AllAdjacentTargets
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
            Me.CanHitUnderground = True
            '#End

            Me.AIField1 = AIField.Damage
            Me.AIField2 = AIField.MultiTurn
        End Sub

        Public Overrides Function GetUseAccEvasion(own As Boolean, BattleScreen As Screen) As Boolean
            Dim screen As BattleScreen = BattleScreen
            Dim dig As Integer = screen.FieldEffects.OwnDigCounter
            If own = False Then
                dig = screen.FieldEffects.OppDigCounter
            End If

            If dig = 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Overrides Sub PreAttack(Own As Boolean, BattleScreen As Screen)
            Dim screen As BattleScreen = BattleScreen
            Dim dig As Integer = screen.FieldEffects.OwnDigCounter
            If Own = False Then
                dig = screen.FieldEffects.OppDigCounter
            End If

            If dig = 0 Then
                Me.FocusOppPokemon = False
            Else
                Me.FocusOppPokemon = True
            End If
        End Sub

        Public Overrides Function MoveFailBeforeAttack(Own As Boolean, BattleScreen As Screen) As Boolean
            Dim screen As BattleScreen = BattleScreen
            Dim digCounter As Integer = screen.FieldEffects.OwnDigCounter

            If Own = False Then
                digCounter = screen.FieldEffects.OppDigCounter
            End If

            Dim p As Pokemon = screen.OwnPokemon
            If Own = False Then
                p = screen.OppPokemon
            End If

            Dim hasToCharge As Boolean = True
            If Not p.Item Is Nothing Then
                If p.Item.Name.ToLower() = "power herb" And screen.FieldEffects.CanUseItem(Own) = True And screen.FieldEffects.CanUseOwnItem(Own, screen) = True Then
                    If screen.Battle.RemoveHeldItem(Own, Own, screen, "Power Herb pushed the use of Dig!", "move:dig") = True Then
                        hasToCharge = False
                    End If
                End If
            End If

            If digCounter = 0 And hasToCharge = True Then
                If Own = True Then
                    screen.FieldEffects.OwnDigCounter = 1
                Else
                    screen.FieldEffects.OppDigCounter = 1
                End If

                screen.BattleQuery.Add(New TextQueryObject(p.GetDisplayName() & " burrowed its way underground!"))

                Return True
            Else
                If Own = True Then
                    screen.FieldEffects.OwnDigCounter = 0
                Else
                    screen.FieldEffects.OppDigCounter = 0
                End If

                Return False
            End If
        End Function

        Public Overrides Sub MoveSelected(own As Boolean, BattleScreen As Screen)
            Dim screen As BattleScreen = BattleScreen
            If own = True Then
                screen.FieldEffects.OwnDigCounter = 0
            Else
                screen.FieldEffects.OppDigCounter = 0
            End If
        End Sub

    End Class

End Namespace