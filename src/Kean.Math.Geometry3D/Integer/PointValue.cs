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
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.using System;
namespace Kean.Math.Geometry3D.Integer
{
	public struct PointValue :
		Abstract.IPoint<int>
	{
		int x;
		int y;
		int z;
		public int X
		{
			get { return this.x; }
			set { this.x = value; }
		}
		public int Y
		{
			get { return this.y; }
			set { this.y = value; }
		}
		public int Z
		{
			get { return this.z; }
			set { this.z = value; }
		}
		public PointValue(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
		public static implicit operator Point(PointValue value)
		{
			return new Point(value.X, value.Y, value.Z);
		}
		public static explicit operator PointValue(Point value)
		{
			return new PointValue(value.X, value.Y, value.Z);
		}
	}
}