using System;

namespace Aima.Utilities
{
    public interface IShape
    {
        Vector2[] Vertices { get; }
        Shape FromPadding(float padding);
    }

    public class Line : IShape
    {
        public Vector2[] Vertices => new [] {A, B};
        public Shape FromPadding(float padding)
        {
            throw new NotImplementedException();
        }

        public Vector2 A;
        public Vector2 B;

        public Line(Vector2 a, Vector2 b)
        {
            A = a;
            B = b;
        }

        public Line(float x1, float y1, float x2, float y2)
        {
            A = new Vector2(x1, y1);
            B = new Vector2(x2, y2);
        }

        public double Length => Math.Sqrt((A.X - B.X)*(A.X - B.X) + (A.Y - B.Y)*(A.Y - B.Y));

        public override string ToString()
        {
            return $"{A} -> {B}";
        }
    }

    public class Shape : IShape
    {
        public Vector2[] Vertices { get; }

        public Shape(params Vector2[] vertices)
        {
            Vertices = vertices;
        }

        public Vector2 GetCenter()
        {
            var sum = new Vector2();
            for (var i = 0; i < Vertices.Length; i++)
            {
                sum = sum + Vertices[i];
            }

            return sum / Vertices.Length;
        }

        public Shape FromPadding(float padding)
        {
            var c = GetCenter();
            var vertices = new Vector2[Vertices.Length];
            Vertices.CopyTo(vertices, 0);

            for (var i = 0; i < vertices.Length; i++)
            {
                var v = vertices[i];
                var d = (v - c).GetUnit();
                vertices[i] = v + (v - c).GetUnit() * padding;
            }
            return new Shape(vertices);
        }
    }

    public class Rectangle : Shape
    {
        public Rectangle(float x, float y, float w, float h)
            : base(new Vector2(x, y), new Vector2(x + w, y), new Vector2(x + w, y + h), new Vector2(x, y + h))
        {
        }
    }
}
