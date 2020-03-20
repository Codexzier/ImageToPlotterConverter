using Components.Ui.EventBus;
using ImageToPlotterConverter.Views.Main;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ImageToPlotterConverter.Views.Menu
{
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            this.InitializeComponent();
        }

        private void ButtonOpenMain_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();

            if (ofd.ShowDialog() != null)
            {
                if (string.IsNullOrEmpty(ofd.FileName))
                {
                    return;
                }

                var img = System.Drawing.Image.FromFile(ofd.FileName);
                var bit = ResizeImage(img, 320, 240);

                string path = $"{Environment.CurrentDirectory}\\temp";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                bit.Save($"{path}\\temp.tmp");

                EventbusManager.GetEventbus().OpenView<MainView>(new MainMessage($"{path}\\temp.tmp"));
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// https://stackoverflow.com/questions/1922040/how-to-resize-an-image-c-sharp
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(System.Drawing.Image image, int width, int height)
        {
            var destRect = new System.Drawing.Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
