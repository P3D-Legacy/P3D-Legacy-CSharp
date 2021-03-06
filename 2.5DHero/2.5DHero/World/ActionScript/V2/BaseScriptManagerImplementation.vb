﻿Imports net.Pokemon3D.Game.ScriptVersion2
Imports P3D.Legacy.Core.ScriptSystem

Public Class BaseScriptManagerImplementation
    Inherits Script.BaseScriptManager

    Public Overrides Function [New]() As BaseScript
        Return new ScriptV2()
    End Function

    Public Overrides Function EvaluateConstruct(ByVal construct As Object) as Object
        Return ScriptComparer.EvaluateConstruct(construct)
    End Function
End Class