Imports net.Pokemon3D.Game.ScriptVersion2

Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Battle
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.Security
Imports PCLExt.FileStorage

Public Class Trainer
    Inherits BaseTrainer

    Public Function IsBeaten() As Boolean
        Return ActionScript.IsRegistered("trainer_" & TrainerFile)
    End Function

    Public Shared Function IsBeaten(ByVal CheckTrainerFile As String) As Boolean
        Return ActionScript.IsRegistered("trainer_" & CheckTrainerFile)
    End Function

    Public Sub New()
    End Sub

    Public Sub New(ByVal TrainerFile As String)
        Me.TrainerFile = TrainerFile

        Dim file = GameModeManager.GetScriptFileAsync("Trainer\" & TrainerFile & ".trainer").Result
        FileValidation.CheckFileValid(file, False, "Trainer.vb")

        Dim Data() As String = file.ReadAllTextAsync().Result.SplitAtNewline()

        If Data(0) = "[TRAINER FORMAT]" Then
            LoadTrainer(Data)
        Else
            LoadTrainerLegacy(Data)
        End If
    End Sub

    Protected Overrides Sub LoadTrainer(ByVal Data() As String)
        Dim PokeLines As New List(Of String)
        Dim isDoubleTrainerValid As Integer = 0
        Dim vsdata As String = "blue"
        Dim bardata As String = "blue"

        For Each line As String In Data
            If line.Contains("|") = True Then
                Dim pointer As String = line.Remove(line.IndexOf("|"))
                Dim value As String = line.Remove(0, line.IndexOf("|") + 1)

                Select Case pointer.ToLower()
                    Case "name"
                        Me.Name = ScriptCommander.Parse(value).ToString()
                        If Me.Name.Contains(",") = True Then
                            Me.Name2 = Me.Name.GetSplit(1)
                            Me.Name = Me.Name.GetSplit(0)

                            isDoubleTrainerValid += 1
                        End If
                    Case "trainerclass"
                        Me.TrainerType = ScriptCommander.Parse(value).ToString()
                        If Me.TrainerType.Contains(",") = True Then
                            Me.TrainerType2 = TrainerType.GetSplit(1)
                            Me.TrainerType = TrainerType.GetSplit(0)

                            isDoubleTrainerValid += 1
                        End If
                    Case "money"
                        Me.Money = CInt(ScriptCommander.Parse(value).ToString())
                    Case "intromessage"
                        Me.IntroMessage = ScriptCommander.Parse(value).ToString()
                    Case "outromessage"
                        Me.OutroMessage = ScriptCommander.Parse(value).ToString()
                        If Me.OutroMessage.Contains("|") = True Then
                            Me.OutroMessage2 = OutroMessage.GetSplit(1, "|")
                            Me.OutroMessage = OutroMessage.GetSplit(0, "|")

                            isDoubleTrainerValid += 1
                        End If
                    Case "defeatmessage"
                        Me.DefeatMessage = ScriptCommander.Parse(value).ToString()
                    Case "textureid"
                        Me.SpriteName = ScriptCommander.Parse(value).ToString()
                        If Me.SpriteName.Contains(",") = True Then
                            Me.SpriteName2 = Me.SpriteName.GetSplit(1)
                            Me.SpriteName = Me.SpriteName.GetSplit(0)

                            isDoubleTrainerValid += 1
                        End If
                    Case "region"
                        Me.Region = ScriptCommander.Parse(value).ToString()
                    Case "inimusic"
                        Me.IniMusic = ScriptCommander.Parse(value).ToString()
                    Case "defeatmusic"
                        Me.DefeatMusic = ScriptCommander.Parse(value).ToString()
                    Case "battlemusic"
                        Me.BattleMusic = ScriptCommander.Parse(value).ToString()
                    Case "insightmusic"
                        Me.InSightMusic = ScriptCommander.Parse(value).ToString()
                    Case "pokemon1", "pokemon2", "pokemon3", "pokemon4", "pokemon5", "pokemon6"
                        If value <> "" Then
                            PokeLines.Add(value)
                        End If
                    Case "items"
                        If value <> "" Then
                            Dim itemData() As String = ScriptCommander.Parse(value).ToString().Split(CChar(","))
                            For Each ItemID As String In itemData
                                Items.Add(Item.GetItemById(CInt(ItemID)))
                            Next
                        End If
                    Case "gender"
                        Dim GenderInt As Integer = CInt(ScriptCommander.Parse(value).ToString())

                        Me.Gender = CInt(MathHelper.Clamp(GenderInt, -1, 1))
                    Case "ai"
                        Me.AILevel = CInt(ScriptCommander.Parse(value).ToString())
                    Case "introsequence"
                        value = ScriptCommander.Parse(value).ToString()

                        If value.Contains(",") = True Then
                            vsdata = value.GetSplit(0)
                            bardata = value.GetSplit(1)
                        Else
                            vsdata = value
                        End If
                    Case "introtype"
                        Me.IntroType = CInt(ScriptCommander.Parse(value).ToString())
                End Select
            End If
        Next

        For Each PokeLine As String In PokeLines
            Dim PokeData As String = PokeLine.GetSplit(1, "|")
            If PokeData <> "" Then
                If ScriptCommander.Parse(PokeData).ToString().StartsWith("{") = True Then
                    PokeData = ScriptCommander.Parse(PokeData).ToString().Replace("§", ",")
                End If
                If PokeData.StartsWith("{") = True And PokeData.EndsWith("}") = True Then
                    Dim p As Pokemon = Pokemon.GetPokemonByData(PokeData)

                    If Core.Player.DifficultyMode > 0 Then
                        Dim level As Integer = p.Level

                        Dim addLevel As Integer = 0
                        If Core.Player.DifficultyMode = 1 Then
                            addLevel = CInt(Math.Ceiling(level / 10))
                        ElseIf Core.Player.DifficultyMode = 2 Then
                            addLevel = CInt(Math.Ceiling(level / 5))
                        End If

                        While level + addLevel > p.Level
                            p.LevelUp(False)
                            p.Experience = p.NeedExperience(p.Level)
                        End While
                        p.HP = p.MaxHP
                    End If

                    Pokemons.Add(p)
                Else
                    Dim firstPart As String = ""
                    Dim secondPart As String = ""
                    Dim endedFirstPart As Boolean = False
                    Dim readData As String = PokeData
                    Dim openTag As Boolean = False

                    While readData.Length > 0
                        Select Case readData(0).ToString()
                            Case "<"
                                openTag = True
                            Case ">"
                                openTag = False
                            Case ","
                                If openTag = False Then
                                    endedFirstPart = True
                                End If
                        End Select

                        If readData(0).ToString() <> "," Or openTag = True Then
                            If endedFirstPart = True Then
                                secondPart &= readData(0).ToString()
                            Else
                                firstPart &= readData(0).ToString()
                            End If
                        End If

                        readData = readData.Remove(0, 1)
                    End While

                    Dim ID As Integer = ScriptConversion.ToInteger(ScriptCommander.Parse(firstPart))
                    Dim Level As Integer = ScriptConversion.ToInteger(ScriptCommander.Parse(secondPart))

                    Dim addLevel As Integer = 0
                    If Core.Player.DifficultyMode = 1 Then
                        addLevel = CInt(Math.Ceiling(Level / 10))
                    ElseIf Core.Player.DifficultyMode = 2 Then
                        addLevel = CInt(Math.Ceiling(Level / 5))
                    End If
                    Level += addLevel

                    Dim maxLevel As Integer = GameModeManager.GetActiveGameRuleValueOrDefault("MaxLevel", 100)

                    If Level > maxLevel Then
                        Level = maxLevel
                    End If

                    Dim p As Pokemon = Nothing

                    If FrontierTrainer > -1 Then
                        p = FrontierSpawner.GetPokemon(Level, FrontierTrainer, Nothing)
                    Else
                        p = Pokemon.GetPokemonByID(ID)
                        p.Generate(Level, True)
                    End If

                    If p.IsGenderless = False Then
                        Select Case Me.Gender
                            Case 0
                                If p.IsMale > 0.0F Then
                                    p.Gender = BasePokemon.Genders.Male
                                End If
                            Case 1
                                If p.IsMale < 100.0F Then
                                    p.Gender = BasePokemon.Genders.Female
                                End If
                        End Select
                    End If

                    Pokemons.Add(p)
                End If
            End If
        Next

        If isDoubleTrainerValid = 4 Then
            Me.DoubleTrainer = True
        End If

        SetIniImage(vsdata, bardata)
        FrontierTrainer = -1
    End Sub

End Class