﻿// 
//  TransformValue.cs
//  
//  Author:
//       Anders Frisk <andersfrisk77@gmail.com>
//  
//  Copyright (c) 2011 Anders Frisk
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
    public struct TransformValue :
        Abstract.ITransform<double>,
		IEquatable<TransformValue>
    {
        double a;
        double b;
        double c;
        double d;
        double e;
        double f;
        public double A
        {
            get { return this.a; }
            set { this.a = value; }
        }
        public double B
        {
            get { return this.b; }
            set { this.b = value; }
        }
        public double C
        {
            get { return this.c; }
            set { this.c = value; }
        }
        public double D
        {
            get { return this.d; }
            set { this.d = value; }
        }
        public double E
        {
            get { return this.e; }
            set { this.e = value; }
        }
        public double F
        {
            get { return this.f; }
            set { this.f = value; }
        }
		public double this[int x, int y]
		{
			get
			{
				double result;
				switch (x)
				{
					case 0:
						switch (y)
						{
							case 0: result = this.A; break;
							case 1: result = this.B; break;
							case 2: result = 0; break;
							default: throw new System.Exception(); // TODO: create new exception
						}
						break;
					case 1:
						switch (y)
						{
							case 0: result = this.C; break;
							case 1: result = this.D; break;
							case 2: result = 0; break;
							default: throw new System.Exception(); // TODO: create new exception
						}
						break;
					case 2:
						switch (y)
						{
							case 0: result = this.E; break;
							case 1: result = this.F; break;
							case 2: result = 1; break;
							default: throw new System.Exception(); // TODO: create new exception
						}
						break;
					default: throw new System.Exception(); // TODO: create new exception
				}
				return result;
			}
		}
		#region Properties
		public SizeValue Translation { get { return new SizeValue(this.E, this.F); }}
        public double Scaling { get { return (this.ScalingX + this.ScalingY) / 2; } }
		public double ScalingX { get { return Kean.Math.Double.SquareRoot(Kean.Math.Double.Squared(this.A) + Kean.Math.Double.Squared(this.B)); } }
		public double ScalingY { get { return Kean.Math.Double.SquareRoot(Kean.Math.Double.Squared(this.C) + Kean.Math.Double.Squared(this.D)); } }
		public double Rotation { get { return Kean.Math.Double.ArcusTangensExtended(this.B, this.A); } }
      	public TransformValue Inverse
		{
			get
			{
				double determinant = this.A * this.D - this.B * this.C;
				return new TransformValue(
					this.D / determinant,
					-this.B / determinant,
					-this.C / determinant,
					this.A / determinant,
					(this.C * this.F - this.D * this.E) / determinant,
					-(this.A * this.F - this.B * this.E) / determinant
					);
			}
		}
		#endregion
		public TransformValue(double a, double b, double c, double d, double e, double f)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.e = e;
            this.f = f;
        }
		#region Absolute Manipulations
		public TransformValue SetTranslation(SizeValue translation)
		{
			return this.Translate(translation - this.Translation);
		}
		public TransformValue SetScaling(double scaling)
		{
			return this.Scale(scaling / this.Scaling);
		}
		public TransformValue SetXScaling(double scaling)
		{
			return this.Scale(scaling / this.ScalingX, 1);
		}
		public TransformValue SetYScaling(double scaling)
		{
			return this.Scale(1, scaling / this.ScalingY);
		}
		public TransformValue SetRotation(double rotation)
		{
			return this.Rotate(rotation - this.Rotation);
		}
		#endregion
		#region Relative Manipulations
		public TransformValue Translate(double delta)
        {
            return this.Translate(delta, delta);
        }
        public TransformValue Translate(Abstract.IVector<double> delta)
        {
            return this.Translate(delta.X, delta.Y);
        }
        public TransformValue Translate(double xDelta, double yDelta)
        {
            return TransformValue.CreateTranslation(xDelta, yDelta) * this;
        }
		public TransformValue Scale(double factor)
        {
            return this.Scale(factor, factor);
        }
		public TransformValue Scale(Abstract.IVector<double> factor)
        {
            return this.Scale(factor.X, factor.Y);
        }
		public TransformValue Scale(double xFactor, double yFactor)
        {
			return TransformValue.CreateScaling(xFactor, yFactor) * this;
        }
		public TransformValue Rotate(double angle)
        {
            return TransformValue.CreateRotation(angle) * this;
        }
		public TransformValue SkewX(double angle)
        {
            return TransformValue.CreateSkewingX(angle) * this;
        }
		public TransformValue SkewY(double angle)
        {
            return TransformValue.CreateSkewingY(angle) * this;
        }
        public TransformValue ReflectX()
        {
			return TransformValue.CreateReflectionX() * this;
        }
        public TransformValue ReflectY()
        {
			return TransformValue.CreateReflectionY() * this;
        }
        #endregion
		#region Static Creators
		public static TransformValue Identity { get {return new TransformValue(1,0,0,1,0,0); } }
		public static TransformValue CreateTranslation(double delta)
		{
			return TransformValue.CreateTranslation(delta, delta);
		}
		public static TransformValue CreateTranslation(Abstract.IVector<double> delta)
		{
			return TransformValue.CreateTranslation(delta.X, delta.Y);
		}
		public static TransformValue CreateTranslation(double xDelta, double yDelta)
		{
			return new TransformValue(1,0,0,1, xDelta, yDelta);
		}
		public static TransformValue CreateScaling(double factor)
		{
			return TransformValue.CreateScaling(factor, factor);
		}
		public static TransformValue CreateScaling(double xFactor, double yFactor)
		{
			return new TransformValue(xFactor, 0, 0, yFactor, 0, 0);
		}
		public static TransformValue CreateRotation(double angle)
		{
			return new TransformValue(Kean.Math.Double.Cosinus(angle), Kean.Math.Double.Sinus(angle), -Kean.Math.Double.Sinus(angle), Kean.Math.Double.Cosinus(angle), 0, 0);
		}
		public static TransformValue CreateRotation(double angle, Abstract.IPoint<double> pivot)
		{
			double one = 1;
			double sine = Kean.Math.Double.Sinus(angle);
			double cosine = Kean.Math.Double.Cosinus(angle);
			return new TransformValue(cosine, sine, -sine, cosine, (one - cosine) * pivot.X + sine * pivot.Y, -sine * pivot.X + (one - cosine) * pivot.Y);
		}
		public static TransformValue CreateSkewingX(double angle)
		{
			return new TransformValue(1, 0, Kean.Math.Double.Tangens(angle), 1, 0, 0);
		}
		public static TransformValue CreateSkewingY(double angle)
		{
			return new TransformValue(1, Kean.Math.Double.Tangens(angle), 0, 1, 0, 0);
		}
		public static TransformValue CreateReflectionX()
		{
			return new TransformValue(-1, 0, 0, 1, 0, 0);
		}
		public static TransformValue CreateReflectionY()
		{
			return new TransformValue(1, 0, 0, -1, 0, 0);
		}
		#endregion
		#region Arithmetic Operators
		public static TransformValue operator *(TransformValue left, Abstract.ITransform<double> right)
		{
			return new TransformValue(
				left.A * right.A + left.C * right.B,
				left.B * right.A + left.D * right.B,
				left.A * right.C + left.C * right.D,
				left.B * right.C + left.D * right.D,
				left.A * right.E + left.C * right.F + left.E,
				left.B * right.E + left.D * right.F + left.F);
		}
		#endregion
		#region ITransform<double> Members
		double Abstract.ITransform<double>.A { get { return this.A; } }
		double Abstract.ITransform<double>.B { get { return this.B; } }
		double Abstract.ITransform<double>.C { get { return this.C; } }
		double Abstract.ITransform<double>.D { get { return this.D; } }
		double Abstract.ITransform<double>.E { get { return this.E; } }
		double Abstract.ITransform<double>.F { get { return this.F; } }
		#endregion
        #region Comparison Operators
        /// <summary>
        /// Defines equality.
        /// </summary>
        /// <param name="Left">Point Left of operator.</param>
        /// <param name="Right">Point Right of operator.</param>
        /// <returns>True if <paramref name="Left"/> equals <paramref name="Right"/> else false.</returns>
        public static bool operator ==(TransformValue left, TransformValue right)
        {
            return left.A == right.A && left.B == right.B && left.C == right.C && left.D == right.D && left.E == right.E && left.F == right.F;
        }
        /// <summary>
        /// Defines inequality.
        /// </summary>
        /// <param name="Left">Point Left of operator.</param>
        /// <param name="Right">Point Right of operator.</param>
        /// <returns>False if <paramref name="Left"/> equals <paramref name="Right"/> else true.</returns>
        public static bool operator !=(TransformValue left, TransformValue right)
        {
            return !(left == right);
        }
        #endregion
		#region IEquatable<TransformValue> Members
		public bool Equals(TransformValue other)
		{
			return this == other;
		}
		#endregion
        #region Casts
        public static implicit operator TransformValue(Single.TransformValue value)
        {
            return new TransformValue(value.A, value.B, value.C, value.D, value.E, value.F);
        }
        public static implicit operator TransformValue(Integer.TransformValue value)
        {
            return new TransformValue(value.A, value.B, value.C, value.D, value.E, value.F);
        }
        public static explicit operator Single.TransformValue(TransformValue value)
        {
            return new Single.TransformValue((Kean.Math.Single)(value.A), (Kean.Math.Single)(value.B), (Kean.Math.Single)(value.C), (Kean.Math.Single)(value.D), (Kean.Math.Single)(value.E), (Kean.Math.Single)(value.F));
        }
        public static explicit operator Integer.TransformValue(TransformValue value)
        {
            return new Integer.TransformValue((Kean.Math.Integer)(value.A), (Kean.Math.Integer)(value.B), (Kean.Math.Integer)(value.C), (Kean.Math.Integer)(value.D), (Kean.Math.Integer)(value.E), (Kean.Math.Integer)(value.F));
        }
        public static implicit operator string(TransformValue value)
        {
            return value.NotNull() ? value.ToString() : null;
        }
        public static implicit operator TransformValue(string value)
        {
            TransformValue result = new TransformValue();
            if (value.NotEmpty())
            {
                try
                {
                    string[] values = value.Split(new char[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length == 6)
                        result = new TransformValue(Kean.Math.Double.Parse(values[0]), Kean.Math.Double.Parse(values[1]), Kean.Math.Double.Parse(values[2]), Kean.Math.Double.Parse(values[3]), Kean.Math.Double.Parse(values[4]), Kean.Math.Double.Parse(values[5]));
                }
                catch
                {
                }
            }
            return result;
        }
		public static explicit operator double[,](TransformValue value)
		{
			double[,] result = new double[3, 3];
			for (int x = 0; x < 3; x++)
				for (int y = 0; y < 3; y++)
					result[x, y] = value[x, y];
			return result;
		}
        #endregion
        #region Object Overrides
		/// <summary>
		/// Return true if this object and <paramref name="other">other</paramref> are equal.
		/// </summary>
		/// <param name="other">Object to compare with</param>
		/// <returns>True if this object and <paramref name="other">other</paramref> are equal else false.</returns>
		public override bool Equals(object other)
		{
			return (other is TransformValue) && this.Equals((TransformValue)other);
		}
		/// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>Hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return (33 * (33 * (33 * (33 * this.A.GetHashCode() ^ this.B.GetHashCode()) ^ this.C.GetHashCode()) ^ this.D.GetHashCode()) ^ this.E.GetHashCode()) ^ this.F.GetHashCode();
        }
        public override string ToString()
        {
            return
                Kean.Math.Double.ToString(this.A) + ", " +
                Kean.Math.Double.ToString(this.B) + ", " +
                Kean.Math.Double.ToString(this.C) + ", " +
                Kean.Math.Double.ToString(this.D) + ", " +
                Kean.Math.Double.ToString(this.E) + ", " +
                Kean.Math.Double.ToString(this.F); 
        }
        public string ToMatlabString()
        {
            return string.Format("{0}, {2}, {4}; {1}, {3}, {5}; 0, 0, 1", Kean.Math.Double.ToString(this.A), Kean.Math.Double.ToString(this.B), Kean.Math.Double.ToString(this.C), Kean.Math.Double.ToString(this.D), Kean.Math.Double.ToString(this.E), Kean.Math.Double.ToString(this.F));
        }
		#endregion
    }
}