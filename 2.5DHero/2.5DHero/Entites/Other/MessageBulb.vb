Imports P3D.Legacy.Core.Entities
Imports P3D.Legacy.Core.Entities.Other
Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.Resources.Managers
Imports P3D.Legacy.Core.Resources.Models
Imports P3D.Legacy.Core.Screens

Public Class MessageBulb
    Inherits BaseMessageBulb

    Public NotificationType As BaseMessageBulb.NotifcationTypes = BaseMessageBulb.NotifcationTypes.Exclamation
    Dim setTexture As Boolean = False
    Dim delay As Single = 0.0F

    Public Sub New(ByVal Position As Vector3, ByVal NotificationType As BaseMessageBulb.NotifcationTypes)
        MyBase.New(Position.X, Position.Y, Position.Z, "MessageBulb", {}, {0, 0}, False, 0, New Vector3(0.8F), BaseModel.BillModel, 0, "", New Vector3(1.0F))

        Me.NotificationType = NotificationType
        LoadTexture()
        Me.NeedsUpdate = True
        Me.delay = 8.0F

        Me.DropUpdateUnlessDrawn = False
    End Sub

    Public Overrides Sub Update(gameTime As GameTime)
        If Me.delay > 0.0F Then
            Me.delay -= 0.1F
            If Me.delay <= 0.0F Then
                Me.delay = 0.0F
                Me.CanBeRemoved = True
            End If
        End If
    End Sub

    Private Sub LoadTexture()
        If Me.setTexture = False Then
            Me.setTexture = True

            Dim r As New Rectangle(0, 0, 16, 16)
            Select Case Me.NotificationType
                Case BaseMessageBulb.NotifcationTypes.Waiting
                    r = New Rectangle(0, 0, 16, 16)
                Case BaseMessageBulb.NotifcationTypes.Exclamation
                    r = New Rectangle(16, 0, 16, 16)
                Case BaseMessageBulb.NotifcationTypes.Shouting
                    r = New Rectangle(32, 0, 16, 16)
                Case BaseMessageBulb.NotifcationTypes.Question
                    r = New Rectangle(48, 0, 16, 16)
                Case BaseMessageBulb.NotifcationTypes.Note
                    r = New Rectangle(0, 16, 16, 16)
                Case BaseMessageBulb.NotifcationTypes.Heart
                    r = New Rectangle(16, 16, 16, 16)
                Case BaseMessageBulb.NotifcationTypes.Unhappy
                    r = New Rectangle(32, 16, 16, 16)
                Case BaseMessageBulb.NotifcationTypes.Happy
                    r = New Rectangle(0, 32, 16, 16)
                Case BaseMessageBulb.NotifcationTypes.Friendly
                    r = New Rectangle(16, 32, 16, 16)
                Case BaseMessageBulb.NotifcationTypes.Poisoned
                    r = New Rectangle(32, 32, 16, 16)
                Case BaseMessageBulb.NotifcationTypes.Battle
                    r = New Rectangle(48, 16, 16, 16)
                Case BaseMessageBulb.NotifcationTypes.Wink
                    r = New Rectangle(48, 32, 16, 16)
                Case BaseMessageBulb.NotifcationTypes.AFK
                    r = New Rectangle(0, 48, 16, 16)
                Case BaseMessageBulb.NotifcationTypes.Angry
                    r = New Rectangle(16, 48, 16, 16)
                Case BaseMessageBulb.NotifcationTypes.CatFace
                    r = New Rectangle(32, 48, 16, 16)
                Case BaseMessageBulb.NotifcationTypes.Unsure
                    r = New Rectangle(48, 48, 16, 16)
            End Select

            Me.Textures = {TextureManager.GetTexture("emoticons", r)}
        End If
    End Sub

    Public Overrides Sub UpdateEntity(gameTime As GameTime)
        If Me.Rotation.Y <> Screen.Camera.Yaw Then
            Me.Rotation.Y = Screen.Camera.Yaw
            CreatedWorld = False
        End If

        MyBase.UpdateEntity(gameTime)
    End Sub

    Public Overrides Sub Render(effect As BasicEffect)
        Me.Draw(effect, Me.Model, Me.Textures, True)
    End Sub

End Class