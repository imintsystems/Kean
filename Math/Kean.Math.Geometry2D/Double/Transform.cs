// 
//  Transform.cs (generated by template)
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
    public struct Transform :
		IEquatable<Transform>
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
		public Size Translation { get { return new Size(this.E, this.F); }}
        public double Scaling { get { return (this.ScalingX + this.ScalingY) / 2; } }
		public double ScalingX { get { return Kean.Math.Double.SquareRoot(Kean.Math.Double.Squared(this.A) + Kean.Math.Double.Squared(this.B)); } }
		public double ScalingY { get { return Kean.Math.Double.SquareRoot(Kean.Math.Double.Squared(this.C) + Kean.Math.Double.Squared(this.D)); } }
		public double Rotation { get { return Kean.Math.Double.ArcusTangensExtended(this.B, this.A); } }
      	public Transform Inverse
		{
			get
			{
				double determinant = this.A * this.D - this.B * this.C;
				return new Transform(
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
		public Transform(double a, double b, double c, double d, double e, double f)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.e = e;
            this.f = f;
        }
		#region Absolute Manipulations
		public Transform SetTranslation(Size translation)
		{
			return this.Translate(translation - this.Translation);
		}
		public Transform SetScaling(double scaling)
		{
			return this.Scale(scaling / this.Scaling);
		}
		public Transform SetXScaling(double scaling)
		{
			return this.Scale(scaling / this.ScalingX, 1);
		}
		public Transform SetYScaling(double scaling)
		{
			return this.Scale(1, scaling / this.ScalingY);
		}
		public Transform SetRotation(double rotation)
		{
			return this.Rotate(rotation - this.Rotation);
		}
		#endregion
		#region Relative Manipulations
		public Transform Translate(double delta)
        {
            return this.Translate(delta, delta);
        }
        public Transform Translate(Size delta)
        {
            return this.Translate(delta.Width, delta.Height);
        }
        public Transform Translate(double xDelta, double yDelta)
        {
            return Transform.CreateTranslation(xDelta, yDelta) * this;
        }
		public Transform Scale(double factor)
        {
            return this.Scale(factor, factor);
        }
		public Transform Scale(Size factor)
        {
            return this.Scale(factor.Width, factor.Height);
        }
		public Transform Scale(double xFactor, double yFactor)
        {
			return Transform.CreateScaling(xFactor, yFactor) * this;
        }
		public Transform Rotate(double angle)
        {
            return Transform.CreateRotation(angle) * this;
        }
		public Transform SkewX(double angle)
        {
            return Transform.CreateSkewingX(angle) * this;
        }
		public Transform SkewY(double angle)
        {
            return Transform.CreateSkewingY(angle) * this;
        }
        public Transform ReflectX()
        {
			return Transform.CreateReflectionX() * this;
        }
        public Transform ReflectY()
        {
			return Transform.CreateReflectionY() * this;
        }
        #endregion
		#region Static Creators
		public static Transform Identity { get {return new Transform(1,0,0,1,0,0); } }
		public static Transform CreateTranslation(double delta)
		{
			return Transform.CreateTranslation(delta, delta);
		}
		public static Transform CreateTranslation(Size delta)
		{
			return Transform.CreateTranslation(delta.Width, delta.Height);
		}
		public static Transform CreateTranslation(double xDelta, double yDelta)
		{
			return new Transform(1,0,0,1, xDelta, yDelta);
		}
		public static Transform CreateScaling(double factor)
		{
			return Transform.CreateScaling(factor, factor);
		}
		public static Transform CreateScaling(double xFactor, double yFactor)
		{
			return new Transform(xFactor, 0, 0, yFactor, 0, 0);
		}
		public static Transform CreateRotation(double angle)
		{
			return new Transform(Kean.Math.Double.Cosinus(angle), Kean.Math.Double.Sinus(angle), -Kean.Math.Double.Sinus(angle), Kean.Math.Double.Cosinus(angle), 0, 0);
		}
		public static Transform CreateRotation(double angle, Point pivot)
		{
			double one = 1;
			double sine = Kean.Math.Double.Sinus(angle);
			double cosine = Kean.Math.Double.Cosinus(angle);
			return new Transform(cosine, sine, -sine, cosine, (one - cosine) * pivot.X + sine * pivot.Y, -sine * pivot.X + (one - cosine) * pivot.Y);
		}
		public static Transform CreateSkewingX(double angle)
		{
			return new Transform(1, 0, Kean.Math.Double.Tangens(angle), 1, 0, 0);
		}
		public static Transform CreateSkewingY(double angle)
		{
			return new Transform(1, Kean.Math.Double.Tangens(angle), 0, 1, 0, 0);
		}
		public static Transform CreateReflectionX()
		{
			return new Transform(-1, 0, 0, 1, 0, 0);
		}
		public static Transform CreateReflectionY()
		{
			return new Transform(1, 0, 0, -1, 0, 0);
		}
		#endregion
		#region Arithmetic Operators
		public static Transform operator *(Transform left, Transform right)
		{
			return new Transform(
				left.A * right.A + left.C * right.B,
				left.B * right.A + left.D * right.B,
				left.A * right.C + left.C * right.D,
				left.B * right.C + left.D * right.D,
				left.A * right.E + left.C * right.F + left.E,
				left.B * right.E + left.D * right.F + left.F);
		}
		#endregion
        #region Comparison Operators
        /// <summary>
        /// Defines equality.
        /// </summary>
        /// <param name="Left">Point Left of operator.</param>
        /// <param name="Right">Point Right of operator.</param>
        /// <returns>True if <paramref name="Left"/> equals <paramref name="Right"/> else false.</returns>
        public static bool operator ==(Transform left, Transform right)
        {
            return left.A == right.A && left.B == right.B && left.C == right.C && left.D == right.D && left.E == right.E && left.F == right.F;
        }
        /// <summary>
        /// Defines inequality.
        /// </summary>
        /// <param name="Left">Point Left of operator.</param>
        /// <param name="Right">Point Right of operator.</param>
        /// <returns>False if <paramref name="Left"/> equals <paramref name="Right"/> else true.</returns>
        public static bool operator !=(Transform left, Transform right)
        {
            return !(left == right);
        }
        #endregion
		#region IEquatable<Transform> Members
		public bool Equals(Transform other)
		{
			return this == other;
		}
		#endregion
        #region Casts
        public static implicit operator Transform(Single.Transform value)
        {
            return new Transform(value.A, value.B, value.C, value.D, value.E, value.F);
        }
        public static implicit operator Transform(Integer.Transform value)
        {
            return new Transform(value.A, value.B, value.C, value.D, value.E, value.F);
        }
        public static explicit operator Single.Transform(Transform value)
        {
            return new Single.Transform((Kean.Math.Single)(value.A), (Kean.Math.Single)(value.B), (Kean.Math.Single)(value.C), (Kean.Math.Single)(value.D), (Kean.Math.Single)(value.E), (Kean.Math.Single)(value.F));
        }
        public static explicit operator Integer.Transform(Transform value)
        {
            return new Integer.Transform((Kean.Math.Integer)(value.A), (Kean.Math.Integer)(value.B), (Kean.Math.Integer)(value.C), (Kean.Math.Integer)(value.D), (Kean.Math.Integer)(value.E), (Kean.Math.Integer)(value.F));
        }
        public static implicit operator string(Transform value)
        {
            return value.NotNull() ? value.ToString() : null;
        }
        public static implicit operator Transform(string value)
        {
            Transform result = new Transform();
            if (value.NotEmpty())
            {
                try
                {
                    string[] values = value.Split(new char[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length == 6)
                        result = new Transform(Kean.Math.Double.Parse(values[0]), Kean.Math.Double.Parse(values[1]), Kean.Math.Double.Parse(values[2]), Kean.Math.Double.Parse(values[3]), Kean.Math.Double.Parse(values[4]), Kean.Math.Double.Parse(values[5]));
                }
                catch
                {
                }
            }
            return result;
        }
		public static explicit operator double[,](Transform value)
		{
			double[,] result = new double[3, 3];
			for (int x = 0; x < 3; x++)
				for (int y = 0; y < 3; y++)
					result[x, y] = value[x, y];
			return result;
		}
		public static explicit operator byte[](Transform value)
		{
			int size = sizeof(double);
			byte[] result = new byte[6 * size];
			for (int x = 0; x < 3; x++)
				for (int y = 0; y < 2; y++)
					Array.Copy(value[x, y].AsBinary(), 0, result, (x + y * 3) * size, size);
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
			return (other is Transform) && this.Equals((Transform)other);
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
