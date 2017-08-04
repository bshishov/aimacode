using Aima.AgentSystems;
using Aima.Utilities;

namespace Aima.Domain.Vaccum.Grid
{
    public class GridVacuumObservableState : IState
    {
        public CellState[,] Field = new CellState[GridVacuumEnviroment.Width, GridVacuumEnviroment.Height];
        public Point VacuumPosition;

        public CellState StateAtVacuumPosition
        {
            get { return Field[VacuumPosition.X, VacuumPosition.Y]; }
            set { Field[VacuumPosition.X, VacuumPosition.Y] = value; }
        }


        public override int GetHashCode()
        {
            var hash = VacuumPosition.GetHashCode();
            hash = hash*31 + Field.ComputeHash(GridVacuumEnviroment.Width, GridVacuumEnviroment.Height);
            return hash;
        }

        public GridVacuumObservableState Clone()
        {
            var newState = new GridVacuumObservableState
            {
                VacuumPosition = VacuumPosition
            };
            for (var i = 0; i < GridVacuumEnviroment.Width; i++)
            {
                for (var j = 0; j < GridVacuumEnviroment.Height; j++)
                {
                    newState.Field[i, j] = Field[i, j];
                }
            }
            return newState;
        }

        public GridVacuumObservableState FromAction(IAction action)
        {
            var n = Clone();
            if (Equals(action, VacuumActions.Suck))
                n.StateAtVacuumPosition = CellState.Clean;

            if (Equals(action, VacuumActions.Left))
                n.VacuumPosition = VacuumPosition.Left;

            if (Equals(action, VacuumActions.Right))
                n.VacuumPosition = VacuumPosition.Right;

            if (Equals(action, VacuumActions.Up))
                n.VacuumPosition = VacuumPosition.Top;

            if (Equals(action, VacuumActions.Down))
                n.VacuumPosition = VacuumPosition.Bottom;

            return n;
        }

        public CellState At(Point p)
        {
            return Field[p.X, p.Y];
        }

        public override bool Equals(object obj)
        {
            var s = obj as GridVacuumObservableState;
            if (s != null)
                return s.GetHashCode() == GetHashCode();
            return base.Equals(obj);
        }
    }
}