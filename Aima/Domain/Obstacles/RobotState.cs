using Aima.AgentSystems;

namespace Aima.Domain.Obstacles
{
    public class RobotState : IState
    {
        public int VertexId;

        public override int GetHashCode()
        {
            return VertexId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var s = obj as RobotState;
            if (s != null)
                return s.VertexId == VertexId;
            return base.Equals(obj);
        }
    }
}