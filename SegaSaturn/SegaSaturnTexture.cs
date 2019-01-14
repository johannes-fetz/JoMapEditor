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
using AForge;
using AForge.Imaging.Filters;

namespace JoMapEditor.SegaSaturn
{
    public class SegaSaturnTexture
    {
        public Bitmap Image { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }

        private static Rectangle GetRectangleFromTextureCoordinates(SegaSaturnTextureCoordinates p1, SegaSaturnTextureCoordinates p2, SegaSaturnTextureCoordinates p3, SegaSaturnTextureCoordinates p4)
        {
            int x = p1.X;
            if (p2.X < x)
                x = p2.X;
            if (p3.X < x)
                x = p3.X;
            if (p4.X < x)
                x = p4.X;

            int x2 = p1.X;
            if (p2.X > x2)
                x2 = p2.X;
            if (p3.X > x2)
                x2 = p3.X;
            if (p4.X > x2)
                x2 = p4.X;

            int y = p1.Y;
            if (p2.Y < y)
                y = p2.Y;
            if (p3.Y < y)
                y = p3.Y;
            if (p4.Y < y)
                y = p4.Y;

            int y2 = p1.Y;
            if (p2.Y > y2)
                y2 = p2.Y;
            if (p3.Y > y2)
                y2 = p3.Y;
            if (p4.Y > y2)
                y2 = p4.Y;
            Rectangle rect = new Rectangle(x, y, x2 - x, y2 - y);
            if (rect.Width <= 0)
                rect.Width = rect.Height;
            if (rect.Height <= 0)
                rect.Height = rect.Width;
            return rect;
        }

        public static SegaSaturnTexture ConvertFrom(Bitmap image, string name, SegaSaturnTextureCoordinates p1, SegaSaturnTextureCoordinates p2, SegaSaturnTextureCoordinates p3, SegaSaturnTextureCoordinates p4, bool isTriangleMapping)
        {
            SegaSaturnTexture toReturn = new SegaSaturnTexture
            {
                Name = name,
                Hash = String.Format("{0}¤{1}¤{2}¤{3}", p1.Hash, p2.Hash, p3.Hash, p4.Hash)
            };
            Rectangle rect = SegaSaturnTexture.GetRectangleFromTextureCoordinates(p1, p2, p3, p4);
            if (!isTriangleMapping)
                toReturn.Image = JoMapEditorTools.CropBitmap(image, rect);
            else
            {
                List<IntPoint> corners = new List<IntPoint>();
                corners.Add(new IntPoint(p1.X, p1.Y));
                corners.Add(new IntPoint(p2.X, p2.Y));
                corners.Add(new IntPoint(p3.X, p3.Y));
                corners.Add(new IntPoint(p4.X, p4.Y));
                SimpleQuadrilateralTransformation filter = new SimpleQuadrilateralTransformation(corners, rect.Width, rect.Height);
                toReturn.Image = filter.Apply(image);
            }
            if ((toReturn.Image.Width % 8) == 0)
                toReturn.Image = toReturn.Image;
            else
            {
                int width = (toReturn.Image.Width / 8) * 8;
                if (width <= 0)
                    width = 8;
                toReturn.Image = JoMapEditorTools.ResizeImage(toReturn.Image, width, image.Height);
            }
            return toReturn;
        }
    }
}
