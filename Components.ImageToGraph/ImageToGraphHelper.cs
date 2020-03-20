using System.Drawing;

namespace Components.ImageToGraph
{
    public static class ImageToGraphHelper
    {
        public static int GrayScale(Color pixel)
        {
            return (int)((pixel.R * 0.3) + (pixel.G * 0.59) + (pixel.B * 0.11));
        }

        public static int Normalize(int rangeResult, int max)
        {
            if (rangeResult < 0)
            {
                return 0;
            }

            if (rangeResult > max)
            {
                return max;
            }

            return rangeResult;
        }
    }
}
