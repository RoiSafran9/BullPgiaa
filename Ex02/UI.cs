namespace BullPgiaLogic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UI()
    {
        /// <summary>
        /// prints on the screen the message that the user won and tells him in how many guesses.
        /// </summary>
        static public void WinnerMessage(int io_GuessedMade)
        {
            string finalMessage = string.Format("You guessed after {0} steps!", io_GuessedMade);
            Console.WriteLine(finalMessage);
        }

        /// <summary>
        ///  prints on the screen the message that the user lost the game.
        /// </summary>
        static public void LoseMessage()
        {
            Console.WriteLine("No more guesses allowed. You Lost.");
        }

        /// <summary>
        /// shows on the screen the current state of the game board.
        /// </summary>
        static public void ShowBoard(List<Turn> io_GuessesList, int i_MaxGuesses)
        {
            int guessesLeft = i_MaxGuesses - io_GuessesList.Count; // to know how manny empty line to show on the board
            Console.WriteLine("Current board status:\n");
            Console.WriteLine("|Pins:    |Result:|");
            Console.WriteLine("|=========|=======|");
            Console.WriteLine("| # # # # |       |");
            Console.WriteLine("|=========|=======|");
            foreach (var turn in io_GuessesList)
            {
                string currentGuess = turn.PlayerGuess;
                string currentFeedback = turn.FeedBack;
                string resultMessgae = string.Format(@"| {0} {1} {2} {3} |{4} {5} {6} {7}|",
                    currentGuess[0], currentGuess[1], currentGuess[2], currentGuess[3],
                    currentFeedback[0], currentFeedback[1], currentFeedback[2], currentFeedback[3]);
                Console.WriteLine(resultMessgae);
                Console.WriteLine("|=========|=======|");
            }

            for (int i = 0; i < guessesLeft; i++) // empty lines how many guesses left
            {
                Console.WriteLine("|         |       |");
                Console.WriteLine("|=========|=======|");
            }
        }

        /// <summary>
        /// gets the last user's input regarding a guess of the secret word.
        /// </summary>
        /// <returns> whether the last guess is valid according to the rules of the game. </returns>
        public static bool GuessedWordValidationCheck(string i_UserInput)
        {
            StringBuilder alreadyCheckedLetters = new StringBuilder();
            if (i_UserInput == "Q")
            {
                return true;
            }

            if (i_UserInput.Length != 4)
            {
                return false;
            }
            else
            {
                foreach (char ch in i_UserInput)
                {
                    if (!char.IsUpper(ch))
                    {
                        return false;
                    }

                    if (ch < 'A' || ch > 'H')
                    {
                        return false;
                    }

                    if (GameLogic.IsContains(alreadyCheckedLetters, ch))
                    {
                        return false;
                    }
                    else
                    {
                        alreadyCheckedLetters.Append(ch);
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// ask from the user his guess.
        /// </summary>
        /// <returns> the user's guess. </returns>
        public static string GetPlayerGuess()
        {
            Console.WriteLine("Please type your next guess <A B C D> or 'Q' to quit");
            string playerCurrentGuess = Console.ReadLine();
            while (!GuessedWordValidationCheck(playerCurrentGuess))
            {
                Console.WriteLine("Invalid input. Upper case letters only (A-H). No duplicates.Try again.");
                playerCurrentGuess = Console.ReadLine();
            }

            return playerCurrentGuess;
        }

        /// <summary>
        /// prints on the screen the opening message of the game.
        /// </summary>
        public static void OpeningMessage()
        {
            Console.WriteLine("Hello welcome to Bull Pgiaa game!\nThere's a secret word of 4 letters and you need to guess it by only few guesses of youre choice");
        }

        /// <summary>
        /// resets the current game by clearing the screen.
        /// </summary>
        public static void ResetGame()
        {
            Clear();
        }

        /// <summary>
        /// asks the users if he likes to play another game or stop playing.
        /// </summary>
        /// <returns> the users answer. </returns>
        public static bool AskIfNewGame()
        {
            Console.WriteLine("Would you like to start a new game? <Y/N>");
            string usersAnswer = Console.ReadLine();
            while (usersAnswer != "Y" && usersAnswer != "N")
            {
                Console.WriteLine("Invalid answer. Press Y or N? <Y/N>");
                usersAnswer = Console.ReadLine();
            }

            if (usersAnswer == "Y")
            {
                return true;
            }
            else // "N"
            {
                return false;
            }
        }

        /// <summary>
        /// prints on the screen a goodbye message.
        /// </summary>
        public static void EndingMessage()
        {
            Console.WriteLine("Thank you for playing. Bye bye!");
        }

        /// <summary>
        /// clears the screen.
        /// </summary>
        public static void Clear()
        {
            Console.WriteLine("ENDING...\n\n\n");
        }

        /// <summary>
        /// set the first state of the game board at the beginning of a game.
        /// </summary>
        public static void InitializedGameBoard(int i_MaxGuesses)
        {
            Console.WriteLine("Current board status:\n");
            Console.WriteLine("|Pins:    |Result:|");
            Console.WriteLine("|=========|=======|");
            Console.WriteLine("| # # # # |       |");
            Console.WriteLine("|=========|=======|");
            for (int i = 1; i < i_MaxGuesses; i++) // empty lines how many guesses left
            {
                Console.WriteLine("|         |       |");
                Console.WriteLine("|=========|=======|");
            }
        }
    }
}
