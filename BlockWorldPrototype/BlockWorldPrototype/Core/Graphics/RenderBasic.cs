// -----------------------------------------------------------------------
// <copyright file="RenderBasic.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework;
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
    public class RenderBasic
    {
        public Texture2D[] Textures;
        public Model Model;

        public RenderBasic(Model model, Effect effect)
        {
            List<Texture2D> textures = new List<Texture2D>();
            foreach (ModelMesh mesh in model.Meshes)
                foreach (BasicEffect currentEffect in mesh.Effects)
                    textures.Add(currentEffect.Texture);

            Textures = textures.ToArray();
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = effect.Clone();
                }
            }

            Model = model;
        }

        public void Draw(GraphicsDevice device, Camera3D camera, Vector3 position, Vector3 offset, float rotation, Vector3 scale)
        {
            int i = 0;
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (Effect currentEffect in mesh.Effects)
                {
                    Matrix worldMatrix = Matrix.CreateScale(scale) * Matrix.CreateRotationX(-1.57079633f) * Matrix.CreateRotationY(rotation) * Matrix.CreateTranslation((position.X * 0.5f) + offset.X, (position.Y * 0.5f) + offset.Y, (position.Z * 0.5f) + offset.Z);
                    currentEffect.Parameters["xTexture"].SetValue(Textures[i++]);
                    currentEffect.Parameters["xWorld"].SetValue(worldMatrix);
                    currentEffect.Parameters["xView"].SetValue(camera.ViewMatrix);
                    currentEffect.Parameters["xProjection"].SetValue(camera.ProjectionMatrix);
                }
                mesh.Draw();
            }
        }
    }
}
