
//
// Transform.cs (generated by template)
// 
// Author:
//      Niklas Borwell <niklas.borwell@imint.se>
//		Anders Frisk <andersfrisk77@gmail.com>
// 
// Copyright (c) 2014 Simon Mika
//               2011 Anders Frisk
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.

using System;
using Kean.Extension;

namespace Kean.Math.Geometry2D.Integer
{

	// The 2D transform is a 3x3 homogeneous coordinate matrix.
	// The element order is:
	// A D G
	// B E H
	// C F I


	public struct Transform :
		IEquatable<Transform>
	{
		int a;
		int b;
		int c;
		int d;
		int e;
        int f;
        int g;
		int h;
        int i;
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
		public int G
		{
			get { return this.g; }
			set { this.g = value; }
		}
		public int H
		{
			get { return this.h; }
			set { this.h = value; }
		}
		public int I
		{
			get { return this.i; }
			set { this.i = value; }
		}
        /// <summary>
        /// Returns the element of the transform at the specified row and column.
        /// </summary>
        /// <param name="x">Column.</param>
        /// <param name="y">Row.></param>
        /// <returns>Transform element.</returns>
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
							case 2: result = this.C; break;
							default: throw new Exception.IndexOutOfRange();
						}
						break;
					case 1:
						switch (y)
						{
							case 0: result = this.D; break;
							case 1: result = this.E; break;
							case 2: result = this.F; break;
							default: throw new Exception.IndexOutOfRange();
						}
						break;
					case 2:
						switch (y)
						{
							case 0: result = this.G; break;
							case 1: result = this.H; break;
							case 2: result = this.I; break;
							default: throw new Exception.IndexOutOfRange();
						}
						break;
					default: throw new Exception.IndexOutOfRange();
				}
				return result;
			}
		}
		#region Properties
        /// <summary>
        /// Returns the determinant of the instance.
        /// </summary>
        int Determinant { get { return this.A*this.E*this.I + this.D*this.H*this.C + this.G*this.B*this.F -  this.G*this.E*this.C - this.D*this.B*this.I -this.A*this.H*this.F ; } }
		/// <summary>
        /// Returns the translation of the instance.
		/// </summary>
        public Size Translation { get { return new Size(this.G, this.H); }}
		/// <summary>
        /// Returns the absolute value of the scaling of the instance.
		/// </summary>
        public int Scaling { get { return (this.ScalingX + this.ScalingY) / 2; } }
		/// <summary>
        /// Returns the X scaling of the instance.
		/// </summary>
        public int ScalingX { get { return Math.Integer.SquareRoot(Math.Integer.Squared(this.A) + Math.Integer.Squared(this.B)); } }
		/// <summary>
        /// Returns the Y scaling of the instance.
		/// </summary>
        public int ScalingY { get { return Math.Integer.SquareRoot(Math.Integer.Squared(this.D) + Math.Integer.Squared(this.E)); } }
	    /// <summary>
		/// Returns the Z rotation of the instance.
		/// </summary>
        public int Rotation { get { return Math.Integer.ArcusTangensExtended(this.B, this.A); } }
		/// <summary>
		/// Returns an inverse of the instance.
		/// </summary>
        public Transform Inverse
		{
			get
			{
				int determinant = this.Determinant;
				return new Transform(
					(this.e*this.i-this.h*this.f)/determinant,
					(this.h*this.c-this.b*this.i)/determinant,
					(this.b*this.f-this.e*this.c)/determinant,
					(this.g*this.f-this.d*this.i)/determinant,
					(this.a*this.i-this.g*this.c)/determinant,
					(this.c*this.d-this.a*this.f)/determinant,
                    (this.d*this.h-this.g*this.e)/determinant,
                    (this.g*this.b-this.a*this.h)/determinant,
                    (this.a*this.e-this.b*this.d)/determinant
					);
			}
		}
        /// <summary>
        /// Returns true if the transform is a projective transform.
        /// </summary>
        public bool IsProjective { get { return this.Determinant != 0; } }
        /// <summary>
        /// Returns true if the transform is an affine transform.
        /// </summary>
        public bool IsAffine { get { return this.C == 0 && this.F == 0 && this.I == 1; } }
        /// <summary>
        /// Returns true if the transform is a similarity transform.
        /// </summary>
        public bool IsSimilarity { get { return this == Transform.Create(this.Translation, this.Scaling, this.Rotation); } }
        /// <summary>
        /// Returns true if the transform is a Euclidian transform.
        /// </summary>
        public bool IsEuclidian { get { return this == Transform.Create(this.Translation, this.Rotation); } }
        /// <summary>
        /// Returns true if the transform is an identity transform.
        /// </summary>
        public bool IsIdentity { get { return this.A == 1 && this.B == 0 && this.C == 0 && this.D == 0 && this.E == 1 && this.F == 0 && this.G == 0 && this.H == 0 && this.I == 1; } }
		#endregion
        /// <summary>
        /// Constructor with all elements except the 3rd row in the transformation matrix.
        /// </summary>
        /// <param name="a">Element a.</param>
        /// <param name="b">Element b.</param>
        /// <param name="d">Element c.</param>
        /// <param name="e">Element d.</param>
        /// <param name="g">Element e.</param>
        /// <param name="h">Element f.</param>
		public Transform(int a, int b, int d, int e, int g, int h) :
            this(a, b, 0, d, e, 0, g, h, 1)
		{
        }
        /// <summary>
        /// Constructor with all elements in the transformation matrix.
        /// </summary>
        /// <param name="a">Element a.</param>
        /// <param name="b">Element b.</param>
        /// <param name="c">Element c.</param>
        /// <param name="d">Element d.</param>
        /// <param name="e">Element e.</param>
        /// <param name="f">Element f.</param>
        /// <param name="g">Element g.</param>
        /// <param name="h">Element h.</param>
        /// <param name="i">Element i.</param>
		public Transform(int a, int b, int c, int d, int e, int f, int g, int h, int i)
		{
			this.a = a;
			this.b = b;
			this.c = c;
			this.d = d;
			this.e = e;
			this.f = f;
			this.g = g;
			this.h = h;
			this.i = i;
		}
		#region Absolute Manipulations
		/// <summary>
        /// Sets the translation values of the instance.
		/// </summary>
		/// <param name="translation">Translation values.</param>
        /// <returns>Updated instance.</returns>
		public Transform SetTranslation(Size translation)
		{
			return this.Translate(translation - this.Translation);
		}
        /// <summary>
        /// Sets the X and Y scaling of the instance.
        /// </summary>
        /// <param name="scaling">Scaling value.</param>
        /// <returns>Updated instance.</returns>
		public Transform SetScaling(int scaling)
		{
			return this.Scale(scaling / this.Scaling);
		}
        /// <summary>
        /// Sets the X scaling of the instance.
        /// </summary>
        /// <param name="scaling">X scaling value.</param>
        /// <returns>Updated instance.</returns>
		public Transform SetXScaling(int scaling)
		{
			return this.Scale(scaling / this.ScalingX, 1);
		}
        /// <summary>
        /// Sets the Y scaling of the instance.
        /// </summary>
        /// <param name="scaling">Y scaling value.</param>
        /// <returns>Updated instance.</returns>
		public Transform SetYScaling(int scaling)
		{
			return this.Scale(1, scaling / this.ScalingY);
		}
        /// <summary>
        /// Sets the Z rotation of the instance.
        /// </summary>
        /// <param name="rotation">Z Rotation value.</param>
        /// <returns>Updated instance.</returns>
		public Transform SetRotation(int rotation)
		{
			return this.Rotate(rotation - this.Rotation);
		}
		#endregion
		#region Relative Manipulations
        /// <summary>
        /// Translates the instance with the same X and Y values.
        /// </summary>
        /// <param name="delta">Translation value.</param>
        /// <returns>Translated transform.</returns>
		public Transform Translate(int delta)
		{
			return this.Translate(delta, delta);
		}
        /// <summary>
        /// Translates the instance with independent X and Y values.
        /// </summary>
        /// <param name="delta">Translation values.</param>
        /// <returns>Translated transform.</returns>
		public Transform Translate(Point delta)
		{
			return this.Translate(delta.X, delta.Y);
		}
        /// <summary>
        /// Translates the instance with independent X and Y values.
        /// </summary>
        /// <param name="delta">Translation values.</param>
        /// <returns>Translated transform.</returns>

		public Transform Translate(Size delta)
		{
			return this.Translate(delta.Width, delta.Height);
		}
        /// <summary>
        /// Translates the instance with independent X and Y values.
        /// </summary>
        /// <param name="xDelta">X translation value.</param>
        /// <param name="yDelta">Y translation value.</param>
        /// <returns>Translated transform.</returns>
		public Transform Translate(int xDelta, int yDelta)
		{
			return Transform.CreateTranslation(xDelta, yDelta) * this;
		}
        /// <summary>
        /// Scales the instance with the same X and Y factors.
        /// </summary>
        /// <param name="factor">Scaling factor.</param>
        /// <returns>Scaled transform.</returns>
		public Transform Scale(int factor)
		{
			return this.Scale(factor, factor);
		}
        /// <summary>
        /// Scales the instance with independent X and Y factors.
        /// </summary>
        /// <param name="factor">Scaling factors.</param>
        /// <returns>Scaled transform</returns>
		public Transform Scale(Size factor)
		{
			return this.Scale(factor.Width, factor.Height);
		}
        /// <summary>
        /// Scales the instance with independent X and Y factors.
        /// </summary>
        /// <param name="xFactor">Scaling factor in X.</param>
        /// <param name="yFactor">Scaling factor in Y.</param>
        /// <returns>Scaled transform.</returns>
		public Transform Scale(int xFactor, int yFactor)
		{
			return Transform.CreateScaling(xFactor, yFactor) * this;
		}
        /// <summary>
        /// Rotates the instance in Z.
        /// </summary>
        /// <param name="angle">Z rotation angle.</param>
        /// <returns>Rotated transform.</returns>
		public Transform Rotate(int angle)
		{
			return Transform.CreateZRotation(angle) * this;
		}
        /// <summary>
        /// Skews the instance in X.
        /// </summary>
        /// <param name="angle">Angle to skew.</param>
        /// <returns>Skewed transform.</returns>
		public Transform SkewX(int angle)
		{
			return Transform.CreateSkewingX(angle) * this;
		}
        /// <summary>
        /// Skews the instance in Y.
        /// </summary>
        /// <param name="angle">Angle to skew.</param>
        /// <returns>Skewed transform.</returns>
		public Transform SkewY(int angle)
		{
			return Transform.CreateSkewingY(angle) * this;
		}
        /// <summary>
        /// Reflects the instance in X.
        /// </summary>
        /// <returns>X reflected transform.</returns>
		public Transform ReflectX()
		{
			return Transform.CreateReflectionX() * this;
		}
        /// <summary>
        /// Reflects the instance in Y.
        /// </summary>
        /// <returns>Y reflected instance.</returns>
		public Transform ReflectY()
		{
			return Transform.CreateReflectionY() * this;
		}
		#endregion
		#region Static Creators
        /// <summary>
        /// Returns an identity transform.
        /// </summary>
		public static Transform Identity { get { return new Transform(1, 0, 0, 1, 0, 0); } }

        /// <summary>
        /// Returns a translation, scale and Z rotation transform.
        /// </summary>
        /// <param name="translation">Translation values.</param>
        /// <param name="scale">Scale factor.</param>
        /// <param name="rotation">Z rotation value.</param>
        /// <returns>Transform.</returns>
		public static Transform Create(Size translation, int scale, int rotation)
		{
			return new Transform (
				Math.Integer.Cosine(rotation) * scale,
				Math.Integer.Sine(rotation) * scale,
				-Math.Integer.Sine(rotation) * scale,
				Math.Integer.Cosine(rotation) * scale,
				translation.Width,
				translation.Height);
		}
        /// <summary>
        /// Returns a translation and Z rotation transform.
        /// </summary>
        /// <param name="translation">Translation values.</param>
        /// <param name="rotation">Rotation value.</param>
        /// <returns>Transform.</returns>
		public static Transform Create(Size translation, int rotation)
		{
			return new Transform (
				Math.Integer.Cosine(rotation),
				Math.Integer.Sine(rotation),
				-Math.Integer.Sine(rotation),
				Math.Integer.Cosine(rotation),
				translation.Width,
				translation.Height);
		}
        /// <summary>
        /// Returns a translation transform with the same X and Y values.
        /// </summary>
        /// <param name="delta">Value to translate.</param>
        /// <returns>Translation transform.</returns>
		public static Transform CreateTranslation(int delta)
		{
			return Transform.CreateTranslation(delta, delta);
		}
        /// <summary>
        /// Returns a translation transform with independent X and Y values.
        /// </summary>
        /// <param name="delta">Translation values.</param>
        /// <returns>Translation transform.</returns>
		public static Transform CreateTranslation(Size delta)
		{
			return Transform.CreateTranslation(delta.Width, delta.Height);
		}
        /// <summary>
        /// Returns a translation transform.
        /// </summary>
        /// <param name="delta">Translation values.</param>
        /// <returns>Translation transform.</returns>
		public static Transform CreateTranslation(Point delta)
		{
			return Transform.CreateTranslation(delta.X, delta.Y);
		}
        /// <summary>
        /// Returns a translation transform.
        /// </summary>
        /// <param name="xDelta">Translation in X.</param>
        /// <param name="yDelta">Translation in Y.</param>
        /// <returns>Translation transform.</returns>
		public static Transform CreateTranslation(int xDelta, int yDelta)
		{
			return new Transform(1,0,0,1, xDelta, yDelta);
		}
        /// <summary>
        /// Returns a translation transform.
        /// </summary>
        /// <param name="xDelta">Translation in X.</param>
        /// <param name="yDelta">Translation in Y.</param>
        /// <param name="zDelta">Translation in Z.</param>
        /// <returns>Translation transform.</returns>
        public static Transform CreateTranslation(int xDelta, int yDelta, int zDelta, Geometry2D.Integer.Size fieldOfView, Geometry2D.Integer.Size scale)
        {
            int k = Math.Integer.Tangens(Math.Integer.ToRadians(fieldOfView.Width / 2)) / (scale.Width / 2);
            int relativeZ = 1 / (1 + zDelta * k);
            return new Transform(relativeZ, 0, 0, relativeZ, xDelta, yDelta);
        }
        /// <summary>
        /// Return a scale transform with the proportional X and Y factors.
        /// </summary>
        /// <param name="factor">Scaling factor.</param>
        /// <returns></returns>
		public static Transform CreateScaling(int factor)
		{
			return Transform.CreateScaling(factor, factor);
		}
        /// <summary>
        /// Returns a scale transform with independent X and Y factors.
        /// </summary>
        /// <param name="factor">Scaling factors.</param>
        /// <returns></returns>
		public static Transform CreateScaling(Size factor)
		{
			return Transform.CreateScaling(factor.Width, factor.Height);
		}
        /// <summary>
        /// Returns a scale transform with independent X and Y factors.
        /// </summary>
        /// <param name="xFactor">Scaling factor in X.</param>
        /// <param name="yFactor">Scaling factor in Y.</param>
        /// <returns>Scale transform.</returns>
		public static Transform CreateScaling(int xFactor, int yFactor)
		{
			return new Transform(xFactor, 0, 0, yFactor, 0, 0);
		}
        /// <summary>
        /// Returns a Z rotation transform.
        /// </summary>
        /// <param name="angle">Z rotation angle.</param>
        /// <returns>Z rotation transform.</returns>
		public static Transform CreateZRotation(int angle)
		{
			return new Transform(Math.Integer.Cosine(angle), Math.Integer.Sine(angle), -Math.Integer.Sine(angle), Math.Integer.Cosine(angle), 0, 0);
		}
        /// <summary>
        /// Returns a Z rotation transform around a pivot.
        /// </summary>
        /// <param name="angle">Z rotation angle.</param>
        /// <param name="pivot">Pivot point.</param>
        /// <returns>Z rotation transform.</returns>
		public static Transform CreateZRotation(int  angle, Point pivot)
		{
			int  one = 1;
			int  sine = Math.Integer.Sine(angle);
			int  cosine = Math.Integer.Cosine(angle);
			return new Transform(cosine, sine, -sine, cosine, (one - cosine) * pivot.X + sine * pivot.Y, -sine * pivot.X + (one - cosine) * pivot.Y);
		}
        /// <summary>
        /// Returns an X rotation transform.
        /// </summary>
        /// <param name="angle">X rotation angle.</param>
        /// <returns>Z rotation transform.</returns>
        public static Transform CreateXRotation(int  angle, Geometry2D.Integer.Size fieldOfView, Geometry2D.Integer.Size scale)
        {
            int  k = Math.Integer.Tangens(Math.Integer.ToRadians(fieldOfView.Width / 2)) / (scale.Width / 2);
			int  tangens = Math.Integer.Tangens(angle);
            int  cosine = Math.Integer.Cosine(angle);
            return new Transform(1 / cosine, 0, 0, 0, 1, k * tangens, 0, -tangens/k, 1);
        }
        /// <summary>
        /// Returns an Y rotation transform.
        /// </summary>
        /// <param name="angle">Y rotation angle.</param>
        /// <returns>Z rotation transform.</returns>
        public static Transform CreateYRotation(int  angle, Geometry2D.Integer.Size fieldOfView, Geometry2D.Integer.Size scale)
        {
            int  k = Math.Integer.Tangens(Math.Integer.ToRadians(fieldOfView.Width / 2)) / (scale.Width / 2);
            int  tangens = Math.Integer.Tangens(angle);
            int  cosine = Math.Integer.Cosine(angle);
            return new Transform(1, 0, k * tangens, 0, 1 / cosine, 0, -tangens / k, 0, 1);
        }
        /// <summary>
        /// Returns a X skewing transform.
        /// </summary>
        /// <param name="angle">Angle to skew.</param>
        /// <returns>X skewinf transform.</returns>
		public static Transform CreateSkewingX(int angle)
		{
			return new Transform(1, 0, Math.Integer.Tangens(angle), 1, 0, 0);
		}
        /// <summary>
        /// Returns a Y skewing transform.
        /// </summary>
        /// <param name="angle">Angle to skew.</param>
        /// <returns>Y skewinf transform.</returns>
		public static Transform CreateSkewingY(int angle)
		{
			return new Transform(1, Math.Integer.Tangens(angle), 0, 1, 0, 0);
		}
        /// <summary>
        /// Returns a X reflection transform.
        /// </summary>
        /// <returns>X reflection transform.</returns>
		public static Transform CreateReflectionX()
		{
			return new Transform(-1, 0, 0, 1, 0, 0);
		}
        /// <summary>
        /// Returns a Y reflection transform.
        /// </summary>
        /// <returns>Y reflection transform.</returns>
		public static Transform CreateReflectionY()
		{
			return new Transform(1, 0, 0, -1, 0, 0);
		}
		#endregion
		#region Arithmetic Operators
        /// <summary>
        /// Defines multiplication.
        /// </summary>
        /// <param name="left">Left transform.</param>
        /// <param name="right">Right transform.</param>
        /// <returns>Multiplied transform.</returns>
		public static Transform operator *(Transform left, Transform right)
		{
			return new Transform(
				left.A * right.A + left.D * right.B + left.G * right.C,
				left.B * right.A + left.E * right.B + left.H * right.C,
                left.C * right.A + left.F * right.B + left.I * right.C,
				left.A * right.D + left.D * right.E + left.G * right.F,
				left.B * right.D + left.E * right.E + left.H * right.F,
				left.C * right.D + left.F * right.E + left.I * right.F,
				left.A * right.G + left.D * right.H + left.G * right.I,
				left.B * right.G + left.E * right.H + left.H * right.I,
                left.C * right.G + left.F * right.H + left.I * right.I);
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
			return  left.A == right.A && 
                    left.B == right.B &&
                    left.C == right.C && 
                    left.D == right.D && 
                    left.E == right.E &&
                    left.F == right.F && 
                    left.G == right.G &&
                    left.H == right.H && 
                    left.I == right.I;
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
        /// <summary>
        /// Returns true if the instance is equal to another transform.
        /// </summary>
        /// <param name="other">Comparison transform.</param>
        /// <returns>True if equal.</returns>
		public bool Equals(Transform other)
		{
			return this == other;
		}
		#endregion
		#region Casts
        /// <summary>
        /// Returns the string representation of a transform.
        /// </summary>
        /// <param name="value">Transform.</param>
        /// <returns>String representation.</returns>
		public static implicit operator string(Transform value)
		{
			return value.NotNull() ? value.ToString() : null;
		}
        /// <summary>
        /// Creates an instance from a string.
        /// </summary>
        /// <param name="value">The string to parse.</param>
        /// <returns>New instance.</returns>
		public static explicit operator Transform(string value)
		{
			Transform result = new Transform();
			if (value.NotEmpty())
			{
				try
				{
					string[] values = value.Split(new char[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);
                    switch (values.Length)
                    {
                        case 6:
                                result = new Transform(Math.Integer.Parse(values[0]), Math.Integer.Parse(values[1]), Math.Integer.Parse(values[2]), Math.Integer.Parse(values[3]), Math.Integer.Parse(values[4]), Math.Integer.Parse(values[5]));
                                break;
                        case 9:
                                result = new Transform(Math.Integer.Parse(values[0]), Math.Integer.Parse(values[1]), Math.Integer.Parse(values[2]), Math.Integer.Parse(values[3]), Math.Integer.Parse(values[4]), Math.Integer.Parse(values[5]), Math.Integer.Parse(values[6]), Math.Integer.Parse(values[7]), Math.Integer.Parse(values[8]));
                                break;
                        default:
                                break;
                    }
				}
				catch
				{
				}
			}
			return result;
		}
        /// <summary>
        /// Returns a float array copy of the instance.
        /// </summary>
        /// <param name="value">Transform to copy.</param>
        /// <returns>Float array copy of the instance.</returns>
		public static explicit operator int[,](Transform value)
		{
			int[,] result = new int[3, 3];
			for (int x = 0; x < 3; x++)
				for (int y = 0; y < 3; y++)
					result[x, y] = value[x, y];
			return result;
		}
        /// <summary>
        /// Returns a byte array copy of the instance.
        /// </summary>
        /// <param name="value">Transform to copy.</param>
        /// <returns>Byte array copy of the instance.</returns>
		public static implicit operator byte[](Transform value)
		{
			int size = sizeof(int);
			byte[] result = new byte[9 * size];
			for (int x = 0; x < 3; x++)
				for (int y = 0; y < 3; y++)
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
			return (33 * (33 * (33* (33 * (33 * (33 * (33 * this.A.GetHashCode() ^ this.B.GetHashCode()) ^ this.C.GetHashCode()) ^ this.D.GetHashCode()) ^ this.E.GetHashCode()) ^ this.F.GetHashCode()) ^ this.G.GetHashCode()) ^ this.H.GetHashCode()) ^ this.I.GetHashCode();
		}
        /// <summary>
        /// Returns a string representation of the instance.
        /// </summary>
        /// <returns>String representation.</returns>
		public override string ToString()
		{
			return this.ToString("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}");
		}
        /// <summary>
        /// Returns a string in the specified format.
        /// </summary>
        /// <param name="format">Format of the return string.</param>
        /// <returns>Formatted string.</returns>
		public string ToString(string format)
		{
			return String.Format(format, 
			Math.Integer.ToString(this.A), 
			Math.Integer.ToString(this.B), 
			Math.Integer.ToString(this.C), 
			Math.Integer.ToString(this.D), 
			Math.Integer.ToString(this.E), 
			Math.Integer.ToString(this.F), 
			Math.Integer.ToString(this.G), 
			Math.Integer.ToString(this.H), 
			Math.Integer.ToString(this.I));
		}
        /// <summary>
        /// Returns a Matlab coded matrix string
        /// </summary>
        /// <returns>Matlab coded matrix string</returns>
		public string ToMatlabString()
		{
			return this.ToString("{0}, {3}, {6}; {1}, {4}, {7}; {2}, {5}, {8}");
		}
		#endregion
	}
}
