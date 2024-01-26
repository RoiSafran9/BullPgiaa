namespace Ex02
{
    using BullPgiaLogic;
    using System.Collections.Generic;

    /// <summary>
    /// dictionary of colors and each color's RGB values.
    /// </summary>
    public class ColorMapper
    {
        private readonly Dictionary<eColor, RGBValue> r_Colormap = new Dictionary<eColor, RGBValue>();
        
        public ColorMapper()
        {
            r_Colormap[eColor.Grey] = new RGBValue(128, 128, 128);
            r_Colormap[eColor.Black] = new RGBValue(0, 0, 0);
            r_Colormap[eColor.Purple] = new RGBValue(128, 0, 128);
            r_Colormap[eColor.Red] = new RGBValue(255, 0, 0);
            r_Colormap[eColor.Green] = new RGBValue(0, 255, 0);
            r_Colormap[eColor.Azure] = new RGBValue(0, 255, 255);
            r_Colormap[eColor.Blue] = new RGBValue(0, 0, 255);
            r_Colormap[eColor.Yellow] = new RGBValue(255, 255, 0);
            r_Colormap[eColor.Brown] = new RGBValue(102, 0, 0);
            r_Colormap[eColor.White] = new RGBValue(255, 255, 255);
        }

        public Dictionary<eColor, RGBValue> ColorMap
        { 
            get { return r_Colormap; } 
        }

        public RGBValue TryGetValue(eColor i_Color)
        {
            return r_Colormap[i_Color];
        }
    }
}
