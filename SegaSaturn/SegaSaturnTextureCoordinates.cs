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

namespace JoMapEditor.SegaSaturn
{
    public class SegaSaturnTextureCoordinates
    {
        public SegaSaturnTextureCoordinates(float x, float y)
        {
            this.originalX = x;
            this.originalY = y;
        }

        public SegaSaturnTextureCoordinates(float x, float y, int imageWidth, int imageHeight) : this(x, y)
        {
            this.ComputeTextureCoordinates(imageWidth, imageHeight);
        }

        public void ComputeTextureCoordinates(int imageWidth, int imageHeight)
        {
            this.X = (int)(imageWidth * this.originalX);
            if (this.X < 0)
                this.X = 0;
            if (this.X > imageWidth)
                this.X = imageWidth;
            this.Y = (int)(imageHeight * this.originalY);
            if (this.Y < 0)
                this.Y = 0;
            if (this.Y > imageWidth)
                this.Y = imageWidth;
        }

        public int X { get; set; }
        public int Y { get; set; }
        
        public string Hash
        {
            get
            {
                return String.Format("{0}¤{1}", this.X, this.Y);
            }
        }

        protected readonly float originalX;
        protected readonly float originalY;
    }
}
