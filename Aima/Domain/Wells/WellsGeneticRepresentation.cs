using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aima.Search.Methods.Genetic;

namespace Aima.Domain.Wells
{
    public class WellsGeneticRepresentation : IGeneticRepresentation<int, WellsState>
    {
        private readonly int _size;
        private readonly Random _random = new Random();
        private readonly WellsProblem _problem;

        public WellsGeneticRepresentation(WellsProblem problem)
        {
            _size = problem.WellsNumber;
            _problem = problem;
        }

        public int[] RandomGenome()
        {
            var wellsPositions = new int[_size];

            var index = 0;
            while (index < _size)
            {
                var candidate = _problem.ToIndex(_random.Next(_problem.Width), _random.Next(_problem.Height));
                if(wellsPositions.Contains(candidate))
                    continue;

                wellsPositions[index++] = candidate;
            }

            return wellsPositions;
        }

        public WellsState FromGenome(int[] genome)
        {
            return new WellsState(genome.ToList());
        }

        public int[] FromState(WellsState state)
        {
            return state.WellsPositions.ToArray();
        }
    }
}
