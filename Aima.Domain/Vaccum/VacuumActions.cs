using Aima.AgentSystems;

namespace Aima.Domain.Vaccum
{
    public static class VacuumActions
    {
        public static IAction Suck = new SimpleAction("Suck");
        public static IAction Left = new SimpleAction("Left");
        public static IAction Right = new SimpleAction("Right");
        public static IAction Up = new SimpleAction("Up");
        public static IAction Down = new SimpleAction("Down");
    }
}