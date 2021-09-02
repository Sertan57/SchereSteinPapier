using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace SchereSteinPapier
{
    class MainWindowViewModel : ObservableObject
    {
        // Private Fields
        private Item scissors;
        private Item stone;
        private Item paper;
        private List<Round> rounds;
        private Player playerOne;
        private Player playerTwo;
        private bool gameEnded;
        private bool playerOneWon;

        // Properties
        public bool GameEnded { get => gameEnded; set { gameEnded = value; OnPropertyChanged("GameEnded"); OnPropertyChanged("GameNotEnded"); OnPropertyChanged("PlayerTwoWon"); } }
        public bool GameNotEnded { get => gameEnded == false; }
        public bool PlayerOneWon { get => playerOneWon; set { playerOneWon = value; OnPropertyChanged("PlayerOneWon"); OnPropertyChanged("PlayerTwoWon"); } }
        public bool PlayerTwoWon { get => GameEnded == true && PlayerOneWon == false; }
        public int RoundsCount { get => rounds.Count; }

        public Player PlayerOne { get => playerOne; }
        public Player PlayerTwo { get => playerTwo; }
        public Round CurrentRound { get
            {
                if (rounds != null)
                {
                    if (rounds.Count > 0)
                    {
                        return rounds[rounds.Count - 1];
                    }
                }
                return null;
            }
        }

        public bool PlayerOneSelectedScissors { get => CurrentRound.PlayerOneSelection == scissors || CurrentRound.PlayerOneSelection == null; }
        public bool PlayerOneSelectedStone { get => CurrentRound.PlayerOneSelection == stone || CurrentRound.PlayerOneSelection == null; }
        public bool PlayerOneSelectedPaper { get => CurrentRound.PlayerOneSelection == paper || CurrentRound.PlayerOneSelection == null; }

        // Commands
        public ICommand RestartCommand { get; private set; }
        public ICommand SelectScissorsCommand { get; private set; }
        public ICommand SelectStoneCommand { get; private set; }
        public ICommand SelectPaperCommand { get; private set; }

        internal MainWindowViewModel()
        {
            RestartCommand = new RelayCommand(Restart);
            SelectScissorsCommand = new RelayCommand(PlayerOneSelectsScissors);
            SelectStoneCommand = new RelayCommand(PlayerOneSelectsStone);
            SelectPaperCommand = new RelayCommand(PlayerOneSelectsPaper);

            InitializeItems();

            playerOne = new Player();
            playerTwo = new Player();

            Restart();
        }

        /// <summary>
        /// Initializes the item-objects for the selections scissors, stone and paper.
        /// </summary>
        private void InitializeItems()
        {
            scissors = new Item("Scissors", "Assets/Textures/Scissors.png");
            stone = new Item("Stone", "Assets/Textures/Stone.png");
            paper = new Item("Paper", "Assets/Textures/Paper.png");

            scissors.AddWeakerItem(paper);
            stone.AddWeakerItem(scissors);
            paper.AddWeakerItem(stone);
        }

        /// <summary>
        /// Restarts the whole game. All scores are set to zero again.
        /// </summary>
        private void Restart()
        {
            GameEnded = false;
            PlayerOneWon = false;

            PlayerOne.Restart();
            PlayerTwo.Restart();

            rounds = new List<Round>();

            Round firstRound = new Round();
            rounds.Add(firstRound);
            UpdateGame();
        }

        /// <summary>
        /// Starts the next round if the current on has ended.
        /// </summary>
        private void NextRound()
        {
            if (CurrentRound.Completed)
            {
                Round nextRound = new Round();
                rounds.Add(nextRound);
            }
        }

        /// <summary>
        /// Returns the winner.
        /// </summary>
        /// <returns>Returns null if no one has reached the max score.Otherwise a Player-Object will be returned.</returns>
        private Player GetWinner()
        {
            if (PlayerOne.Score > Settings.maxScore - 1)
            {
                GameEnded = true;
                PlayerOneWon = true;

                return PlayerOne;
            }
            else if (PlayerTwo.Score > Settings.maxScore - 1)
            {
                GameEnded = true;
                PlayerOneWon = false;

                return PlayerTwo;
            }

            return null;
        }

        /// <summary>
        /// Updates the scores of the players if the both did their selection.
        /// The game ends if the maxscore value defined in the settings
        /// gets reached by one of the players.
        /// </summary>
        private void UpdateGame()
        {
            OnPropertyChanged("CurrentRound");
            OnPropertyChanged("PlayerOneSelectedScissors");
            OnPropertyChanged("PlayerOneSelectedStone");
            OnPropertyChanged("PlayerOneSelectedPaper");
            OnPropertyChanged("RoundsCount");

            if (CurrentRound.Completed)
            {
                if (CurrentRound.GetStrongerItem() == CurrentRound.PlayerOneSelection)
                {
                    PlayerOne.IncreaseScore();
                }
                else if (CurrentRound.GetStrongerItem() == CurrentRound.PlayerTwoSelection)
                {
                    PlayerTwo.IncreaseScore();
                }
            }

            GetWinner();
        }

        /// <summary>
        /// Sets an item for the current round if the game is not ended and the round is not completed already.
        /// A new round starts automatically if there was no winner.
        /// </summary>
        /// <param name="selectedItem">The item the player one has selected.</param>
        private void HandleSelectionOfPlayerOne(Item selectedItem)
        {
            if (CurrentRound != null && GetWinner() == null)
            {
                if (!CurrentRound.Completed)
                {
                    CurrentRound.PlayerTwoSelection = GetRandomItem();
                    CurrentRound.PlayerOneSelection = selectedItem;
                }
                else
                {
                    NextRound();
                }
            }

            UpdateGame();
        }

        /// <summary>
        /// Has to be called if the player one selects the scissors-item.
        /// </summary>
        private void PlayerOneSelectsScissors()
        {
            HandleSelectionOfPlayerOne(scissors);
        }

        /// <summary>
        /// Has to be called if the player one selects the stone-item.
        /// </summary>
        private void PlayerOneSelectsStone()
        {
            HandleSelectionOfPlayerOne(stone);
        }

        /// <summary>
        /// Has to be called if the player one selects the paper-item.
        /// </summary>
        private void PlayerOneSelectsPaper()
        {
            HandleSelectionOfPlayerOne(paper);
        }

        /// <summary>
        /// Returns a randomly chosen item: either scissors, stone or paper.
        /// </summary>
        /// <returns>Item-Object</returns>
        private Item GetRandomItem()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 3);

            if (randomNumber == 0)
            {
                return scissors;
            }
            else if (randomNumber == 1)
            {
                return stone;
            }
            else
            {
                return paper;
            }
        }
    }
}
