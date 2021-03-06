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
using Kean.Extension;
using Kean.Math.Exception;

namespace Kean.Math.Geometry2D.Integer
{
	public struct Transform :
		IEquatable<Transform>
	{
		int a;
		int b;
		int c;
		int d;
		int e;
		int f;
		public int A
		{
			get { return this.a; }
			set { this.a = value; }
		}
		public int B
		{
			get { return this.b; }
			set { this.b = value; }
		}
		public int C
		{
			get { return this.c; }
			set { this.c = value; }
		}
		public int D
		{
			get { return this.d; }
			set { this.d = value; }
		}
		public int E
		{
			get { return this.e; }
			set { this.e = value; }
		}
		public int F
		{
			get { return this.f; }
			set { this.f = value; }
		}
		public int this[int x, int y]
		{
			get
			{
				int result;
				switch (x)
				{
					case 0:
						switch (y)
						{
							case 0: result = this.A; break;
							case 1: result = this.B; break;
							case 2: result = 0; break;
							default: throw new IndexOutOfRange();
						}
						break;
					case 1:
						switch (y)
						{
							case 0: result = this.C; break;
							case 1: result = this.D; break;
							case 2: result = 0; break;
							default: throw new IndexOutOfRange();
						}
						break;
					case 2:
						switch (y)
						{
							case 0: result = this.E; break;
							case 1: result = this.F; break;
							case 2: result = 1; break;
							default: throw new IndexOutOfRange();
						}
						break;
					default: throw new IndexOutOfRange();
				}
				return result;
			}
		}
		#region Properties
		public Size Translation { get { return new Size(this.E, this.F); }}
		public int Scaling { get { return (this.ScalingX + this.ScalingY) / 2; } }
		public int ScalingX { get { return Kean.Math.Integer.SquareRoot(Kean.Math.Integer.Squared(this.A) + Kean.Math.Integer.Squared(this.B)); } }
		public int ScalingY { get { return Kean.Math.Integer.SquareRoot(Kean.Math.Integer.Squared(this.C) + Kean.Math.Integer.Squared(this.D)); } }
		public int Rotation { get { return Kean.Math.Integer.ArcusTangensExtended(this.B, this.A); } }
		public Transform Inverse
		{
			get
			{
				int determinant = this.A * this.D - this.B * this.C;
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
		public Transform(int a, int b, int c, int d, int e, int f)
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
		public Transform SetScaling(int scaling)
		{
			return this.Scale(scaling / this.Scaling);
		}
		public Transform SetXScaling(int scaling)
		{
			return this.Scale(scaling / this.ScalingX, 1);
		}
		public Transform SetYScaling(int scaling)
		{
			return this.Scale(1, scaling / this.ScalingY);
		}
		public Transform SetRotation(int rotation)
		{
			return this.Rotate(rotation - this.Rotation);
		}
		#endregion
		#region Relative Manipulations
		public Transform Translate(int delta)
		{
			return this.Translate(delta, delta);
		}
		public Transform Translate(Point delta)
		{
			return this.Translate(delta.X, delta.Y);
		}
		public Transform Translate(Size delta)
		{
			return this.Translate(delta.Width, delta.Height);
		}
		public Transform Translate(int xDelta, int yDelta)
		{
			return Transform.CreateTranslation(xDelta, yDelta) * this;
		}
		public Transform Scale(int factor)
		{
			return this.Scale(factor, factor);
		}
		public Transform Scale(Size factor)
		{
			return this.Scale(factor.Width, factor.Height);
		}
		public Transform Scale(int xFactor, int yFactor)
		{
			return Transform.CreateScaling(xFactor, yFactor) * this;
		}
		public Transform Rotate(int angle)
		{
			return Transform.CreateRotation(angle) * this;
		}
		public Transform SkewX(int angle)
		{
			return Transform.CreateSkewingX(angle) * this;
		}
		public Transform SkewY(int angle)
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
		public static Transform CreateTranslation(int delta)
		{
			return Transform.CreateTranslation(delta, delta);
		}
		public static Transform CreateTranslation(Size delta)
		{
			return Transform.CreateTranslation(delta.Width, delta.Height);
		}
		public static Transform CreateTranslation(Point delta)
		{
			return Transform.CreateTranslation(delta.X, delta.Y);
		}
		public static Transform CreateTranslation(int xDelta, int yDelta)
		{
			return new Transform(1,0,0,1, xDelta, yDelta);
		}
		public static Transform CreateScaling(int factor)
		{
			return Transform.CreateScaling(factor, factor);
		}
		public static Transform CreateScaling(int xFactor, int yFactor)
		{
			return new Transform(xFactor, 0, 0, yFactor, 0, 0);
		}
		public static Transform CreateRotation(int angle)
		{
			return new Transform(Kean.Math.Integer.Cosinus(angle), Kean.Math.Integer.Sinus(angle), -Kean.Math.Integer.Sinus(angle), Kean.Math.Integer.Cosinus(angle), 0, 0);
		}
		public static Transform CreateRotation(int angle, Point pivot)
		{
			int one = 1;
			int sine = Kean.Math.Integer.Sinus(angle);
			int cosine = Kean.Math.Integer.Cosinus(angle);
			return new Transform(cosine, sine, -sine, cosine, (one - cosine) * pivot.X + sine * pivot.Y, -sine * pivot.X + (one - cosine) * pivot.Y);
		}
		public static Transform CreateSkewingX(int angle)
		{
			return new Transform(1, 0, Kean.Math.Integer.Tangens(angle), 1, 0, 0);
		}
		public static Transform CreateSkewingY(int angle)
		{
			return new Transform(1, Kean.Math.Integer.Tangens(angle), 0, 1, 0, 0);
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
		public static implicit operator string(Transform value)
		{
			return value.NotNull() ? value.ToString() : null;
		}
		public static explicit operator Transform(string value)
		{
			Transform result = new Transform();
			if (value.NotEmpty())
			{
				try
				{
					string[] values = value.Split(new char[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);
					if (values.Length == 6)
						result = new Transform(Kean.Math.Integer.Parse(values[0]), Kean.Math.Integer.Parse(values[1]), Kean.Math.Integer.Parse(values[2]), Kean.Math.Integer.Parse(values[3]), Kean.Math.Integer.Parse(values[4]), Kean.Math.Integer.Parse(values[5]));
				}
				catch
				{
				}
			}
			return result;
		}
		public static explicit operator int[,](Transform value)
		{
			int[,] result = new int[3, 3];
			for (int x = 0; x < 3; x++)
				for (int y = 0; y < 3; y++)
					result[x, y] = value[x, y];
			return result;
		}
		public static implicit operator byte[](Transform value)
		{
			int size = sizeof(int);
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
			return this.ToString("{0}, {1}, {2}, {3}, {4}, {5}");
		}
		public string ToString(string format)
		{
			return String.Format(format, 
			Kean.Math.Integer.ToString(this.A), 
			Kean.Math.Integer.ToString(this.B), 
			Kean.Math.Integer.ToString(this.C), 
			Kean.Math.Integer.ToString(this.D), 
			Kean.Math.Integer.ToString(this.E), 
			Kean.Math.Integer.ToString(this.F));
		}
		public string ToMatlabString()
		{
			return this.ToString("{0}, {2}, {4}; {1}, {3}, {5}; 0, 0, 1");
		}
		#endregion
	}
}
