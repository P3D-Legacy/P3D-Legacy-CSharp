Imports System.Globalization
Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Server

Namespace Servers

    Public Class OnlinePlayer
        Inherits BaseOnlinePlayer
        
        Private _gameVersion As String = ""
        Private _isGameJoltPlayer As Boolean = False

        Private _decimalSeparator As String = ","
        Private _gameMode As String = ""


        Public Overrides Sub ApplyNewData(ByVal p As IPackage)
            '---General information---
            '0: Active gamemode
            '1: isgamejoltsave
            '2: GameJoltID
            '3: DecimalSeparator

            '---Player Information---
            '4: playername
            '5: levelfile
            '6: position
            '7: facing
            '8: moving
            '9: skin
            '10: busytype

            '---OverworldPokemon---
            '11: Visible
            '12: Position
            '13: Skin
            '14: facing

            Dim d() As String = p.DataItems.ToArray()

            For i = 0 To PLAYERDATAITEMSCOUNT - 1
                Dim value As String = d(i)
                If value <> "" Then
                    Select Case i
                        Case 0 '0: Active gamemode
                            Me._gameMode = value
                        Case 1 '1: isgamejoltsave
                            Me._isGameJoltPlayer = CBool(value)
                        Case 2 '2: GameJoltID
                            Me.GameJoltId = value
                        Case 3 '3: DecimalSeparator
                            Me._decimalSeparator = value
                        Case 4 '4: playername
                            Me.Name = value
                        Case 5 '5: levelfile
                            Me.LevelFile = value
                        Case 6 '6: position
                            ' TODO: Check
                            Dim posString As String = value.Replace(Me._decimalSeparator, GameController.DecSeparator)
                            Dim posList() As String = posString.Split(CChar("|"))

                            Me.Position = New Vector3(Single.Parse(posList(0), NumberFormatInfo.InvariantInfo), Single.Parse(posList(1), NumberFormatInfo.InvariantInfo), Single.Parse(posList(2), NumberFormatInfo.InvariantInfo))
                        Case 7 '7: facing
                            Me.Facing = CInt(value)
                        Case 8 '8: moving
                            Me.Moving = CBool(value)
                        Case 9 '9: skin
                            Me.Skin = CStr(value)
                        Case 10 '10: busytype
                            Me.BusyType = CInt(value)
                        Case 11 '11: Visible
                            Me.PokemonVisible = CBool(value)
                        Case 12 '12: Position
                            Dim posString As String = value.Replace(Me._decimalSeparator, GameController.DecSeparator)
                            Dim posList() As String = posString.Split(CChar("|"))

                            Me.PokemonPosition = New Vector3(Single.Parse(posList(0), NumberFormatInfo.InvariantInfo), Single.Parse(posList(1), NumberFormatInfo.InvariantInfo), Single.Parse(posList(2), NumberFormatInfo.InvariantInfo))
                        Case 13 '13: Skin
                            Me.PokemonSkin = value
                        Case 14 '14: facing
                            Me.PokemonFacing = CInt(value)
                    End Select
                End If
            Next

            Me.Initialized = True
            Core.ServersManager.PlayerManager.NeedsUpdate = True
        End Sub

    End Class

End Namespace