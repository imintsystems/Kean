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

namespace Kean.Math.Geometry2D.Double
{
    public struct SizeValue :
        Abstract.ISize<double>, 
        Abstract.IVector<double>,
        IEquatable<SizeValue>
    {
        public double Width;
        public double Height;
        #region IPoint<double> Members
        double Kean.Math.Geometry2D.Abstract.ISize<double>.Width { get { return this.Width; } }
        double Kean.Math.Geometry2D.Abstract.ISize<double>.Height { get { return this.Height; } }
        #endregion
        #region IVector<double> Members
        double Kean.Math.Geometry2D.Abstract.IVector<double>.X { get { return this.Width; } }
        double Kean.Math.Geometry2D.Abstract.IVector<double>.Y { get { return this.Height; } }
        #endregion
        public double Area { get { return this.Width * this.Height; } }
        public double Length { get { return this.Norm; } }
        public bool Empty { get { return this.Width == 0 || this.Height == 0; } }
        public double Norm { get { return Kean.Math.Double.SquareRoot(this.ScalarProduct(this)); } }
        public double Azimuth { get { return Kean.Math.Double.ArcusTangensExtended(this.Height, this.Width); } }
        #region Static Constants
        public static SizeValue BasisX { get { return new SizeValue(1, 0); } }
        public static SizeValue BasisY { get { return new SizeValue(0, 1); } }
        #endregion
        public SizeValue(double width, double height)
        {
            this.Width = width;
            this.Height = height;
        }
        public double PNorm(double p)
        {
            double result;
            if (double.IsPositiveInfinity(p))
                result = Kean.Math.Double.Maximum(Kean.Math.Double.Absolute(this.Width), Kean.Math.Double.Absolute(this.Height));
            else if (p == 1)
                result = Kean.Math.Double.Absolute(this.Width) + Kean.Math.Double.Absolute(this.Height);
            else
                result = Kean.Math.Double.Power(Kean.Math.Double.Power(Kean.Math.Double.Absolute(this.Width), p) + Kean.Math.Double.Power(Kean.Math.Double.Absolute(this.Height), p), 1 / p);
            return result;
        }
        /// <summary>
        /// Angle from current to other point vector.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Angle in radians.</returns>
        public double Angle(SizeValue other)
        {
            double result = 0;
            result = this.ScalarProduct(other) / (this.Norm * other.Norm);
            double sign = this.Width * other.Height - this.Height * other.Width;
            result = Kean.Math.Double.ArcusCosinus(Kean.Math.Double.Clamp(result, -1, 1));
            result *= sign < 0 ? -1 : 1;
            return result;
        }
        public double ScalarProduct(SizeValue other)
        {
            return this.Width * other.Width + this.Height * other.Height;
        }
        public double Distance(SizeValue other)
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
            return new SizeValue(Kean.Math.Double.Round(this.Width), Kean.Math.Double.Round(this.Height));
        }
        public SizeValue Ceiling()
        {
            return new SizeValue(Kean.Math.Double.Ceiling(this.Width), Kean.Math.Double.Ceiling(this.Height));
        }
        public SizeValue Floor()
        {
            return new SizeValue(Kean.Math.Double.Floor(this.Width), Kean.Math.Double.Floor(this.Height));
        }
        public SizeValue Minimum(Abstract.IVector<double> floor)
        {
            return new SizeValue(Kean.Math.Double.Minimum(this.Width, floor.X), Kean.Math.Double.Minimum(this.Height, floor.Y));
        }
        public SizeValue Maximum(Abstract.IVector<double> ceiling)
        {
            return new SizeValue(Kean.Math.Double.Maximum(this.Width, ceiling.X), Kean.Math.Double.Maximum(this.Height, ceiling.Y));
        }
        public SizeValue Clamp(Abstract.IVector<double> floor, Abstract.IVector<double> ceiling)
        {
            return new SizeValue(Kean.Math.Double.Clamp(this.Width, floor.X, ceiling.X), Kean.Math.Double.Clamp(this.Height, floor.Y, ceiling.Y));
        }
        #endregion
        #region Arithmetic Vector - Vector Operators
        public static SizeValue operator +(SizeValue left, SizeValue right)
        {
            return new SizeValue(left.Width + right.Width, left.Height + right.Height);
        }
        public static SizeValue operator +(SizeValue left, Abstract.IVector<double> right)
        {
            return new SizeValue(left.Width + right.X, left.Height + right.Y);
        }
        public static SizeValue operator +(Abstract.IVector<double> left, SizeValue right)
        {
            return new SizeValue(left.X + right.Width, left.Y + right.Height);
        }
        public static SizeValue operator -(SizeValue left, SizeValue right)
        {
            return new SizeValue(left.Width - right.Width, left.Height - right.Height);
        }
        public static SizeValue operator -(SizeValue left, Abstract.IVector<double> right)
        {
            return new SizeValue(left.Width - right.X, left.Height - right.Y);
        }
        public static SizeValue operator -(Abstract.IVector<double> left, SizeValue right)
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
        public static SizeValue operator *(SizeValue left, Abstract.IVector<double> right)
        {
            return new SizeValue(left.Width * right.X, left.Height * right.Y);
        }
        public static SizeValue operator *(Abstract.IVector<double> left, SizeValue right)
        {
            return new SizeValue(left.X * right.Width, left.Y * right.Height);
        }
        public static SizeValue operator /(SizeValue left, SizeValue right)
        {
            return new SizeValue(left.Width / right.Width, left.Height / right.Height);
        }
        public static SizeValue operator /(SizeValue left, Abstract.IVector<double> right)
        {
            return new SizeValue(left.Width / right.X, left.Height / right.Y);
        }
        public static SizeValue operator /(Abstract.IVector<double> left, SizeValue right)
        {
            return new SizeValue(left.X / right.Width, left.Y / right.Height);
        }
        #endregion
        #region Arithmetic Vector and Scalar
        public static SizeValue operator *(SizeValue left, double right)
        {
            return new SizeValue(left.Width * right, left.Height * right);
        }
        public static SizeValue operator *(double left, SizeValue right)
        {
            return right * left;
        }

