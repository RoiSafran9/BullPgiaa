namespace BullPgiaUI
{
    using BullPgiaLogic;
    using System;

    public class ColorChangedEventArgs : EventArgs
    {
        public eColor NewColor { get; }

        public ColorChangedEventArgs(eColor i_newColor)
        {
            NewColor = i_newColor;
        }
    }

}
