using System;
using System.Collections.Generic;
using Aima.AgentSystems;
using Aima.Domain.Vaccum.Grid;
using Aima.Search;

namespace Aima.Domain.Vaccum
{
    public class VacuumProblem : IProblem<GridVacuumObservableState>
    {
        public GridVacuumObservableState InitialState { get; }
        private GridVacuumObservableState TargetState { get; }

        public VacuumProblem(GridVacuumObservableState initial, GridVacuumObservableState target)
        {
            InitialState = initial;
            TargetState = target;
        }

        public IEnumerable<Tuple<IAction, GridVacuumObservableState>> SuccessorFn(GridVacuumObservableState state)
        {
            var successors = new List<Tuple<IAction, GridVacuumObservableState>>();

            if (state.StateAtVacuumPosition == CellState.Dirty)
                successors.Add(new Tuple<IAction, GridVacuumObservableState>(VacuumActions.Suck, state.FromAction(VacuumActions.Suck)));

            if (state.VacuumPosition.X > 0 &&
                state.At(state.VacuumPosition.Left) != CellState.Obstacle)
                successors.Add(new Tuple<IAction, GridVacuumObservableState>(VacuumActions.Left, state.FromAction(VacuumActions.Left)));

            if (state.VacuumPosition.X < GridVacuumEnviroment.Width - 1 &&
                state.At(state.VacuumPosition.Right) != CellState.Obstacle)
                successors.Add(new Tuple<IAction, GridVacuumObservableState>(VacuumActions.Right, state.FromAction(VacuumActions.Right)));

            if (state.VacuumPosition.Y < GridVacuumEnviroment.Height - 1 &&
                state.At(state.VacuumPosition.Top) != CellState.Obstacle)
                successors.Add(new Tuple<IAction, GridVacuumObservableState>(VacuumActions.Up, state.FromAction(VacuumActions.Up)));

            if (state.VacuumPosition.Y > 0 &&
                state.At(state.VacuumPosition.Bottom) != CellState.Obstacle)
                successors.Add(new Tuple<IAction, GridVacuumObservableState>(VacuumActions.Down, state.FromAction(VacuumActions.Down)));

            return successors;
        }

        public bool GoalTest(GridVacuumObservableState state)
        {
            return state.Equals(TargetState);
        }

        public double Cost(IAction action, GridVacuumObservableState @from, GridVacuumObservableState to)
        {
            return 1;
        }
    }
}
