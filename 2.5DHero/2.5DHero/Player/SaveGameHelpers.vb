Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.GameJolt
Imports P3D.Legacy.Core.GameJolt.Profiles

''' <summary>
''' A class that helps with saving the game.
''' </summary>
Public Class SaveGameHelpers

    Shared _savingProgress As Integer = 0
    Shared _failedSaveParts As Integer = 0
    Shared _tempGJSave As GamejoltSave = Nothing
    Shared _validatedDownloadCheck As Boolean = False
    Shared _startedDownloadCheck As Boolean = False

    ''' <summary>
    ''' If the game encountered any errors while uploading data.
    ''' </summary>
    Public Shared ReadOnly Property EncounteredErrors() As Boolean
        Get
            Return _failedSaveParts > 0
        End Get
    End Property

    ''' <summary>
    ''' If the download check has already started.
    ''' </summary>
    Public Shared ReadOnly Property StartedDownloadCheck() As Boolean
        Get
            Return _startedDownloadCheck
        End Get
    End Property

    ''' <summary>
    ''' Increments the value that counts the upload parts and starts the download check if the upload is finished.
    ''' </summary>
    Public Shared Sub AddGameJoltSaveCounter(ByVal result As String)
        _savingProgress += 1

        Dim resultList As List(Of API.JoltValue) = API.HandleData(result)
        For Each lEntry In resultList
            If lEntry.Value.ToLower() <> "true" Then
                _failedSaveParts += 1
            End If
        Next

        If _failedSaveParts = 0 And _savingProgress >= GamejoltSave.EXTRADATAUPLOADCOUNT + GamejoltSave.SAVEFILECOUNT Then
            PerformDownloadCheck()
        End If
    End Sub

    ''' <summary>
    ''' Increments the counter once the data files have been uploaded and starts the download check if the upload is finished.
    ''' </summary>
    Public Shared Sub CompleteGameJoltSave(ByVal result As String)
        _savingProgress += GamejoltSave.SAVEFILECOUNT

        Dim resultList As List(Of API.JoltValue) = API.HandleData(result)
        For Each lEntry In resultList
            If lEntry.Value.ToLower() <> "true" Then
                _failedSaveParts += 1
            End If
        Next

        If _failedSaveParts = 0 And _savingProgress >= GamejoltSave.EXTRADATAUPLOADCOUNT + GamejoltSave.SAVEFILECOUNT Then
            PerformDownloadCheck()
        End If
    End Sub

    ''' <summary>
    ''' If saving the GameJolt save to the GameJolt server is finished (with checks).
    ''' </summary>
    Public Shared ReadOnly Property GameJoltSaveDone() As Boolean
        Get
            If _tempGJSave Is Nothing Then
                Return False
            End If
            If _tempGJSave.DownloadFailed = True Then
                If _failedSaveParts = 0 Then
                    _failedSaveParts += 1
                End If
                Return True
            End If
            If _tempGJSave.DownloadFinished = True Then
                If _validatedDownloadCheck = False Then
                    ValidateDownloadCheck()
                End If
            End If
            Return _savingProgress >= GamejoltSave.EXTRADATAUPLOADCOUNT + GamejoltSave.SAVEFILECOUNT And _validatedDownloadCheck = True
        End Get
    End Property

    ''' <summary>
    ''' Resets the temporary variables used to save the game.
    ''' </summary>
    Public Shared Sub ResetSaveCounter()
        _savingProgress = 0
        _failedSaveParts = 0
        _tempGJSave = Nothing
        _startedDownloadCheck = False
        _validatedDownloadCheck = False
    End Sub

    ''' <summary>
    ''' Starts the download check.
    ''' </summary>
    Private Shared Sub PerformDownloadCheck()
        _startedDownloadCheck = True

        _tempGJSave = New GamejoltSave()
        _tempGJSave.DownloadSave(Core.GameJoltSave.GameJoltID, False)
    End Sub

    ''' <summary>
    ''' Validates the results of the download check.
    ''' </summary>
    Private Shared Sub ValidateDownloadCheck()
        'Checks all data downloaded against current save data:
        If _tempGJSave.Apricorns = Core.Player.ApricornData And
            _tempGJSave.Berries = Core.Player.BerryData And
            _tempGJSave.Box = Core.Player.BoxData And
            _tempGJSave.Daycare = Core.Player.DaycareData And
            _tempGJSave.HallOfFame = Core.Player.HallOfFameData And
            _tempGJSave.ItemData = Core.Player.ItemData And
            _tempGJSave.Items = Core.Player.GetItemsData() And
            _tempGJSave.NPC = Core.Player.NPCData And
            _tempGJSave.Options = Core.Player.GetOptionsData() And
            _tempGJSave.Party = Core.Player.GetPartyData() And
            GetTimeFixedPlayerData(_tempGJSave.Player) = GetTimeFixedPlayerData(Core.Player.GetPlayerData(False)) And
            _tempGJSave.Pokedex = Core.Player.PokedexData And
            _tempGJSave.Register = Core.Player.RegisterData And
            _tempGJSave.RoamingPokemon = Core.Player.RoamingPokemonData And
            _tempGJSave.SecretBase = Core.Player.SecretBaseData And
            _tempGJSave.Statistics = Core.Player.Statistics Then
            _validatedDownloadCheck = True
            Exit Sub
        End If
        _failedSaveParts += 1
        _validatedDownloadCheck = True
    End Sub

    ''' <summary>
    ''' We convert the string saved in "PlayTime" to "xxxxxxxxxx" here because some time passed between starting to save the game and finishing the test.
    ''' The "PlayTime" variable would count up during this time, making it different from the time that got saved to the server.
    ''' To work around this, we replace both sides with some generic string.
    ''' This makes "PlayTime" the only part of the entire save that could get corrupted and passed on undetected (I believe this is impossible to even happen).
    ''' </summary>
    ''' <param name="input">The player data.</param>
    Private Shared Function GetTimeFixedPlayerData(ByVal input As String) As String
        Dim inputArr As String() = input.Split({vbNewLine}, StringSplitOptions.None)
        Dim output As String = ""

        For Each l As String In inputArr
            Dim inputLine As String = l
            If inputLine.StartsWith("PlayTime|") = True Then
                inputLine = "PlayTime|xxxxxxxxxx"
            End If
            If output <> "" Then
                output &= vbNewLine
            End If
            output &= inputLine
        Next

        Return output
    End Function

End Class
