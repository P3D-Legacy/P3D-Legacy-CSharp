Imports System.Globalization
Imports P3D.Legacy.Core.Entities
Imports P3D.Legacy.Core.Screens

Namespace ScriptVersion2

    Partial Class ScriptComparer

        '--------------------------------------------------------------------------------------------------------------------------
        'Contains the <entity> constructs.
        '--------------------------------------------------------------------------------------------------------------------------

        Private Shared Function DoEntity(ByVal subClass As String) As Object
            Dim command As String = GetSubClassArgumentPair(subClass).Command
            Dim argument As String = GetSubClassArgumentPair(subClass).Argument

            Dim entID As Integer = int(argument.GetSplit(0))
            Dim ent As Entity = Screen.Level.GetEntity(entID)

            If Not ent Is Nothing Then
                Select Case command.ToLower()
                    Case "visible"
                        Return ReturnBoolean(ent.Visible)
                    Case "opacity"
                        Return ent.Opacity * 100
                    Case "position"
                        Return ent.Position.X.ToString(CultureInfo.InvariantCulture) & "," & ent.Position.Y.ToString(CultureInfo.InvariantCulture) & "," & ent.Position.Z.ToString(CultureInfo.InvariantCulture)
                    Case "positiony"
                        Return ent.Position.Y
                    Case "positionz"
                        Return ent.Position.Z
                    Case "positionx"
                        Return ent.Position.X
                    Case "scale"
                        Return ent.Scale.X.ToString(CultureInfo.InvariantCulture) & "," & ent.Scale.Y.ToString(CultureInfo.InvariantCulture) & "," & ent.Scale.Z.ToString(CultureInfo.InvariantCulture)
                    Case "additionalvalue"
                        Return ent.AdditionalValue
                    Case "collision"
                        Return ReturnBoolean(ent.Collision)
                End Select
            End If

            Return DEFAULTNULL
        End Function

    End Class

End Namespace