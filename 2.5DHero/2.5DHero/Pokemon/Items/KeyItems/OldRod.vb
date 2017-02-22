Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Entities
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.Screens
Imports P3D.Legacy.Core.World

Namespace Items.KeyItems

    <Item(58, "Old Rod")>
    Public Class OldRod

        Inherits KeyItem

        Public Overrides ReadOnly Property Description As String = "An old and beat-up fishing rod. Use it by any body of water to fish for wild aquatic Pokémon."
        Public Overrides ReadOnly Property CanBeUsed As Boolean = True

        Public Sub New()
            TextureRectangle = New Rectangle(240, 48, 24, 24)
        End Sub

        Public Overrides Sub Use()
            If IsInfrontOfWater() = True And Screen.Level.Surfing = False And Screen.Level.Riding = False Then
                Dim s As String = "version=2"

                While Core.CurrentScreen.Identification <> Screen.Identifications.OverworldScreen
                    Core.CurrentScreen = Core.CurrentScreen.PreScreen
                End While

                Dim p As Pokemon = Nothing
                
                Dim pokeFile As String = "poke\" & Screen.Level.LevelFile.Remove(Screen.Level.LevelFile.Length - 4, 4) & ".poke"
                If GameModeManager.MapFileExistsAsync(pokeFile).Result = True Then
                    p = Spawner.GetPokemon(Screen.Level.LevelFile, EncounterMethods.OldRod, False)
                End If

                If p Is Nothing Then
                    p = Pokemon.GetPokemonByID(129)
                    p.Generate(10, True)
                End If

                Dim PokemonID As Integer = p.Number
                Dim PokemonShiny As String = "N"
                If p.IsShiny = True Then
                    PokemonShiny = "S"
                End If

                If Core.Random.Next(0, 3) <> 0 Or Core.Player.Pokemons(0).Ability.Name.ToLower() = "suction cups" Or Core.Player.Pokemons(0).Ability.Name.ToLower() = "sticky hold" Then
                    Dim LookingOffset As New Vector3(0)

                    Select Case Screen.Camera.GetPlayerFacingDirection() 
                        Case 0
                            LookingOffset.Z = -1
                        Case 1
                            LookingOffset.X = -1
                        Case 2
                            LookingOffset.Z = 1
                        Case 3
                            LookingOffset.X = 1
                    End Select

                    Dim spawnPosition As Vector3 = New Vector3(Screen.Camera.Position.X + LookingOffset.X, Screen.Camera.Position.Y, Screen.Camera.Position.Z + LookingOffset.Z)

                    Dim endRotation As Integer = Screen.Camera.GetPlayerFacingDirection() + 2 
                    If endRotation > 3 Then
                        endRotation = endRotation - 4
                    End If

                    s &= vbNewLine & "@player.showrod(0)" & vbNewLine &
                        "@text.show(. . . . . . . . . .)" & vbNewLine &
                        "@text.show(Oh!~A bite!)" & vbNewLine &
                        "@player.hiderod" & vbNewLine &
                        "@npc.spawn(" & spawnPosition.X.ToString().Replace(GameController.DecSeparator, ".") & "," & spawnPosition.Y.ToString().Replace(GameController.DecSeparator, ".") & "," & spawnPosition.Z.ToString().Replace(GameController.DecSeparator, ".") & ",0,...,[POKEMON|" & PokemonShiny & "]" & PokemonID & PokemonForms.GetOverworldAddition(p) & ",0," & endRotation & ",POKEMON,1337,Still)" & vbNewLine &
                        "@Level.Update" & vbNewLine &
                        "@pokemon.cry(" & PokemonID & ")" & vbNewLine &
                        "@level.wait(50)" & vbNewLine &
                        "@text.show(The wild " & p.OriginalName & "~attacked!)" & vbNewLine &
                        "@npc.remove(1337)" & vbNewLine &
                        "@battle.setvar(divebattle,true)" & vbNewLine &
                        "@battle.wild(" & p.GetSaveData() & ")" & vbNewLine &
                        ":end"
                Else
                    s &= vbNewLine & "@player.showrod(0)" & vbNewLine &
                        "@text.show(. . . . . . . . . .)" & vbNewLine &
                        "@text.show(No, there's nothing here...)" & vbNewLine &
                        "@player.hiderod" & vbNewLine &
                        ":end"
                End If

                CType(Core.CurrentScreen, OverworldScreen).ActionScript.StartScript(s, 2)
            Else
                Screen.TextBox.Show("Now is not the time~to use that.", {}, True, True)
            End If
        End Sub

        Public Shared Function IsInfrontOfWater() As Boolean
            Dim lookingAtEntities As New List(Of Entity)

            Dim LookingOffset As New Vector3(0)

            Select Case Screen.Camera.GetPlayerFacingDirection() 
                Case 0
                    LookingOffset.Z = -1
                Case 1
                    LookingOffset.X = -1
                Case 2
                    LookingOffset.Z = 1
                Case 3
                    LookingOffset.X = 1
            End Select

            For Each e As Entity In Screen.Level.Entities
                If e.Position.X = Screen.Camera.Position.X + LookingOffset.X And CInt(e.Position.Y) = CInt(Screen.Camera.Position.Y) And e.Position.Z = Screen.Camera.Position.Z + LookingOffset.Z Then
                    lookingAtEntities.Add(e)
                End If
            Next

            If lookingAtEntities.Count > 0 Then
                For Each e As Entity In lookingAtEntities
                    If e.EntityID = "Water" And e.ActionValue = 0 Then
                        Return True
                    End If
                Next
            End If

            Return False
        End Function

    End Class

End Namespace
