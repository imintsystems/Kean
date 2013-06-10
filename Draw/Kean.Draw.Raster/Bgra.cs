﻿// 
//  Bgra.cs
//  
//  Author:
//       Simon Mika <smika@hx.se>
//  
//  Copyright (c) 2010-2011 Simon Mika
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
using Buffer = Kean.Core.Buffer;
using Geometry2D = Kean.Math.Geometry2D;
using Collection = Kean.Core.Collection;
using Integer = Kean.Math.Integer;
using Single = Kean.Math.Single;

namespace Kean.Draw.Raster
{
	[System.Runtime.InteropServices.ComVisible(true)]
	public class Bgra :
		Packed
	{
		public Color.Bgra this[Geometry2D.Integer.Point position]
		{
			get { return this[position.X, position.Y]; }
			set { this[position.X, position.Y] = value; }
		}
		public Color.Bgra this[int x, int y]
		{
			get { unsafe { return *((Color.Bgra*)((byte*)this.Buffer + y * this.Stride) + x); } }
			set { unsafe { *((Color.Bgra*)((byte*)this.Buffer + y * this.Stride) + x) = value; } }
		}
		public Color.Bgra this[Geometry2D.Single.Point position]
		{
			get { return this[position.X, position.Y]; }
		}
		public Color.Bgra this[float x, float y]
		{
			get
			{
				float left = x - Integer.Floor(x);
				float top = y - Integer.Floor(y);

				Color.Bgra topLeft = this[Integer.Floor(x), Integer.Floor(y)];
				Color.Bgra bottomLeft = this[Integer.Floor(x), Integer.Ceiling(y)];
				Color.Bgra topRight = this[Integer.Ceiling(x), Integer.Floor(y)];
				Color.Bgra bottomRight = this[Integer.Ceiling(x), Integer.Ceiling(y)];

				float r, g, b, a;
				b = top * (left * topLeft.Blue + (1 - left) * topRight.Blue) + (1 - top) * (left * bottomLeft.Blue + (1 - left) * bottomRight.Blue);
				g = top * (left * topLeft.Green + (1 - left) * topRight.Green) + (1 - top) * (left * bottomLeft.Green + (1 - left) * bottomRight.Green);
				r = top * (left * topLeft.Red + (1 - left) * topRight.Red) + (1 - top) * (left * bottomLeft.Red + (1 - left) * bottomRight.Red);
				a = top * (left * topLeft.Alpha + (1 - left) * topRight.Alpha) + (1 - top) * (left * bottomLeft.Alpha + (1 - left) * bottomRight.Alpha);

				return new Color.Bgra((byte)b, (byte)g, (byte)r, (byte)a);
			}
		}

		protected override int BytesPerPixel { get { return 4; } }
		public Bgra(Geometry2D.Integer.Size size) :
			this(new Buffer.Vector<byte>(Packed.CalculateLength(size, 4)), size) { }
		public Bgra(Geometry2D.Integer.Size size, CoordinateSystem coordinateSystem) :
			base(new Buffer.Vector<byte>(Packed.CalculateLength(size, 4)), size, coordinateSystem) { }
		public Bgra(byte[] data, Geometry2D.Integer.Size size) :
			this(new Buffer.Vector<byte>(data), size) { }
		public Bgra(IntPtr pointer, Geometry2D.Integer.Size size) :
			this(new Buffer.Sized(pointer, size.Area * 4), size) { }
		public Bgra(Buffer.Sized buffer, Geometry2D.Integer.Size size) :
			base(buffer, size, CoordinateSystem.Default) { }
		public Bgra(Buffer.Sized buffer, Geometry2D.Integer.Size size, CoordinateSystem coordinateSystem) :
			base(buffer, size, coordinateSystem) { }
		protected Bgra(Bgra original) :
			base(original) { }
		internal Bgra(Image original) :
			this(original.Size, original.CoordinateSystem)
		{
			unsafe
			{
				int* destination = (int*)this.Pointer;
				original.Apply(color => *((Color.Bgra*)destination++) = new Color.Bgra(color, 255));
			}
		}
		protected override Draw.Cairo.Image CreateCairoImage(Buffer.Sized buffer, Geometry2D.Integer.Size size)
		{
			return new Cairo.Bgra(buffer, size);
		}
		public override Draw.Image Create(Geometry2D.Integer.Size size)
		{
			return new Bgra(size) { Crop = this.Crop, Wrap = this.Wrap };
		}
		public override Draw.Image Copy()
		{
			return new Bgra(this);
		}
		public override void Apply(Action<Color.Bgr> action)
		{
			unsafe
			{
				int* end = (int*)this.Pointer + this.Size.Area;
				for (int* source = (int*)this.Pointer; source < end; source++)
					action(*((Color.Bgr*)source));
			}
		}
		public override void Apply(Action<Color.Yuv> action)
		{
			this.Apply(Color.Convert.FromBgr(action));
		}
		public override void Apply(Action<Color.Y> action)
		{
			this.Apply(Color.Convert.FromBgr(action));
		}
		public override float Distance(Draw.Image other)
		{
			float result = 0;
			if (other is Bgra && this.Size == other.Size)
			{
				for (int y = 0; y < this.Size.Height; y++)
					for (int x = 0; x < this.Size.Width; x++)
					{
						Color.Bgra pixel = this[x, y];
						float minimumDistance = float.MaxValue;
						for (int otherY = Integer.Maximum(0, y - 1); otherY < Integer.Minimum(y + 1, this.Size.Height); otherY++)
							for (int otherX = Integer.Maximum(0, x - 1); otherX < Integer.Minimum(x + 1, this.Size.Width); otherX++)
								minimumDistance = Single.Minimum(minimumDistance, pixel.Distance((other as Bgra)[otherX, otherY]));
						result += minimumDistance;
					}
				result /= this.Size.Length;
			}
			else
				result = float.PositiveInfinity;
			return result;
		}
		#region Static Open
		public static new Bgra OpenResource(System.Reflection.Assembly assembly, string name)
		{
			return Image.OpenResource<Bgra>(assembly, name);
		}
		public static new Bgra OpenResource(string name)
		{
			return Image.OpenResource<Bgra>(System.Reflection.Assembly.GetCallingAssembly(), name);
		}
		public static new Bgra Open(string filename)
		{
			return Image.Open<Bgra>(filename);
		}
		public static new Bgra Open(System.IO.Stream stream)
		{
			return Image.Open<Bgra>(stream);
		}
		#endregion
	}
}
