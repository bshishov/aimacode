using System;
using Aima.AgentSystems.Vacuum;
using Aima.AgentSystems.Vacuum.Grid;
using Aima.Search;
using Aima.Search.Domain;
using Aima.Search.Methods;

namespace Sample.Excersises.Search
{
    class OptimalVacuum : IExcersice
    {
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
