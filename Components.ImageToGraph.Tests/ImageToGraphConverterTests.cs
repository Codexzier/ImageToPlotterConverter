using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace Components.ImageToGraph.Tests
{
    [TestClass]
    public class ImageToGraphConverterTests
    {
        private readonly string _bitmapFile = $"{Environment.CurrentDirectory}\\ExampleBitmap.bmp";
        private readonly string _bitmapFile2 = $"{Environment.CurrentDirectory}\\TestPicture_01.bmp";

        [TestMethod]
        public void ImageToGraphConverterSimpleTest()
        {
            // arrange
            var converter = new ImageToGraphConverter();
            var bitmap = new Bitmap(this._bitmapFile);

            // act
            var result = converter.Convert(bitmap, 5, 5, new ImageToGraphState());

            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IMageToGraphConverterSequenzTest()
        {
            // arrange
            var converter = new ImageToGraphConverter();
            var bitmap = new Bitmap(this._bitmapFile);

            // act
            var result = converter.Convert(bitmap, 5, 5, new ImageToGraphState());

            // assert
            Assert.IsTrue(result.Count > 0, "squenz has no points");
        }

        [TestMethod]
        public void ImageToGraphConverterSmallPictureTest()
        {
            // arrange
            var converter = new ImageToGraphConverter();
            var bitmap = new Bitmap(this._bitmapFile2);

            // act
            var result = converter.Convert(bitmap, 5, 5, new ImageToGraphState());

            // assert
            Assert.IsTrue(result.Count > 24, "sequenz has lower than 25 points");
        }
    }
}
