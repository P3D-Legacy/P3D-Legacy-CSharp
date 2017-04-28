Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Entities

Public Class TurningSign

    Inherits Entity

    Dim TurningSpeed As Single = 0.01F

    Public Overrides Sub Initialize()
        MyBase.Initialize()

        Dim randomValue As Single = CSng(MathHelper.TwoPi * Core.Random.NextDouble())
        Select Case Me.ActionValue
            Case 1
                Me.Rotation.X = randomValue
            Case 2
                Me.Rotation.Z = randomValue
            Case Else
                Me.Rotation.Y = randomValue
        End Select

        If IsNumeric(Me.AdditionalValue) = True Then
            Me.TurningSpeed = CSng(CInt(Me.AdditionalValue) / 100)
        End If
        Me.CreateWorldEveryFrame = True
    End Sub

    Public Overrides Sub UpdateEntity(gameTime As GameTime)
        Select Case Me.ActionValue
            Case 1
                Me.Rotation.X += TurningSpeed
            Case 2
                Me.Rotation.Z += TurningSpeed
            Case Else
                Me.Rotation.Y += TurningSpeed
        End Select

        MyBase.UpdateEntity(gameTime)
    End Sub

    Public Overrides Sub Render(effect As BasicEffect)
        Me.Draw(effect, Me.Model, Textures, True)
    End Sub

End Class