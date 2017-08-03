using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Debug;
using P3D.Legacy.Core.DebugC;
using P3D.Legacy.Core.Entities;
using P3D.Legacy.Core.Resources.Models;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.World
{
    public class MapRenderer
    {
        private BasicEffect Effect { get; }

        public List<Entity> Entities { get; protected set; } = new List<Entity>();
        public List<Entity> Floors { get; protected set; } = new List<Entity>();

        public MapRenderer()
        {
            Effect = Screen.Effect;
        }


        public void Update(GameTime gameTime)
        {
            for (var i = 0; i < Entities.Count; i++)
            {
                if (Entities[i].CanBeRemoved)
                {
                    Entities.RemoveAt(i);
                    i--;
                }
                else if (Entities[i].NeedsUpdate)
                    Entities[i].Update(gameTime);
            }


            for (var i = Floors.Count - 1; i >= 0; i--)
                Floors[i].UpdateEntity(gameTime);

            for (var i = Entities.Count - 1; i >= 0; i--)
                Entities[i].UpdateEntity(gameTime);

            SortEntities();
        }
        public void SortEntities() => Entities = Entities.OrderBy(e => e.CameraDistance).ToList();

        public void Draw()
        {
            Effect.View = Screen.Camera.View;
            Effect.Projection = Screen.Camera.Projection;

            for (var i = Floors.Count - 1; i >= 0; i--)
            {
                Floors[i].Render(Effect);
                RenderTracker.MaxVertices += Floors[i].VertexCount;
            }

            for (var i = Entities.Count - 1; i >= 0; i--)
            {
                Entities[i].Render(Effect);
                RenderTracker.MaxVertices += Entities[i].VertexCount;
            }
        }

        public void Clear()
        {
            Entities.Clear();
            Floors.Clear();
        }
    }
}
