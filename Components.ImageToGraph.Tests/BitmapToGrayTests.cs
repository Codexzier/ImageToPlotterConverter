using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.IO;

namespace Components.ImageToGraph.Tests
{
    [TestClass]
    public class BitmapToGrayTests
    {
        private readonly string _bitmapFile = $"{Environment.CurrentDirectory}\\ExampleBitmap.bmp";

        [TestMethod]
        public void BitmapToGraySimpleTest()
        {
            // arrange
            var toGray = new BitmapToGray();
            var bitmap = new Bitmap(this._bitmapFile);

            // act
            var result = toGray.Convert(bitmap, 500, 50);

            // assert
            Assert.IsNotNull(result);
            //Sresult.Save($"{Environment.CurrentDirectory}\\ResultBitmap.bmp");
        }

        [Ignore]
        [TestMethod]
        public void BitmapToGrayDifferentTest()
        {
            // arrange
            Directory.CreateDirectory($"{Environment.CurrentDirectory}\\Results");
            var toGray = new BitmapToGray();
            var bitmap = new Bitmap(this._bitmapFile);

            // act
            for (int i = 40; i < 100; i++)
            {
                var result = toGray.Convert(bitmap, i, 30);
                result.Save($"{Environment.CurrentDirectory}\\Results\\ResultBitmap_{i}.bmp");
            }
            
            // assert

        }
    }
}
