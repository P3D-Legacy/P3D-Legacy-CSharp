Imports P3D.Legacy.Core.Resources
Imports P3D.Legacy.Core.World

''' <summary>
''' Defines and handles terrain definitions for a map.
''' </summary>
Public Class Terrain
    Implements ITerrain

#Region "Fields and Constants"

    Private _terrainType As TerrainTypeEnums = TerrainTypeEnums.Plain

#End Region

#Region "Properties"

    ''' <summary>
    ''' The terrain type of this Terrain instance.
    ''' </summary>
    Public Property TerrainType() As TerrainTypeEnums Implements ITerrain.TerrainType
        Get
            Return Me._terrainType
        End Get
        Set(value As TerrainTypeEnums)
            Me._terrainType = value
        End Set
    End Property

#End Region

#Region "Constructors"

    ''' <summary>
    ''' Creates a new instance of the Terrain class and sets an initial TerrainType.
    ''' </summary>
    ''' <param name="InitialTerrainType">The TerrainType for this instance.</param>
    Public Sub New(ByVal InitialTerrainType As TerrainTypeEnums)
        Me._terrainType = InitialTerrainType
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Converts a Terrain name to the correct Terrain class instance.
    ''' </summary>
    ''' <param name="input">The Terrain name.</param>
    Public Shared Function FromString(ByVal input As String) As TerrainTypeEnums
        Select Case input.ToLower()
            Case "plain"
                Return TerrainTypeEnums.Plain
            Case "sand"
                Return TerrainTypeEnums.Sand
            Case "cave"
                Return TerrainTypeEnums.Cave
            Case "rock"
                Return TerrainTypeEnums.Rock
            Case "tallgrass"
                Return TerrainTypeEnums.TallGrass
            Case "longgrass"
                Return TerrainTypeEnums.LongGrass
            Case "pondwater"
                Return TerrainTypeEnums.PondWater
            Case "seawater"
                Return TerrainTypeEnums.SeaWater
            Case "underwater"
                Return TerrainTypeEnums.Underwater
            Case "disortionworld"
                Return TerrainTypeEnums.DisortionWorld
            Case "puddles"
                Return TerrainTypeEnums.Puddles
            Case "snow"
                Return TerrainTypeEnums.Snow
            Case "magma"
                Return TerrainTypeEnums.Magma
            Case "pvp"
                Return TerrainTypeEnums.PvPBattle
        End Select

        'Default terrain:
        Logger.Log(Logger.LogTypes.Warning, "Terrain.vb: Invalid terrain name: """ & input & """. Returning ""Plains"".")
        Return TerrainTypeEnums.Plain
    End Function

#End Region

    ''' <summary>
    ''' Test for TerrainType equality.
    ''' </summary>
    ''' <param name="value1">The first Terrain instance.</param>
    ''' <param name="value2">The second Terrain instance.</param>
    Public Shared Operator =(ByVal value1 As Terrain, ByVal value2 As Terrain) As Boolean
        Return value1.TerrainType = value2.TerrainType
    End Operator

    ''' <summary>
    ''' Test for TerrainType inequality.
    ''' </summary>
    ''' <param name="value1">The first Terrain instance.</param>
    ''' <param name="value2">The second Terrain instance.</param>
    Public Shared Operator <>(ByVal value1 As Terrain, ByVal value2 As Terrain) As Boolean
        Return Not value1 = value2
    End Operator

End Class
