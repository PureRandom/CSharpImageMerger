using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;

namespace ImageMerge
{
    public class FileImageMerge : IImageMerge
    {
        private readonly Helper _helper;

        public FileImageMerge() : this(new Helper())
        {
        }

        internal FileImageMerge(Helper helper)
        {
            _helper = helper;
        }

        /// <summary>
        /// Merger images from folder
        /// </summary>
        /// <param name="folderPath">folder to path to where images are held</param>
        /// <param name="imageFormat">Format of wha the images are</param>
        public Bitmap MergeSourceImages(string folderPath, ImageFormat imageFormat)
        {
            // Convert images to Bitmaps
            List<Bitmap> bitmapList = ConvertUrlsToBitmaps(folderPath, imageFormat);

            return _helper.Merge(bitmapList);
        }

        /// <summary>
        /// Get images from file systems and return as Bitmaps
        /// </summary>
        /// <param name="folderPath">folder to path to where images are held</param>
        /// <param name="imageFormat">Format of wha the images are</param>
        /// <returns>List of images as Bitmaps</returns>
        private List<Bitmap> ConvertUrlsToBitmaps(string folderPath, ImageFormat imageFormat)
        {
            List<Bitmap> bitmapList = new List<Bitmap>();

            List<string> imagesFromFolder = Directory.GetFiles(folderPath, "*." + imageFormat, SearchOption.AllDirectories).ToList();

            // Loop Files
            foreach (string imgPath in imagesFromFolder)
            {
                try
                {
                    var bmp = (Bitmap)Image.FromFile(imgPath);

                    bitmapList.Add(bmp);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            return bitmapList;
        }

        public Bitmap MergeSourceImages(List<string> imageUrls, WebProxy proxy = null)
        {
            throw new NotImplementedException();
        }

        public Bitmap MergeSourceImages(List<Bitmap> bitmaps)
        {
            throw new NotImplementedException();
        }
    }
}
