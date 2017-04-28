using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3D.Legacy.Core.ScriptSystem.V2
{
    /// <summary>
    /// Represents the default void return, if the contruct could not return anything else.
    /// </summary>
    public class DefaultNullObj
    {

        public override string ToString()
        {
            return "return:void";
            //Just return "void" when this gets used as string to indicate that this type got returned.
        }
    }
}
