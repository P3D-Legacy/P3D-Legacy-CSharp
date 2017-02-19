﻿Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Input
Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.Screens
Imports P3D.Legacy.Core.Screens.GUI
Imports P3D.Legacy.Core.Settings

Public Class OfflineGameWarningScreen

    Inherits Screen

    Public Sub New(ByVal currentScreen As Screen)
        Me.PreScreen = currentScreen
        Me.Identification = Identifications.OfflineGameWarningScreen

        Me.CanBePaused = False
        Me.CanChat = False
        Me.CanDrawDebug = True
        Me.CanGoFullscreen = True
        Me.CanMuteMusic = True
        Me.CanTakeScreenshot = True
        Me.MouseVisible = True
    End Sub

    Public Overrides Sub Draw()
        Me.PreScreen.Draw()

        Canvas.DrawRectangle(Core.WindowSize, New Color(0, 0, 0, 150))
        Canvas.DrawRectangle(New Rectangle(CInt(Core.WindowSize.Width / 2 - 310), 90, 620, 420), New Color(16, 16, 16))
        Canvas.DrawRectangle(New Rectangle(CInt(Core.WindowSize.Width / 2 - 300), 100, 600, 400), New Color(39, 39, 39))

        Core.SpriteBatch.DrawString(FontManager.InGameFont, "Start a new offline game", New Vector2(CSng(Core.WindowSize.Width / 2 - 280), 130), Color.White)

        Dim t As String = "If you start a game in ""Offline Mode"" by pressing the ""New Game"" button, you cannot access online features of Pokémon3D such as Trading and Trainer Customization. Click on the GameJolt button in the lower right corner in order to start a game in ""Online Mode""."
        t = t.CropStringToWidth(FontManager.MainFont, 450)

        Core.SpriteBatch.DrawString(FontManager.MainFont, t, New Vector2(CSng(Core.WindowSize.Width / 2 - 310) + 50, 240), Color.White)

        Dim d As New Dictionary(Of Buttons, String)
        d.Add(Buttons.A, "Accept")
        d.Add(Buttons.B, "Dismiss")
        DrawGamePadControls(d, New Vector2(CSng(Core.WindowSize.Width / 2) - 140, 420))
    End Sub

    Public Overrides Sub Update()
        If Controls.Accept(True, True, True) = True Then
            Core.SetScreen(Me.PreScreen)
        End If
        If Controls.Dismiss(True, True, True) = True Then
            Core.SetScreen(Me.PreScreen)
            Core.GameOptions.StartedOfflineGame = False
            Options.SaveOptions(Core.GameOptions)
        End If
    End Sub

End Class