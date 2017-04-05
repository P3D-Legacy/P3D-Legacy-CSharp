Imports System.Globalization
Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Data
Imports P3D.Legacy.Core.Dialogues
Imports P3D.Legacy.Core.Entities
Imports P3D.Legacy.Core.GameJolt
Imports P3D.Legacy.Core.GameJolt.Profiles
Imports P3D.Legacy.Core.Input
Imports P3D.Legacy.Core.Interfaces
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.Resources.Managers
Imports P3D.Legacy.Core.Resources.Managers.Music
Imports P3D.Legacy.Core.Resources.Managers.Sound
Imports P3D.Legacy.Core.Resources.Sound
Imports P3D.Legacy.Core.Screens
Imports P3D.Legacy.Core.Security
Imports P3D.Legacy.Core.Server
Imports P3D.Legacy.Core.Storage
Imports P3D.Legacy.Core.Storage.Folders
Imports P3D.Legacy.Core.World
Imports PCLExt.FileStorage

Public Class Player
    Inherits HashSecureBase
    Implements IPlayer
    'Implements IPlayer

#Region "Properties"

    Public Property Name() As String Implements IPlayer.Name
        Get
            Assert("_name", _name)
            Return _name
        End Get
        Set(value As String)
            Assert("_name", _name, value)
            _name = value
        End Set
    End Property

    Public Property RivalName() As String Implements IPlayer.RivalName
        Get
            Return _rivalName
        End Get
        Set(value As String)
            _rivalName = value
        End Set
    End Property

    Public Property Male() As Boolean Implements IPlayer.Male
        Get
            Return _male
        End Get
        Set(value As Boolean)
            _male = value
        End Set
    End Property

    Public Property Money() As Integer Implements IPlayer.Money
        Get
            Return _money
        End Get
        Set(value As Integer)
            _money = value
        End Set
    End Property

    Public Property OT() As String Implements IPlayer.OT
        Get
            Assert("_ot", _OT)
            Return _OT
        End Get
        Set(value As String)
            Assert("_ot", _OT, value)
            _OT = value
        End Set
    End Property

    Public Property Points() As Integer Implements IPlayer.Points
        Get
            Return _points
        End Get
        Set(value As Integer)
            _points = value
        End Set
    End Property

    Public Property BP() As Integer Implements IPlayer.BP
        Get
            Return _BP
        End Get
        Set(value As Integer)
            _BP = value
        End Set
    End Property

    Public Property Coins() As Integer Implements IPlayer.Coins
        Get
            Return _coins
        End Get
        Set(value As Integer)
            _coins = value
        End Set
    End Property

    Public Property HasPokedex() As Boolean Implements IPlayer.hasPokedex
        Get
            Return _hasPokedex
        End Get
        Set(value As Boolean)
            _hasPokedex = value
        End Set
    End Property

    Public Property HasPokegear() As Boolean Implements IPlayer.HasPokegear
        Get
            Return _hasPokegear
        End Get
        Set(value As Boolean)
            _hasPokegear = value
        End Set
    End Property

    Public Property LastRestPlace() As String Implements IPlayer.LastRestPlace
        Get
            Return _lastRestPlace
        End Get
        Set(value As String)
            _lastRestPlace = value
        End Set
    End Property

    Public Property LastRestPlacePosition() As String Implements IPlayer.LastRestPlacePosition
        Get
            Return _lastRestPlacePosition
        End Get
        Set(value As String)
            _lastRestPlacePosition = value
        End Set
    End Property

    Public Property LastSavePlace() As String Implements IPlayer.LastSavePlace
        Get
            Return _lastSavePlace
        End Get
        Set(value As String)
            _lastSavePlace = value
        End Set
    End Property

    Public Property LastSavePlacePosition() As String Implements IPlayer.LastSavePlacePosition
        Get
            Return _lastSavePlacePosition
        End Get
        Set(value As String)
            _lastSavePlacePosition = value
        End Set
    End Property

    Public Property RepelSteps() As Integer Implements IPlayer.RepelSteps
        Get
            Return _repelSteps
        End Get
        Set(value As Integer)
            _repelSteps = value
        End Set
    End Property

    Public Property SaveCreated() As String Implements IPlayer.SaveCreated
        Get
            Return _saveCreated
        End Get
        Set(value As String)
            _saveCreated = value
        End Set
    End Property

    Public Property DaycareSteps() As Integer Implements IPlayer.DaycareSteps
        Get
            Return _daycareSteps
        End Get
        Set(value As Integer)
            _daycareSteps = value
        End Set
    End Property

    Public Property GameMode() As String Implements IPlayer.GameMode
        Get
            Return _gameMode
        End Get
        Set(value As String)
            _gameMode = value
        End Set
    End Property

    Public Property Skin() As String Implements IPlayer.Skin
        Get
            Return _skin
        End Get
        Set(value As String)
            _skin = value
        End Set
    End Property

    Public Property VisitedMaps() As String Implements IPlayer.VisitedMaps
        Get
            Return _visitedMaps
        End Get
        Set(value As String)
            _visitedMaps = value
        End Set
    End Property

    Public Property GTSStars() As Integer Implements IPlayer.GTSStars
        Get
            Return _GTSStars
        End Get
        Set(value As Integer)
            _GTSStars = value
        End Set
    End Property

    Public Property SandBoxMode() As Boolean Implements IPlayer.SandBoxMode
        Get
            Assert("_sandboxmode", _sandBoxMode)
            Return _sandBoxMode
        End Get
        Set(value As Boolean)
            Assert("_sandboxmode", _sandBoxMode, value)
            _sandBoxMode = value
        End Set
    End Property

    Public Property RegisterData() As String Implements IPlayer.RegisterData
        Get
            Return _registerData
        End Get
        Set(value As String)
            _registerData = value
        End Set
    End Property

    Public Property BerryData() As String Implements IPlayer.BerryData
        Get
            Return _berryData
        End Get
        Set(value As String)
            _berryData = value
        End Set
    End Property

    Public Property PokedexData() As String Implements IPlayer.PokedexData
        Get
            Return _pokedexData
        End Get
        Set(value As String)
            _pokedexData = value
        End Set
    End Property

    Public Property ItemData() As String Implements IPlayer.ItemData
        Get
            Return _itemData
        End Get
        Set(value As String)
            _itemData = value
        End Set
    End Property

    Public Property BoxData() As String Implements IPlayer.BoxData
        Get
            Return _boxData
        End Get
        Set(value As String)
            _boxData = value
        End Set
    End Property

    Public Property NPCData() As String Implements IPlayer.NPCData
        Get
            Return _NPCData
        End Get
        Set(value As String)
            _NPCData = value
        End Set
    End Property

    Public Property ApricornData() As String Implements IPlayer.ApricornData
        Get
            Return _apricornData
        End Get
        Set(value As String)
            _apricornData = value
        End Set
    End Property

    Public Property SecretBaseData() As String Implements IPlayer.SecretBaseData
        Get
            Return _secretBaseData
        End Get
        Set(value As String)
            _secretBaseData = value
        End Set
    End Property

    Public Property DaycareData() As String Implements IPlayer.DaycareData
        Get
            Return _daycareData
        End Get
        Set(value As String)
            _daycareData = value
        End Set
    End Property

    Public Property HallOfFameData() As String Implements IPlayer.HallOfFameData
        Get
            Return _hallOfFameData
        End Get
        Set(value As String)
            _hallOfFameData = value
        End Set
    End Property

    Public Property RoamingPokemonData() As String Implements IPlayer.RoamingPokemonData
        Get
            Return _roamingPokemonData
        End Get
        Set(value As String)
            _roamingPokemonData = value
        End Set
    End Property

    Public Property HistoryData() As String Implements IPlayer.HistoryData
        Get
            Return _historyData
        End Get
        Set(value As String)
            _historyData = value
        End Set
    End Property

    Public Property IsGameJoltSave() As Boolean Implements IPlayer.IsGamejoltSave
        Get
            Assert("_isgamejoltsave", _isGamejoltSave)
            Return _isGamejoltSave
        End Get
        Set(value As Boolean)
            Assert("_isgamejoltsave", _isGamejoltSave, value)
            _isGamejoltSave = value
        End Set
    End Property

    Public Property EmblemBackground() As String Implements IPlayer.EmblemBackground
        Get
            Assert("_emblembackground", _emblemBackground)
            Return _emblemBackground
        End Get
        Set(value As String)
            Assert("_emblembackground", _emblemBackground, value)
            _emblemBackground = value
        End Set
    End Property

