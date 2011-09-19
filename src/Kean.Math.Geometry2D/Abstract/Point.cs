// 
//  Point.cs
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
namespace Kean.Math.Geometry2D.Abstract
{
    public abstract class Point<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V> :
		Vector<PointType, PointValue, TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V>,
        IPoint<V>,
		IEquatable<PointType>
		where TransformType : Transform<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V>, ITransform<V>, new()
        where TransformValue : struct, ITransform<V>
		where ShellType : Shell<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V>, IShell<V>, new()
		where ShellValue : struct, IShell<V>
		where BoxType : Box<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V>, IBox<PointValue, SizeValue, V>, new()
        where BoxValue : struct, IBox<PointValue, SizeValue, V>
		where PointType : Point<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V>, IPoint<V>, new()
        where PointValue : struct, IPoint<V>, IVector<V>
		where SizeType : Size<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V>, ISize<V>, new()
        where SizeValue : struct, ISize<V>, IVector<V>
        where R : Kean.Math.Abstract<R, V>, new()
        where V : struct
	{
		public V X { get { return base.X; } }
		public V Y { get { return base.Y; } }
		#region IPoint<V> Members
		V IPoint<V>.X { get { return this.X; } }
		V IPoint<V>.Y { get { return this.Y; } }
		#endregion
		#region Constructors
        protected Point() { }
        protected Point(R x, R y) :
			base(x, y)
		{ }
        #endregion
		#region IEquatable<PointType> Members
		public bool Equals(PointType other)
		{
			return other.NotNull() && this.X.Equals(other.X) && this.Y.Equals(other.Y);
		}
		#endregion
		#region Arithmetic Operators
        public static PointType operator +(Point<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V> left, Point<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V> right)
        {
            return new PointType().Create(left.X + (R)right.X, left.Y + (R)right.Y);
        }
        public static PointType operator +(Point<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V> left, IVector<V> right)
        {
            return new PointType().Create(left.X + (R)right.X, left.Y + (R)right.Y);
        }
        public static PointType operator +(IVector<V> left, Point<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V> right)
        {
            return new PointType().Create(left.X + (R)right.X, left.Y + (R)right.Y);
        }
        public static PointType operator -(Point<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V> left, Point<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V> right)
        {
            return new PointType().Create(left.X - (R)right.X, left.Y - (R)right.Y);
        }
        public static PointType operator -(Point<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V> left, IVector<V> right)
        {
            return new PointType().Create(left.X - (R)right.X, left.Y - (R)right.Y);
        }
        public static PointType operator -(IVector<V> left, Point<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V> right)
        {
            return new PointType().Create(left.X - (R)right.X, left.Y - (R)right.Y);
        }
        public static PointType operator *(Point<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V> left, Point<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V> right)
        {
            return new PointType().Create(left.X * (R)right.X, left.Y * (R)right.Y);
        }
        public static PointType operator *(Point<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V> left, IVector<V> right)
        {
            return new PointType().Create(left.X * (R)right.X, left.Y * (R)right.Y);
        }
        public static PointType operator *(IVector<V> left, Point<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V> right)
        {
            return new PointType().Create(left.X * (R)right.X, left.Y * (R)right.Y);
        }
        public static PointType operator /(Point<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V> left, Point<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V> right)
        {
            return new PointType().Create(left.X / (R)right.X, left.Y / (R)right.Y);
        }
        public static PointType operator /(Point<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V> left, IVector<V> right)
        {
            return new PointType().Create(left.X / (R)right.X, left.Y / (R)right.Y);
        }
        public static PointType operator /(IVector<V> left, Point<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V> right)
        {
            return new PointType().Create(left.X / (R)right.X, left.Y / (R)right.Y);
        }
        public static PointType operator *(TransformType left, Point<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V> right)
        {
            return new PointType().Create(left.A * (R)right.X + left.C * (R)right.Y + left.E, left.B * (R)right.X + left.D * (R)right.Y + left.F);
        }
        #endregion
        public static PointType Polar(R radius, R azimuth)
        {
            return new PointType().Create(radius * azimuth.Cosinus(), radius * azimuth.Sinus());
		}
		#region Casts
		public static explicit operator SizeType(Point<TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V> point)
		{
			return Vector<SizeType, SizeValue, TransformType, TransformValue, ShellType, ShellValue, BoxType, BoxValue, PointType, PointValue, SizeType, SizeValue, R, V>.Create(point.X, point.Y);
		}
		#endregion
	}
}

