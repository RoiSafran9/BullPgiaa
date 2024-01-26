namespace BullPgiaLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class GameLogic
    {
        readonly string r_secretWord;
        readonly int r_maxGuesses;
        private List<Turn> m_turnHistory;

        public GameLogic(int io_MaxGuesses) // constructor
        {
            r_secretWord = GenerateSecretWord();
            r_maxGuesses = io_MaxGuesses;
            m_turnHistory = new List<Turn>();
        }

        public string SecretWord
        {
            get { return r_secretWord; }
        }

        public int MaxGuesses
        {
            get { return r_maxGuesses; }
        }

        public List<Turn> TurnHistory
        {
            get { return m_turnHistory; }
        }

        /// <summary>
        /// getter.
        /// </summary>
        /// <returns> max guesses. </returns>
        public int GetMaxGuesses()
        {
            return r_maxGuesses;
        }

        /// <summary>
        /// getter.
        /// </summary>
        /// <returns> the list of all the turns made in the game so far.</returns>
        public List<Turn> GetTurnHistory()
        {
            return m_turnHistory;
        }

        /// <summary>
        /// clears the list of the turns made so far in the current game. 
        /// </summary>
        public void ClearTurnHistory()
        {
            m_turnHistory.Clear();
        }

        /// <summary>
        /// gets a guess which the user made and adds to the turns list the current guess alongside its feedback according to the secret word.
        /// </summary>
        public string NewGuess(string i_PlayerGuess)
        {
            string feedBack = ProcessGuess(i_PlayerGuess);
            m_turnHistory.Add(new Turn(i_PlayerGuess, feedBack));
            return feedBack;
        }

        /// <summary>
        /// gets a guess and analyze it according to the secret word.
        /// </summary>
        /// <returns> the feedback of the guess. </returns>
        public string ProcessGuess(string i_PlayerGuess)
        {
            int VCounter = 0;
            int XCounter = 0;
            StringBuilder feedback = new StringBuilder();

            for (int i = 0; i < i_PlayerGuess.Length; i++)
            {
                if (i_PlayerGuess[i] == r_secretWord[i]) // same place as the secret word
                {
                    VCounter++;
                    continue; // next letter 
                }
                else
                {
                    char currentLetter = i_PlayerGuess[i];
                    if (r_secretWord.Contains(currentLetter)) // secret word contains letter but not in the same place as the guess
                    {
                        XCounter++;
                    }
                }
            }

            for (int i = 0; i < VCounter; i++) // V's first
            {
                feedback.Append("V");
            }

            for (int i = 0; i < XCounter; i++) // X's come after the V's
            {
                feedback.Append("X");
            }

            for (int i = 0; i < 4 - VCounter - XCounter; i++)
            {
                feedback.Append(" ");
            }

            return feedback.ToString(); // result of the guess 
        }

        /// <summary>
        /// creates the secret word.
        /// </summary>
        /// <returns> secret word of the current game. </returns>
        public string GenerateSecretWord()
        {
            Random random = new Random();
            StringBuilder secretWord = new StringBuilder();

            for (int i = 0; i < 4; i++)
            {
                int randomInt = random.Next(0, 8); // A = 0, H = 8
                char randomChar = (char)('A' + randomInt);
                while (IsContains(secretWord, randomChar))
                {
                    randomInt = random.Next(0, 8);
                    randomChar = (char)('A' + randomInt);
                }

                secretWord.Append(randomChar); // append to the current other letters
            }

            return secretWord.ToString();
        }

        /// <summary>
        /// gets the secret word and a letter.
        /// </summary>
        /// <returns> whether the letter is in the secret word. </returns>
        public static bool IsContains(StringBuilder i_CurrentSecretWord, char i_RandomChar)
        {
            if (i_CurrentSecretWord.ToString().IndexOf(i_RandomChar) == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// gets the last guess the user made.
        /// </summary>
        /// <returns> whether the last guess is identical to the secret word. </returns>
        public bool IsCorrectGuess(string io_CurrentGuess)
        {
            if (io_CurrentGuess.Equals(r_secretWord))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// gets a string of letters and translate it to colors.
        /// </summary>
        /// <param name="i_ColorsGuesses"></param>
        /// <returns> list o colors represented by eColor type.</returns>
        public string ColorsToLettersConverter(List<eColor> i_ColorsGuesses)
        {
            StringBuilder guessedWord = new StringBuilder();
            foreach (eColor color in  i_ColorsGuesses) 
            {
                guessedWord.Append(ConvertToLetter(color));
            }
            return guessedWord.ToString();
        }

        /// <summary>
        /// translator.
        /// </summary>
        public char ConvertToLetter(eColor color) 
        {
            char letter = ' ';
            switch (color) 
            {
                case eColor.Purple:
                    letter = 'A';
                    break;
                case eColor.Red:
                    letter = 'B';
                    break;
                case eColor.Green:
                    letter = 'C';
                    break;
                case eColor.Azure:
                    letter = 'D';
                    break;
                case eColor.Blue:
                    letter = 'E';
                    break;
                case eColor.Yellow:
                    letter = 'F';
                    break;
                case eColor.Brown:
                    letter = 'G';
                    break;
                case eColor.White:
                    letter = 'H';
                    break;
            }
            return letter;
        }

        /// <summary>
        /// gets a list of colors and translate it to a string of letters.
        /// </summary>
        /// <param name="i_ColorsGuesses"></param>
        /// <returns> list o colors represented by eColor type.</returns>
        public List<eColor> LettersToColorsConverter(string i_SecretWord)
        {
            List<eColor> secretWordColors = new List<eColor>();
            foreach(char letter in i_SecretWord) 
            {
                secretWordColors.Add(ConvertToColor(letter));
            }
            return secretWordColors;
        }

        /// <summary>
        /// translator.
        /// </summary>
        /// <param name="i_Letter"></param>
        /// <returns></returns>
        public eColor ConvertToColor(char i_Letter)
        {
            eColor color = eColor.Grey;
            switch (i_Letter)
            {
                case 'A':
                    color = eColor.Purple;
                    break;
                case 'B':
                    color = eColor.Red;
                    break;
                case 'C':
                    color = eColor.Green;
                    break;
                case 'D':
                    color = eColor.Azure;
                    break;
                case 'E':
                    color = eColor.Blue;
                    break;
                case 'F':
                    color = eColor.Yellow;
                    break;
                case 'G':
                    color = eColor.Brown;
                    break;
                case 'H':
                    color = eColor.White;
                    break;
            }
            return color;
        }

        /// <summary>
        /// clears the list of the current game turns.
        /// </summary>
        public void Reset()
        {
            m_turnHistory.Clear();
        }
    }
}