﻿Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Screens

Namespace BattleSystem.Moves.Normal

    Public Class Recycle

        Inherits Attack

        Public Sub New()
            '#Definitions
            Me.Type = New Element(Element.Types.Normal)
            Me.ID = 278
            Me.OriginalPP = 10
            Me.CurrentPP = 10
            Me.MaxPP = 10
            Me.Power = 0
            Me.Accuracy = 0
            Me.Category = Categories.Status
            Me.ContestCategory = ContestCategories.Smart
            Me.Name = "Recycle"
            Me.Description = "The user recycles a held item that has been used in battle so it can be used again."
            Me.CriticalChance = 0
            Me.IsHMMove = False
            Me.Target = Targets.Self
            Me.Priority = 0
            Me.TimesToAttack = 1
            '#End

            '#SpecialDefinitions
            Me.MakesContact = False
            Me.ProtectAffected = False
            Me.MagicCoatAffected = False
            Me.SnatchAffected = True
            Me.MirrorMoveAffected = True
            Me.KingsrockAffected = False
            Me.CounterAffected = False

            Me.DisabledWhileGravity = False
            Me.UseEffectiveness = False
            Me.ImmunityAffected = False
            Me.HasSecondaryEffect = False
            Me.RemovesFrozen = False

            Me.IsHealingMove = False
            Me.IsRecoilMove = False
            Me.IsPunchingMove = False
            Me.IsDamagingMove = False
            Me.IsProtectMove = False
            Me.IsSoundMove = False

            Me.IsAffectedBySubstitute = False
            Me.IsOneHitKOMove = False
            Me.IsWonderGuardAffected = False
            '#End

            Me.AIField1 = AIField.Support
            Me.AIField2 = AIField.Nothing
        End Sub

        Public Overrides Sub MoveHits(own As Boolean, screen As Screen)
            Dim BattleScreen As BattleScreen = CType(screen, BattleScreen)
            If own = True Then
                Dim p As Pokemon = BattleScreen.OwnPokemon

                If Not BattleScreen.FieldEffects.OwnConsumedItem Is Nothing Then
                    p.Item = BattleScreen.FieldEffects.OwnConsumedItem
                    BattleScreen.BattleQuery.Add(New TextQueryObject(p.GetDisplayName() & " found one " & p.Item.Name & "!"))
                Else
                    BattleScreen.BattleQuery.Add(New TextQueryObject("Recycle failed!"))
                End If
            Else
                Dim p As Pokemon = BattleScreen.OppPokemon

                If Not BattleScreen.FieldEffects.OppConsumedItem Is Nothing Then
                    p.Item = BattleScreen.FieldEffects.OppConsumedItem
                    BattleScreen.BattleQuery.Add(New TextQueryObject(p.GetDisplayName() & " found one " & p.Item.Name & "!"))
                Else
                    BattleScreen.BattleQuery.Add(New TextQueryObject("Recycle failed!"))
                End If
            End If
        End Sub

    End Class

End Namespace