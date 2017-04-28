Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Screens

Namespace ScriptVersion2

    Partial Class ScriptCommander

        '--------------------------------------------------------------------------------------------------------------------------
        'Contains the @camera commands.
        '--------------------------------------------------------------------------------------------------------------------------

        Private Shared Sub DoCamera(gameTime As GameTime, ByVal subClass As String)
            Dim command As String = ScriptComparer.GetSubClassArgumentPair(subClass).Command
            Dim argument As String = ScriptComparer.GetSubClassArgumentPair(subClass).Argument

            Dim c As OverworldCamera = CType(Screen.Camera, OverworldCamera)
            Dim doCameraUpdate As Boolean = True

            Select Case command.ToLower()
                Case "set"
                    Dim x As Single = sng(argument.GetSplit(0).Replace("~", CStr(c.ThirdPersonOffset.X)))
                    Dim y As Single = sng(argument.GetSplit(1).Replace("~", CStr(c.ThirdPersonOffset.Y)))
                    Dim z As Single = sng(argument.GetSplit(2).Replace("~", CStr(c.ThirdPersonOffset.Z)))
                    Dim yaw As Single = sng(argument.GetSplit(3))
                    Dim pitch As Single = sng(argument.GetSplit(4))

                    c.ThirdPersonOffset = New Vector3(x, y, z)
                    c.Yaw = yaw
                    c.Pitch = pitch
                Case "reset"
                    c.ThirdPersonOffset = New Vector3(0.0F, 0.3F, 1.5F)
                    If argument <> "" Then
                        doCameraUpdate = CBool(argument)
                    End If
                Case "setyaw"
                    Dim yaw As Single = sng(argument.Replace(",", "."))

                    c.Yaw = yaw
                Case "setpitch"
                    Dim pitch As Single = sng(argument.Replace(",", "."))

                    c.Pitch = pitch
                Case "setposition"
                    Dim x As Single = sng(argument.GetSplit(0).Replace("~", CStr(c.ThirdPersonOffset.X)))
                    Dim y As Single = sng(argument.GetSplit(1).Replace("~", CStr(c.ThirdPersonOffset.Y)))
                    Dim z As Single = sng(argument.GetSplit(2).Replace("~", CStr(c.ThirdPersonOffset.Z)))

                    c.ThirdPersonOffset = New Vector3(x, y, z)
                Case "setx"
                    Dim x As Single = sng(argument.Replace("~", CStr(c.ThirdPersonOffset.X)))

                    c.ThirdPersonOffset.X = x
                Case "sety"
                    Dim y As Single = sng(argument.Replace("~", CStr(c.ThirdPersonOffset.Y)))

                    c.ThirdPersonOffset.Y = y
                Case "setz"
                    Dim z As Single = sng(argument.Replace("~", CStr(c.ThirdPersonOffset.Z)))

                    c.ThirdPersonOffset.Z = z
                Case "togglethirdperson"
                    If Core.CurrentScreen.Identification = Screen.Identifications.OverworldScreen Then
                        c.SetThirdPerson(Not c.ThirdPerson, False)
                    End If
                    If argument <> "" Then
                        doCameraUpdate = CBool(argument)
                    End If
                Case "activatethirdperson"
                    If Core.CurrentScreen.Identification = Screen.Identifications.OverworldScreen Then
                        c.SetThirdPerson(True, False)
                    End If
                    If argument <> "" Then
                        doCameraUpdate = CBool(argument)
                    End If
                Case "deactivethirdperson", "deactivatethirdperson"
                    If Core.CurrentScreen.Identification = Screen.Identifications.OverworldScreen Then
                        c.SetThirdPerson(False, False)
                    End If
                    If argument <> "" Then
                        doCameraUpdate = CBool(argument)
                    End If
                Case "setthirdperson"
                    Dim thirdPerson As Boolean = CBool(argument.GetSplit(0))
                    If Core.CurrentScreen.Identification = Screen.Identifications.OverworldScreen Then
                        c.SetThirdPerson(thirdPerson, False)
                    End If
                    If argument.CountSplits() > 1 Then
                        doCameraUpdate = CBool(argument.GetSplit(1))
                    End If
                Case "fix"
                    c.Fixed = True
                    If argument <> "" Then
                        doCameraUpdate = CBool(argument)
                    End If
                Case "defix"
                    c.Fixed = False
                    If argument <> "" Then
                        doCameraUpdate = CBool(argument)
                    End If
                Case "togglefix"
                    c.Fixed = Not c.Fixed
                    If argument <> "" Then
                        doCameraUpdate = CBool(argument)
                    End If
                Case "update"
                    doCameraUpdate = True
                    CanContinue = False
                Case "setfocus"
                    Dim focusType = OverworldCamera.CameraFocusTypes.Player
                    Select Case argument.GetSplit(0).ToLower()
                        Case "player"
                            focusType = OverworldCamera.CameraFocusTypes.Player
                        Case "npc"
                            focusType = OverworldCamera.CameraFocusTypes.NPC
                        Case "entity"
                            focusType = OverworldCamera.CameraFocusTypes.Entity
                    End Select
                    c.SetupFocus(focusType, int(argument.GetSplit(1)))
                Case "setfocustype"
                    Select Case argument.ToLower()
                        Case "player"
                            c.CameraFocusType = OverworldCamera.CameraFocusTypes.Player
                        Case "npc"
                            c.CameraFocusType = OverworldCamera.CameraFocusTypes.NPC
                        Case "entity"
                            c.CameraFocusType = OverworldCamera.CameraFocusTypes.Entity
                    End Select
                Case "setfocusid"
                    c.CameraFocusID = int(argument)
                Case "resetfocus"
                    c.CameraFocusType = OverworldCamera.CameraFocusTypes.Player
                    c.CameraFocusID = -1
                Case "settoplayerfacing"
                    Dim facing As Integer = Screen.Camera.GetPlayerFacingDirection()
                    c.Yaw = facing * MathHelper.PiOver2
            End Select

            c.UpdateThirdPersonCamera()
            If doCameraUpdate = True Then
                c.UpdateFrustum()
                c.UpdateViewMatrix()
                Screen.Level.SortEntities()
                Screen.Level.UpdateEntities(gameTime)
            End If

            IsReady = True
        End Sub

    End Class

End Namespace