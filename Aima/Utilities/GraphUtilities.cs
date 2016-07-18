using System;
using System.Collections.Generic;
using System.Linq;

namespace Aima.Utilities
{
    public static class GraphUtilities
    {
        public struct TreeSubset
        {
            public uint Parent;
            public uint Rank;
        }

        // A utility function to find set of an element i
        // (uses path compression technique)
        public static uint Find(TreeSubset[] subsets, uint i)
        {
            // find root and make root as parent of i (path compression)
            if (subsets[i].Parent != i)
                subsets[i].Parent = Find(subsets, subsets[i].Parent);

            return subsets[i].Parent;
        }

        // A function that does union of two sets of x and y
        // (uses union by rank)
        public static void Union(TreeSubset[] subsets, uint x, uint y)
        {
            var xroot = Find(subsets, x);
            var yroot = Find(subsets, y);

            // Attach smaller rank tree under root of high rank tree
            // (Union by Rank)
            if (subsets[xroot].Rank < subsets[yroot].Rank)
                subsets[xroot].Parent = yroot;
            else if (subsets[xroot].Rank > subsets[yroot].Rank)
                subsets[yroot].Parent = xroot;

            // If ranks are same, then make one as root and increment
            // its rank by one
            else
            {
                subsets[yroot].Parent = xroot;
                subsets[xroot].Rank++;
            }
        }

        public static WeightedGraph KruskalMST(WeightedGraph graph)
        {
            if(graph.Oriented)
                throw new InvalidOperationException("Creating of minimum spanning tree available only for non-oriented graphs");
            var result = new WeightedGraph(graph.VerticesCount, false);

            // Create V subsets with single elements
            var subsets = new TreeSubset[graph.VerticesCount];
            for (uint i = 0; i < graph.VerticesCount; i++)
                subsets[i].Parent = i;

            var edges = graph.AllEdges().ToArray();
            Array.Sort(edges);
            
            foreach (var edge in edges)
            {
                var x = Find(subsets, edge.From);
                var y = Find(subsets, edge.To);

                // If including this edge does't cause cycle, include it
                // in result and increment the index of result for next edge
                if (x != y)
                {
                    result.AddEdge(edge);
                    Union(subsets, x, y);
                }
            }

            return result;
        }

        public static double TotalLength(this WeightedGraph graph)
        {
            return graph.AllEdges().Sum(edge => edge.Weight);
        }

        public static WeightedGraph Subtract(this WeightedGraph graph, WeightedGraph another)
        {
            var result = new WeightedGraph(graph.VerticesCount);
            var allAnotherEdges = another.AllEdges().ToList();
            foreach (var edge in graph.AllEdges())
            {
                if(!allAnotherEdges.Contains(edge))
                    result.AddEdge(edge);
            }
            return result;
        }

        public static WeightedGraph DetachedVertices(this WeightedGraph graph, List<uint> exclude)
        {
            var result = new WeightedGraph(graph.VerticesCount);
            foreach (var edge in graph.AllEdges())
            {
                if (!exclude.Contains(edge.To) && !exclude.Contains(edge.From))
                    result.AddEdge(edge);
            }
            return result;
        }
    }
}