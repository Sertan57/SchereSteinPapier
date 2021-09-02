using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace SchereSteinPapier
{
    /// <summary>
    /// Player class
    /// </summary>
    internal class Player : ObservableObject
    {
        // Private Fields
        private int score;

        // Properties
        public int Score { get => score;
            private set
            {
                if (score != value)
                {
                    score = value;
                    OnPropertyChanged("Score");
                }
            }
        }

        internal Player()
        {
            Restart();
        }

        /// <summary>
        /// Increases the score by one.
        /// </summary>
        internal void IncreaseScore()
        {
            if (Score < Settings.maxScore)
            {
                Score += 1;
            }
        }

        /// <summary>
        /// Sets the score to zero.
        /// </summary>
        internal void Restart()
        {
            Score = 0;
        }
    }
}
