Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Screens

Imports P3D.Legacy.Core.World

Namespace BattleSystem.Moves.Normal

    Public Class NaturePower

        Inherits Attack

        Public Sub New()
            '#Definitions
            Me.Type = New Element(Element.Types.Normal)
            Me.ID = 267
            Me.OriginalPP = 20
            Me.CurrentPP = 20
            Me.MaxPP = 20
            Me.Power = 0
            Me.Accuracy = 0
            Me.Category = Categories.Status
            Me.ContestCategory = ContestCategories.Beauty
            Me.Name = "Nature Power"
            Me.Description = "An attack that makes use of nature’s power. Its effects vary depending on the user’s environment."
            Me.CriticalChance = 0
            Me.IsHMMove = False
            Me.Target = Targets.Self
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
        End Sub

        Public Shared Function GetMoveID() As Integer
            Select Case BattleScreen.Level.Terrain.TerrainType
                Case TerrainTypeEnums.Plain
                    Return 161
                Case TerrainTypeEnums.Cave
                    Return 247
                Case TerrainTypeEnums.DisortionWorld
                    Return 185
                Case TerrainTypeEnums.LongGrass
                    Return 75
                Case TerrainTypeEnums.Magma
                    Return 172
                Case TerrainTypeEnums.PondWater
                    Return 61
                Case TerrainTypeEnums.Puddles
                    Return 426
                Case TerrainTypeEnums.Rock
                    Return 157
                Case TerrainTypeEnums.Sand
                    Return 89
                Case TerrainTypeEnums.SeaWater
                    Return 56
                Case TerrainTypeEnums.Snow
                    Return 58
                Case TerrainTypeEnums.TallGrass
                    Return 402
                Case TerrainTypeEnums.Underwater
                    Return 291
            End Select

            Return 89
        End Function

    End Class

End Namespace