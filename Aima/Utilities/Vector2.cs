using System;

namespace Aima.Utilities
{
    public class Vector2 : ICloneable
    {
        public const float Epsilon = 0.0000000001f;
        public static readonly Vector2 Zero = new Vector2(0, 0);
        public static readonly Vector2 Up = new Vector2(0, 1f);
        public static readonly Vector2 Down = new Vector2(0, -1f);
        public static readonly Vector2 Left = new Vector2(-1f, 0);
        public static readonly Vector2 Right = new Vector2(1f, 0);


        public float X;
        public float Y;

        public Vector2()
        {
            X = 0;
            Y = 0;
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float Length => (float)Math.Sqrt(X * X + Y * Y);

        public void Normalize()
        {
            var length = Length;
            X /= length;
            Y /= length;
        }

        public float DotProduct(Vector2 p) 
            => X * p.X + Y * p.Y;

        public float WedgeProduct(Vector2 p)
            => X * p.Y - Y * p.X;

        public float CosBetween(Vector2 p)
            => DotProduct(p) / (Length * p.Length);

        public float ProjectionOn(Vector2 b)
            => Length * CosBetween(b);

        public static Vector2 operator +(Vector2 c1, Vector2 c2)
            => new Vector2(c1.X + c2.X, c1.Y + c2.Y);

        public static Vector2 operator -(Vector2 c1, Vector2 c2)
            => new Vector2(c1.X - c2.X, c1.Y - c2.Y);

        public static Vector2 operator *(Vector2 c1, Vector2 c2)
            => new Vector2(c1.X * c2.X, c1.Y * c2.Y);

        public static Vector2 operator /(Vector2 c1, Vector2 c2)
            => new Vector2(c1.X / c2.X, c1.Y / c2.Y);

        public static Vector2 operator *(Vector2 c1, float c2)
            => new Vector2(c1.X * c2, c1.Y * c2);

        public static Vector2 operator /(Vector2 c1, float c2)
            => new Vector2(c1.X / c2, c1.Y / c2);

        public static bool operator ==(Vector2 c1, Vector2 c2)
            => c1.Equals(c2);

        public static bool operator !=(Vector2 c1, Vector2 c2)
            => !c1.Equals(c2);

        public Vector2 GetUnit()
            => new Vector2(X / Length, Y / Length);

        public Vector2 Copy()
            => new Vector2(X, Y);

        public object Clone()
        {
            return new Vector2(X, Y);
        }

        public bool Equals(Vector2 other)
        {
            return Math.Abs(X - other.X) < Epsilon && Math.Abs(Y - other.Y) < Epsilon;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector2))
                return false;

            return Equals((Vector2)obj);
        }

        public override int GetHashCode() //from System.Double
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"({X:F}, {Y:F})";
        }
    }
}
