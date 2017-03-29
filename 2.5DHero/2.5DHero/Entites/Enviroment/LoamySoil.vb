Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Entities
Imports P3D.Legacy.Core.Pokemon
Imports P3D.Legacy.Core.Resources.Sound
Imports P3D.Legacy.Core.Screens

Public Class LoamySoil

    Inherits Entity

    Public Overrides Sub Initialize()
        MyBase.Initialize()

        Me.Visible = False
    End Sub

    Public Overrides Sub ClickFunction()
        Dim hasBerry As Boolean = False
        For Each Entity As Entity In Screen.Level.Entities
            If Entity.EntityID = "BerryPlant" And Entity.Position = Me.Position Then
                hasBerry = True
                Entity.ClickFunction()
                Exit For
            End If
        Next
        If hasBerry = False Then
            Screen.TextBox.Show("Do you want to plant a~berry here?%Yes|No%", {Me})
            SoundManager.PlaySound("select")
        End If
    End Sub

    Public Overrides Sub ResultFunction(ByVal Result As Integer)
        If Result = 0 Then
            Core.SetScreen(New InventoryScreen(Core.CurrentScreen, {4}, 4, AddressOf Me.PlantBerry))
        End If
    End Sub

    Public Sub PlantBerry(ByVal ChosenBerry As Integer)
        Dim testItem As Item = Item.GetItemByID(ChosenBerry)
        If testItem.isBerry = True Then
            Dim Berry As Berry = CType(Item.GetItemById(ChosenBerry), Berry)

            BerryPlant.AddBerryPlant(Screen.Level.LevelFile, Me.Position, Berry.BerryIndex)
            Screen.TextBox.reDelay = 0.0F
            Screen.TextBox.Show("You planted a~" & Berry.Name & " Berry here.", {})
        End If
    End Sub

    Public Overrides Sub Render(effect As BasicEffect)
        Me.Draw(effect, Me.Model, Textures, False)
    End Sub

End Class