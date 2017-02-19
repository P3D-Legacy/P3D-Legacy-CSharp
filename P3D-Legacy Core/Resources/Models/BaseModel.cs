using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using P3D.Legacy.Core.Debug;
using P3D.Legacy.Core.Entities;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.Resources.Models
{
    public class BaseModel
    {
        public static List<VertexPositionNormalTexture> vertexData = new List<VertexPositionNormalTexture>();

        public VertexBuffer vertexBuffer;

        public int ID = 0;
        public void Setup()
        {
            vertexBuffer = new VertexBuffer(Core.GraphicsDevice, typeof(VertexPositionNormalTexture), vertexData.Count, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertexData.ToArray());
        }

        public void Draw(Entity Entity, Texture2D[] Textures)
        {
            Vector3 effectDiffuseColor = Screen.Effect.DiffuseColor;

            Screen.Effect.World = Entity.World;
            Screen.Effect.TextureEnabled = true;
            Screen.Effect.Alpha = Entity.Opacity;

            Screen.Effect.DiffuseColor = effectDiffuseColor * Entity.Shader;

            if (Screen.Level.IsDark)
            {
                Screen.Effect.DiffuseColor *= new Vector3(0.5f, 0.5f, 0.5f);
            }

            Core.GraphicsDevice.SetVertexBuffer(vertexBuffer);

            if (Convert.ToInt32(vertexBuffer.VertexCount / 3) > Entity.TextureIndex.Length)
            {
                int[] newTextureIndex = new int[Convert.ToInt32(vertexBuffer.VertexCount / 3) + 1];
                for (var i = 0; i <= Convert.ToInt32(vertexBuffer.VertexCount / 3); i++)
                {
                    if (Entity.TextureIndex.Length - 1 >= i)
                    {
                        newTextureIndex[i] = Entity.TextureIndex[i];
                    }
                    else
                    {
                        newTextureIndex[i] = 0;
                    }
                }
                Entity.TextureIndex = newTextureIndex;
            }

            bool isEqual = true;
            if (Entity.HasEqualTextures == -1)
            {
                Entity.HasEqualTextures = 1;
                int contains = Entity.TextureIndex[0];
                for (var index = 1; index <= Entity.TextureIndex.Length - 1; index++)
                {
                    if (contains != Entity.TextureIndex[index])
                    {
                        Entity.HasEqualTextures = 0;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
            }
            if (Entity.HasEqualTextures == 0)
            {
                isEqual = false;
            }

            if (isEqual)
            {
                if (Entity.TextureIndex[0] > -1)
                {
                    ApplyTexture(Textures[Entity.TextureIndex[0]]);

                    Core.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, Convert.ToInt32(vertexBuffer.VertexCount / 3));

                    DebugDisplay.DrawnVertices += Convert.ToInt32(vertexBuffer.VertexCount / 3);
                }
            }
            else
            {
                for (var i = 0; i <= vertexBuffer.VertexCount - 1; i += 3)
                {
                    if (Entity.TextureIndex[Convert.ToInt32(i / 3)] > -1)
                    {
                        ApplyTexture(Textures[Entity.TextureIndex[Convert.ToInt32(i / 3)]]);

                        Core.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, i, 1);
                        DebugDisplay.DrawnVertices += 1;
                    }
                }
            }

            Screen.Effect.DiffuseColor = effectDiffuseColor;
            if (DebugDisplay.MaxDistance < Entity.CameraDistance)
                DebugDisplay.MaxDistance = Convert.ToInt32(Entity.CameraDistance);
        }

        private void ApplyTexture(Texture2D texture)
        {
            Screen.Effect.Texture = texture;
            Screen.Effect.CurrentTechnique.Passes[0].Apply();
        }

        public static FloorModel FloorModel = new FloorModel();
        public static BlockModel BlockModel = new BlockModel();
        public static SlideModel SlideModel = new SlideModel();
        public static BillModel BillModel = new BillModel();
        public static SignModel SignModel = new SignModel();
        public static CornerModel CornerModel = new CornerModel();
        public static InsideCornerModel InsideCornerModel = new InsideCornerModel();
        public static StepModel StepModel = new StepModel();
        public static InsideStepModel InsideStepModel = new InsideStepModel();
        public static CliffModel CliffModel = new CliffModel();
        public static CliffInsideModel CliffInsideModel = new CliffInsideModel();
        public static CliffCornerModel CliffCornerModel = new CliffCornerModel();
        public static CubeModel CubeModel = new CubeModel();
        public static CrossModel CrossModel = new CrossModel();
        public static DoubleFloorModel DoubleFloorModel = new DoubleFloorModel();
        public static PyramidModel PyramidModel = new PyramidModel();

        public static StairsModel StairsModel = new StairsModel();
        public static BaseModel getModelbyID(int ID)
        {
            switch (ID)
            {
                case 0:
                    return FloorModel;
                case 1:
                    return BlockModel;
                case 2:
                    return SlideModel;
                case 3:
                    return BillModel;
                case 4:
                    return SignModel;
                case 5:
                    return CornerModel;
                case 6:
                    return InsideCornerModel;
                case 7:
                    return StepModel;
                case 8:
                    return InsideStepModel;
                case 9:
                    return CliffModel;
                case 10:
                    return CliffInsideModel;
                case 11:
                    return CliffCornerModel;
                case 12:
                    return CubeModel;
                case 13:
                    return CrossModel;
                case 14:
                    return DoubleFloorModel;
                case 15:
                    return PyramidModel;
                case 16:
                    return StairsModel;
                default:
                    return BlockModel;
            }
        }
    }
}
