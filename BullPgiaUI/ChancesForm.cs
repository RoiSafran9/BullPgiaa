namespace BullPgiaUI
{
    using System;
    using System.Windows.Forms;

    public partial class ChancesForm : Form
    {
        private int chancesCount = 4;

        public ChancesForm()
        {
            InitializeComponent();
        }

        public int ChancesCount
        {
            get { return chancesCount; }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void chancesButton_Click(object sender, EventArgs e)
        {
            if (chancesCount < 10)
            {
                chancesCount++;
            }
            else
            {
                chancesCount = 4;
            }

            numberOfChancesButton.Text = $"Number of chances: {chancesCount}";
        }

        private void ChancesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }

        private void ChancesForm_Load(object sender, EventArgs e)
        {
        }
    }
}
