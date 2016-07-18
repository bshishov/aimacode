using Aima.Utilities;

namespace Aima.Domain.TSP
{
    public struct City
    {
        public string Name;
        public Vector2 Position;

        public override string ToString()
        {
            return $"{Name} {Position}";
        }
    }
}