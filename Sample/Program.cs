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
            //new RiverSample().Run();
            //new GameOfNSample().Run();
            //new TravellingSalesPerson().Run();
            //new WebCrawl().Run();
            //new GeometryPath().Run();
            //new Robot().Run();
            //new OptimalVacuum().Run();
            new HeuristicTSP().Run();

            Console.ReadKey();
        }
    }
}
