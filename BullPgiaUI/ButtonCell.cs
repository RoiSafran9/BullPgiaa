namespace BullPgiaUI
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using BullPgiaLogic;
    using System.ComponentModel;

    public sealed partial class ButtonCell : Button
    {
        public EventHandler<ColorChangedEventArgs> ColorChanged;

        private readonly int r_ColumnIndex;
        private readonly int r_RowIndex;
        private eColor m_Color;

        public ButtonCell(int i_RowIndex, int i_ColumnIndex)
        {
            r_ColumnIndex = i_ColumnIndex;
            r_RowIndex = i_RowIndex;
            m_Color = eColor.Grey;
            Height = 100;
            Width = 100;
        }

        public eColor CellColor
        {
            get { return m_Color; }

            set { m_Color = value; }
        }

        public void PaintCell(eColor i_ColorChosed)
        {
            m_Color = i_ColorChosed;

            switch (i_ColorChosed)
            {
                case eColor.Purple:
                    this.BackColor = Color.FromArgb(153, 0, 153);
                    break;

                case eColor.Red:
                    this.BackColor = Color.FromArgb(255, 0, 0);
                    break;

                case eColor.Green:
                    this.BackColor = Color.FromArgb(0, 255, 0);
                    break;

                case eColor.Azure:
                    this.BackColor = Color.FromArgb(51, 255, 255);
                    break;

                case eColor.Blue:
                    this.BackColor = Color.FromArgb(0, 0, 255);
                    break;

                case eColor.Yellow:
                    this.BackColor = Color.FromArgb(255, 255, 0);
                    break;

                case eColor.Brown:
                    this.BackColor = Color.FromArgb(102, 0, 0);
                    break;

                case eColor.White:
                    this.BackColor = Color.FromArgb(255, 255, 255);
                    break;
                default:
                    break;
            }
        }

        public int RowIndex
        {
            get { return this.r_RowIndex; }
        }

        public int ColumnIndex
        {
            get { return this.r_ColumnIndex; }
        }

        public ButtonCell(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
    }
}