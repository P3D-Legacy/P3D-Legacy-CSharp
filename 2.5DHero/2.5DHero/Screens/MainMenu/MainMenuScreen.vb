﻿Imports System.Drawing
Imports System.Globalization

Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.GameJolt
Imports P3D.Legacy.Core.GameJolt.Profiles
Imports P3D.Legacy.Core.GameModes
Imports P3D.Legacy.Core.Input
Imports P3D.Legacy.Core.Objects
Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.Resources.Sound
Imports P3D.Legacy.Core.Screens.GUI
Imports P3D.Legacy.Core.Security
Imports P3D.Legacy.Core.Settings
Imports P3D.Legacy.Core.Storage
Imports P3D.Legacy.Shared.Extensions

Imports PCLExt.FileStorage

Public Class MainMenuScreen

    Inherits Screens.Screen

    Dim mainmenuIndex As Integer = 0
    Dim loadMenuIndex(3) As Integer
    Dim languageMenuIndex(3) As Integer
    Dim packsMenuIndex(3) As Integer
    Dim gameModeMenuIndex(3) As Integer
    Dim packInfoIndex As Integer = 0
    Dim deleteIndex As Integer = 0
    Public menuIndex As Integer = 0
    Dim loadGameJoltIndex As Integer = 0

    Dim currentLevel As Integer = -1
    Dim levelChangeDelay As Integer = 0

    Dim mainTexture As Texture2D

    Dim Saves As New List(Of String)
    Dim SaveNames As New List(Of String)

    Dim Languages As New List(Of CultureInfo)
    Dim LanguageNames As New List(Of String)
    Dim currentLanguage As CultureInfo = new CultureInfo("en")

    Dim PackNames As New List(Of String)
    Dim EnabledPackNames As New List(Of String)

    Dim ModeNames As New List(Of String)

    Dim tempLoadDisplay As String = ""

    Public Overrides Function GetScreenStatus() As String
        Dim s As String = "MenuIndex=" & Me.menuIndex & vbNewLine &
            "CurrentLevel=" & Me.currentLevel & vbNewLine &
            "LevelChangeDelay=" & Me.levelChangeDelay.ToString(NumberFormatInfo.InvariantInfo)

        Return s
    End Function

    Public Sub New()
        GameModeManager.SetGameModePointer("Kolben")

        Me.Identification = Identifications.MainMenuScreen
        Me.CanBePaused = False
        Me.MouseVisible = True
        Me.CanChat = False
        Me.currentLanguage = P3D.Legacy.Core.Localization.Language

        Screens.Screen.TextBox.Showing = False
        Screens.Screen.PokemonImageView.Showing = False
        Screens.Screen.ChooseBox.Showing = False

        Effect = New BasicEffect(Core.GraphicsDevice)
        Effect.FogEnabled = True
        SkyDome = New SkyDome()
        Camera = New MainMenuCamera()

        'renderTarget = New RenderTarget2D(Core.GraphicsDevice, Core.windowSize.Width, Core.windowSize.Height, False, Core.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24)
        'blurEffect = Core.Content.Load(Of Effect)("Effects\BlurEffect")

        Core.Player.Skin = "Hilbert"
        Level = New Level()
        ChangeLevel()

        mainTexture = TextureManager.GetTexture("GUI\Menus\Menu")

        Screens.Screen.Level.World.Initialize(Screens.Screen.Level.EnvironmentType, Screens.Screen.Level.WeatherType)

        Dim savePath = Path.Combine(GameController.GamePath, "Save")
        If System.IO.Directory.Exists(savePath) = False Then
            System.IO.Directory.CreateDirectory(savePath)
        End If

        GetSaves()
        GetLanguages()
        GetPacks()

        Emblem.ClearOnlineSpriteCache()
        Screens.Screen.Level.World.Initialize(Screens.Screen.Level.EnvironmentType, Screens.Screen.Level.WeatherType)
    End Sub

    Private Sub GetPacks(Optional ByVal reload As Boolean = False)
        PackNames.Clear()

        If reload = False Then
            EnabledPackNames.Clear()
            EnabledPackNames.AddRange(Core.GameOptions.ContentPackNames)
        End If

        PackNames.AddRange(EnabledPackNames)

        Dim contentPackPath = Path.Combine(GameController.GamePath, "ContentPacks")
        If System.IO.Directory.Exists(contentPackPath) = True Then
            For Each ContentPackFolder As String In System.IO.Directory.GetDirectories(contentPackPath)
                Dim newContentPack As String = ContentPackFolder.Remove(0, (contentPackPath).Length)
                If PackNames.Contains(newContentPack) = False Then
                    PackNames.Add(newContentPack)
                End If
            Next
        End If
    End Sub

    Private Sub GetLanguages()
        Languages.Clear()
        LanguageNames.Clear()

        Dim languageFiles = StorageInfo.LocalizationFolder.GetTranslationFiles()
        For Each localizationFile In languageFiles
            Dim data = localizationFile.ReadAllLines()
            Dim languageName = data(0).GetSplit(1)

            Languages.Add(localizationFile.Language)
            LanguageNames.Add(languageName)
        Next
    End Sub

    Private Sub GetSaves()
        ' -- (Aragas) Seems that this is not used anymore
        'If System.IO.File.Exists(GameController.GamePath & "\Save\lastSession.id") = True Then
        '    Dim idData As String = System.IO.File.ReadAllText(GameController.GamePath & "\Save\lastSession.id")
        '    If System.IO.Directory.Exists(GameController.GamePath & "\Save\" & idData) = False Then
        '        System.IO.File.Delete(GameController.GamePath & "\Save\lastSession.id")
        '    End If
        'End If

        Saves.Clear()
        SaveNames.Clear()

        For Each Folder As String In System.IO.Directory.GetDirectories(GameController.GamePath & "\Save")
            If Player.IsSaveGameFolder(Folder) = True Then
                Saves.Add(Folder)
            End If
        Next

        For i = 0 To Saves.Count - 1
            If i <= Saves.Count - 1 Then
                Dim entry As String = Saves(i)

                Dim userFolder = StorageInfo.SaveFolder.GetUserSaveFolder(Path.GetFileName(entry.TrimEnd("/").TrimEnd("\")))
                Dim Data() As String = userFolder.PlayerFile.ReadAllLines()

                Dim Name As String = "Missingno."
                Dim Autosave As Boolean = False

                For Each Line As String In Data
                    If Line.StartsWith("Name|") = True Then
                        Name = Line.GetSplit(1, "|")
                    End If
                    If Line.StartsWith("AutoSave|") = True Then
                        Autosave = True
                    End If
                Next

                If Autosave = True Then
                    Saves.RemoveAt(i)
                    i -= 1
                Else
                    SaveNames.Add(Name)
                End If
            End If
        Next
    End Sub

    Private Sub GetGameModes()
        ModeNames.Clear()

        For Each folder As String In System.IO.Directory.GetDirectories(GameController.GamePath & "\GameModes\")
            If System.IO.File.Exists(folder & "\GameMode.dat") = True Then
                Dim directory As String = folder
                If directory.EndsWith("\") = True Then
                    directory = directory.Remove(directory.Length - 1, 1)
                End If
                directory = directory.Remove(0, directory.LastIndexOf("\") + 1)

                ModeNames.Add(directory)
            End If
        Next
    End Sub

    Private Sub ChangeLevel()
        Dim levelCount As Integer = 0
        For Each levelPath As String In System.IO.Directory.GetFiles(GameController.GamePath & "\maps\mainmenu\")
            Dim levelFile As String = System.IO.Path.GetFileName(levelPath)
            If levelFile.StartsWith("mainmenu") = True And levelFile.EndsWith(".dat") = True Then
                levelCount += 1
            End If
        Next

        Dim levelID As Integer = Core.Random.Next(0, levelCount)

        If levelCount > 1 Then
            While levelID = currentLevel
                levelID = Core.Random.Next(0, levelCount)
            End While
        End If

        Select Case levelID
            Case 0
                Camera.Position = New Vector3(13, 2, 14)
            Case 1
                Camera.Position = New Vector3(23, 2, 10)
            Case 2
                Camera.Position = New Vector3(23, 2, 12)
            Case 3
                Camera.Position = New Vector3(24, 2, 14)
        End Select

        If Me.currentLevel <> levelID Then
            Me.currentLevel = levelID
            Level.Load("mainmenu\mainmenu" & levelID & ".dat")
        End If

        levelChangeDelay = 1000
    End Sub

    Public Overrides Sub Update()
        Lighting.UpdateLighting(Screens.Screen.Effect)

        Camera.Update()
        Level.Update()
        SkyDome.Update()

        If Core.GameInstance.IsActive = True Then
            Select Case Me.menuIndex
                Case 0
                    UpdateMainMenu()
                Case 1
                    UpdateLoadMenu()
                Case 2
                    UpdateDeleteMenu()
                Case 3
                    UpdateLanguageMenu()
                Case 4
                    UpdatePacksMenu()
                Case 5
                    UpdatePackInformationMenu()
                Case 6
                    UpdateNewGameMenu()
                Case 7
                    UpdateLoadGameJoltSaveMenu()
            End Select
        End If

        If Me.levelChangeDelay <= 0 Then
            If Core.Random.Next(0, 1000) = 0 Then
                ChangeLevel()
            End If
        Else
            Me.levelChangeDelay -= 1
        End If
    End Sub

    Dim renderTarget As RenderTarget2D
    Dim blurEffect As Effect

#Region "MainMenu"

    Public Overrides Sub Draw()
        'Core.GraphicsDevice.SetRenderTarget(renderTarget)

        'Core.GraphicsDevice.Clear(Core.BackgroundColor)

        SkyDome.Draw(45.0F)
        Level.Draw()
        World.DrawWeather(Screens.Screen.Level.World.CurrentWeather)

        'Core.GraphicsDevice.SetRenderTarget(Nothing)

        'Core.SpriteBatch.EndBatch()

        'blurEffect.CurrentTechnique = blurEffect.Techniques("GaussianBlur")
        'blurEffect.Parameters("TextureWidth").SetValue(Core.windowSize.Width)

        'Core.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise, blurEffect)

        'Core.SpriteBatch.Draw(renderTarget, New Vector2(0, 0), Color.White)

        'Core.SpriteBatch.End()
        'Core.SpriteBatch.BeginBatch()

        Select Case Me.menuIndex
            Case 0
                DrawMainMenu()
            Case 1
                DrawLoadMenu()
            Case 2
                DrawDeleteMenu()
            Case 3
                DrawLanguageMenu()
            Case 4
                DrawPacksMenu()
            Case 5
                DrawPackInformationMenu()
            Case 6
                DrawNewGameMenu()
            Case 7
                DrawLoadGameJoltSaveMenu()
        End Select
        Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, GameController.DEVELOPER_NAME, New Vector2(7, Core.ScreenSize.Height - FontManager.InGameFont.MeasureString(GameController.DEVELOPER_NAME).Y - 1), Microsoft.Xna.Framework.Color.Black)
        Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, GameController.DEVELOPER_NAME, New Vector2(4, Core.ScreenSize.Height - FontManager.InGameFont.MeasureString(GameController.DEVELOPER_NAME).Y - 4), Microsoft.Xna.Framework.Color.White)
        Core.SpriteBatch.DrawInterface(TextureManager.GetTexture("GUI\Logos\P3D"), New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 260, 40, 500, 110), Microsoft.Xna.Framework.Color.White)

        If Core.GameOptions.ShowDebug = 0 Then
            Dim s As String = GameController.GAMENAME & " " & GameController.GAMEDEVELOPMENTSTAGE & " " & GameController.GAMEVERSION
            Core.SpriteBatch.DrawInterfaceString(FontManager.MainFont, s, New Vector2(7, 7), Microsoft.Xna.Framework.Color.Black)
            Core.SpriteBatch.DrawInterfaceString(FontManager.MainFont, s, New Vector2(5, 5), Microsoft.Xna.Framework.Color.White)
        End If
    End Sub

    Private Sub DrawMainMenu()
        Dim CanvasTexture As Texture2D

        For i = 0 To 7
            Dim Text As String = ""
            Select Case i
                Case 0
                    Text = P3D.Legacy.Core.Localization.GetString("main_menu_continue")
                Case 1
                    Text = P3D.Legacy.Core.Localization.GetString("main_menu_load_game")
                Case 2
                    Text = P3D.Legacy.Core.Localization.GetString("main_menu_new_game")
                Case 3
                    Text = P3D.Legacy.Core.Localization.GetString("main_menu_quit_game")
                Case 7
                    Text = "Play online"
            End Select

            If i = mainmenuIndex Then
                CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 48, 48, 48), "")
            Else
                If i < 2 And Saves.Count = 0 Or i = 0 And System.IO.Directory.Exists(GameController.GamePath & "\Save\autosave") = False Then
                    CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(48, 0, 48, 48), "")
                Else
                    CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 0, 48, 48), "")
                End If
            End If

            If i = 4 Then
                If i = mainmenuIndex Then
                    Core.SpriteBatch.DrawInterface(mainTexture, New Microsoft.Xna.Framework.Rectangle(Core.ScreenSize.Width - 64, 0, 64, 64), New Microsoft.Xna.Framework.Rectangle(96, 80, 16, 16), Microsoft.Xna.Framework.Color.White)
                Else
                    Core.SpriteBatch.DrawInterface(mainTexture, New Microsoft.Xna.Framework.Rectangle(Core.ScreenSize.Width - 64, 0, 64, 64), New Microsoft.Xna.Framework.Rectangle(96, 64, 16, 16), Microsoft.Xna.Framework.Color.White)
                End If
            ElseIf i = 5 Then
                If i = mainmenuIndex Then
                    Core.SpriteBatch.DrawInterface(mainTexture, New Microsoft.Xna.Framework.Rectangle(Core.ScreenSize.Width - 64, 64, 64, 64), New Microsoft.Xna.Framework.Rectangle(112, 80, 16, 16), Microsoft.Xna.Framework.Color.White)
                Else
                    Core.SpriteBatch.DrawInterface(mainTexture, New Microsoft.Xna.Framework.Rectangle(Core.ScreenSize.Width - 64, 64, 64, 64), New Microsoft.Xna.Framework.Rectangle(112, 64, 16, 16), Microsoft.Xna.Framework.Color.White)
                End If
            ElseIf i = 6 Then
                If FileValidation.IsValid(False) = True And GameController.Hacker = False Then
                    If API.LoggedIn = True Then
                        If i = mainmenuIndex Then
                            Core.SpriteBatch.DrawInterface(mainTexture, New Microsoft.Xna.Framework.Rectangle(Core.ScreenSize.Width - 196, Core.ScreenSize.Height - 60, 192, 56), New Microsoft.Xna.Framework.Rectangle(160, 96, 96, 28), Microsoft.Xna.Framework.Color.White)
                        Else
                            Core.SpriteBatch.DrawInterface(mainTexture, New Microsoft.Xna.Framework.Rectangle(Core.ScreenSize.Width - 196, Core.ScreenSize.Height - 60, 192, 56), New Microsoft.Xna.Framework.Rectangle(160, 65, 96, 28), Microsoft.Xna.Framework.Color.White)
                        End If
                        Core.SpriteBatch.DrawInterfaceString(FontManager.MiniFont, "Logged in as", New Vector2(Core.ScreenSize.Width - 148, Core.ScreenSize.Height - 54), Microsoft.Xna.Framework.Color.White)
                        Core.SpriteBatch.DrawInterfaceString(FontManager.MiniFont, API.username, New Vector2(Core.ScreenSize.Width - 148, Core.ScreenSize.Height - 34), New Microsoft.Xna.Framework.Color(204, 255, 0))
                    Else
                        If i = mainmenuIndex Then
                            Core.SpriteBatch.DrawInterface(mainTexture, New Microsoft.Xna.Framework.Rectangle(Core.ScreenSize.Width - 60, Core.ScreenSize.Height - 60, 56, 56), New Microsoft.Xna.Framework.Rectangle(129, 96, 28, 28), Microsoft.Xna.Framework.Color.White)
                        Else
                            Core.SpriteBatch.DrawInterface(mainTexture, New Microsoft.Xna.Framework.Rectangle(Core.ScreenSize.Width - 60, Core.ScreenSize.Height - 60, 56, 56), New Microsoft.Xna.Framework.Rectangle(129, 65, 28, 28), Microsoft.Xna.Framework.Color.White)
                        End If
                    End If
                Else
                    Core.SpriteBatch.DrawInterfaceString(FontManager.MiniFont, "File Validation failed. Download a new copy of the game to fix this.", New Vector2(220, Core.ScreenSize.Height - 30), Microsoft.Xna.Framework.Color.White)
                End If
            ElseIf i = 7 Then
                If API.LoggedIn = True And FileValidation.IsValid(False) = True And GameController.Hacker = False Then
                    Canvas.DrawImageBorder(CanvasTexture, 2, New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2), 160 + 128, 320, 64), True)
                    Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Text, New Vector2(CInt(Core.ScreenSize.Width / 2) - (FontManager.InGameFont.MeasureString(Text).X / 2) + 160 + 20, 196 + 128), Microsoft.Xna.Framework.Color.Black)
                End If
            ElseIf i = 1 And API.LoggedIn = True Then
                Canvas.DrawImageBorder(CanvasTexture, 2, New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 180 - 160 - 20, 160 + i * 128, 320, 64), True)
                Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Text, New Vector2(CInt(Core.ScreenSize.Width / 2) - (FontManager.InGameFont.MeasureString(Text).X / 2) - 10 - 160 - 20, 196 + i * 128), Microsoft.Xna.Framework.Color.Black)
            Else
                Canvas.DrawImageBorder(CanvasTexture, 2, New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 180, 160 + i * 128, 320, 64), True)
                Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Text, New Vector2(CInt(Core.ScreenSize.Width / 2) - (FontManager.InGameFont.MeasureString(Text).X / 2) - 10, 196 + i * 128), Microsoft.Xna.Framework.Color.Black)
            End If
        Next

        Dim d As New Dictionary(Of Buttons, String)
        d.Add(Buttons.A, "Accept")
        If API.LoggedIn = True Then
            DrawGamePadControls(d, New Vector2(Core.ScreenSize.Width - 170, Core.ScreenSize.Height - 100))
        Else
            DrawGamePadControls(d, New Vector2(Core.ScreenSize.Width - 234, Core.ScreenSize.Height - 40))
        End If
    End Sub

    Private Sub UpdateMainMenu()
        If Controls.Up(True, True) = True Then
            Me.mainmenuIndex -= 1
        End If
        If Controls.Down(True, True) = True Then
            Me.mainmenuIndex += 1
        End If

        If Core.GameInstance.IsMouseVisible = True Then
            For i = 0 To 7
                If i = 4 Then
                    If Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(Core.ScreenSize.Width - 64, 0, 64, 64)).Contains(MouseHandler.MousePosition) = True Then
                        Me.mainmenuIndex = 4

                        If MouseHandler.ButtonPressed(MouseHandler.MouseButtons.LeftButton) = True Then
                            LanguageButton()
                        End If
                    End If
                ElseIf i = 5 Then
                    If Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(Core.ScreenSize.Width - 64, 64, 64, 64)).Contains(MouseHandler.MousePosition) = True Then
                        Me.mainmenuIndex = 5

                        If MouseHandler.ButtonPressed(MouseHandler.MouseButtons.LeftButton) = True Then
                            PacksButton()
                        End If
                    End If
                ElseIf i = 6 Then
                    Dim r As Microsoft.Xna.Framework.Rectangle = Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(Core.ScreenSize.Width - 196, Core.ScreenSize.Height - 60, 192, 56))
                    If API.LoggedIn = False Then
                        r = Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(Core.ScreenSize.Width - 64, Core.ScreenSize.Height - 64, 64, 64))
                    End If

                    If r.Contains(MouseHandler.MousePosition) = True Then
                        Me.mainmenuIndex = 6

                        If MouseHandler.ButtonPressed(MouseHandler.MouseButtons.LeftButton) = True Then
                            GameJoltButton()
                        End If
                    End If
                ElseIf i = 1 And API.LoggedIn = True Then
                    If Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 180 - 160 - 20, 160 + i * 128, 320 + 32, 64 + 32)).Contains(MouseHandler.MousePosition) = True Then
                        Me.mainmenuIndex = i
                        If MouseHandler.ButtonPressed(MouseHandler.MouseButtons.LeftButton) = True Then
                            LoadGameButton()
                        End If
                    End If
                ElseIf i = 7 And API.LoggedIn = True Then
                    If Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2), 160 + 128, 320 + 32, 64 + 32)).Contains(MouseHandler.MousePosition) = True Then
                        Me.mainmenuIndex = i
                        If MouseHandler.ButtonPressed(MouseHandler.MouseButtons.LeftButton) = True Then
                            LoadGameJoltButton()
                        End If
                    End If
                Else
                    If Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 180, 160 + i * 128, 320 + 32, 64 + 32)).Contains(MouseHandler.MousePosition) = True Then
                        Me.mainmenuIndex = i

                        If MouseHandler.ButtonPressed(MouseHandler.MouseButtons.LeftButton) = True Then
                            Select Case Me.mainmenuIndex
                                Case 0
                                    ContinueButton()
                                Case 1
                                    LoadGameButton()
                                Case 2
                                    NewGameButton()
                                Case 3
                                    CloseGameButton()
                            End Select
                        End If
                    End If
                End If
            Next
        End If

        If FileValidation.IsValid(False) = True And GameController.Hacker = False Then
            If API.LoggedIn = True Then
                mainmenuIndex = CInt(MathHelper.Clamp(mainmenuIndex, 0, 7))
            Else
                mainmenuIndex = CInt(MathHelper.Clamp(mainmenuIndex, 0, 6))
            End If
        Else
            mainmenuIndex = mainmenuIndex.Clamp(0, 5)
        End If

        If Controls.Accept(False, True) = True Then
            Select Case Me.mainmenuIndex
                Case 0
                    ContinueButton()
                Case 1
                    LoadGameButton()
                Case 2
                    NewGameButton()
                Case 3
                    CloseGameButton()
                Case 4
                    LanguageButton()
                Case 5
                    PacksButton()
                Case 6
                    GameJoltButton()
                Case 7
                    LoadGameJoltButton()
            End Select
        End If
    End Sub

    Private Sub PacksButton()
        GetPacks()
        packsMenuIndex(0) = 0
        packsMenuIndex(2) = 0

        Me.menuIndex = 4
    End Sub

    Private Sub LanguageButton()
        GetLanguages()
        If Languages.Contains(currentLanguage) = True Then
            languageMenuIndex(0) = Languages.IndexOf(currentLanguage)
        End If

        Me.menuIndex = 3
    End Sub

    Private Sub ContinueButton()
        If Saves.Count > 0 And Player.IsSaveGameFolder(GameController.GamePath & "\Save\autosave") = True Then
            Core.Player.IsGamejoltSave = False
            Core.Player.LoadGame("autosave")

            Core.SetScreen(New JoinServerScreen(Me))
        End If
    End Sub

    Private Sub LoadGameButton()
        GetSaves()

        If Saves.Count > 0 Then
            Me.menuIndex = 1
        End If
    End Sub

    Private Sub LoadGameJoltButton()
        If FileValidation.IsValid(False) = True And GameController.Hacker = False Then
            If API.LoggedIn = True Then
                Core.GameJoltSave.DownloadSave(API.GameJoltID, True)
            End If

            Me.menuIndex = 7
        End If
    End Sub

    Private Sub CloseGameButton()
        Options.SaveOptions(Core.GameOptions)
        Core.GameInstance.Exit()
    End Sub

    Private Sub GameJoltButton()
        If API.LoggedIn
            SessionManager.Close()
            API.LoggedIn = False
        Else 
            Dim apiCall As New APICall(AddressOf VerifyResult)
            apiCall.VerifyUser(API.Username, API.Token)
        End If
        'If FileValidation.IsValid(False) = True And GameController.Hacker = False Then
        '    Core.SetScreen(New GameJolt.LogInScreen(Me))
        'End If
    End Sub
    'TODO One for all method, doubles in CommandLineArgHandler.cs
    Private Sub VerifyResult(result As String)
        Dim list As List(Of API.JoltValue) = API.HandleData(result)
            If CBool(list(0).Value) = True Then
                API.LoggedIn = True

                Dim apiCall As New APICall(AddressOf HandleUserData)
                apiCall.FetchUserdata(API.username)
            Else
                API.LoggedIn = False
            End If
    End Sub
    Private Sub HandleUserData(result As String)
            Dim list As List(Of API.JoltValue) = API.HandleData(result)
            For Each item As API.JoltValue In list
                If item.Name = "id" Then
                    API.GameJoltID = item.Value 'set the public shared field to the GameJolt ID.

                    If GameController.UPDATEONLINEVERSION = True And GameController.IS_DEBUG_ACTIVE = True Then
                        Dim apiCall As New APICall
                        apiCall.SetStorageDataRestricted("ONLINEVERSION", GameController.GAMEVERSION)
                        Logger.Debug("UPDATED ONLINE VERSION TO: " & GameController.GAMEVERSION)
                    End If
                    Exit For
                End If
            Next
    End Sub

