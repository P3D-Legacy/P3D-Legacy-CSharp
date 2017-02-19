namespace P3D.Legacy.Core.Pokemon
{
    public abstract class BallItem : Item
    {
        public override bool CanBeUsed { get; }
        public override ItemTypes ItemType { get; }
        public override int PokeDollarPrice { get; }
    }
}
