Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Dialogues
Imports P3D.Legacy.Core.GameJolt.Profiles
Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.Screens

#If WINDOWS Or XBOX Then

Module Program

    Private _gameCrashed As Boolean = False

    ''' <summary>
    ''' The main entry point for the application.
    ''' </summary>
    Sub Main(ByVal args As String())
        'Debug.Print(" ")
        'Debug.Print("PROGRAM EXECUTION STARTED")
        'Debug.Print("STACK TRACE ENTRY                   | MESSAGE")
        'Debug.Print("------------------------------------|------------------------------------")

        CommandLineArgHandler.Initialize(args)

        Logger.Debug("---Start game---")

        Using Game As New GameController(AddressOf DoShit)
            Core.Player = New Player()
            Screen.ChooseBox = New ChooseBox()

            If GameController.IS_DEBUG_ACTIVE = True And Debugger.IsAttached = True Then
                Game.Run()
            Else
                Try
                    Game.Run()
                Catch ex As Exception
                    _gameCrashed = True
                    Dim informationItem As New Logger.ErrorInformation(ex)
                    Logger.LogCrash(ex)
                    Logger.Log(Logger.LogTypes.ErrorMessage, "The game crashed with error ID: " & informationItem.ErrorIDString & " (" & ex.Message & ")")
                End Try
            End If
        End Using
    End Sub

    Sub DoShit(Game As GameController)
        ScriptVersion2.ScriptLibrary.InitializeLibrary()
        Core.SetScreen(New SplashScreen(Game))
    End Sub

    Public ReadOnly Property GameCrashed() As Boolean
        Get
            Return _gameCrashed
        End Get
    End Property

End Module

#End If
