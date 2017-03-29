Imports System.Globalization
Imports P3D.Legacy.Core.Screens

Namespace ScriptVersion2

    Partial Class ScriptComparer

        '--------------------------------------------------------------------------------------------------------------------------
        'Contains the <environment> constructs.
        '--------------------------------------------------------------------------------------------------------------------------

        Private Shared Function DoEnvironment(ByVal subClass As String) As Object
            Dim command As String = GetSubClassArgumentPair(subClass).Command
            Dim argument As String = GetSubClassArgumentPair(subClass).Argument

            Select Case command.ToLower()
                Case "daytime"
                    Return World.GetTime.ToString(CultureInfo.InvariantCulture)
                Case "daytimeid"
                    Return int(CInt(World.GetTime)).ToString(CultureInfo.InvariantCulture)
                Case "season"
                    Return World.CurrentSeason.ToString(CultureInfo.InvariantCulture)
                Case "seasonid"
                    Return int(CInt(World.CurrentSeason)).ToString(CultureInfo.InvariantCulture)
                Case "day"
                    Return My.Computer.Clock.LocalTime.DayOfWeek.ToString(CultureInfo.InvariantCulture)
                Case "dayofyear"
                    Return My.Computer.Clock.LocalTime.DayOfYear().ToString(CultureInfo.InvariantCulture)
                Case "dayinformation"
                    Return My.Computer.Clock.LocalTime.DayOfWeek.ToString(CultureInfo.InvariantCulture) & "," & World.GetTime.ToString(CultureInfo.InvariantCulture)
                Case "week"
                    Return World.WeekOfYear.ToString(CultureInfo.InvariantCulture)
                Case "hour"
                    Return My.Computer.Clock.LocalTime.Hour.ToString(CultureInfo.InvariantCulture)
                Case "year"
                    Return My.Computer.Clock.LocalTime.Year.ToString(CultureInfo.InvariantCulture)
                Case "weather", "mapweather", "currentmapweather"
                    Return Screen.Level.World.CurrentWeather.ToString(CultureInfo.InvariantCulture)
                Case "weatherid", "mapweatherid", "currentmapweatherid"
                    Return int(CInt(Screen.Level.World.CurrentWeather)).ToString(CultureInfo.InvariantCulture)
                Case "regionweather"
                    Return World.GetCurrentRegionWeather().ToString(CultureInfo.InvariantCulture)
                Case "regionweatherid"
                    Return int(CInt(World.GetCurrentRegionWeather())).ToString(CultureInfo.InvariantCulture)
                Case "canfly"
                    Return ReturnBoolean(Screen.Level.CanFly)
                Case "candig"
                    Return ReturnBoolean(Screen.Level.CanDig)
                Case "canteleport"
                    Return ReturnBoolean(Screen.Level.CanTeleport)
                Case "wildpokemongrass"
                    Return ReturnBoolean(Screen.Level.WildPokemonGrass)
                Case "wildpokemonwater"
                    Return ReturnBoolean(Screen.Level.WildPokemonWater)
                Case "wildpokemoneverywhere"
                    Return ReturnBoolean(Screen.Level.WildPokemonFloor)
                Case "isdark"
                    Return ReturnBoolean(Screen.Level.IsDark)
            End Select

            Return DEFAULTNULL
        End Function

    End Class

End Namespace