using System;
using System.Collections.Generic;
using Aima.AgentSystems;
using Aima.AgentSystems.Vacuum;
using Aima.AgentSystems.Vacuum.Grid;
using Aima.Search;
using Aima.Search.Methods;

namespace Sample.Excersises.Search
{
    class OptimalVacuum : IExcersice
    {
        class VacuumState : IState
        {
            
        }

        class VacuumProblem : IProblem<GridVacuumObservableState>
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

                if(state.StateAtVacuumPosition == CellState.Dirty)
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

        class OptimalVacuumAgent : ErrorFixingSolvingAgent<GridVacuumObservableState, GridVacuumPerception>
        {
            public GridVacuumEnviroment Env { get; set; }

            public OptimalVacuumAgent()
                // optimal but slow
                //: base(new BroadGraphSearch<GridVacuumObservableState>())
                : base(new UniformCostSearch<GridVacuumObservableState>())

                //unoptimal but fast
                //: base(new DepthLimitedSearch<GridVacuumObservableState>(GridVacuumEnviroment.Width * GridVacuumEnviroment.Height * 10))
            {
            }

            public override GridVacuumObservableState FormulateGoal(GridVacuumObservableState state)
            {
                var goal = new GridVacuumObservableState()
                {
                    VacuumPosition = state.VacuumPosition,
                };

                for (var i = 0; i < GridVacuumEnviroment.Width; i++)
                {
                    for (var j = 0; j < GridVacuumEnviroment.Height; j++)
                    {
                        if(state.Field[i, j] == CellState.Obstacle)
                            goal.Field[i, j] = state.Field[i, j];
                    }
                }

                return goal;
            }

            public override IProblem<GridVacuumObservableState> FormulateProblem(GridVacuumObservableState state, GridVacuumObservableState goal)
            {
                return new VacuumProblem(state, goal);
            }

            public override GridVacuumObservableState UpdateState(GridVacuumObservableState state, GridVacuumPerception percept)
            {
                // fully observable
                return Env?.State;
            }
        }

        public void Run()
        {
            var testEnviroments = 50;
            var scores = 0.0;
            for (var i = 0; i < testEnviroments; i++)
            {
                //var agent = new GridRandomVacuumAgent();
                var agent = new OptimalVacuumAgent();
                var env = new GridVacuumEnviroment(agent, i);
                agent.Env = env;
                var result = RunEnv(env);
                scores += result;
                Console.WriteLine("Score: {0} [{1} steps] ", result, env.CurrentStep);
            }
            Console.WriteLine("Average: {0}", scores / (double)testEnviroments);
        }

        static double RunEnv(IVacuumEnviroment env)
        {
            const int maxSteps = 50000;

            while (!env.AllClean)
            {
                if (env.CurrentStep > maxSteps)
                    break;
                env.Step();
            }
            return env.AgentScore;
        }
    }
}
