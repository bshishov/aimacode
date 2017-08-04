using System;
using System.Collections.Generic;
using System.Linq;
using Aima.AgentSystems;
using Aima.AgentSystems.Agents;
using Aima.Utilities;

namespace Aima.Domain.Vaccum.Grid
{
    public class GridVacuumModelAgent : ModelBasedReflexAgent<GridVacuumPerception, GridVacuumNonObservableState>
    {
        private readonly List<CellInfo> _model = new List<CellInfo>();
        private readonly Random _random = new Random();

        public GridVacuumModelAgent()
        {
            Rules.Add(new Rule<GridVacuumNonObservableState>(s => s.CellState == CellState.Dirty, VacuumActions.Suck));
            Rules.Add(new Rule<GridVacuumNonObservableState>(s => s.SelectedMovement == Direction.Left,
                VacuumActions.Left));
            Rules.Add(new Rule<GridVacuumNonObservableState>(s => s.SelectedMovement == Direction.Right,
                VacuumActions.Right));
            Rules.Add(new Rule<GridVacuumNonObservableState>(s => s.SelectedMovement == Direction.Up, VacuumActions.Up));
            Rules.Add(new Rule<GridVacuumNonObservableState>(s => s.SelectedMovement == Direction.Down,
                VacuumActions.Down));
        }

        public override GridVacuumNonObservableState UpdateState(GridVacuumNonObservableState lastState,
            IAction lastAction, GridVacuumPerception percept)
        {
            var state = new GridVacuumNonObservableState
            {
                Position = percept.Position,
                CellState = percept.State
            };

            SetModelState(percept.Position, percept.State);

            if (lastState != null)
            {
                // kinda exploration
                if (lastState.Position == state.Position && lastState.CellState == CellState.Clean)
                {
                    if (lastState.SelectedMovement == Direction.Left)
                        SetModelState(lastState.Position.Left, CellState.Obstacle);

                    if (lastState.SelectedMovement == Direction.Right)
                        SetModelState(lastState.Position.Right, CellState.Obstacle);

                    if (lastState.SelectedMovement == Direction.Up)
                        SetModelState(lastState.Position.Top, CellState.Obstacle);

                    if (lastState.SelectedMovement == Direction.Down)
                        SetModelState(lastState.Position.Bottom, CellState.Obstacle);
                }
            }

            var priorities = new[]
            {
                Priority(state.Position.Left, Direction.Left),
                Priority(state.Position.Right, Direction.Right),
                Priority(state.Position.Top, Direction.Up),
                Priority(state.Position.Bottom, Direction.Down)
            };

            var maxPriority = priorities.Max();
            var ps = priorities.Select((val, i) => val == maxPriority ? i : -1).Where(i => i != -1).ToArray();
            state.SelectedMovement = (Direction) ps[_random.Next(ps.Length)];

            return state;
        }

        private void SetModelState(Point position, CellState state)
        {
            var existed = _model.FirstOrDefault(m => m.Position == position);
            if (existed != null)
                existed.State = state;
            else
            {
                _model.Add(new CellInfo {State = state, Position = position});
            }
        }

        private int Priority(Point p, Direction lastdir, int traverse = 10)
        {
            var priority = 0;
            var s = _model.FirstOrDefault(m => m.Position == p);

            if (s == null)
            {
                priority += 20; // UNEXPLORED FIRST
            }
            else if (s.State != CellState.Obstacle)
            {
                priority += 1;

                if (traverse > 0)
                {
                    if (lastdir != Direction.Down)
                        priority += Priority(p.Top, Direction.Up, traverse - 1);

                    if (lastdir != Direction.Right)
                        priority += Priority(p.Left, Direction.Left, traverse - 1);

                    if (lastdir != Direction.Left)
                        priority += Priority(p.Right, Direction.Right, traverse - 1);

                    if (lastdir != Direction.Up)
                        priority += Priority(p.Bottom, Direction.Down, traverse - 1);
                }
            }
            else if (s.State == CellState.Obstacle)
            {
                return 0;
            }

            return priority;
        }

        class CellInfo
        {
            public Point Position;
            public CellState State;
        }
    }
}