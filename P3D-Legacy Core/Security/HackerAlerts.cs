using System.Timers;

namespace P3D.Legacy.Core.Security
{
    public class HackerAlerts
    {
        private static Timer _timer;

        public static void Activate()
        {
            _timer = new Timer();
            _timer.Elapsed += TimerTick;
            _timer.AutoReset = true;
            _timer.Interval = 25000;
            _timer.Start();
        }

        private static void TimerTick(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            _timer.Stop();

            string text = @"Hello. We have detected that you attempted to hack the game. Please don't do that. We are trying to create an online experience where everyone is set equal, where everyone can play the game and achieve about the same things by playing the same amount of time.
So please reconsider utilizing certain RAM editing tools in Pokémon 3D.
Of course, you are free to use those whenever you want in Offline Mode, since that is essentially a Sandbox for you to play around in.
To remove these messages and the reverse texture effects, consider following this link: 

http://pokemon3d.net/forum/faq/44/

Thank you for your cooperation.";

            // TODO
            //Interactions.MsgBox(text, MsgBoxStyle.OkOnly, "Pokémon3D Injection Shield");

            _timer.Start();
        }

    }
}
