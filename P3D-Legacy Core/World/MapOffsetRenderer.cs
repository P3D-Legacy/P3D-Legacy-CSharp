using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Entities;
using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.World
{
    public class MapOffsetRenderer
    {
        private BasicEffect Effect { get; }

        public List<Entity> Entities => OffsetMaps.Values.SelectMany(m => m.Entities).ToList();
        public List<Entity> Floors => OffsetMaps.Values.SelectMany(m => m.Floors).ToList();

        public Dictionary<string, MapRenderer> OffsetMaps { get; } = new Dictionary<string, MapRenderer>();


        public MapOffsetRenderer()
        {
            Effect = Screen.Effect;
        }


        public void Update(GameTime gameTime)
        {
            foreach (var mapRenderer in OffsetMaps)
                mapRenderer.Value.Update(gameTime);

            SortEntities();
        }
        public void Draw()
        {
            Effect.View = Screen.Camera.View;
            Effect.Projection = Screen.Camera.Projection;

            foreach (var mapRenderer in OffsetMaps)
                mapRenderer.Value.Draw();
        }



        public void SortEntities()
        {
            foreach (var keyValuePair in OffsetMaps)
                keyValuePair.Value.SortEntities();
        }

        public void AddEntity(string mapName, Entity newEnt)
        {
            if (!OffsetMaps.ContainsKey(mapName))
                OffsetMaps.Add(mapName, new MapRenderer());

            OffsetMaps[mapName].Entities.Add(newEnt);
        }
        public void AddFloor(string mapName, Entity newEnt)
        {
            if (!OffsetMaps.ContainsKey(mapName))
                OffsetMaps.Add(mapName, new MapRenderer());

            OffsetMaps[mapName].Floors.Add(newEnt);
        }
        public void Clear()
        {
            foreach (var mapRenderer in OffsetMaps.Values)
                mapRenderer.Clear();
            OffsetMaps.Clear();
        }
    }
}