using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System;

namespace Grondslag
{
    public struct Vector2i
    {
        public int X;
        public int Y;
        public Vector2i(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector2i Zero
        {
            get { return new Vector2i(0, 0); }
        }
        public static Vector2i One
        {
            get { return new Vector2i(1, 1); }
        }
        public static Vector2i Invalid
        {
            get { return new Vector2i(-1, -1); }
        }

        public float Length()
        {
            return MathF.Sqrt(X * X + Y * Y);
        }
        public float LengthSqrd()
        {
            return X * X + Y * Y;
        }
        public Vector2 Normalised()
        {
            float len = Length();
            if (len == 0)
            {
                return Vector2.Zero;
            }

            return new Vector2(X / len, Y / len);
        }
        public Vector2 ToMGVector2()
        {
            return new Vector2(X, Y);
        }

        #region Operator overrides

        public static implicit operator Vector2(Vector2i v) => new Vector2(v.X, v.Y);
        //public static implicit operator Vector2i(Vector2 v) => new Vector2i((int)v.X, (int)v.Y);
        public static explicit operator Vector2i(Vector2 v) => new Vector2i((int)v.X, (int)v.Y);



        public static Vector2i operator +(Vector2i a, Vector2i b)
        {
            return new Vector2i(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2i operator -(Vector2i a, Vector2i b)
        {
            return new Vector2i(a.X - b.X, a.Y - b.Y);
        }
        public static Vector2i operator *(Vector2i v, float s)
        {
            return new Vector2i((int)(v.X * s), (int)(v.Y * s));
        }
        public static Vector2i operator *(float s, Vector2i v) // Same, reversed parameters
        {
            return new Vector2i((int)(v.X * s), (int)(v.Y * s));
        }
        public static Vector2i operator /(Vector2i v, float s)
        {
            return new Vector2i((int)(v.X / s), (int)(v.Y / s));
        }

        public static Vector2i operator -(Vector2i v)
        {
            return new Vector2i(-v.X, -v.Y);
        }
        public bool Equals(Vector2i other)
        {
            return this.X == other.X && this.Y == other.Y;
        }
        public static bool operator ==(Vector2i a, Vector2i b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector2i a, Vector2i b)
        {
            return !a.Equals(b);
        }
        public override bool Equals(object obj)
        {
            if (obj is Vector2i other)
            {
                return Equals(other); // StackOverflow error?
            }

            return false;
        }

        public override int GetHashCode()
        {
            return new { this.X, this.Y }.GetHashCode();
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }
        #endregion
    }
}
