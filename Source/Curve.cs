using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CurveCreator
{
    public struct Curve
    {
        private int _lower, _upper;
        private float _exponent;

        private float _gradient;

        public Curve(int lower, int upper, float exponent)
        {
            _lower = lower;
            _upper = upper;
            _exponent = exponent;
            _gradient = 0;

            CalculateGradient();
        }

        public int Lower => _lower;
        public int Upper => _upper;
        public float Exponent => _exponent;

        public void SetLowerBound(int value)
        {
            _lower = value;
            CalculateGradient();
        }
        public void SetUpperBound(int value)
        {
            _upper = value;
            CalculateGradient();
        }
        public void SetExponent(int value)
        {
            _exponent = value;
            CalculateGradient();
        }

        private void CalculateGradient()
        {
            _gradient = 1 / ((float)_upper - _lower);
        }

        public float GetValue(float xValue)
        {
            return _gradient * (int)Math.Pow(xValue - Lower, Exponent);
        }
    }
}
