Imports P3D.Legacy.Core.Pokemon
Namespace Items.Standard

    <Item(288, "Full Incense")>
    Public Class FullIncense

        Inherits Item

        Public Overrides ReadOnly Property Description As String = "An item to be held by a Pokémon. This exotic-smelling incense makes the holder bloated and slow moving."
        Public Overrides ReadOnly Property PokeDollarPrice As Integer = 9600
        Public Overrides ReadOnly Property FlingDamage As Integer = 10
        Public Overrides ReadOnly Property CanBeUsedInBattle As Boolean = False
        Public Overrides ReadOnly Property CanBeUsed As Boolean = False

        Public Sub New()
            TextureRectangle = New Rectangle(216, 264, 24, 24)
        End Sub

    End Class

End Namespace
