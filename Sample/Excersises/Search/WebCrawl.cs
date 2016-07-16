using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Aima.AgentSystems;
using Aima.Search;
using Aima.Search.Methods;

namespace Sample.Excersises.Search
{
    class WebCrawl : IExcersice
    {
        class State : IState
        {
            public readonly Uri Uri;
            
            public State(string url)
            {
                Uri = new Uri(url);
            }

            public override string ToString()
            {
                return Uri.GetLeftPart(UriPartial.Path);
            }

            public override int GetHashCode()
            {
                return Uri.GetLeftPart(UriPartial.Path).GetHashCode();
            }

            public override bool Equals(object obj)
            {
                var s = obj as State;
                if (s != null)
                    return Uri.GetLeftPart(UriPartial.Path) == s.Uri.GetLeftPart(UriPartial.Path);
                return base.Equals(obj);
            }
        }

        class WebCrawlProblem : IProblem<State>
        {
            public State InitialState { get; }
            public State FinalState { get; }

            private readonly WebClient _client = new WebClient();
            private readonly Regex _regex = new Regex("<a.*?href=\"(.*?)\"", RegexOptions.Compiled);

            public WebCrawlProblem(string from, string to)
            {
                InitialState = new State(from);
                FinalState = new State(to);
            }

            public IEnumerable<Tuple<IAction, State>> SuccessorFn(State state)
            {
                Console.WriteLine("Downloading {0}", state.Uri.GetLeftPart(UriPartial.Path));
                string html;
                try
                {
                    html = _client.DownloadString(state.Uri);
               
                    var successors = new List<Tuple<IAction, State>>();
                    foreach (Match match in _regex.Matches(html))
                    {
                        if(!match.Groups[1].Success)
                            continue;

                        var url = WebUtility.UrlDecode(match.Groups[1].Value);

                        var newUri = new Uri(state.Uri, url);
                        url = newUri.AbsoluteUri;

                        if(string.IsNullOrEmpty(url))
                            continue;
                        //Console.WriteLine("Found: {0}", url);
                        successors.Add(new Tuple<IAction, State>(new SimpleAction(url), new State(url)));
                    }

                    return successors;
                }
                catch
                {
                    return null;
                }
            }

            public bool GoalTest(State state)
            {
                return state.Uri.LocalPath.Equals(FinalState.Uri.LocalPath);
            }

            public double Cost(IAction action, State @from, State to)
            {
                return 1;
            }
        }

        public void Run()
        {
            //var from = "https://ru.wikipedia.org/wiki/Traceroute";
            //var from = "https://ru.wikipedia.org/wiki/%D0%91%D0%B5%D1%80%D0%BB%D0%B8%D0%BD";
            var from = "https://habrahabr.ru/post/305312/";
            var to = "https://geektimes.ru/post/278256/";

            var searchMethod = new BroadGraphSearch<State>();
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
