using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Components.ImageToGraph
{
    public class AlternatePoint
    {
        public DrawPointItem ScanToNextPossiblePoint(Bitmap bitmap, int iY, int iX, int threshold, int range)
        {
            var rad = range / 2;

            if(rad == 0)
            {
                rad = 1;
            }

            var collect = new List<DrawPointItem>();

            for (int iRangeY = iY - rad; iRangeY < iY + rad; iRangeY++)
            {
                for (int iRangeX = iX - rad; iRangeX < iX + rad; iRangeX++)
                {
                    int targetY = iRangeY;
                    int targetX = iRangeX;

                    //bool isMinOutRangeY = false;
                    //bool isMaxOutRangeY = false;

                    //bool isMinOutRangeX = false;
                    //bool isMaxOutRangeX = false;

                    if (targetY < 0)
                    {
                        //isMinOutRangeY = true;
                        targetY = 0;
                    }

                    if (targetY >= bitmap.Height)
                    {
                        //isMaxOutRangeY = true;
                        targetY = bitmap.Height - 1;
                    }

                    if (targetX < 0)
                    {
                        //isMinOutRangeX = true;
                        targetX = 0;
                    }

                    if (targetX >= bitmap.Width)
                    {
                        //isMaxOutRangeX = true;
                        targetX = bitmap.Width - 1;
                    }

                   
                    //if (isMinOutRangeY || isMaxOutRangeY)
                    //{   
                    //}
                    //else
                    //{
                    //    targetY = iRangeY;
                    //}

                    //if(isMinOutRangeX || isMaxOutRangeX)
                    //{
                    //}
                    //else
                    //{
                    //    targetX = iRangeX;
                    //}

                    var t = ImageToGraphHelper.GrayScale(bitmap.GetPixel(targetX, targetY));
                    collect.Add(new DrawPointItem(targetX, targetY, t));
                }
            }

            var max = collect.Min(m => m.GrayScaleValue);

            if (collect.All(a => a.GrayScaleValue == max))
            {
                var t = ImageToGraphHelper.GrayScale(bitmap.GetPixel(iX, iY));
                return new DrawPointItem(iX, iY, t);
            }

            var result = collect.First(f => f.GrayScaleValue <= max);

            return result;
        }
    }
}
