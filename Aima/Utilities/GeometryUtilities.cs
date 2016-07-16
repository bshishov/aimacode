using System.Collections.Generic;

namespace Aima.Utilities
{
    public static class GeometryUtilities
    {
        public static void CreateGraph(IEnumerable<Vector2> initialVertices, IEnumerable<IShape> obstacles, out List<Vector2> verticesOut, out List<Line> edgesOut, float padding = 5f)
        {
            var edges = new List<Line>();
            var vertices = new List<Vector2> (initialVertices);

            foreach (var shape in obstacles)
            {
                vertices.AddRange(shape.FromPadding(padding).Vertices);
            }

            for (var i = 0; i < vertices.Count - 1; i++)
            {
                for (var j = i+1; j < vertices.Count; j++)
                {
                    var line = new Line(vertices[i], vertices[j]);
                    var intersects = false;

                    foreach (var shape in obstacles)
                    {
                        if (Intersects(line.A, line.B, shape))
                            intersects = true;
                    }

                    if(!intersects)
                        edges.Add(line);
                }
            }

            verticesOut = vertices;
            edgesOut = edges;
        }

        public static bool Intersects(Vector2 p1, Vector2 p2, IShape shape)
        {
            for (var i = 1; i < shape.Vertices.Length; i++)
            {
                if (Intersects(p1, p2, shape.Vertices[i - 1], shape.Vertices[i]))
                    return true;
            }

            return false;
        }

        public static bool Intersects(Vector2 p1, Vector2 p2, Vector2 m1, Vector2 m2)
        {
            var r1 = (p2 - p1).WedgeProduct(m2 - p1);
            var r2 = (p2 - p1).WedgeProduct(m1 - p1);

            var r3 = (m2 - m1).WedgeProduct(p2 - m1);
            var r4 = (m2 - m1).WedgeProduct(p1 - m1);

            var p = (r1 >= 0 && r2 <= 0) || (r1 <= 0 && r2 >= 0);
            var m = (r3 >= 0 && r4 <= 0) || (r3 <= 0 && r4 >= 0);
            
            return m && p;
        }
    }
}
