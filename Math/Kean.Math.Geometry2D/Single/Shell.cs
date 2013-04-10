// 
//  Shell.cs (generated by template)
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

namespace Kean.Math.Geometry2D.Single
{
    public struct Shell :
		IEquatable<Shell>
    {
        public float Left;
        public float Right;
        public float Top;
        public float Bottom;
        public Point LeftTop { get { return new Point(this.Left, this.Top); } }
        public Size Size { get { return new Size(this.Left + this.Right, this.Top + this.Bottom); } }
        public Point Balance { get { return new Point(this.Right - this.Left, this.Bottom - this.Top); } }
      
		public Shell(float value) : this(value, value) { }
		public Shell(float x, float y) : this(x, x, y, y) { }
		public Shell(float left, float right, float top, float bottom)
        {
            this.Left = left;
            this.Right = right;
            this.Top = top;
            this.Bottom = bottom;
        }
		#region Increase, Decrease
		public Box Decrease(Size size)
		{
			return new Box(this.Left, this.Top, size.Width - this.Left - this.Right, size.Height - this.Top - this.Bottom);
		}
		public Box Increase(Size size)
		{
			return  new Box(-this.Left, -this.Right, size.Width + this.Left + this.Right, size.Height + this.Top + this.Bottom);
		}
		public Box Decrease(Box  box)
		{
			return new Box(box.LeftTop.X + this.Left, box.LeftTop.Y + this.Top, box.Size.Width - this.Left - this.Right, box.Size.Height - this.Top - this.Bottom);
		}
		public Box Increase(Box box)
		{
			return new Box(box.LeftTop.X - this.Left, box.LeftTop.Y - this.Top, box.Size.Width + this.Left + this.Right, box.Size.Height + this.Top + this.Bottom);
		}
		#endregion
        #region Static Operators
        public static Size operator -(Size left, Shell right)
        {
            return new Size(left.Width - right.Left - right.Right, left.Height - right.Top - right.Bottom);
        }
        public static Size operator +(Size left, Shell right)
        {
            return new Size(left.Width + right.Left + right.Right, left.Height + right.Top + right.Bottom);
        }
        public static Shell operator +(Shell left, Shell right)
        {
            return new Shell(left.Left + right.Left, left.Right + right.Right, left.Top + right.Top, left.Bottom + right.Bottom);
        }
        public static Shell operator -(Shell left, Shell right)
        {
            return new Shell(left.Left - right.Left, left.Right - right.Right, left.Top - right.Top, left.Bottom - right.Bottom);
        }
        public static Shell Maximum(Shell left, Shell right)
        {
            return new Shell(Kean.Math.Single.Maximum(left.Left, right.Left), Kean.Math.Single.Maximum(left.Right, right.Right), Kean.Math.Single.Maximum(left.Top, right.Top), Kean.Math.Single.Maximum(left.Bottom, right.Bottom));
        }
        public static Shell Minimum(Shell left, Shell right)
        {
            return new Shell(Kean.Math.Single.Minimum(left.Left, right.Left), Kean.Math.Single.Minimum(left.Right, right.Right), Kean.Math.Single.Minimum(left.Top, right.Top), Kean.Math.Single.Minimum(left.Bottom, right.Bottom));
        }
        #endregion
        #region Comparison Operators
        /// <summary>
        /// Defines equality.
        /// </summary>
        /// <param name="Left">Point Left of operator.</param>
        /// <param name="Right">Point Right of operator.</param>
        /// <returns>True if <paramref name="Left"/> equals <paramref name="Right"/> else false.</returns>
        public static bool operator ==(Shell left, Shell right)
        {
            return left.Left == right.Left && left.Right == right.Right && left.Top == right.Top && left.Bottom == right.Bottom;
        }
        /// <summary>
        /// Defines inequality.
        /// </summary>
        /// <param name="Left">Point Left of operator.</param>
        /// <param name="Right">Point Right of operator.</param>
        /// <returns>False if <paramref name="Left"/> equals <paramref name="Right"/> else true.</returns>
        public static bool operator !=(Shell left, Shell right)
        {
            return !(left == right);
        }
        #endregion
        #region Casts
        public static implicit operator Shell(Integer.Shell value)
        {
            return new Shell(value.Left, value.Right, value.Top, value.Bottom);
        }
        public static explicit operator Integer.Shell(Shell value)
        {
            return new Integer.Shell((Kean.Math.Integer)(value.Left), (Kean.Math.Integer)(value.Right), (Kean.Math.Integer)(value.Top), (Kean.Math.Integer)(value.Bottom));
        }
        public static implicit operator string(Shell value)
        {
            return value.NotNull() ? value.ToString() : null;
        }
        public static implicit operator Shell(string value)
        {
            Shell result = new Shell();
            if (value.NotEmpty())
            {
                try
                {
                    string[] values = value.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length == 4)
                        result = new Shell(Kean.Math.Single.Parse(values[0]), Kean.Math.Single.Parse(values[1]), Kean.Math.Single.Parse(values[2]), Kean.Math.Single.Parse(values[3]));
                }
                catch
                {
                }
            }
            return result;
        }
        #endregion
        #region Object Overrides
		public override bool Equals(object other)
		{
			return (other is Shell) && this.Equals((Shell)other);
		}
		/// <summary>
		/// Return true if this object and <paramref name="other">other</paramref> are equal.
		/// </summary>
		/// <param name="other">Object to compare with</param>
		/// <returns>True if this object and <paramref name="other">other</paramref> are equal else false.</returns>
		public bool Equals(Shell other)
		{
			return this == other;
		}
        public override int GetHashCode()
        {
            return 33 * (33 * (33 * this.Left.GetHashCode() ^ this.Right.GetHashCode()) ^ this.Top.GetHashCode()) ^ this.Bottom.GetHashCode();
        }
        public override string ToString()
        {
            return Kean.Math.Single.ToString(this.Left) + ", " + Kean.Math.Single.ToString(this.Right) + ", " + Kean.Math.Single.ToString(this.Top) + ", " + Kean.Math.Single.ToString(this.Bottom);
        }
    	#endregion
    }
}