#End Region

    'Non-base datatypes:
    Public PokegearModules As New List(Of Integer)
    Public PhoneContacts As New List(Of String)
    Public Trophies As New List(Of Integer)

    'Non-secure fields:

    'Secure fields:
    Private _name As String = "<playername>"
    Private _rivalName As String = ""
    Private _male As Boolean = True
    Private _money As Integer = 0
    Private _OT As String = "00000"
    Private _points As Integer = 0
    Private _BP As Integer = 0
    Private _coins As Integer = 0
    Private _hasPokedex As Boolean = False
    Private _hasPokegear As Boolean = False
    Private _lastRestPlace As String = "yourroom.dat"
    Private _lastRestPlacePosition As String = "1,0.1,3"
    Private _lastSavePlace As String = "yourroom.dat"
    Private _lastSavePlacePosition As String = "1,0.1,3"
    Private _repelSteps As Integer = 0
    Private _saveCreated As String = "Pre 0.21"
    Private _daycareSteps As Integer = 0
    Private _gameMode As String = "Pokemon 3D"
    Private _skin As String = "Hilbert"
    Private _visitedMaps As String = ""
    Private _GTSStars As Integer = 8
    Private _sandBoxMode As Boolean = False

    Private _registerData As String = ""
    Private _berryData As String = ""
    Private _pokedexData As String = ""
    Private _itemData As String = ""
    Private _boxData As String = ""
    Private _NPCData As String = ""
    Private _apricornData As String = ""
    Private _secretBaseData As String = ""
    Private _daycareData As String = ""
    Private _hallOfFameData As String = ""
    Private _roamingPokemonData As String = ""
    Private _historyData As String = ""
    Private _isGamejoltSave As Boolean = False
    Private _emblemBackground As String = "standard"

    private _userSaveFolder as UserSaveFolder

    Public filePrefix As String = "nilllzz"
    Public newFilePrefix As String = ""
    Public AutosaveUsed As Boolean = False

    Public Structure Temp
        Public Shared PokemonScreenIndex As Integer = 0
        Public Shared PokemonStatusPageIndex As Integer = 0
        Public Shared BagIndex As Integer = 0
        Public Shared BagSelectIndex As Integer = 0
        Public Shared MenuIndex As Integer = 0
        Public Shared PokedexIndex As Integer = 0
        Public Shared PCBoxIndex As Integer = 0
        Public Shared StorageSystemCursorPosition As New Vector2(1, 0)
        Public Shared OptionScreenIndex As Integer = 0
        Public Shared MapSwitch(3) As Boolean
        Public Shared LastPosition As Vector3
        Public Shared IsInBattle As Boolean = False
        Public Shared BeforeBattlePosition As Vector3 = New Vector3(0)
        Public Shared BeforeBattleLevelFile As String = "yourroom.dat"
        Public Shared BeforeBattleFacing As Integer = 0
        Public Shared PokedexModeIndex As Integer = 0
        Public Shared PokedexHabitatIndex As Integer = 0
        Public Shared PokegearPage As Integer = 0
        Public Shared LastCall As Integer = 32
        Public Shared LastUsedRepel As Integer = -1
        Public Shared MapSteps As Integer = 0
        Public Shared HallOfFameIndex As Integer = 0
        Public Shared PCBoxChooseMode As Boolean = False
        Public Shared PCSelectionType As StorageSystemScreen.SelectionModes = StorageSystemScreen.SelectionModes.SingleMove
        Public Shared RadioStation As Decimal = 0D
        Public Shared LastPokegearPage As GameJolt.PokegearScreen.MenuScreens = GameJolt.PokegearScreen.MenuScreens.Main
    End Structure

    Private Sub ResetTemp()
        Temp.PokemonScreenIndex = 0
        Temp.PokemonStatusPageIndex = 0
        Temp.BagIndex = 0
        Temp.BagSelectIndex = 0
        Temp.MenuIndex = 0
        Temp.PokedexIndex = 0
        Temp.PCBoxIndex = 0
        Temp.OptionScreenIndex = 0
        Temp.IsInBattle = False
        For i = 0 To 3
            Temp.MapSwitch(i) = True
        Next
        Temp.PokedexModeIndex = 0
        Temp.PokedexHabitatIndex = 0
        Temp.PokegearPage = 0
        Temp.LastCall = 32
        Temp.LastUsedRepel = -1
        Temp.MapSteps = 0
        Temp.HallOfFameIndex = 0
        Temp.PCBoxChooseMode = False
        Temp.StorageSystemCursorPosition = New Vector2(1, 0)
        Temp.PCSelectionType = StorageSystemScreen.SelectionModes.SingleMove
        Temp.RadioStation = 0D
        Temp.LastPokegearPage = GameJolt.PokegearScreen.MenuScreens.Main
    End Sub

