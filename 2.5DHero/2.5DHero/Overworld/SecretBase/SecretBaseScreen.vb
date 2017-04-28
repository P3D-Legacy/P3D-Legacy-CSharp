Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Resources.Managers.Music
Imports P3D.Legacy.Core.Resources.Managers.Sound
Imports P3D.Legacy.Core.Resources.Sound
Imports P3D.Legacy.Core.Screens

Public Class SecretBaseScreen

    Inherits Screen

    Public SavedOverworld As OverworldStorage
    Public SecretBase As SecretBase

    Dim ParticlesTexture As Texture2D

    Public Sub New()
        Me.Identification = Identifications.SecretBaseScreen
        Me.CanBePaused = True

        Me.SavedOverworld = New OverworldStorage()
        Me.SavedOverworld.SetToCurrentEnvironment()

        Me.SecretBase = New SecretBase()

        Effect = New BasicEffect(Core.GraphicsDevice)

        Camera = New SecretBaseCamera()
        Level = New Level()
        Level.Load("|")

        Me.SecretBase.LoadSecretBaseFromStore(Screen.Level)
        MusicManager.PlayMusic(Level.MusicLoop, True)
    End Sub

    Public Overrides Sub Update(gameTime As GameTime)
        'Lighting.UpdateLighting(Screen.Effect)

        Camera.Update(gameTime)
        Level.Update(gameTime)
    End Sub

    Public Overrides Sub Draw()
        TextBox.Draw()
        If Me.IsCurrentScreen() = True Then
            ChooseBox.Draw()
        End If

        Level.Draw()
    End Sub

End Class