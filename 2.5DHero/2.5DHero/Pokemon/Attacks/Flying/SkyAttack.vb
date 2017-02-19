Imports P3D.Legacy.Core
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

        Public Overrides Function GetUseAccEvasion(own As Boolean, BattleScreen As Screen) As Boolean
            Dim screen As BattleScreen = BattleScreen
            Dim SkyAttack As Integer = screen.FieldEffects.OwnSkyAttackCounter
            If own = False Then
                SkyAttack = screen.FieldEffects.OppSkyAttackCounter
            End If

            If SkyAttack = 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Overrides Sub PreAttack(Own As Boolean, BattleScreen As Screen)
            Dim screen As BattleScreen = BattleScreen
            Dim SkyAttack As Integer = screen.FieldEffects.OwnSkyAttackCounter
            If Own = False Then
                SkyAttack = screen.FieldEffects.OppSkyAttackCounter
            End If

            If SkyAttack = 0 Then
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

            Dim skyattack As Integer = screen.FieldEffects.OwnSkyAttackCounter
            If Own = False Then
                skyattack = screen.FieldEffects.OppSkyAttackCounter
            End If

            If Not p.Item Is Nothing Then
                If p.Item.Name.ToLower() = "power herb" And screen.FieldEffects.CanUseItem(Own) = True And screen.FieldEffects.CanUseOwnItem(Own, screen) = True Then
                    If screen.Battle.RemoveHeldItem(Own, Own, screen, "Power Herb pushed the use of Sky Attack!", "move:skyattack") = True Then
                        skyattack = 1
                    End If
                End If
            End If

            If skyattack = 0 Then
                screen.BattleQuery.Add(New TextQueryObject(p.GetDisplayName() & " became cloaked in a harsh light!"))
                If Own = True Then
                    screen.FieldEffects.OwnSkyAttackCounter = 1
                Else
                    screen.FieldEffects.OppSkyAttackCounter = 1
                End If
                Return True
            Else
                If Own = True Then
                    screen.FieldEffects.OwnSkyAttackCounter = 0
                Else
                    screen.FieldEffects.OppSkyAttackCounter = 0
                End If
                Return False
            End If
        End Function

        Public Overrides Function DeductPp(own As Boolean, BattleScreen As Screen) As Boolean
            Dim screen As BattleScreen = BattleScreen
            Dim skyattack As Integer = screen.FieldEffects.OwnSkyAttackCounter
            If own = False Then
                skyattack = screen.FieldEffects.OppSkyAttackCounter
            End If

            If skyattack = 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Overloads Sub MoveHits(own As Boolean, BattleScreen As BattleScreen)
            If Core.Random.Next(0, 100) < Me.GetEffectChance(0, own, BattleScreen) Then
                BattleScreen.Battle.InflictFlinch(Not own, own, BattleScreen, "", "move:skyattack")
            End If
        End Sub

    End Class

End Namespace