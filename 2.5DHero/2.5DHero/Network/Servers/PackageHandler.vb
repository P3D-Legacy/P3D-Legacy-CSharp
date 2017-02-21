﻿Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.Screens
Imports P3D.Legacy.Core.Server
Imports P3D.Legacy.Core.World

Namespace Servers

    Public Class PackageHandler

        Public Shared Sub HandlePackage(ByVal p As Package)
            Select Case p.PackageType
                Case PackageTypes.ChatMessage
                    HandleChatMessage(p)
                Case PackageTypes.PrivateMessage
                    HandlePrivateMessage(p)
                Case PackageTypes.ID
                    HandleID(p)
                Case PackageTypes.Kicked
                    HandleKicked(p)
                Case PackageTypes.GameData, PackageTypes.PlayData 'Note: Only use GameData.
                    HandleGameData(p)
                Case PackageTypes.CreatePlayer
                    HandleCreatePlayer(p)
                Case PackageTypes.DestroyPlayer
                    HandleDestroyPlayer(p)
                Case PackageTypes.ServerClose
                    HandleServerClose(p)
                Case PackageTypes.ServerMessage
                    HandleServerMessage(p)
                Case PackageTypes.WorldData
                    HandleWorldData(p)

                Case PackageTypes.TradeJoin
                    HandleTradeJoin(p)
                Case PackageTypes.TradeQuit
                    HandleTradeQuit(p)
                Case PackageTypes.TradeOffer
                    HandleTradeOffer(p)
                Case PackageTypes.TradeStart
                    HandleTradeStart(p)
                Case PackageTypes.TradeRequest
                    HandleTradeRequest(p)

                Case PackageTypes.BattleJoin
                    HandleBattleJoin(p)
                Case PackageTypes.BattleOffer
                    HandleBattleOffer(p)
                Case PackageTypes.BattleQuit
                    HandleBattleQuit(p)
                Case PackageTypes.BattleRequest
                    HandleBattleRequest(p)
                Case PackageTypes.BattleStart
                    HandleBattleStart(p)
                Case PackageTypes.BattleHostData
                    HandleBattleHostData(p)
                Case PackageTypes.BattleClientData
                    HandleBattleClientData(p)
                Case PackageTypes.BattlePokemonData
                    HandleBattlePokemonData(p)
            End Select
        End Sub

#Region "GameData"

        Private Shared Sub HandleGameData(ByVal p As Package)
            Core.ServersManager.PlayerCollection.ApplyPlayerDataPackage(p)
        End Sub

#End Region

#Region "BaseFunctions"

        Private Shared Sub HandleChatMessage(ByVal p As Package)
            Dim GameJoltID As String = ""
            Dim Player As OnlinePlayer = Core.ServersManager.PlayerCollection.GetPlayer(p.Origin)

            If Not Player Is Nothing Then
                GameJoltID = Player.GameJoltID
            End If

            Chat.AddLine(New Chat.ChatMessage(Core.ServersManager.PlayerCollection.GetPlayerName(p.Origin), p.DataItems(0), GameJoltID, Chat.ChatMessage.MessageTypes.GlobalMessage))
            ChatScreen.ReceivedGlobalMessage()
        End Sub

        Private Shared Sub HandlePrivateMessage(ByVal p As Package)
            If p.Origin = -1 Then
                'Server sent a PM. Convert this to a global message.
                Dim message As New Chat.ChatMessage("[SERVER]", "sent you a message: " & p.DataItems(0), "", Chat.ChatMessage.MessageTypes.GlobalMessage)
                Chat.AddLine(message)
                ChatScreen.ReceivedGlobalMessage()
            ElseIf p.Origin = Core.ServersManager.ID Then
                'Server sent own PM back to the client. Add the PM to the PM history.
                Dim message As New Chat.ChatMessage(Core.Player.Name, p.DataItems(1), "", Chat.ChatMessage.MessageTypes.PMMessage)
                message.PMChatInclude = p.DataItems(0)
                Chat.AddLine(message)
            Else
                'Someone else sent a PM
                Dim GJID As String = ""
                Dim Player As OnlinePlayer = Core.ServersManager.PlayerCollection.GetPlayer(p.Origin)

                If Not Player Is Nothing Then
                    GJID = Player.GameJoltId
                End If

                Dim message = New Chat.ChatMessage(Core.ServersManager.PlayerCollection.GetPlayerName(p.Origin), p.DataItems(0), GJID, Chat.ChatMessage.MessageTypes.PMMessage)
                Chat.AddLine(message)
                ChatScreen.ReceivedPMMessage(message)
            End If
        End Sub

        Private Shared Sub HandleID(ByVal p As Package)
            Core.ServersManager.ID = CInt(p.DataItems(0))
        End Sub

        Private Shared Sub HandleKicked(ByVal p As Package)
            Core.ServersManager.ServerConnection.Disconnect("You got kicked!", "Reason: " & vbNewLine & p.DataItems(0))
        End Sub

        Private Shared Sub HandleCreatePlayer(ByVal p As Package)
            If Core.ServersManager.PlayerCollection.HasPlayer(CInt(p.DataItems(0))) = False Then
                Dim newPlayer As New OnlinePlayer()
                newPlayer.ServersID = CInt(p.DataItems(0))
                Core.ServersManager.PlayerCollection.Add(newPlayer)
                Core.ServersManager.PlayerManager.NeedsUpdate = True
            End If
        End Sub

        Private Shared Sub HandleDestroyPlayer(ByVal p As Package)
            If Core.ServersManager.ID = CInt(p.DataItems(0)) Then
                Logger.Log(Logger.LogTypes.Warning, "Server misidentification: The server tried to destroy your player connection.")
            Else
                ChatScreen.ClosePMChat(Core.ServersManager.PlayerCollection.GetPlayer(CInt(p.DataItems(0))).Name)

                Core.ServersManager.PlayerCollection.RemoveByID(CInt(p.DataItems(0)))
                Core.ServersManager.PlayerManager.NeedsUpdate = True
            End If
        End Sub

        Private Shared Sub HandleServerClose(ByVal p As Package)
            Dim message As String = "The server closed down."
            If p.DataItems.Count > 0 Then
                If p.DataItems(0) <> "" Then
                    message = p.DataItems(0)
                End If
            End If

            Core.ServersManager.ServerConnection.Disconnect("Disconnected from server!", message)
        End Sub

        Private Shared Sub HandleServerMessage(ByVal p As Package)
            Logger.Debug("Got ServerMessage: " & p.DataItems(0))
            JoinServerScreen.AddServerMessage(p.DataItems(0), JoinServerScreen.SelectedServer.IdentifierName)
        End Sub

        Private Shared Sub HandleWorldData(ByVal p As Package)
            World.ServerSeason = CType(p.DataItems(0), SeasonEnum)
            World.ServerWeather = CType(p.DataItems(1), WeatherEnum)
            World.ServerTimeData = p.DataItems(2)
            World.LastServerDataReceived = Date.Now

            Core.ServersManager.PlayerManager.ReceivedWorldData = True
        End Sub

