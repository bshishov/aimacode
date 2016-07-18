using Aima.AgentSystems;

namespace Aima.Domain.River
{
    public static class RiverActions
    {
        public static readonly IAction Move1AToRight = new SimpleAction("Move 1xA To Right");
        public static readonly IAction Move2AToRight = new SimpleAction("Move 2xA To Right");

        public static readonly IAction Move1BToRight = new SimpleAction("Move 1xB To Right");
        public static readonly IAction Move2BToRight = new SimpleAction("Move 2xB To Right");

        public static readonly IAction MoveBothToRight = new SimpleAction("Move AB To Right");
    }
}