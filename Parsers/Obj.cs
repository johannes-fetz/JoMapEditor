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
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using JoMapEditor.SegaSaturn;

namespace JoMapEditor
{
    public class Obj
    {
        public class Option
        {
            public Option()
            {
                this.ZoomFactor = 1.0m;
                this.UseLight = true;
            }

            public decimal ZoomFactor { get; set; }
            public bool UseScreenDoors { get; set; }
            public bool UseLight { get; set; }
            public bool UseTexture { get; set; }
            public bool DualPlane { get; set; }
            public string WorkingDir { get; set; }
        }

        private static readonly List<Color> DefaultColors = new List<Color> { Color.Red, Color.Green, Color.Blue, Color.Magenta };

        public static string ToSourceFile(string filename, List<SegaSaturnPolygonData> list)
        {
            return SegaSaturnConverter.ToSourceFile(list, true, true, filename);
        }

        public List<SegaSaturnPolygonData> Parse(string path, Option option)
        {
            List<SegaSaturnPolygonData> toReturn = new List<SegaSaturnPolygonData>();
            Dictionary<string, Material> material = new Dictionary<string, Material>();
            Dictionary<string, SegaSaturnTexture> dict = new Dictionary<string, SegaSaturnTexture>();
            List<SegaSaturnNormal> normals = new List<SegaSaturnNormal>();
            List<SegaSaturnTextureCoordinates> textureCoordinates = new List<SegaSaturnTextureCoordinates>();
            SegaSaturnPolygonData currentPolygonData = new SegaSaturnPolygonData();
            Material currentMaterial = new Material();
            bool newMesh = false;
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(new[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length <= 0)
                        continue;
                    switch (data[0].ToLower())
                    {
                        case "usemtl":
                            if (material.ContainsKey(data[1]))
                                currentMaterial = material[data[1]];
                            else
                                currentMaterial = new Material();
                            break;
                        case "mtllib":
                            material = this.ParseMaterial(data[1], option.WorkingDir, 128);
                            break;
                        case "g":
                            if (String.IsNullOrWhiteSpace(currentPolygonData.Name))
                                currentPolygonData.Name = data[1];
                            break;
                        case "v":
                            if (newMesh)
                            {
                                dict.Clear();
                                newMesh = false;
                                if (currentPolygonData != null)
                                {
                                    if (String.IsNullOrWhiteSpace(currentPolygonData.Name))
                                        currentPolygonData.Name = String.Format("Unnamed{0}", toReturn.Count + 1);
                                    toReturn.Add(currentPolygonData);
                                }
                                //normals = new List<SegaSaturnNormal>();
                                //textureCoordinates = new List<SegaSaturnTextureCoordinates>();
                                currentMaterial = new Material();
                                currentPolygonData = new SegaSaturnPolygonData();
                            }
                            float x = float.Parse(data[1], CultureInfo.InvariantCulture) * (float)option.ZoomFactor;
                            float y = float.Parse(data[2], CultureInfo.InvariantCulture) * (float)option.ZoomFactor;
                            float z = float.Parse(data[3], CultureInfo.InvariantCulture) * (float)option.ZoomFactor;
                            currentPolygonData.Points.Add(new SegaSaturnPoint(x, y, z));
                            break;
                        case "vn":
                            normals.Add(new SegaSaturnNormal(float.Parse(data[1], CultureInfo.InvariantCulture), float.Parse(data[2], CultureInfo.InvariantCulture), float.Parse(data[3], CultureInfo.InvariantCulture)));
                            break;
                        case "vt":
                            textureCoordinates.Add(new SegaSaturnTextureCoordinates(float.Parse(data[1], CultureInfo.InvariantCulture), float.Parse(data[2], CultureInfo.InvariantCulture)));
                            break;
                        case "f":
                            newMesh = true;
                            if (data.Length > 5)
                                throw new NotSupportedException("Ngons are not supported");
                            string v;
                            string vt;
                            string vn;
                            this.ParseFace(data[1], out v, out vt, out vn);
                            bool hasTexture = option.UseTexture && !String.IsNullOrEmpty(vt) && currentMaterial.Texture != null;
                            bool hasNormal = !String.IsNullOrEmpty(vn);
                            SegaSaturnAttribute attributes = new SegaSaturnAttribute
                            {
                                Color = hasTexture ? null : new SegaSaturnColor(Obj.DefaultColors[toReturn.Count % Obj.DefaultColors.Count]),
                                ZSortSpecification = SegaSaturnAttribute.ZSortSpecificationEnum.Cen,
                                FrontBackPlane = option.DualPlane ? SegaSaturnAttribute.FrontBackPlaneEnum.Dual : SegaSaturnAttribute.FrontBackPlaneEnum.Single,
                                UseScreenDoors = option.UseScreenDoors,
                                UseLight = option.UseLight
                            };
                            currentPolygonData.Attributes.Add(attributes);
                            SegaSaturnPolygon polygon = new SegaSaturnPolygon();
                            SegaSaturnTextureVerticesIndexes textureVertices = new SegaSaturnTextureVerticesIndexes();
                            if (hasNormal)
                                polygon.Normal = normals[int.Parse(vn, CultureInfo.InvariantCulture) - 1];
                            polygon.Vertices = new SegaSaturnVertices();
                            //vertice #1
                            polygon.Vertices.Vertice1 = int.Parse(v, CultureInfo.InvariantCulture) - 1;
                            textureVertices.Vertice1 = !String.IsNullOrWhiteSpace(vt) ? int.Parse(vt, CultureInfo.InvariantCulture) - 1 : 0;
                            //vertice #2
                            this.ParseFace(data[2], out v, out vt, out vn);
                            polygon.Vertices.Vertice2 = int.Parse(v, CultureInfo.InvariantCulture) - 1;
                            textureVertices.Vertice2 = !String.IsNullOrWhiteSpace(vt) ? int.Parse(vt, CultureInfo.InvariantCulture) - 1 : 0;
                            //vertice #3
                            this.ParseFace(data[3], out v, out vt, out vn);
                            polygon.Vertices.Vertice3 = int.Parse(v, CultureInfo.InvariantCulture) - 1;
                            textureVertices.Vertice3 = !String.IsNullOrWhiteSpace(vt) ? int.Parse(vt, CultureInfo.InvariantCulture) - 1 : 0;
                            //vertice #4
                            if (data.Length <= 4)
                            {
                                polygon.Vertices.Vertice4 = polygon.Vertices.Vertice3;
                                textureVertices.Vertice4 = textureVertices.Vertice3;
                            }
                            else
                            {
                                this.ParseFace(data[4], out v, out vt, out vn);
                                polygon.Vertices.Vertice4 = int.Parse(v, CultureInfo.InvariantCulture) - 1;
                                textureVertices.Vertice4 = !String.IsNullOrWhiteSpace(vt) ? int.Parse(vt, CultureInfo.InvariantCulture) - 1 : 0;
                            }
                            // Textures
                            if (hasTexture)
                            {
                                SegaSaturnTextureCoordinates p1 = textureCoordinates[textureVertices.Vertice1];
                                p1.ComputeTextureCoordinates(currentMaterial.Texture.Width, currentMaterial.Texture.Height);
                                SegaSaturnTextureCoordinates p2 = textureCoordinates[textureVertices.Vertice2];
                                p2.ComputeTextureCoordinates(currentMaterial.Texture.Width, currentMaterial.Texture.Height);
                                SegaSaturnTextureCoordinates p3 = textureCoordinates[textureVertices.Vertice3];
                                p3.ComputeTextureCoordinates(currentMaterial.Texture.Width, currentMaterial.Texture.Height);
                                SegaSaturnTextureCoordinates p4 = textureCoordinates[textureVertices.Vertice4];
                                p4.ComputeTextureCoordinates(currentMaterial.Texture.Width, currentMaterial.Texture.Height);
                                if (p1.Hash != p2.Hash || p2.Hash != p3.Hash)
                                {
                                    SegaSaturnTexture texture = SegaSaturnTexture.ConvertFrom(currentMaterial.Texture, currentMaterial.Name ?? Path.GetFileNameWithoutExtension(currentMaterial.TexturePath), p1, p2, p3, p4, textureVertices.IsTriangleMapping);
                                    if (!dict.ContainsKey(texture.Hash))
                                    {
                                        texture.Name += currentPolygonData.Textures.Count.ToString();
                                        dict.Add(texture.Hash, texture);
                                        currentPolygonData.Textures.Add(texture);
                                    }
                                    attributes.SpriteIndex = currentPolygonData.Textures.FindIndex(item => item.Hash == texture.Hash);
                                    attributes.Color = null;
                                }
                            }
                            currentPolygonData.Polygons.Add(polygon);
                            break;
                        default:
                            break;
                    }
                }
            }
            if (currentPolygonData != null)
            {
                if (String.IsNullOrWhiteSpace(currentPolygonData.Name))
                    currentPolygonData.Name = String.Format("Unnamed{0}", toReturn.Count + 1);
                toReturn.Add(currentPolygonData);
            }
            return toReturn;
        }

