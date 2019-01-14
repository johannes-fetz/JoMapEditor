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

using System.Drawing;
using System.IO;

namespace JoMapEditor
{
    public static class TgaWriter
    {
        public static void WriteTga24Bits(Bitmap bitmap, string path, Color? transparentColor = null)
        {
            using (Stream file = File.Create(path))
            {
                TgaWriter.WriteTga24Bits(bitmap, file, transparentColor);
            }
        }

        public static void WriteTga24Bits(Bitmap bitmap, Stream output, Color? transparentColor = null)
        {
            using (BinaryWriter writer = new BinaryWriter(output))
            {
                writer.Write(new byte[]
                {
                    0, // ID length
                    0, // no color map
                    2, // uncompressed, true color
                    0, 0, 0, 0,
                    0,
                    0, 0, 0, 0, // x and y origin
                    (byte)(bitmap.Width & 0x00FF),
                    (byte)((bitmap.Width & 0xFF00) >> 8),
                    (byte)(bitmap.Height & 0x00FF),
                    (byte)((bitmap.Height & 0xFF00) >> 8),
                    24, // 24 bit bitmap
                    0
                });
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        Color c = bitmap.GetPixel(x, bitmap.Height - y - 1);
                        if (c.A != 255)
                        {
                            if (transparentColor.HasValue && transparentColor.Value.A == 255)
                                c = transparentColor.Value;
                            else
                                c = Editor.TransparentColor;
                        }
                        writer.Write(new[]
                        {
                            c.B,
                            c.G,
                            c.R
                        });
                    }
                }
            }
        }
    }
}
