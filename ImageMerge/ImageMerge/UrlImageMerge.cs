using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace ImageMerge
{
    public class UrlImageMerge : IImageMerge
    {
        private readonly Helper _helper;

        public UrlImageMerge() : this(new Helper())
        {
        }

        internal UrlImageMerge(Helper helper)
        {
            _helper = helper;
        }

        /// <summary>
        /// Merger URLs of images
        /// </summary>
        /// <param name="imageUrls">List of image URLs</param>
        /// <param name="proxy">Web Network Proxy setting</param>
        public Bitmap MergeSourceImages(List<String> imageUrls, WebProxy proxy = null)
        {
            // Convert images to Bitmaps
            List<Bitmap> bitmapList = ConvertUrlsToBitmaps(imageUrls, proxy);

            return _helper.Merge(bitmapList);
        }

        /// <summary>
        /// Download iamges from URL and return as Bitmaps
        /// </summary>
        /// <param name="imageUrls">List of image URLs</param>
        /// <param name="proxy">Web Network Proxy setting</param>
        /// <returns>List of images as Bitmaps</returns>
        private List<Bitmap> ConvertUrlsToBitmaps(List<String> imageUrls, WebProxy proxy = null)
        {
            List<Bitmap> bitmapList = new List<Bitmap>();

            // Loop URLs
            foreach (string imgUrl in imageUrls)
            {
                try
                {

                    WebClient wc = new WebClient();

                    // If proxy setting then set
                    if (proxy != null)
                        wc.Proxy = proxy;

                    // Download image
                    byte[] bytes = wc.DownloadData(imgUrl);
                    MemoryStream ms = new MemoryStream(bytes);
                    Image img = Image.FromStream(ms);

                    bitmapList.Add((Bitmap)img);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            return bitmapList;
        }

        public Bitmap MergeSourceImages(string folderPath, ImageFormat imageFormat)
        {
            throw new NotImplementedException();
        }

        public Bitmap MergeSourceImages(List<Bitmap> bitmaps)
        {
            throw new NotImplementedException();
        }
    }
}
