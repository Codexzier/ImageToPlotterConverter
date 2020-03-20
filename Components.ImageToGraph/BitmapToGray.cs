using System.Drawing;

namespace Components.ImageToGraph
{
    public class BitmapToGray
    {
        public Bitmap Convert(Bitmap bitmap, int threshold, int range)
        {
            var gray = new Bitmap(bitmap.Width, bitmap.Height);
            var max = 3 * 255;

            for (int iY = 0; iY < bitmap.Height; iY++)
            {
                for (int iX = 0; iX < bitmap.Width; iX++)
                {
                    var pixel = bitmap.GetPixel(iX, iY);

                    //int sum = pixel.R + pixel.G + pixel.B;

                    int sum = ImageToGraphHelper.GrayScale(pixel);
                    //gray.SetPixel(iX, iY, Color.FromArgb(255, res, res, res));
                    //continue;

                    if(sum > ImageToGraphHelper.Normalize(threshold - range, max) && sum < ImageToGraphHelper.Normalize(threshold + range, max))
                    {
                        gray.SetPixel(iX, iY, Color.Black);
                        continue;
                    }
                    
                    if (sum < ImageToGraphHelper.Normalize( threshold - range, max) || sum > ImageToGraphHelper.Normalize(threshold + range, max))
                    {
                        gray.SetPixel(iX, iY, Color.Gray);
                        continue;
                    }

                    gray.SetPixel(iX, iY, Color.White);
                }
            }

            return gray;
        }        
    }
}