        private void ParseFace(string data, out string v, out string vt, out string vn)
        {
            string[] face = data.Split('/');
            v = face[0].Trim(' ', '\t', '-');
            if (face.Length > 1)
                vt = face[1].Trim(' ', '\t', '-');
            else
                vt = null;
            if (face.Length > 2)
                vn = face[2].Trim(' ', '\t', '-');
            else
                vn = null;
        }

        private class Material
        {
            public string Name { get; set; }
            public string TexturePath { get; set; }
            public Bitmap Texture { get; set; }
        }

        private Dictionary<string, Material> ParseMaterial(string filename, string dir, int maxTextureSize)
        {
            Dictionary<string, Material> toReturn = new Dictionary<string, Material>();
            string path = Path.Combine(dir, filename);
            if (!File.Exists(path))
            {
                if (MessageBox.Show(String.Format("File {0} doesn't exists. Would you like to select it manually ?", path), "Wavefront material file", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (OpenFileDialog dlg = new OpenFileDialog())
                    {
                        dlg.Multiselect = false;
                        dlg.Filter = "Wavefront material file (MTL)|*.mtl";
                        dlg.CheckFileExists = true;
                        if (dlg.ShowDialog() != DialogResult.OK)
                            return toReturn;
                        path = dlg.FileName;
                    }
                }
                else
                    return toReturn;
            }
            Material currentMaterial = null;
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(new[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length <= 0)
                        continue;
                    switch (data[0].ToLower())
                    {
                        case "newmtl":
                            if (currentMaterial != null)
                                toReturn.Add(currentMaterial.Name, currentMaterial);
                            currentMaterial = new Material
                            {
                                Name = data[1]
                            };
                            break;
                        case "map_kd":
                            string texturePath = Path.Combine(dir, data[1]);
                            if (File.Exists(texturePath))
                            {
                                currentMaterial.TexturePath = texturePath;
                                try
                                {
                                    currentMaterial.Texture = JoMapEditorTools.GetBitmap(currentMaterial.TexturePath);
                                    if (currentMaterial.Texture.Width > maxTextureSize && currentMaterial.Texture.Height > maxTextureSize)
                                    {
                                        currentMaterial.Texture = JoMapEditorTools.ResizeImage(currentMaterial.Texture, maxTextureSize, maxTextureSize);
                                    }
                                    else if (currentMaterial.Texture.Width > maxTextureSize)
                                    {
                                        currentMaterial.Texture = JoMapEditorTools.ResizeImage(currentMaterial.Texture, maxTextureSize, currentMaterial.Texture.Height);
                                    }
                                    else if (currentMaterial.Texture.Height > maxTextureSize)
                                    {
                                        currentMaterial.Texture = JoMapEditorTools.ResizeImage(currentMaterial.Texture, currentMaterial.Texture.Width, maxTextureSize);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Program.Logger.Error(String.Format("Texture {0} format is not supported", texturePath), ex);
                                    throw new NotSupportedException(String.Format("Texture {0} format is not supported", texturePath), ex);
                                }
                            }
                            break;
                    }
                }
            }
            if (currentMaterial != null)
                toReturn.Add(currentMaterial.Name, currentMaterial);
            return toReturn;
        }
    }
}