#End Region

#Region "LoadMenu"

    Private Sub DrawLoadMenu()
        Dim CanvasTexture As Texture2D
        CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 0, 48, 48), "")

        For i = 0 To 3
            Dim c As Microsoft.Xna.Framework.Color = Microsoft.Xna.Framework.Color.White
            If i + loadMenuIndex(2) = loadMenuIndex(0) Then
                c = New Microsoft.Xna.Framework.Color(101, 142, 255)
            End If

            Canvas.DrawRectangle(New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 258, 180 + i * 50, 480, 48), c, True)
        Next

        Canvas.DrawScrollBar(New Vector2(CInt(Core.ScreenSize.Width / 2) + 250, 180), Saves.Count, 4, loadMenuIndex(2), New Size(4, 200), False, New Microsoft.Xna.Framework.Color(190, 190, 190), New Microsoft.Xna.Framework.Color(63, 63, 63), True)

        Dim x As Integer = Saves.Count - 1
        x = CInt(MathHelper.Clamp(x, 0, 3))

        For i = 0 To x
            Dim Name As String = SaveNames(i + loadMenuIndex(2))

            If i + loadMenuIndex(2) = loadMenuIndex(0) Then
                Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Name, New Vector2(CInt(Core.ScreenSize.Width / 2) - 245, 191 + i * 50), Microsoft.Xna.Framework.Color.Black)
                Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Name, New Vector2(CInt(Core.ScreenSize.Width / 2) - 248, 188 + i * 50), Microsoft.Xna.Framework.Color.White)
            Else
                Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Name, New Vector2(CInt(Core.ScreenSize.Width / 2) - 248, 188 + i * 50), Microsoft.Xna.Framework.Color.Black)
            End If
        Next

        Canvas.DrawImageBorder(CanvasTexture, 2, New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 272, 388, 512, 128), True)

        If tempLoadDisplay = "" Then
            Dim dispName As String = "(Unknown)"
            Dim dispBadges As String = "(Unknown)"
            Dim dispPlayTime As String = "(Unknown)"
            Dim dispLocation As String = "(Unknown)"
            Dim dispGameMode As String = "Kolben"

            Dim userFolder = StorageInfo.SaveFolder.GetUserSaveFolder(Saves(loadMenuIndex(0)).TrimEnd("/").TrimEnd("\"))
            Dim Data() As String = userFolder.PlayerFile.ReadAllLines()
            For Each Line As String In Data
                If Line.Contains("|") = True Then
                    Dim ID As String = Line.Remove(Line.IndexOf("|"))
                    Dim Value As String = Line.Remove(0, Line.IndexOf("|") + 1)
                    Select Case ID
                        Case "Name"
                            dispName = Value
                        Case "Badges"
                            Dim bCount As Integer = 0
                            If Value = "0" Then
                                bCount = 0
                            Else
                                If Value.Contains(",") = False Then
                                    bCount = 1
                                Else
                                    Dim s() As String = Value.Split(CChar(","))
                                    bCount = s.Length
                                End If
                            End If
                            dispBadges = bCount.ToString(NumberFormatInfo.InvariantInfo)
                        Case "PlayTime"
                            Dim dd() As String = Value.Split(CChar(","))

                            Dim tSpan As TimeSpan = Nothing
                            If dd.Count = 3 Then
                                tSpan = New TimeSpan(CInt(dd(0)), CInt(dd(1)), CInt(dd(2)))
                            ElseIf dd.Count = 4 Then
                                tSpan = New TimeSpan(CInt(dd(3)), CInt(dd(0)), CInt(dd(1)), CInt(dd(2)))
                            End If

                            dispPlayTime = TimeHelpers.GetDisplayTime(tSpan, True)
                        Case "location"
                            dispLocation = Value
                        Case "GameMode"
                            dispGameMode = Value
                    End Select
                End If
            Next

            Me.tempLoadDisplay = P3D.Legacy.Core.Localization.GetString("load_menu_name") & ": " & dispName & vbNewLine &
                P3D.Legacy.Core.Localization.GetString("load_menu_gamemode") & ": " & dispGameMode & vbNewLine &
                P3D.Legacy.Core.Localization.GetString("load_menu_badges") & ": " & dispBadges & vbNewLine &
                P3D.Legacy.Core.Localization.GetString("load_menu_location") & ": " & P3D.Legacy.Core.Localization.GetString("Places_" & dispLocation) & vbNewLine &
                P3D.Legacy.Core.Localization.GetString("load_menu_time") & ": " & dispPlayTime
        End If

        Core.SpriteBatch.DrawInterfaceString(FontManager.MiniFont, tempLoadDisplay, New Vector2(CInt(Core.ScreenSize.Width / 2) - 252, 416), Microsoft.Xna.Framework.Color.Black)

        For i = 0 To 2
            Dim Text As String = ""
            Select Case i
                Case 0
                    Text = P3D.Legacy.Core.Localization.GetString("load_menu_load")
                Case 1
                    Text = P3D.Legacy.Core.Localization.GetString("load_menu_delete")
                Case 2
                    Text = P3D.Legacy.Core.Localization.GetString("load_menu_back")
            End Select

            If i = loadMenuIndex(1) Then
                CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 48, 48, 48), "")
            Else
                CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 0, 48, 48), "")
            End If

            Canvas.DrawImageBorder(CanvasTexture, 2, New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 272 + i * 192, 550, 128, 64), True)
            Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Text, New Vector2(CInt(Core.ScreenSize.Width / 2) - 254 + i * 192, 582), Microsoft.Xna.Framework.Color.Black)
        Next

        Dim d As New Dictionary(Of Buttons, String)
        d.Add(Buttons.A, "Accept")
        d.Add(Buttons.B, "Back")
        DrawGamePadControls(d)
    End Sub

    Private Sub UpdateLoadMenu()
        If Controls.Up(True, True, True) = True Then
            loadMenuIndex(0) -= 1
            If loadMenuIndex(0) - loadMenuIndex(2) < 0 Then
                loadMenuIndex(2) -= 1
            End If
            tempLoadDisplay = ""
        End If
        If Controls.Down(True, True, True) = True Then
            loadMenuIndex(0) += 1
            If loadMenuIndex(0) + loadMenuIndex(2) > 3 Then
                loadMenuIndex(2) += 1
            End If
            tempLoadDisplay = ""
        End If

        If Core.GameInstance.IsMouseVisible = True Then
            For i = 0 To 2
                If Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 272 + i * 192, 550, 128 + 32, 64 + 32)).Contains(MouseHandler.MousePosition) = True Then
                    Me.loadMenuIndex(1) = i

                    If MouseHandler.ButtonPressed(MouseHandler.MouseButtons.LeftButton) = True Then
                        Select Case loadMenuIndex(1)
                            Case 0
                                Core.Player.IsGamejoltSave = False
                                Core.Player.LoadGame(Path.GetFileName(Saves(loadMenuIndex(0))))

                                Core.SetScreen(New JoinServerScreen(Me))
                            Case 1
                                Me.menuIndex = 2
                            Case 2
                                Me.menuIndex = 0

                                tempLoadDisplay = ""
                        End Select
                    End If
                End If
            Next
        End If

        For i = 0 To 3
            If Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 258, 180 + i * 50, 480, 48)).Contains(MouseHandler.MousePosition) = True Then
                If MouseHandler.ButtonPressed(MouseHandler.MouseButtons.LeftButton) = True Then
                    Me.loadMenuIndex(0) = i + loadMenuIndex(2)
                    tempLoadDisplay = ""
                End If
            End If
        Next

        loadMenuIndex(0) = CInt(MathHelper.Clamp(loadMenuIndex(0), 0, Saves.Count - 1))
        loadMenuIndex(2) = CInt(MathHelper.Clamp(loadMenuIndex(2), 0, Saves.Count - 4))

        If Controls.Right(True, True, False) = True Then
            loadMenuIndex(1) += 1
        End If
        If Controls.Left(True, True, False) = True Then
            loadMenuIndex(1) -= 1
        End If

        loadMenuIndex(1) = CInt(MathHelper.Clamp(loadMenuIndex(1), 0, 2))

        If Controls.Accept(False, True) = True Then
            Select Case loadMenuIndex(1)
                Case 0
                    Core.Player.IsGamejoltSave = False
                    Core.Player.LoadGame(System.IO.Path.GetFileName(Saves(loadMenuIndex(0))))

                    Core.SetScreen(New JoinServerScreen(Me))
                Case 1
                    Me.menuIndex = 2
                Case 2
                    Me.menuIndex = 0

                    tempLoadDisplay = ""
            End Select
        End If

        If Controls.Dismiss() = True Then
            Me.menuIndex = 0
        End If
    End Sub

