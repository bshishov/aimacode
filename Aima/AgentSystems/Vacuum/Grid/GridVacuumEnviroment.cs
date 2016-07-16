using System;
using System.Threading;
using Aima;
using Aima.Utilities;

namespace Aima.AgentSystems.Vacuum.Grid
{
    public class GridVacuumEnviroment : ISingleAgentEnviroment<GridVacuumPerception>, IVacuumEnviroment
    {
        public IAgent<GridVacuumPerception> Agent { get; }
        public double AgentScore { get; private set; }
        public static int Height { get; } = 3;
        public static int Width { get; } = 3;
        public int CurrentStep => _step;

        public double BadDirtySensorChance = 0.0;
        public double SuccessCleaningChance = 0.7;
        public double MovePenalty = 1;
        public double CleanCellScore = 0;
        public double DirtCellPenalty = 1;

        public GridVacuumObservableState State { get; }
        
        private int _step;
        private readonly Random _random;
        private bool _vizualize;

        public GridVacuumEnviroment(IAgent<GridVacuumPerception> agent, int seed = 0, bool visualize = false)
        {
            State = new GridVacuumObservableState
            {
                Field = new CellState[Width, Height]
            };

            Agent = agent;
            AgentScore = 0;

            _vizualize = visualize;

            if (seed != 0)
                _random = new Random(seed);
            else
                _random = new Random();

            for (var i = 0; i < Width; i++)
            for (var j = 0; j < Height; j++)
            {
                var rnd = _random.NextDouble();
                if (rnd <= 0.1)
                    State.Field[i, j] = CellState.Obstacle;
                else if (rnd <= 0.3)
                    State.Field[i, j] = CellState.Dirty;
            }

            State.VacuumPosition = new Point(_random.Next(Width), _random.Next(Height));

            while (StatusAt(State.VacuumPosition) != CellState.Clean)
            {
                State.VacuumPosition = new Point(_random.Next(Width), _random.Next(Height));
            }
        }

        public void Step()
        {
            var perceptionState = StatusAt(State.VacuumPosition);

            // 10% chance to dirty sensor work wrong
            if (_random.NextDouble() < BadDirtySensorChance && perceptionState == CellState.Dirty)
            {
                perceptionState = CellState.Clean;
            }

            var action = Agent.Execute(new GridVacuumPerception
            {
                Position = State.VacuumPosition,
                State = perceptionState
            });
            DoAction(action);


            // Scoring
            for (var i = 0; i < Width; i++)
            for (var j = 0; j < Height; j++)
            {
                if (State.Field[i, j] == CellState.Clean)
                    AgentScore += CleanCellScore;
                if (State.Field[i, j] == CellState.Dirty)
                    AgentScore -= DirtCellPenalty;
            }

            if(_vizualize)
                Visualize(action);

            _step++;
        }

        private void Visualize(IAction action)
        {
            for (var i = 0; i < Width; i++)
                for (var j = 0; j < Height; j++)
                {
                    Console.SetCursorPosition(i, j);

                    var field = State.Field[i, j];
                    if(field == CellState.Clean)
                        Console.Write(".");
                    if (field == CellState.Dirty)
                        Console.Write("#");
                    if (field == CellState.Obstacle)
                        Console.Write("█");
                }

            Console.SetCursorPosition(State.VacuumPosition.X, State.VacuumPosition.Y);
            Console.Write("O");

            Console.SetCursorPosition(0, 12);
            Console.WriteLine("[{0}] {1}\tscore:{2}", _step, action?.Name, AgentScore);
            Thread.Sleep(500);
        }

        private void DoAction(IAction action)
        {
            if (action == null)
                return;

            

            if (action.Equals(VacuumActions.Left))
            {
                
                if (State.VacuumPosition.X > 0 && StatusAt(State.VacuumPosition.Left) != CellState.Obstacle)
                {
                    State.VacuumPosition = State.VacuumPosition.Left;
                    AgentScore -= 1;
                }
            }

            if (action.Equals(VacuumActions.Right))
            {
                if (State.VacuumPosition.X < Width - 1 && StatusAt(State.VacuumPosition.Right) != CellState.Obstacle)
                {
                    State.VacuumPosition = State.VacuumPosition.Right;
                    AgentScore -= 1;
                }
            }

            if (action.Equals(VacuumActions.Up))
            {
                if (State.VacuumPosition.Y < Height - 1 && StatusAt(State.VacuumPosition.Top) != CellState.Obstacle)
                {
                    State.VacuumPosition = State.VacuumPosition.Top;
                    AgentScore -= 1;
                }
                
            }

            if (action.Equals(VacuumActions.Down))
            {
                if (State.VacuumPosition.Y > 0 && StatusAt(State.VacuumPosition.Bottom) != CellState.Obstacle)
                {
                    State.VacuumPosition = State.VacuumPosition.Bottom;
                    AgentScore -= 1;
                }
            }

            if (action.Equals(VacuumActions.Suck))
            {
                var chanceComputed = _random.NextDouble();

                // Cleaning works only in 75% 
                if (StatusAt(State.VacuumPosition) == CellState.Dirty && chanceComputed < SuccessCleaningChance)
                    State.Field[State.VacuumPosition.X, State.VacuumPosition.Y] = CellState.Clean;
            }
        }

        private CellState StatusAt(Point point)
        {
            return State.Field[point.X, point.Y];
        }

        public bool AllClean => State.Field.IsClear(Width, Height);
    }
}