#Region "Load"

    Public Sub LoadGame(ByVal filePrefix As String) Implements IPlayer.LoadGame
        For Each s As String In Core.GameOptions.ContentPackNames
            ContentPackManager.Load(GameController.GamePath & "\ContentPacks\" & s & "\exceptions.dat")
        Next

        GameModeManager.CreateDefaultGameMode()

        ScriptStorage.Clear()
        ScriptBlock.TriggeredScriptBlock = False
        MysteryEventScreen.ClearActivatedEvents()
        Pokedex.AutoDetect = True
        LevelLoader.ClearTempStructures()
        BattleSystem.BattleScreen.ResetVars()
        World.RegionWeatherSet = False

        Me.filePrefix = filePrefix
        _userSaveFolder = StorageInfo.SaveFolder.GetUserSaveFolder(filePrefix)
        PokeFiles.Clear()
        GameMode = "Pokemon 3D"

        LoadPlayer()

        If GameModeManager.GameModeExists(GameMode) = False Then
            GameMode = "Pokemon 3D"
            GameModeManager.SetGameModePointer("Pokemon 3D")
        Else
            GameModeManager.SetGameModePointer(GameMode)
        End If

        GameModeAttackLoader.Load()

        If IsGameJoltSave = True Then
            SandBoxMode = False
        End If

        Localization.ReloadGameModeTokens()

        If GameModeManager.ActiveGameMode.IsDefaultGamemode = False Then
            MusicManager.LoadMusic(True)
            SoundEffectManager.LoadSounds(True)
        End If
        SmashRock.Load()
        Badge.Load()
        Pokedex.Load()
        PokemonInteractions.Load()
        PokemonForms.Initialize()

        LoadPokedex()
        LoadParty()
        LoadItems()
        LoadBerries()
        LoadApricorns()
        LoadDaycare()
        LoadOptions()
        LoadRegister()
        LoadItemData()
        LoadBoxData()
        LoadNPCData()
        LoadHallOfFameData()
        LoadSecretBaseData()
        LoadRoamingPokemonData()
        LoadStatistics()

        PlayerTemp.Reset()
        ResetTemp()
        Chat.ClearChat()

        If AutosaveUsed = True Then
            IO.Directory.Delete(GameController.GamePath & "\Save\" & Me.filePrefix, True)

            Me.filePrefix = newFilePrefix
            AutosaveUsed = False

            Dim outputString As String = newFilePrefix

            Core.GameMessage.ShowMessage(Localization.GetString("game_message_continue_autosave") & " """ & outputString & """", 12, FontManager.MainFont, Color.White)

            newFilePrefix = ""
        End If

        If IsGameJoltSave = True Then
            lastLevel = Emblem.GetPlayerLevel(Core.GameJoltSave.Points)
            OT = Core.GameJoltSave.GameJoltID
        End If

        Entity.MakeShake = Name.ToLower() = "drunknilllzz"

        ''' Indev 0.54 Removal List
        ''' 1. All Mega Stones. [ID: 507 - 553]
        ''' 2. Shiny Candy [ID: 501]
        If Not ActionScript.IsRegistered("PokemonIndev054Update") Then
            ' Check Inventory.
            Inventory.RemoveItem(501)
            For i As Integer = 507 To 553 Step +1
                Inventory.RemoveItem(i)
            Next

            ' Check Party Pokemon.
            For Each Pokemon As Pokemon In Pokemons
                If Pokemon.Item IsNot Nothing AndAlso (Pokemon.Item.ID >= 501 OrElse (Pokemon.Item.ID >= 507 AndAlso Pokemon.Item.ID <= 553)) Then
                    Pokemon.Item = Nothing
                End If
            Next

            ' Check PC Boxes.
            If Not String.IsNullOrWhiteSpace(BoxData) Then
                Dim TempBoxData As New List(Of String)
                TempBoxData.AddRange(BoxData.SplitAtNewline())

                For Each item As String In TempBoxData
                    If Not String.IsNullOrWhiteSpace(item) AndAlso Not item.StartsWith("BOX") Then
                        Dim TempString As String = item.Remove(item.IndexOf("{"))
                        Dim TempPokemon As Pokemon = Pokemon.GetPokemonByData(item.Remove(0, item.IndexOf("{")))

                        If TempPokemon.Item IsNot Nothing AndAlso (TempPokemon.Item.ID >= 501 OrElse (TempPokemon.Item.ID >= 507 AndAlso TempPokemon.Item.ID <= 553)) Then
                            TempPokemon.Item = Nothing
                        End If

                        item = TempString & TempPokemon.ToString()
                    End If
                Next

                BoxData = String.Join(vbNewLine, TempBoxData)
            End If

            ' Check Day Care.
            If Not String.IsNullOrWhiteSpace(DaycareData) Then
                Dim TempDaycareData As New List(Of String)
                TempDaycareData.AddRange(DaycareData.SplitAtNewline())

                For Each item As String In TempDaycareData
                    If Not String.IsNullOrWhiteSpace(item) AndAlso item.Contains("{") Then
                        Dim TempString As String = ItemData.Remove(item.IndexOf("{"))
                        Dim TempPokemon As Pokemon = Pokemon.GetPokemonByData(item.Remove(0, item.IndexOf("{")))

                        If TempPokemon.Item IsNot Nothing AndAlso (TempPokemon.Item.ID >= 501 OrElse (TempPokemon.Item.ID >= 507 AndAlso TempPokemon.Item.ID <= 553)) Then
                            TempPokemon.Item = Nothing
                        End If

                        item = TempString & TempPokemon.ToString()
                    End If
                Next

                DaycareData = String.Join(vbNewLine, TempDaycareData)
            End If

            ActionScript.RegisterID("PokemonIndev054Update")
        End If

        loadedSave = True
    End Sub

    Private Sub LoadParty()
        Pokemons.Clear()

        Dim PokeData() As String
        If IsGameJoltSave = True Then
            PokeData = Core.GameJoltSave.Party.SplitAtNewline()
        Else
            PokeData = _userSaveFolder.PartyFile.ReadAllLines()
        End If

        If PokeData.Count > 0 AndAlso PokeData(0) <> "" Then
            For Each Line As String In PokeData
                If Line.StartsWith("{") = True And Line.EndsWith("}") = True Then
                    Dim p As Pokemon = Pokemon.GetPokemonByData(Line)

                    If p.IsEgg() = False Then
                        If p.IsShiny = True Then
                            PokedexData = Pokedex.ChangeEntry(PokedexData, p.Number, 3)
                        Else
                            PokedexData = Pokedex.ChangeEntry(PokedexData, p.Number, 2)
                        End If
                    End If

                    Pokemons.Add(p)
                End If
            Next
        End If
    End Sub

    Private Sub LoadPlayer()
        Dim Data() As String
        If IsGameJoltSave = True Then
            Data = Core.GameJoltSave.Player.SplitAtNewline()
        Else
            Data = _userSaveFolder.PlayerFile.ReadAllLines()
        End If

        Screen.Level.IsRiding = False

        For Each Line As String In Data
            If Line <> "" And Line.Contains("|") = True Then
                Dim ID As String = Line.Remove(Line.IndexOf("|"))
                Dim Value As String = Line.Remove(0, Line.IndexOf("|") + 1)
                Select Case ID.ToLower()
                    Case "name"
                        Name = Value

                        If IsGameJoltSave = True Then
                            If Name.ToLower() <> API.username.ToLower() Then
                                Name = API.username
                            End If
                        End If
                    Case "position"
                        Dim v() As String = Value.Split(CChar(","))
                        dim x  = Single.Parse(v(0), NumberFormatInfo.InvariantInfo)
                        dim y = Single.Parse(v(1), NumberFormatInfo.InvariantInfo)
                        dim z = Single.Parse(v(2), NumberFormatInfo.InvariantInfo)
                        startPosition = new Vector3(x, y, z)
                    Case "lastpokemonposition"
                        Dim v() As String = Value.Split(CChar(","))
                        dim x = Single.Parse(v(0), NumberFormatInfo.InvariantInfo)
                        dim y = Single.Parse(v(1), NumberFormatInfo.InvariantInfo)
                        dim z = Single.Parse(v(2), NumberFormatInfo.InvariantInfo)
                        LastPokemonPosition = New Vector3(x, y, z)
                    Case "mapfile"
                        startMap = Value
                    Case "rivalname"
                        RivalName = Value
                    Case "money"
                        Money = CInt(Value)
                    Case "badges"
                        Badges.Clear()

                        If Value = "0" Then
                            Badges = New List(Of Integer)
                        Else
                            If Value.Contains(",") = False Then
                                Badges = {CInt(Value)}.ToList()
                            Else
                                Dim l As List(Of String) = Value.Split(CChar(",")).ToList()

                                For i = 0 To l.Count - 1
                                    Badges.Add(CInt(l(i)))
                                Next
                            End If
                        End If
                    Case "rotation"
                        startRotation = Single.Parse(Value, NumberFormatInfo.InvariantInfo)
                    Case "Gender"
                        If Value = "Male" Then
                            Male = True
                        Else
                            Male = False
                        End If
                    Case "playtime"
                        Dim dd() As String = Value.Split(CChar(","))
                        If dd.Count >= 4 Then
                            PlayTime = New TimeSpan(CInt(dd(3)), CInt(dd(0)), CInt(dd(1)), CInt(dd(2)))
                        Else
                            PlayTime = New TimeSpan(CInt(dd(0)), CInt(dd(1)), CInt(dd(2)))
                        End If
                    Case "ot"
                        OT = CStr(CInt(Value).Clamp(0, 99999))
                    Case "points"
                        Points = CInt(Value)
                    Case "haspokedex"
                        HasPokedex = CBool(Value)
                    Case "haspokegear"
                        HasPokegear = CBool(Value)
                    Case "freecamera"
                        startFreeCameraMode = CBool(Value)
                    Case "thirdperson"
                        startThirdPerson = CBool(Value)
                    Case "skin"
                        Skin = Value
                    Case "battleanimations"
                        ShowBattleAnimations = CInt(Value)
                    Case "boxamount"
                        BoxAmount = CInt(Value)
                    Case "lastrestplace"
                        LastRestPlace = Value
                    Case "lastrestplaceposition"
                        LastRestPlacePosition = Value
                    Case "diagonalmovement"
                        If GameController.IS_DEBUG_ACTIVE = True Then
                            DiagonalMovement = CBool(Value)
                        Else
                            DiagonalMovement = False
                        End If
                    Case "repelsteps"
                        RepelSteps = CInt(Value)
                    Case "lastsaveplace"
                        LastSavePlace = Value
                    Case "lastsaveplaceposition"
                        LastSavePlacePosition = Value
                    Case "difficulty"
                        DifficultyMode = CInt(Value)
                    Case "battlestyle"
                        BattleStyle = CInt(Value)
                    Case "savecreated"
                        SaveCreated = Value
                    Case "autosave"
                        If IsGameJoltSave = False Then
                            newFilePrefix = Value
                            AutosaveUsed = True
                        End If
                    Case "daycaresteps"
                        DaycareSteps = CInt(Value)
                    Case "gamemode"
                        GameMode = Value
                    Case "pokefiles"
                        If Value <> "" Then
                            If Value.Contains(",") = True Then
                                PokeFiles.AddRange(Value.Split(CChar(",")))
                            Else
                                PokeFiles.Add(Value)
                            End If
                        End If
                    Case "visitedmaps"
                        VisitedMaps = Value
                    Case "tempsurfskin"
                        TempSurfSkin = Value
                    Case "surfing"
                        startSurfing = CBool(Value)
                        Screen.Level.IsSurfing = CBool(Value)
                    Case "bp"
                        BP = CInt(Value)
                    Case "gtsstars"
                        GTSStars = CInt(Value)
                    Case "showmodels"
                        ShowModelsInBattle = CBool(Value)
                    Case "sandboxmode"
                        SandBoxMode = CBool(Value)
                    Case "earnedachievements"
                        If Value <> "" Then
                            EarnedAchievements = Value.Split(CChar(",")).ToList()
                        End If
                End Select
            Else
                Logger.Log(Logger.LogTypes.Warning, "Player.vb: The line """ & Line & """ is either empty or does not conform the player.dat file rules.")
            End If
        Next

        If IsGameJoltSave = True And Screen.Level.IsSurfing = False Then
            Skin = Emblem.GetPlayerSpriteFile(Emblem.GetPlayerLevel(Core.GameJoltSave.Points), Core.GameJoltSave.GameJoltID, Core.GameJoltSave.Gender)
            Select Case Core.GameJoltSave.Gender
                Case "0"
                    Male = True
                Case "1"
                    Male = False
                Case Else
                    Male = True
            End Select
        End If

        GameStart = Date.Now
    End Sub

    Private Sub LoadOptions()
        Dim Data() As String
        If IsGameJoltSave = True Then
            Data = Core.GameJoltSave.Options.SplitAtNewline()
        Else
            Data = _userSaveFolder.OptionsFile.ReadAllLines()
        End If

        For Each Line As String In Data
            If Line.Contains("|") = True Then
                Dim ID As String = Line.Remove(Line.IndexOf("|"))
                Dim Value As String = Line.Remove(0, Line.IndexOf("|") + 1)
                Select Case ID.ToLower()
                    Case "fov"
                        startFOV = Single.Parse(Value, NumberFormatInfo.InvariantInfo).Clamp(1, 179)
                    Case "textspeed"
                        TextBox.TextSpeed = CInt(Value)
                    Case "mousespeed"
                        startRotationSpeed = CInt(Value)
                End Select
            End If
        Next
    End Sub

    Private Sub LoadItems()
        Inventory.Clear()
        Mails.Clear()

        Dim Data As String
        If IsGameJoltSave = True Then
            Data = Core.GameJoltSave.Items
        Else
            Data = _userSaveFolder.ItemsFile.ReadAllText()
        End If

        If Data <> "" Then
            Dim ItemData() As String = Data.SplitAtNewline()

            For Each ItemDat As String In ItemData
                If ItemDat <> "" And ItemDat.StartsWith("{") = True And ItemDat.EndsWith("}") = True And ItemDat.Contains("|") = True Then
                    Dim ItemID As String = ItemDat.Remove(0, ItemDat.IndexOf("{") + 1)
                    ItemID = ItemID.Remove(ItemID.IndexOf("}"))

                    Dim amount As Integer = CInt(ItemID.Remove(0, ItemID.IndexOf("|") + 1))
                    ItemID = ItemID.Remove(ItemID.IndexOf("|"))

                    Inventory.AddItem(CInt(ItemID), amount)
                Else
                    If ItemDat <> "" And ItemDat.StartsWith("Mail|") = True Then
                        Dim mailData As String = ItemDat.Remove(0, 5)
                        Mails.Add(MailItem.GetMailDataFromString(mailData))
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub LoadBerries()
        If IsGameJoltSave = True Then
            Core.Player.BerryData = Core.GameJoltSave.Berries
        Else
            Core.Player.BerryData = _userSaveFolder.BerriesFile.ReadAllText()
        End If
    End Sub

    Private Sub LoadApricorns()
        If IsGameJoltSave = True Then
            Core.Player.ApricornData = Core.GameJoltSave.Apricorns
        Else
            Core.Player.ApricornData = _userSaveFolder.ApricornsFile.ReadAllText()
        End If
    End Sub

    Private Sub LoadDaycare()
        Core.Player.DaycareData = ""
        If IsGameJoltSave = True Then
            Core.Player.DaycareData = Core.GameJoltSave.Daycare
        Else
            Core.Player.DaycareData = _userSaveFolder.DaycareFile.ReadAllText()
        End If
    End Sub

    Private Sub LoadPokedex()
        If IsGameJoltSave = True Then
            PokedexData = Core.GameJoltSave.Pokedex
        Else
            PokedexData = _userSaveFolder.PokedexFile.ReadAllText()
        End If

        If PokedexData = "" Then
            PokedexData = Pokedex.NewPokedex()
        End If
    End Sub

    Private Sub LoadRegister()
        If IsGameJoltSave = True Then
            RegisterData = Core.GameJoltSave.Register
        Else
            RegisterData = _userSaveFolder.RegisterFile.ReadAllText()
        End If
    End Sub

    Private Sub LoadItemData()
        If IsGameJoltSave = True Then
            ItemData = Core.GameJoltSave.ItemData
        Else
            ItemData = _userSaveFolder.ItemDataFile.ReadAllText()
        End If
    End Sub

    Private Sub LoadBoxData()
        If IsGameJoltSave = True Then
            BoxData = Core.GameJoltSave.Box
        Else
            BoxData = _userSaveFolder.BoxFile.ReadAllText()
        End If
    End Sub

    Private Sub LoadNPCData()
        If IsGameJoltSave = True Then
            NPCData = Core.GameJoltSave.NPC
        Else
            NPCData = _userSaveFolder.NPCFile.ReadAllText()
        End If
    End Sub

    Private Sub LoadHallOfFameData()
        If IsGameJoltSave = True Then
            HallOfFameData = Core.GameJoltSave.HallOfFame
        Else
            HallOfFameData = _userSaveFolder.HallOfFameFile.ReadAllText()
        End If
    End Sub

    Private Sub LoadSecretBaseData()
        If IsGameJoltSave = True Then
            SecretBaseData = Core.GameJoltSave.SecretBase
        Else
            SecretBaseData = _userSaveFolder.SecretBaseFile.ReadAllText()
        End If
    End Sub

    Private Sub LoadRoamingPokemonData()
        RoamingPokemonData = ""
        If IsGameJoltSave = True Then
            RoamingPokemonData = Core.GameJoltSave.RoamingPokemon
        Else
            For Each line As String In _userSaveFolder.RoamingPokemonFile.ReadAllText()
                If RoamingPokemonData <> "" Then
                    RoamingPokemonData &= vbNewLine
                End If
                If line.CountSeperators("|") < 5 Then
                    'Convert potential old data:
                    Dim data() As String = line.Split(CChar("|"))
                    Dim newP As Pokemon = Pokemon.GetPokemonByID(CInt(data(0)))
                    newP.Generate(CInt(data(1)), True)
                    
                    RoamingPokemonData &= newP.Number.ToString(NumberFormatInfo.InvariantInfo) & "|" & newP.Level.ToString(NumberFormatInfo.InvariantInfo) & "|" & data(2) & "|" & data(3) & "||" & newP.GetSaveData()
                Else
                    RoamingPokemonData &= line
                End If
            Next
        End If
    End Sub

    Private Sub LoadStatistics()
        If IsGameJoltSave = True Then
            Statistics = Core.GameJoltSave.Statistics
        Else
            Statistics = _userSaveFolder.StatisticsFile.ReadAllText()
        End If
        PlayerStatistics.Load(Statistics)
    End Sub

#End Region

#Region "Save"

    Dim GameJoltTempStoreString As New Dictionary(Of String, String)

    Public Sub SaveGame(ByVal IsAutosave As Boolean) Implements IPlayer.SaveGame
        SaveGameHelpers.ResetSaveCounter()

        Dim oldUserSaveFolder = _userSaveFolder
        If IsAutosave = True Then
            _userSaveFolder = StorageInfo.SaveFolder.AutosaveFolder
        End If

        GameJoltTempStoreString.Clear()

        SavePlayer(IsAutosave)
        SaveParty()
        SaveItems()
        SaveBerries()
        SaveApricorns()
        SaveDaycare()
        SaveOptions()
        SavePokedex()
        SaveRegister()
        SaveItemData()
        SaveBoxData()
        SaveNPCData()
        SaveHallOfFameData()
        SaveSecretBaseData()
        SaveRoamingPokemonData()
        SaveStatistics()

        _userSaveFolder = oldUserSaveFolder

        If IsGameJoltSave = True Then
            Dim APICallSave As New APICall(AddressOf SaveGameHelpers.CompleteGameJoltSave)

            Dim keys As New List(Of String)
            Dim dataItems As New List(Of String)
            Dim useUsername As New List(Of Boolean)

            For i = 0 To GameJoltTempStoreString.Count - 1
                keys.Add(GameJoltTempStoreString.Keys(i))
                dataItems.Add(GameJoltTempStoreString.Values(i))
                useUsername.Add(True)
            Next
            APICallSave.SetStorageData(keys.ToArray(), dataItems.ToArray(), useUsername.ToArray())

            SavePublicVars()

            Core.GameJoltSave.UpdatePlayerScore()
        End If
    End Sub

    Private Sub SavePublicVars()
        If API.UserBanned(Core.GameJoltSave.GameJoltID) = False Then
            Dim APICallPoints As New APICall(AddressOf SaveGameHelpers.AddGameJoltSaveCounter)
            APICallPoints.SetStorageData("saveStorageV" & GamejoltSave.VERSION & "|" & Core.GameJoltSave.GameJoltID & "|points", Core.GameJoltSave.Points.ToString(NumberFormatInfo.InvariantInfo), False)

            Dim APICallEmblem As New APICall(AddressOf SaveGameHelpers.AddGameJoltSaveCounter)
            APICallEmblem.SetStorageData("saveStorageV" & GamejoltSave.VERSION & "|" & Core.GameJoltSave.GameJoltID & "|emblem", Core.GameJoltSave.EmblemS, False)

            Dim APICallGender As New APICall(AddressOf SaveGameHelpers.AddGameJoltSaveCounter)
            APICallGender.SetStorageData("saveStorageV" & GamejoltSave.VERSION & "|" & Core.GameJoltSave.GameJoltID & "|gender", Core.GameJoltSave.Gender, False)
        End If
    End Sub

    Public Function GetPartyData() As String Implements IPlayer.GetPartyData
        Dim Data As String = ""
        For i = 0 To Pokemons.Count - 1
            If Data <> "" Then
                Data &= vbNewLine
            End If
            Data &= Pokemons(i).GetSaveData()
        Next
        Return Data
    End Function

    Public Function GetPlayerData(ByVal IsAutosave As Boolean) As String Implements IPlayer.GetPlayerData
        Dim GenderString As String = ""
        If Male = True Then
            GenderString = "Male"
        Else
            GenderString = "Female"
        End If

        Dim badgeString As String = ""
        If Badges.Count > 0 Then
            For i = 0 To Badges.Count - 1
                If i <> 0 Then
                    badgeString &= ","
                End If
                badgeString &= Badges(i).ToString(NumberFormatInfo.InvariantInfo)
            Next
        Else
            badgeString = "0"
        End If

        Dim hasPokedexString As String = HasPokedex.ToNumberString()

        Dim c As OverworldCamera = GetOverworldCamera()
        Dim freeCameraString As String = c.FreeCameraMode.ToNumberString()

        Dim diff As Integer = CInt(DateDiff(DateInterval.Second, GameStart, Date.Now))
        Dim p As TimeSpan = PlayTime + TimeHelpers.ConvertSecondToTime(diff)
        Dim PlayTimeString As String = p.Hours & "," & p.Minutes & "," & p.Seconds & "," & p.Days

        Dim lastPokemonPosition As String = "999,999,999"
        If Screen.Level.OverworldPokemon.Visible = True Then
            lastPokemonPosition = (Screen.Level.OverworldPokemon.Position.X.ToString(NumberFormatInfo.InvariantInfo) & "," & Screen.Level.OverworldPokemon.Position.Y.ToString(NumberFormatInfo.InvariantInfo) & "," & Screen.Level.OverworldPokemon.Position.Z.ToString(NumberFormatInfo.InvariantInfo))
        End If

        Dim PokeFilesString As String = ""
        If PokeFiles.Count > 0 Then
            For Each pokefile As String In PokeFiles
                If PokeFilesString <> "" Then
                    PokeFilesString &= ","
                End If

                PokeFilesString &= pokefile
            Next
        End If

        Dim EarnedAchievementsString As String = ""
        If EarnedAchievements.Count > 0 Then
            For Each ea As String In EarnedAchievements
                If EarnedAchievementsString <> "" Then
                    EarnedAchievementsString &= ","
                End If

                EarnedAchievementsString &= ea
            Next
        End If

        Dim skin As String = Screen.Level.OwnPlayer.SkinName
        If Screen.Level.IsRiding = True Then
            skin = TempRideSkin
        End If

        Dim Data As String = "Name|" & Name & vbNewLine &
            "Position|" & c.Position.X.ToString(NumberFormatInfo.InvariantInfo) & "," & c.Position.Y.ToString(NumberFormatInfo.InvariantInfo) & "," & c.Position.Z.ToString(NumberFormatInfo.InvariantInfo) & vbNewLine &
            "MapFile|" & Screen.Level.LevelFile & vbNewLine &
            "Rotation|" & c.Yaw.ToString(NumberFormatInfo.InvariantInfo) & vbNewLine &
            "RivalName|" & RivalName & vbNewLine &
            "Money|" & Money & vbNewLine &
            "Badges|" & badgeString & vbNewLine &
            "Gender|" & GenderString & vbNewLine &
            "PlayTime|" & PlayTimeString & vbNewLine &
            "OT|" & OT & vbNewLine &
            "Points|" & Points.ToString(NumberFormatInfo.InvariantInfo) & vbNewLine &
            "hasPokedex|" & hasPokedexString & vbNewLine &
            "hasPokegear|" & HasPokegear.ToNumberString() & vbNewLine &
            "freeCamera|" & freeCameraString & vbNewLine &
            "thirdPerson|" & c.ThirdPerson.ToNumberString() & vbNewLine &
            "skin|" & skin & vbNewLine &
            "location|" & Screen.Level.MapName & vbNewLine &
            "battleAnimations|" & ShowBattleAnimations.ToString(NumberFormatInfo.InvariantInfo) & vbNewLine &
            "BoxAmount|" & BoxAmount.ToString(NumberFormatInfo.InvariantInfo) & vbNewLine &
            "LastRestPlace|" & LastRestPlace & vbNewLine &
            "LastRestPlacePosition|" & LastRestPlacePosition & vbNewLine &
            "DiagonalMovement|" & DiagonalMovement.ToNumberString() & vbNewLine &
            "RepelSteps|" & RepelSteps.ToString(NumberFormatInfo.InvariantInfo) & vbNewLine &
            "LastSavePlace|" & LastSavePlace & vbNewLine &
            "LastSavePlacePosition|" & LastSavePlacePosition & vbNewLine &
            "Difficulty|" & DifficultyMode.ToString(NumberFormatInfo.InvariantInfo) & vbNewLine &
            "BattleStyle|" & BattleStyle.ToString(NumberFormatInfo.InvariantInfo) & vbNewLine &
            "saveCreated|" & SaveCreated & vbNewLine &
            "LastPokemonPosition|" & lastPokemonPosition & vbNewLine &
            "DaycareSteps|" & DaycareSteps.ToString(NumberFormatInfo.InvariantInfo) & vbNewLine &
            "GameMode|" & GameMode & vbNewLine &
            "PokeFiles|" & PokeFilesString & vbNewLine &
            "VisitedMaps|" & VisitedMaps & vbNewLine &
            "TempSurfSkin|" & TempSurfSkin & vbNewLine &
            "Surfing|" & Screen.Level.IsSurfing.ToNumberString() & vbNewLine &
            "BP|" & BP & vbNewLine &
            "ShowModels|" & ShowModelsInBattle.ToNumberString() & vbNewLine &
            "GTSStars|" & GTSStars & vbNewLine &
            "SandBoxMode|" & SandBoxMode.ToNumberString() & vbNewLine &
            "EarnedAchievements|" & EarnedAchievementsString

        If IsAutosave = True Then
            Data &= vbNewLine & "AutoSave|" & newFilePrefix
        End If

        Return Data
    End Function

    Public Function GetOptionsData() As String Implements IPlayer.GetOptionsData
        Dim c As OverworldCamera = GetOverworldCamera()

        Dim FOVstring As String = c.FOV.ToString.Replace(",", ".")
        Dim MouseSpeedString As String = CStr(c.RotationSpeed * 10000)
        Dim TextSpeedString As String = CStr(TextBox.TextSpeed)

        Dim Data As String = "FOV|" & FOVstring & vbNewLine &
            "TextSpeed|" & TextSpeedString & vbNewLine &
            "MouseSpeed|" & MouseSpeedString

        Return Data
    End Function

    Public Function GetItemsData() As String Implements IPlayer.GetItemsData
        Dim Data As String = ""

        For Each c In Inventory
            If Data <> "" Then
                Data &= vbNewLine
            End If

            Data &= "{" & c.ItemId & "|" & c.Amount & "}"
        Next

        For Each mail As MailItem.MailData In Mails
            If Data <> "" Then
                Data &= vbNewLine
            End If
            Data &= "Mail|" & MailItem.GetStringFromMail(mail)
        Next

        Return Data
    End Function

    Public Function GetBerriesData() As String Implements IPlayer.GetBerriesData
        Return BerryData
    End Function

    Public Function GetApricornsData() As String Implements IPlayer.GetApricornsData
        Return ApricornData
    End Function

    Public Function GetDaycareData() As String Implements IPlayer.GetDaycareData
        Return DaycareData
    End Function

    Public Function GetPokedexData() As String Implements IPlayer.GetPokedexData
        Return PokedexData
    End Function

    Public Function GetRegisterData() As String Implements IPlayer.GetRegisterData
        Return RegisterData
    End Function

    Public Function GetItemDataData() As String Implements IPlayer.GetItemDataData
        Return ItemData
    End Function

    Public Function GetBoxData() As String Implements IPlayer.GetBoxData
        Return BoxData
    End Function

    Public Function GetNPCDataData() As String Implements IPlayer.GetNPCDataData
        Return NPCData
    End Function

    Public Function GetHallOfFameData() As String Implements IPlayer.GetHallOfFameData
        Return HallOfFameData
    End Function

    Public Function GetSecretBaseData() As String Implements IPlayer.GetSecretBaseData
        Return SecretBaseData
    End Function

    Public Function GetRoamingPokemonData() As String Implements IPlayer.GetRoamingPokemonData
        Return RoamingPokemonData
    End Function

    Public Function GetStatisticsData() As String Implements IPlayer.GetStatisticsData
        Return Statistics
    End Function

    Private Function GetOverworldCamera() As OverworldCamera
        Dim baseScreen As Screen = Core.CurrentScreen
        While Not baseScreen.PreScreen Is Nothing
            baseScreen = baseScreen.PreScreen
        End While

        If baseScreen.Identification = Screen.Identifications.BattleScreen Then
            Return CType(CType(baseScreen, BattleSystem.BattleScreen).SavedOverworld.Camera, OverworldCamera)
        ElseIf baseScreen.Identification = Screen.Identifications.CreditsScreen Then
            Return CType(CType(baseScreen, CreditsScreen).SavedOverworld.Camera, OverworldCamera)
        ElseIf baseScreen.Identification = Screen.Identifications.HallofFameScreen Then
            Return CType(CType(baseScreen, HallOfFameScreen).SavedOverworld.Camera, OverworldCamera)
        End If

        Return CType(Screen.Camera, OverworldCamera)
    End Function

    Private Sub SaveParty()
        If IsGameJoltSave = True Then
            GameJoltTempStoreString.Add("saveStorageV" & GamejoltSave.VERSION & "|" & Core.GameJoltSave.GameJoltID & "|party", GetPartyData())
        Else
            _userSaveFolder.PartyFile.WriteAllText(GetPartyData())
        End If
    End Sub

    Private Sub SavePlayer(ByVal IsAutosave As Boolean)
        If IsGameJoltSave = True Then
            GameJoltTempStoreString.Add("saveStorageV" & GamejoltSave.VERSION & "|" & Core.GameJoltSave.GameJoltID & "|player", GetPlayerData(IsAutosave))
        Else
            _userSaveFolder.PlayerFile.WriteAllText(GetPlayerData(IsAutosave))
        End If
    End Sub

    Private Sub SaveOptions()
        If IsGameJoltSave = True Then
            GameJoltTempStoreString.Add("saveStorageV" & GamejoltSave.VERSION & "|" & Core.GameJoltSave.GameJoltID & "|options", GetOptionsData())
        Else
            _userSaveFolder.OptionsFile.WriteAllText(GetOptionsData())
        End If
    End Sub

    Private Sub SaveItems()
        If IsGameJoltSave = True Then
            GameJoltTempStoreString.Add("saveStorageV" & GamejoltSave.VERSION & "|" & Core.GameJoltSave.GameJoltID & "|items", GetItemsData())
        Else
            _userSaveFolder.ItemsFile.WriteAllText(GetItemsData())
        End If
    End Sub

    Private Sub SaveBerries()
        If IsGameJoltSave = True Then
            GameJoltTempStoreString.Add("saveStorageV" & GamejoltSave.VERSION & "|" & Core.GameJoltSave.GameJoltID & "|berries", GetBerriesData())
        Else
            _userSaveFolder.BerriesFile.WriteAllText(GetBerriesData())
        End If
    End Sub

    Private Sub SaveApricorns()
        If IsGameJoltSave = True Then
            GameJoltTempStoreString.Add("saveStorageV" & GamejoltSave.VERSION & "|" & Core.GameJoltSave.GameJoltID & "|apricorns", GetApricornsData())
        Else
            _userSaveFolder.ApricornsFile.WriteAllText(GetApricornsData())
        End If
    End Sub

    Private Sub SaveDaycare()
        If IsGameJoltSave = True Then
            GameJoltTempStoreString.Add("saveStorageV" & GamejoltSave.VERSION & "|" & Core.GameJoltSave.GameJoltID & "|daycare", GetDaycareData())
        Else
            _userSaveFolder.DaycareFile.WriteAllText(GetDaycareData())
        End If
    End Sub

    Private Sub SavePokedex()
        If IsGameJoltSave = True Then
            GameJoltTempStoreString.Add("saveStorageV" & GamejoltSave.VERSION & "|" & Core.GameJoltSave.GameJoltID & "|pokedex", GetPokedexData())
        Else
            _userSaveFolder.PokedexFile.WriteAllText(GetPokedexData())
        End If
    End Sub

    Private Sub SaveRegister()
        If IsGameJoltSave = True Then
            GameJoltTempStoreString.Add("saveStorageV" & GamejoltSave.VERSION & "|" & Core.GameJoltSave.GameJoltID & "|register", GetRegisterData())
        Else
            _userSaveFolder.RegisterFile.WriteAllText(GetRegisterData())
        End If
    End Sub

    Private Sub SaveItemData()
        If IsGameJoltSave = True Then
            GameJoltTempStoreString.Add("saveStorageV" & GamejoltSave.VERSION & "|" & Core.GameJoltSave.GameJoltID & "|itemdata", GetItemDataData())
        Else
            _userSaveFolder.ItemDataFile.WriteAllText(GetItemDataData())
        End If
    End Sub

    Private Sub SaveBoxData()
        If IsGameJoltSave = True Then
            GameJoltTempStoreString.Add("saveStorageV" & GamejoltSave.VERSION & "|" & Core.GameJoltSave.GameJoltID & "|box", GetBoxData())
        Else
            _userSaveFolder.BoxFile.WriteAllText(GetBoxData())
        End If
    End Sub

    Private Sub SaveNPCData()
        If IsGameJoltSave = True Then
            GameJoltTempStoreString.Add("saveStorageV" & GamejoltSave.VERSION & "|" & Core.GameJoltSave.GameJoltID & "|npc", GetNPCDataData())
        Else
            _userSaveFolder.NPCFile.WriteAllText(GetNPCDataData())
        End If
    End Sub

    Private Sub SaveHallOfFameData()
        If IsGameJoltSave = True Then
            GameJoltTempStoreString.Add("saveStorageV" & GamejoltSave.VERSION & "|" & Core.GameJoltSave.GameJoltID & "|halloffame", GetHallOfFameData())
        Else
            _userSaveFolder.HallOfFameFile.WriteAllText(GetHallOfFameData())
        End If
    End Sub

    Private Sub SaveSecretBaseData()
        If IsGameJoltSave = True Then
            GameJoltTempStoreString.Add("saveStorageV" & GamejoltSave.VERSION & "|" & Core.GameJoltSave.GameJoltID & "|secretbase", GetSecretBaseData())
        Else
            _userSaveFolder.SecretBaseFile.WriteAllText(GetSecretBaseData())
        End If
    End Sub

    Private Sub SaveRoamingPokemonData()
        If IsGameJoltSave = True Then
            GameJoltTempStoreString.Add("saveStorageV" & GamejoltSave.VERSION & "|" & Core.GameJoltSave.GameJoltID & "|roamingpokemon", GetRoamingPokemonData())
        Else
            _userSaveFolder.RoamingPokemonFile.WriteAllText(GetRoamingPokemonData())
        End If
    End Sub

    Private Sub SaveStatistics()
        Statistics = PlayerStatistics.GetData()
        If IsGameJoltSave = True Then
            GameJoltTempStoreString.Add("saveStorageV" & GamejoltSave.VERSION & "|" & Core.GameJoltSave.GameJoltID & "|statistics", GetStatisticsData())
        Else
            _userSaveFolder.StatisticsFile.WriteAllText(GetStatisticsData())
        End If
    End Sub

#End Region

#Region "Heal"

    Public Sub HealParty() Implements IPlayer.HealParty
        For i = 0 To Pokemons.Count - 1
            Pokemons(i).FullRestore()
        Next
    End Sub

    Public Sub HealParty(ByVal Members() As Integer) Implements IPlayer.HealParty
        For Each member As Integer In Members
            If Pokemons.Count - 1 >= member Then
                Pokemons(member).FullRestore()
            End If
        Next
    End Sub

#End Region

#Region "Pokemon"

    Public ReadOnly Property CountFightablePokemon() As Integer Implements IPlayer.CountFightablePokemon
        Get
            Dim i As Integer = 0

            For Each Pokemon As Pokemon In Pokemons
                If Pokemon.Status <> BasePokemon.StatusProblems.Fainted And Pokemon.EggSteps = 0 And Pokemon.HP > 0 Then
                    i += 1
                End If
            Next
            Return i
        End Get
    End Property

    Public ReadOnly Property CanCatchPokémon() As Boolean Implements IPlayer.CanCatchPokémon
        Get
            Dim data() As String = BoxData.ToArray("§")
            If data.Count >= BoxAmount * 30 Then
                Return False
            End If
            Return True
        End Get
    End Property

    Public ReadOnly Property SurfPokemon() As Integer Implements IPlayer.SurfPokemon
        Get
            For i = 0 To Pokemons.Count - 1
                Dim p As Pokemon = Pokemons(i)

                If p.IsEgg() = False Then
                    For Each a As BattleSystem.Attack In p.Attacks
                        If a.Name.ToLower() = "surf" Then
                            Return i
                        End If
                    Next
                End If
            Next
            If GameController.IS_DEBUG_ACTIVE = True Or Core.Player.SandBoxMode = True Then
                Return 0
            Else
                Return -1
            End If
        End Get
    End Property

    Public Function GetWalkPokemon() As BasePokemon Implements IPlayer.GetWalkPokemon
        If Pokemons.Count = 0 Then
            Return Nothing
        End If

        For i = 0 To Pokemons.Count - 1
            If Pokemons(i).Status <> BasePokemon.StatusProblems.Fainted And Pokemons(i).IsEgg() = False Then
                Return Pokemons(i)
            End If
        Next
        Return Nothing
    End Function

    Public Function GetValidPokemonCount() As Integer Implements IPlayer.GetValidPokemonCount
        Dim c As Integer = 0
        For Each p As Pokemon In Core.Player.Pokemons
            If p.Status <> BasePokemon.StatusProblems.Fainted And p.EggSteps = 0 Then
                c += 1
            End If
        Next
        Return c
    End Function

#End Region

#Region "Steps"

    '===STEP EVENT INFORMATION===
    'Events when taking a step	| Priority	| Event Type    | Resolution if Not fired
    '---------------------------|-----------|---------------|--------------------------------------------------------------------------------
    'ScriptBlock trigger		| 0		    | ScriptBlock	| Always fire!
    'Trainer Is in sight		| 1		    | Script		| Ignore, will be activated when walked by on a different tile. Design failure.
    'Egg hatches			    | 2		    | Screen change	| Will happen On Next Step automatically.
    'Repel wears out			| 3		    | Script		| Add one Step To the repel counter, so the Event happens On the Next Step.
    'Wild Pokémon appears		| 4		    | WildPokemon	| Just ignore, random Event
    'Pokegear call			    | 5		    | Script		| Just ignore, Not too important
    '----------------------------------------------------------------------------------------------------------------------------------------
    'All Script Events need a special check condition set.
    'Script Blocks are handled externally.
    '
    'Additional things to do that always fire:
    ' - Set the player's LastPosition
    ' - Add to the daycare cycle, if it finishes, do daycare events, add to the friendship value of Pokémon, add points and check or following pokemon pickup.
    ' - Apply shaders to following pokemon and player, and make following pokemon visible
    ' - make wild Pokémon noises
    ' - add to the Temp map step count
    ' - track the statistic for walked steps.

    Private _stepEventStartedTrainer As Boolean = False
    Private _stepEventRepelMessage As Boolean = False
    Private _stepEventEggHatched As Boolean = False

    Public Sub TakeStep(ByVal stepAmount As Integer) Implements IPlayer.TakeStep
        _stepEventEggHatched = False
        _stepEventRepelMessage = False
        _stepEventStartedTrainer = False

        If IsFlying = False Then
            'Set the last position:
            Temp.LastPosition = Screen.Camera.Position

            'Increment step counters:
            Screen.Level.WalkedSteps += 1
            Temp.MapSteps += 1
            DaycareSteps += stepAmount
            PlayerStatistics.Track("Steps taken", stepAmount)

            'Daycare cycle:
            PlayerTemp.DayCareCycle -= stepAmount
            If PlayerTemp.DayCareCycle <= 0 Then
                Daycare.EggCircle()

                'Every 256 steps, add friendship to the Pokémon in the player's team.
                For Each p As Pokemon In Pokemons
                    If p.Status <> BasePokemon.StatusProblems.Fainted And p.IsEgg() = False Then
                        p.ChangeFriendShip(BasePokemon.FriendShipCauses.Walking)
                    End If
                Next

                AddPoints(1, "Completed an Egg Circle.")

                PokemonInteractions.CheckForRandomPickup()
            End If

            'Apply shaders and set following pokemon:
            Screen.Level.OwnPlayer.ApplyShaders()
            Screen.Level.OverworldPokemon.ApplyShaders()

            Screen.Level.OverworldPokemon.ChangeRotation()
            Screen.Level.OverworldPokemon.MakeVisible()

            'Make wild pokemon noises:
            MakeWildPokemonNoise()

            StepEventCheckTrainers()
            StepEventCheckEggHatching(stepAmount)
            StepEventCheckRepel(stepAmount)
            StepEventWildPokemon()
            StepEventPokegearCall()
        Else
            IsFlying = False
        End If
    End Sub

    Private Sub StepEventCheckTrainers()
        If CanFireStepEvent() = True Then
            Screen.Level.CheckTrainerSights()
            If Core.CurrentScreen.Identification = Screen.Identifications.OverworldScreen Then
                If CType(Core.CurrentScreen, OverworldScreen).ActionScript.IsReady = False Then
                    _stepEventStartedTrainer = True
                End If
            End If
        End If
    End Sub

    Private Sub StepEventCheckEggHatching(ByVal stepAmount As Integer)
        If CanFireStepEvent() = True Then
            Dim addEggSteps As Integer = stepAmount
            For Each p As Pokemon In Pokemons
                If p.Ability.Name.ToLower() = "magma armor" Or p.Ability.Name.ToLower() = "flame body" Then
                    addEggSteps *= Core.Random.Next(1, 4)
                    Exit For
                End If
            Next

            Dim eggsReady As New List(Of Pokemon)
            For Each p As Pokemon In Pokemons
                If p.EggSteps > 0 Then
                    p.EggSteps += addEggSteps
                    If p.EggSteps >= p.BaseEggSteps Then
                        eggsReady.Add(p)
                    End If
                End If
            Next

            If eggsReady.Count > 0 Then
                For Each p As Pokemon In eggsReady
                    Pokemons.Remove(p)
                Next

                Core.SetScreen(New TransitionScreen(Core.CurrentScreen, New HatchEggScreen(Core.CurrentScreen, eggsReady), Color.White, False))

                _stepEventEggHatched = True
            End If
        End If
    End Sub

    Private Sub StepEventCheckRepel(ByVal stepAmount As Integer)
        If RepelSteps > 0 Then
            RepelSteps -= stepAmount

            If RepelSteps <= 0 Then
                If Core.CurrentScreen.Identification = Screen.Identifications.OverworldScreen Then
                    If CanFireStepEvent() = True Then
                        Screen.Level.WalkedSteps = 0

                        Dim s As String = "version=2" & vbNewLine &
                                        "@Text.Show(Your repel effect wore off.)" & vbNewLine &
                                        ":end"


                        If Temp.LastUsedRepel > -1 Then
                            Dim haveItemLeft As Boolean = Inventory.GetItemAmount(Temp.LastUsedRepel) > 0

                            If haveItemLeft = True Then
                                s = "version=2" & vbNewLine &
                                    "@Text.Show(Your repel effect wore off.*Do you want to use~another <inventory.name(" & Temp.LastUsedRepel & ")>?)" & vbNewLine &
                                    "@Options.Show(Yes,No)" & vbNewLine &
                                    ":when:Yes" & vbNewLine &
                                    "@sound.play(repel_use)" & vbNewLine &
                                    "@Text.Show(<player.name> used~a <inventory.name(" & Temp.LastUsedRepel & ")>.)" & vbNewLine &
                                    "@item.repel(" & Temp.LastUsedRepel & ")" & vbNewLine &
                                    "@item.remove(" & Temp.LastUsedRepel & ",1,0)" & vbNewLine &
                                    ":endwhen" & vbNewLine &
                                    ":end"
                            End If
                        End If

                        CType(Core.CurrentScreen, OverworldScreen).ActionScript.StartScript(s, 2, False)
                        _stepEventRepelMessage = True
                    Else
                        _repelSteps = 1
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub StepEventWildPokemon()
        If CanFireStepEvent() = True Then
            If Screen.Level.WildPokemonFloor = True And Screen.Level.IsSurfing = False Then
                Screen.Level.PokemonEncounter.TryEncounterWildPokemon(Screen.Camera.Position, EncounterMethods.Land, "")
            End If
        End If
    End Sub

    Private Sub StepEventPokegearCall()
        If CanFireStepEvent() = True Then
            If Temp.MapSteps > 0 Then
                If Temp.LastCall < 256 Then
                    Temp.LastCall += 1
                Else
                    If Core.Random.Next(0, 700) = 0 Then
                        GameJolt.PokegearScreen.RandomCall()
                        Temp.LastCall = 0
                    End If
                End If
            End If
        End If
    End Sub

    Private Function CanFireStepEvent() As Boolean
        If ScriptBlock.TriggeredScriptBlock = False Then
            If _stepEventStartedTrainer = False Then
                If _stepEventEggHatched = False Then
                    If _stepEventRepelMessage = False Then
                        If Screen.Level.PokemonEncounterData.EncounteredPokemon = False Then
                            Return True
                        End If
                    End If
                End If
            End If
        End If
        Return False
    End Function

    Private Sub MakeWildPokemonNoise()
        If Screen.Level.WildPokemonGrass = True Then
            If Core.Random.Next(0, 193) = 0 Then
                Dim p As Pokemon = Spawner.GetPokemon(Screen.Level.LevelFile, EncounterMethods.Land, False, "")

                If Not p Is Nothing Then
                    PlayWildPokemonNoise(p.Number)
                End If
            End If
        End If
        If Screen.Level.WildPokemonFloor = True Then
            If Core.Random.Next(0, 193) = 0 Then
                Dim p As Pokemon = Spawner.GetPokemon(Screen.Level.LevelFile, EncounterMethods.Land, False, "")

                If Not p Is Nothing Then
                    PlayWildPokemonNoise(p.Number)
                    Exit Sub
                End If
            End If
        End If
        If Screen.Level.WildPokemonWater = True Then
            If Core.Random.Next(0, 193) = 0 Then
                Dim p As Pokemon = Spawner.GetPokemon(Screen.Level.LevelFile, EncounterMethods.Surfing, False, "")

                If Not p Is Nothing Then
                    PlayWildPokemonNoise(p.Number)
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub PlayWildPokemonNoise(ByVal number As Integer)
        SoundEffectManager.PlayPokemonCry(number, Core.Random.Next(0, 6) / 10.0F, Core.Random.Next(0, 20) / 10.0F - 1, SoundEffectManager.Volume * 0.35F)
    End Sub

#End Region

    Public Sub AddVisitedMap(ByVal mapFile As String) Implements IPlayer.AddVisitedMap
        Dim maps As List(Of String) = VisitedMaps.Split(CChar(",")).ToList()

        If maps.Contains(mapFile) = False Then
            maps.Add(mapFile)
            VisitedMaps = ""
            For Each map As String In maps
                If VisitedMaps <> "" Then
                    VisitedMaps &= ","
                End If
                VisitedMaps &= map
            Next
        End If
    End Sub

    Public Sub AddPoints(ByVal amount As Integer, ByVal reason As String) Implements IPlayer.AddPoints
        Dim addPoints As Integer = amount

        For Each mysteryEvent As MysteryEventScreen.MysteryEvent In MysteryEventScreen.ActivatedMysteryEvents
            If mysteryEvent.EventType = MysteryEventScreen.EventTypes.PointsMultiplier Then
                addPoints = CInt(addPoints * Single.Parse(mysteryEvent.Value, NumberFormatInfo.InvariantInfo))
            End If
        Next

        If IsGameJoltSave = True Then
            If API.UserBanned(Core.GameJoltSave.GameJoltID) = False Then
                Core.GameJoltSave.Points += addPoints
            End If
        Else
            Points += addPoints
        End If

        HistoryScreen.HistoryHandler.AddHistoryItem("Obtained game points", "Amount: " & addPoints.ToString(NumberFormatInfo.InvariantInfo) & "; Reason: " & reason, False, False)
    End Sub

    Public Sub ResetNewLevel() Implements IPlayer.ResetNewLevel
        lastLevel = 0
        displayEmblemDelay = 0.0F
        emblemPositionX = Core.WindowSize.Width
    End Sub

    Dim lastLevel As Integer = 0
    Dim displayEmblemDelay As Single = 0.0F
    Dim emblemPositionX As Integer = Core.WindowSize.Width
    Private _startMap As String = "barktown.dat"
    Private _pokemons As New List(Of BasePokemon)
    Private _showBattleAnimations As Integer = 2
    Private _inventory As IPlayerInventory = New PlayerInventory()
    Private _difficultyMode As Integer
    Private _tempSurfSkin As String = "Hilbert"
    Private _texture As Texture2D
    Private _opacity As Single
    Private _lastPokemonPosition As Vector3 = New Vector3(999, 999, 999)
    Private _cameraDistance As Single
    Private _loadedSave As Boolean
    Private _playTime As TimeSpan
    Private _gameStart As Date
    Private _startPosition As Vector3 = New Vector3(14, 0.1, 10)
    Private _startThirdPerson As Boolean
    Private _startRotationSpeed As Double = 12
    Private _startFov As Single = 45.0F
    Private _startRotation As Single
    Private _startFreeCameraMode As Boolean
    Private _doAnimation As Boolean
    Private _skinName As String
    Private _startSurfing As Boolean
    Private _tempRideSkin As String = ""
    Private _pokedexes As New List(Of BasePokedex)
    Private _badges As New List(Of Integer)
    Private _pokeFiles As New List(Of String)
    Private _showModelsInBattle As Boolean = True
    Private _playerTemp As New PlayerTemp()
    Private _earnedAchievements As New List(Of String)
    Private _isFlying As Boolean
    Private _diagonalMovement As Boolean
    Private _battleStyle As Integer
    Private _mails As New List(Of MailItem.MailData)
    Private _boxAmount As Integer = 10
    Private _serversId As Integer
    Private _gameJoltId As String
    Private _busyType As Integer
    Private _position As Vector3
    Private _initialized As Boolean
    Private _pokemonPosition As Vector3
    Private _pokemonSkin As String
    Private _pokemonVisible As Boolean
    Private _pokemonFacing As Integer
    Private _facing As Integer
    Private _levelFile As String
    Private _moving As Boolean
    Private _usingGameJoltTexture As Boolean
    Private _statistics As String = ""

    Public Sub DrawLevelUp() Implements IPlayer.DrawLevelUp
        If IsGameJoltSave = True Then
            If lastLevel <> Emblem.GetPlayerLevel(Core.GameJoltSave.Points) And lastLevel <> 0 Then
                lastLevel = Emblem.GetPlayerLevel(Core.GameJoltSave.Points)
                displayEmblemDelay = 35.0F
                Skin = Emblem.GetPlayerSpriteFile(lastLevel, Core.GameJoltSave.GameJoltID, Core.GameJoltSave.Gender)
            End If

            If displayEmblemDelay > 0.0F Then
                displayEmblemDelay -= 0.1F
                If displayEmblemDelay <= 6.4F Then
                    If emblemPositionX < Core.WindowSize.Width Then
                        emblemPositionX += 8
                    End If
                Else
                    If emblemPositionX > Core.WindowSize.Width - 512 Then
                        emblemPositionX -= 8
                    End If
                End If

                Emblem.Draw(API.username, Core.GameJoltSave.GameJoltID, Core.GameJoltSave.Points, Core.GameJoltSave.Gender, Core.GameJoltSave.EmblemS, New Vector2(emblemPositionX, 0), 4, Core.GameJoltSave.DownloadedSprite)

                If displayEmblemDelay <= 0.0F Then
                    displayEmblemDelay = 0.0F
                    emblemPositionX = Core.WindowSize.Width
                End If
            End If
        End If
    End Sub

    Public Shared Function IsSaveGameFolder(ByVal folder As String) As Boolean
        If IO.Directory.Exists(folder) = True Then
            Dim files() As String = {"Apricorns", "Berries", "Box", "Daycare", "HallOfFame", "ItemData", "Items", "NPC", "Options", "Party", "Player", "Pokedex", "Register", "RoamingPokemon", "SecretBase"}
            For Each file As String In files
                If IO.File.Exists(folder & "\" & file & ".dat") = False Then
                    Return False
                End If
            Next
            Return True
        End If
        Return False
    End Function

    Public ReadOnly Property IsRunning As Boolean Implements IPlayer.IsRunning
        Get
            If KeyBoardHandler.KeyDown(Keys.LeftShift) = True Or ControllerHandler.ButtonDown(Buttons.B) = True Then
                If Screen.Level.IsRiding = False And Screen.Level.IsSurfing = False And Inventory.HasRunningShoes = True Then
                    Return True
                End If
            End If

            Return False
        End Get
    End Property

    Public Sub Unload() Implements IPlayer.Unload
        'This function clears all data from the loaded player and restores the default values.

        If loadedSave = True Then
            'Clearning lists:
            Pokemons.Clear()
            Pokedexes.Clear()
            Inventory.Clear()
            Badges.Clear()
            PokeFiles.Clear()
            EarnedAchievements.Clear()
            PokegearModules.Clear()
            PhoneContacts.Clear()
            Mails.Clear()
            Trophies.Clear()

            'Restore default values:
            Name = "<playername>"
            RivalName = ""
            Male = True
            Money = 0
            PlayTime = TimeSpan.Zero
            GameStart = Date.Now
            OT = "00000"
            Points = 0
            BP = 0
            Coins = 0
            HasPokedex = False
            HasPokegear = False
            ShowBattleAnimations = 2
            BoxAmount = 10
            LastRestPlace = "yourroom.dat"
            LastRestPlacePosition = "1,0.1,3"
            LastSavePlace = "yourroom.dat"
            LastSavePlacePosition = "1,0.1,3"
            DiagonalMovement = False
            RepelSteps = 0
            DifficultyMode = 0
            BattleStyle = 0
            ShowModelsInBattle = True
            SaveCreated = "Pre 0.21"
            LastPokemonPosition = New Vector3(999)
            DaycareSteps = 0
            GameMode = "Pokemon 3D"
            VisitedMaps = ""
            TempSurfSkin = "Hilbert"
            TempRideSkin = ""
            GTSStars = 8
            SandBoxMode = False
            Statistics = ""
            startPosition = New Vector3(14, 0.1, 10)
            startRotation = 0
            startFreeCameraMode = False
            startMap = "barktown.dat"
            startFOV = 45.0F
            startRotationSpeed = 12
            startThirdPerson = False
            startSurfing = False
            startRiding = False
            Skin = "Hilbert"

            'Clear temp save data:
            RegisterData = ""
            BerryData = ""
            PokedexData = ""
            ItemData = ""
            BoxData = ""
            NPCData = ""
            ApricornData = ""
            SecretBaseData = ""
            DaycareData = ""
            HallOfFameData = ""
            RoamingPokemonData = ""

            filePrefix = "nilllzz"
            newFilePrefix = ""
            AutosaveUsed = False
            loadedSave = False

            IsGameJoltSave = False
            EmblemBackground = "standard"

            ResetNewLevel()
        End If
    End Sub

    Public Property startMap As String Implements IPlayer.startMap
        Get
            Return _startMap
        End Get
        Set
            _startMap = Value
        End Set
    End Property

    Public Property Pokemons As List(Of BasePokemon) Implements IPlayer.Pokemons
        Get
            Return _pokemons
        End Get
        Set
            _pokemons = Value
        End Set
    End Property

    Public Property ShowBattleAnimations As Integer Implements IPlayer.ShowBattleAnimations
        Get
            Return _showBattleAnimations
        End Get
        Set
            _showBattleAnimations = Value
        End Set
    End Property

    Public Property Inventory As IPlayerInventory Implements IPlayer.Inventory
        Get
            Return _inventory
        End Get
        Set
            _inventory = Value
        End Set
    End Property

    Public Property DifficultyMode As Integer Implements IPlayer.DifficultyMode
        Get
            Return _difficultyMode
        End Get
        Set
            _difficultyMode = Value
        End Set
    End Property

    Public Property TempSurfSkin As String Implements IPlayer.TempSurfSkin
        Get
            Return _tempSurfSkin
        End Get
        Set
            _tempSurfSkin = Value
        End Set
    End Property

    Public Property Texture As Texture2D Implements IPlayer.Texture
        Get
            Return _texture
        End Get
        Set
            _texture = Value
        End Set
    End Property

    Public Property Opacity As Single Implements IPlayer.Opacity
        Get
            Return _opacity
        End Get
        Set
            _opacity = Value
        End Set
    End Property

    Public Property LastPokemonPosition As Vector3 Implements IPlayer.LastPokemonPosition
        Get
            Return _lastPokemonPosition
        End Get
        Set
            _lastPokemonPosition = Value
        End Set
    End Property

    Public Property CameraDistance As Single Implements IPlayer.CameraDistance
        Get
            Return _cameraDistance
        End Get
        Set
            _cameraDistance = Value
        End Set
    End Property

    Public Property loadedSave As Boolean Implements IPlayer.loadedSave
        Get
            Return _loadedSave
        End Get
        Set
            _loadedSave = Value
        End Set
    End Property

    Public Property PlayTime As TimeSpan Implements IPlayer.PlayTime
        Get
            Return _playTime
        End Get
        Set
            _playTime = Value
        End Set
    End Property

    Public Property GameStart As Date Implements IPlayer.GameStart
        Get
            Return _gameStart
        End Get
        Set
            _gameStart = Value
        End Set
    End Property

    Public Property startPosition As Vector3 Implements IPlayer.startPosition
        Get
            Return _startPosition
        End Get
        Set
            _startPosition = Value
        End Set
    End Property

    Public Property startThirdPerson As Boolean Implements IPlayer.startThirdPerson
        Get
            Return _startThirdPerson
        End Get
        Set
            _startThirdPerson = Value
        End Set
    End Property

    Public Property startRotationSpeed As Double Implements IPlayer.startRotationSpeed
        Get
            Return _startRotationSpeed
        End Get
        Set
            _startRotationSpeed = Value
        End Set
    End Property

    Public Property startFOV As Single Implements IPlayer.startFOV
        Get
            Return _startFov
        End Get
        Set
            _startFov = Value
        End Set
    End Property

    Public Property startRotation As Single Implements IPlayer.startRotation
        Get
            Return _startRotation
        End Get
        Set
            _startRotation = Value
        End Set
    End Property

    Public Property startFreeCameraMode As Boolean Implements IPlayer.startFreeCameraMode
        Get
            Return _startFreeCameraMode
        End Get
        Set
            _startFreeCameraMode = Value
        End Set
    End Property

    Public Property DoAnimation As Boolean Implements IPlayer.DoAnimation
        Get
            Return _doAnimation
        End Get
        Set
            _doAnimation = Value
        End Set
    End Property

    Public Property SkinName As String Implements IPlayer.SkinName
        Get
            Return _skinName
        End Get
        Set
            _skinName = Value
        End Set
    End Property

    Public Property startSurfing As Boolean Implements IPlayer.startSurfing
        Get
            Return _startSurfing
        End Get
        Set
            _startSurfing = Value
        End Set
    End Property


    Public Property startRiding As Boolean Implements IPlayer.startRiding

    Public Property TempRideSkin As String Implements IPlayer.TempRideSkin
        Get
            Return _tempRideSkin
        End Get
        Set
            _tempRideSkin = Value
        End Set
    End Property

    Public Property Pokedexes As List(Of BasePokedex) Implements IPlayer.Pokedexes
        Get
            Return _pokedexes
        End Get
        Set
            _pokedexes = Value
        End Set
    End Property

    Public Property Badges As List(Of Integer) Implements IPlayer.Badges
        Get
            Return _badges
        End Get
        Set
            _badges = Value
        End Set
    End Property

    Public Property PokeFiles As List(Of String) Implements IPlayer.PokeFiles
        Get
            Return _pokeFiles
        End Get
        Set
            _pokeFiles = Value
        End Set
    End Property

    Public Property ShowModelsInBattle As Boolean Implements IPlayer.ShowModelsInBattle
        Get
            Return _showModelsInBattle
        End Get
        Set
            _showModelsInBattle = Value
        End Set
    End Property

    Public Property PlayerTemp As PlayerTemp Implements IPlayer.PlayerTemp
        Get
            Return _playerTemp
        End Get
        Set
            _playerTemp = Value
        End Set
    End Property

    Public Property EarnedAchievements As List(Of String) Implements IPlayer.EarnedAchievements
        Get
            Return _earnedAchievements
        End Get
        Set
            _earnedAchievements = Value
        End Set
    End Property

    Public Property IsFlying As Boolean Implements IPlayer.IsFlying
        Get
            Return _isFlying
        End Get
        Set
            _isFlying = Value
        End Set
    End Property

    Public Property DiagonalMovement As Boolean Implements IPlayer.DiagonalMovement
        Get
            Return _diagonalMovement
        End Get
        Set
            _diagonalMovement = Value
        End Set
    End Property

    Public Property BattleStyle As Integer Implements IPlayer.BattleStyle
        Get
            Return _battleStyle
        End Get
        Set
            _battleStyle = Value
        End Set
    End Property

    Public Property Mails As List(Of MailItem.MailData) Implements IPlayer.Mails
        Get
            Return _mails
        End Get
        Set
            _mails = Value
        End Set
    End Property

    Public Property BoxAmount As Integer Implements IPlayer.BoxAmount
        Get
            Return _boxAmount
        End Get
        Set
            _boxAmount = Value
        End Set
    End Property

    Public Property ServersID As Integer Implements IPlayer.ServersID
        Get
            Return _serversId
        End Get
        Set
            _serversId = Value
        End Set
    End Property

    Public Property GameJoltId As String Implements IPlayer.GameJoltId
        Get
            Return _gameJoltId
        End Get
        Set
            _gameJoltId = Value
        End Set
    End Property

    Public Property BusyType As Integer Implements IPlayer.BusyType
        Get
            Return _busyType
        End Get
        Set
            _busyType = Value
        End Set
    End Property

    Public Property Position As Vector3 Implements IPlayer.Position
        Get
            Return _position
        End Get
        Set
            _position = Value
        End Set
    End Property

    Public Property Initialized As Boolean Implements IPlayer.Initialized
        Get
            Return _initialized
        End Get
        Set
            _initialized = Value
        End Set
    End Property

    Public Property PokemonPosition As Vector3 Implements IPlayer.PokemonPosition
        Get
            Return _pokemonPosition
        End Get
        Set
            _pokemonPosition = Value
        End Set
    End Property

    Public Property PokemonSkin As String Implements IPlayer.PokemonSkin
        Get
            Return _pokemonSkin
        End Get
        Set
            _pokemonSkin = Value
        End Set
    End Property

    Public Property PokemonVisible As Boolean Implements IPlayer.PokemonVisible
        Get
            Return _pokemonVisible
        End Get
        Set
            _pokemonVisible = Value
        End Set
    End Property

    Public Property PokemonFacing As Integer Implements IPlayer.PokemonFacing
        Get
            Return _pokemonFacing
        End Get
        Set
            _pokemonFacing = Value
        End Set
    End Property

    Public Property Facing As Integer Implements IPlayer.Facing
        Get
            Return _facing
        End Get
        Set
            _facing = Value
        End Set
    End Property

    Public Property LevelFile As String Implements IPlayer.LevelFile
        Get
            Return _levelFile
        End Get
        Set
            _levelFile = Value
        End Set
    End Property

    Public Property Moving As Boolean Implements IPlayer.Moving
        Get
            Return _moving
        End Get
        Set
            _moving = Value
        End Set
    End Property

    Public Property UsingGameJoltTexture As Boolean Implements IPlayer.UsingGameJoltTexture
        Get
            Return _usingGameJoltTexture
        End Get
        Set
            _usingGameJoltTexture = Value
        End Set
    End Property

    Public Property Statistics As String Implements IPlayer.Statistics
        Get
            Return _statistics
        End Get
        Set
            _statistics = Value
        End Set
    End Property

    Public Sub ChangeTexture() Implements IPlayer.ChangeTexture
        Throw New NotImplementedException
    End Sub

    Public Sub SetTexture(tempSurfSkin As String, [boolean] As Boolean) Implements IPlayer.SetTexture
        Throw New NotImplementedException
    End Sub

    Public Function ApplyShaders() As String Implements IPlayer.ApplyShaders
        Throw New NotImplementedException
    End Function

    Public Sub UpdateEntity() Implements IPlayer.UpdateEntity
        Throw New NotImplementedException
    End Sub

    Public Sub ApplyNewData(package As IPackage) Implements IPlayer.ApplyNewData
        Throw New NotImplementedException
    End Sub
End Class