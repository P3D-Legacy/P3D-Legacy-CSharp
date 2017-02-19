Imports P3D.Legacy.Core.Entities

Public Class AllSidesObject

    Inherits Entity

    Public Overrides Sub Render()
        Me.Draw(Me.Model, Textures, True)
    End Sub

End Class