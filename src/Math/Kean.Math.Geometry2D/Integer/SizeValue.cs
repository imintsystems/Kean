// 
//  SizeValue.cs
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
    public struct SizeValue :
        Abstract.ISize<int>, 
        Abstract.IVector<int>,
        IEquatable<SizeValue>
    {
        public int Width;
        public int Height;
        #region IPoint<int> Members
        int Kean.Math.Geometry2D.Abstract.ISize<int>.Width { get { return this.Width; } }
        int Kean.Math.Geometry2D.Abstract.ISize<int>.Height { get { return this.Height; } }
        #endregion
        #region IVector<int> Members
        int Kean.Math.Geometry2D.Abstract.IVector<int>.X { get { return this.Width; } }
        int Kean.Math.Geometry2D.Abstract.IVector<int>.Y { get { return this.Height; } }
        #endregion
        public int Area { get { return this.Width * this.Height; } }
        public int Length { get { return this.Norm; } }
        public bool Empty { get { return this.Width == 0 || this.Height == 0; } }
        public int Norm { get { return Kean.Math.Integer.SquareRoot(this.ScalarProduct(this)); } }
        public int Azimuth { get { return Kean.Math.Integer.ArcusTangensExtended(this.Height, this.Width); } }
        #region Static Constants
        public static SizeValue BasisX { get { return new SizeValue(1, 0); } }
        public static SizeValue BasisY { get { return new SizeValue(0, 1); } }
        #endregion
        public SizeValue(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
        public int PNorm(int p)
        {
            int result;
            if (p == int.MaxValue)
                result = Kean.Math.Integer.Maximum(Kean.Math.Integer.Absolute(this.Width), Kean.Math.Integer.Absolute(this.Height));
            else if (p == 1)
                result = Kean.Math.Integer.Absolute(this.Width) + Kean.Math.Integer.Absolute(this.Height);
            else
                result = Kean.Math.Integer.Power(Kean.Math.Integer.Power(Kean.Math.Integer.Absolute(this.Width), p) + Kean.Math.Integer.Power(Kean.Math.Integer.Absolute(this.Height), p), 1 / p);
            return result;
        }
        /// <summary>
        /// Angle from current to other point vector.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Angle in radians.</returns>
        public int Angle(SizeValue other)
        {
            int result = 0;
            result = this.ScalarProduct(other) / (this.Norm * other.Norm);
            int sign = this.Width * other.Height - this.Height * other.Width;
            result = Kean.Math.Integer.ArcusCosinus(Kean.Math.Integer.Clamp(result, -1, 1));
            result *= sign < 0 ? -1 : 1;
            return result;
        }
        public int ScalarProduct(SizeValue other)
        {
            return this.Width * other.Width + this.Height * other.Height;
        }
        public int Distance(SizeValue other)
        {
            return (this - other).Norm;
        }
        #region Utility functions
        public SizeValue Swap()
        {
            return new SizeValue(this.Height, this.Width);
        }
        public SizeValue Round()
        {
            return new SizeValue(Kean.Math.Integer.Round(this.Width), Kean.Math.Integer.Round(this.Height));
        }
        public SizeValue Ceiling()
        {
            return new SizeValue(Kean.Math.Integer.Ceiling(this.Width), Kean.Math.Integer.Ceiling(this.Height));
        }
        public SizeValue Floor()
        {
            return new SizeValue(Kean.Math.Integer.Floor(this.Width), Kean.Math.Integer.Floor(this.Height));
        }
        public SizeValue Minimum(Abstract.IVector<int> floor)
        {
            return new SizeValue(Kean.Math.Integer.Minimum(this.Width, floor.X), Kean.Math.Integer.Minimum(this.Height, floor.Y));
        }
        public SizeValue Maximum(Abstract.IVector<int> ceiling)
        {
            return new SizeValue(Kean.Math.Integer.Maximum(this.Width, ceiling.X), Kean.Math.Integer.Maximum(this.Height, ceiling.Y));
        }
        public SizeValue Clamp(Abstract.IVector<int> floor, Abstract.IVector<int> ceiling)
        {
            return new SizeValue(Kean.Math.Integer.Clamp(this.Width, floor.X, ceiling.X), Kean.Math.Integer.Clamp(this.Height, floor.Y, ceiling.Y));
        }
        #endregion
        #region Arithmetic Vector - Vector Operators
        public static SizeValue operator +(SizeValue left, SizeValue right)
        {
            return new SizeValue(left.Width + right.Width, left.Height + right.Height);
        }
        public static SizeValue operator +(SizeValue left, Abstract.IVector<int> right)
        {
            return new SizeValue(left.Width + right.X, left.Height + right.Y);
        }
        public static SizeValue operator +(Abstract.IVector<int> left, SizeValue right)
        {
            return new SizeValue(left.X + right.Width, left.Y + right.Height);
        }
        public static SizeValue operator -(SizeValue left, SizeValue right)
        {
            return new SizeValue(left.Width - right.Width, left.Height - right.Height);
        }
        public static SizeValue operator -(SizeValue left, Abstract.IVector<int> right)
        {
            return new SizeValue(left.Width - right.X, left.Height - right.Y);
        }
        public static SizeValue operator -(Abstract.IVector<int> left, SizeValue right)
        {
            return new SizeValue(left.X - right.Width, left.Y - right.Height);
        }
        public static SizeValue operator -(SizeValue vector)
        {
            return new SizeValue(-vector.Width, -vector.Height);
        }
        public static SizeValue operator *(SizeValue left, SizeValue right)
        {
            return new SizeValue(left.Width * right.Width, left.Height * right.Height);
        }
        public static SizeValue operator *(SizeValue left, Abstract.IVector<int> right)
        {
            return new SizeValue(left.Width * right.X, left.Height * right.Y);
        }
        public static SizeValue operator *(Abstract.IVector<int> left, SizeValue right)
        {
            return new SizeValue(left.X * right.Width, left.Y * right.Height);
        }
        public static SizeValue operator /(SizeValue left, SizeValue right)
        {
            return new SizeValue(left.Width / right.Width, left.Height / right.Height);
        }
        public static SizeValue operator /(SizeValue left, Abstract.IVector<int> right)
        {
            return new SizeValue(left.Width / right.X, left.Height / right.Y);
        }
        public static SizeValue operator /(Abstract.IVector<int> left, SizeValue right)
        {
            return new SizeValue(left.X / right.Width, left.Y / right.Height);
        }
        #endregion
        #region Arithmetic Vector and Scalar
        public static SizeValue operator *(SizeValue left, int right)
        {
            return new SizeValue(left.Width * right, left.Height * right);
        }
        public static SizeValue operator *(int left, SizeValue right)
        {
            return right * left;
        }
        public static SizeValue operator /(SizeValue left, int right)
        {
            return new SizeValue(left.Width / right, left.Height / right);
        }
        #endregion
        #region Arithmetic Transform and Vector
        public static SizeValue operator *(TransformValue left, SizeValue right)
        {
            return new SizeValue(left.A * right.Width + left.C * right.Height, left.B * right.Width + left.D * right.Height);
        }
        #endregion
        #region Comparison Operators
        /// <summary>
        /// Defines equality.
        /// </summary>
        /// <param name="Left">Point Left of operator.</param>
        /// <param name="Right">Point Right of operator.</param>
        /// <returns>True if <paramref name="Left"/> equals <paramref name="Right"/> else false.</returns>
        public static bool operator ==(SizeValue left, SizeValue right)
        {
            return left.Width == right.Width && left.Height == right.Height;
        }
        /// <summary>
        /// Defines inequality.
        /// </summary>
        /// <param name="Left">Point Left of operator.</param>
        /// <param name="Right">Point Right of operator.</param>
        /// <returns>False if <paramref name="Left"/> equals <paramref name="Right"/> else true.</returns>
        public static bool operator !=(SizeValue left, SizeValue right)
        {
            return !(left == right);
        }
        public static bool operator <(SizeValue left, SizeValue right)
        {
            return left.Width < right.Width && left.Height < right.Height;
        }
        public static bool operator >(SizeValue left, SizeValue right)
        {
            return left.Width > right.Width && left.Height > right.Height;
        }
        public static bool operator <=(SizeValue left, SizeValue right)
        {
            return left.Width <= right.Width && left.Height <= right.Height;
        }
        public static bool operator >=(SizeValue left, SizeValue right)
        {
            return left.Width >= right.Width && left.Height >= right.Height;
        }
        #endregion
        #region IEquatable<SizeValue> Members
        public bool Equals(SizeValue other)
        {
            return this == other;
        }
        #endregion
        #region Object Overrides
        public override bool Equals(object other)
        {
            return other is SizeValue && this.Equals((SizeValue)other);
        }
        public override int GetHashCode()
        {
            return 33 * this.Width.GetHashCode() ^ this.Height.GetHashCode();
        }
		public override string ToString()
        {
			return Kean.Math.Integer.ToString(this.Width) + ", " + Kean.Math.Integer.ToString(this.Height);
		}
        #endregion
        #region Static Creators
        public static SizeValue Polar(int radius, int azimuth)
        {
            return new SizeValue(radius * Kean.Math.Integer.Cosinus(azimuth), radius * Kean.Math.Integer.Sinus(azimuth));
        }
        #endregion
        #region Casts
        public static explicit operator int[](SizeValue value)
        {
            return new int[] { value.Width, value.Height };
        }
        public static explicit operator SizeValue(int[] value)
        {
            return new SizeValue(value[0], value[1]);
        }
        public static implicit operator string(SizeValue value)
        {
            return value.NotNull() ? value.ToString() : null;
        }
        public static implicit operator SizeValue(string value)
        {
            SizeValue result = new SizeValue();
            if (value.NotEmpty())
            {
                try
                {
                    string[] values = value.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length == 2)
                        result = new SizeValue(Kean.Math.Integer.Parse(values[0]), Kean.Math.Integer.Parse(values[1]));
                }
                catch
                {
                }
            }
            return result;
        }
        public static SizeValue Maximum(SizeValue left, SizeValue right)
        {
            return new SizeValue(Kean.Math.Integer.Maximum(left.Width, right.Width), Kean.Math.Integer.Maximum(left.Height, right.Height));
        }
        public static SizeValue Minimum(SizeValue left, SizeValue right)
        {
            return new SizeValue(Kean.Math.Integer.Minimum(left.Width, right.Width), Kean.Math.Integer.Minimum(left.Height, right.Height));
        }
        #endregion
  }
}
