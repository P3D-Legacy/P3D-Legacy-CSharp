Imports System.Drawing
Imports System.Globalization
Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Data
Imports P3D.Legacy.Core.GameModes
Imports P3D.Legacy.Core.Input
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.Resources.Sound
Imports P3D.Legacy.Core.Screens
Imports P3D.Legacy.Core.Screens.GUI
Imports P3D.Legacy.Core.Storage
Imports PCLExt.FileStorage

Public Class NewGameScreen
    Inherits BaseNewGameScreen

    Dim startSkins() As String = {"Ethan", "Lyra", "Nate", "Rosa", "Hilbert", "Hilda"}
    Dim skinNames() As String = {"Ethan", "Lyra", "Nate", "Rosa", "Hilbert", "Hilda"}
    Dim backColors() As Microsoft.Xna.Framework.Color = {New Microsoft.Xna.Framework.Color(248, 176, 32), New Microsoft.Xna.Framework.Color(248, 216, 88), New Microsoft.Xna.Framework.Color(56, 88, 200), New Microsoft.Xna.Framework.Color(216, 96, 112), New Microsoft.Xna.Framework.Color(56, 88, 152), New Microsoft.Xna.Framework.Color(239, 90, 156)}

    Public Index As Integer = 0
    Dim pokeIndex As Integer = 0

    Dim ProfAlpha As Integer = 0
    Dim OtherAlpha As Integer = 0

    Dim Name As String = ""
    Dim SkinIndex As Integer = 0
    Dim skinTexture As Texture2D
    Dim enterCorrectName As Boolean = False
    Dim nameMessage As String = "This name is too short"

    Dim mainTexture As Texture2D
    Dim pokeTexture As Texture2D

    Dim ballPosition As Vector2
    Dim ballIndex As Vector2 = New Vector2(0, 0)
    Dim ballAnimationDelay As Single = 0.2F
    Dim ballVelocity As Single = -4.0F
    Dim pokePosition As Vector2
    Dim pokeID As Integer = 0

    Dim CurrentText As String = ""

    Dim currentBackColor As Microsoft.Xna.Framework.Color = New Microsoft.Xna.Framework.Color(59, 123, 165)
    Dim normalColor As Microsoft.Xna.Framework.Color = New Microsoft.Xna.Framework.Color(59, 123, 165)

    Dim pokemonRange() As Integer = {1, 252}
    Dim introMusic As String = "welcome"
    Dim startMap As String = "yourroom.dat"
    Dim startPosition As Vector3 = New Vector3(1, 0.1F, 3)
    Dim startLocation As String = "Your Room"
    Dim startYaw As Single = MathHelper.PiOver2

    Dim Dialogues As New List(Of String)

    Public Sub New()
        For Each s As String In Core.GameOptions.ContentPackNames
            ContentPackManager.Load(GameController.GamePath & "\ContentPacks\" & s & "\exceptions.dat")
        Next

        GameModeAttackLoader.Load()

        Localization.ReloadGameModeTokens()

        If GameModeManager.ActiveGameMode.IsDefaultGamemode = False Then
            MusicManager.LoadMusic(True)
            SoundManager.LoadSounds(True)
        End If
        SmashRock.Load()
        Badge.Load()
        Pokedex.Load()
        BattleSystem.BattleScreen.ResetVars()
        LevelLoader.ClearTempStructures()
        PokemonForms.Initialize()

        Me.Identification = Identifications.NewGameScreen
        Me.CanChat = False
        mainTexture = TextureManager.GetTexture("GUI\Intro")

        LoadIntroValues()

        Dim p As Pokemon = Pokemon.GetPokemonByID(Core.Random.Next(pokemonRange(0), pokemonRange(1)))
        p.Generate(1, True)
        pokeID = p.Number
        pokeTexture = p.GetTexture(True)

        CurrentText = ""

        TextBox.Showing = False
        Me.Index = 0
        TextBox.reDelay = 0
        skinTexture = TextureManager.GetTexture(TextureManager.GetTexture("Textures\NPC\" & startSkins(SkinIndex)), New Microsoft.Xna.Framework.Rectangle(0, 64, 32, 32))

        MusicManager.PlayMusic("nomusic")
    End Sub

    Private Sub LoadIntroValues()
        Dim GameMode As GameMode = GameModeManager.ActiveGameMode

        Me.skinNames = GameMode.SkinNames.ToArray()
        Me.startSkins = GameMode.SkinFiles.ToArray()
        Me.backColors = GameMode.SkinColors.ToArray()

        Me.pokemonRange = GameMode.PokemonRange
        Me.introMusic = GameMode.IntroMusic
        Me.startMap = GameMode.StartMap
        Me.startPosition = GameMode.StartPosition
        Me.startLocation = GameMode.StartLocationName
        Me.startYaw = GameMode.StartRotation
        Me.normalColor = GameMode.StartColor
        Me.currentBackColor = GameMode.StartColor

        If GameMode.StartDialogue <> "" Then
            If GameMode.StartDialogue.CountSplits("|") >= 3 Then
                Dim Splits() As String = GameMode.StartDialogue.Split(CChar("|"))
                Me.Dialogues.AddRange(Splits)
            End If
        End If
        If Me.Dialogues.Count < 3 Then
            Me.Dialogues.Clear()
            Me.Dialogues.AddRange({Localization.GetString("new_game_oak_1"), Localization.GetString("new_game_oak_2"), Localization.GetString("new_game_oak_3")})
        End If
    End Sub

    Public Overrides Sub Update()
        If Index = 5 Then
            Core.GameInstance.IsMouseVisible = True
        Else
            Core.GameInstance.IsMouseVisible = False
        End If

        If ProfAlpha < 255 And Index = 0 Then
            ProfAlpha += 2
            If ProfAlpha >= 255 Then
                MusicManager.PlayMusic(Me.introMusic, True, 0.0F, 0.0F)
            End If
        ElseIf ProfAlpha >= 255 Or Index > 0 Then
            TextBox.Update()

            If TextBox.Showing = False Then
                Select Case Index
                    Case 1
                        UpdatePokemon()
                    Case 3
                        UpdateTransition(False)
                    Case 5
                        UpdateTextbox()
                    Case 7
                        UpdateTransition(True)
                    Case 9
                        If ProfAlpha > 0 Then
                            ProfAlpha -= 2
                        End If
                        If ProfAlpha <= 0 Then
                            CreateGame()
                        End If
                    Case 4
                        UpdateChooseSkin()
                    Case Else
                        ShowText()
                End Select
            End If
        End If

        Dim AimColor As Microsoft.Xna.Framework.Color = normalColor

        If Me.Index = 4 Then
            AimColor = backColors(SkinIndex)
        End If

        Dim diffR As Byte = 5
        If (CInt(currentBackColor.R) - CInt(AimColor.R)).ToPositive() < 5 Then
            diffR = 1
        End If
        Dim diffG As Byte = 5
        If (CInt(currentBackColor.G) - CInt(AimColor.G)).ToPositive() < 5 Then
            diffG = 1
        End If
        Dim diffB As Byte = 5
        If (CInt(currentBackColor.B) - CInt(AimColor.B)).ToPositive() < 5 Then
            diffB = 1
        End If
        If currentBackColor.R < AimColor.R Then
            currentBackColor.R += diffR
        ElseIf currentBackColor.R > AimColor.R Then
            currentBackColor.R -= diffR
        End If
        If currentBackColor.G < AimColor.G Then
            currentBackColor.G += diffG
        ElseIf currentBackColor.G > AimColor.G Then
            currentBackColor.G -= diffG
        End If
        If currentBackColor.B < AimColor.B Then
            currentBackColor.B += diffB
        ElseIf currentBackColor.B > AimColor.B Then
            currentBackColor.B -= diffB
        End If
    End Sub

    Private Sub ShowText()
        Dim Text As String = ""

        Select Case Index
            Case 0
                Text = Dialogues(0)
            Case 2
                Text = Dialogues(1)
            Case 6
                Text = NameReaction(Name)
            Case 8
                Text = Name & Dialogues(2)
        End Select

        TextBox.reDelay = 0
        TextBox.Show(Text, {})

        Index += 1
    End Sub


    Public Overrides Sub Draw()
        Canvas.DrawRectangle(New Microsoft.Xna.Framework.Rectangle(0, 0, Core.WindowSize.Width, Core.WindowSize.Height), currentBackColor)

        TextBox.Draw()

        Core.SpriteBatch.Draw(mainTexture, New Microsoft.Xna.Framework.Rectangle(CInt(Core.WindowSize.Width / 2) - 62, CInt(Core.WindowSize.Height / 2) - 218, 130, 256), New Microsoft.Xna.Framework.Rectangle(0, 0, 62, 128), New Microsoft.Xna.Framework.Color(255, 255, 255, ProfAlpha))
        Core.SpriteBatch.Draw(skinTexture, New Microsoft.Xna.Framework.Rectangle(CInt(Core.WindowSize.Width / 2) - 128, CInt(Core.WindowSize.Height / 2) - 218, 256, 256), New Microsoft.Xna.Framework.Color(255, 255, 255, OtherAlpha))

        Select Case pokeIndex
            Case 1
                Core.SpriteBatch.Draw(mainTexture, New Microsoft.Xna.Framework.Rectangle(CInt(ballPosition.X), CInt(ballPosition.Y), 22, 22), New Microsoft.Xna.Framework.Rectangle(62 + CInt(ballIndex.X * 22), 48 + CInt(ballIndex.Y * 22), 22, 22), Microsoft.Xna.Framework.Color.White)
            Case 2
                Core.SpriteBatch.Draw(pokeTexture, New Microsoft.Xna.Framework.Rectangle(CInt(pokePosition.X) - 100, CInt(pokePosition.Y) - 160, 256, 256), Microsoft.Xna.Framework.Color.White)
            Case 3
                If Index < 6 Then
                    Core.SpriteBatch.Draw(pokeTexture, New Microsoft.Xna.Framework.Rectangle(CInt(Core.WindowSize.Width / 2) - 300, CInt(Core.WindowSize.Height / 2) - 130, 256, 256), New Microsoft.Xna.Framework.Color(255, 255, 255, ProfAlpha))
                End If
        End Select

        Select Case Index
            Case 5
                Core.SpriteBatch.DrawString(FontManager.MiniFont, Localization.GetString("new_game_your_name") & ":", New Vector2(TextboxPosition.X, TextboxPosition.Y - 24), Microsoft.Xna.Framework.Color.White)
                DrawTextBox()

                If enterCorrectName = True Then
                    Core.SpriteBatch.DrawString(FontManager.MiniFont, Localization.GetString("new_game_name_too_short"), New Vector2(TextboxPosition.X, TextboxPosition.Y + 30), Microsoft.Xna.Framework.Color.DarkRed)
                End If
            Case 4
                Canvas.DrawRectangle(New Microsoft.Xna.Framework.Rectangle(CInt(TextboxPosition.X - 5), CInt(TextboxPosition.Y - 24), 138, 42), New Microsoft.Xna.Framework.Color(0, 0, 0, 80))

                Core.SpriteBatch.DrawString(FontManager.MiniFont, Localization.GetString("new_game_choose_skin") & ":" & vbNewLine & skinNames(SkinIndex), New Vector2(TextboxPosition.X, TextboxPosition.Y - 24), Microsoft.Xna.Framework.Color.White)

                Canvas.DrawScrollBar(New Vector2(TextboxPosition.X, TextboxPosition.Y + 48), startSkins.Count, 1, SkinIndex, New Size(128, 4), True, TextureManager.GetTexture(TextureManager.GetTexture("GUI\Menus\Menu"), New Microsoft.Xna.Framework.Rectangle(112, 12, 1, 1)), TextureManager.GetTexture(TextureManager.GetTexture("GUI\Menus\Menu"), New Microsoft.Xna.Framework.Rectangle(113, 12, 1, 1)))
        End Select
    End Sub

    Private Sub DrawTextBox()
        Canvas.DrawRectangle(New Microsoft.Xna.Framework.Rectangle(CInt(Core.WindowSize.Width / 2) - 74, CInt(Core.WindowSize.Height / 2) + 124, 148, 32), New Microsoft.Xna.Framework.Color(101, 142, 255))
        Canvas.DrawRectangle(New Microsoft.Xna.Framework.Rectangle(CInt(Core.WindowSize.Width / 2) - 70, CInt(Core.WindowSize.Height / 2) + 128, 140, 24), Microsoft.Xna.Framework.Color.White)

        Dim t As String = Me.CurrentText
        If t.Length < 14 Then
            t &= "_"
        End If
        Core.SpriteBatch.DrawString(FontManager.MiniFont, t, TextboxPosition(), Microsoft.Xna.Framework.Color.Black)

        Dim l As New Dictionary(Of Buttons, String)
        l.Add(Buttons.A, "Accept")
        l.Add(Buttons.X, "Edit name")
        Me.DrawGamePadControls(l)
    End Sub

    Private Function TextboxPosition() As Vector2
        Return New Vector2(CInt(Core.WindowSize.Width / 2) - 70, CInt(Core.WindowSize.Height / 2) + 128)
    End Function

    Private Sub UpdatePokemon()
        Select Case pokeIndex
            Case 0
                ballPosition = New Vector2(CInt(Core.WindowSize.Width / 2) - 40, CInt(Core.WindowSize.Height / 2) - 110)
                pokePosition = New Vector2(CInt(Core.WindowSize.Width / 2) - 200, CInt(Core.WindowSize.Height / 2) - 110)
                pokeIndex = 1
                AnimateBall()
            Case 1
                If ballPosition.X > CInt(Core.WindowSize.Width / 2) - 200 Then
                    ballPosition.X -= 3
                    ballPosition.Y += ballVelocity
                    ballVelocity += 0.2F
                Else
                    pokeIndex = 2
                End If
                AnimateBall()
            Case 2
                If pokePosition.Y < CInt(Core.WindowSize.Height / 2) + 38 Then
                    pokePosition.Y += 5
                Else
                    Dim p As Pokemon = Pokemon.GetPokemonByID(pokeID)
                    p.PlayCry()
                    pokeIndex = 3
                    Index = 2
                End If
        End Select
    End Sub

    Private Sub AnimateBall()
        If ballAnimationDelay <= 0.0F Then
            ballIndex.X += 1
            If ballIndex.X = 2 And ballIndex.Y = 2 Then
                ballIndex = New Vector2(0, 0)
            End If
            If ballIndex.X = 2 Then
                ballIndex.X = 0
                ballIndex.Y += 1
            End If
            ballAnimationDelay = 0.2F
        Else
            ballAnimationDelay -= 0.1F
        End If
    End Sub

    Private Sub UpdateTransition(ByVal ToProf As Boolean)
        If ToProf = True Then
            If OtherAlpha > 0 Then
                OtherAlpha -= 5
            End If
            If OtherAlpha <= 0 Then
                If ProfAlpha < 255 Then
                    ProfAlpha += 5

                    If ProfAlpha >= 255 Then
                        Index += 1
                    End If
                End If
            End If
        Else
            If ProfAlpha > 0 Then
                ProfAlpha -= 5
            End If
            If ProfAlpha <= 0 Then
                If OtherAlpha < 255 Then
                    OtherAlpha += 5

                    If OtherAlpha >= 255 Then
                        Index += 1
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub UpdateChooseSkin()
        Dim sIndex As Integer = SkinIndex
        If Controls.Right(True, True, True, True) = True Then
            SkinIndex += 1
        End If
        If Controls.Left(True, True, True, True) = True Then
            SkinIndex -= 1
        End If

        SkinIndex = CInt(MathHelper.Clamp(SkinIndex, 0, startSkins.Count - 1))

        If sIndex <> SkinIndex Then
            skinTexture = TextureManager.GetTexture(TextureManager.GetTexture("Textures\NPC\" & startSkins(SkinIndex)), New Microsoft.Xna.Framework.Rectangle(0, 64, 32, 32))
        End If

        If Controls.Accept() = True Then
            Index += 1
        End If
    End Sub

    Private Sub UpdateTextbox()
        CanMuteMusic = False

        If ControllerHandler.ButtonPressed(Buttons.X) = True Then
            Core.SetScreen(New InputScreen(Core.CurrentScreen, "Player", InputScreen.InputModes.Name, Me.CurrentText, 14, {TextureManager.GetTexture(TextureManager.GetTexture("Textures\NPC\" & startSkins(SkinIndex)), New Microsoft.Xna.Framework.Rectangle(0, 64, 32, 32))}.ToList(), AddressOf Me.ConfirmInput))
        Else
            KeyBindings.GetNameInput(Me.CurrentText, 14)

            Me.CurrentText = CorrectChars(Me.CurrentText)

            If Controls.Accept(False, True, True) = True And KeyBoardHandler.KeyPressed(Keys.Space) = False Then
                If CurrentText.Length > 2 And String.IsNullOrWhiteSpace(CurrentText) = False Then
                    Me.Name = CurrentText
                    Index += 1
                    CanMuteMusic = True
                Else
                    enterCorrectName = True
                End If
            End If
        End If
    End Sub

    Private Sub ConfirmInput(ByVal input As String)
        Me.CurrentText = CorrectChars(input)
    End Sub

    Private Function CorrectChars(ByVal t As String) As String
        Dim exclude() As String = {"\", "/", ":", "*", "?", """", "<", ">", "|", ",", "."}
        Dim s As String = ""
        For Each c As Char In t
            If exclude.Contains(c.ToString(NumberFormatInfo.InvariantInfo)) = True Then
                c = CChar(" ")
            End If
            s &= c.ToString(NumberFormatInfo.InvariantInfo)
        Next
        Return s
    End Function

    Private Async Sub CreateGame()
        Dim folderPath As String = Name
        Dim folderPrefix As Integer = 0

        If folderPath.ToLower() = "autosave" Then
            folderPath = "autosave0"
        End If

        Dim savePath As String = GameController.GamePath & "\Save\"

        While System.IO.Directory.Exists(savePath & folderPath) = True
            If folderPath <> Name Then
                folderPath = folderPath.Remove(folderPath.Length - folderPrefix.ToString(NumberFormatInfo.InvariantInfo).Length, folderPrefix.ToString(NumberFormatInfo.InvariantInfo).Length)
            End If

            folderPath &= folderPrefix

            folderPrefix += 1
        End While

        Dim playerFolder = Await StorageInfo.SaveFolder.GetUserSaveFolder(folderPath)
        Await playerFolder.PlayerFile.WriteAllTextAsync(GetPlayerData())
        Await playerFolder.BerriesFile.WriteAllTextAsync(GetBerryData())
        Await playerFolder.OptionsFile.WriteAllTextAsync(GetOptionsData())

        Core.Player.IsGamejoltSave = False
        Core.Player.LoadGame(folderPath)
        Core.SetScreen(New TransitionScreen(Me, New OverworldScreen(), Microsoft.Xna.Framework.Color.Black, False, 5))
    End Sub

    Private Function GetPlayerData() As String
        Dim ot As String = Core.Random.Next(0, 65256).ToString(NumberFormatInfo.InvariantInfo)
        While ot.Length < 5
            ot = "0" & ot
        End While

        Dim s As String = "Name|" & Name & vbNewLine &
            "Position|" & Me.startPosition.X.ToString(NumberFormatInfo.InvariantInfo) & "," & Me.startPosition.Y.ToString(NumberFormatInfo.InvariantInfo) & "," & Me.startPosition.Z.ToString(NumberFormatInfo.InvariantInfo) & vbNewLine &
            "MapFile|" & Me.startMap & vbNewLine &
            "Rotation|" & Me.startYaw.ToString(NumberFormatInfo.InvariantInfo) & vbNewLine &
            "RivalName|???" & vbNewLine &
            "Money|3000" & vbNewLine &
            "Badges|0" & vbNewLine &
            "Gender|Male" & vbNewLine &
            "PlayTime|0,0,0" & vbNewLine &
            "OT|" & ot & vbNewLine &
            "Points|0" & vbNewLine &
            "hasPokedex|0" & vbNewLine &
            "hasPokegear|0" & vbNewLine &
            "freeCamera|1" & vbNewLine &
            "thirdPerson|0" & vbNewLine &
            "skin|" & startSkins(SkinIndex) & vbNewLine &
            "location|" & Me.startLocation & vbNewLine &
            "battleAnimations|2" & vbNewLine &
            "BoxAmount|5" & vbNewLine &
            "LastRestPlace|yourroom.dat" & vbNewLine &
            "LastRestPlacePosition|1,0.1,3" & vbNewLine &
            "DiagonalMovement|0" & vbNewLine &
            "RepelSteps|0" & vbNewLine &
            "LastSavePlace|yourroom.dat" & vbNewLine &
            "LastSavePlacePosition|1,0.1,3" & vbNewLine &
            "Difficulty|" & GameModeManager.GetActiveGameRuleValueOrDefault("Difficulty", 0).ToString() & vbNewLine &
            "BattleStyle|0" & vbNewLine &
            "saveCreated|" & GameController.GAMEDEVELOPMENTSTAGE & " " & GameController.GAMEVERSION & vbNewLine &
            "LastPokemonPosition|999,999,999" & vbNewLine &
            "DaycareSteps|0" & vbNewLine &
            "GameMode|" & GameModeManager.ActiveGameMode.Name & vbNewLine &
            "PokeFiles|" & vbNewLine &
            "VisitedMaps|yourroom.dat" & vbNewLine &
            "TempSurfSkin|Hilbert" & vbNewLine &
            "Surfing|0" & vbNewLine &
            "ShowModels|1" & vbNewLine &
            "GTSStars|4" & vbNewLine &
            "SandBoxMode|0"

        Return s
    End Function

    Public Shared Function GetOptionsData() As String
        Dim s As String = "FOV|50" & vbNewLine &
            "TextSpeed|2" & vbNewLine &
            "MouseSpeed|12"

        Return s
    End Function

    Public Shared Function GetBerryData() As String
        Dim s As String = "{route29.dat|13,0,5|6|2|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{route29.dat|14,0,5|6|2|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{route29.dat|15,0,5|6|2|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{azalea.dat|9,0,3|0|2|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{azalea.dat|9,0,4|1|1|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{azalea.dat|9,0,5|0|2|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{route30.dat|7,0,41|10|1|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{route30.dat|14,0,5|2|1|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{route30.dat|15,0,5|6|2|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{route30.dat|16,0,5|2|1|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{routes\route35.dat|0,0,4|7|2|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{routes\route35.dat|1,0,4|8|2|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{route36.dat|37,0,7|0|1|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{route36.dat|38,0,7|4|1|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{route36.dat|39,0,7|3|2|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{route39.dat|8,0,2|9|1|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{route39.dat|8,0,3|6|1|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{route38.dat|13,0,12|16|2|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{route38.dat|14,0,12|23|1|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{route38.dat|15,0,12|16|2|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{routes\route43.dat|13,0,45|23|2|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{routes\route43.dat|13,0,46|24|2|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{routes\route43.dat|13,0,47|25|2|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{safarizone\main.dat|3,0,11|5|2|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{safarizone\main.dat|4,0,11|0|2|0|2012,9,21,4,0,0|1}" & vbNewLine &
            "{safarizone\main.dat|5,0,11|6|3|0|2012,9,21,4,0,0|1}"

        Return s
    End Function

    Private Function NameReaction(ByVal name As String) As String
        Dim WeirdNames() As String = {"derp", "karp"}
        Dim KnownNames() As String = {"ash", "gary", "misty", "brock", "tracey", "may", "max", "dawn", "iris", "cilan", "red", "blue", "green", "gold", "silver"}
        Dim OwnNames() As String = {"oak", "samuel", "prof. oak", "prof oak"}

        Select Case True
            Case WeirdNames.Contains(name.ToLower())
                Return Localization.GetString("new_game_oak_weird_name_1") & name & Localization.GetString("new_game_oak_weird_name_2")
            Case KnownNames.Contains(name.ToLower())
                Return Localization.GetString("new_game_oak_known_name_1") & name & Localization.GetString("new_game_oak_known_name_2")
            Case OwnNames.Contains(name.ToLower())
                Return Localization.GetString("new_game_oak_same_name_1") & name & Localization.GetString("new_game_oak_same_name_2")
        End Select

        Return Localization.GetString("new_game_oak_name_1") & name & Localization.GetString("new_game_oak_name_2")
    End Function
End Class