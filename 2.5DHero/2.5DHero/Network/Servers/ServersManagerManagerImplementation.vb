Imports net.Pokemon3D.Game.Servers
Imports P3D.Legacy.Core.Server

Public Class ServersManagerManagerImplementation
    Inherits P3D.Legacy.Core.Server.ServersManager.ServersManagerManager

    Public Overrides Function CreateServerConnection() As IServerConnection
        Return New ServerConnection()
    End Function

    Public Overrides Function CreatePlayerManager() As IPlayerManager
        Return New PlayerManager()
    End Function
End Class
