Imports P3D.Legacy.Core.Entities
Imports P3D.Legacy.Core.Resources.Models

Public Class EntityManagerImplementation
    Inherits Entity.EntityManager

    Public Overrides Function GetNewEntity(ByVal EntityID As String, ByVal Position As Vector3, ByVal Textures() As Texture2D, ByVal TextureIndex() As Integer, ByVal Collision As Boolean, ByVal Rotation As Vector3, ByVal Scale As Vector3, ByVal Model As BaseModel, ByVal ActionValue As Integer, ByVal AdditionalValue As String, ByVal Visible As Boolean, ByVal Shader As Vector3, ByVal ID As Integer, ByVal MapOrigin As String, ByVal SeasonColorTexture As String, ByVal Offset As Vector3, Optional ByVal Params() As Object = Nothing, Optional ByVal Opacity As Single = 1.0F) As Entity
        Dim newEnt As New Entity()
        Dim propertiesEnt As New Entity()

        propertiesEnt.EntityID = EntityID
        propertiesEnt.Position = Position
        propertiesEnt.Textures = Textures
        propertiesEnt.TextureIndex = TextureIndex
        propertiesEnt.Collision = Collision
        propertiesEnt.Rotation = Rotation
        propertiesEnt.Scale = Scale
        propertiesEnt.Model = Model
        propertiesEnt.ActionValue = ActionValue
        propertiesEnt.AdditionalValue = AdditionalValue
        propertiesEnt.Visible = Visible
        propertiesEnt.Shader = Shader
        propertiesEnt.NormalOpacity = Opacity

        propertiesEnt.ID = ID
        propertiesEnt.MapOrigin = MapOrigin
        propertiesEnt.SeasonColorTexture = SeasonColorTexture
        propertiesEnt.Offset = Offset

        Select Case EntityID.ToLower()
            Case "wallblock"
                newEnt = New WallBlock()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, WallBlock).Initialize()
            Case "cube", "allsidesobject"
                newEnt = New AllSidesObject()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, AllSidesObject).Initialize()
            Case "slideblock"
                newEnt = New SlideBlock()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, SlideBlock).Initialize()
            Case "wallbill"
                newEnt = New WallBill()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, WallBill).Initialize()
            Case "signblock"
                newEnt = New SignBlock()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, SignBlock).Initialize()
            Case "warpblock"
                newEnt = New WarpBlock()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, WarpBlock).Initialize()
            Case "floor"
                newEnt = New Floor()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, Floor).Initialize(True, False, True)
            Case "step"
                newEnt = New StepBlock()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, StepBlock).Initialize()
            Case "cuttree"
                newEnt = New CutDownTree()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, CutDownTree).Initialize()
            Case "water"
                newEnt = New Water()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, Water).Initialize()
            Case "grass"
                newEnt = New Grass()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, Grass).Initialize()
            Case "berryplant"
                newEnt = New BerryPlant()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, BerryPlant).Initialize()
            Case "loamysoil"
                newEnt = New LoamySoil()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, LoamySoil).Initialize()
            Case "itemobject"
                newEnt = New ItemObject()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, ItemObject).Initialize()
            Case "scriptblock"
                newEnt = New ScriptBlock()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, ScriptBlock).Initialize()
            Case "turningsign"
                newEnt = New TurningSign()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, TurningSign).Initialize()
            Case "apricornplant"
                newEnt = New ApricornPlant()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, ApricornPlant).Initialize()
            Case "headbutttree"
                newEnt = New HeadbuttTree()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, HeadbuttTree).Initialize()
            Case "smashrock"
                newEnt = New SmashRock()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, SmashRock).Initialize()
            Case "strengthrock"
                newEnt = New StrengthRock()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, StrengthRock).Initialize()
            Case "npc"
                newEnt = New NPC()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, NPC).Initialize(CStr(Params(0)), CInt(Params(1)), CStr(Params(2)), CInt(Params(3)), CBool(Params(4)), CStr(Params(5)), CType(Params(6), List(Of Rectangle)))
            Case "waterfall"
                newEnt = New Waterfall()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, Waterfall).Initialize()
            Case "whirlpool"
                newEnt = New Whirlpool()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, Whirlpool).Initialize()
            Case "strengthtrigger"
                newEnt = New StrengthTrigger()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, StrengthTrigger).Initialize()
            Case "modelentity"
                newEnt = New ModelEntity()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, ModelEntity).Initialize()
            Case "rotationtile"
                newEnt = New RotationTile()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, RotationTile).Initialize()
            Case "divetile"
                newEnt = New DiveTile()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, DiveTile).Initialize()
            Case "rockclimbentity"
                newEnt = New RockClimbEntity()
                Entity.SetProperties(newEnt, propertiesEnt)
                CType(newEnt, RockClimbEntity).Initialize()
        End Select

        Return newEnt
    End Function

End Class