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
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using JoMapEditor.SegaSaturn;

namespace JoMapEditor
{
    public class Collada
    {
        public class Option
        {
            public bool UseScreenDoors { get; set; }
            public bool UseLight { get; set; }
            public bool UseTexture { get; set; }
            public string WorkingDir { get; set; }
        }

        public Collada(XmlDocument doc, Option option)
        {
            this.Textures = new List<Texture>();
            this.Geometries = new List<Geometry>();
            this.ParseXmlDocument(doc, option);
        }

        private static readonly List<Color> DefaultColors = new List<Color> { Color.Red, Color.Green, Color.Blue, Color.Magenta };

        public string ToSourceFile(string filename, Option option)
        {
            List<SegaSaturnPolygonData> list = this.Geometries.ConvertAll(geometry => geometry.GetPolygonData(this.Textures, option));
            return SegaSaturnConverter.ToSourceFile(list, true, true, filename);
        }

        private void ParseXmlDocument(XmlDocument doc, Option option)
        {
            List<XmlNode> rootNodes = doc.GetElementsByTagNameCaseInsensetive("COLLADA");
            if (rootNodes.Count != 1)
                throw new InvalidOperationException("COLLADA node not found");
            XmlNode root = rootNodes.First();
            if (option.UseTexture)
                this.ParseTextures(root, option);
            this.ParseMesh(root, option);
            if (option.UseTexture && this.Textures.Count <= 0 && this.Geometries.Any(geometry => geometry.MeshSources.Any(source => source.MeshSourceKind == MeshSource.MeshSourceKindEnum.Texcoord)))
            {
                if (MessageBox.Show("No textures defined in this file. Would you like to select one on your disk ?", "COLLADA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (OpenFileDialog dlg = new OpenFileDialog())
                    {
                        dlg.Multiselect = false;
                        dlg.Filter = "All image files|*.*";
                        dlg.CheckFileExists = true;
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            this.Textures.Add(new Texture
                            {
                                Name = "Default",
                                Path = dlg.FileName
                            });
                        }
                    }
                }
            }
        }

        private void ParseTextures(XmlNode root, Option option)
        {
            foreach (XmlNode libraryImagesNode in root.GetElementsByTagNameCaseInsensetive("library_images"))
            {
                foreach (XmlNode imageNode in libraryImagesNode.GetElementsByTagNameCaseInsensetive("image"))
                {
                    foreach (XmlNode textureNode in imageNode.GetElementsByTagNameCaseInsensetive("init_from"))
                    {
                        string path = textureNode.InnerText;
                        if (path.StartsWith("file:", StringComparison.InvariantCultureIgnoreCase))
                            path = path.Substring(5);
                        path = Path.Combine(option.WorkingDir, path.TrimStart('/', '.'));
                        this.Textures.Add(new Texture
                        {
                            Path = path,
                            Id = imageNode.GetAttributeByNameCaseInsensetive("id"),
                            Name = imageNode.GetAttributeByNameCaseInsensetive("name")
                        });
                    }
                }
            }
        }

