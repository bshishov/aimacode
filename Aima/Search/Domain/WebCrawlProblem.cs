using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using Aima.AgentSystems;

namespace Aima.Search.Domain
{
    public class WebCrawlState : IState
    {
        public readonly Uri Uri;

        public WebCrawlState(string url)
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
            var s = obj as WebCrawlState;
            if (s != null)
                return Uri.GetLeftPart(UriPartial.Path) == s.Uri.GetLeftPart(UriPartial.Path);
            return base.Equals(obj);
        }
    }

    public class WebCrawlProblem : IProblem<WebCrawlState>
    {
        public WebCrawlState InitialState { get; }
        public WebCrawlState FinalState { get; }

        private readonly WebClient _client = new WebClient();
        private readonly Regex _regex = new Regex("<a.*?href=\"(.*?)\"", RegexOptions.Compiled);

        public WebCrawlProblem(string from, string to)
        {
            InitialState = new WebCrawlState(from);
            FinalState = new WebCrawlState(to);
        }

        public IEnumerable<Tuple<IAction, WebCrawlState>> SuccessorFn(WebCrawlState state)
        {
            Console.WriteLine("Downloading {0}", state.Uri.GetLeftPart(UriPartial.Path));
            try
            {
                var html = _client.DownloadString(state.Uri);

                var successors = new List<Tuple<IAction, WebCrawlState>>();
                foreach (Match match in _regex.Matches(html))
                {
                    if (!match.Groups[1].Success)
                        continue;

                    var url = WebUtility.UrlDecode(match.Groups[1].Value);

                    var newUri = new Uri(state.Uri, url);
                    url = newUri.AbsoluteUri;

                    if (string.IsNullOrEmpty(url))
                        continue;
                    //Console.WriteLine("Found: {0}", url);
                    successors.Add(new Tuple<IAction, WebCrawlState>(new SimpleAction(url), new WebCrawlState(url)));
                }

                return successors;
            }
            catch
            {
                return null;
            }
        }

        public bool GoalTest(WebCrawlState state)
        {
            return state.Uri.LocalPath.Equals(FinalState.Uri.LocalPath);
        }

        public double Cost(IAction action, WebCrawlState @from, WebCrawlState to)
        {
            return 1;
        }
    }
}
