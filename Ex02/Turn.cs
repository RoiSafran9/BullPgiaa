namespace BullPgiaLogic
{
    /// <summary>
    /// Turn represents every guess of the user alongside the feedback of his guess accordinf to the secret word.
    /// </summary>
    public class Turn
    {
        string m_PlayerGuess;
        string m_FeedBack;

        public string PlayerGuess
        {
            get
            {
                return m_PlayerGuess;
            }
        }

        public string FeedBack
        {
            get
            {
                return m_FeedBack;
            }
        }

        public Turn(string i_PlayerGuess, string i_FeedBack) // constructor
        {
            m_PlayerGuess = i_PlayerGuess;
            m_FeedBack = i_FeedBack;
        }
    }
}
