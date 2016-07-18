using Aima.AgentSystems;

namespace Aima.Domain.SlidingTiles
{
    public static class SlidingTilesActions
    {
        public static readonly IAction Top = new SimpleAction("TOP");
        public static readonly IAction Left = new SimpleAction("LEFT");
        public static readonly IAction Right = new SimpleAction("RIGHT");
        public static readonly IAction Bottom = new SimpleAction("BOTTOM");
    }
}