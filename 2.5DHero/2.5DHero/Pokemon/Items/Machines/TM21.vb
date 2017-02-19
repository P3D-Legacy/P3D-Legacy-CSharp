Imports P3D.Legacy.Core.Pokemon
Namespace Items.Machines

    <Item(211, "TM 21")>
    Public Class TM21

        Inherits TechMachine

        Public Sub New()
            MyBase.New(True, 1000, 218)
            CanTeachAlways = True
        End Sub

    End Class

End Namespace
