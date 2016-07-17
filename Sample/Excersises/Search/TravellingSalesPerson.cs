using Aima.Search.Domain;
using Aima.Search.Methods;

namespace Sample.Excersises.Search
{
    class TravellingSalesPerson : IExcersice
    {
       

        public void Run()
        {
            var problem = new TravelingSalespersonProblem(10, 1337);
            
            //Measure.SearchPerformance(problem, new DepthLimitedSearch<TravelingSalesPersonState>(20));
            Measure.SearchPerformance(problem, new DepthGraphSearch<TravelingSalesPersonState>());
        }
    }
}
