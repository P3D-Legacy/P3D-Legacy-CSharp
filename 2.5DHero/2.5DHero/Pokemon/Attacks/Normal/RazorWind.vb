Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Screens

Namespace BattleSystem.Moves.Normal

    Public Class RazorWind

        Inherits Attack

        Public Sub New()
            '#Definitions
            Me.Type = New Element(Element.Types.Normal)
            Me.ID = 13
            Me.OriginalPP = 10
            Me.CurrentPP = 10
            Me.MaxPP = 10
            Me.Power = 80
            Me.Accuracy = 100
            Me.Category = Categories.Special
            Me.ContestCategory = ContestCategories.Cool
            Me.Name = "Razor Wind"
            Me.Description = "A two-turn attack. Blades of wind hit opposing Pokémon on the second turn. Critical hits land more easily."
            Me.CriticalChance = 2
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
            '#End

            Me.AIField1 = AIField.Damage
            Me.AIField2 = AIField.MultiTurn
        End Sub

        Public Overrides Function GetUseAccEvasion(own As Boolean, BattleScreen As Screen) As Boolean
            Dim screen as BattleScreen = BattleScreen
            Dim RazorWind As Integer = screen.FieldEffects.OwnRazorWindCounter
            If own = False Then
                RazorWind = screen.FieldEffects.OppRazorWindCounter
            End If

            If RazorWind = 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Overrides Sub PreAttack(Own As Boolean, BattleScreen As Screen)
            Dim screen as BattleScreen = BattleScreen
            Dim RazorWind As Integer = screen.FieldEffects.OwnRazorWindCounter
            If Own = False Then
                RazorWind = screen.FieldEffects.OppRazorWindCounter
            End If

            If RazorWind = 0 Then
                Me.FocusOppPokemon = False
            Else
                Me.FocusOppPokemon = True
            End If
        End Sub

        Public Overrides Function MoveFailBeforeAttack(Own As Boolean, BattleScreen As Screen) As Boolean
            Dim screen as BattleScreen = BattleScreen
            Dim p As Pokemon = screen.OwnPokemon
            Dim op As Pokemon = screen.OppPokemon
            If Own = False Then
                p = screen.OppPokemon
                op = screen.OwnPokemon
            End If

            Dim razorWind As Integer = screen.FieldEffects.OwnRazorWindCounter
            If Own = False Then
                razorWind = screen.FieldEffects.OppRazorWindCounter
            End If

            If Not p.Item Is Nothing Then
                If p.Item.Name.ToLower() = "power herb" And screen.FieldEffects.CanUseItem(Own) = True And screen.FieldEffects.CanUseOwnItem(Own, BattleScreen) = True Then
                    If screen.Battle.RemoveHeldItem(Own, Own, screen, "Power Herb pushed the use of Razor Wind!", "move:razorwind") = True Then
                        razorWind = 1
                    End If
                End If
            End If

            If razorWind = 0 Then
                screen.BattleQuery.Add(New TextQueryObject(p.GetDisplayName() & " whipped up a whirlwind!"))
                If Own = True Then
                    screen.FieldEffects.OwnRazorWindCounter = 1
                Else
                    screen.FieldEffects.OppRazorWindCounter = 1
                End If
                Return True
            Else
                If Own = True Then
                    screen.FieldEffects.OwnRazorWindCounter = 0
                Else
                    screen.FieldEffects.OppRazorWindCounter = 0
                End If
                Return False
            End If
        End Function

        Public Overrides Function DeductPp(own As Boolean, BattleScreen As Screen) As Boolean
            Dim screen as BattleScreen = BattleScreen
            Dim razorWind As Integer = screen.FieldEffects.OwnRazorWindCounter
            If own = False Then
                razorWind = screen.FieldEffects.OppRazorWindCounter
            End If

            If razorWind = 0 Then
                Return False
            Else
                Return True
            End If
        End Function

    End Class

End Namespace