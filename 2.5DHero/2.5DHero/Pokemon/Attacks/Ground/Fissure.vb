Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Screens

Namespace BattleSystem.Moves.Ground

    Public Class Fissure

        Inherits Attack

        Public Sub New()
            '#Definitions
            Me.Type = New Element(Element.Types.Ground)
            Me.ID = 90
            Me.OriginalPP = 5
            Me.CurrentPP = 5
            Me.MaxPP = 5
            Me.Power = 0
            Me.Accuracy = 0
            Me.Category = Categories.Physical
            Me.ContestCategory = ContestCategories.Tough
            Me.Name = "Fissure"
            Me.Description = "The user opens up a fissure in the ground and drops the target in. The target instantly faints if it hits."
            Me.CriticalChance = 0
            Me.IsHMMove = False
            Me.Target = Targets.OneAdjacentTarget
            Me.Priority = 0
            Me.TimesToAttack = 1
            '#End

            '#SpecialDefinitions
            Me.MakesContact = False
            Me.ProtectAffected = True
            Me.MagicCoatAffected = False
            Me.SnatchAffected = False
            Me.MirrorMoveAffected = True
            Me.KingsrockAffected = False
            Me.CounterAffected = True

            Me.DisabledWhileGravity = False
            Me.UseEffectiveness = False
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
            Me.IsOneHitKOMove = True
            Me.IsWonderGuardAffected = True
            Me.UseAccEvasion = False
            Me.CanHitUnderground = True
            '#End

            Me.AIField1 = AIField.Damage
            Me.AIField2 = AIField.OHKO
        End Sub

        Public Overrides Function GetAccuracy(own As Boolean, BattleScreen As Screen) As Integer
            Dim screen As BattleScreen = BattleScreen
            Dim p As Pokemon = screen.OwnPokemon
            Dim op As Pokemon = screen.OppPokemon
            If own = False Then
                p = screen.OppPokemon
                op = screen.OwnPokemon
            End If

            Dim acc As Integer = ((p.Level - op.Level) + 30)
            Return acc
        End Function

        Public Overrides Function MoveFailBeforeAttack(Own As Boolean, BattleScreen As Screen) As Boolean
            Dim screen As BattleScreen = BattleScreen
            Dim p As Pokemon = screen.OwnPokemon
            Dim op As Pokemon = screen.OppPokemon
            If Own = False Then
                p = screen.OppPokemon
                op = screen.OwnPokemon
            End If

            If op.Level > p.Level Then
                screen.BattleQuery.Add(New TextQueryObject(Me.Name & " failed!"))
                Return True
            Else
                Return False
            End If
        End Function

    End Class

End Namespace