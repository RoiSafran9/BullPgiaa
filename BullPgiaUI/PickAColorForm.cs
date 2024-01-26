namespace BullPgiaUI
{
    using BullPgiaLogic;
    using System;
    using System.Windows.Forms;

    public partial class PickAColorForm : Form
    {
        public event ColorSelectedEventHandler ColorSelected;

        public readonly ColorSelectedEventHandler m_ColorSelectedEventHandler;

        public ButtonCell ButtonFromGameBoardForm { get; private set; }

        public PickAColorForm(ButtonCell buttonFromGameBoardForm)
        {
            InitializeComponent();
            ButtonFromGameBoardForm = buttonFromGameBoardForm;
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            ButtonCell colorButton = sender as ButtonCell;
            if (colorButton != null)
            {
                eColor selectedColor = colorButton.CellColor;
                ColorSelected?.Invoke(this, selectedColor);
                ButtonFromGameBoardForm.PaintCell(selectedColor);
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void PickAColorForm_Load(object sender, EventArgs e)
        {
        }
    }
}