#End Region

#Region "LoadGameJoltSaveMenu"

    Private Sub DrawLoadGameJoltSaveMenu()
        If Core.GameJoltSave.DownloadFailed = False Then
            Dim downloaded As Boolean = Core.GameJoltSave.DownloadFinished

            If downloaded = True Then
                Dim r As New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 256, 300, 512, 128)
                If Core.ScaleScreenRec(r).Contains(MouseHandler.MousePosition) = True And Core.GameInstance.IsMouseVisible = True Or Core.GameInstance.IsMouseVisible = False And loadGameJoltIndex = 0 Then
                    Canvas.DrawRectangle(Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 264, 292, 528, 144)), New Microsoft.Xna.Framework.Color(255, 255, 255, 150))
                End If

                If API.UserBanned(Core.GameJoltSave.GameJoltID) = True Then
                    Dim reason As String = API.GetBanReasonByID(API.BanReasonIDForUser(Core.GameJoltSave.GameJoltID))
                    Core.SpriteBatch.DrawInterfaceString(FontManager.MainFont, reason, New Vector2(CSng(Core.ScreenSize.Width / 2 - FontManager.MainFont.MeasureString(reason).X / 2) + 2, 260 + 2), Microsoft.Xna.Framework.Color.Black)
                    Core.SpriteBatch.DrawInterfaceString(FontManager.MainFont, reason, New Vector2(CSng(Core.ScreenSize.Width / 2 - FontManager.MainFont.MeasureString(reason).X / 2), 260), Microsoft.Xna.Framework.Color.Red)
                End If

                Emblem.Draw(API.username, Core.GameJoltSave.GameJoltID, Core.GameJoltSave.Points, Core.GameJoltSave.Gender, Core.GameJoltSave.EmblemS, Core.ScaleScreenVec(New Vector2(CSng(Core.ScreenSize.Width / 2) - 256, 300)), CSng(4 * Core.SpriteBatch.InterfaceScale), Core.GameJoltSave.DownloadedSprite)

                Dim y As Integer = 0
                If Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(r.X + 32 + r.Width, r.Y, 32, 32)).Contains(MouseHandler.MousePosition) = True And Core.GameInstance.IsMouseVisible = True Or Core.GameInstance.IsMouseVisible = False And loadGameJoltIndex = 1 Then
                    y = 16

                    Core.SpriteBatch.DrawInterfaceString(FontManager.MiniFont, "Change to male.", New Vector2(r.X + 64 + 4 + r.Width, r.Y + 4), Microsoft.Xna.Framework.Color.White)
                End If
                Core.SpriteBatch.DrawInterface(mainTexture, New Microsoft.Xna.Framework.Rectangle(r.X + 32 + r.Width, r.Y, 32, 32), New Microsoft.Xna.Framework.Rectangle(144, 32 + y, 16, 16), Microsoft.Xna.Framework.Color.White)

                y = 0
                If Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(r.X + 32 + r.Width, r.Y + 48, 32, 32)).Contains(MouseHandler.MousePosition) = True And Core.GameInstance.IsMouseVisible = True Or Core.GameInstance.IsMouseVisible = False And loadGameJoltIndex = 2 Then
                    y = 16

                    Core.SpriteBatch.DrawInterfaceString(FontManager.MiniFont, "Change to female.", New Vector2(r.X + 64 + 4 + r.Width, r.Y + 4 + 48), Microsoft.Xna.Framework.Color.White)
                End If
                Core.SpriteBatch.DrawInterface(mainTexture, New Microsoft.Xna.Framework.Rectangle(r.X + 32 + r.Width, r.Y + 48, 32, 32), New Microsoft.Xna.Framework.Rectangle(160, 32 + y, 16, 16), Microsoft.Xna.Framework.Color.White)

                y = 0
                If Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(r.X + 32 + r.Width, r.Y + 48 + 48, 32, 32)).Contains(MouseHandler.MousePosition) = True And Core.GameInstance.IsMouseVisible = True Or Core.GameInstance.IsMouseVisible = False And loadGameJoltIndex = 3 Then
                    y = 16

                    Core.SpriteBatch.DrawInterfaceString(FontManager.MiniFont, "Reset save.", New Vector2(r.X + 64 + 4 + r.Width, r.Y + 4 + 48 + 48), Microsoft.Xna.Framework.Color.White)
                End If
                Core.SpriteBatch.DrawInterface(mainTexture, New Microsoft.Xna.Framework.Rectangle(r.X + 32 + r.Width, r.Y + 48 + 48, 32, 32), New Microsoft.Xna.Framework.Rectangle(176, 32 + y, 16, 16), Microsoft.Xna.Framework.Color.White)
            Else
                Dim downloadProgress As Integer = Core.GameJoltSave.DownloadProgress
                Dim total As Integer = Core.GameJoltSave.TotalDownloadItems

                Dim downloadtext As String = "Downloading profile"
                Core.SpriteBatch.DrawInterfaceString(FontManager.MainFont, downloadtext & LoadingDots.Dots, New Vector2(CSng(Core.ScreenSize.Width / 2 - FontManager.MainFont.MeasureString(downloadtext).X / 2) + 2, 322), Microsoft.Xna.Framework.Color.Black)
                Core.SpriteBatch.DrawInterfaceString(FontManager.MainFont, downloadtext & LoadingDots.Dots, New Vector2(CSng(Core.ScreenSize.Width / 2 - FontManager.MainFont.MeasureString(downloadtext).X / 2), 320), Microsoft.Xna.Framework.Color.White)

                Canvas.DrawScrollBar(New Vector2(CInt(Core.ScreenSize.Width / 2) - 256, 400), total, downloadProgress, 0, New Size(512, 8), True, Microsoft.Xna.Framework.Color.Black, Microsoft.Xna.Framework.Color.White, True)
            End If
        Else
            Dim failText As String = "The download failed! Please try again."

            Core.SpriteBatch.DrawInterfaceString(FontManager.MainFont, failText, New Vector2(CSng(Core.ScreenSize.Width / 2 - FontManager.MainFont.MeasureString(failText).X / 2) + 2, 322), Microsoft.Xna.Framework.Color.Black)
            Core.SpriteBatch.DrawInterfaceString(FontManager.MainFont, failText, New Vector2(CSng(Core.ScreenSize.Width / 2 - FontManager.MainFont.MeasureString(failText).X / 2), 320), Microsoft.Xna.Framework.Color.DarkRed)
        End If

        If ControllerHandler.IsConnected() = False Then
            Dim text As String = "Right-Click to quit to the main menu"
            Core.SpriteBatch.DrawInterfaceString(FontManager.MainFont, text, New Vector2(CSng(Core.ScreenSize.Width / 2 - FontManager.MainFont.MeasureString(text).X / 2) + 2, 502), Microsoft.Xna.Framework.Color.Black)
            Core.SpriteBatch.DrawInterfaceString(FontManager.MainFont, text, New Vector2(CSng(Core.ScreenSize.Width / 2 - FontManager.MainFont.MeasureString(text).X / 2), 500), Microsoft.Xna.Framework.Color.White)
        End If

        Dim d As New Dictionary(Of Buttons, String)
        d.Add(Buttons.A, "Select")
        d.Add(Buttons.B, "Back")
        Me.DrawGamePadControls(d)
    End Sub

    Private Sub UpdateLoadGameJoltSaveMenu()
        Dim downloaded As Boolean = Core.GameJoltSave.DownloadFinished

        If downloaded = True Then
            If Controls.Down(True, True, False, True, True) = True Or Controls.Right(True, True, False, True, True) = True Then
                loadGameJoltIndex += 1
            End If
            If Controls.Up(True, True, False, True, True) = True Or Controls.Left(True, True, False, True, True) = True Then
                loadGameJoltIndex -= 1
            End If

            loadGameJoltIndex = loadGameJoltIndex.Clamp(0, 3)

            If Controls.Accept(True, True) = True Then
                If Core.GameInstance.IsMouseVisible = False And loadGameJoltIndex = 0 Or Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 256, 300, 512, 128)).Contains(MouseHandler.MousePosition) = True And Core.GameInstance.IsMouseVisible = True Then
                    Core.Player.IsGamejoltSave = True
                    Core.Player.LoadGame("GAMEJOLTSAVE")

                    Core.SetScreen(New JoinServerScreen(Me))
                End If

                Dim r As Microsoft.Xna.Framework.Rectangle = Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 256, 300, 512, 128))
                If Core.GameInstance.IsMouseVisible = False And loadGameJoltIndex = 1 Or Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(r.X + 32 + r.Width, r.Y, 32, 32)).Contains(MouseHandler.MousePosition) = True And Core.GameInstance.IsMouseVisible = True Then
                    ButtonChangeMale()
                End If
                If Core.GameInstance.IsMouseVisible = False And loadGameJoltIndex = 2 Or Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(r.X + 32 + r.Width, r.Y + 48, 32, 32)).Contains(MouseHandler.MousePosition) = True And Core.GameInstance.IsMouseVisible = True Then
                    ButtonChangeFemale()
                End If
                If Core.GameInstance.IsMouseVisible = False And loadGameJoltIndex = 3 Or Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(r.X + 32 + r.Width, r.Y + 48 + 48, 32, 32)).Contains(MouseHandler.MousePosition) = True And Core.GameInstance.IsMouseVisible = True Then
                    ButtonResetSave()
                End If
            End If
        End If

        If Controls.Dismiss(True, True) = True Then
            Me.menuIndex = 0
        End If
    End Sub

    Private Sub ButtonChangeMale()
        Core.GameJoltSave.Gender = "0"

        Core.Player.Skin = Emblem.GetPlayerSpriteFile(Emblem.GetPlayerLevel(Core.GameJoltSave.Points), Core.GameJoltSave.GameJoltID, Core.GameJoltSave.Gender)
        Select Case Core.GameJoltSave.Gender
            Case "0"
                Core.Player.Male = True
            Case "1"
                Core.Player.Male = False
            Case Else
                Core.Player.Male = True
        End Select
    End Sub

    Private Sub ButtonChangeFemale()
        Core.GameJoltSave.Gender = "1"

        Core.Player.Skin = Emblem.GetPlayerSpriteFile(Emblem.GetPlayerLevel(Core.GameJoltSave.Points), Core.GameJoltSave.GameJoltID, Core.GameJoltSave.Gender)
        Select Case Core.GameJoltSave.Gender
            Case "0"
                Core.Player.Male = True
            Case "1"
                Core.Player.Male = False
            Case Else
                Core.Player.Male = True
        End Select
    End Sub

    Private Sub ButtonResetSave()
        Core.GameJoltSave.ResetSave()
    End Sub

