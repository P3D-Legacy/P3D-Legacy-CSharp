﻿Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Screens

Namespace BattleSystem.Moves.Grass

    Public Class SolarBeam

        Inherits Attack

        Public Sub New()
            '#Definitions
            Me.Type = New Element(Element.Types.Grass)
            Me.ID = 76
            Me.OriginalPP = 10
            Me.CurrentPP = 10
            Me.MaxPP = 10
            Me.Power = 120
            Me.Accuracy = 100
            Me.Category = Categories.Special
            Me.ContestCategory = ContestCategories.Cool
            Me.Name = "Solar Beam"
            Me.Description = "A two-turn attack. The user gathers light, then blasts a bundled beam on the second turn."
            Me.CriticalChance = 1
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
            Dim screen As BattleScreen = BattleScreen
            Dim SolarBeam As Integer = screen.FieldEffects.OwnSolarBeam
            If own = False Then
                SolarBeam = screen.FieldEffects.OppSolarBeam
            End If

            If SolarBeam = 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Overrides Sub PreAttack(Own As Boolean, BattleScreen As Screen)
            Dim screen As BattleScreen = BattleScreen
            Dim SolarBeam As Integer = screen.FieldEffects.OwnSolarBeam
            If Own = False Then
                SolarBeam = screen.FieldEffects.OppSolarBeam
            End If

            If SolarBeam = 0 Then
                Me.FocusOppPokemon = False
            Else
                Me.FocusOppPokemon = True
            End If
        End Sub

        Public Overrides Function MoveFailBeforeAttack(Own As Boolean, BattleScreen As Screen) As Boolean
            Dim screen As BattleScreen = BattleScreen
            Dim p As Pokemon = screen.OwnPokemon
            If Own = False Then
                p = screen.OppPokemon
            End If

            Dim hasToCharge As Boolean = True

            Dim beam As Integer = screen.FieldEffects.OwnSolarBeam
            If Own = False Then
                beam = screen.FieldEffects.OppSolarBeam
            End If

            If beam = 0 Then
                screen.BattleQuery.Add(New TextQueryObject(p.GetDisplayName() & " absorbed sunlight!"))
            Else
                hasToCharge = False
            End If

            If hasToCharge = True Then
                If screen.FieldEffects.Weather = BattleWeather.WeatherTypes.Sunny Then
                    hasToCharge = False
                Else
                    If Not p.Item Is Nothing Then
                        If p.Item.Name.ToLower() = "power herb" And screen.FieldEffects.CanUseItem(Own) = True And screen.FieldEffects.CanUseOwnItem(Own, BattleScreen) = True Then
                            If screen.Battle.RemoveHeldItem(Own, Own, screen, "Power Herb pushed the use of Solar Beam!", "move:solarbeam") = True Then
                                hasToCharge = False
                            End If
                        End If
                    End If
                End If
            End If

            If hasToCharge = True Then
                If Own = True Then
                    screen.FieldEffects.OwnSolarBeam = 1
                Else
                    screen.FieldEffects.OppSolarBeam = 1
                End If
                Return True
            Else
                If Own = True Then
                    screen.FieldEffects.OwnSolarBeam = 0
                Else
                    screen.FieldEffects.OppSolarBeam = 0
                End If
                Return False
            End If
        End Function

        Public Overrides Function GetBasePower(own As Boolean, BattleScreen As Screen) As Integer
            Dim screen As BattleScreen = BattleScreen
            If screen.FieldEffects.Weather = BattleWeather.WeatherTypes.Rain Or screen.FieldEffects.Weather = BattleWeather.WeatherTypes.Sandstorm Or screen.FieldEffects.Weather = BattleWeather.WeatherTypes.Hailstorm Then
                Return CInt(Me.Power / 2)
            Else
                Return Me.Power
            End If
        End Function

        Public Overrides Sub MoveSelected(own As Boolean, BattleScreen As Screen)
            Dim screen As BattleScreen = BattleScreen
            If own = True Then
                screen.FieldEffects.OwnSolarBeam = 0
            Else
                screen.FieldEffects.OppSolarBeam = 0
            End If
        End Sub

    End Class

End Namespace