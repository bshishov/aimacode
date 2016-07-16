using System;
using Aima.AgentSystems.Vacuum;
using Aima.AgentSystems.Vacuum.Grid;

namespace Sample.Excersises.Agent
{
    internal class Vaccuum : IExcersice
    {
        public void Run()
        {
            //RunEnv(new SimpleVacuumEnviroment(new SimpleStatelessReflexVacuumAgent(), "A", true, false));
            //RunEnv(new SimpleVacuumEnviroment(new SimpleStatelessReflexVacuumAgent(), "A", false, true));
            //RunEnv(new SimpleVacuumEnviroment(new SimpleStatelessReflexVacuumAgent(), "A", true, true));
            //RunEnv(new SimpleVacuumEnviroment(new SimpleStatelessReflexVacuumAgent(), "B", true, false));
            //RunEnv(new SimpleVacuumEnviroment(new SimpleStatelessReflexVacuumAgent(), "B", false, true));
            //RunEnv(new SimpleVacuumEnviroment(new SimpleStatelessReflexVacuumAgent(), "B", true, true));

            var testEnviroments = 10;
            var scores = 0.0;
            for (var i = 0; i < testEnviroments; i++)
            {
                //var agent = new GridRandomVacuumAgent();
                var agent = new GridVacuumModelAgent();
                var env = new GridVacuumEnviroment(agent);
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