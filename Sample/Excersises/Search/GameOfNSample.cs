using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Aima.AgentSystems;
using Aima.Search;
using Aima.Search.Methods;

namespace Sample.Excersises.Search
{
    class GameOfNSample : IExcersice
    {
        static class Actions
        {
            public static readonly IAction Top = new SimpleAction("TOP");
            public static readonly IAction Left = new SimpleAction("LEFT");
            public static readonly IAction Right = new SimpleAction("RIGHT");
            public static readonly IAction Bottom = new SimpleAction("BOTTOM");
        }

        class State : IState
        {
            public static int N = 4;
            public int[] Values = new int[N * N];

            public int ZeroX = 0;
            public int ZeroY = 0;

            public int ZeroIdx
            {
                get { return ToIndex(ZeroX, ZeroY); }
                set
                {
                    ZeroX = value%N;
                    ZeroY = value/N;
                }
            }

            public State FromAction(IAction action)
            {
                var s = Clone();
                var swapTarget = -1;

                if (action.Equals(Actions.Left))
                    swapTarget = ToIndex(ZeroX - 1, ZeroY);
                if (action.Equals(Actions.Right))
                    swapTarget = ToIndex(ZeroX + 1, ZeroY);
                if (action.Equals(Actions.Top))
                    swapTarget = ToIndex(ZeroX, ZeroY - 1);
                if (action.Equals(Actions.Bottom))
                    swapTarget = ToIndex(ZeroX, ZeroY + 1);

                var old = s.Values[swapTarget];
                s.Values[swapTarget] = 0;
                s.Values[ZeroIdx] = old;
                s.ZeroIdx = swapTarget;

                return s;
            }

            private static int ToIndex(int x, int y)
            {
                return y*N + x;
            }

            private State Clone()
            {
                var newState = new State()
                {
                    ZeroX = ZeroX,
                    ZeroY = ZeroY,
                };
                Values.CopyTo(newState.Values, 0);
                return newState;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                for (var i = 0; i < N; i++)
                {
                    for (var j = 0; j < N; j++)
                    {
                        sb.AppendFormat("{0}\t", Values[ToIndex(j,i)]);
                    }

                    sb.Append("\n");
                }
                return sb.ToString();
            }
            
            public override int GetHashCode()
            {
                var sb = new StringBuilder(Values.Length);
                for (var i = 0; i < Values.Length; i++)
                {
                    sb.Append((char)Values[i]);
                }
                return sb.ToString().GetHashCode();
            }

            public override bool Equals(object obj)
            {
                var s = obj as State;
                if (s != null)
                    return s.Values.SequenceEqual(Values);
                return base.Equals(obj);
            }
        }

        class Problem : IProblem<State>
        {
            public State InitialState { get; }

            public Problem()
            {
                InitialState = new State
                {

                    Values = new[] { 7, 2, 4, 5, 0, 6, 8, 3, 1, 9, 10, 11, 12, 13, 14, 15},
                    //Values = new[] { 7, 2, 4, 5, 0, 6, 8, 3, 1 },
                    //Values = new[] { 3, 2, 1, 0 },
                    ZeroX = 1,
                    ZeroY = 1
                };
            }

            public IEnumerable<Tuple<IAction, State>> SuccessorFn(State state)
            {
                var successors = new List<Tuple<IAction, State>>();

                if (state.ZeroX < State.N - 1)
                    successors.Add(new Tuple<IAction, State>(Actions.Right, state.FromAction(Actions.Right)));

                if (state.ZeroX > 0)
                    successors.Add(new Tuple<IAction, State>(Actions.Left, state.FromAction(Actions.Left)));

                if (state.ZeroY < State.N - 1)
                    successors.Add(new Tuple<IAction, State>(Actions.Bottom, state.FromAction(Actions.Bottom)));

                if (state.ZeroY > 0)
                    successors.Add(new Tuple<IAction, State>(Actions.Top, state.FromAction(Actions.Top)));

                return successors;
            }

            public bool GoalTest(State state)
            {
                //Console.SetCursorPosition(0, State.N + 1);
                //Console.WriteLine(state);
                //Thread.Sleep(1000);

                for (var i = 0; i < state.Values.Length; i++)
                {
                    if (state.Values[i] != i)
                        return false;
                }

                return true;
            }

            public double Cost(IAction action, State @from, State to)
            {
                return 1;
            }
        }

        public void Run()
        {
            var problem = new Problem();
            Console.WriteLine(problem.InitialState);

            var searchMethod = new BroadGraphSearch<State>();
            //var searchMethod = new DepthGraphSearch<State>();
            var solution = searchMethod.Search(problem);

            if (solution != null)
            {
                Console.WriteLine("Solution found in {0} steps", solution.Steps.Count());
                /*
                foreach (var action in solution.Steps)
                {
                    Console.WriteLine(action.Name);
                }

                var node = solution.ParentNode;
                while (node != null)
                {
                    if(node.Action != null)
                        Console.WriteLine(node.Action);
                    Console.WriteLine(node.State);
                    node = node.ParentNode;
                }*/
            }
            else
            {
                Console.WriteLine("No solution");
            }
        }
    }
}