#End Region

#Region "Trades"

        Private Shared Sub HandleTradeJoin(ByVal p As Package)
            DirectTradeScreen.OtherPlayerJoins()
        End Sub

        Private Shared Sub HandleTradeQuit(ByVal p As Package)
            DirectTradeScreen.OtherPlayerQuits()

            GameJolt.PokegearScreen.TradeRequestData = -1
            Dim s As Screen = Core.CurrentScreen
            While Not s.PreScreen Is Nothing And s.Identification <> Screen.Identifications.PokegearScreen
                s = s.PreScreen
            End While

            If s.Identification = Screen.Identifications.PokegearScreen Then
                If CType(s, GameJolt.PokegearScreen).menuIndex = GameJolt.PokegearScreen.MenuScreens.TradeRequest Then
                    Core.SetScreen(s.PreScreen)
                End If
            End If
        End Sub

        Private Shared Sub HandleTradeStart(ByVal p As Package)
            DirectTradeScreen.ReceiveTradeStart()
        End Sub

        Private Shared Sub HandleTradeOffer(ByVal p As Package)
            DirectTradeScreen.ReceiveOfferPokemon(p.DataItems(0))
        End Sub

        Private Shared Sub HandleTradeRequest(ByVal p As Package)
            If GameJolt.PokegearScreen.TradeRequestData = -1 Then
                GameJolt.PokegearScreen.TradeRequestData = p.Origin
            End If
        End Sub

#End Region

#Region "Battle"

        Private Shared Sub HandleBattleJoin(ByVal p As Package)
            PVPLobbyScreen.OtherPlayerJoins()
        End Sub

        Private Shared Sub HandleBattleOffer(ByVal p As Package)
            'Remove the whole "if" when server API is updated.
            If p.DataItems(0).Length = 1 Then
                PVPLobbyScreen.ReceiveBattleStart(CInt(p.DataItems(0)))
            Else
                PVPLobbyScreen.ReceiveOppTeam(p.DataItems(0))
            End If

            'PVPLobbyScreen.ReceiveOppTeam(p.DataItems(0))

        End Sub

        Private Shared Sub HandleBattleQuit(ByVal p As Package)
            PVPLobbyScreen.OtherPlayerQuits()

            GameJolt.PokegearScreen.BattleRequestData = -1
            Dim s As Screen = Core.CurrentScreen
            While Not s.PreScreen Is Nothing And s.Identification <> Screen.Identifications.PokegearScreen
                s = s.PreScreen
            End While

            If s.Identification = Screen.Identifications.PokegearScreen Then
                If CType(s, GameJolt.PokegearScreen).menuIndex = GameJolt.PokegearScreen.MenuScreens.BattleRequest Then
                    Core.SetScreen(s.PreScreen)
                End If
            End If
        End Sub

        Private Shared Sub HandleBattleRequest(ByVal p As Package)
            If GameJolt.PokegearScreen.BattleRequestData = -1 Then
                GameJolt.PokegearScreen.BattleRequestData = p.Origin
            End If
        End Sub

        Private Shared Sub HandleBattleStart(ByVal p As Package)
            PVPLobbyScreen.ReceiveBattleStart(CInt(p.DataItems(0)))
        End Sub

        Private Shared Sub HandleBattleHostData(ByVal p As Package)
            BattleSystem.BattleScreen.ReceiveHostData(p.DataItems(0))
        End Sub

        Private Shared Sub HandleBattleClientData(ByVal p As Package)
            BattleSystem.BattleScreen.ReceiveClientData(p.DataItems(0))
        End Sub

        Private Shared Sub HandleBattlePokemonData(ByVal p As Package)
            BattleSystem.BattleScreen.ReceiveHostEndRoundData(p.DataItems(0))
        End Sub

#End Region

    End Class

End Namespace