        public static SizeValue operator *(SizeValue left, float right)
        {
            return new SizeValue(left.Width * right, left.Height * right);
        } 
        public static SizeValue operator *(SizeValue left, int right)
        {
            return new SizeValue(left.Width * right, left.Height * right);
        }
        public static SizeValue operator *(float left, SizeValue right)
        {
           return right * left;
        } 
        public static SizeValue operator *(int left, SizeValue right)
        {
            return right * left;
        }
        public static SizeValue operator /(SizeValue left, float right)
        {
            return new SizeValue(left.Width / right, left.Height / right);
        } 
        public static SizeValue operator /(SizeValue left, int right)
        {
            return new SizeValue(left.Width / right, left.Height / right);
        }

        public static SizeValue operator /(SizeValue left, double right)
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
			return Kean.Math.Double.ToString(this.Width) + ", " + Kean.Math.Double.ToString(this.Height);
		}
        #endregion
        #region Static Creators
        public static SizeValue Polar(double radius, double azimuth)
        {
            return new SizeValue(radius * Kean.Math.Double.Cosinus(azimuth), radius * Kean.Math.Double.Sinus(azimuth));
        }
        #endregion
        #region Casts
        public static explicit operator double[](SizeValue value)
        {
            return new double[] { value.Width, value.Height };
        }
        public static explicit operator SizeValue(double[] value)
        {
            return new SizeValue(value[0], value[1]);
        }
        public static implicit operator string(SizeValue value)
        {
            return value.ToString();
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
                        result = new SizeValue(Kean.Math.Double.Parse(values[0]), Kean.Math.Double.Parse(values[1]));
                }
                catch
                {
                }
            }
            return result;
        }
        public static implicit operator SizeValue(Single.SizeValue value)
        {
            return new SizeValue(value.Width, value.Height);
        }
        public static implicit operator SizeValue(Integer.SizeValue value)
        {
            return new SizeValue(value.Width, value.Height);
        }
        public static explicit operator Single.SizeValue(SizeValue value)
        {
            return new Single.SizeValue(Kean.Math.Single.Convert(value.Width), Kean.Math.Single.Convert(value.Height));
        }
        public static explicit operator Integer.SizeValue(SizeValue value)
        {
            return new Integer.SizeValue(Kean.Math.Integer.Convert(value.Width), Kean.Math.Integer.Convert(value.Height));
        }
        #endregion
        #region Static Operators
        public static SizeValue Ceiling(Geometry2D.Single.SizeValue other)
        {
            return new SizeValue(Kean.Math.Integer.Ceiling(other.Width), Kean.Math.Integer.Ceiling(other.Height));
        }
        public static SizeValue Floor(Geometry2D.Single.SizeValue other)
        {
            return new SizeValue(Kean.Math.Integer.Floor(other.Width), Kean.Math.Integer.Floor(other.Height));
        }
         public static SizeValue Floor(SizeValue other)
        {
            return new SizeValue(Kean.Math.Integer.Floor(other.Width), Kean.Math.Integer.Floor(other.Height));
        }
        public static SizeValue Ceiling(SizeValue other)
        {
            return new SizeValue(Kean.Math.Integer.Ceiling(other.Width), Kean.Math.Integer.Ceiling(other.Height));
        }
        public static SizeValue Maximum(SizeValue left, SizeValue right)
        {
            return new SizeValue(Kean.Math.Double.Maximum(left.Width, right.Width), Kean.Math.Double.Maximum(left.Height, right.Height));
        }
        public static SizeValue Minimum(SizeValue left, SizeValue right)
        {
            return new SizeValue(Kean.Math.Double.Minimum(left.Width, right.Width), Kean.Math.Double.Minimum(left.Height, right.Height));
        }
        #endregion
  }
}
