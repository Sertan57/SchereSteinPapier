using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace SchereSteinPapier
{
    /// <summary>
    /// This class can be instantiated for each round in the game. It provides the selections of the players.
    /// </summary>
    internal class Round : ObservableObject
    {
        // Private Fields
        private Item playerOneSelection;
        private Item playerTwoSelection;

        // Properties
        public Item PlayerOneSelection
        {
            get => playerOneSelection;
            set {
                if (playerOneSelection == null) playerOneSelection = value;
                OnPropertyChanged("PlayerOneSelection");
            }
        }
        public Item PlayerTwoSelection
        {
            get => playerTwoSelection;
            set {
                if (playerTwoSelection == null) playerTwoSelection = value;
                OnPropertyChanged("playerTwoSelection");
            }
        }
        internal bool Completed { get => PlayerOneSelection != null && PlayerTwoSelection != null; }
        public bool NotCompleted { get => Completed == false; }

        /// <summary>
        /// The round class has one selection each player. This methods returns the stronger item.
        /// </summary>
        /// <returns>Stronger item e.g. paper beats stone.</returns>
        internal Item GetStrongerItem()
        {
            // Player one´s item is stronger.
            if (PlayerOneSelection.StrongerThan.Contains(PlayerTwoSelection))
            {
                return PlayerOneSelection;
            }
            // Player twos´s item is stronger.
            else if (PlayerTwoSelection.StrongerThan.Contains(PlayerOneSelection))
            {
                return PlayerTwoSelection;
            }
            // No one´s item is stronger.
            else
            {
                return null;
            }
        }
    }
}
