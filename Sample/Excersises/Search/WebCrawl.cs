using System;
using Aima.Search.Domain;
using Aima.Search.Methods;

namespace Sample.Excersises.Search
{
    class WebCrawl : IExcersice
    {
       public void Run()
        {
            //var from = "https://ru.wikipedia.org/wiki/Traceroute";
            //var from = "https://ru.wikipedia.org/wiki/%D0%91%D0%B5%D1%80%D0%BB%D0%B8%D0%BD";
            var from = "https://habrahabr.ru/post/305312/";
            var to = "https://geektimes.ru/post/278256/";

            var searchMethod = new BroadGraphSearch<WebCrawlState>();
            var solution = searchMethod.Search(new WebCrawlProblem(from, to));

            if (solution != null)
            {
                Console.WriteLine("SOLUTION FOUND!");
                foreach (var action in solution.Steps)
                {
                    Console.WriteLine(action.Name);
                }

                var node = solution.ParentNode;
                while (node != null)
                {
                    Console.WriteLine(node.State);
                    node = node.ParentNode;
                }
            }
            else
            {
                Console.WriteLine("No solution");
            }
        }
    }
}
