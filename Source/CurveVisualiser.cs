using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grondslag;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CurveCreator
{
    public class CurveVisualiser
    {
        private Vector2i _pos; // top-right
        private int _width, _height;
        private int _lineThickness;
        private float _accuracy; // (0, 1]. Percentage of pixels on the x-axis that will have their corresponding y-values individually calculated.
        private float _pointDist; // Distance between calculated points. Screen-space.

        private Texture2D _blank;

        public CurveVisualiser(Texture2D blank)
        {
            _blank = blank;

            _pos = new Vector2i(100, 100);
            _width = 400;
            _height = 400;
            _lineThickness = 10;

            _accuracy = 1f;
            _pointDist = (1 / _accuracy) * (_width / 100);
        }

        public void Draw(Curve c, SpriteBatch sb)
        {
            sb.Draw(_blank, new Rectangle(_pos.X, _pos.Y, _width, _height), new Color(15, 15, 15));

            Rectangle rect = new Rectangle(0, 0, (int)MathF.Ceiling(_pointDist), _lineThickness);
            int domain = c.Upper - c.Lower;
            float pointDist = (1 / _accuracy) * (domain / 100); // Graph-space.

            for (float x = c.Lower; x < c.Upper; x += pointDist)
            {
                float yValue = c.GetValue(x);
                int xPos = (int)((x - c.Lower) / domain * _width); // Convert to screenspace.
                int yPos = (int)(yValue * _height);

                rect.X = _pos.X + xPos;
                rect.Y = _pos.Y + _height - (yPos - (rect.Height/2));

                sb.Draw(_blank, rect, Color.White);
            }
        }
    }
}
