using Grondslag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CurveCreator
{
    public struct Curve
    {
        private Vector2[] _points;
        private const float MinY = 0f;
        private const float MaxY = 1f;
        private const float ControlMinX = -100f;
        private const float ControlMaxX = 100f;

        public Curve(Vector2 start, Vector2 control, Vector2 end)
        {
            _points = new Vector2[3]
            {
                start,
                control,
                end,
            };
        }

        public Vector2[] Points => _points;
        public Vector2 P0 => Points[0];
        public Vector2 P1 => Points[1];
        public Vector2 P2 => Points[2];

        public float Domain => P2.X - P0.X;

        public void SetStart(Vector2 value)
        {
            _points[0] = value;
        }
        public void SetControl(Vector2 value)
        {
            _points[1] = value;
        }
        public void SetEnd(Vector2 value)
        {
            _points[2] = value;
        }

        public void AdjustPoint(int index, float deltaX, float deltaY)
        {
            _points[index].X += deltaX;
            _points[index].Y += deltaY;

            _points[index].X = MathF.Round(_points[index].X, 2);
            _points[index].Y = MathF.Round(_points[index].Y, 2);

            if (index == 1)
            {
                _points[index].X = Math.Clamp(_points[index].X, ControlMinX, ControlMaxX);
            }

            if (!Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                _points[index].Y = Math.Clamp(_points[index].Y, MinY, MaxY);
            }
        }

        #region shit, rubbish, abominable code
        //public void IncrementStartY()
        //{
        //    _points[0].Y += 0.02f;
        //    _points[0].Y = MathF.Round(_points[0].Y, 2);
        //    _points[0].Y = Math.Clamp(P0.Y, 0, 1);
        //}
        //public void DecrementStartY()
        //{
        //    _points[0].Y -= 0.02f;
        //    _points[0].Y = MathF.Round(_points[0].Y, 2);
        //    _points[0].Y = Math.Clamp(P0.Y, 0, 1);
        //}

        //public void IncrementControlX()
        //{
        //    _points[1].X += 2f;
        //    _points[1].X = MathF.Round(_points[1].X, 2);
        //    _points[1].X = Math.Clamp(P1.X, -100, 100);
        //}
        //public void IncrementControlY()
        //{
        //    _points[1].Y += 0.02f;
        //    _points[1].Y = MathF.Round(_points[1].Y, 2);
        //    _points[1].Y = Math.Clamp(P1.Y, 0, 1);
        //}
        //public void DecrementControlX()
        //{
        //    _points[1].X -= 2f;
        //    _points[1].X = MathF.Round(_points[1].X, 2);
        //    _points[1].X = Math.Clamp(P1.X, -100, 100);
        //}
        //public void DecrementControlY()
        //{
        //    _points[1].Y -= 0.02f;
        //    _points[1].Y = MathF.Round(_points[1].Y, 2);
        //    _points[1].Y = Math.Clamp(P1.Y, 0, 1);
        //}

        //public void IncrementEndY()
        //{
        //    _points[2].Y += 0.02f;
        //    _points[2].Y = MathF.Round(_points[2].Y, 2);
        //    _points[2].Y = Math.Clamp(P2.Y, 0, 1);
        //}
        //public void DecrementEndY()
        //{
        //    _points[2].Y -= 0.02f;
        //    _points[2].Y = MathF.Round(_points[2].Y, 2);
        //    _points[2].Y = Math.Clamp(P2.Y, 0, 1);
        //}
        #endregion

        public Vector2 GetVector(float t) // Where t is [0,1]
        {
            //float q0 = P0 + t*(P1 - P0); // q refers to the points between the set points;
            //float q1 = P1 + t*(P2 - P1);

            //return q0 + t*(q1 - q0);
            return P0 + t * (P1 - P0) + t*(P1 + t * (P2 - P1) - (P0 + t * (P1 - P0)));

            //return MathF.Pow(1 - t, 2) * P0 + 2 * (1 - t) * t * P1 + MathF.Pow(t, 2) * P2;
        }
        public float GetT(float x)
        {
            float a = P2.X - 2 * P1.X + P0.X;
            float b = 2 * (P1.X - P0.X);
            float c = P0.X - x;

            if (a > -0.00001 && a < 0.00001)
            {
                return -c / b;
            }

            float discriminant = b * b - 4 * a * c;
            float n = -b + MathF.Sqrt(discriminant);
            float d = 2 * a;

            float result = n / d;
            return result;
        }
        public float GetYFromX(float x)
        {
            float t = GetT(x);
            return P0.Y + t * (P1.Y - P0.Y) + t * (P1.Y + t * (P2.Y - P1.Y) - (P0.Y + t * (P1.Y - P0.Y)));
        }
    }
}
