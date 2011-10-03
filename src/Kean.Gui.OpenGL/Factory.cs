﻿// 
//  Factory.cs
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
using Geometry2D = Kean.Math.Geometry2D;
using GL = OpenTK.Graphics.OpenGL.GL;
using Error = Kean.Core.Error;
using Log = Kean.Extra.Log;
using Draw = Kean.Draw;
using Gpu = Kean.Draw.Gpu;
using Raster = Kean.Draw.Raster;
using Kean.Gui.OpenGL.Extension;

namespace Kean.Gui.OpenGL
{
	public abstract class Factory :
		Gpu.Backend.IFactory
	{
		#region Constructors
		protected Factory()
		{

		}
		#endregion

		#region IFactory Members
		public abstract Gpu.Backend.IImage CreateImage(Gpu.Backend.ImageType type, Geometry2D.Integer.Size size, Draw.CoordinateSystem coordinateSystem);
		public abstract Gpu.Backend.IImage CreateImage(Raster.Image image);
		#endregion
	}
}
