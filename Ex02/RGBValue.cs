namespace Ex02
{
    /// <summary>
    /// type that hold 3 int values which together represents a color.
    /// </summary>
    public class RGBValue
    {
        private readonly int r_R;
        private readonly int r_G;
        private readonly int r_B;

        public RGBValue(int i_R, int i_G, int i_B)
        {
            r_R = i_R;
            r_G = i_G;
            r_B = i_B;
        }

        public int R
        {
            get { return r_R; }
        }

        public int G
        {
            get { return r_G; }
        }

        public int B
        {
            get { return r_B; }
        }

    }
}
