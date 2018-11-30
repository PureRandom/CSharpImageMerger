using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;

namespace ImageMerge
{
    public interface IImageMerge
    {
        #region URL Merge

        /// <summary>
        /// Merger URLs of images
        /// </summary>
        /// <param name="imageUrls">List of image URLs</param>
        /// <param name="proxy">Web Network Proxy setting</param>
        Bitmap MergeSourceImages(List<String> imageUrls, WebProxy proxy = null);

        #endregion

        #region File Merge

        /// <summary>
        /// Merger images from folder
        /// </summary>
        /// <param name="folderPath">folder to path to where images are held</param>
        /// <param name="imageFormat">Format of wha the images are</param>
        Bitmap MergeSourceImages(string folderPath, ImageFormat imageFormat);

        #endregion

        #region Bitmap Merge

        /// <summary>
        /// Merger URLs of images
        /// </summary>
        /// <param name="bitmaps">List of image URLs</param>
        Bitmap MergeSourceImages(List<Bitmap> bitmaps);

        #endregion

    }
}
