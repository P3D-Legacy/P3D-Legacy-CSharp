Imports System.Drawing
Imports System.Globalization

Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.Resources.Managers
Imports P3D.Legacy.Core.Resources.Sound
Imports P3D.Legacy.Core.Screens
Imports P3D.Legacy.Core.Security

''' <summary>
''' Represents a Pokémon.
''' </summary>
Public Class Pokemon
    Inherits BasePokemon

    Private Class PokemonManagerImplementation
        Inherits PokemonManager

        ''' <summary>
        ''' Returns a new Pokémon class instance.
        ''' </summary>
        ''' <param name="Number">The number of the Pokémon in the national BasePokemon.</param>
        Public Overrides Function GetPokemonByID(ByVal Number As Integer) As BasePokemon
            Return GetPokemonByID(Number, "")
        End Function

        Public Overrides Function GetPokemonByID(ByVal Number As Integer, ByVal AdditionalData As String) As BasePokemon
            Dim p As New Pokemon()
            p.LoadDefinitions(Number, AdditionalData)
            Return p
        End Function

        ''' <summary>
        ''' Returns a new Pokémon class instance defined by data.
        ''' </summary>
        ''' <param name="InputData">The data that defines the Pokémon.</param>
        Public Overrides Function GetPokemonByData(ByVal InputData As String) As BasePokemon
            Dim Tags As New Dictionary(Of String, String)
            Dim Data() As String = InputData.Split(CChar("}"))
            For Each Tag As String In Data
                If Tag.Contains("{") = True And Tag.Contains("[") = True Then
                    Dim TagName As String = Tag.Remove(0, 2)
                    TagName = TagName.Remove(TagName.IndexOf(""""))

                    Dim TagContent As String = Tag.Remove(0, Tag.IndexOf("[") + 1)
                    TagContent = TagContent.Remove(TagContent.IndexOf("]"))

                    If Tags.ContainsKey(TagName) = False Then
                        Tags.Add(TagName, TagContent)
                    End If
                End If
            Next

            Dim NewAdditionalData As String = ""
            If Tags.ContainsKey("AdditionalData") = True Then
                NewAdditionalData = CStr(Tags("AdditionalData"))
            End If

            Dim PokemonID As Integer = 10
            If Tags.ContainsKey("Pokemon") = True Then
                PokemonID = CInt(Tags("Pokemon"))
            End If

            Dim p As Pokemon = GetPokemonByID(PokemonID, NewAdditionalData)
            p.LoadData(InputData)

            Return p
        End Function

    End Class

    Shared Sub New()
        PM = New PokemonManagerImplementation()
    End Sub



#Region "Temp"

    ''' <summary>
    ''' Resets the temp storages of the Pokémon.
    ''' </summary>
    Public Overrides Sub ResetTemp()
        Volatiles.Clear()

        StatAttack = 0
        StatDefense = 0
        StatSpAttack = 0
        StatSpDefense = 0
        StatSpeed = 0

        Accuracy = 0
        Evasion = 0

        If _originalNumber > -1 Then
            Me.Number = _originalNumber
            _originalNumber = -1
        End If

        If Not _originalType1 Is Nothing Then
            Me.Type1 = _originalType1
            _originalType1 = Nothing
        End If

        If Not _originalType2 Is Nothing Then
            Me.Type2 = _originalType2
            _originalType2 = Nothing
        End If

        If _originalStats(0) > -1 Then
            Me.Attack = _originalStats(0)
            _originalStats(0) = -1
        End If
        If _originalStats(1) > -1 Then
            Me.Defense = _originalStats(1)
            _originalStats(1) = -1
        End If
        If _originalStats(2) > -1 Then
            Me.SpAttack = _originalStats(2)
            _originalStats(2) = -1
        End If
        If _originalStats(3) > -1 Then
            Me.SpDefense = _originalStats(3)
            _originalStats(3) = -1
        End If
        If _originalStats(4) > -1 Then
            Me.Speed = _originalStats(4)
            _originalStats(4) = -1
        End If

        If _originalShiny > -1 Then
            If _originalShiny = 0 Then
                Me.IsShiny = False
            ElseIf _originalShiny = 1 Then
                Me.IsShiny = True
            End If
            Me._originalShiny = -1
        End If

        If Not Me._originalMoves Is Nothing Then
            Me.Attacks.Clear()
            For Each a As BattleSystem.Attack In Me._originalMoves
                Me.Attacks.Add(a)
            Next
            Me._originalMoves = Nothing
        End If

        If Not Me._originalAbility Is Nothing Then
            Me.Ability = Me._originalAbility
            Me._originalAbility = Nothing
        End If

        'If Not Me._originalItem Is Nothing Then
        '    Me.Item = P3D.Legacy.Core.Pokemon.Item.GetItemById(Me._originalItem.Id)
        '    Me.Item.AdditionalData = Me._originalItem.AdditionalData
        '    Me._originalItem = Nothing
        'End If

        Me.IsTransformed = False

        Me.CalculateStats()
    End Sub

    'Just use these subs when doing/reverting mega evolutions.
    Public NormalAbility As BaseAbility = New Abilities.Stench
    Public Overrides Sub LoadAltAbility()
        NormalAbility = Ability
        Me.Ability = NewAbilities(0)
    End Sub
    Public Overrides Sub RestoreAbility()
        Me.Ability = NormalAbility
    End Sub

