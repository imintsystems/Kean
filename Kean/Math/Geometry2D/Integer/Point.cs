// 
//  Point.cs (generated by template)
//  
//  Author:
//       Simon Mika <smika@hx.se>
//  
//  Copyright (c) 2011-2013 Simon Mika
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using Kean.Extension;

namespace Kean.Math.Geometry2D.Integer
{
    public struct Point :
        IEquatable<Point>
    {
        public int X;
        public int Y;
        public int Length { get { return this.Norm; } }
        public int Norm { get { return Kean.Math.Integer.SquareRoot(this.ScalarProduct(this)); } }
        public int Azimuth { get { return Kean.Math.Integer.ArcusTangensExtended(this.Y, this.X); } }
        #region Static Constants
        public static Point BasisX { get { return new Point(1, 0); } }
        public static Point BasisY { get { return new Point(0, 1); } }
        #endregion
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public int PNorm(int p)
        {
            int result;
            if (p == int.MaxValue)
                result = Kean.Math.Integer.Maximum(Kean.Math.Integer.Absolute(this.X), Kean.Math.Integer.Absolute(this.Y));
            else if (p == 1)
                result = Kean.Math.Integer.Absolute(this.X) + Kean.Math.Integer.Absolute(this.Y);
            else
                result = Kean.Math.Integer.Power(Kean.Math.Integer.Power(Kean.Math.Integer.Absolute(this.X), p) + Kean.Math.Integer.Power(Kean.Math.Integer.Absolute(this.Y), p), 1 / p);
            return result;
        }
        /// <summary>
        /// Angle from current to other point vector.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Angle in radians.</returns>
        public int Angle(Point other)
        {
            int result = 0;
            result = this.ScalarProduct(other) / (this.Norm * other.Norm);
            int sign = this.X * other.Y - this.Y * other.X;
            result = Kean.Math.Integer.ArcusCosinus(Kean.Math.Integer.Clamp(result, -1, 1));
            result *= sign < 0 ? -1 : 1;
            return result;
        }
        public int ScalarProduct(Point other)
        {
            return this.X * other.X + this.Y * other.Y;
        }
        public int Distance(Point other)
        {
            return (this - other).Norm;
        }
        #region Utility functions
        public Point Swap()
        {
            return new Point(this.Y, this.X);
        }
        public Point Round()
        {
            return new Point(Kean.Math.Integer.Round(this.X), Kean.Math.Integer.Round(this.Y));
        }
        public Point Ceiling()
        {
            return new Point(Kean.Math.Integer.Ceiling(this.X), Kean.Math.Integer.Ceiling(this.Y));
        }
        public Point Floor()
        {
            return new Point(Kean.Math.Integer.Floor(this.X), Kean.Math.Integer.Floor(this.Y));
        }
        public Point Minimum(Size floor)
        {
            return new Point(Kean.Math.Integer.Minimum(this.X, floor.Width), Kean.Math.Integer.Minimum(this.Y, floor.Height));
        }
        public Point Maximum(Size ceiling)
        {
            return new Point(Kean.Math.Integer.Maximum(this.X, ceiling.Width), Kean.Math.Integer.Maximum(this.Y, ceiling.Height));
        }
        public Point Clamp(Size floor, Size ceiling)
        {
            return new Point(Kean.Math.Integer.Clamp(this.X, floor.Width, ceiling.Width), Kean.Math.Integer.Clamp(this.Y, floor.Height, ceiling.Height));
        }
        #endregion
        #region Arithmetic Vector - Vector Operators
        public static Point operator +(Point left, Point right)
        {
            return new Point(left.X + right.X, left.Y + right.Y);
        }
        public static Point operator +(Point left, Size right)
        {
            return new Point(left.X + right.Width, left.Y + right.Height);
        }
        public static Point operator +(Size left, Point right)
        {
            return new Point(left.Width + right.X, left.Height + right.Y);
        }
        public static Point operator -(Point left, Point right)
        {
            return new Point(left.X - right.X, left.Y - right.Y);
        }
        public static Point operator -(Point left, Size right)
        {
            return new Point(left.X - right.Width, left.Y - right.Height);
        }
        public static Point operator -(Size left, Point right)
        {
            return new Point(left.Width - right.X, left.Height - right.Y);
        }
        public static Point operator -(Point vector)
        {
            return new Point(-vector.X, -vector.Y);
        }
        public static Point operator *(Point left, Point right)
        {
            return new Point(left.X * right.X, left.Y * right.Y);
        }
        public static Point operator *(Point left, Size right)
        {
            return new Point(left.X * right.Width, left.Y * right.Height);
        }
        public static Point operator *(Size left, Point right)
        {
            return new Point(left.Width * right.X, left.Height * right.Y);
        }
        public static Point operator /(Point left, Point right)
        {
            return new Point(left.X / right.X, left.Y / right.Y);
        }
        public static Point operator /(Point left, Size right)
        {
            return new Point(left.X / right.Width, left.Y / right.Height);
        }
        public static Point operator /(Size left, Point right)
        {
            return new Point(left.Width / right.X, left.Height / right.Y);
        }
        #endregion
        #region Arithmetic Vector and Scalar
        public static Point operator *(Point left, int right)
        {
            return new Point(left.X * right, left.Y * right);
        }
        public static Point operator *(int left, Point right)
        {
            return right * left;
        }


        public static Point operator /(Point left, int right)
        {
            return new Point(left.X / right, left.Y / right);
        }
        #endregion
        #region Arithmetic Transform and Vector
        public static Point operator *(Transform left, Point right)
        {
            return new Point(left.A * right.X + left.C * right.Y + left.E, left.B * right.X + left.D * right.Y + left.F);
        }
        #endregion
        #region Comparison Operators
        /// <summary>
        /// Defines equality.
        /// </summary>
        /// <param name="Left">Point Left of operator.</param>
        /// <param name="Right">Point Right of operator.</param>
        /// <returns>True if <paramref name="Left"/> equals <paramref name="Right"/> else false.</returns>
        public static bool operator ==(Point left, Point right)
        {
            return left.X == right.X && left.Y == right.Y;
        }
        /// <summary>
        /// Defines inequality.
        /// </summary>
        /// <param name="Left">Point Left of operator.</param>
        /// <param name="Right">Point Right of operator.</param>
        /// <returns>False if <paramref name="Left"/> equals <paramref name="Right"/> else true.</returns>
        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }
        public static bool operator <(Point left, Point right)
        {
            return left.X < right.X && left.Y < right.Y;
        }
        public static bool operator >(Point left, Point right)
        {
            return left.X > right.X && left.Y > right.Y;
        }
        public static bool operator <=(Point left, Point right)
        {
            return left.X <= right.X && left.Y <= right.Y;
        }
        public static bool operator >=(Point left, Point right)
        {
            return left.X >= right.X && left.Y >= right.Y;
        }
        #endregion
        #region IEquatable<Point> Members
        public bool Equals(Point other)
        {
            return this == other;
        }
        #endregion
        #region Object Overrides
        public override bool Equals(object other)
        {
            return other is Point && this.Equals((Point)other);
        }
        public override int GetHashCode()
        {
            return 33 * this.X.GetHashCode() ^ this.Y.GetHashCode();
        }
        public override string ToString()
        {
			return this.ToString("{0}, {1}");
		}
		public string ToString(string format)
		{
			return String.Format(format, Kean.Math.Integer.ToString(this.X), Kean.Math.Integer.ToString(this.Y));
		}
        #endregion
        #region Static Creators
        public static Point Polar(int radius, int azimuth)
        {
            return new Point(radius * Kean.Math.Integer.Cosinus(azimuth), radius * Kean.Math.Integer.Sinus(azimuth));
        }
        #endregion
        #region Casts
        public static implicit operator int[](Point value)
        {
            return new int[] { value.X, value.Y };
        }
        public static explicit operator Point(int[] value)
        {
            return new Point(value[0], value[1]);
        }
        public static implicit operator string(Point value)
        {
            return value.ToString();
        }
        public static explicit operator Point(string value)
        {
            Point result = new Point();
            if (value.NotEmpty())
            {
                try
                {
                    string[] values = value.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length == 2)
                        result = new Point(Kean.Math.Integer.Parse(values[0]), Kean.Math.Integer.Parse(values[1]));
                }
                catch
                {
                }
            }
            return result;
        }
        public static explicit operator Size(Point value)
        {
            return new Size(value.X, value.Y);
        }
        public static explicit operator Point(Size value)
        {
            return new Point(value.Width, value.Height);
        }
        public static Point Maximum(Point left, Point right)
        {
            return new Point(Kean.Math.Integer.Maximum(left.X, right.X), Kean.Math.Integer.Maximum(left.Y, right.Y));
        }
        public static Point Minimum(Point left, Point right)
        {
            return new Point(Kean.Math.Integer.Minimum(left.X, right.X), Kean.Math.Integer.Minimum(left.Y, right.Y));
        }
        #endregion
  }
}
