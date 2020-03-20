namespace Components.ImageToGraph
{
    public class DrawPointItem
    {
        public DrawPointItem(int x, int y, int grayScaleValue)
        {
            this.X = x;
            this.Y = y;
            this.GrayScaleValue = grayScaleValue;
        }

        public int X { get; }

        public int Y { get; }

        public int GrayScaleValue { get; }

        public override string ToString() => $"X={this.X}, Y={this.Y}, g={this.GrayScaleValue}";
    }
}