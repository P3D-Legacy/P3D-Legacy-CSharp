Imports P3D.Legacy.Core.Entities

Public Class WallBlock

    Inherits Entity

    Protected Overrides Function CalculateCameraDistance(CPosition As Vector3) As Single
        Return MyBase.CalculateCameraDistance(CPosition) - 0.2F
    End Function

    Public Overrides Sub Render(effect As BasicEffect)
        Me.Draw(effect, Me.Model, Textures, False)
    End Sub

End Class