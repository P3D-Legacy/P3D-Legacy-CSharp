﻿Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Entities
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.Resources.Sound
Imports P3D.Legacy.Core.Screens
Imports P3D.Legacy.Core.Security
Imports P3D.Legacy.Core.World
Imports PCLExt.FileStorage

Public Class SmashRock

    Inherits Entity

    Public Overrides Sub ClickFunction()
        If Screen.TextBox.Showing = False Then
            Dim pName As String = ""

            For Each p As Pokemon In Core.Player.Pokemons
                If p.IsEgg() = False Then
                    For Each a As BattleSystem.Attack In p.Attacks
                        If a.Name.ToLower() = ("Rock Smash").ToLower() Then
                            pName = p.GetDisplayName()
                            Exit For
                        End If
                    Next
                End If

                If pName <> "" Then
                    Exit For
                End If
            Next

            Dim text As String = "This rock looks like~it can be broken!"

            If pName <> "" Or GameController.IS_DEBUG_ACTIVE = True Or Core.Player.SandBoxMode = True Then
                text &= "~Do you want to~use Rock Smash?%Yes|No%"
            End If

            Screen.TextBox.Show(text, {Me})
            SoundManager.PlaySound("select")
        End If
    End Sub

    Public Overrides Sub ResultFunction(Result As Integer)
        If Result = 0 Then
            Dim pName As String = ""

            For Each p As Pokemon In Core.Player.Pokemons
                If p.IsEgg() = False Then
                    For Each a As BattleSystem.Attack In p.Attacks
                        If a.Name.ToLower() = ("Rock Smash").ToLower() Then
                            pName = p.GetDisplayName()
                            Exit For
                        End If
                    Next
                End If

                If pName <> "" Then
                    Exit For
                End If
            Next

            Dim spawnedPokemon As Pokemon = Nothing
            If Core.Random.Next(0, 100) < 20 Then
                spawnedPokemon = Spawner.GetPokemon(Screen.Level.LevelFile, EncounterMethods.RockSmash, False)
                If spawnedPokemon Is Nothing Then
                    Dim s As String = "version=2" & vbNewLine &
                        "@text.show(" & pName & " used~Rock Smash!)" & vbNewLine &
                        "@sound.play(destroy)" & vbNewLine &
                        ":end"
                    CType(Core.CurrentScreen, OverworldScreen).ActionScript.StartScript(s, 2)
                Else
                    Dim s As String = "version=2" & vbNewLine &
                        "@text.show(" & pName & " used~Rock Smash!)" & vbNewLine &
                        "@sound.play(destroy)" & vbNewLine &
                        "@level.update" & vbNewLine &
                        "@text.show(A wild Pokémon~appeared!)" & vbNewLine &
                        "@battle.wild(" & spawnedPokemon.Number & "," & spawnedPokemon.Level & ")" & vbNewLine &
                        ":end"
                    CType(Core.CurrentScreen, OverworldScreen).ActionScript.StartScript(s, 2)
                End If
            Else
                If Core.Random.Next(0, 100) < 20 Then
                    Dim ItemID As Integer = GetItemID()
                    Dim s As String = "version=2" & vbNewLine &
                        "@text.show(" & pName & " used~Rock Smash!)" & vbNewLine &
                        "@sound.play(destroy)" & vbNewLine &
                        "@level.update" & vbNewLine &
                        "@item.give(" & ItemID & ",1)" & vbNewLine &
                        "@item.messagegive(" & ItemID & ",1)" & vbNewLine &
                        ":end"
                    CType(Core.CurrentScreen, OverworldScreen).ActionScript.StartScript(s, 2)
                Else
                    Dim s As String = "version=2" & vbNewLine &
                        "@text.show(" & pName & " used~Rock Smash!)" & vbNewLine &
                        "@sound.play(destroy)" & vbNewLine &
                        ":end"
                    CType(Core.CurrentScreen, OverworldScreen).ActionScript.StartScript(s, 2)
                End If
            End If
            PlayerStatistics.Track("Rock Smash used", 1)

            Me.CanBeRemoved = True
        End If
    End Sub

    Private Function GetItemID() As Integer
        Dim MatchingContainers As New List(Of ItemContainer)
        Dim Chances As New List(Of Integer)
        For Each c As ItemContainer In ItemContainerlist
            If c.MapFile.ToLower() = Screen.Level.LevelFile.ToLower() Then
                MatchingContainers.Add(c)
                Chances.Add(c.Chance)
            End If
        Next
        If MatchingContainers.Count = 0 Then
            Return 190
        End If

        Return MatchingContainers(GetRandomChance(Chances)).ItemID
    End Function

    Private Class ItemContainer

        Public ItemID As Integer = 190
        Public Chance As Integer = 0
        Public MapFile As String = ""

        Public Sub New(ByVal MapFile As String, ByVal Data As String)
            Me.MapFile = MapFile
            '{ID,Chance}
            Data = Data.Remove(Data.Length - 1, 1).Remove(0, 1)
            Dim DataArray() As String = Data.Split(CChar(","))
            Me.ItemID = CInt(DataArray(0))
            Me.Chance = CInt(DataArray(1))
        End Sub

    End Class

    Private Shared ItemContainerlist As New List(Of ItemContainer)

    Public Shared Sub Load()
        ItemContainerlist.Clear()
        Dim File As IFile = GameModeManager.GetContentFile("Data\smashrockitems.dat").Result
        If System.IO.File.Exists(File) = True Then
            FileValidation.CheckFileValid(File, False, "SmashRock.vb")
            Dim data() As String = System.IO.File.ReadAllLines(File)
            For Each line As String In data
                Dim Linedata() As String = line.Split(CChar("|"))
                Dim Mapfile As String = Linedata(0)
                For i = 1 To Linedata.Length - 1
                    ItemContainerlist.Add(New ItemContainer(Mapfile, Linedata(i)))
                Next
            Next
        End If
    End Sub

    Public Overrides Sub UpdateEntity()
        MyBase.UpdateEntity()

        If Rotation.Y <> Screen.Camera.Yaw Then
            Me.Rotation.Y = Screen.Camera.Yaw
            Me.CreatedWorld = False
        End If
    End Sub

    Public Overrides Sub Render()
        Me.Draw(Me.Model, Me.Textures, False)
    End Sub

End Class