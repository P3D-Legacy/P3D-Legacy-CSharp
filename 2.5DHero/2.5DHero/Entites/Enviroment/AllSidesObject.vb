Imports P3D.Legacy.Core.Entities

Public Class AllSidesObject

    Inherits Entity

    Public Overrides Sub Render(effect As BasicEffect)
        Me.Draw(effect, Me.Model, Textures, True)
    End Sub

End Class