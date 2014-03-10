﻿//
//  Data1D.cs
//
//  Author:
//       Simon Mika <smika@hx.se>
//
//  Copyright (c) 2013 Simon Mika
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

using Kean.Collection.Extension;
using System;
using Collection = Kean.Collection;
using Error = Kean.Error;
using GL = OpenTK.Graphics.OpenGL.GL;
using Geometry2D = Kean.Math.Geometry2D;
using Geometry3D = Kean.Math.Geometry3D;
using Raster = Kean.Draw.Raster;
using Kean.Extension;

namespace Kean.Draw.OpenGL.Backend.OpenGL21
{
	public class Data1D :
		Backend.Data1D
	{
		protected internal Data1D(Backend.Context context) :
			base(context)
		{ }
		protected override int CreateIdentifier()
		{
			return GL.GenTexture();
		}
		protected override void Allocate(IntPtr pointer)
		{
			GL.TexParameter(OpenTK.Graphics.OpenGL.TextureTarget.Texture1D, OpenTK.Graphics.OpenGL.TextureParameterName.TextureMinFilter, (int)OpenTK.Graphics.OpenGL.TextureMinFilter.Nearest);
			GL.TexParameter(OpenTK.Graphics.OpenGL.TextureTarget.Texture1D, OpenTK.Graphics.OpenGL.TextureParameterName.TextureMagFilter, (int)OpenTK.Graphics.OpenGL.TextureMagFilter.Nearest);
			GL.TexParameter(OpenTK.Graphics.OpenGL.TextureTarget.Texture1D, OpenTK.Graphics.OpenGL.TextureParameterName.TextureWrapS, (int)OpenTK.Graphics.OpenGL.TextureWrapMode.MirroredRepeat);
			GL.TexImage1D(OpenTK.Graphics.OpenGL.TextureTarget.Texture1D, 0, this.PixelInternalFormat, this.Size, 0, this.PixelFormat, this.PixelType, pointer);
//			GL.TexImage1D(OpenTK.Graphics.OpenGL.TextureTarget.Texture1D, 0, OpenTK.Graphics.OpenGL.PixelInternalFormat.Luminance, this.Size, 0, OpenTK.Graphics.OpenGL.PixelFormat.Red, OpenTK.Graphics.OpenGL.PixelType.UnsignedByte, IntPtr.Zero);
		}
		protected override void Load(IntPtr pointer)
		{
			//GL.TexSubImage1D(OpenTK.Graphics.OpenGL.TextureTarget.Texture1D, 0, 0, this.Size, OpenTK.Graphics.OpenGL.PixelFormat.Red, OpenTK.Graphics.OpenGL.PixelType.UnsignedByte, data);
			GL.TexImage1D(OpenTK.Graphics.OpenGL.TextureTarget.Texture1D, 0, this.PixelInternalFormat, this.Size, 0, this.PixelFormat, this.PixelType, pointer);
		}
		public override void Use()
		{
			GL.BindTexture(OpenTK.Graphics.OpenGL.TextureTarget.Texture1D, this.Identifier);
		}
		public override void UnUse()
		{
			GL.BindTexture(OpenTK.Graphics.OpenGL.TextureTarget.Texture1D, 0);
		}
		protected internal override void Delete()
		{
			GL.DeleteTexture(this.Identifier);
			base.Delete();
		}
	}
}
