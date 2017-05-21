Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Resources.Managers.Music
Imports P3D.Legacy.Core.Resources.Managers.Sound
Imports P3D.Legacy.Core.Resources.Sound
Imports P3D.Legacy.Core.Screens

Namespace ScriptVersion2

    Partial Class ScriptCommander

        '--------------------------------------------------------------------------------------------------------------------------
        'Contains the @music commands.
        '--------------------------------------------------------------------------------------------------------------------------

        Private Shared Sub DoMusic(ByVal subClass As String)
            Dim command As String = ScriptComparer.GetSubClassArgumentPair(subClass).Command
            Dim argument As String = ScriptComparer.GetSubClassArgumentPair(subClass).Argument

            Select Case command.ToLower()
                Case "play"
                    MusicManager.PlayMusic(argument, True)

                    If Core.CurrentScreen.Identification = Screen.Identifications.OverworldScreen Then
                        Screen.Level.MusicLoop = argument
                    End If
                Case "forceplay"
                    MusicManager.Stop()
                    MusicManager.PlayMusic(argument)
                Case "playnomusic"
                    MusicManager.PlayNoMusic()
                Case "setmusicloop"
                    If Core.CurrentScreen.Identification = Screen.Identifications.OverworldScreen Then
                        Screen.Level.MusicLoop = argument
                    End If
                Case "stop"
                    MusicManager.[Stop]()
                Case "pause"
                    MusicManager.Pause()
                Case "resume"
                    MusicManager.[Resume]()
            End Select

            IsReady = True
        End Sub

    End Class

End Namespace