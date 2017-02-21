using System;

namespace P3D.Legacy.Core
{
    public static class TimeHelpers
    {
        /// <summary>
        /// Converts an amount of seconds to a TimeSpan.
        /// </summary>
        /// <param name="seconds">The seconds to convert.</param>
        public static TimeSpan ConvertSecondToTime(this int seconds)
        {
            int minutes = 0;
            int hours = 0;

            while (seconds > 60)
            {
                minutes += 1;
                seconds -= 60;

                if (minutes > 60)
                {
                    minutes = 0;
                    hours += 1;
                }
            }

            return new TimeSpan(hours, minutes, seconds);
        }

        /// <summary>
        /// Returns the amount of time the player has played.
        /// </summary>
        public static TimeSpan GetCurrentPlayTime()
        {
            TimeSpan pTime = Core.Player.PlayTime;

            int diff = Convert.ToInt32((DateTime.Now - Core.Player.GameStart).Seconds); // TODO
            pTime += ConvertSecondToTime(diff);

            return pTime;
        }

        /// <summary>
        /// Returns the time to display as string.
        /// </summary>
        /// <param name="dateTime">The DateTime to display.</param>
        /// <param name="showSeconds">To show the seconds or not.</param>
        public static string GetDisplayTime(DateTime dateTime, bool showSeconds) => GetDisplayTime(new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second), showSeconds);

        /// <summary>
        /// Returns the time to display as string.
        /// </summary>
        /// <param name="time">The TimeSpan to display.</param>
        /// <param name="showSeconds">To show the seconds or not.</param>
        public static string GetDisplayTime(TimeSpan time, bool showSeconds)
        {
            int days = time.Days;
            int hour = time.Hours;
            if (days > 0)
                hour += days * 24;

            string hours = hour.ToString();
            if (hours.Length == 1)
                hours = "0" + hours;

            string minutes = time.Minutes.ToString();
            if (minutes.Length == 1)
                minutes = "0" + minutes;

            string seconds = time.Seconds.ToString();
            if (seconds.Length == 1)
                seconds = "0" + seconds;

            string t = hours + ":" + minutes;
            if (showSeconds)
                t += "." + seconds;

            return t;
        }
    }
}
