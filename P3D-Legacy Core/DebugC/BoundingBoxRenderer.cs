using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace P3D.Legacy.Core.Debug
{
    public class BoundingBoxRenderer
    {

        #region "Fields"

        static VertexPositionColor[] verts = new VertexPositionColor[9];
        static Int16[] indices = new Int16[] {
        0,
        1,
        1,
        2,
        2,
        3,
        3,
        0,
        0,
        4,
        1,
        5,
        2,
        6,
        3,
        7,
        4,
        5,
        5,
        6,
        6,
        7,
        7,
        4
    };

        static BasicEffect effect;
        #endregion

        public static void Render(BoundingBox box, GraphicsDevice graphicsDevice, Matrix view, Matrix projection, Color color)
        {
            if ((effect == null) == true)
            {
                effect = new BasicEffect(graphicsDevice);
                effect.VertexColorEnabled = true;
                effect.LightingEnabled = false;
            }

            Vector3[] corners = box.GetCorners();
            for (var i = 0; i <= 7; i++)
            {
                verts[i].Position = corners[i];
                verts[i].Color = color;
            }

            effect.View = view;
            effect.Projection = projection;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.LineList, verts, 0, 8, indices, 0, Convert.ToInt32(indices.Length / 2));
            }
        }

        public static void Render(BoundingFrustum frustum, GraphicsDevice graphicsDevice, Matrix view, Matrix projection, Color color)
        {
            if ((effect == null) == true)
            {
                effect = new BasicEffect(graphicsDevice);
                effect.VertexColorEnabled = true;
                effect.LightingEnabled = false;
            }

            Vector3[] corners = frustum.GetCorners();
            for (var I = 0; I <= 7; I++)
            {
                verts[I].Position = corners[I];
                verts[I].Color = color;
            }

            effect.View = view;
            effect.Projection = projection;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.LineList, verts, 0, 8, indices, 0, Convert.ToInt32(indices.Length / 2));
            }

        }

    }
}
