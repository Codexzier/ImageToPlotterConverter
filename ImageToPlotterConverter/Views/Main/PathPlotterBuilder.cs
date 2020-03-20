using Components.ImageToGraph;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ImageToPlotterConverter.Views.Main
{
    public class PathPlotterBuilder
    {
        public Path CreatePathLine(IDictionary<long, DrawPointItem> segments, Brush brush, int height, int width, double scale)
        {
            var pf = new PathFigure
            {
                StartPoint = new Point(0, 0)
            };

            foreach (var item in segments)
            {
                pf.Segments.Add( new LineSegment(new Point(item.Value.X * scale, item.Value.Y * scale), true));
            }

            var pg = new PathGeometry();
            pg.Figures.Add(pf);

            var p = new Path
            {
                Width = width,
                Height = height,
                Margin = new Thickness(0, 0, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Stroke = brush,
                StrokeThickness = 1,
                Data = pg
            };

            return p;
        }
    }
}
