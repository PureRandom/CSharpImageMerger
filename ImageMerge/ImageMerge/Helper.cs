using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace ImageMerge
{
    public class Helper
    {

        /// <summary>
        /// Merge Bitmap images into one
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        public Bitmap Merge(IEnumerable<Bitmap> images)
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
}
