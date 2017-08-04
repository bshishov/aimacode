using Aima.AgentSystems;

namespace Aima.Domain.River
{
    public class RiverState : IState
    {
        public int LeftA { get; set; }
        public int LeftB { get; set; }
        public int RightA { get; set; }
        public int RightB { get; set; }

        public bool IsValid => LeftA >= LeftB && RightA >= RightB;
    }
}