#End Region

#Region "LanguageMenu"

    Private Sub DrawLanguageMenu()
        Dim CanvasTexture As Texture2D

        For i = 0 To 3
            Dim c As Microsoft.Xna.Framework.Color = Microsoft.Xna.Framework.Color.White
            If i + languageMenuIndex(2) = languageMenuIndex(0) Then
                c = New Microsoft.Xna.Framework.Color(101, 142, 255)
            End If

            Canvas.DrawRectangle(New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 258, 180 + i * 50, 480, 48), c, True)
        Next

        Canvas.DrawScrollBar(New Vector2(CInt(Core.ScreenSize.Width / 2) + 250, 180), Languages.Count, 4, languageMenuIndex(2), New Size(4, 200), False, New Microsoft.Xna.Framework.Color(190, 190, 190), New Microsoft.Xna.Framework.Color(63, 63, 63), True)

        Dim x As Integer = Languages.Count - 1
        x = CInt(MathHelper.Clamp(x, 0, 3))

        For i = 0 To x
            Dim Name As String = LanguageNames(i + languageMenuIndex(2))

            If i + languageMenuIndex(2) = languageMenuIndex(0) Then
                Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Name, New Vector2(CInt(Core.ScreenSize.Width / 2) - 245, 191 + i * 50), Microsoft.Xna.Framework.Color.Black)
                Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Name, New Vector2(CInt(Core.ScreenSize.Width / 2) - 248, 188 + i * 50), Microsoft.Xna.Framework.Color.White)
            Else
                Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Name, New Vector2(CInt(Core.ScreenSize.Width / 2) - 248, 188 + i * 50), Microsoft.Xna.Framework.Color.Black)
            End If
        Next

        CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 0, 48, 48), "")

        For i = 0 To 1
            Dim Text As String = ""
            Select Case i
                Case 0
                    Text = P3D.Legacy.Core.Localization.GetString("language_menu_apply")
                Case 1
                    Text = P3D.Legacy.Core.Localization.GetString("language_menu_back")
            End Select

            If i = languageMenuIndex(1) Then
                CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 48, 48, 48), "")
            Else
                CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 0, 48, 48), "")
            End If

            Canvas.DrawImageBorder(CanvasTexture, 2, New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 208 + i * 192, 550, 128, 64), True)
            Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Text, New Vector2(CInt(Core.ScreenSize.Width / 2) - 190 + i * 192, 582), Microsoft.Xna.Framework.Color.Black)
        Next
    End Sub

    Private Sub UpdateLanguageMenu()
        Dim currentIndex As Integer = languageMenuIndex(0)

        If Controls.Up(True, True, True) = True Then
            languageMenuIndex(0) -= 1
            If languageMenuIndex(0) - languageMenuIndex(2) < 0 Then
                languageMenuIndex(2) -= 1
            End If
        End If
        If Controls.Down(True, True, True) = True Then
            languageMenuIndex(0) += 1
            If languageMenuIndex(0) + languageMenuIndex(2) > 3 Then
                languageMenuIndex(2) += 1
            End If
        End If

        If Core.GameInstance.IsMouseVisible = True Then
            For i = 0 To 1
                If Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 208 + i * 192, 550, 128 + 32, 64 + 32)).Contains(MouseHandler.MousePosition) = True Then
                    Me.languageMenuIndex(1) = i

                    If MouseHandler.ButtonPressed(MouseHandler.MouseButtons.LeftButton) = True Then
                        Select Case languageMenuIndex(1)
                            Case 0
                                currentLanguage = Languages(languageMenuIndex(0))
                                Options.SaveOptions(Core.GameOptions)
                                Me.menuIndex = 0
                            Case 1
                                P3D.Legacy.Core.Localization.Load(currentLanguage)
                                Me.menuIndex = 0
                        End Select
                    End If
                End If
            Next

            For i = 0 To 3
                If Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 258, 180 + i * 50, 480, 48)).Contains(MouseHandler.MousePosition) = True Then
                    If MouseHandler.ButtonPressed(MouseHandler.MouseButtons.LeftButton) = True Then
                        Me.languageMenuIndex(0) = i + languageMenuIndex(2)
                    End If
                End If
            Next
        End If

        languageMenuIndex(0) = CInt(MathHelper.Clamp(languageMenuIndex(0), 0, Languages.Count - 1))
        languageMenuIndex(2) = CInt(MathHelper.Clamp(languageMenuIndex(2), 0, Languages.Count - 4))

        If languageMenuIndex(0) <> currentIndex Then
            P3D.Legacy.Core.Localization.Load(Languages(languageMenuIndex(0)))
        End If

        If Controls.Right(True, True, False) = True Then
            languageMenuIndex(1) += 1
        End If
        If Controls.Left(True, True, False) = True Then
            languageMenuIndex(1) -= 1
        End If

        languageMenuIndex(1) = CInt(MathHelper.Clamp(languageMenuIndex(1), 0, 1))

        If Controls.Accept(False, True) = True Then
            Select Case languageMenuIndex(1)
                Case 0
                    currentLanguage = Languages(languageMenuIndex(0))
                    Options.SaveOptions(Core.GameOptions)
                    Me.menuIndex = 0
                Case 1
                    P3D.Legacy.Core.Localization.Load(currentLanguage)
                    Me.menuIndex = 0
            End Select
        End If

        If Controls.Dismiss() = True Then
            Me.menuIndex = 0
        End If
    End Sub

