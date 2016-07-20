using System;
using Sample.Excersises.Search;
using Sample.Excersises.Search2;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            //new Vaccuum().Run();

            // SEARCH
            //new RiverSample().Run();
            //new SlidingTiles().Run();
            //new TravellingSalesPerson().Run();
            //new WebCrawl().Run();
            //new GeometryPath().Run();
            //new Robot().Run();
            //new OptimalVacuum().Run();

            // HEURISTIC
            //new HeuristicTSP().Run();
            //new HeuristicSlidingTiles().Run();
            new NQueens().Run();

            Console.ReadKey();
        }
    }
}
