using System;
using System.Linq;

namespace Aima.Search.Methods.SimulatedAnnealing
{
    public class SimulatedAnnealing<TState> : HeuristicSearch<TState>
    {
        private readonly IAnnealingSchedule _schedule;

        public SimulatedAnnealing(IAnnealingSchedule schedule, HeuristicNodeExpander<TState> expander) 
            : base(expander)
        {
            _schedule = schedule;
        }

        public SimulatedAnnealing(IAnnealingSchedule schedule, IHeuristic<TState> heuristic) 
            : this(schedule, new DefaultHeuristicNodeExpander<TState>(heuristic))
        {
        }

        public SimulatedAnnealing(IAnnealingSchedule schedule, Func<TState, double> heuristic)
            : this(schedule, new DefaultHeuristicNodeExpander<TState>(heuristic))
        {
        }

        public SimulatedAnnealing(HeuristicNodeExpander<TState> expander)
             : this(new DefaultAnnealingSchedule(), expander)
        {
        }

        public SimulatedAnnealing(IHeuristic<TState> heuristic)
             : this(new DefaultAnnealingSchedule(),
                   new DefaultHeuristicNodeExpander<TState>(heuristic))
        {
        }

        public SimulatedAnnealing(Func<TState, double> heuristic)
            : this(new DefaultAnnealingSchedule(),
                  new DefaultHeuristicNodeExpander<TState>(heuristic))
        {
        }

        public override ISolution<TState> Search(IProblem<TState> problem)
        {
            var rnd = new Random();
            var current = new HeuristicTreeNode<TState>(problem.InitialState)
            {
                Heuristic = Expander.ComputeHeuristic(problem.InitialState)
            };

            var time = 0;
            while (true)
            {
                var temperature = _schedule.Temparature(time);

                // annealing end
                if (temperature < Double.Epsilon)
                    return new Solution<TState>(current);

                // get random successor
                var successors = Expand(current, problem).ToList();
                var next = successors[rnd.Next(successors.Count)];
                var deltaE = next.Heuristic - current.Heuristic;

                // next is better than current
                if (deltaE > 0)
                    current = next;
                // with some probability accept worse state
                else if(rnd.NextDouble() < Math.Exp(deltaE / temperature))
                    current = next;

                time++;
            }
        }
    }
}