        private void ParseMeshInput(XmlNode rootNode, Geometry geometry)
        {
            foreach (XmlNode inputNode in rootNode.GetElementsByTagNameCaseInsensetive("input"))
            {
                string semantic = inputNode.GetAttributeByNameCaseInsensetive("semantic").ToUpperInvariant();
                string offset = inputNode.GetAttributeByNameCaseInsensetive("offset");

                int offsetValue = 0;
                if (!String.IsNullOrWhiteSpace(offset))
                    offsetValue = int.Parse(offset, CultureInfo.InvariantCulture);
                if (offsetValue > geometry.IndexOffsetRange)
                    geometry.IndexOffsetRange = offsetValue;

                string source = inputNode.GetAttributeByNameCaseInsensetive("source");
                MeshSource meshSource;
                if (source.StartsWith("#"))
                {
                    source = source.Substring(1);
                    meshSource = geometry.MeshSources.FirstOrDefault(item => item.Id == source);
                }
                else
                    meshSource = geometry.MeshSources.FirstOrDefault(item => item.Name == source);
                if (meshSource != null)
                {
                    meshSource.IndexOffset = offsetValue;
                    switch (semantic)
                    {
                        case "POSITION":
                            meshSource.MeshSourceKind = MeshSource.MeshSourceKindEnum.Position;
                            break;
                        case "NORMAL":
                            meshSource.MeshSourceKind = MeshSource.MeshSourceKindEnum.Normal;
                            break;
                        case "TEXCOORD":
                            meshSource.MeshSourceKind = MeshSource.MeshSourceKindEnum.Texcoord;
                            break;
                        case "VERTEX":
                            meshSource.MeshSourceKind = MeshSource.MeshSourceKindEnum.Vertex;
                            break;
                        case "TANGENT":
                            meshSource.MeshSourceKind = MeshSource.MeshSourceKindEnum.Tangent;
                            break;
                        case "BINORMAL":
                            meshSource.MeshSourceKind = MeshSource.MeshSourceKindEnum.Binormal;
                            break;
                    }
                }
            }
        }

