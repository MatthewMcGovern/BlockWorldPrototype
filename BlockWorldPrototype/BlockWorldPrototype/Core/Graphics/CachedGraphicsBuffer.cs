// -----------------------------------------------------------------------
// <copyright file="CachedGraphicsBuffer.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework.Graphics;

namespace BlockWorldPrototype.Core.Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CachedGraphicsBuffer<T> where T : struct
    {
        public GraphicsDevice Device;
        public VertexDeclaration VertexDeclaration;
        private VertexBuffer _vertexBuffer;
        private IndexBuffer _indexBuffer;
        private List<T> _vertices;
        private List<ushort> _indices;
        private const ushort Max = 65535;

        private bool _readyToDraw;

        public CachedGraphicsBuffer(GraphicsDevice device, VertexDeclaration vertexDeclaration)
        {
            VertexDeclaration = vertexDeclaration;
            Device = device;

            _readyToDraw = false;

            _vertices = new List<T>();
            _indices = new List<ushort>();
        }

        public bool AddData(List<T> vertices, List<ushort> indices)
        {
            ushort offset = (ushort)_vertices.Count();

            _vertices.AddRange(vertices);

            foreach (ushort index in indices)
            {
                _indices.Add((ushort)(index + offset));
            }

            return true;
        }

        public void PrepareToDraw()
        {
            if (_vertices.Count == 0 || _indices.Count == 0)
            {
                _readyToDraw = false;
                return;
            }
            _vertexBuffer = new VertexBuffer(Device, VertexDeclaration, _vertices.Count, BufferUsage.WriteOnly);
            _vertexBuffer.SetData(_vertices.ToArray());

            _indexBuffer = new IndexBuffer(Device, typeof(ushort), _indices.Count, BufferUsage.WriteOnly);
            _indexBuffer.SetData(_indices.ToArray());

            _indices.Clear();
            _vertices.Clear();

            _readyToDraw = true;
        }

        public void Draw()
        {
            if (_readyToDraw)
            {
                Device.SetVertexBuffer(_vertexBuffer);
                Device.Indices = _indexBuffer;
                Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _vertexBuffer.VertexCount, 0, _indexBuffer.IndexCount / 3);
            }
        }
    }
}
