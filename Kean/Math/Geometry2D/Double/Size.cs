// 
//  Size.cs (generated by template)
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
using Kean.Extension;

namespace Kean.Math.Geometry2D.Double
{
    public struct Size :
        IEquatable<Size>
    {
        public double Width;
        public double Height;
        public double Area { get { return this.Width * this.Height; } }
        public double Length { get { return this.Norm; } }
        public bool Empty { get { return this.Width == 0 || this.Height == 0; } }
        public double Norm { get { return Kean.Math.Double.SquareRoot(this.ScalarProduct(this)); } }
        public double Azimuth { get { return Kean.Math.Double.ArcusTangensExtended(this.Height, this.Width); } }
        #region Static Constants
        public static Size BasisX { get { return new Size(1, 0); } }
        public static Size BasisY { get { return new Size(0, 1); } }
        #endregion
        public Size(double width, double height)
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
        public double Angle(Size other)
        {
            double result = 0;
            result = this.ScalarProduct(other) / (this.Norm * other.Norm);
            double sign = this.Width * other.Height - this.Height * other.Width;
            result = Kean.Math.Double.ArcusCosinus(Kean.Math.Double.Clamp(result, -1, 1));
            result *= sign < 0 ? -1 : 1;
            return result;
        }
        public double ScalarProduct(Size other)
        {
            return this.Width * other.Width + this.Height * other.Height;
        }
        public double Distance(Size other)
        {
            return (this - other).Norm;
        }
        #region Utility functions
        public Size Swap()
        {
            return new Size(this.Height, this.Width);
        }
        public Size Round()
        {
            return new Size(Kean.Math.Double.Round(this.Width), Kean.Math.Double.Round(this.Height));
        }
        public Size Ceiling()
        {
            return new Size(Kean.Math.Double.Ceiling(this.Width), Kean.Math.Double.Ceiling(this.Height));
        }
        public Size Floor()
        {
            return new Size(Kean.Math.Double.Floor(this.Width), Kean.Math.Double.Floor(this.Height));
        }
        public Size Minimum(Size floor)
        {
            return new Size(Kean.Math.Double.Minimum(this.Width, floor.Width), Kean.Math.Double.Minimum(this.Height, floor.Height));
        }
        public Size Maximum(Size ceiling)
        {
            return new Size(Kean.Math.Double.Maximum(this.Width, ceiling.Width), Kean.Math.Double.Maximum(this.Height, ceiling.Height));
        }
        public Size Clamp(Size floor, Size ceiling)
        {
            return new Size(Kean.Math.Double.Clamp(this.Width, floor.Width, ceiling.Width), Kean.Math.Double.Clamp(this.Height, floor.Height, ceiling.Height));
        }
        #endregion
        #region Arithmetic Vector - Vector Operators
        public static Size operator +(Size left, Size right)
        {
            return new Size(left.Width + right.Width, left.Height + right.Height);
        }
        public static Size operator +(Size left, Point right)
        {
            return new Size(left.Width + right.X, left.Height + right.Y);
        }
        public static Size operator -(Size left, Size right)
        {
            return new Size(left.Width - right.Width, left.Height - right.Height);
        }
        public static Size operator -(Size left, Point right)
        {
            return new Size(left.Width - right.X, left.Height - right.Y);
        }
        public static Size operator -(Size vector)
        {
            return new Size(-vector.Width, -vector.Height);
        }
        public static Size operator *(Size left, Size right)
        {
            return new Size(left.Width * right.Width, left.Height * right.Height);
        }
        public static Size operator *(Size left, Point right)
        {
            return new Size(left.Width * right.X, left.Height * right.Y);
        }
        public static Size operator /(Size left, Size right)
        {
            return new Size(left.Width / right.Width, left.Height / right.Height);
        }
        public static Size operator /(Size left, Point right)
        {
            return new Size(left.Width / right.X, left.Height / right.Y);
        }
        #endregion
        #region Arithmetic Vector and Scalar
        public static Size operator *(Size left, double right)
        {
            return new Size(left.Width * right, left.Height * right);
        }
        public static Size operator *(double left, Size right)
        {
            return right * left;
        }

        public static Size operator *(Size left, float right)
        {
            return new Size(left.Width * right, left.Height * right);
        } 
        public static Size operator *(Size left, int right)
        {
            return new Size(left.Width * right, left.Height * right);
        }
        public static Size operator *(float left, Size right)
        {
           return right * left;
        } 
        public static Size operator *(int left, Size right)
        {
            return right * left;
        }
        public static Size operator /(Size left, float right)
        {
            return new Size(left.Width / right, left.Height / right);
        } 
        public static Size operator /(Size left, int right)
        {
            return new Size(left.Width / right, left.Height / right);
        }

        public static Size operator /(Size left, double right)
        {
            return new Size(left.Width / right, left.Height / right);
        }
        #endregion
        #region Arithmetic Transform and Vector
        public static Size operator *(Transform left, Size right)
        {
            return new Size(left.A * right.Width + left.C * right.Height, left.B * right.Width + left.D * right.Height);
        }
        #endregion
        #region Comparison Operators
        /// <summary>
        /// Defines equality.
        /// </summary>
        /// <param name="Left">Point Left of operator.</param>
        /// <param name="Right">Point Right of operator.</param>
        /// <returns>True if <paramref name="Left"/> equals <paramref name="Right"/> else false.</returns>
        public static bool operator ==(Size left, Size right)
        {
            return left.Width == right.Width && left.Height == right.Height;
        }
        /// <summary>
        /// Defines inequality.
        /// </summary>
        /// <param name="Left">Point Left of operator.</param>
        /// <param name="Right">Point Right of operator.</param>
        /// <returns>False if <paramref name="Left"/> equals <paramref name="Right"/> else true.</returns>
        public static bool operator !=(Size left, Size right)
        {
            return !(left == right);
        }
        public static bool operator <(Size left, Size right)
        {
            return left.Width < right.Width && left.Height < right.Height;
        }
        public static bool operator >(Size left, Size right)
        {
            return left.Width > right.Width && left.Height > right.Height;
        }
        public static bool operator <=(Size left, Size right)
        {
            return left.Width <= right.Width && left.Height <= right.Height;
        }
        public static bool operator >=(Size left, Size right)
        {
            return left.Width >= right.Width && left.Height >= right.Height;
        }
        #endregion
        #region IEquatable<Size> Members
        public bool Equals(Size other)
        {
            return this == other;
        }
        #endregion
        #region Object Overrides
        public override bool Equals(object other)
        {
            return other is Size && this.Equals((Size)other);
        }
        public override int GetHashCode()
        {
            return 33 * this.Width.GetHashCode() ^ this.Height.GetHashCode();
        }
        public override string ToString()
        {
			return this.ToString("{0}, {1}");
		}
		public string ToString(string format)
		{
			return String.Format(format, Kean.Math.Double.ToString(this.Width), Kean.Math.Double.ToString(this.Height));
		}
        #endregion
        #region Static Creators
        public static Size Polar(double radius, double azimuth)
        {
            return new Size(radius * Kean.Math.Double.Cosinus(azimuth), radius * Kean.Math.Double.Sinus(azimuth));
        }
        #endregion
        #region Casts
        public static implicit operator double[](Size value)
        {
            return new double[] { value.Width, value.Height };
        }
        public static explicit operator Size(double[] value)
        {
            return new Size(value[0], value[1]);
        }
        public static implicit operator string(Size value)
        {
            return value.ToString();
        }
        public static explicit operator Size(string value)
        {
            Size result = new Size();
            if (value.NotEmpty())
            {
                try
                {
                    string[] values = value.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length == 2)
                        result = new Size(Kean.Math.Double.Parse(values[0]), Kean.Math.Double.Parse(values[1]));
                }
                catch
                {
                }
            }
            return result;
        }
        public static implicit operator Size(Single.Size value)
        {
            return new Size(value.Width, value.Height);
        }
        public static implicit operator Size(Integer.Size value)
        {
            return new Size(value.Width, value.Height);
        }
        public static explicit operator Single.Size(Size value)
        {
            return new Single.Size(Kean.Math.Single.Convert(value.Width), Kean.Math.Single.Convert(value.Height));
        }
        public static explicit operator Integer.Size(Size value)
        {
            return new Integer.Size(Kean.Math.Integer.Convert(value.Width), Kean.Math.Integer.Convert(value.Height));
        }
        #endregion
        #region Static Operators
        public static Size Ceiling(Geometry2D.Single.Size other)
        {
            return new Size(Kean.Math.Integer.Ceiling(other.Width), Kean.Math.Integer.Ceiling(other.Height));
        }
        public static Size Floor(Geometry2D.Single.Size other)
        {
            return new Size(Kean.Math.Integer.Floor(other.Width), Kean.Math.Integer.Floor(other.Height));
        }
         public static Size Floor(Size other)
        {
            return new Size(Kean.Math.Integer.Floor(other.Width), Kean.Math.Integer.Floor(other.Height));
        }
        public static Size Ceiling(Size other)
        {
            return new Size(Kean.Math.Integer.Ceiling(other.Width), Kean.Math.Integer.Ceiling(other.Height));
        }
        public static Size Maximum(Size left, Size right)
        {
            return new Size(Kean.Math.Double.Maximum(left.Width, right.Width), Kean.Math.Double.Maximum(left.Height, right.Height));
        }
        public static Size Minimum(Size left, Size right)
        {
            return new Size(Kean.Math.Double.Minimum(left.Width, right.Width), Kean.Math.Double.Minimum(left.Height, right.Height));
        }
        #endregion
  }
}
