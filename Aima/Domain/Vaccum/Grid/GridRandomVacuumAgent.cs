using System;
using Aima.AgentSystems;
using Aima.AgentSystems.Agents;
using Aima.Utilities;

namespace Aima.Domain.Vaccum.Grid
{
    public class GridRandomVacuumAgent : SimpleReflexAgent<GridVacuumPerception, GridRandomVacuumState>
    {
        private readonly Random _random;

        public GridRandomVacuumAgent(int seed = 0)
        {
            _random = seed != 0 ? new Random(seed) : new Random();

            Rules.Add(new Rule<GridRandomVacuumState>(p => p.State == CellState.Dirty, VacuumActions.Suck));
            Rules.Add(new Rule<GridRandomVacuumState>(p => p.RandomState == Direction.Left, VacuumActions.Left));
            Rules.Add(new Rule<GridRandomVacuumState>(p => p.RandomState == Direction.Right, VacuumActions.Right));
            Rules.Add(new Rule<GridRandomVacuumState>(p => p.RandomState == Direction.Up, VacuumActions.Up));
            Rules.Add(new Rule<GridRandomVacuumState>(p => p.RandomState == Direction.Down, VacuumActions.Down));
        }

        public override GridRandomVacuumState InterpretInput(GridVacuumPerception percept)
        {
            return new GridRandomVacuumState(percept.State, (Direction)_random.Next(4));
        }
    }

    public enum Direction
    {
        Left = 0,
        Right = 1,
        Up = 2,
        Down = 3
    }

    public class GridVacuumNonObservableState : IState
    {
        public Point Position;
        public CellState CellState;
        public Direction SelectedMovement;
    }
}