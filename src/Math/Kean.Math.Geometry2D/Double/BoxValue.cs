﻿// 
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

namespace Kean.Math.Geometry2D.Double
{
    public struct BoxValue :
        Abstract.IBox<PointValue, SizeValue, double>,
		IEquatable<BoxValue>
    {
        public PointValue LeftTop;
        public SizeValue Size;
        #region IBox<PointValue,SizeValue,double> Members
        PointValue Kean.Math.Geometry2D.Abstract.IBox<PointValue, SizeValue, double>.LeftTop {get { return this.LeftTop; }}
        SizeValue Kean.Math.Geometry2D.Abstract.IBox<PointValue, SizeValue, double>.Size { get { return this.Size; } }
        #endregion
		#region Sizes
		public double Width { get { return this.Size.Width; } }
        public double Height { get { return this.Size.Height; } }
		#endregion
		#region All sides
		public double Left { get { return this.LeftTop.X; } }
        public double Top { get { return this.LeftTop.Y; } }
        public double Right { get { return this.LeftTop.X + this.Size.Width; } }
        public double Bottom { get { return this.LeftTop.Y + this.Size.Height; } }
		#endregion
		#region All other corners
		public PointValue RightTop { get { return new PointValue(this.Right, this.Top); } }
		public PointValue LeftBottom { get { return new PointValue(this.Left, this.Bottom); } }
		public PointValue RightBottom { get { return this.LeftTop + (Abstract.IVector<double>)this.Size; } }
		#endregion
		public PointValue Center { get { return this.LeftTop + (Abstract.IVector<double>)(this.Size / 2); } }
		public bool Empty { get { return this.Size.Empty; } }

		public BoxValue(double left, double top, double width, double height)
        {
            this.LeftTop = new PointValue(left, top);
            this.Size = new SizeValue(width, height);
        }
        public BoxValue(PointValue leftTop, SizeValue size)
        {
            this.LeftTop = leftTop;
            this.Size = size;
		}
		#region Methods
		public BoxValue Swap()
		{
			return new BoxValue(this.LeftTop.Swap(), this.Size.Swap());
		}
		public BoxValue Pad(double pad)
		{
			return this.Pad(pad, pad, pad, pad);
		}
		public BoxValue Pad(SizeValue padding)
		{
			return this.Pad(padding.Width, padding.Width, padding.Height, padding.Height);
		}
		public BoxValue Pad(double left, double right, double top, double bottom)
		{
			return new BoxValue(new PointValue(this.Left - left, this.Top - top), new SizeValue(this.Size.Width + left + right, this.Size.Height + top + bottom));
		}
		public BoxValue Intersection(BoxValue other)
		{
			double left = this.Left > other.Left ? this.Left : other.Left;
			double top = this.Top > other.Top ? this.Top : other.Top;
			double width = Kean.Math.Double.Maximum((this.Right < other.Right ? this.Right : other.Right) - left, 0);
			double height = Kean.Math.Double.Maximum((this.Bottom < other.Bottom ? this.Bottom : other.Bottom) - top, 0);
			return new BoxValue(left, top, width, height);
		}
		public BoxValue Union(BoxValue other)
		{
			double left = Kean.Math.Double.Minimum(this.Left, other.Left);
			double top = Kean.Math.Double.Minimum(this.Top, other.Top);
			double width = Kean.Math.Double.Maximum(this.Right, other.Right) - Kean.Math.Double.Minimum(this.Left, other.Left);
			double height = Kean.Math.Double.Maximum(this.Bottom, other.Bottom) - Kean.Math.Double.Minimum(this.Top, other.Top);
			return new BoxValue(left, top, width, height);
		}
        public bool Contains(Integer.PointValue point)
        {
            return this.Left <= point.X && point.X < this.Right && this.Top <= point.Y && point.Y < this.Bottom;
        }
        public bool Contains(Single.PointValue point)
        {
            return this.Left <= point.X && point.X < this.Right && this.Top <= point.Y && point.Y < this.Bottom;
        }
        public bool Contains(Double.PointValue point)
        {
            return this.Left <= point.X && point.X < this.Right && this.Top <= point.Y && point.Y < this.Bottom;
		}
		public bool Contains(BoxValue box)
		{
			return this.Intersection(box) == box;
		}
		public BoxValue Round()
		{
			return new BoxValue(this.LeftTop.Round(), this.Size.Round());
		}
		public BoxValue Ceiling()
		{
			PointValue leftTop = this.LeftTop.Floor();
			return new BoxValue(leftTop, (SizeValue)(this.RightBottom.Ceiling() - (Abstract.IVector<double>)leftTop));
		}
		public BoxValue Floor()
		{
			return new BoxValue(this.LeftTop.Round(), this.Size.Round());
		}
		#endregion
		#region Arithmetic operators
		public static BoxValue operator +(BoxValue left, BoxValue right)
		{
			BoxValue result;
			if (left.Empty)
				result = right;
			else if (right.Empty)
				result = left;
			else
				result = new BoxValue(Kean.Math.Double.Minimum(left.Left, right.Left), Kean.Math.Double.Minimum(left.Top, right.Top), Kean.Math.Double.Maximum(left.Right, right.Right) - Kean.Math.Double.Minimum(left.Left, right.Left), Kean.Math.Double.Maximum(left.Bottom, right.Bottom) - Kean.Math.Double.Minimum(left.Top, right.Top));
			return result;
		}
		public static BoxValue operator -(BoxValue left, BoxValue right)
		{
			BoxValue result;
			if (!left.Empty && !right.Empty)
			{
				double l = Kean.Math.Double.Maximum(left.Left, right.Left);
				double r = Kean.Math.Double.Minimum(left.Right, right.Right);
				double t = Kean.Math.Double.Maximum(left.Top, right.Top);
				double b = Kean.Math.Double.Minimum(left.Bottom, right.Bottom);
				if (l < r && t < b)
				{
					result = new BoxValue(l, t, r - l, b - t);
				}
				else
					result = new BoxValue();
			}
			else
				result = new BoxValue();
			return result;
		}
		public static BoxValue operator +(BoxValue left, PointValue right)
		{
			return new BoxValue(left.LeftTop + (Abstract.IVector<double>)right, left.Size);
		}
		public static BoxValue operator -(BoxValue left, PointValue right)
		{
			return new BoxValue(left.LeftTop - right, left.Size);
		}
		public static BoxValue operator +(BoxValue left, SizeValue right)
		{
			return new BoxValue(left.LeftTop, left.Size + right);
		}
		public static BoxValue operator -(BoxValue left, SizeValue right)
		{
			return new BoxValue(left.LeftTop, left.Size - right);
		}
		public static BoxValue operator *(TransformValue left, BoxValue right)
		{
			return new BoxValue(left * right.LeftTop, left * right.Size);
		}
		#endregion
        
		#region Comparison Operators
		/// <summary>
        /// Defines equality.
        /// </summary>
        /// <param name="Left">Point Left of operator.</param>
        /// <param name="Right">Point Right of operator.</param>
        /// <returns>True if <paramref name="Left"/> equals <paramref name="Right"/> else false.</returns>
        public static bool operator ==(BoxValue left, BoxValue right)
        {
            return left.Left == right.Left && left.Top == right.Top && left.Width == right.Width && left.Height == right.Height;
        }
        /// <summary>
        /// Defines inequality.
        /// </summary>
        /// <param name="Left">Point Left of operator.</param>
        /// <param name="Right">Point Right of operator.</param>
        /// <returns>False if <paramref name="Left"/> equals <paramref name="Right"/> else true.</returns>
        public static bool operator !=(BoxValue left, BoxValue right)
        {
            return !(left == right);
        }
        #endregion
        #region Static Operators
        public static BoxValue operator -(BoxValue left, ShellValue right)
        {
            return new BoxValue(left.LeftTop + right.LeftTop, left.Size - right.Size);
        }
        public static BoxValue operator +(BoxValue left, ShellValue right)
        {
            return new BoxValue(left.LeftTop - right.LeftTop, left.Size + right.Size);
        }
        #endregion
        #region Casts
        public static implicit operator BoxValue(Single.BoxValue value)
        {
            return new BoxValue(value.LeftTop, value.Size);
        }
        public static implicit operator BoxValue(Integer.BoxValue value)
        {
            return new BoxValue(value.LeftTop, value.Size);
        }
        public static explicit operator Single.BoxValue(BoxValue value)
        {
            return new Single.BoxValue((Single.PointValue)(value.LeftTop), (Single.SizeValue)(value.Size));
        }
        public static explicit operator Integer.BoxValue(BoxValue value)
        {
            return new Integer.BoxValue((Integer.PointValue)(value.LeftTop), (Integer.SizeValue)(value.Size));
        }
        public static implicit operator string(BoxValue value)
        {
            return value.ToString();
        }
        public static implicit operator BoxValue(string value)
        {
            BoxValue result = new BoxValue();
            if (value.NotEmpty())
            {

                try
                {
                    string[] values = value.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length == 4)
                        result = new BoxValue((PointValue)(values[0] + " " + values[1]), (SizeValue)(values[2] + " " + values[3]));
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
			return (other is BoxValue) && this.Equals((BoxValue)other);
		}
		public bool Equals(BoxValue other)
		{
			return this == other;
		}
		public override string ToString()
		{
            return this.LeftTop.ToString() + ", " + this.Size.ToString();
        }
		public override int GetHashCode()
        {
            return 33 * this.LeftTop.GetHashCode() ^ this.Size.GetHashCode();
        }
        #endregion
		#region Static Creators
		public static BoxValue Bounds(double left, double right, double top, double bottom)
		{
			return new BoxValue(left, top, right - left, bottom - top);
		}
		public static BoxValue Bounds(params PointValue[] points)
		{
			BoxValue result = new BoxValue();
			if (points.Length > 0)
			{
				double xMinimum = 0;
				double xMaximum = xMinimum;
				double yMinimum = xMinimum;
				double yMaximum = xMinimum;
				bool initilized = false;
				foreach (PointValue point in points)
				{
					
						if (!initilized)
						{
							initilized = true;
							xMinimum = point.X;
							xMaximum = point.X;
							yMinimum = point.Y;
							yMaximum = point.Y;
						}
						else
						{
							if (point.X < xMinimum)
								xMinimum = point.X;
							else if (point.X > xMaximum)
								xMaximum = point.X;
							if (point.Y < yMinimum)
								yMinimum = point.Y;
							else if (point.Y > yMaximum)
								yMaximum = point.Y;
						}
				}
				result = new BoxValue(xMinimum, yMinimum, xMaximum - xMinimum, yMaximum - yMinimum);
			}
			return result;
		}
		#endregion
	}
}
