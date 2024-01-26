namespace Ex02
{
    using BullPgiaLogic;
    using System;
    using System.Collections.Generic;

    public class GameEngine
    {
        private GameLogic m_GameLogic;
        private List<Turn> m_GuessesList;
         
        public GameEngine(int i_NumberOfChances) // constructor
        {
            m_GameLogic = new GameLogic(i_NumberOfChances);
            m_GuessesList = new List<Turn>();
            string m_secretWord = m_GameLogic.SecretWord;

            //gameLogic.PlayerGuessEvent += HandlePlayerGuess;
            //gameLogic.FeedbackEvent += HandleFeedBack;
            //gameLogic.CorrectGuessEvent += HandleCorrectGuess;
        }

        /// <summary>
        /// gets a list of colors of the last guess of the user and checks if its similar to the secret colors.
        /// </summary>
        public string CheckGuess(List<eColor> i_CurrentGuesses)
        {
            string lastGuessString = m_GameLogic.ColorsToLettersConverter(i_CurrentGuesses);
            string feedBackString = m_GameLogic.NewGuess(lastGuessString);
            return feedBackString;
        }

        /// <summary>
        /// The current game starts running and gets all of the user's guesses and presents the current state of the game board.
        /// </summary>
        /// <returns> whether the user wants to start a new game of finish playing. </returns>
        public bool Run() // play the game
        {
            UI.InitializedGameBoard(m_GameLogic.GetMaxGuesses());
            int guessesMade = 0;
            bool finishGameRequested = false;
            bool isWin = false;

            UI.OpeningMessage(); // hello message
            while (!isWin && guessesMade < m_GameLogic.GetMaxGuesses())
            {
                UI.Clear();
                UI.ShowBoard(m_GameLogic.GetTurnHistory(), m_GameLogic.GetMaxGuesses());
                string playerGuess = UI.GetPlayerGuess();
                m_GameLogic.NewGuess(playerGuess);
                while (!UI.GuessedWordValidationCheck(playerGuess)) // validation check of the users input guess
                {
                    playerGuess = UI.GetPlayerGuess();
                }

                guessesMade++;
                if (playerGuess == "Q") // FINISH
                {
                    finishGameRequested = true;
                    break;
                }
                else
                {
                    if (m_GameLogic.IsCorrectGuess(playerGuess))
                    {
                        isWin = true;
                        break;
                    }
                }
            }

            if (!finishGameRequested || guessesMade == m_GameLogic.GetMaxGuesses()) // made all the guesses or pressed "Q"
            {
                UI.Clear();
                UI.ShowBoard(m_GameLogic.GetTurnHistory(), m_GameLogic.GetMaxGuesses());
                if (isWin)
                {
                    UI.WinnerMessage(guessesMade);
                }
                else
                {
                    UI.LoseMessage();
                }
            }

            m_GameLogic.ClearTurnHistory();

            return UI.AskIfNewGame(); // press Y or N
        }

        /// <summary>
        /// asks the user how many guesses he would like to get in the game.
        /// </summary>
        /// <returns> amount of guesses the user wants. </returns>
        public int GetMaxGuessesFromUser()
        {
            Console.WriteLine("please enter the maximum amount of guesses you would like to have in the game");
            string maxGuessesSrinInput = Console.ReadLine();
            while (!MaxGuessesValidationCheck(maxGuessesSrinInput))
            {
                Console.WriteLine("Invalid input. Between 4 to 10 only. Try again.");
                maxGuessesSrinInput = Console.ReadLine();
            }
            if (int.TryParse(maxGuessesSrinInput, out int maxGuesses))
            {
                return maxGuesses;
            }
            else
            {
                Console.WriteLine("Invalid input");

                return -1;
            }
        }

        /// <summary>
        /// Gets the users input regarding the amount of guesses he wants.
        /// </summary>
        /// <returns>whether the input is valid (4-10) </returns>
        public bool MaxGuessesValidationCheck(string i_UserInput)
        {
            if (int.TryParse(i_UserInput, out int maxGuesses))
            {
                if (maxGuesses >= 4 && maxGuesses <= 10)
                {
                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        public List<eColor> GetColorsOfSecretWord()
        {
            return m_GameLogic.LettersToColorsConverter(m_GameLogic.SecretWord);
        }

        /// <summary>
        /// checks if game is over. 1 - the correct guess was made, 2 - user made all of his guess allowed.
        /// </summary>
        /// <param name="i_FeedBackString"></param>
        /// <returns> true or false. </returns>
        public bool IsGameOver(string i_FeedBackString)
        {
            bool gameEnded = false;

            // Check if the game win condition is met
            if (i_FeedBackString == "VVVV")
            {
                // Handle the win condition
                gameEnded = true;
            }
            else  // Check if the loss condition is met
            {
                if (m_GameLogic.MaxGuesses <= m_GameLogic.TurnHistory.Count)
                {
                    // Handle the loss condition
                    gameEnded = true;
                }
            }
      
            return gameEnded;
        }

        /// <summary>
        /// clears the list of the guesses of the user.
        /// </summary>
        public void Reset()
        {
            m_GuessesList.Clear();
            m_GameLogic.Reset();
        }
    }

}
