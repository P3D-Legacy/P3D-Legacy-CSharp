Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Screens

Namespace BattleSystem.Moves.Dragon

    Public Class Twister

        Inherits Attack

        Public Sub New()
            '#Definitions
            Me.Type = New Element(Element.Types.Dragon)
            Me.ID = 239
            Me.OriginalPP = 20
            Me.CurrentPP = 20
            Me.MaxPP = 20
            Me.Power = 40
            Me.Accuracy = 100
            Me.Category = Categories.Special
            Me.ContestCategory = ContestCategories.Cool
            Me.Name = "Twister"
            Me.Description = "The user whips up a vicious tornado to tear at the opposing team. It may also make targets flinch."
            Me.CriticalChance = 1
            Me.IsHMMove = False
            Me.Target = Targets.AllAdjacentFoes
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
            Me.CanHitInMidAir = True
            '#End

            Me.AIField1 = AIField.Damage
            Me.AIField2 = AIField.CanFlinch

            Me.EffectChances.Add(20)
        End Sub

        Public Overloads Sub MoveHits(own As Boolean, BattleScreen As BattleScreen)
            If Core.Random.Next(0, 100) < Me.GetEffectChance(0, own, BattleScreen) Then
                BattleScreen.Battle.InflictFlinch(Not own, own, BattleScreen, "", "move:twister")
            End If
        End Sub

        Public Overrides Function GetBasePower(own As Boolean, BattleScreen As Screen) As Integer
            Dim screen As BattleScreen = BattleScreen
            Dim fly As Integer = 0
            Dim bounce As Integer = 0

            If own = True Then
                fly = screen.FieldEffects.OppFlyCounter
                bounce = screen.FieldEffects.OppBounceCounter
            Else
                fly = screen.FieldEffects.OwnFlyCounter
                bounce = screen.FieldEffects.OwnBounceCounter
            End If

            If fly > 0 Or bounce > 0 Then
                Return Me.Power * 2
            Else
                Return Me.Power
            End If
        End Function

    End Class

End Namespace