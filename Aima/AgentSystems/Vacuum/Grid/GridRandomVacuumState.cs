namespace Aima.AgentSystems.Vacuum.Grid
{
    public class GridRandomVacuumState : IState
    {
        public CellState State { get; }
        public Direction RandomState { get; }

        public GridRandomVacuumState(CellState state, Direction randomState)
        {
            State = state;
            RandomState = randomState;
        }
    }
}