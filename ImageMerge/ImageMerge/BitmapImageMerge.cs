using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;

namespace ImageMerge
{
    public class BitmapImageMerge : IImageMerge
    {
        private readonly Helper _helper;

        public BitmapImageMerge() : this(new Helper())
        {
        }

        internal BitmapImageMerge(Helper helper)
        {
            _helper = helper;
        }

        /// <summary>
        /// Merger URLs of images
        /// </summary>
        /// <param name="bitmaps">List of image URLs</param>
        public Bitmap MergeSourceImages(List<Bitmap> bitmaps)
        {
            return _helper.Merge(bitmaps);
        }

        public Bitmap MergeSourceImages(List<string> imageUrls, WebProxy proxy = null)
        {
            throw new NotImplementedException();
        }

        public Bitmap MergeSourceImages(string folderPath, ImageFormat imageFormat)
        {
            throw new NotImplementedException();
        }
    }
}
