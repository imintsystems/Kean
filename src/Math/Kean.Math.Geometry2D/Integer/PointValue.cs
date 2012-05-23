// 
//  PointValue.cs
//  
//  Author:
//       Simon Mika <smika@hx.se>
//  
//  Copyright (c) 2011 Simon Mika
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
using Kean.Core.Extension;

namespace Kean.Math.Geometry2D.Integer
{
    public struct PointValue :
        Abstract.IPoint<int>, 
        Abstract.IVector<int>,
        IEquatable<PointValue>
    {
        public int X;
        public int Y;
        #region IPoint<int> Members
        int Kean.Math.Geometry2D.Abstract.IPoint<int>.X { get { return this.X; } }
        int Kean.Math.Geometry2D.Abstract.IPoint<int>.Y { get { return this.Y; } }
        #endregion
        #region IVector<int> Members
        int Kean.Math.Geometry2D.Abstract.IVector<int>.X { get { return this.X; } }
        int Kean.Math.Geometry2D.Abstract.IVector<int>.Y { get { return this.Y; } }
        #endregion
        public int Norm { get { return Kean.Math.Integer.SquareRoot(this.ScalarProduct(this)); } }
        public int Azimuth { get { return Kean.Math.Integer.ArcusTangensExtended(this.Y, this.X); } }
        #region Static Constants
        public static PointValue BasisX { get { return new PointValue(1, 0); } }
        public static PointValue BasisY { get { return new PointValue(0, 1); } }
        #endregion
        public PointValue(int x, int y)
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
        public int Angle(PointValue other)
        {
            int result = 0;
            result = this.ScalarProduct(other) / (this.Norm * other.Norm);
            int sign = this.X * other.Y - this.Y * other.X;
            result = Kean.Math.Integer.ArcusCosinus(Kean.Math.Integer.Clamp(result, -1, 1));
            result *= sign < 0 ? -1 : 1;
            return result;
        }
        public int ScalarProduct(PointValue other)
        {
            return this.X * other.X + this.Y * other.Y;
        }
        public int Distance(PointValue other)
        {
            return (this - other).Norm;
        }
        #region Utility functions
        public PointValue Swap()
        {
            return new PointValue(this.Y, this.X);
        }
        public PointValue Round()
        {
            return new PointValue(Kean.Math.Integer.Round(this.X), Kean.Math.Integer.Round(this.Y));
        }
        public PointValue Ceiling()
        {
            return new PointValue(Kean.Math.Integer.Ceiling(this.X), Kean.Math.Integer.Ceiling(this.Y));
        }
        public PointValue Floor()
        {
            return new PointValue(Kean.Math.Integer.Floor(this.X), Kean.Math.Integer.Floor(this.Y));
        }
        public PointValue Minimum(Abstract.IVector<int> floor)
        {
            return new PointValue(Kean.Math.Integer.Minimum(this.X, floor.X), Kean.Math.Integer.Minimum(this.Y, floor.Y));
        }
        public PointValue Maximum(Abstract.IVector<int> ceiling)
        {
            return new PointValue(Kean.Math.Integer.Maximum(this.X, ceiling.X), Kean.Math.Integer.Maximum(this.Y, ceiling.Y));
        }
        public PointValue Clamp(Abstract.IVector<int> floor, Abstract.IVector<int> ceiling)
        {
            return new PointValue(Kean.Math.Integer.Clamp(this.X, floor.X, ceiling.X), Kean.Math.Integer.Clamp(this.Y, floor.Y, ceiling.Y));
        }
        #endregion
        #region Arithmetic Vector - Vector Operators
        public static PointValue operator +(PointValue left, PointValue right)
        {
            return new PointValue(left.X + right.X, left.Y + right.Y);
        }
        public static PointValue operator +(PointValue left, Abstract.IVector<int> right)
        {
            return new PointValue(left.X + right.X, left.Y + right.Y);
        }
        public static PointValue operator +(Abstract.IVector<int> left, PointValue right)
        {
            return new PointValue(left.X + right.X, left.Y + right.Y);
        }
        public static PointValue operator -(PointValue left, PointValue right)
        {
            return new PointValue(left.X - right.X, left.Y - right.Y);
        }
        public static PointValue operator -(PointValue left, Abstract.IVector<int> right)
        {
            return new PointValue(left.X - right.X, left.Y - right.Y);
        }
        public static PointValue operator -(Abstract.IVector<int> left, PointValue right)
        {
            return new PointValue(left.X - right.X, left.Y - right.Y);
        }
        public static PointValue operator -(PointValue vector)
        {
            return new PointValue(-vector.X, -vector.Y);
        }
        public static PointValue operator *(PointValue left, PointValue right)
        {
            return new PointValue(left.X * right.X, left.Y * right.Y);
        }
        public static PointValue operator *(PointValue left, Abstract.IVector<int> right)
        {
            return new PointValue(left.X * right.X, left.Y * right.Y);
        }
        public static PointValue operator *(Abstract.IVector<int> left, PointValue right)
        {
            return new PointValue(left.X * right.X, left.Y * right.Y);
        }
        public static PointValue operator /(PointValue left, PointValue right)
        {
            return new PointValue(left.X / right.X, left.Y / right.Y);
        }
        public static PointValue operator /(PointValue left, Abstract.IVector<int> right)
        {
            return new PointValue(left.X / right.X, left.Y / right.Y);
        }
        public static PointValue operator /(Abstract.IVector<int> left, PointValue right)
        {
            return new PointValue(left.X / right.X, left.Y / right.Y);
        }
        #endregion
        #region Arithmetic Vector and Scalar
        public static PointValue operator *(PointValue left, int right)
        {
            return new PointValue(left.X * right, left.Y * right);
        }
        public static PointValue operator *(int left, PointValue right)
        {
            return right * left;
        }
        public static PointValue operator /(PointValue left, int right)
        {
            return new PointValue(left.X / right, left.Y / right);
        }
        #endregion
        #region Arithmetic Transform and Vector
        public static PointValue operator *(TransformValue left, PointValue right)
        {
            return new PointValue(left.A * right.X + left.C * right.Y + left.E, left.B * right.X + left.D * right.Y + left.F);
        }
        #endregion
        #region Comparison Operators
        /// <summary>
        /// Defines equality.
        /// </summary>
        /// <param name="Left">Point Left of operator.</param>
        /// <param name="Right">Point Right of operator.</param>
        /// <returns>True if <paramref name="Left"/> equals <paramref name="Right"/> else false.</returns>
        public static bool operator ==(PointValue left, PointValue right)
        {
            return left.X == right.X && left.Y == right.Y;
        }
        /// <summary>
        /// Defines inequality.
        /// </summary>
        /// <param name="Left">Point Left of operator.</param>
        /// <param name="Right">Point Right of operator.</param>
        /// <returns>False if <paramref name="Left"/> equals <paramref name="Right"/> else true.</returns>
        public static bool operator !=(PointValue left, PointValue right)
        {
            return !(left == right);
        }
        public static bool operator <(PointValue left, PointValue right)
        {
            return left.X < right.X && left.Y < right.Y;
        }
        public static bool operator >(PointValue left, PointValue right)
        {
            return left.X > right.X && left.Y > right.Y;
        }
        public static bool operator <=(PointValue left, PointValue right)
        {
            return left.X <= right.X && left.Y <= right.Y;
        }
        public static bool operator >=(PointValue left, PointValue right)
        {
            return left.X >= right.X && left.Y >= right.Y;
        }
        #endregion
        #region IEquatable<PointValue> Members
        public bool Equals(PointValue other)
        {
            return this == other;
        }
        #endregion
        #region Object Overrides
        public override bool Equals(object other)
        {
            return other is PointValue && this.Equals((PointValue)other);
        }
        public override int GetHashCode()
        {
            return 33 * this.X.GetHashCode() ^ this.Y.GetHashCode();
        }
		public override string ToString()
        {
			return Kean.Math.Integer.ToString(this.X) + ", " + Kean.Math.Integer.ToString(this.Y);
		}
        #endregion
        #region Static Creators
        public static PointValue Polar(int radius, int azimuth)
        {
            return new PointValue(radius * Kean.Math.Integer.Cosinus(azimuth), radius * Kean.Math.Integer.Sinus(azimuth));
        }
        #endregion
        #region Casts
        public static explicit operator int[](PointValue value)
        {
            return new int[] { value.X, value.Y };
        }
        public static explicit operator PointValue(int[] value)
        {
            return new PointValue(value[0], value[1]);
        }
        public static implicit operator string(PointValue value)
        {
            return value.NotNull() ? value.ToString() : null;
        }
        public static implicit operator PointValue(string value)
        {
            PointValue result = new PointValue();
            if (value.NotEmpty())
            {
                try
                {
                    string[] values = value.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length == 2)
                        result = new PointValue(Kean.Math.Integer.Parse(values[0]), Kean.Math.Integer.Parse(values[1]));
                }
                catch
                {
                }
            }
            return result;
        }
        public static explicit operator SizeValue(PointValue value)
        {
            return new SizeValue(value.X, value.Y);
        }
        public static explicit operator PointValue(SizeValue value)
        {
            return new PointValue(value.Width, value.Height);
        }
        public static PointValue Maximum(PointValue left, PointValue right)
        {
            return new PointValue(Kean.Math.Integer.Maximum(left.X, right.X), Kean.Math.Integer.Maximum(left.Y, right.Y));
        }
        public static PointValue Minimum(PointValue left, PointValue right)
        {
            return new PointValue(Kean.Math.Integer.Minimum(left.X, right.X), Kean.Math.Integer.Minimum(left.Y, right.Y));
        }
        #endregion
  }
}
