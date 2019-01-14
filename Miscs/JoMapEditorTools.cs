/*
** Jo Sega Saturn Engine
** Copyright (c) 2012-2019, Johannes Fetz (johannesfetz@gmail.com)
** All rights reserved.
**
** Redistribution and use in source and binary forms, with or without
** modification, are permitted provided that the following conditions are met:
**     * Redistributions of source code must retain the above copyright
**       notice, this list of conditions and the following disclaimer.
**     * Redistributions in binary form must reproduce the above copyright
**       notice, this list of conditions and the following disclaimer in the
**       documentation and/or other materials provided with the distribution.
**     * Neither the name of the Johannes Fetz nor the
**       names of its contributors may be used to endorse or promote products
**       derived from this software without specific prior written permission.
**
** THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
** ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
** WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
** DISCLAIMED. IN NO EVENT SHALL Johannes Fetz BE LIABLE FOR ANY
** DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
** (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
** LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
** ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
** (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
** SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using Paloma;

namespace JoMapEditor
{
    public static class JoMapEditorTools
    {
        public static Bitmap GetBitmap(string path)
        {
            try
            {
                Bitmap img = null;
                if (path.ToLower().EndsWith("tga"))
                    img = TargaImage.LoadTargaImage(path);
                else
                    img = new Bitmap(path);
                img.ReplaceColor(Editor.TransparentColor, Color.Transparent);
                return img;
            }
            catch (Exception ex)
            {
                Program.Logger.Error(String.Format("Fail to load texture : {0}", path), ex);
                throw new InvalidOperationException(String.Format("Fail to load texture : {0}", path), ex);
            }
        }

        public static void ReplaceColor(this Bitmap img, Color src, Color dest)
        {
            int srcColor = src.ToArgb();
            for (int y = 0; y < img.Height; ++y)
            {
                for (int x = 0; x < img.Width; ++x)
                {
                    if (img.GetPixel(x, y).ToArgb() == srcColor)
                        img.SetPixel(x, y, dest);
                }
            }
        }

        public static bool IsTransparent(this Bitmap img)
        {
            int transparentColor = Editor.TransparentColor.ToArgb();
            for (int y = 0; y < img.Height; ++y)
            {
                for (int x = 0; x < img.Width; ++x)
                {
                    Color c = img.GetPixel(x, y);
                    if (c.A > 0 || c.ToArgb() == transparentColor)
                        return false;
                }
            }
            return true;
        }

        public static bool ContainsColor(this Bitmap img, Color toFind)
        {
            for (int y = 0; y < img.Height; ++y)
            {
                for (int x = 0; x < img.Width; ++x)
                {
                    if (img.GetPixel(x, y) == toFind)
                        return true;
                }
            }
            return true;
        }

        public static Bitmap CropBitmap(Bitmap img, Rectangle cropArea)
        {
            return img.Clone(cropArea, img.PixelFormat);
        }

        public static Cursor CursorFromBitmap(Bitmap bmp)
        {
            var hicon = bmp.GetHicon();
            var cursor = new Cursor(hicon);
            var fi = typeof(Cursor).GetField("ownHandle", BindingFlags.NonPublic | BindingFlags.Instance);
            fi.SetValue(cursor, true);
            return cursor;
        }

        public static List<XmlNode> GetElementsByTagNameCaseInsensetive(this XmlNode node, string tag)
        {
            List<XmlNode> toReturn = new List<XmlNode>();
            foreach (XmlNode child in node.ChildNodes)
            {
                if (String.Compare(child.Name, tag, true) == 0)
                    toReturn.Add(child);
            }
            return toReturn;
        }

        public static string GetAttributeByNameCaseInsensetive(this XmlNode node, string attributeName)
        {
            foreach (XmlAttribute xmlAttribute in node.Attributes)
            {
                if (String.Compare(xmlAttribute.Name, attributeName, true) == 0)
                    return xmlAttribute.Value;
            }
            return null;
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height, image.PixelFormat);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

    }
}
