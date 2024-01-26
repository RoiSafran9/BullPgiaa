namespace BullPgiaUI
{
    using BullPgiaLogic;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Ex02;

    public delegate void GameIsOverEventHandler();

    public delegate void ColorSelectedEventHandler(object sender, eColor selectedColor);

    public partial class GameBoardForm : Form
    {
        private int m_NumberOfChances;
        private const int k_ButtonSize = 50;
        private const int m_SideMargin = 8;
        private ChancesForm m_ChanceForm;
        private List<Button> arrowButtons = new List<Button>();
        private List<Button> m_BlackButtons = new List<Button>();
        private bool m_UserWantsNewGame = false;
        private ColorMapper m_ColorMap;
        private ButtonCell[,] m_ButtonCellTable;
        private ButtonCell[,] m_FeedBackCells;
        private GameEngine m_GameEngine;
        private int[,] m_Checker;

        public event ColorSelectedEventHandler ColorSelected;

        public event EventHandler ArrowButtonClicked;

        public event EventHandler<ColorGuessEventArgs> ColorGuessSubmitted;

        //public event Action GameOver;
        //public event GameIsOverEventHandler GameIsOver;

        public GameBoardForm(ChancesForm i_ChanceForm)
        {
            m_ChanceForm = i_ChanceForm;
            m_NumberOfChances = i_ChanceForm.ChancesCount;
            m_Checker = new int[m_NumberOfChances, 4];
            m_ColorMap = new ColorMapper();

            InitializeComponent();
            InitializeGameSettings();
            GenerateGameEngine();
            InitializeGameBoard();
        }

        private void InitializeGameSettings()
        {
            m_NumberOfChances = m_ChanceForm.ChancesCount;
        }

        private void GenerateGameEngine()
        {
            m_GameEngine = new GameEngine(m_NumberOfChances);
        }

        /// <summary>
        /// when a cell is clicked. opens a pick a color form to pick the color of the cell and paints it.
        /// </summary>
        public void ButtonCell_Clicked(object sender, EventArgs e)
        {
            ButtonCell clickedButton = sender as ButtonCell;

            if (clickedButton != null)
            {
                PickAColorForm pickAColorForm = new PickAColorForm(clickedButton);
                pickAColorForm.ShowDialog();
                if (pickAColorForm.DialogResult == DialogResult.OK)
                {
                    m_Checker[clickedButton.RowIndex, clickedButton.ColumnIndex] = 1;
                }

                if (LineIsFull(clickedButton.RowIndex))
                {
                    arrowButtons[clickedButton.RowIndex].Enabled = true;
                }
                else
                {
                    arrowButtons[clickedButton.RowIndex].Enabled = false;
                }
            }
        }

        /// <summary>
        /// when line is full the arrow button is enabled. when user press it triggers the analization of the last guess of the user.
        /// </summary>
        public void ArrowButton_Clicked(object sender, EventArgs e)
        {
            Button arrowButton = sender as Button;
            if (arrowButton != null)
            {
                arrowButton.Enabled = false;
                int rowIndex = arrowButtons.IndexOf(arrowButton);
                EnabledNextLine(rowIndex + 1);
                StopEnabledLastLine(rowIndex);
                List<eColor> CurrentGuessColors = CollectColorsOfCurrentGuess(rowIndex);
                StartCheckingProccess(CurrentGuessColors, rowIndex);
            }
        }

        /// <summary>
        /// checks the last guess of the user. displayed the feedback of the guess and checks if the game has ended.
        /// </summary>
        private void StartCheckingProccess(List<eColor> i_CurrentGuessColors, int i_RowIndex)
        {
            string feedBackString = m_GameEngine.CheckGuess(i_CurrentGuessColors);
            UpdateFeedBackCells(feedBackString, i_RowIndex);
            if (m_GameEngine.IsGameOver(feedBackString))
            {
                RevealSecretCells();
                StopEnabledAll();
                //DisplayExitArrow();
                AskIfNewGame();
            }
        }

        /// <summary>
        /// opens a new form and asks the user if he wants to play again.
        /// </summary>
        private void AskIfNewGame()
        {
            AskIfNewGameForm askIfNewGameForm = new AskIfNewGameForm(this);
            askIfNewGameForm.ShowDialog();
            if (askIfNewGameForm.DialogResult == DialogResult.OK)
            {
                m_UserWantsNewGame = true;
            }
        }

        /// <summary>
        /// when the game is finished whether its a win or a lost the black cells reveals the secret colors.
        /// </summary>
        private void UpdateFeedBackCells(string i_FeedBackString, int i_RowIndex)
        {
            int feedBackCellIndex = 0;
            foreach (char feedBackLetter in i_FeedBackString)
            {
                if (feedBackLetter == 'V')
                {
                    m_FeedBackCells[i_RowIndex, feedBackCellIndex].BackColor = Color.Black;
                    feedBackCellIndex++;
                }
                else
                {
                    if (feedBackLetter == 'X')
                    {
                        m_FeedBackCells[i_RowIndex, feedBackCellIndex].BackColor = Color.Yellow;
                        feedBackCellIndex++;
                    }
                }
            }
        }

        public class ColorGuessEventArgs : EventArgs
        {
            public int RowIndex { get; }

            public List<eColor> ColorGuess { get; }

            public ColorGuessEventArgs(int rowIndex, List<eColor> colorGuess)
            {
                RowIndex = rowIndex;
                ColorGuess = colorGuess;
            }
        }

        /// <summary>
        /// creates a list of eColor of the last guess of the user.
        /// </summary>
        private List<eColor> CollectColorsOfCurrentGuess(int i_RowIndex)
        {
            List<eColor> colorList = new List<eColor>();
            for (int i = 0; i < 4; i++)
            {
                colorList.Add(m_ButtonCellTable[i_RowIndex, i].CellColor);
            }

            return colorList;
        }

        /// <summary>
        /// get an index of a line and enables the click function of the next line of cells.
        /// </summary>
        private void EnabledNextLine(int i_RowIndex)
        {
            if (i_RowIndex != m_NumberOfChances)
            {
                for (int j = 0; j < 4; j++)
                {
                    m_ButtonCellTable[i_RowIndex, j].Enabled = true;
                }
            }
        }

        /// <summary>
        /// after a full guess is made and the arrow button is pressed. stop enable clicks on the lst line of cells.
        /// </summary>
        private void StopEnabledLastLine(int i_RowIndex)
        {
            for (int j = 0; j < 4; j++)
            {
                m_ButtonCellTable[i_RowIndex, j].Enabled = false;
            }
        }

        /// <summary>
        /// when game is finished. stop enabling the click fnction of the rest of the board.
        /// </summary>
        private void StopEnabledAll()
        {
            for (int i = 0; i < m_NumberOfChances; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    m_ButtonCellTable[i, j].Enabled = false;
                }
            }
        }

        /// <summary>
        /// changed the color of the black secret cells with the color of the secret colors.
        /// </summary>
        private void RevealSecretCells()
        {
            int blackCellIndex = 0;
            List<eColor> SecretColors = m_GameEngine.GetColorsOfSecretWord();
            foreach (eColor color in SecretColors)
            {
                PaintAccordingtoRGBValue(m_BlackButtons[blackCellIndex], color);
                blackCellIndex++;
            }
        }

        /// <summary>
        /// paint the secret colors according to RGB values of the colors.
        /// </summary>
        private void PaintAccordingtoRGBValue(Button i_ButtonToPaint, eColor i_Color)
        {
            int rValue = m_ColorMap.TryGetValue(i_Color).R;
            int gValue = m_ColorMap.TryGetValue(i_Color).G;
            int bValue = m_ColorMap.TryGetValue(i_Color).B;
            i_ButtonToPaint.BackColor = Color.FromArgb(rValue, gValue, bValue);
        }

        /// <summary>
        /// initialize the board of the game.
        /// </summary>
        private void InitializeGameBoard()
        {
            for (int i = 0; i < 4; i++)
            {
                ButtonCell blackCell = new ButtonCell(0, i);
                blackCell.Size = new Size(k_ButtonSize, k_ButtonSize);
                blackCell.Location = new Point(m_SideMargin + (i * (k_ButtonSize + m_SideMargin)), m_SideMargin);
                blackCell.BackColor = Color.Black;
                blackCell.Enabled = false;
                m_BlackButtons.Add(blackCell);
                this.Controls.Add(blackCell);
            }

            m_FeedBackCells = new ButtonCell[m_NumberOfChances, 4];
            m_ButtonCellTable = new ButtonCell[m_NumberOfChances, 4];
            this.ClientSize = new Size(((7) * m_SideMargin) + (k_ButtonSize * 6), ((m_NumberOfChances + 1) * (m_SideMargin + k_ButtonSize) + m_SideMargin));

            for (int i = 0; i < m_NumberOfChances; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.m_ButtonCellTable[i, j] = new ButtonCell(i, j);
                    this.m_ButtonCellTable[i, j].Size = new Size(k_ButtonSize, k_ButtonSize);
                    this.m_ButtonCellTable[i, j].Location = new Point(m_SideMargin + (j * (k_ButtonSize + m_SideMargin)), (i * (k_ButtonSize + m_SideMargin)) + k_ButtonSize + 2 * m_SideMargin);
                    this.m_ButtonCellTable[i, j].Enabled = false;
                    m_ButtonCellTable[i, j].Click += ButtonCell_Clicked;
                    this.Controls.Add(m_ButtonCellTable[i, j]);
                }

                Button arrowButton = new Button();
                arrowButton.Enabled = false;
                arrowButton.Size = new Size(k_ButtonSize, 20);
                arrowButton.Location = new Point(5 * m_SideMargin + 4 * k_ButtonSize, m_SideMargin * 2 + m_SideMargin * (i + 2) + (k_ButtonSize * (i + 1)));
                arrowButton.Text = "-->>";
                arrowButton.Click += ArrowButton_Clicked;
                arrowButtons.Add(arrowButton);
                this.Controls.Add(arrowButton);

                int feedbackCellSize = k_ButtonSize / 2 - 5;
                for (int k = 0; k < 4; k++)
                {
                    int feedBackCellCol = k % 2;
                    int feedBackCellRow = k / 2;
                    m_FeedBackCells[i, k] = new ButtonCell(i, k);
                    m_FeedBackCells[i, k].Size = new Size(feedbackCellSize, feedbackCellSize);
                    m_FeedBackCells[i, k].Location = new Point(6 * m_SideMargin + 5 * k_ButtonSize + (feedbackCellSize + 3) * feedBackCellCol, m_SideMargin * feedBackCellRow + feedBackCellRow * feedbackCellSize + m_SideMargin * (i + 2) + (k_ButtonSize * (i + 1)));
                    m_FeedBackCells[i, k].Enabled = false;
                    this.Controls.Add(m_FeedBackCells[i, k]);
                }
            }

            for (int k = 0; k < 4; k++)
            {
                this.m_ButtonCellTable[0, k].Enabled = true; // enabling only first line for chosing.
            }
        }

        /// <summary>
        /// resets the current game.
        /// </summary>
        public void ResetGame()
        {
            m_GameEngine.Reset();
            //this.Dispose();
            //CleanBoard();
        }

        /// <summary>
        /// makes of the game board cells empty.
        /// </summary>
        private void CleanBoard()
        {
            foreach (ButtonCell buttonCell in this.m_ButtonCellTable)
            {
                buttonCell.BackColor = Color.Empty;
            }
        }

        /// <summary>
        /// single move is preformed. a button is painted.
        /// </summary>
        private void GameLogic_MovePerformed(int i_RowIndex, int i_ColumnIndex, eColor i_ColorSelected)
        {
            this.m_ButtonCellTable[i_RowIndex, i_ColumnIndex].CellColor = i_ColorSelected;
            m_Checker[i_RowIndex, i_ColumnIndex] = 1;
            if (LineIsFull(i_RowIndex))
            {
                m_Checker[i_RowIndex, 4] = 1;
                arrowButtons[i_RowIndex].Enabled = true;
            }
        }

        /// <summary>
        /// checks if a specific line is full. 4 different colors selected.
        /// </summary>
        /// <returns> truw or false.</returns>
        public bool LineIsFull(int i_NumberOfLineChecked)
        {
            List<eColor> colorsInLine = new List<eColor>();
            bool isFull = true;
            for (int i = 0; i < 4; i++)
            {
                eColor currentColor = m_ButtonCellTable[i_NumberOfLineChecked, i].CellColor;
                if (m_Checker[i_NumberOfLineChecked, i] != 1 || colorsInLine.Contains(currentColor))
                {
                    isFull = false;
                    break;
                }

                colorsInLine.Add(currentColor);
            }

            colorsInLine.Clear();
            return isFull;
        }

        // getter.
        public bool UserWantsNewGame
        {
            get { return m_UserWantsNewGame;}
        }

        private void DisplayExitArrow()
        {
            TextBox textBox = new TextBox();
            textBox.Text = " Λ";
            textBox.Location = new Point(m_FeedBackCells[0, 1].Location.X - 5 - m_SideMargin, m_BlackButtons[0].Location.Y - 5);
            textBox.Enabled = false;
            textBox.Multiline = false;
            textBox.Font = new Font(FontFamily.GenericSerif, k_ButtonSize - 30, FontStyle.Bold);
            textBox.BackColor = Color.Red;
            textBox.BorderStyle = BorderStyle.None;
            this.Controls.Add(textBox);
        }

        private void GameBoardForm_Load(object sender, EventArgs e)
        {
        }
    }
}