Imports P3D.Legacy.Core.Pokemon
Namespace Items.Standard

    <Item(291, "Pure Incense")>
    Public Class PureIncense

        Inherits Item

        Public Overrides ReadOnly Property Description As String = "An item to be held by a Pokémon. It helps keep wild Pokémon away if the holder is the head of the party."
        Public Overrides ReadOnly Property PokeDollarPrice As Integer = 9600
        Public Overrides ReadOnly Property FlingDamage As Integer = 10
        Public Overrides ReadOnly Property CanBeUsedInBattle As Boolean = False
        Public Overrides ReadOnly Property CanBeUsed As Boolean = False

        Public Sub New()
            TextureRectangle = New Rectangle(288, 264, 24, 24)
        End Sub

    End Class

End Namespace
