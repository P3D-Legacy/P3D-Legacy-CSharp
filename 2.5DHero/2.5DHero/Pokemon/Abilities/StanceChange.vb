Namespace Abilities

    Public Class StanceChange

        Inherits Ability

        Public Sub New()
            MyBase.New(176, "Stance Change", "The Pokémon changes form depending on how it battles.")
        End Sub

        Public Overloads Sub SwitchOut(parentPokemon As Pokemon)
            parentPokemon.AdditionalData = ""
            parentPokemon.ReloadDefinitions()
        End Sub

        Public Overloads Sub EndBattle(parentPokemon As Pokemon)
            parentPokemon.AdditionalData = ""
            parentPokemon.ReloadDefinitions()
        End Sub

    End Class

End Namespace