using System;
using System.Configuration;

namespace SchereSteinPapier
{
    /// <summary>
    /// This static class provides general settings for the application.
    /// </summary>
    internal static class Settings
    {
        /// <summary>
        /// Defines the max score the players have to reach for winning the game.
        /// </summary>
        internal static int maxScore = Convert.ToInt32(ConfigurationManager.AppSettings["MaxScore"]);
        /// <summary>
        /// Sets the window language.
        /// </summary>
        internal static string language = ConfigurationManager.AppSettings["Language"];
    }
}
