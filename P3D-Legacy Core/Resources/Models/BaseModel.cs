using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Debug;
using P3D.Legacy.Core.DebugC;
using P3D.Legacy.Core.Entities;
using P3D.Legacy.Core.Resources.Models.Blocks;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.Resources.Models
{
    public abstract class BaseModel
    {
        public abstract int ID { get; }
        public abstract VertexBuffer VertexBuffer { get; }

        public Texture2D Texture { get; set; }
        public Matrix World { get; set; }

        //public BaseModel() { }

        public abstract void Draw(BasicEffect effect, Entity entity, Texture2D[] textures);

        public static FloorModel FloorModel => new FloorModel();
        public static BlockModel BlockModel => new BlockModel();
        public static BillModel BillModel => new BillModel();

        public static BaseModel GetModelByID(int id)
        {
            switch (id)
            {
                case 0:
                    return new FloorModel();
                case 1:
                    return new BlockModel();
                case 2:
                    return new SlideModel();
                case 3:
                    return new BillModel();
                case 4:
                    return new SignModel();
                case 5:
                    return new CornerModel();
                case 6:
                    return new InsideCornerModel();
                case 7:
                    return new StepModel();
                case 8:
                    return new InsideStepModel();
                case 9:
                    return new CliffModel();
                case 10:
                    return new CliffInsideModel();
                case 11:
                    return new CliffCornerModel();
                case 12:
                    return new CubeModel();
                case 13:
                    return new CrossModel();
                case 14:
                    return new DoubleFloorModel();
                case 15:
                    return new PyramidModel();
                case 16:
                    return new StairsModel();
                default:
                    return new BlockModel();
            }
        }
    }
    public abstract class BaseModel<T> : BaseModel
    {
        protected static List<BaseModel<T>> Models { get; } = new List<BaseModel<T>>();

        protected BaseModel() { Models.Add(this); }


        public static List<VertexPositionNormalTexture> VertexData { get; } = new List<VertexPositionNormalTexture>();
        private static VertexBuffer SVertexBuffer { get; set; }

        protected static List<int> IndexData { get; } = new List<int>();
        private static IndexBuffer SIndexBuffer { get; set; }

        protected static void SetupVb()
        {
            SVertexBuffer = new VertexBuffer(Core.GraphicsDevice, typeof(VertexPositionNormalTexture), VertexData.Count, BufferUsage.WriteOnly);
            SVertexBuffer.SetData(VertexData.ToArray());
            VertexData.Clear();
        }
        protected static void SetupIb()
        {
            SIndexBuffer = new IndexBuffer(Core.GraphicsDevice, typeof(int), IndexData.Count, BufferUsage.WriteOnly);
            SIndexBuffer.SetData(IndexData.ToArray());
            //VertexData.Clear();
        }

        public sealed override VertexBuffer VertexBuffer => SVertexBuffer;

        public override void Draw(BasicEffect effect, Entity entity, Texture2D[] textures)
        {
            Vector3 effectDiffuseColor = effect.DiffuseColor;

            effect.World = entity.World;
            effect.TextureEnabled = true;
            effect.Alpha = entity.Opacity;

            effect.DiffuseColor = effectDiffuseColor * entity.Shader;

            if (Screen.Level.IsDark)
                effect.DiffuseColor *= new Vector3(0.5f, 0.5f, 0.5f);

            Core.GraphicsDevice.SetVertexBuffer(VertexBuffer);

            if (VertexBuffer.VertexCount / 3 > entity.TextureIndex.Length)
            {
                int[] newTextureIndex = new int[VertexBuffer.VertexCount / 3 + 1];
                for (var i = 0; i <= VertexBuffer.VertexCount / 3; i++)
                {
                    if (entity.TextureIndex.Length - 1 >= i)
                    {
                        newTextureIndex[i] = entity.TextureIndex[i];
                    }
                    else
                    {
                        newTextureIndex[i] = 0;
                    }
                }
                entity.TextureIndex = newTextureIndex;
            }

            bool isEqual = true;
            if (entity.HasEqualTextures == -1)
            {
                entity.HasEqualTextures = 1;
                int contains = entity.TextureIndex[0];
                for (var index = 1; index <= entity.TextureIndex.Length - 1; index++)
                {
                    if (contains != entity.TextureIndex[index])
                    {
                        entity.HasEqualTextures = 0;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
            }
            if (entity.HasEqualTextures == 0)
            {
                isEqual = false;
            }

            if (isEqual)
            {
                if (entity.TextureIndex[0] > -1)
                {
                    ApplyTexture(effect, textures[entity.TextureIndex[0]]);

                    Core.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, VertexBuffer.VertexCount / 3);

                    RenderTracker.DrawnVertices += VertexBuffer.VertexCount / 3;
                }
            }
            else
            {
                for (var i = 0; i <= VertexBuffer.VertexCount - 1; i += 3)
                {
                    if (entity.TextureIndex[i / 3] > -1)
                    {
                        ApplyTexture(effect, textures[entity.TextureIndex[i / 3]]);

                        Core.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, i, 1);
                        RenderTracker.DrawnVertices += 1;
                    }
                }
            }

            Screen.Effect.DiffuseColor = effectDiffuseColor;
            if (RenderTracker.MaxDistance < entity.CameraDistance)
                RenderTracker.MaxDistance = (int) entity.CameraDistance;
        }

        /*
        public override void Draw(BasicEffect effect, Entity entity, Texture2D[] textures)
        {
            Vector3 effectDiffuseColor = effect.DiffuseColor;

            effect.World = entity.World;
            effect.TextureEnabled = true;
            effect.Alpha = entity.Opacity;

            effect.DiffuseColor = effectDiffuseColor * entity.Shader;

            if (Screen.Level.IsDark)
                effect.DiffuseColor *= new Vector3(0.5f);

            Core.GraphicsDevice.SetVertexBuffer(VertexBuffer);


            if (VertexBuffer.VertexCount / 3 > entity.TextureIndex.Length)
            {
                int[] newTextureIndex = new int[VertexBuffer.VertexCount / 3 + 1];
                for (var i = 0; i <= VertexBuffer.VertexCount / 3; i++)
                {
                    if (entity.TextureIndex.Length - 1 >= i)
                        newTextureIndex[i] = entity.TextureIndex[i];
                    else
                        newTextureIndex[i] = 0;
                }
                entity.TextureIndex = newTextureIndex;
            }

            //
            //for (var i = 0; i < entity.TextureIndex.Length; i++)
            //{
            //    if (entity.TextureIndex[i] > -1)
            //    {
            //        ApplyTexture(textures[entity.TextureIndex[i]]);
            //
            //        Core.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, i * 3, 3);
            //        DebugDisplay.DrawnVertices += 1;
            //    }
            //}
            //return;
            //


            bool isEqual = true;
            if (entity.HasEqualTextures == -1)
            {
                entity.HasEqualTextures = 1;
                int contains = entity.TextureIndex[0];
                for (var index = 1; index <= entity.TextureIndex.Length - 1; index++)
                {
                    if (contains != entity.TextureIndex[index])
                    {
                        entity.HasEqualTextures = 0;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
            }
            if (entity.HasEqualTextures == 0)
                isEqual = false;

            if (isEqual)
            {
                if (entity.TextureIndex[0] > -1)
                {
                    ApplyTexture(textures[entity.TextureIndex[0]]);

                    Core.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, VertexBuffer.VertexCount / 3);
                    DebugDisplay.DrawnVertices += VertexBuffer.VertexCount / 3;
                }
            }
            else
            {
                for (var i = 0; i < entity.TextureIndex.Length; i++)
                {
                    if (entity.TextureIndex[i] > -1)
                    {
                        ApplyTexture(textures[entity.TextureIndex[i]]);

                        Core.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, VertexBuffer.VertexCount / 3, 1);
                        DebugDisplay.DrawnVertices += 1;
                    }
                }


                for (var i = 0; i <= VertexBuffer.VertexCount - 1; i += 3)
                {
                    if (entity.TextureIndex[i / 3] > -1)
                    {
                        ApplyTexture(textures[entity.TextureIndex[i / 3]]);

                        Core.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, i, 1);
                        DebugDisplay.DrawnVertices += 1;
                    }
                }
            }

            effect.DiffuseColor = effectDiffuseColor;
            if (DebugDisplay.MaxDistance < entity.CameraDistance)
                DebugDisplay.MaxDistance = (int) entity.CameraDistance;
        }
        */

        protected void ApplyTexture(BasicEffect effect, Texture2D texture)
        {
            effect.Texture = texture;
            effect.CurrentTechnique.Passes[0].Apply();
        }
    }
}
