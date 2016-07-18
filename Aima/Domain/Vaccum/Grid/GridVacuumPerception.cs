using Aima.AgentSystems;
using Aima.Utilities;

namespace Aima.Domain.Vaccum.Grid
{
    public class GridVacuumPerception : IPerception
    {
        public Point Position;
        public CellState State;
    }
}