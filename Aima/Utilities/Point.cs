using System;

namespace Aima.Utilities
{
    public struct Point
    {
        public bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X*397) ^ Y;
            }
        }

        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Point))
                return false;

            var o = (Point) obj;
            return (X == o.X) && (Y == o.Y);
        }

        public static Point operator +(Point c1, Point c2)
        {
            return new Point(c1.X + c2.X, c1.Y + c2.Y);
        }

        public static Point operator *(Point c1, int c2)
        {
            return new Point(c1.X*c2, c1.Y*c2);
        }

        public static Point operator -(Point c1, Point c2)
        {
            return new Point(c1.X - c2.X, c1.Y - c2.Y);
        }

        public static bool operator ==(Point c1, Point c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(Point c1, Point c2)
        {
            return !c1.Equals(c2);
        }

        public double Length => Math.Abs(X*X + Y*Y);

        public override string ToString()
        {
            return $"x: {X} y: {Y}";
        }

        public Point Top => new Point(X, Y + 1);

        public Point Bottom => new Point(X, Y - 1);

        public Point Left => new Point(X - 1, Y);

        public Point Right => new Point(X + 1, Y);
    }
}