#End Region


#Region "Constructors and Data Handlers"

    ''' <summary>
    ''' Creates a new instance of the Pokémon class.
    ''' </summary>
    Friend Sub New()
        MyBase.New()
        Me.ClearTextures()
    End Sub

    ''' <summary>
    ''' Checks if a requested Pokémon data file exists.
    ''' </summary>
    Public Shared Function PokemonDataExists(ByVal Number As Integer) As Boolean
        ' TODO
        Return System.IO.File.Exists(GameModeManager.GetPokemonDataFile(Number.ToString(NumberFormatInfo.InvariantInfo) & ".dat").Path)
    End Function

    ''' <summary>
    ''' Loads definition data from the data files and empties the temp textures.
    ''' </summary>
    Public Overrides Sub ReloadDefinitions()
        Me.LoadDefinitions(Me.Number, Me.AdditionalData)
        Me.ClearTextures()
    End Sub

    ''' <summary>
    ''' Loads definition data from the data file.
    ''' </summary>
    ''' <param name="Number">The number of the Pokémon in the national Pokédex.</param>
    ''' <param name="AdditionalData">The additional data.</param>
    Public Overrides Sub LoadDefinitions(ByVal Number As Integer, ByVal AdditionalData As String)
        Dim path As String = PokemonForms.GetPokemonDataFile(Number, AdditionalData)
        FileValidation.CheckFileValid(path, False, "Pokemon.vb")
        NewAbilities.Clear()

        Dim Data() As String = System.IO.File.ReadAllLines(path)

        For Each Line As String In Data
            Dim VarName As String = Line.GetSplit(0, "|")
            Dim Value As String = Line.GetSplit(1, "|")

            Select Case VarName.ToLower()
                Case "name"
                    Me.Name = Value
                Case "number"
                    Me.Number = CInt(Value)
                Case "baseexperience"
                    Me.BaseExperience = CInt(Value)
                Case "experiencetype"
                    Select Case CInt(Value)
                        Case 0
                            Me.ExperienceType = ExperienceTypes.Fast
                        Case 1
                            Me.ExperienceType = ExperienceTypes.MediumFast
                        Case 2
                            Me.ExperienceType = ExperienceTypes.MediumSlow
                        Case 3
                            Me.ExperienceType = ExperienceTypes.Slow
                    End Select
                Case "type1"
                    Me.Type1 = New Element(Value)
                Case "type2"
                    Me.Type2 = New Element(Value)
                Case "catchrate"
                    Me.CatchRate = CInt(Value)
                Case "basefriendship"
                    Me.BaseFriendship = CInt(Value)
                Case "egggroup1"
                    Me.EggGroup1 = ConvertIDToEggGroup(Value)
                Case "egggroup2"
                    Me.EggGroup2 = ConvertIDToEggGroup(Value)
                Case "baseeggsteps"
                    Me.BaseEggSteps = CInt(Value)
                Case "ismale"
                    'If Value.Contains(".") Then
                    'Value = Value.Replace(".", ",")
                    'End If
                    Me.IsMale = Decimal.Parse(Value, NumberFormatInfo.InvariantInfo)
                Case "isgenderless"
                    Me.IsGenderless = CBool(Value)
                Case "devolution"
                    Me.Devolution = CInt(Value)
                Case "ability1", "ability"
                    If Value <> "Nothing" Then
                        Me.NewAbilities.Add(net.Pokemon3D.Game.Ability.GetAbilityByID(CInt(Value)))
                    End If
                Case "ability2"
                    If Value <> "Nothing" Then
                        Me.NewAbilities.Add(net.Pokemon3D.Game.Ability.GetAbilityByID(CInt(Value)))
                    End If
                Case "hiddenability"
                    If Value <> "Nothing" Then
                        Me.HiddenAbility = net.Pokemon3D.Game.Ability.GetAbilityByID(CInt(Value))
                    End If
                Case "machines"
                    If Value <> "" Then
                        If Value.Contains(",") = True Then
                            Dim MachinesValue() As String = Value.Split(CChar(","))
                            For i = 0 To MachinesValue.Length - 1
                                If IsNumeric(MachinesValue(i)) = True Then
                                    Me.Machines.Add(CInt(MachinesValue(i)))
                                End If
                            Next
                        Else
                            If IsNumeric(Value) = True Then
                                If CInt(Value) = -1 Then
                                    Me.Machines.Clear()
                                    Me.CanLearnAllMachines = True
                                Else
                                    Me.Machines.Add(CInt(Value))
                                End If
                            End If
                        End If
                    End If
                Case "eggmoves"
                    If Value <> "" Then
                        If Value.Contains(",") = True Then
                            Dim EggMoveValues() As String = Value.Split(CChar(","))
                            For i = 0 To EggMoveValues.Length - 1
                                EggMoves.Add(CInt(EggMoveValues(i)))
                            Next
                        Else
                            EggMoves.Add(CInt(Value))
                        End If
                    End If
                Case "tutormoves"
                    If Value <> "" Then
                        If Value.Contains(",") = True Then
                            Dim TutorValue() As String = Value.Split(CChar(","))
                            For i = 0 To TutorValue.Length - 1
                                TutorAttacks.Add(BattleSystem.Attack.GetAttackByID(CInt(TutorValue(i))))
                            Next
                        Else
                            TutorAttacks.Add(BattleSystem.Attack.GetAttackByID(CInt(Value)))
                        End If
                    End If
                Case "eggpokemon"
                    Me.EggPokemon = CInt(Value)
                Case "canbreed"
                    Me.CanBreed = CBool(Value)
                Case "basehp"
                    Me.BaseHP = CInt(Value)
                Case "baseattack"
                    Me.BaseAttack = CInt(Value)
                Case "basedefense"
                    Me.BaseDefense = CInt(Value)
                Case "basespattack"
                    Me.BaseSpAttack = CInt(Value)
                Case "basespdefense"
                    Me.BaseSpDefense = CInt(Value)
                Case "basespeed"
                    Me.BaseSpeed = CInt(Value)
                Case "fphp"
                    Me.GiveEVHP = CInt(Value)
                Case "fpattack"
                    Me.GiveEVAttack = CInt(Value)
                Case "fpdefense"
                    Me.GiveEVDefense = CInt(Value)
                Case "fpspattack"
                    Me.GiveEVSpAttack = CInt(Value)
                Case "fpspdefense"
                    Me.GiveEVSpDefense = CInt(Value)
                Case "fpspeed"
                    Me.GiveEVSpeed = CInt(Value)
                Case "canswim"
                    Me.CanSwim = CBool(Value)
                Case "canfly"
                    Me.CanFly = CBool(Value)
                Case "pokedex"
                    Dim PokedexData() As String = Value.Split(CChar("\"))
                    Me.PokedexEntry = New PokedexEntry(PokedexData(0), PokedexData(1), Single.Parse(PokedexData(2), NumberFormatInfo.InvariantInfo), Single.Parse(PokedexData(3), NumberFormatInfo.InvariantInfo), New Microsoft.Xna.Framework.Color(CInt(PokedexData(4).GetSplit(0)), CInt(PokedexData(4).GetSplit(1)), CInt(PokedexData(4).GetSplit(2))))
                Case "scale"
                    If Value.Contains(".") Then
                        Value = Value.Replace(".", ",")
                    End If
                    Me.Scale = New Vector3(Single.Parse(Value, NumberFormatInfo.InvariantInfo))
                Case "move"
                    Dim Level As Integer = CInt(Value.GetSplit(0))
                    Dim MoveID As Integer = CInt(Value.GetSplit(1))

                    If AttackLearns.ContainsKey(Level) = True Then
                        While AttackLearns.ContainsKey(Level)
                            Level += 1
                        End While
                    End If

                    Me.AttackLearns.Add(Level, BattleSystem.Attack.GetAttackByID(MoveID))
                Case "evolutioncondition"
                    'Evolution,Type,Argument,Trigger

                    Dim EvolutionData() As String = Value.Split(CChar(","))

                    Dim Evolution As Integer = CInt(EvolutionData(0))
                    Dim Type As String = EvolutionData(1)
                    Dim Argument As String = EvolutionData(2)
                    Dim Trigger As String = EvolutionData(3)

                    Dim EvolutionExists As Boolean = False
                    Dim e As EvolutionCondition = New EvolutionCondition

                    For Each oldE As EvolutionCondition In Me.EvolutionConditions
                        If Evolution = oldE.Evolution Then
                            e = oldE
                            EvolutionExists = True
                        End If
                    Next

                    e.SetEvolution(Evolution)
                    e.AddCondition(Type, Argument, Trigger)

                    If EvolutionExists = False Then
                        EvolutionConditions.Add(e)
                    End If
                Case "item"
                    Dim chance As Integer = CInt(Value.GetSplit(0))
                    Dim itemID As Integer = CInt(Value.GetSplit(1))

                    If Not WildItems.ContainsKey(chance) Then
                        WildItems.Add(chance, itemID)
                    End If
                Case "tradevalue"
                    Me.TradeValue = CInt(Value)
            End Select
        Next

        If Me.EggPokemon = 0 Then
            Me.EggPokemon = Me.Number
        End If

        Dim pAttacks As New SortedDictionary(Of Integer, BattleSystem.Attack)
        For i = 0 To AttackLearns.Count - 1
            pAttacks.Add(AttackLearns.Keys(i), AttackLearns.Values(i))
        Next
        AttackLearns.Clear()
        For i = 0 To pAttacks.Count - 1
            AttackLearns.Add(pAttacks.Keys(i), pAttacks.Values(i))
        Next

        If Me.Devolution = 0 Then
            If Me.EggPokemon > 0 And Me.EggPokemon <> Me.Number Then
                If Me.Number - Me.EggPokemon = 2 Then
                    Me.Devolution = Me.Number - 1
                ElseIf Me.Number - Me.EggPokemon = 1 Then
                    Me.Devolution = Me.EggPokemon
                End If
            End If
        End If

        If AdditionalData = "" Then
            Me.AdditionalData = PokemonForms.GetInitialAdditionalData(Me)
        End If
    End Sub

    ''' <summary>
    ''' Applies data to the Pokémon.
    ''' </summary>
    ''' <param name="InputData">The input data.</param>
    Public Overrides Sub LoadData(ByVal InputData As String)
        Dim loadedHP As Boolean = False

        Dim Tags As New Dictionary(Of String, String)
        Dim Data() As String = InputData.Split(CChar("}"))
        For Each Tag As String In Data
            If Tag.Contains("{") = True And Tag.Contains("[") = True Then
                Dim TagName As String = Tag.Remove(0, 2)
                TagName = TagName.Remove(TagName.IndexOf(""""))

                Dim TagContent As String = Tag.Remove(0, Tag.IndexOf("[") + 1)
                TagContent = TagContent.Remove(TagContent.IndexOf("]"))

                If Tags.ContainsKey(TagName) = False Then
                    Tags.Add(TagName, TagContent)
                End If
            End If
        Next

        For i = 0 To Tags.Count - 1
            Dim tagName As String = Tags.Keys(i)
            Dim tagValue As String = Tags.Values(i)

            Select Case tagName.ToLower()
                Case "experience"
                    Me.Experience = CInt(tagValue)
                Case "gender"
                    Select Case CInt(tagValue)
                        Case 0
                            Me.Gender = Genders.Male
                        Case 1
                            Me.Gender = Genders.Female
                        Case 2
                            Me.Gender = Genders.Genderless
                    End Select
                Case "eggsteps"
                    Me.EggSteps = CInt(tagValue)
                Case "item"
                    If IsNumeric(tagValue) = True Then
                        Me.Item = P3D.Legacy.Core.Pokemon.Item.GetItemById(CInt(tagValue))
                    End If
                Case "itemdata"
                    If Not Me.Item Is Nothing Then
                        Me.Item.AdditionalData = tagValue
                    End If
                Case "nickname"
                    Me.NickName = tagValue
                Case "level"
                    Me.Level = CInt(tagValue).Clamp(1, GameModeManager.GetActiveGameRuleValueOrDefault("MaxLevel", 100))
                Case "ot"
                    Me.OT = tagValue
                Case "ability"
                    Me.Ability = net.Pokemon3D.Game.Ability.GetAbilityByID(CInt(tagValue))
                    Me.NormalAbility = Ability
                Case "status"
                    Select Case tagValue
                        Case "BRN"
                            Me.Status = StatusProblems.Burn
                        Case "PSN"
                            Me.Status = StatusProblems.Poison
                        Case "PRZ"
                            Me.Status = StatusProblems.Paralyzed
                        Case "SLP"
                            Me.Status = StatusProblems.Sleep
                        Case "FNT"
                            Me.Status = StatusProblems.Fainted
                        Case "FRZ"
                            Me.Status = StatusProblems.Freeze
                        Case "BPSN"
                            Me.Status = StatusProblems.BadPoison
                        Case Else
                            Me.Status = StatusProblems.None
                    End Select
                Case "nature"
                    Me.Nature = ConvertIDToNature(CInt(tagValue))
                Case "catchlocation"
                    Me.CatchLocation = tagValue
                Case "catchtrainer"
                    Me.CatchTrainerName = tagValue
                Case "catchball"
                    Me.CatchBall = P3D.Legacy.Core.Pokemon.Item.GetItemById(CInt(tagValue))
                Case "catchmethod"
                    Me.CatchMethod = tagValue
                Case "friendship"
                    Me.Friendship = CInt(tagValue)
                Case "isshiny"
                    Me.IsShiny = CBool(tagValue)
                Case "attack1", "attack2", "attack3", "attack4"
                    If Not net.Pokemon3D.Game.BattleSystem.Attack.ConvertStringToAttack(tagValue) Is Nothing Then
                        Attacks.Add(net.Pokemon3D.Game.BattleSystem.Attack.ConvertStringToAttack(tagValue))
                    End If
                Case "stats"
                    Dim Stats() As String = tagValue.Split(CChar(","))
                    HP = CInt(Stats(0)).Clamp(0, 999)
                    loadedHP = True
                Case "hp"
                    HP = CInt(tagValue)
                    loadedHP = True
                Case "fps", "evs"
                    Dim EVs() As String = tagValue.Split(CChar(","))
                    EVHP = CInt(EVs(0)).Clamp(0, 255)
                    EVAttack = CInt(EVs(1)).Clamp(0, 255)
                    EVDefense = CInt(EVs(2)).Clamp(0, 255)
                    EVSpAttack = CInt(EVs(3)).Clamp(0, 255)
                    EVSpDefense = CInt(EVs(4)).Clamp(0, 255)
                    EVSpeed = CInt(EVs(5)).Clamp(0, 255)
                Case "dvs", "ivs"
                    Dim IVs() As String = tagValue.Split(CChar(","))
                    IVHP = CInt(IVs(0))
                    IVAttack = CInt(IVs(1))
                    IVDefense = CInt(IVs(2))
                    IVSpAttack = CInt(IVs(3))
                    IVSpDefense = CInt(IVs(4))
                    IVSpeed = CInt(IVs(5))
                Case "additionaldata"
                    Me.AdditionalData = tagValue
                Case "idvalue"
                    Me.IndividualValue = tagValue
            End Select
        Next

        If Me.IndividualValue = "" Then
            Me.GenerateIndividualValue()
        End If

        CalculateStats()

        If loadedHP = False Then
            Me.HP = Me.MaxHP
        Else
            Me.HP = Me.HP.Clamp(0, Me.MaxHP)
        End If
    End Sub

    ''' <summary>
    ''' Returns the important save data from the Pokémon to be displayed in the Hall of Fame.
    ''' </summary>
    Public Function GetHallOfFameData() As String
        Dim Data As String = ""
        Dim SaveGender As Integer = 0
        If Me.Gender = Genders.Female Then
            SaveGender = 1
        End If
        If Me.IsGenderless = True Then
            SaveGender = 2
        End If

        Dim shinyString As String = "0"
        If Me.IsShiny = True Then
            shinyString = "1"
        End If

        Data = "{""Pokemon""[" & Me.Number & "]}" &
        "{""Gender""[" & SaveGender & "]}" &
        "{""NickName""[" & Me.NickName & "]}" &
        "{""Level""[" & Me.Level & "]}" &
        "{""OT""[" & Me.OT & "]}" &
        "{""CatchTrainer""[" & Me.CatchTrainerName & "]}" &
        "{""isShiny""[" & shinyString & "]}" &
        "{""AdditionalData""[" & Me.AdditionalData & "]}" &
        "{""IDValue""[" & Me.IndividualValue & "]}"

        Return Data
    End Function

    ''' <summary>
    ''' Returns the save data from the Pokémon.
    ''' </summary>
    Public Overrides Function GetSaveData() As String
        Dim SaveGender As Integer = 0
        If Me.Gender = Genders.Female Then
            SaveGender = 1
        End If
        If Me.IsGenderless = True Then
            SaveGender = 2
        End If

        Dim SaveStatus As String = ""
        Select Case Me.Status
            Case StatusProblems.Burn
                SaveStatus = "BRN"
            Case StatusProblems.Poison
                SaveStatus = "PSN"
            Case StatusProblems.Paralyzed
                SaveStatus = "PRZ"
            Case StatusProblems.Sleep
                SaveStatus = "SLP"
            Case StatusProblems.Fainted
                SaveStatus = "FNT"
            Case StatusProblems.Freeze
                SaveStatus = "FRZ"
            Case StatusProblems.BadPoison
                SaveStatus = "BPSN"
        End Select

        Dim A1 As String = ""
        If Attacks.Count > 0 Then
            If Not Attacks(0) Is Nothing Then
                A1 = Me.Attacks(0).ToString()
            End If
        End If

        Dim A2 As String = ""
        If Attacks.Count > 1 Then
            If Not Attacks(1) Is Nothing Then
                A2 = Me.Attacks(1).ToString()
            End If
        End If

        Dim A3 As String = ""
        If Attacks.Count > 2 Then
            If Not Attacks(2) Is Nothing Then
                A3 = Me.Attacks(2).ToString()
            End If
        End If

        Dim A4 As String = ""
        If Attacks.Count > 3 Then
            If Not Attacks(3) Is Nothing Then
                A4 = Me.Attacks(3).ToString()
            End If
        End If

        Dim SaveItemID As String = "0"
        If Not Me.Item Is Nothing Then
            SaveItemID = Me.Item.Id.ToString(NumberFormatInfo.InvariantInfo)
        End If

        Dim ItemData As String = ""
        If Not Me.Item Is Nothing Then
            ItemData = Me.Item.AdditionalData
        End If

        Dim EVSave As String = Me.EVHP & "," & Me.EVAttack & "," & Me.EVDefense & "," & Me.EVSpAttack & "," & Me.EVSpDefense & "," & Me.EVSpeed
        Dim IVSave As String = Me.IVHP & "," & Me.IVAttack & "," & Me.IVDefense & "," & Me.IVSpAttack & "," & Me.IVSpDefense & "," & Me.IVSpeed

        Dim shinyString As String = "0"
        If Me.IsShiny = True Then
            shinyString = "1"
        End If

        If Me.Ability Is Nothing Then
            Me.Ability = Me.NewAbilities(Core.Random.Next(0, Me.NewAbilities.Count))
        End If

        Dim Data As String = "{""Pokemon""[" & Me.Number & "]}" &
        "{""Experience""[" & Me.Experience & "]}" &
        "{""Gender""[" & SaveGender & "]}" &
        "{""EggSteps""[" & Me.EggSteps & "]}" &
        "{""Item""[" & SaveItemID & "]}" &
        "{""ItemData""[" & ItemData & "]}" &
        "{""NickName""[" & Me.NickName & "]}" &
        "{""Level""[" & Me.Level & "]}" &
        "{""OT""[" & Me.OT & "]}" &
        "{""Ability""[" & Me.Ability.Id & "]}" &
        "{""Status""[" & SaveStatus & "]}" &
        "{""Nature""[" & Me.Nature & "]}" &
        "{""CatchLocation""[" & Me.CatchLocation & "]}" &
        "{""CatchTrainer""[" & Me.CatchTrainerName & "]}" &
        "{""CatchBall""[" & Me.CatchBall.Id & "]}" &
        "{""CatchMethod""[" & Me.CatchMethod & "]}" &
        "{""Friendship""[" & Me.Friendship & "]}" &
        "{""isShiny""[" & shinyString & "]}" &
        "{""Attack1""[" & A1 & "]}" &
        "{""Attack2""[" & A2 & "]}" &
        "{""Attack3""[" & A3 & "]}" &
        "{""Attack4""[" & A4 & "]}" &
        "{""HP""[" & Me.HP & "]}" &
        "{""EVs""[" & EVSave & "]}" &
        "{""IVs""[" & IVSave & "]}" &
        "{""AdditionalData""[" & Me.AdditionalData & "]}" &
        "{""IDValue""[" & Me.IndividualValue & "]}"

        Return Data
    End Function

    ''' <summary>
    ''' Generates a Pokémon's initial values.
    ''' </summary>
    ''' <param name="newLevel">The level to set the Pokémon's level to.</param>
    ''' <param name="SetParameters">If the parameters like Nature and Ability should be set. Otherwise, it just loads the attacks and sets the level.</param>
    Public Overrides Sub Generate(ByVal newLevel As Integer, ByVal SetParameters As Boolean)
        Me.Level = 0

        If SetParameters = True Then
            Me.GenerateIndividualValue()
            Me.AdditionalData = PokemonForms.GetInitialAdditionalData(Me)

            Me.Nature = CType(Core.Random.Next(0, 25), Natures)

            'Synchronize ability:
            If Core.Player.Pokemons.Count > 0 Then
                If Core.Player.Pokemons(0).Ability.Name.ToLower() = "synchronize" Then
                    If Core.Random.Next(0, 100) < 50 Then
                        Me.Nature = Core.Player.Pokemons(0).Nature
                    End If
                End If
            End If

            If Screen.Level IsNot Nothing Then
                If Screen.Level.HiddenAbilityChance > Core.Random.Next(0, 100) And Me.HasHiddenAbility = True Then
                    Me.Ability = net.Pokemon3D.Game.Ability.GetAbilityByID(Me.HiddenAbility.Id)
                Else
                    Me.Ability = net.Pokemon3D.Game.Ability.GetAbilityByID(Me.NewAbilities(Core.Random.Next(0, Me.NewAbilities.Count)).Id)
                End If
            End If

            Dim shinyRate As Integer = 8192

            For Each mysteryEvent As MysteryEventScreen.MysteryEvent In MysteryEventScreen.ActivatedMysteryEvents
                If mysteryEvent.EventType = MysteryEventScreen.EventTypes.ShinyMultiplier Then
                    shinyRate = CInt(shinyRate / Single.Parse(mysteryEvent.Value, NumberFormatInfo.InvariantInfo))
                End If
            Next

            'ShinyCharm
            If Core.Player.Inventory.GetItemAmount(242) > 0 Then
                shinyRate = CInt(shinyRate * 0.75F)
            End If

            If Core.Random.Next(0, shinyRate) = 0 And Pokemon.Legendaries.Contains(Me.Number) = False Then
                Me.IsShiny = True
            End If

            If Me.IsGenderless = True Then
                Me.Gender = Genders.Genderless
            Else
                'Determine if Pokemon is male or female depending on the rate defined in the data file:
                If Core.Random.Next(1, 101) > Me.IsMale Then
                    Me.Gender = Genders.Female
                Else
                    Me.Gender = Genders.Male
                End If

                'Cute Charm ability:
                If Core.Player.Pokemons.Count > 0 Then
                    If Core.Player.Pokemons(0).Gender <> Genders.Genderless And Core.Player.Pokemons(0).Ability.Name.ToLower() = "cute charm" Then
                        If Core.Random.Next(0, 100) < 67 Then
                            If Core.Player.Pokemons(0).Gender = Genders.Female Then
                                Me.Gender = Genders.Male
                            Else
                                Me.Gender = Genders.Female
                            End If
                        Else
                            Me.Gender = Core.Player.Pokemons(0).Gender
                        End If
                    End If
                End If
            End If

            If StartItems.Count > 0 Then
                Dim i As Integer = Core.Random.Next(0, StartItems.Count)
                If Core.Random.Next(0, StartItems.Values(i)) = 0 Then
                    Me.Item = StartItems.Keys(i)
                End If
            End If

            'Set the IV values of the Pokémon randomly, range 0-31.
            Me.IVHP = Core.Random.Next(0, 32)
            Me.IVAttack = Core.Random.Next(0, 32)
            Me.IVDefense = Core.Random.Next(0, 32)
            Me.IVSpAttack = Core.Random.Next(0, 32)
            Me.IVSpDefense = Core.Random.Next(0, 32)
            Me.IVSpeed = Core.Random.Next(0, 32)

            Me.Friendship = Me.BaseFriendship

            If WildItems.Count > 0 Then
                Dim has100 As Boolean = False
                Dim ChosenItemID As Integer = 0
                For i = 0 To WildItems.Count - 1
                    If WildItems.Keys(i) = 100 Then
                        has100 = True
                        ChosenItemID = WildItems.Values(i)
                        Exit For
                    End If
                Next
                If has100 = True Then
                    Me.Item = P3D.Legacy.Core.Pokemon.Item.GetItemById(ChosenItemID)
                Else
                    Dim usedWildItems As Dictionary(Of Integer, Integer) = Me.WildItems

                    'Compound eyes ability:
                    If Core.Player.Pokemons.Count > 0 Then
                        If Core.Player.Pokemons(0).Ability.Name.ToLower() = "compound eyes" Then
                            usedWildItems = Abilities.Compoundeyes.ConvertItemChances(usedWildItems)
                        End If
                    End If

                    For i = 0 To usedWildItems.Count - 1
                        Dim v As Integer = Core.Random.Next(0, 100)
                        If v < usedWildItems.Keys(i) Then
                            ChosenItemID = usedWildItems.Values(i)
                            Exit For
                        End If
                    Next
                    Me.Item = P3D.Legacy.Core.Pokemon.Item.GetItemById(ChosenItemID)
                End If
            End If
        End If

        'Level the Pokémon up and give the appropriate move set for its new level:
        While newLevel > Me.Level
            LevelUp(False)
            Me.Experience = NeedExperience(Me.Level)
        End While

        Dim canLearnMoves As New List(Of BattleSystem.Attack)
        For i = 0 To Me.AttackLearns.Count - 1
            If Me.AttackLearns.Keys(i) <= Me.Level Then

                Dim hasMove As Boolean = False
                For Each m As BattleSystem.Attack In Me.Attacks
                    If m.Id = Me.AttackLearns.Values(i).Id Then
                        hasMove = True
                        Exit For
                    End If
                Next
                If hasMove = False Then
                    For Each m As BattleSystem.Attack In canLearnMoves
                        If m.Id = Me.AttackLearns.Values(i).Id Then
                            hasMove = True
                            Exit For
                        End If
                    Next
                End If

                If hasMove = False Then
                    canLearnMoves.Add(Me.AttackLearns.Values(i))
                End If
            End If
        Next

        If canLearnMoves.Count > 0 Then
            Me.Attacks.Clear()

            Dim startIndex As Integer = canLearnMoves.Count - 4

            If startIndex < 0 Then
                startIndex = 0
            End If

            For t = startIndex To canLearnMoves.Count - 1
                Me.Attacks.Add(canLearnMoves(t))
            Next
        End If

        Me.HP = MaxHP
    End Sub

#End Region


End Class
