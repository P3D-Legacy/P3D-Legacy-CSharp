Imports P3D.Legacy.Core
Imports P3D.Legacy.Core.Entities.Other
Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.Resources.Models
Imports P3D.Legacy.Core.Resources.Sound
Imports P3D.Legacy.Core.Screens
Imports P3D.Legacy.Core.World

Public Class World
    Inherits BaseWorld

    Private Shared _regionWeather As WeatherEnum = WeatherEnum.Clear
    Private Shared _regionWeatherSet As Boolean = False

    Private _currentMapWeather As WeatherEnum = WeatherEnum.Clear
    Private _environmentType As EnvironmentTypeEnum = EnvironmentTypeEnum.Outside
    Public Overrides Property EnvironmentType As EnvironmentTypeEnum
        Get
            Return _environmentType
        End Get
        Set
            _environmentType = Value
        End Set
    End Property

    Public Overrides Property CurrentWeather As WeatherEnum
        Get
            Return _currentMapWeather
        End Get
        Set
            _currentMapWeather = Value
        End Set
    End Property

    Public UseLightning As Boolean = False

    Public Sub New(ByVal EnvironmentType As Integer, ByVal WeatherType As Integer)
        Initialize(EnvironmentType, WeatherType)
    End Sub

    Public Overrides Sub Initialize(ByVal EnvironmentType As Integer, ByVal WeatherType As Integer)
        If _regionWeatherSet = False Then
            World._regionWeather = GetRegionWeather(World.CurrentSeason)
            World._regionWeatherSet = True
        End If

        Me.CurrentWeather = GetWeatherFromWeatherType(WeatherType)

        Select Case EnvironmentType
            Case 0 'Overworld
                Me.EnvironmentType = EnvironmentTypeEnum.Outside
                Me.UseLightning = True
            Case 1 'Day always
                Me.EnvironmentType = EnvironmentTypeEnum.Inside
                Me.UseLightning = False
            Case 2 'Cave
                Me.EnvironmentType = EnvironmentTypeEnum.Cave
                If WeatherType = 0 Then
                    Me.CurrentWeather = WeatherEnum.Clear
                End If
                Me.UseLightning = False
            Case 3 'Night always
                Me.EnvironmentType = EnvironmentTypeEnum.Dark
                If WeatherType = 0 Then
                    Me.CurrentWeather = WeatherEnum.Clear
                End If
                Me.UseLightning = False
            Case 4 'Underwater
                Me.EnvironmentType = EnvironmentTypeEnum.Underwater
                If WeatherType = 0 Then
                    Me.CurrentWeather = WeatherEnum.Underwater
                End If
                Me.UseLightning = True
            Case 5 'Forest
                Me.EnvironmentType = EnvironmentTypeEnum.Forest
                Me.UseLightning = True
        End Select

        SetWeatherLevelColor()
        ChangeEnvironment()
        Screen.SetRenderDistance(Me.EnvironmentType, Me.CurrentWeather)
    End Sub

    Private Sub SetWeatherLevelColor()
        Select Case CurrentWeather
            Case WeatherEnum.Clear
                Screen.Effect.DiffuseColor = New Vector3(1)
            Case WeatherEnum.Rain, WeatherEnum.Thunderstorm
                Screen.Effect.DiffuseColor = New Vector3(0.4, 0.4, 0.7)
            Case WeatherEnum.Snow
                Screen.Effect.DiffuseColor = New Vector3(0.8)
            Case WeatherEnum.Underwater
                Screen.Effect.DiffuseColor = New Vector3(0.1, 0.3, 0.9)
            Case WeatherEnum.Sunny
                Screen.Effect.DiffuseColor = New Vector3(1.6, 1.3, 1.3)
            Case WeatherEnum.Fog
                Screen.Effect.DiffuseColor = New Vector3(0.5, 0.5, 0.6)
            Case WeatherEnum.Sandstorm
                Screen.Effect.DiffuseColor = New Vector3(0.8, 0.5, 0.2)
            Case WeatherEnum.Ash
                Screen.Effect.DiffuseColor = New Vector3(0.5, 0.5, 0.5)
            Case WeatherEnum.Blizzard
                Screen.Effect.DiffuseColor = New Vector3(0.6, 0.6, 0.6)
        End Select

        Screen.Effect.DiffuseColor = Screen.SkyDome.GetWeatherColorMultiplier(Screen.Effect.DiffuseColor)
    End Sub

    Private Function GetWeatherBackgroundColor(ByVal defaultColor As Color) As Color
        Dim v As Vector3 = Vector3.One

        Select Case CurrentWeather
            Case WeatherEnum.Clear, WeatherEnum.Sunny
                v = New Vector3(1)
            Case WeatherEnum.Rain, WeatherEnum.Thunderstorm
                v = New Vector3(0.4, 0.4, 0.7)
            Case WeatherEnum.Snow
                v = New Vector3(0.8)
            Case WeatherEnum.Underwater
                v = New Vector3(0.1, 0.3, 0.9)
            Case WeatherEnum.Fog
                v = New Vector3(0.7, 0.7, 0.8)
            Case WeatherEnum.Sandstorm
                v = New Vector3(0.8, 0.5, 0.2)
            Case WeatherEnum.Ash
                v = New Vector3(0.5, 0.5, 0.5)
            Case WeatherEnum.Blizzard
                v = New Vector3(0.6, 0.6, 0.6)
        End Select

        Dim colorV As Vector3 = defaultColor.ToVector3 * Screen.SkyDome.GetWeatherColorMultiplier(v)
        Return colorV.ToColor()
    End Function

    Private Sub ChangeEnvironment()
        Select Case Me.EnvironmentType
            Case EnvironmentTypeEnum.Outside
                Core.BackgroundColor = GetWeatherBackgroundColor(SkyDome.GetDaytimeColor(False))
                Screen.Effect.FogColor = Core.BackgroundColor.ToVector3()
                If IsAurora = True Then
                    Screen.SkyDome.TextureUp = TextureManager.GetTexture("SkyDomeResource\AuroraBoralis")
                Else
                    Screen.SkyDome.TextureUp = TextureManager.GetTexture("SkyDomeResource\Clouds1")
                End If
                Screen.SkyDome.TextureDown = TextureManager.GetTexture("SkyDomeResource\Stars")
            Case EnvironmentTypeEnum.Inside
                Core.BackgroundColor = GetWeatherBackgroundColor(New Color(173, 216, 255))
                Screen.Effect.FogColor = Core.BackgroundColor.ToVector3()
                Screen.SkyDome.TextureUp = TextureManager.GetTexture("SkyDomeResource\Clouds")
                Screen.SkyDome.TextureDown = Nothing
            Case EnvironmentTypeEnum.Dark
                Core.BackgroundColor = GetWeatherBackgroundColor(New Color(29, 29, 50))
                Screen.Effect.FogColor = Core.BackgroundColor.ToVector3()
                Screen.SkyDome.TextureUp = TextureManager.GetTexture("SkyDomeResource\Dark")
                Screen.SkyDome.TextureDown = Nothing
            Case EnvironmentTypeEnum.Cave
                Core.BackgroundColor = GetWeatherBackgroundColor(New Color(34, 19, 12))
                Screen.Effect.FogColor = Core.BackgroundColor.ToVector3()
                Screen.SkyDome.TextureUp = TextureManager.GetTexture("SkyDomeResource\Cave")
                Screen.SkyDome.TextureDown = Nothing
            Case EnvironmentTypeEnum.Underwater
                Core.BackgroundColor = GetWeatherBackgroundColor(New Color(19, 54, 117))
                Screen.Effect.FogColor = Core.BackgroundColor.ToVector3()
                Screen.SkyDome.TextureUp = TextureManager.GetTexture("SkyDomeResource\Underwater")
                Screen.SkyDome.TextureDown = TextureManager.GetTexture("SkyDomeResource\UnderwaterGround")
            Case EnvironmentTypeEnum.Forest
                Core.BackgroundColor = GetWeatherBackgroundColor(New Color(30, 66, 21))
                Screen.Effect.FogColor = Core.BackgroundColor.ToVector3()
                Screen.SkyDome.TextureUp = TextureManager.GetTexture("SkyDomeResource\Forest")
                Screen.SkyDome.TextureDown = Nothing
        End Select
    End Sub

    Private Shared WeatherOffset As New Vector2(0, 0)
    Private Shared ObjectsList As New List(Of Rectangle)

    Public Shared Sub DrawWeather(ByVal MapWeather As WeatherEnum)
        If NoParticlesList.Contains(MapWeather) = False Then
            If Core.GameOptions.GraphicStyle = 1 Then
                Dim identifications() As Screen.Identifications = {Screen.Identifications.OverworldScreen, Screen.Identifications.MainMenuScreen, Screen.Identifications.BattleScreen, Screen.Identifications.BattleCatchScreen}
                If identifications.Contains(Core.CurrentScreen.Identification) = True Then
                    If Core.CurrentScreen.Identification = Screen.Identifications.OverworldScreen Then
                        If Screen.TextBox.Showing = False Then
                            GenerateParticles(0, MapWeather)
                        End If
                    Else
                        GenerateParticles(0, MapWeather)
                    End If
                End If
            Else
                Dim T As Texture2D = Nothing

                Dim size As Integer = 128
                Dim opacity As Integer = 30

                Select Case MapWeather
                    Case WeatherEnum.Rain
                        T = TextureManager.GetTexture("Textures\Weather\rain")

                        WeatherOffset.X += 8
                        WeatherOffset.Y += 16
                    Case WeatherEnum.Thunderstorm
                        T = TextureManager.GetTexture("Textures\Weather\rain")

                        WeatherOffset.X += 12
                        WeatherOffset.Y += 20

                        opacity = 50
                    Case WeatherEnum.Snow
                        T = TextureManager.GetTexture("Textures\Weather\snow")

                        WeatherOffset.X += 1
                        WeatherOffset.Y += 1
                    Case WeatherEnum.Blizzard
                        T = TextureManager.GetTexture("Textures\Weather\snow")

                        WeatherOffset.X += 8
                        WeatherOffset.Y += 2

                        opacity = 80
                    Case WeatherEnum.Sandstorm
                        T = TextureManager.GetTexture("Textures\Weather\sand")

                        WeatherOffset.X += 4
                        WeatherOffset.Y += 1

                        opacity = 80
                        size = 48
                    Case WeatherEnum.Underwater
                        T = TextureManager.GetTexture("Textures\Weather\bubble")

                        If Core.Random.Next(0, 100) = 0 Then
                            ObjectsList.Add(New Rectangle(Core.Random.Next(0, Core.WindowSize.Width - 32), Core.WindowSize.Height, 32, 32))
                        End If

                        For i = 0 To ObjectsList.Count - 1
                            Dim r As Rectangle = ObjectsList(i)
                            ObjectsList(i) = New Rectangle(r.X, r.Y - 2, r.Width, r.Height)

                            Core.SpriteBatch.Draw(T, ObjectsList(i), New Color(255, 255, 255, 150))
                        Next
                    Case WeatherEnum.Ash
                        T = TextureManager.GetTexture("Textures\Weather\ash2")

                        WeatherOffset.Y += 1
                        opacity = 65
                        size = 48
                End Select

                If WeatherOffset.X >= size Then
                    WeatherOffset.X = 0
                End If
                If WeatherOffset.Y >= size Then
                    WeatherOffset.Y = 0
                End If

                Select Case MapWeather
                    Case WeatherEnum.Rain, WeatherEnum.Snow, WeatherEnum.Sandstorm, WeatherEnum.Ash, WeatherEnum.Blizzard, WeatherEnum.Thunderstorm
                        For x = -size To Core.WindowSize.Width Step size
                            For y = -size To Core.WindowSize.Height Step size
                                Core.SpriteBatch.Draw(T, New Rectangle(CInt(x + WeatherOffset.X), CInt(y + WeatherOffset.Y), size, size), New Color(255, 255, 255, opacity))
                            Next
                        Next
                End Select
            End If
        End If
    End Sub

    Public Shared Sub GenerateParticles(ByVal chance As Integer, ByVal MapWeather As WeatherEnum)
        If MapWeather = WeatherEnum.Thunderstorm Then
            If Core.Random.Next(0, 250) = 0 Then
                Dim pitch As Single = -(Core.Random.Next(8, 11) / 10.0F)
                'Debug.Print(pitch.ToString())
                SoundManager.PlaySound("Battle\Effects\effect_thunderbolt", pitch, 0F, SoundManager.Volume, False)
            End If
        End If

        If LevelLoader.IsBusy = False Then
            Dim validScreen() As Screen.Identifications = {Screen.Identifications.OverworldScreen, Screen.Identifications.BattleScreen, Screen.Identifications.BattleCatchScreen, Screen.Identifications.MainMenuScreen}
            If validScreen.Contains(Core.CurrentScreen.Identification) = True Then
                If Core.CurrentScreen.Identification = Screen.Identifications.OverworldScreen Then
                    If CType(Core.CurrentScreen, OverworldScreen).ActionScript.IsReady = False Then
                        Exit Sub
                    End If
                End If

                Dim T As Texture2D = Nothing

                Dim speed As Single
                Dim scale As New Vector3(1)
                Dim range As Integer = 3

                Select Case MapWeather
                    Case WeatherEnum.Rain
                        speed = 0.1F
                        T = TextureManager.GetTexture("Textures\Weather\rain3")
                        If chance > -1 Then chance = 3
                        scale = New Vector3(0.03F, 0.06F, 0.1F)
                    Case WeatherEnum.Thunderstorm
                        speed = 0.15F
                        Select Case Core.Random.Next(0, 4)
                            Case 0
                                T = TextureManager.GetTexture("Textures\Weather\rain2")
                                scale = New Vector3(0.1F, 0.1F, 0.1F)
                            Case Else
                                T = TextureManager.GetTexture("Textures\Weather\rain3")
                                scale = New Vector3(0.03F, 0.06F, 0.1F)
                        End Select
                        If chance > -1 Then chance = 1
                    Case WeatherEnum.Snow
                        speed = 0.02F
                        T = TextureManager.GetTexture("Textures\Weather\snow2")
                        If chance > -1 Then chance = 5
                        scale = New Vector3(0.03F, 0.03F, 0.1F)
                    Case WeatherEnum.Underwater
                        speed = -0.02F
                        T = TextureManager.GetTexture("Textures\Weather\bubble")
                        If chance > -1 Then chance = 60
                        scale = New Vector3(0.5F)
                        range = 1
                    Case WeatherEnum.Sandstorm
                        speed = 0.1F
                        T = TextureManager.GetTexture("Textures\Weather\sand")
                        If chance > -1 Then chance = 4
                        scale = New Vector3(0.03F, 0.03F, 0.1F)
                    Case WeatherEnum.Ash
                        speed = 0.02F
                        T = TextureManager.GetTexture("Textures\Weather\ash")
                        If chance > -1 Then chance = 20
                        scale = New Vector3(0.03F, 0.03F, 0.1F)
                    Case WeatherEnum.Blizzard
                        speed = 0.1F
                        T = TextureManager.GetTexture("Textures\Weather\snow")
                        If chance > -1 Then chance = 1
                        scale = New Vector3(0.12F, 0.12F, 0.1F)
                End Select

                If chance = -1 Then
                    chance = 1
                End If

                Dim cameraPosition As Vector3 = Screen.Camera.Position
                If Screen.Camera.Name = "Overworld" Then
                    cameraPosition = CType(Screen.Camera, OverworldCamera).CPosition
                End If

                If Core.Random.Next(0, chance) = 0 Then
                    For x = cameraPosition.X - range To cameraPosition.X + range
                        For z = cameraPosition.Z - range To cameraPosition.Z + range
                            If z <> 0 Or x <> 0 Then
                                Dim rY As Single = CSng(Core.Random.Next(0, 40) / 10) - 2.0F
                                Dim rX As Single = CSng(Core.Random.NextDouble()) - 0.5F
                                Dim rZ As Single = CSng(Core.Random.NextDouble()) - 0.5F
                                Dim p As Particle = New Particle(New Vector3(x + rX, cameraPosition.Y + 1.8F + rY, z + rZ), {T}, {0, 0}, Core.Random.Next(0, 2), scale, BaseModel.BillModel, New Vector3(1))
                                p.MoveSpeed = speed
                                If MapWeather = WeatherEnum.Rain Then
                                    p.Opacity = 0.7F
                                End If
                                If MapWeather = WeatherEnum.Thunderstorm Then
                                    p.Opacity = 1.0F
                                End If
                                If MapWeather = WeatherEnum.Underwater Then
                                    p.Position.Y = 0.0F
                                    p.Destination = 10
                                    p.Behavior = Particle.Behaviors.Rising
                                End If
                                If MapWeather = WeatherEnum.Sandstorm Then
                                    p.Behavior = Particle.Behaviors.LeftToRight
                                    p.Destination = cameraPosition.X + 5
                                    p.Position.X -= 2
                                End If
                                If MapWeather = WeatherEnum.Blizzard Then
                                    p.Opacity = 1.0F
                                End If
                                Screen.Level.Entities.Add(p)
                            End If
                        Next
                    Next
                End If
            End If
        End If
    End Sub

    Private Shared SeasonTextureBuffer As New Dictionary(Of Texture2D, Texture2D)
    Private Shared BufferSeason As SeasonEnum = SeasonEnum.Fall

    Private Shared Function NeedServerObject() As Boolean
        Return JoinServerScreen.Online = True And ConnectScreen.Connected = True
    End Function
End Class
