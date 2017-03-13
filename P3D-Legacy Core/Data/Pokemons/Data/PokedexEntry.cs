using Microsoft.Xna.Framework;

namespace P3D.Legacy.Core.Pokemon
{
    public class PokedexEntry
    {
        public string Species { get; }
        public string Text { get; }
        public float Weight { get; }
        public float Height { get; }
        public Color Color { get; }

        public PokedexEntry(string text, string species, float weight, float height, Color color)
        {
            Text = text;
            Species = species;
            Weight = weight;
            Height = height;
            Color = color;
        }
    }
}