        private void ParseMeshSource(XmlNode meshNode, Geometry geometry)
        {
            foreach (XmlNode sourceNode in meshNode.GetElementsByTagNameCaseInsensetive("source"))
            {
                MeshSource source = new MeshSource
                {
                    Id = sourceNode.GetAttributeByNameCaseInsensetive("id"),
                    Name = sourceNode.GetAttributeByNameCaseInsensetive("name"),
                    MeshSourceKind = MeshSource.MeshSourceKindEnum.Unknown
                };
                XmlNode floatArrayNode = sourceNode.GetElementsByTagNameCaseInsensetive("float_array").FirstOrDefault();
                if (floatArrayNode != null)
                {
                    List<string> floatArray = new List<string>(floatArrayNode.InnerText.Split(new[] { " ", "\t", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries));
                    source.Floats = floatArray.ConvertAll(item => float.Parse(item, CultureInfo.InvariantCulture));
                    geometry.MeshSources.Add(source);
                }
            }
            foreach (XmlNode verticesNode in meshNode.GetElementsByTagNameCaseInsensetive("vertices"))
                this.ParseMeshInput(verticesNode, geometry);
        }

        private void ParseMesh(XmlNode root, Option option)
        {
            int geometryIndex = 1;
            foreach (XmlNode libraryGeometriesNode in root.GetElementsByTagNameCaseInsensetive("library_geometries"))
            {
                foreach (XmlNode geometryNode in libraryGeometriesNode.GetElementsByTagNameCaseInsensetive("geometry"))
                {
                    foreach (XmlNode meshNode in geometryNode.GetElementsByTagNameCaseInsensetive("mesh"))
                    {
                        foreach (XmlNode polylistNode in meshNode.GetElementsByTagNameCaseInsensetive("polylist"))
                        {
                            string count = polylistNode.GetAttributeByNameCaseInsensetive("count");
                            if (String.IsNullOrWhiteSpace(count))
                                throw new InvalidOperationException("Missing count attribute in polylist");
                            Geometry geometry = new Geometry
                            {
                                Id = geometryNode.GetAttributeByNameCaseInsensetive("id"),
                                Name = geometryNode.GetAttributeByNameCaseInsensetive("name"),
                                PolygonCount = int.Parse(count, CultureInfo.InvariantCulture),
                                GeometryIndex = geometryIndex++
                            };
                            this.ParseMeshSource(meshNode, geometry);
                            this.Geometries.Add(geometry);
                            this.ParseMeshInput(polylistNode, geometry);
                            foreach (XmlNode vcountNode in polylistNode.GetElementsByTagNameCaseInsensetive("vcount"))
                            {
                                List<string> tmp = new List<string>(vcountNode.InnerText.Split(new[] { " ", "\t", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries));
                                List<int> vertexCountArray = tmp.ConvertAll(item => int.Parse(item, CultureInfo.InvariantCulture));
                                geometry.VertexCount.AddRange(vertexCountArray);
                                if (vertexCountArray.TrueForAll(item => item == 3))
                                    geometry.GeometryType = Geometry.GeometryTypeEnum.TrianglePolylist;
                                else if (vertexCountArray.TrueForAll(item => item == 4))
                                    geometry.GeometryType = Geometry.GeometryTypeEnum.QuadPolylist;
                                else if (vertexCountArray.TrueForAll(item => item == 3 || item == 4))
                                    geometry.GeometryType = Geometry.GeometryTypeEnum.Polygons;
                                else
                                {
                                    MessageBox.Show("Sorry, Ngons are not supported (only tris and quads)", Editor.Instance.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    throw new InvalidOperationException("Ngons are not supported");
                                }
                            }
                            foreach (XmlNode pNode in polylistNode.GetElementsByTagNameCaseInsensetive("p"))
                            {
                                List<string> vertexArray = new List<string>(pNode.InnerText.Split(new[] { " ", "\t", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries));
                                geometry.VertexIndexes.AddRange(vertexArray.ConvertAll(item => int.Parse(item, CultureInfo.InvariantCulture)));
                            }
                        }
                        foreach (XmlNode trianglesNode in meshNode.GetElementsByTagNameCaseInsensetive("triangles"))
                        {
                            string count = trianglesNode.GetAttributeByNameCaseInsensetive("count");
                            if (String.IsNullOrWhiteSpace(count))
                                throw new InvalidOperationException("Missing count attribute in triangles");
                            Geometry geometry = new Geometry
                            {
                                Id = geometryNode.GetAttributeByNameCaseInsensetive("id"),
                                Name = geometryNode.GetAttributeByNameCaseInsensetive("name"),
                                PolygonCount = int.Parse(count, CultureInfo.InvariantCulture),
                                GeometryIndex = geometryIndex++
                            };
                            this.ParseMeshSource(meshNode, geometry);
                            this.Geometries.Add(geometry);
                            this.ParseMeshInput(trianglesNode, geometry);
                            foreach (XmlNode pNode in trianglesNode.GetElementsByTagNameCaseInsensetive("p"))
                            {
                                List<string> vertexArray = new List<string>(pNode.InnerText.Split(new[] { " ", "\t", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries));
                                geometry.VertexIndexes.AddRange(vertexArray.ConvertAll(item => int.Parse(item, CultureInfo.InvariantCulture)));
                                geometry.GeometryType = Geometry.GeometryTypeEnum.Triangles;
                            }
                        }
                    }
                }
            }
        }

        public class MeshSource
        {
            public enum MeshSourceKindEnum
            {
                Unknown,
                Position,
                Normal,
                Texcoord,
                Vertex,
                Tangent,
                Binormal
            }

            public MeshSourceKindEnum MeshSourceKind { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }
            public List<float> Floats { get; set; }
            public int IndexOffset { get; set; }
        }

        public class Texture
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Path { get; set; }            
        }

        public class Geometry
        {
            public Geometry()
            {
                this.MeshSources = new List<MeshSource>();
                this.VertexIndexes = new List<int>();
                this.VertexCount = new List<int>();
            }

            public enum GeometryTypeEnum
            {
                Triangles,
                TrianglePolylist,
                QuadPolylist,
                /*Not Supported*/
                Polygons
            }

            public SegaSaturnPolygonData GetPolygonData(List<Texture> textures, Option option)
            {
                bool hasTexture = this.MeshSources.Any(item => item.MeshSourceKind == MeshSource.MeshSourceKindEnum.Texcoord);
                string name = this.Name ?? this.Id ?? this.GetHashCode().ToString();
                name = Regex.Replace(name, "[^A-Za-z0-9]", "");
                if (this.GeometryIndex > 1)
                    name += this.GeometryIndex.ToString();
                SegaSaturnPolygonData toReturn = new SegaSaturnPolygonData
                {
                    Points = this.GetPoints(),
                    Name = name
                };
                if (this.PolygonCount > 10000)
                {
                    MessageBox.Show(String.Format("{0} polygons({1}) ? Seriously !? It's a Sega Saturn not a PS4 :)", this.PolygonCount, toReturn.Name), Editor.Instance.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new InvalidOperationException("Too many polygons");
                }
                List<SegaSaturnVertices> vertices = this.GetVertices();
                List<SegaSaturnNormal> normals = this.GetNormals();
            retry:
                toReturn.Textures = new List<SegaSaturnTexture>();
                toReturn.Polygons = new List<SegaSaturnPolygon>();
                toReturn.Attributes = new List<SegaSaturnAttribute>();
                for (int i = 0; i < this.PolygonCount; ++i)
                {
                    toReturn.Polygons.Add(new SegaSaturnPolygon
                    {
                        Normal = normals[i],
                        Vertices = vertices[i]
                    });
                    toReturn.Attributes.Add(new SegaSaturnAttribute
                    {
                        FrontBackPlane = SegaSaturnAttribute.FrontBackPlaneEnum.Dual,
                        ZSortSpecification = SegaSaturnAttribute.ZSortSpecificationEnum.Cen,
                        Color = new SegaSaturnColor(Collada.DefaultColors[(this.GeometryIndex - 1) % Collada.DefaultColors.Count]),
                        UseLight = option.UseLight,
                        UseScreenDoors = option.UseScreenDoors
                    });
                }
                if (hasTexture)
                    toReturn.Textures = this.GetTextures(textures, toReturn.Attributes, 64);
                else
                    toReturn.Textures = new List<SegaSaturnTexture>();
                if (toReturn.Textures.Count > 64)
                {
                    if (MessageBox.Show(String.Format("{0} textured polygons({1}) is too much for the Sega Saturn but would you like to export the mesh without textures ?", this.PolygonCount, toReturn.Name), Editor.Instance.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        throw new InvalidOperationException("Too many polygons");
                    hasTexture = false;
                    goto retry;
                }
                return toReturn;
            }

            private List<SegaSaturnTexture> GetTextures(List<Texture> textureLibrary, List<SegaSaturnAttribute> attributes, int maxTextureSize)
            {
                if (textureLibrary == null || textureLibrary.Count <= 0)
                    return new List<SegaSaturnTexture>();
                if (textureLibrary.Count > 1)
                {
                    MessageBox.Show("Sorry, only one texture per file are supported yet :'(", Editor.Instance.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new InvalidOperationException("Too many textures");
                }
                MeshSource textureSource = this.MeshSources.FirstOrDefault(item => item.MeshSourceKind == MeshSource.MeshSourceKindEnum.Texcoord);
                if (textureSource == null)
                    return new List<SegaSaturnTexture>();
                List<SegaSaturnTexture> toReturn = new List<SegaSaturnTexture>();
                Dictionary<string, SegaSaturnTexture> dict = new Dictionary<string, SegaSaturnTexture>();
                List<SegaSaturnTextureVerticesIndexes> list = this.GetTextureVerticesIndexes();
                Texture tmp = textureLibrary.First();
                Bitmap img = JoMapEditorTools.GetBitmap(tmp.Path);
                if (img.Width > maxTextureSize && img.Height > maxTextureSize)
                {
                    img = JoMapEditorTools.ResizeImage(img, maxTextureSize, maxTextureSize);
                }
                else if (img.Width > maxTextureSize)
                {
                    img = JoMapEditorTools.ResizeImage(img, maxTextureSize, img.Height);
                }
                else if (img.Height > maxTextureSize)
                {
                    img = JoMapEditorTools.ResizeImage(img, img.Width, maxTextureSize);
                }
                for (int i = 0; i < list.Count; ++i)
                {
                    SegaSaturnTextureCoordinates p1 = Geometry.ConvertToSegaSaturnTextureCoordinates(img, textureSource.Floats, list[i].Vertice1);
                    SegaSaturnTextureCoordinates p2 = Geometry.ConvertToSegaSaturnTextureCoordinates(img, textureSource.Floats, list[i].Vertice2);
                    SegaSaturnTextureCoordinates p3 = Geometry.ConvertToSegaSaturnTextureCoordinates(img, textureSource.Floats, list[i].Vertice3);
                    SegaSaturnTextureCoordinates p4 = Geometry.ConvertToSegaSaturnTextureCoordinates(img, textureSource.Floats, list[i].Vertice4);
                    if (p1.Hash == p2.Hash && p2.Hash == p3.Hash)
                        continue;
                    SegaSaturnTexture texture = SegaSaturnTexture.ConvertFrom(img, tmp.Name ?? Path.GetFileNameWithoutExtension(tmp.Path), p1, p2, p3, p4, list[i].IsTriangleMapping);
                    if (!dict.ContainsKey(texture.Hash))
                    {
                        texture.Name += toReturn.Count.ToString();
                        dict.Add(texture.Hash, texture);
                        toReturn.Add(texture);
                    }
                    attributes[i].SpriteIndex = toReturn.FindIndex(item => item.Hash == texture.Hash);
                    attributes[i].Color = null;
                }
                return toReturn;
            }

            private static SegaSaturnTextureCoordinates ConvertToSegaSaturnTextureCoordinates(Image img, List<float> floats, int textureVerticesIndex)
            {
                return new SegaSaturnTextureCoordinates(floats[textureVerticesIndex * 2 + 0],
                                                        floats[textureVerticesIndex * 2 + 1],
                                                        img.Width,
                                                        img.Height);
            }

            private List<SegaSaturnPoint> GetPoints()
            {
                MeshSource positionSource = this.MeshSources.FirstOrDefault(item => item.MeshSourceKind == MeshSource.MeshSourceKindEnum.Position);
                if (positionSource == null)
                    throw new InvalidOperationException("no Position source in Collada");
                List<SegaSaturnPoint> toReturn = new List<SegaSaturnPoint>();

                float coef = 1.0f;
                float max = positionSource.Floats.Max();
                if (max < 50.0f)
                {
                    coef = 50.0f / max;
                }
                else
                {
                    coef = max / 50.0f;
                }
                for (int i = 0; i < positionSource.Floats.Count; i += 3)
                    toReturn.Add(new SegaSaturnPoint(positionSource.Floats[i + 0] * coef, positionSource.Floats[i + 1] * coef, positionSource.Floats[i + 2] * coef));
                return toReturn;
            }

            private List<SegaSaturnTextureVerticesIndexes> GetTextureVerticesIndexes()
            {
                MeshSource textureSource = this.MeshSources.FirstOrDefault(item => item.MeshSourceKind == MeshSource.MeshSourceKindEnum.Texcoord);
                if (textureSource == null)
                    throw new InvalidOperationException("no Texcoord source in Collada");
                if (this.GeometryType == GeometryTypeEnum.QuadPolylist)
                {
                    List<SegaSaturnTextureVerticesIndexes> toReturn = new List<SegaSaturnTextureVerticesIndexes>();
                    int i = textureSource.IndexOffset;
                    while (i < this.VertexIndexes.Count)
                    {
                        SegaSaturnTextureVerticesIndexes coord = new SegaSaturnTextureVerticesIndexes();
                        coord.Vertice1 = this.VertexIndexes[i];
                        i += this.IndexOffsetRange + 1;
                        coord.Vertice2 = this.VertexIndexes[i];
                        i += this.IndexOffsetRange + 1;
                        coord.Vertice3 = this.VertexIndexes[i];
                        i += this.IndexOffsetRange + 1;
                        coord.Vertice4 = this.VertexIndexes[i];
                        i += this.IndexOffsetRange + 1;
                        toReturn.Add(coord);
                    }
                    return toReturn;
                }
                if (this.GeometryType == GeometryTypeEnum.Polygons)
                {
                    int vcountIndex = 0;
                    List<SegaSaturnTextureVerticesIndexes> toReturn = new List<SegaSaturnTextureVerticesIndexes>();
                    int i = textureSource.IndexOffset;
                    while (i < this.VertexIndexes.Count)
                    {
                        SegaSaturnTextureVerticesIndexes coord = new SegaSaturnTextureVerticesIndexes();
                        coord.Vertice1 = this.VertexIndexes[i];
                        i += this.IndexOffsetRange + 1;
                        coord.Vertice2 = this.VertexIndexes[i];
                        i += this.IndexOffsetRange + 1;
                        coord.Vertice3 = this.VertexIndexes[i];
                        if (this.VertexCount[vcountIndex] == 4)
                            i += this.IndexOffsetRange + 1;
                        coord.Vertice4 = this.VertexIndexes[i];
                        i += this.IndexOffsetRange + 1;
                        toReturn.Add(coord);
                        ++vcountIndex;
                    }
                    return toReturn;
                }
                else
                {
                    List<SegaSaturnTextureVerticesIndexes> toReturn = new List<SegaSaturnTextureVerticesIndexes>();
                    int i = textureSource.IndexOffset;
                    while (i < this.VertexIndexes.Count)
                    {
                        SegaSaturnTextureVerticesIndexes coord = new SegaSaturnTextureVerticesIndexes();
                        coord.Vertice1 = this.VertexIndexes[i];
                        i += this.IndexOffsetRange + 1;
                        coord.Vertice2 = this.VertexIndexes[i];
                        i += this.IndexOffsetRange + 1;
                        coord.Vertice3 = this.VertexIndexes[i];
                        coord.Vertice4 = this.VertexIndexes[i];/*same as previous*/
                        i += this.IndexOffsetRange + 1;
                        toReturn.Add(coord);

                    }
                    return toReturn;
                }
            }

            private List<SegaSaturnVertices> GetVertices()
            {
                MeshSource positionSource = this.MeshSources.FirstOrDefault(item => item.MeshSourceKind == MeshSource.MeshSourceKindEnum.Position);
                if (positionSource == null)
                    throw new InvalidOperationException("no Position source in Collada");
                if (this.GeometryType == GeometryTypeEnum.QuadPolylist)
                {
                    List<SegaSaturnVertices> toReturn = new List<SegaSaturnVertices>();
                    int i = positionSource.IndexOffset;
                    while (i < this.VertexIndexes.Count)
                    {
                        SegaSaturnVertices vertices = new SegaSaturnVertices();
                        vertices.Vertice1 = this.VertexIndexes[i];
                        i += this.IndexOffsetRange + 1;
                        vertices.Vertice2 = this.VertexIndexes[i];
                        i += this.IndexOffsetRange + 1;
                        vertices.Vertice3 = this.VertexIndexes[i];
                        i += this.IndexOffsetRange + 1;
                        vertices.Vertice4 = this.VertexIndexes[i];
                        i += this.IndexOffsetRange + 1;
                        toReturn.Add(vertices);
                    }
                    return toReturn;
                }
                if (this.GeometryType == GeometryTypeEnum.Polygons)
                {
                    int vcountIndex = 0;
                    List<SegaSaturnVertices> toReturn = new List<SegaSaturnVertices>();
                    int i = positionSource.IndexOffset;
                    while (i < this.VertexIndexes.Count)
                    {
                        SegaSaturnVertices vertices = new SegaSaturnVertices();
                        vertices.Vertice1 = this.VertexIndexes[i];
                        i += this.IndexOffsetRange + 1;
                        vertices.Vertice2 = this.VertexIndexes[i];
                        i += this.IndexOffsetRange + 1;
                        vertices.Vertice3 = this.VertexIndexes[i];
                        if (this.VertexCount[vcountIndex] == 4)
                            i += this.IndexOffsetRange + 1;
                        vertices.Vertice4 = this.VertexIndexes[i];
                        i += this.IndexOffsetRange + 1;
                        toReturn.Add(vertices);
                        ++vcountIndex;
                    }
                    return toReturn;
                }
                else
                {
                    List<SegaSaturnVertices> toReturn = new List<SegaSaturnVertices>();
                    int i = positionSource.IndexOffset;
                    while (i < this.VertexIndexes.Count)
                    {
                        SegaSaturnVertices vertices = new SegaSaturnVertices();
                        vertices.Vertice1 = this.VertexIndexes[i];
                        i += this.IndexOffsetRange + 1;
                        vertices.Vertice2 = this.VertexIndexes[i];
                        i += this.IndexOffsetRange + 1;
                        vertices.Vertice3 = this.VertexIndexes[i];
                        vertices.Vertice4 = this.VertexIndexes[i];/*same as previous*/
                        i += this.IndexOffsetRange + 1;
                        toReturn.Add(vertices);
                    }
                    return toReturn;
                }
            }

            private List<SegaSaturnNormal> GetNormals()
            {
                MeshSource normalSource = this.MeshSources.FirstOrDefault(item => item.MeshSourceKind == MeshSource.MeshSourceKindEnum.Normal);
                if (normalSource == null)
                {
                    List<SegaSaturnNormal> toReturn = new List<SegaSaturnNormal>();
                    for (int i = 0; i < this.PolygonCount; ++i)
                        toReturn.Add(new SegaSaturnNormal(0.0f, 1.0f, 0.0f));
                    return toReturn;
                }
                if (this.GeometryType == GeometryTypeEnum.QuadPolylist)
                {
                    List<SegaSaturnNormal> toReturn = new List<SegaSaturnNormal>();
                    int i = normalSource.IndexOffset;
                    while (i < this.VertexIndexes.Count)
                    {
                        int index = this.VertexIndexes[i];
                        toReturn.Add(new SegaSaturnNormal(normalSource.Floats[index], normalSource.Floats[index + 1], normalSource.Floats[index + 2]));
                        i += (this.IndexOffsetRange + 1) * 4;
                    }
                    return toReturn;
                }
                if (this.GeometryType == GeometryTypeEnum.Polygons)
                {
                    int vcountIndex = 0;
                    List<SegaSaturnNormal> toReturn = new List<SegaSaturnNormal>();
                    int i = normalSource.IndexOffset;
                    while (i < this.VertexIndexes.Count)
                    {
                        int index = this.VertexIndexes[i];
                        toReturn.Add(new SegaSaturnNormal(normalSource.Floats[index], normalSource.Floats[index + 1], normalSource.Floats[index + 2]));
                        i += (this.IndexOffsetRange + 1) * this.VertexCount[vcountIndex];
                        ++vcountIndex;
                    }
                    return toReturn;
                }
                else
                {
                    List<SegaSaturnNormal> toReturn = new List<SegaSaturnNormal>();
                    int i = normalSource.IndexOffset;
                    while (i < this.VertexIndexes.Count)
                    {
                        int index = this.VertexIndexes[i];
                        toReturn.Add(new SegaSaturnNormal(normalSource.Floats[index], normalSource.Floats[index + 1], normalSource.Floats[index + 2]));
                        i += (this.IndexOffsetRange + 1) * 3;
                    }
                    return toReturn;
                }
            }

            public string Id { get; set; }
            public string Name { get; set; }
            public int PolygonCount { get; set; }
            public int IndexOffsetRange { get; set; }
            public int GeometryIndex { get; set; }

            public List<MeshSource> MeshSources { get; set; }
            public List<int> VertexIndexes { get; set; }
            public List<int> VertexCount { get; set; }
            public GeometryTypeEnum GeometryType { get; set; }
        }

        public List<Texture> Textures { get; set; }
        public List<Geometry> Geometries { get; set; }
    }
}
