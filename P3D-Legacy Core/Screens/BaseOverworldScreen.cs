//using P3D.Legacy.Core.ScriptSystem;

namespace P3D.Legacy.Core.Screens
{
    public abstract class BaseOverworldScreen : Screen
    {
        /// <summary>
        /// Fade progress value for the black screen fade.
        /// </summary>
        public static int FadeValue { get; set; }

        /// <summary>
        /// The Fishing Rod that should be rendered on the screen.
        /// </summary>
        /// <remarks>-1 = No Rod, 0 = Old Rod, 1 = Good Rod, 2 = Super Rod</remarks>
        public static int DrawRodId { get; set; }

        public abstract bool TrainerEncountered { get; set; }
        public abstract bool ActionScriptIsReady { get; }

        //public ActionScript ActionScript { get; set; }
    }
}
