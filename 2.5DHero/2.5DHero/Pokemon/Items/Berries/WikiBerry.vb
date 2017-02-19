Imports P3D.Legacy.Core.Pokemon

Namespace Items.Berries

    <Item(2011, "Wiki")>
    Public Class WikiBerry

        Inherits Berry

        Public Sub New()
            MyBase.New(21600, "If held by a Pokémon, it restores the user's HP in a pinch, but it will cause confusion if the user hates the taste.", "11.5cm", "Hard", 2, 3)

            Me.Spicy = 0
            Me.Dry = 15
            Me.Sweet = 0
            Me.Bitter = 0
            Me.Sour = 0

            Me.Type = Element.Types.Rock
            Me.Power = 60
        End Sub

    End Class

End Namespace
