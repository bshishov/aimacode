using System;
using System.Collections.Generic;

namespace Aima.Utilities
{
    public class WeightedGraph
    {
        public struct Edge : IComparable<Edge>
        {
            public readonly uint From;
            public readonly uint To;
            public readonly double Weight;

            public Edge(uint from, uint to, double weight)
            {
                From = from;
                To = to;
                Weight = weight;
            }

            public int CompareTo(Edge other)
            {
                return Weight.CompareTo(other.Weight);
            }

            public override string ToString()
            {
                return $"{From}->{To} ({Weight:F})";
            }

            public bool Equals(Edge e)
            {
                return From == e.From 
                    && To == e.To 
                    && Math.Abs(Weight - e.Weight) < double.Epsilon;
            }
        }

        public int VerticesCount { get; }
        public bool Oriented { get; }

        private readonly double[][] _weights;
        
        public WeightedGraph(int verticesCount, bool oriented = false)
        {
            VerticesCount = verticesCount;
            Oriented = oriented;
            _weights = new double[VerticesCount][];

            for (var i = 0; i < verticesCount; i++)
            {
                _weights[i] = new double[verticesCount];
            }
        }

        public double Distance(uint a, uint b)
        {
            if(!Oriented && a > b)
                return _weights[b][a];
            return _weights[a][b];
        }

        public void AddEdge(uint a, uint b, double weight = 1.0)
        {
            SetEdgeWeight(a, b, weight);
        }

        public void SetEdgeWeight(uint a, uint b, double weight = 1.0)
        {
            _weights[a][b] = weight;
            if (!Oriented)
                _weights[b][a] = weight;
        }

        public void AddEdge(Edge edge)
        {
            AddEdge(edge.From, edge.To, edge.Weight);
        }

        public IEnumerable<Edge> Edges(uint from)
        {
            for (uint i = 0; i < VerticesCount; i++)
            {
                var w = _weights[from][i];
                if (w > 0)
                    yield return new Edge(from, i, w);
            }
        }

        public IEnumerable<Edge> AllEdges()
        {
            for (uint i = 0; i < VerticesCount - 1; i++)
            {
                uint startJ = 0;
                if (!Oriented)
                    startJ = i;

                for (uint j = startJ + 1; j < VerticesCount; j++)
                {
                    var w = _weights[i][j];
                    if (w > 0)
                        yield return new Edge(i, j, w);
                }
            }
        }

        public void RemoveEdge(uint a, uint b)
        {
            SetEdgeWeight(a, b, 0.0);
        }

        public void RemoveEdge(Edge e)
        {
            SetEdgeWeight(e.From, e.To, 0.0);
        }

        public WeightedGraph Clone()
        {
            var g = new WeightedGraph(this.VerticesCount, Oriented);
            foreach (var edge in AllEdges())
            {
                g.AddEdge(edge);
            }
            return g;
        }
    }
}
