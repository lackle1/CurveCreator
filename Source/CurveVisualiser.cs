using Grondslag;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CurveCreator
{
    public class CurveVisualiser
    {
        private Vector2i _pos; // top-right
        private int _width, _height;
        private int _lineThickness;
        private float _numSegements; // Number of lines.

        private Texture2D _blank;
        private SpriteFont _font;

        private Vector2i _max, _min;
        private int _domain, _range;

        public CurveVisualiser(Texture2D blank, SpriteFont font)
        {
            _blank = blank;

            _pos = new Vector2i(600, 250);
            _width = 400;
            _height = 400;
            _lineThickness = 10;
            _numSegements = 100;
            _font = font;

            _max = new Vector2i(100, 1);
            _min = new Vector2i(-100, 0);
            _domain = _max.X - _min.X;
            _range = _max.Y - _min.Y;
        }

        public Vector2 ScreenToGraphSpace(Vector2 screenPos, Curve curve)
        {
            Vector2 result = Vector2.Zero;
            result.X = (screenPos.X - _pos.X) / _width * _domain + _min.X;
            result.Y = (_pos.Y + _height - screenPos.Y) / _height * _range + _min.Y;

            return result;
        }
        private Vector2 GraphToScreenSpace(float x, float y)
        {
            Vector2 newPos = Vector2.Zero;
            newPos.X = _pos.X + (x - _min.X) / _domain * _width;
            newPos.Y = _pos.Y + _height - (y - _min.Y) / _range * _height;

            return newPos;
        }

        public void Draw(Curve c, SpriteBatch sb)
        {
            sb.Draw(_blank, new Rectangle(_pos.X, _pos.Y, _width, _height), new Color(15, 15, 15));

            Rectangle rect = Rectangle.Empty;
            Vector2 lastPoint = Vector2i.Invalid;

            //for (int i = 0; i <= _numSegements; i++)
            //{
            //    float t = i / _numSegements; // Incrementing float results in 10.000001

            //    Vector2 currentPoint = Vector2.Zero;
            //    Vector2 value = c.GetVector(t);
            //    currentPoint.X = _pos.X + (value.X / c.Domain) * _width;
            //    currentPoint.Y = _pos.Y + _height - value.Y * _height;

            //    if (lastPoint == Vector2i.Invalid) // Is first point?
            //    {
            //        lastPoint = currentPoint;
            //        continue;
            //    }

            //    int dist = (int)Vector2.Distance(lastPoint, currentPoint);
            //    Vector2i origin = new Vector2i(0, 0);
            //    float rot = MathF.Atan2(currentPoint.Y - lastPoint.Y, currentPoint.X - lastPoint.X);
            //    rect = new Rectangle((int)lastPoint.X, (int)lastPoint.Y, dist, _lineThickness);

            //    sb.Draw(_blank, rect, null, Color.Blue, rot, origin, SpriteEffects.None, 0);

            //    lastPoint = currentPoint;
            //}

            //rect = Rectangle.Empty;
            //lastPoint = Vector2i.Invalid;

            for (int i = 0; i <= _numSegements; i++)
            {
                float x = c.P0.X + i / _numSegements * c.Domain;

                Vector2 currentPoint = Vector2.Zero;
                float yValue = c.GetYFromX(x);
                //currentPoint.X = _pos.X + (x - _min.X) / _domain * _width;
                //currentPoint.Y = _pos.Y + _height - (yValue - _min.Y) / _range * _height;
                currentPoint = GraphToScreenSpace(x, yValue);

                if (lastPoint == Vector2i.Invalid) // Is first point?
                {
                    lastPoint = currentPoint;
                    continue;
                }

                int dist = (int)Vector2.Distance(lastPoint, currentPoint);
                Vector2i origin = new Vector2i(0, 0);
                float rot = MathF.Atan2(currentPoint.Y - lastPoint.Y, currentPoint.X - lastPoint.X);
                rect = new Rectangle((int)lastPoint.X, (int)lastPoint.Y, dist, _lineThickness);

                sb.Draw(_blank, rect, null, Color.Green, rot, origin, SpriteEffects.None, 0);

                lastPoint = currentPoint;
            }

            rect.Width = 8; rect.Height = 8;

            //rect.X = (int)(_pos.X + (c.P0.X - _min.X) / _domain * _width);
            //rect.Y = (int)(_pos.Y + _height - (c.P0.Y * _height));
            //sb.Draw(_blank, rect, Color.Red);
            //sb.DrawString(_font, c.P0.X + ", " + c.GetYFromX(c.P0.X), new Vector2(rect.X + 8, rect.Y + 8), Color.White);

            //rect.X = (int)(_pos.X + (c.P1.X - c.P0.X) / c.Domain * _width);
            //rect.Y = (int)(_pos.Y + _height - (c.P1.Y *_height));
            //sb.Draw(_blank, rect, Color.Red);
            //sb.DrawString(_font, c.P1.X + ", " + c.P1.Y, new Vector2(rect.X + 8, rect.Y + 8), Color.White);

            //rect.X = (int)(_pos.X + (c.P2.X - c.P0.X) / c.Domain * _width);
            //rect.Y = (int)(_pos.Y + _height - (c.P2.Y *_height));
            //sb.Draw(_blank, rect, Color.Red);
            //sb.DrawString(_font, c.P2.X + ", " + c.P2.Y, new Vector2(rect.X + 8, rect.Y + 8), Color.White);

            Vector2 screenPoint = GraphToScreenSpace(c.P0.X, c.P0.Y);
            rect.X = (int)screenPoint.X;
            rect.Y = (int)screenPoint.Y;
            sb.Draw(_blank, rect, Color.Red);
            sb.DrawString(_font, c.P0.X + ", " + c.GetYFromX(c.P0.X), new Vector2(rect.X + 8, rect.Y + 8), Color.White);

            screenPoint = GraphToScreenSpace(c.P1.X, c.P1.Y);
            rect.X = (int)screenPoint.X;
            rect.Y = (int)screenPoint.Y;
            sb.Draw(_blank, rect, Color.Red);
            sb.DrawString(_font, c.P1.X + ", " + c.P1.Y, new Vector2(rect.X + 8, rect.Y + 8), Color.White);

            screenPoint = GraphToScreenSpace(c.P2.X, c.P2.Y);
            rect.X = (int)screenPoint.X;
            rect.Y = (int)screenPoint.Y;
            sb.Draw(_blank, rect, Color.Red);
            sb.DrawString(_font, c.P2.X + ", " + c.P2.Y, new Vector2(rect.X + 8, rect.Y + 8), Color.White);
        }
    }
}
