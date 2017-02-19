using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace P3D.Legacy.Core.Resources
{
    public static class ModelManager
    {
        private static Dictionary<string, Model> ModelList { get; } = new Dictionary<string, Model>();

        public static Model GetModel(string path)
        {
            var cContent = ContentPackManager.GetContentManager(path, ".xnb");

            var tKey = Path.Combine(cContent.RootDirectory, path);

            if (ModelList.Keys.Contains(tKey) == false)
            {
                var m = cContent.Load<Model>(path);

                ModelList.Add(tKey, CreateShallowCopy(m));
            }

            return ModelList[tKey];
        }

        /// <summary>
        /// This method creates a shallow copy of the Model class.
        /// And because there's no other way to properly do this, we access its Protected method MemberwiseClone (inherited from Object) via reflection.
        /// </summary>
        /// <param name="m">The model to copy.</param>
        private static Model CreateShallowCopy(Model m)
        {
            //I AM SO SORRY!!!
            var memberWiseCloneMethod = m.GetType().GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            return (Model) memberWiseCloneMethod.Invoke(m, new object[0]);
            //invokes the method on the passed in Model instance to create a shallow copy.
        }

        public static bool ModelExist(string path)
        {
            var cContent = ContentPackManager.GetContentManager(path, ".xnb");

            var p0 = Path.Combine(GameController.GamePath,cContent.RootDirectory, path + ".xnb");
            return !string.IsNullOrEmpty(path) && File.Exists(p0);
        }

        public static void Clear()
        {
            ModelList.Clear();
        }

        public static Texture2D DrawModelToTexture(string modelName, Vector2 texSize, Vector3 modelPosition, Vector3 cameraPosition, Vector3 cameraRotation, float scale, bool enableLight)
        {
            RenderTarget2D renderTarget = new RenderTarget2D(Core.GraphicsDevice, Convert.ToInt32(texSize.X), Convert.ToInt32(texSize.Y), false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8);
            Core.GraphicsDevice.SetRenderTarget(renderTarget);
            Core.GraphicsDevice.Clear(Color.Transparent);

            Core.GraphicsDevice.BlendState = BlendState.Opaque;
            Core.GraphicsDevice.SamplerStates[0] = Core.Sampler;

            Model m = GetModel(modelName);

            if (enableLight)
            {
                foreach (ModelMesh mesh in m.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        Lighting.UpdateLighting(effect, true);

                        effect.DirectionalLight0.DiffuseColor = new Vector3(0.7f);
                        effect.DirectionalLight1.DiffuseColor = new Vector3(0.7f);
                        effect.DirectionalLight2.DiffuseColor = new Vector3(0.7f);

                        effect.DirectionalLight0.Direction = new Vector3(0, 1, 0);
                        effect.DirectionalLight1.Direction = new Vector3(1, 0, 0);
                        effect.DirectionalLight2.Direction = new Vector3(0, 0, 1);

                        effect.DirectionalLight0.Enabled = true;
                        effect.DirectionalLight1.Enabled = true;
                        effect.DirectionalLight2.Enabled = true;
                    }
                }
            }

            m.Draw(Matrix.CreateFromYawPitchRoll(cameraRotation.X, cameraRotation.Y, cameraRotation.Z) * Matrix.CreateScale(new Vector3(scale)) * Matrix.CreateTranslation(modelPosition), Matrix.CreateLookAt(cameraPosition, modelPosition, Vector3.Up), Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), Core.GraphicsDevice.Viewport.AspectRatio, 0.1f, 10000f));

            Core.GraphicsDevice.SetRenderTarget(null);

            return renderTarget;
        }

        public static class Lighting
        {
            public static void UpdateLighting(BasicEffect effect, bool b)
            {

            }
        }
    }
}
