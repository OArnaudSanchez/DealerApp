using System;
using System.Linq;
using DealerApp.Core.Enumerations;

namespace DealerApp.Infrastructure.Helpers
{
    public class ImageHelper
    {
        public static ImageFormat GetImageFormat(byte[] bytes)
        {
            var png = new byte[] { 137, 80, 78, 71 };
            var jpeg = new byte[] { 255, 216, 255, 224 };
            var jpg = new byte[] { 255, 216, 255, 225 };

            if (png.SequenceEqual(bytes.Take(png.Length)))
            {
                return ImageFormat.png;
            }

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
            {
                return ImageFormat.jpeg;
            }

            if (jpg.SequenceEqual(bytes.Take(jpg.Length)))
            {
                return ImageFormat.jpg;
            }

            return ImageFormat.unkown;
        }
    }
}