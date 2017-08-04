using Aima.AgentSystems;

namespace Aima.Domain.Vaccum.Grid
{
    public class GridRandomVacuumState : IState
    {
        public GridRandomVacuumState(CellState state, Direction randomState)
        {
            State = state;
            RandomState = randomState;
        }

        public CellState State { get; }
        public Direction RandomState { get; }
    }
}