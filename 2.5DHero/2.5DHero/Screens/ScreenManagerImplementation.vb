Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Screens

Public Class ScreenManagerImplementation
    Inherits Screen.ScreenManager

    Public Overrides Function CreateConnectScreen(myMode As BaseConnectScreen.Modes, header As String, message As String, currentScreen As Screen) As BaseConnectScreen
        Return New ConnectScreen(myMode, header, message, currentScreen)
    End Function

    Public Overrides Function CreateSplashScreen(gameInstance As GameController) As Screen
        Return New SplashScreen(gameInstance)
    End Function

    Public Overrides Function CreatePauseScreen(prevScreen As Screen) As Screen
        Return New PauseScreen(prevScreen)
    End Function

    Public Overrides Function CreateChatScreen(prevScreen As Screen) As Screen
        Return New ChatScreen(prevScreen)
    End Function

End Class