#End Region

#Region "PackMenu"

    Private Sub DrawPacksMenu()
        Dim CanvasTexture As Texture2D
        Dim isSelectedEnabled As Boolean = False

        For i = 0 To 3
            Dim c As Microsoft.Xna.Framework.Color = Microsoft.Xna.Framework.Color.White
            If i + packsMenuIndex(2) = packsMenuIndex(0) Then
                c = New Microsoft.Xna.Framework.Color(101, 142, 255)

                If EnabledPackNames.Count > 0 Then
                    If EnabledPackNames.Contains(PackNames(i + packsMenuIndex(2))) = True Then
                        isSelectedEnabled = True
                    End If
                End If
            End If

            Canvas.DrawRectangle(New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 258, 180 + i * 50, 480, 48), c, True)
        Next

        Canvas.DrawScrollBar(New Vector2(CInt(Core.ScreenSize.Width / 2) + 250, 180), PackNames.Count, 4, packsMenuIndex(2), New Size(4, 200), False, New Microsoft.Xna.Framework.Color(190, 190, 190), New Microsoft.Xna.Framework.Color(63, 63, 63), True)

        Dim x As Integer = PackNames.Count - 1
        x = CInt(MathHelper.Clamp(x, 0, 3))

        If PackNames.Count > 0 Then
            For i = 0 To x
                Dim Name As String = PackNames(i + packsMenuIndex(2))
                Dim textColor As Microsoft.Xna.Framework.Color = Microsoft.Xna.Framework.Color.Gray

                If EnabledPackNames.Contains(Name) = True Then
                    Name &= " (" & P3D.Legacy.Core.Localization.GetString("pack_menu_enabled") & ")"
                    textColor = Microsoft.Xna.Framework.Color.Black
                End If

                If i + packsMenuIndex(2) = packsMenuIndex(0) Then
                    Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Name, New Vector2(CInt(Core.ScreenSize.Width / 2) - 245, 191 + i * 50), textColor)
                    Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Name, New Vector2(CInt(Core.ScreenSize.Width / 2) - 248, 188 + i * 50), Microsoft.Xna.Framework.Color.White)
                Else
                    Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Name, New Vector2(CInt(Core.ScreenSize.Width / 2) - 248, 188 + i * 50), textColor)
                End If
            Next
        End If

        CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 0, 48, 48), "")

        For i = 0 To 1
            Dim Text As String = ""
            Select Case i
                Case 0
                    Text = P3D.Legacy.Core.Localization.GetString("pack_menu_apply")
                Case 1
                    Text = P3D.Legacy.Core.Localization.GetString("pack_menu_back")
            End Select

            If i = packsMenuIndex(1) Then
                CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 48, 48, 48), "")
            Else
                CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 0, 48, 48), "")
            End If

            Canvas.DrawImageBorder(CanvasTexture, 2, New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 208 + i * 192, 550, 128, 64), True)
            Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Text, New Vector2(CInt(Core.ScreenSize.Width / 2) - 190 + i * 192, 582), Microsoft.Xna.Framework.Color.Black)
        Next
        For i = 2 To 5
            Dim Text As String = ""
            Select Case i
                Case 2
                    Text = P3D.Legacy.Core.Localization.GetString("pack_menu_up")
                Case 3
                    Text = P3D.Legacy.Core.Localization.GetString("pack_menu_down")
                Case 4
                    If isSelectedEnabled = True Then
                        Text = P3D.Legacy.Core.Localization.GetString("pack_menu_toggle_off")
                    Else
                        Text = P3D.Legacy.Core.Localization.GetString("pack_menu_toggle_on")
                    End If
                Case 5
                    Text = P3D.Legacy.Core.Localization.GetString("pack_menu_information")
            End Select

            If i = packsMenuIndex(1) Then
                If i = 2 Or i = 3 Or PackNames.Count = 0 Then
                    If isSelectedEnabled = True Then
                        CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 48, 48, 48), "")
                    Else
                        CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(48, 0, 48, 48), "")
                    End If
                Else
                    CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 48, 48, 48), "")
                End If
            Else
                CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 0, 48, 48), "")
            End If

            Canvas.DrawImageBorder(CanvasTexture, 2, New Microsoft.Xna.Framework.Rectangle(CInt((Core.ScreenSize.Width / 2) + 280), ((i - 2) * 64) + 180, 160, 32), True)
            Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Text, New Vector2(CInt((Core.ScreenSize.Width / 2) + 280) + 15, ((i - 2) * 64) + 16 + 180), Microsoft.Xna.Framework.Color.Black)
        Next
    End Sub

    Private Sub UpdatePacksMenu()
        Dim currentIndex As Integer = packsMenuIndex(0)

        If Controls.Up(True, True, True) = True Then
            packsMenuIndex(0) -= 1
            If packsMenuIndex(0) - packsMenuIndex(2) < 0 Then
                packsMenuIndex(2) -= 1
            End If
        End If
        If Controls.Down(True, True, True) = True Then
            packsMenuIndex(0) += 1
            If packsMenuIndex(0) + packsMenuIndex(2) > 3 Then
                packsMenuIndex(2) += 1
            End If
        End If

        If Core.GameInstance.IsMouseVisible = True Then
            For i = 0 To 1
                If Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 208 + i * 192, 550, 128 + 32, 64 + 32)).Contains(MouseHandler.MousePosition) = True Then
                    Me.packsMenuIndex(1) = i

                    If MouseHandler.ButtonPressed(MouseHandler.MouseButtons.LeftButton) = True Then
                        Select Case packsMenuIndex(1)
                            Case 0
                                ButtonApplyPacks()
                            Case 1
                                Me.menuIndex = 0
                        End Select
                    End If
                End If
            Next

            For i = 2 To 5
                If Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(CInt((Core.ScreenSize.Width / 2) + 280), ((i - 2) * 64) + 180, 160 + 32, 32 + 32)).Contains(MouseHandler.MousePosition) = True Then
                    Me.packsMenuIndex(1) = i

                    If MouseHandler.ButtonPressed(MouseHandler.MouseButtons.LeftButton) = True Then
                        Select Case packsMenuIndex(1)
                            Case 2 'up
                                Me.ButtonUp()
                            Case 3 'down
                                Me.ButtonDown()
                            Case 4 'toggle
                                If PackNames.Count > 0 Then
                                    Me.ButtonToggle(PackNames(packsMenuIndex(0)))
                                End If
                            Case 5 'packinformation
                                Me.ButtonPackInformation()
                        End Select
                    End If
                End If
            Next
        End If

        For i = 0 To 3
            If Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 258, 180 + i * 50, 480, 48)).Contains(MouseHandler.MousePosition) = True Then
                If MouseHandler.ButtonPressed(MouseHandler.MouseButtons.LeftButton) = True Then
                    Me.packsMenuIndex(0) = i + packsMenuIndex(2)
                End If
            End If
        Next

        packsMenuIndex(0) = CInt(MathHelper.Clamp(packsMenuIndex(0), 0, PackNames.Count - 1))
        packsMenuIndex(2) = CInt(MathHelper.Clamp(packsMenuIndex(2), 0, PackNames.Count - 4))

        If Controls.Right(True, True, False) = True Then
            packsMenuIndex(1) += 1
        End If
        If Controls.Left(True, True, False) = True Then
            packsMenuIndex(1) -= 1
        End If

        packsMenuIndex(1) = CInt(MathHelper.Clamp(packsMenuIndex(1), 0, 5))

        If Controls.Accept(False, True) = True Then
            Select Case packsMenuIndex(1)
                Case 0
                    ButtonApplyPacks()
                Case 1
                    Me.menuIndex = 0
                Case 2
                    Me.ButtonUp()
                Case 3
                    Me.ButtonDown()
                Case 4
                    If PackNames.Count > 0 Then
                        Me.ButtonToggle(PackNames(packsMenuIndex(0)))
                    End If
                Case 5
                    Me.ButtonPackInformation()
            End Select
        End If

        If Controls.Dismiss() = True Then
            Me.menuIndex = 0
        End If
    End Sub

    Private Sub ButtonPackInformation()
        If PackNames.Count = 0 Then
            Exit Sub
        End If

        Me.menuIndex = 5

        Dim packName As String = PackNames(packsMenuIndex(0))
        PInfoSlpash = Nothing
        PInfoContent = ""

        ' TODO TRY
        'Try
            If System.IO.File.Exists(GameController.GamePath & "\ContentPacks\" & packName & "\splash.png") = True Then
                Using stream As System.IO.Stream = System.IO.File.Open(GameController.GamePath & "\ContentPacks\" & packName & "\splash.png", IO.FileMode.OpenOrCreate)
                    PInfoSlpash = Texture2D.FromStream(Core.GraphicsDevice, stream)
                End Using
            End If
        'Catch ex As Exception
        '    Logger.Log(Logger.LogTypes.ErrorMessage, "MainMenuScreen.vb/ButtonPackInformation: An error occurred trying to load the splash image at """ & GameController.GamePath & "\ContentPacks\" & packName & "\splash.png" & """. This could have been caused by an invalid file header. (Exception: " & ex.Message & ")")
        'End Try

        Dim contentPackPath As String = GameController.GamePath & "\ContentPacks\" & packName & "\"
        If System.IO.Directory.Exists(contentPackPath & "Songs") = True Then
            Dim hasWMA As Boolean = False
            Dim hasXNB As Boolean = False
            Dim hasMP3 As Boolean = False
            For Each file As String In System.IO.Directory.GetFiles(contentPackPath & "Songs")
                If System.IO.Path.GetExtension(file).ToLower() = ".xnb" Then
                    hasXNB = True
                End If
                If System.IO.Path.GetExtension(file).ToLower() = ".wma" Then
                    hasWMA = True
                End If
                If System.IO.Path.GetExtension(file).ToLower() = ".mp3" Then
                    hasMP3 = True
                End If
            Next

            If hasMP3 = True Or hasWMA = True And hasXNB = True Then
                PInfoContent = P3D.Legacy.Core.Localization.GetString("pack_menu_songs")
            End If
        End If
        If System.IO.Directory.Exists(contentPackPath & "Sounds") = True Then
            Dim hasWMA As Boolean = False
            Dim hasXNB As Boolean = False
            Dim hasWAV As Boolean = False
            For Each file As String In System.IO.Directory.GetFiles(contentPackPath & "Sounds")
                If System.IO.Path.GetExtension(file).ToLower() = ".xnb" Then
                    hasXNB = True
                End If
                If System.IO.Path.GetExtension(file).ToLower() = ".wma" Then
                    hasWMA = True
                End If
                If System.IO.Path.GetExtension(file).ToLower() = ".wav" Then
                    hasWAV = True
                End If
            Next

            If hasWAV = True Or hasWMA = True And hasXNB = True Then
                If PInfoContent <> "" Then
                    PInfoContent &= ", "
                End If

                PInfoContent &= P3D.Legacy.Core.Localization.GetString("pack_menu_sounds")
            End If
        End If

        Dim textureDirectories() As String = {"Textures", "GUI", "Items", "Pokemon", "SkyDomeResource"}
        For Each folder As String In textureDirectories
            If System.IO.Directory.Exists(contentPackPath & folder) = True Then
                Dim hasXNB As Boolean = False
                Dim hasPNG As Boolean = False
                For Each file As String In System.IO.Directory.GetFiles(contentPackPath & folder, "*.*", IO.SearchOption.AllDirectories)
                    If System.IO.Path.GetExtension(file).ToLower() = ".xnb" Then
                        hasXNB = True
                    End If
                    If System.IO.Path.GetExtension(file).ToLower() = ".png" Then
                        hasPNG = True
                    End If
                Next

                If hasXNB = True Or hasPNG = True Then
                    If PInfoContent <> "" Then
                        PInfoContent &= ", "
                    End If

                    PInfoContent &= P3D.Legacy.Core.Localization.GetString("pack_menu_textures")
                    Exit For
                End If
            End If
        Next

        Dim s() As String = ContentPackManager.GetContentPackInfo(packName)

        If s.Length > 0 Then
            PInfoVersion = s(0)
        End If
        If s.Length > 1 Then
            PInfoAuthor = s(1)
        End If
        If s.Length > 2 Then
            PInfoDescription = s(2)
        End If
        PInfoName = packName
    End Sub

    Private PInfoName As String = ""
    Private PInfoSlpash As Texture2D = Nothing
    Private PInfoVersion As String = ""
    Private PInfoAuthor As String = ""
    Private PInfoDescription As String = ""
    Private PInfoContent As String = ""

    Private Sub DrawPackInformationMenu()
        Dim isEnabled As Boolean = False
        Dim packName As String = PInfoName
        If EnabledPackNames.Contains(packName) = True Then
            isEnabled = True
        End If

        If Not PInfoSlpash Is Nothing Then
            Core.SpriteBatch.DrawInterface(PInfoSlpash, Core.ScreenSize, Microsoft.Xna.Framework.Color.White)
        End If

        Dim CanvasTexture As Texture2D = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 0, 48, 48), "")

        Canvas.DrawImageBorder(CanvasTexture, 2, New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 256, 160, 480, 64), True)
        Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, P3D.Legacy.Core.Localization.GetString("pack_menu_name") & ": " & PInfoName, New Vector2(CInt(Core.ScreenSize.Width / 2) - CInt(FontManager.InGameFont.MeasureString(P3D.Legacy.Core.Localization.GetString("pack_menu_name") & ": " & PInfoName).X / 2), 195), Microsoft.Xna.Framework.Color.Black)

        Canvas.DrawImageBorder(CanvasTexture, 2, New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 256, 288, 480, 224), True)
        Core.SpriteBatch.DrawInterfaceString(FontManager.MiniFont, P3D.Legacy.Core.Localization.GetString("pack_menu_version") & ": " & PInfoVersion & vbNewLine & P3D.Legacy.Core.Localization.GetString("pack_menu_by") & ": " & PInfoAuthor & vbNewLine & P3D.Legacy.Core.Localization.GetString("pack_menu_content") & ": " & PInfoContent & vbNewLine & P3D.Legacy.Core.Localization.GetString("pack_menu_description") & ": " & PInfoDescription.Replace("<br>", vbNewLine), New Vector2(CInt(Core.ScreenSize.Width / 2) - 220, 323), Microsoft.Xna.Framework.Color.Black)

        For i = 0 To 1
            If i = packInfoIndex Then
                CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 48, 48, 48), "")
            Else
                CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 0, 48, 48), "")
            End If

            Dim Text As String = P3D.Legacy.Core.Localization.GetString("pack_menu_back")

            Select Case i
                Case 0
                    If isEnabled = True Then
                        Text = P3D.Legacy.Core.Localization.GetString("pack_menu_toggle_off")
                    Else
                        Text = P3D.Legacy.Core.Localization.GetString("pack_menu_toggle_on")
                    End If
                Case 1
                    Text = P3D.Legacy.Core.Localization.GetString("pack_menu_back")
            End Select

            Canvas.DrawImageBorder(CanvasTexture, 2, New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 180 + (200 * i), 550, 128, 64), True)
            Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Text, New Vector2(CInt(Core.ScreenSize.Width / 2) - 160 + (200 * i), 582), Microsoft.Xna.Framework.Color.Black)
        Next
    End Sub

    Private Sub UpdatePackInformationMenu()
        Dim packName As String = PInfoName

        If Core.GameInstance.IsMouseVisible = True Then
            For i = 0 To 1
                If Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 180 + (200 * i), 550, 160, 96)).Contains(MouseHandler.MousePosition) = True Then
                    packInfoIndex = i

                    If MouseHandler.ButtonPressed(MouseHandler.MouseButtons.LeftButton) = True Then
                        Select Case i
                            Case 0
                                ButtonToggle(packName)
                            Case 1
                                menuIndex = 4
                        End Select
                    End If
                End If
            Next
        End If

        If Controls.Right(True, True, True, True) = True Then
            packInfoIndex += 1
        End If
        If Controls.Left(True, True, True, True) = True Then
            packInfoIndex -= 1
        End If

        Me.packInfoIndex = CInt(MathHelper.Clamp(Me.packInfoIndex, 0, 1))

        If Controls.Accept(False) = True Then
            Select Case packInfoIndex
                Case 0
                    ButtonToggle(packName)
                Case 1
                    menuIndex = 4
            End Select
        End If

        If Controls.Dismiss(False) = True Then
            menuIndex = 4
        End If
    End Sub

    Private Sub ButtonUp()
        If PackNames.Count > 0 Then
            If EnabledPackNames.Contains(PackNames(packsMenuIndex(0))) = True Then
                Dim idx As Integer = EnabledPackNames.IndexOf(PackNames(packsMenuIndex(0)))
                If idx > 0 Then
                    Dim tempString As String = EnabledPackNames(idx - 1)
                    EnabledPackNames(idx - 1) = EnabledPackNames(idx)
                    EnabledPackNames(idx) = tempString
                    GetPacks(True)
                End If
            End If
        End If
    End Sub

    Private Sub ButtonDown()
        If PackNames.Count > 0 Then
            If EnabledPackNames.Contains(PackNames(packsMenuIndex(0))) = True Then
                Dim idx As Integer = EnabledPackNames.IndexOf(PackNames(packsMenuIndex(0)))
                If idx < EnabledPackNames.Count - 1 Then
                    Dim tempString As String = EnabledPackNames(idx + 1)
                    EnabledPackNames(idx + 1) = EnabledPackNames(idx)
                    EnabledPackNames(idx) = tempString
                    GetPacks(True)
                End If
            End If
        End If
    End Sub

    Private Sub ButtonToggle(ByVal PackName As String)
        If PackNames.Count > 0 Then
            If EnabledPackNames.Contains(PackName) = True Then
                EnabledPackNames.Remove(PackName)
                GetPacks(True)
            Else
                EnabledPackNames.Add(PackName)
                GetPacks(True)
            End If
        Else
            GetPacks(True)
        End If
    End Sub

    Private Sub ButtonApplyPacks()
        If PackNames.Count > 0 Then
            Core.GameOptions.ContentPackNames = EnabledPackNames.ToArray()
            Options.SaveOptions(Core.GameOptions)
            MediaPlayer.Stop()
            ContentPackManager.Clear()
            For Each s As String In Core.GameOptions.ContentPackNames
                ContentPackManager.Load(GameController.GamePath & "\ContentPacks\" & s & "\exceptions.dat")
            Next
            MusicManager.PlayNoMusic()
            Core.OffsetMaps.Clear()
            Core.SetScreen(New MainMenuScreen)
        End If
        Me.menuIndex = 0
    End Sub

#End Region

#Region "DeleteMenu"

    Private Sub DrawDeleteMenu()
        Dim CanvasTexture As Texture2D = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 0, 48, 48), "")

        Canvas.DrawImageBorder(CanvasTexture, 2, New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2 - 352), 172, 704, 96), Microsoft.Xna.Framework.Color.White, True)

        Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, P3D.Legacy.Core.Localization.GetString("delete_menu_delete_confirm"), New Vector2(CInt(Core.ScreenSize.Width / 2) - CInt(FontManager.InGameFont.MeasureString(P3D.Legacy.Core.Localization.GetString("delete_menu_delete_confirm")).X / 2), 200), Microsoft.Xna.Framework.Color.Black)

        Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, """" & SaveNames(loadMenuIndex(0)) & """ ?", New Vector2(CInt(Core.ScreenSize.Width / 2) - CInt(FontManager.InGameFont.MeasureString("""" & SaveNames(loadMenuIndex(0)) & """ ?").X / 2), 240), Microsoft.Xna.Framework.Color.Black)

        For i = 0 To 1
            Dim Text As String = P3D.Legacy.Core.Localization.GetString("delete_menu_delete")

            If i = 1 Then
                Text = P3D.Legacy.Core.Localization.GetString("delete_menu_cancel")
            End If

            If i = deleteIndex Then
                CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 48, 48, 48), "")
            Else
                CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 0, 48, 48), "")
            End If

            Canvas.DrawImageBorder(CanvasTexture, 2, New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 182 + i * 192, 370, 128, 64), True)
            Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Text, New Vector2(CInt(Core.ScreenSize.Width / 2) - 164 + i * 192, 402), Microsoft.Xna.Framework.Color.Black)
        Next
    End Sub

    Private Sub UpdateDeleteMenu()
        If Controls.Right(True, True, False) = True Then
            deleteIndex = 1
        End If
        If Controls.Left(True, True, False) = True Then
            deleteIndex = 0
        End If

        If Core.GameInstance.IsMouseVisible = True Then
            For i = 0 To 1
                If Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 182 + i * 192, 370, 128 + 32, 64 + 32)).Contains(MouseHandler.MousePosition) = True Then
                    deleteIndex = i

                    If MouseHandler.ButtonPressed(MouseHandler.MouseButtons.LeftButton) = True Then
                        Select Case deleteIndex
                            Case 0
                                Delete()
                            Case 1
                                menuIndex = 1
                        End Select
                    End If
                End If
            Next
        End If

        If Controls.Accept(False, True) = True Then
            Select Case deleteIndex
                Case 0
                    Delete()
                Case 1
                    menuIndex = 1
            End Select
        End If
    End Sub

    Private Sub Delete()
        System.IO.Directory.Delete(Saves(loadMenuIndex(0)), True)

        Dim deleteAutosave As Boolean = False
        For Each f As String In System.IO.Directory.GetDirectories(GameController.GamePath & "\Save\")

            Dim userFolder = StorageInfo.SaveFolder.GetUserSaveFolder(f.TrimEnd("/").TrimEnd("\"))
            Dim Data() As String = userFolder.PlayerFile.ReadAllLines()
            For Each Line As String In Data
                If Line.StartsWith("AutoSave|") = True Then
                    Dim autosaveName As String = Line.GetSplit(1, "|")
                    If autosaveName = Saves(loadMenuIndex(0)).Remove(0, Saves(loadMenuIndex(0)).LastIndexOf("\") + 1) Then
                        deleteAutosave = True
                    End If
                End If
            Next
        Next
        If deleteAutosave = True Then
            System.IO.Directory.Delete(GameController.GamePath & "\Save\autosave", True)
        End If

        tempLoadDisplay = ""
        GetSaves()
        loadMenuIndex(0) = 0
        loadMenuIndex(1) = 0
        loadMenuIndex(2) = 0
        If Saves.Count = 0 Then
            menuIndex = 0
        Else
            menuIndex = 1
        End If
    End Sub

#End Region

#Region "NewGameMenu"

    Public Sub NewGameButton()
        If Core.GameOptions.StartedOfflineGame = True Then
            If GameModeManager.GameModeCount < 2 Then
                GameModeManager.SetGameModePointer("Kolben")
                Core.SetScreen(New TransitionScreen(Me, New NewGameScreen(), Microsoft.Xna.Framework.Color.Black, False))
            Else
                GetGameModes()
                GameModeSplash = Nothing
                Me.menuIndex = 6
            End If
        Else
            Core.GameOptions.StartedOfflineGame = True
            Options.SaveOptions(Core.GameOptions)
            Core.SetScreen(New OfflineGameWarningScreen(Me))
        End If
    End Sub

    Private tempGameModesDisplay As String = ""
    Private GameModeSplash As Texture2D = Nothing

    Private Sub DrawNewGameMenu()
        If Not GameModeSplash Is Nothing Then
            Core.SpriteBatch.DrawInterface(GameModeSplash, Core.ScreenSize, Microsoft.Xna.Framework.Color.White)
        End If

        Dim CanvasTexture As Texture2D

        For i = 0 To 3
            Dim c As Microsoft.Xna.Framework.Color = Microsoft.Xna.Framework.Color.White
            If i + gameModeMenuIndex(2) = gameModeMenuIndex(0) Then
                c = New Microsoft.Xna.Framework.Color(101, 142, 255)
            End If

            Canvas.DrawRectangle(New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 258, 180 + i * 50, 480, 48), c, True)
        Next

        Canvas.DrawScrollBar(New Vector2(CInt(Core.ScreenSize.Width / 2) + 250, 180), ModeNames.Count, 4, gameModeMenuIndex(2), New Size(4, 200), False, New Microsoft.Xna.Framework.Color(190, 190, 190), New Microsoft.Xna.Framework.Color(63, 63, 63), True)

        Dim x As Integer = ModeNames.Count - 1
        x = CInt(MathHelper.Clamp(x, 0, 3))

        For i = 0 To x
            Dim Name As String = ModeNames(i + gameModeMenuIndex(2))

            If i + gameModeMenuIndex(2) = gameModeMenuIndex(0) Then
                Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Name, New Vector2(CInt(Core.ScreenSize.Width / 2) - 245, 191 + i * 50), Microsoft.Xna.Framework.Color.Black)
                Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Name, New Vector2(CInt(Core.ScreenSize.Width / 2) - 248, 188 + i * 50), Microsoft.Xna.Framework.Color.White)
            Else
                Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Name, New Vector2(CInt(Core.ScreenSize.Width / 2) - 248, 188 + i * 50), Microsoft.Xna.Framework.Color.Black)
            End If
        Next

        CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 0, 48, 48), "")
        Canvas.DrawImageBorder(CanvasTexture, 2, New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 272, 388, 512, 128), True)

        If tempGameModesDisplay = "" Then
            Dim GameMode As GameMode = GameModeManager.GetGameMode(ModeNames(gameModeMenuIndex(0)))

            Dim dispName As String = GameMode.Name
            Dim dispDescription As String = GameMode.Description
            Dim dispVersion As String = GameMode.Version
            Dim dispAuthor As String = GameMode.Author
            Dim dispContentPath As String = GameMode.ContentFolder.Path

            Me.tempGameModesDisplay = P3D.Legacy.Core.Localization.GetString("gamemode_menu_name") & ": " & dispName & vbNewLine &
                P3D.Legacy.Core.Localization.GetString("gamemode_menu_version") & ": " & dispVersion & vbNewLine &
                P3D.Legacy.Core.Localization.GetString("gamemode_menu_author") & ": " & dispAuthor & vbNewLine &
                P3D.Legacy.Core.Localization.GetString("gamemode_menu_contentpath") & ": " & dispContentPath & vbNewLine &
                P3D.Legacy.Core.Localization.GetString("gamemode_menu_description") & ": " & dispDescription
        End If

        Core.SpriteBatch.DrawInterfaceString(FontManager.MiniFont, tempGameModesDisplay, New Vector2(CInt(Core.ScreenSize.Width / 2) - 252, 416), Microsoft.Xna.Framework.Color.Black)

        For i = 0 To 1
            If i = gameModeMenuIndex(1) Then
                CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 48, 48, 48), "")
            Else
                CanvasTexture = TextureManager.GetTexture("GUI\Menus\Menu", New Microsoft.Xna.Framework.Rectangle(0, 0, 48, 48), "")
            End If

            Dim Text As String = P3D.Legacy.Core.Localization.GetString("gamemode_menu_back")

            Select Case i
                Case 0
                    Text = P3D.Legacy.Core.Localization.GetString("gamemode_menu_create")
                Case 1
                    Text = P3D.Legacy.Core.Localization.GetString("gamemode_menu_back")
            End Select

            Canvas.DrawImageBorder(CanvasTexture, 2, New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 180 + (200 * i), 550, 128, 64), True)
            Core.SpriteBatch.DrawInterfaceString(FontManager.InGameFont, Text, New Vector2(CInt(Core.ScreenSize.Width / 2) - 160 + (200 * i), 582), Microsoft.Xna.Framework.Color.Black)
        Next
    End Sub

    Private Sub UpdateNewGameMenu()
        If Controls.Up(True, True, True) = True Then
            gameModeMenuIndex(0) -= 1
            If gameModeMenuIndex(0) - gameModeMenuIndex(2) < 0 Then
                gameModeMenuIndex(2) -= 1
            End If
            tempGameModesDisplay = ""
            GameModeSplash = Nothing
        End If
        If Controls.Down(True, True, True) = True Then
            gameModeMenuIndex(0) += 1
            If gameModeMenuIndex(0) + gameModeMenuIndex(2) > 3 Then
                gameModeMenuIndex(2) += 1
            End If
            tempGameModesDisplay = ""
            GameModeSplash = Nothing
        End If

        If Core.GameInstance.IsMouseVisible = True Then
            For i = 0 To 1
                If Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 180 + (200 * i), 550, 160, 96)).Contains(MouseHandler.MousePosition) = True Then
                    Me.gameModeMenuIndex(1) = i

                    If MouseHandler.ButtonPressed(MouseHandler.MouseButtons.LeftButton) = True Then
                        Select Case gameModeMenuIndex(1)
                            Case 0
                                AcceptGameMode()
                            Case 1
                                Me.menuIndex = 0

                                tempGameModesDisplay = ""
                        End Select
                    End If
                End If
            Next
        End If

        For i = 0 To 3
            If Core.ScaleScreenRec(New Microsoft.Xna.Framework.Rectangle(CInt(Core.ScreenSize.Width / 2) - 258, 180 + i * 50, 480, 48)).Contains(MouseHandler.MousePosition) = True Then
                If MouseHandler.ButtonPressed(MouseHandler.MouseButtons.LeftButton) = True Then
                    Me.gameModeMenuIndex(0) = i + gameModeMenuIndex(2)
                    tempGameModesDisplay = ""
                    GameModeSplash = Nothing
                End If
            End If
        Next

        gameModeMenuIndex(0) = CInt(MathHelper.Clamp(gameModeMenuIndex(0), 0, ModeNames.Count - 1))
        gameModeMenuIndex(2) = CInt(MathHelper.Clamp(gameModeMenuIndex(2), 0, ModeNames.Count - 4))

        If Controls.Right(True, True, False) = True Then
            gameModeMenuIndex(1) += 1
        End If
        If Controls.Left(True, True, False) = True Then
            gameModeMenuIndex(1) -= 1
        End If

        gameModeMenuIndex(1) = CInt(MathHelper.Clamp(gameModeMenuIndex(1), 0, 1))

        If Controls.Accept(False, True) = True Then
            Select Case gameModeMenuIndex(1)
                Case 0
                    AcceptGameMode()
                Case 1
                    Me.menuIndex = 0

                    tempGameModesDisplay = ""
            End Select
        End If

        If GameModeSplash Is Nothing Then
            ' TODO TRY
            'Try
                Dim fileName As String = GameController.GamePath & "\GameModes\" & ModeNames(gameModeMenuIndex(0)) & "\GameMode.png"
                If System.IO.File.Exists(fileName) = True Then
                    Using stream As System.IO.Stream = System.IO.File.Open(fileName, IO.FileMode.OpenOrCreate)
                        GameModeSplash = Texture2D.FromStream(Core.GraphicsDevice, stream)
                    End Using
                End If
            'Catch ex As Exception
            '    Logger.Log(Logger.LogTypes.ErrorMessage, "MainMenuScreen.vb/UpdateNewGameMenu: An error occurred trying to load the splash image at """ & GameController.GamePath & "\GameModes\" & ModeNames(gameModeMenuIndex(0)) & "\GameMode.png"". This could have been caused by an invalid file header. (Exception: " & ex.Message & ")")
            'End Try
        End If

        If Controls.Dismiss() = True Then
            Me.menuIndex = 0
        End If
    End Sub

    Private Sub AcceptGameMode()
        GameModeManager.SetGameModePointer(ModeNames(gameModeMenuIndex(0)))
        Core.SetScreen(New TransitionScreen(Me, New NewGameScreen(), Microsoft.Xna.Framework.Color.Black, False))
    End Sub

#End Region

    Public Overrides Sub ChangeTo()
        Core.Player.Unload()
        Core.Player.Skin = "Hilbert"
        TextBox.Hide()
        TextBox.CanProceed = True
        OverworldScreen.FadeValue = 0

        MusicManager.PlayMusic("title", True, 0.0F, 0.0F)
    End Sub

End Class