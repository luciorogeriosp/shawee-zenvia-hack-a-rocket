using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace Tropical.Infrastructure.Images
{
    public class ImageTool
    {
        public static void DeleteImage(string path, bool pathWithExtension = true)
        {
            if (File.Exists(path + ((!pathWithExtension) ? ".jpg" : "")))
                File.Delete(path + ((!pathWithExtension) ? ".jpg" : ""));

            if (File.Exists(path + ((!pathWithExtension) ? ".gif" : "")))
                File.Delete(path + ((!pathWithExtension) ? ".gif" : ""));

            if (File.Exists(path + ((!pathWithExtension) ? ".png" : "")))
                File.Delete(path + ((!pathWithExtension) ? ".png" : ""));

        }

        public static Image ScaleImage(string img, int maxWidth, int maxHeight)
        {
            using (Image image = Image.FromFile(img))
            {
                var ratioX = (double)maxWidth / image.Width;
                var ratioY = (double)maxHeight / image.Height;
                var ratio = Math.Min(ratioX, ratioY);

                var newWidth = (int)(image.Width * ratio);
                var newHeight = (int)(image.Height * ratio);

                var newImage = new Bitmap(newWidth, newHeight);
                Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
                return newImage;
            }            
        }

        public static void Resize(System.Drawing.Image img, int resizedW, int resizedH, string filename)
        {
            //get the height and width of the image
            int originalW = img.Width;
            int originalH = img.Height;

            //get the new size based on the percentage change
            //int resizedW = (int)(originalW * percentage);
            //int resizedH = (int)(originalH * percentage);

            //create a new Bitmap the size of the new image
            Bitmap bmp = new Bitmap(resizedW, resizedH);
            //create a new graphic from the Bitmap
            Graphics graphic = Graphics.FromImage((System.Drawing.Image)bmp);
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //draw the newly resized image
            graphic.DrawImage(img, 0, 0, resizedW, resizedH);
            //dispose and free up the resources
            graphic.Dispose();
            //return the image

            ((System.Drawing.Image)bmp).Save(filename);

            bmp.Dispose();

        }

        public static void Resize(string img, int resizedW, int resizedH, string filename)
        {
            Image image = Image.FromFile(img);
            Resize(image, resizedW, resizedH, filename);
            image.Dispose();
        }

        /// <summary>
        /// method for cropping an image.
        /// </summary>
        /// <param name="img">the image to crop</param>
        /// <param name="width">new height</param>
        /// <param name="height">new width</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static void Crop(string ImageSource, string ImageDestination, int width, int height, int x, int y, int ResizeWidth, int ResizeHeight)
        {
            using (Bitmap bmp = Image.FromFile(ImageSource) as Bitmap)
            {
                using (Bitmap cropBmp = bmp.Clone(new Rectangle(x, y, width, height), bmp.PixelFormat))
                {
                    Resize(cropBmp as Image, ResizeWidth, ResizeHeight, ImageDestination);
                    //cropBmp.Save(ImageDestination);
                }
            }

            /*
            Image image = Image.FromFile(ImageSource);
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            bmp.SetResolution(80, 60);

            Graphics gfx = Graphics.FromImage(bmp);
            gfx.SmoothingMode = SmoothingMode.AntiAlias;
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
            gfx.DrawImage(image, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);

            bmp.Save(ImageDestination);

            // Dispose to free up resources
            image.Dispose();
            bmp.Dispose();
            gfx.Dispose();
             * */
        }


        public static Size GetImageSize(string filename)
        {
            Size s = new Size();

            using (Image image = Image.FromFile(filename))
            {
                s.Width = image.Width;
                s.Height = image.Height;
            }

            return s;            
        }

        public static void SaveImage(Image image, string ImageDestination)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height);
            //create a new graphic from the Bitmap
            Graphics graphic = Graphics.FromImage((System.Drawing.Image)bmp);
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //draw the newly resized image
            graphic.DrawImage(image, 0, 0, image.Width, image.Height);
            //dispose and free up the resources
            graphic.Dispose();
            //return the image

            ((System.Drawing.Image)bmp).Save(ImageDestination);

            bmp.Dispose();
        }

        public static string SaveImageToSection(int id, string caminho, string imagemOriginal, string imagemTemporaria)
        {
            string extensao = "";
            
            if (!String.IsNullOrEmpty(imagemOriginal))
                extensao = imagemOriginal.Substring(imagemOriginal.LastIndexOf(".") + 1);

            if (!String.IsNullOrEmpty(imagemTemporaria))
            {
                extensao = imagemTemporaria.Substring(imagemTemporaria.LastIndexOf(".") + 1);

                DeleteImage(caminho + "/" + id, false);
                DeleteImage(caminho + "/" + id + "t", false);

                string imagemTemporariaArquivo;
                string imagemTemporariaDiretorio;
                imagemTemporariaArquivo = imagemTemporaria.Substring(imagemTemporaria.LastIndexOf("\\"));
                imagemTemporariaDiretorio = imagemTemporaria.Replace(imagemTemporariaArquivo, "");

                System.IO.File.Move(imagemTemporaria, caminho + "/" + id + "." + extensao);
                System.IO.File.Move(imagemTemporariaDiretorio + "/" + imagemTemporariaArquivo.Replace(".", "_thumb."), caminho + "/" + id + "t." + extensao);
            }

            return extensao;
        }
    }
}
