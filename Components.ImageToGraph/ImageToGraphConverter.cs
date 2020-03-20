using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Components.ImageToGraph
{
    public class ImageToGraphConverter
    {
        public IDictionary<long, DrawPointItem> Convert(Bitmap bitmap, int threshold, int range, ImageToGraphState state)
        {
            if(state == null)
            {
                throw new ImageToGraphConverterException("state must be instatiated");
            }

            state.Progress = 0;
            state.ProgressEnd = bitmap.Height;

            var alt = new AlternatePoint();
            var toGray = new BitmapToGray();
            var r = toGray.Convert(bitmap, threshold, range);

            var sequenz = new List<DrawPointItem>();

            bool goToRight = true;
            for (int iY = 0; iY < r.Height; iY++)
            {
                if (goToRight)
                {
                    for (int iX = 0; iX < r.Width; iX++)
                    {
                        var dpi = alt.ScanToNextPossiblePoint(bitmap, iY, iX, threshold, range);
                        sequenz.Add(dpi);
                    }
                }
                else
                {
                    for (int iX = r.Width - 1; iX >= 0; iX--)
                    {
                        var dpi = alt.ScanToNextPossiblePoint(bitmap, iY, iX, threshold, range);
                        sequenz.Add(dpi);
                    }
                }

                state.Progress++;
                goToRight = !goToRight;
            }

            var gr = sequenz.GroupBy(g => $"{g.X}:{g.Y}");
            var dict = new Dictionary<long, DrawPointItem>();

            long index = 0;
            foreach (var item in gr.Select(s => s.First()))
            {
                dict.Add(index, item);
                index++;
            }
            return dict;
        }


    }
}
