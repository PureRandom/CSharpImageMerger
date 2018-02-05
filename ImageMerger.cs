using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;

/// <summary>
/// Merge a collection of images into one single image
/// </summary>
public static class ImageMerger
{

    #region Url Merging

    /// <summary>
    /// Merger URLs of images
    /// </summary>
    /// <param name="imageUrls">List of image URLs</param>
    /// <param name="proxy">Web Network Proxy setting</param>
    public static Bitmap MergeImages(List<String> imageUrls, WebProxy proxy = null)
    {
        // Convert images to Bitmaps
        List<Bitmap> bitmapList = ConvertUrlsToBitmaps(imageUrls, proxy);

        return Merge(bitmapList);
    }

    /// <summary>
    /// Download iamges from URL and return as Bitmaps
    /// </summary>
    /// <param name="imageUrls">List of image URLs</param>
    /// <param name="proxy">Web Network Proxy setting</param>
    /// <returns>List of images as Bitmaps</returns>
    private static List<Bitmap> ConvertUrlsToBitmaps(List<String> imageUrls, WebProxy proxy = null)
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

    #endregion

    #region File System Merging

    /// <summary>
    /// Merger images from folder
    /// </summary>
    /// <param name="folderPath">folder to path to where images are held</param>
    /// <param name="imageFormat">Format of wha the images are</param>
    public static Bitmap MergeImages(string folderPath, ImageFormat imageFormat)
    {
        // Convert images to Bitmaps
        List<Bitmap> bitmapList = ConvertUrlsToBitmaps(folderPath, imageFormat);

        return Merge(bitmapList);
    }

    /// <summary>
    /// Get images from file systems and return as Bitmaps
    /// </summary>
    /// <param name="folderPath">folder to path to where images are held</param>
    /// <param name="imageFormat">Format of wha the images are</param>
    /// <returns>List of images as Bitmaps</returns>
    private static List<Bitmap> ConvertUrlsToBitmaps(string folderPath, ImageFormat imageFormat)
    {
        List<Bitmap> bitmapList = new List<Bitmap>();

        List<string> imagesFromFolder = Directory.GetFiles(folderPath, "*." + imageFormat, SearchOption.AllDirectories).ToList();

        // Loop Files
        foreach (string imgPath in imagesFromFolder)
        {
            try
            {
                var bmp = (Bitmap) Image.FromFile(imgPath);

                bitmapList.Add(bmp);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
        return bitmapList;
    }

    #endregion

    #region Bitmap Merging

    /// <summary>
    /// Merger URLs of images
    /// </summary>
    /// <param name="bitmaps">List of image URLs</param>
    public static Bitmap MergeImages(List<Bitmap> bitmaps)
    {
        return Merge(bitmaps);
    }

    #endregion

    /// <summary>
    /// Merge Bitmap images into one
    /// </summary>
    /// <param name="images"></param>
    /// <returns></returns>
    private static Bitmap Merge(IEnumerable<Bitmap> images)
    {
        var enumerable = images as IList<Bitmap> ?? images.ToList();

        var width = 0;
        var height = 0;

        // Get max width and height of the image
        foreach (var image in enumerable)
        {
            width = image.Width > width
                ? image.Width
                : width;
            height = image.Height > height
                ? image.Height
                : height;
        }

        // merge images
        var bitmap = new Bitmap(width, height);
        using (var g = Graphics.FromImage(bitmap))
        {
            foreach (var image in enumerable)
            {
                g.DrawImage(image, 0, 0);
            }
        }
        return bitmap;
    }
}
