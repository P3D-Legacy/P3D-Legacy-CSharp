﻿Imports P3D.Legacy.Core.Entities
Imports P3D.Legacy.Core.Screens

Public Class WallBill

    Inherits Entity

    Protected Overrides Function CalculateCameraDistance(CPosition As Vector3) as Single
        Return MyBase.CalculateCameraDistance(CPosition) - 0.4F
    End Function

    Public Overrides Sub UpdateEntity(gameTime As GameTime)
        If Me.Rotation.Y <> Screen.Camera.Yaw Then
            Me.Rotation.Y = Screen.Camera.Yaw
            CreatedWorld = False
        End If

        MyBase.UpdateEntity(gameTime)
    End Sub

    Public Overrides Sub Render(effect As BasicEffect)
        Me.Draw(effect, Me.Model, Textures, False)
    End Sub

End Class