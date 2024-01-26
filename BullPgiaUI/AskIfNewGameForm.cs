namespace BullPgiaUI
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Form that displayed when a game is finished asking the user if he wants to play again or exit.
    /// </summary>
    public partial class AskIfNewGameForm : Form
    {
        GameBoardForm m_CurrentGameBoardForm;
        bool m_StartNewGame;

        /// <summary>
        /// Initializes a new instance of the <see cref="AskIfNewGameForm"/> class.
        /// constructor.
        /// </summary>
        /// <param name="i_CurrentGameBoardForm">.</param>
        public AskIfNewGameForm(GameBoardForm i_CurrentGameBoardForm)
        {
            InitializeComponent();
            m_CurrentGameBoardForm = i_CurrentGameBoardForm;
            m_StartNewGame = false;
        }

        /// <summary>
        /// get & set.
        /// </summary>
        public bool StartNewGame
        {
            get { return m_StartNewGame; }
            set { m_StartNewGame = value; }
        }

        /// <summary>
        /// when user press the Yes button.
        /// </summary>
        private void Yes_Click(object sender, EventArgs e)
        {
            Button yesButton = sender as Button;
            if (yesButton != null)
            {
                StartNewGame = true;
                DialogResult = DialogResult.OK;
                Close();
                CloseCurrentGameBoardForm();
                //StartAnewGame();
            }
        }

        /// <summary>
        /// closes the game board form of the current game that just got ended.
        /// </summary>
        private void CloseCurrentGameBoardForm()
        {
            //m_CurrentGameBoardForm.ResetGame();
            m_CurrentGameBoardForm.Close();
            //m_CurrentGameBoardForm.Dispose();
        }

        /// <summary>
        /// when user press the No button.
        /// </summary>
        private void No_Click(object sender, EventArgs e)
        {
            Button noButton = sender as Button;
            if (noButton != null)
            {
                StartNewGame = false;
                Close();
                m_CurrentGameBoardForm.Close();
            }
        }

        private void AskIfNewGameForm_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
