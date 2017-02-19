Imports P3D.Legacy.Core.Pokemon

Namespace Items.Berries

    <Item(2062, "Jaboca")>
    Public Class JabocaBerry

        Inherits Berry

        Public Sub New()
            MyBase.New(86400, "If held by a Pokémon, and if a foe's physical attack lands, the foe also takes damage.", "3.3cm", "Soft", 1, 5)

            Me.Spicy = 0
            Me.Dry = 0
            Me.Sweet = 0
            Me.Bitter = 40
            Me.Sour = 10

            Me.Type = Element.Types.Dragon
            Me.Power = 80
        End Sub

    End Class

End Namespace
