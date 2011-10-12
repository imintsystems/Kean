﻿// 
//  Bgra.cs
//  
//  Author:
//      Simon Mika <smika@hx.se
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
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Geometry2D = Kean.Math.Geometry2D;
namespace Kean.Draw.Cairo.Test
{
	[TestFixture]
	public class Bgra :
		Kean.Test.Fixture<Bgra>
	{
		string prefix = "Kean.Draw.Cairo.Test.Bgra.";
		float tolerance = 0.01f;
		protected override void Run()
		{
			this.Run(
				this.Create,
				this.Rectangle
				);
		}
		[Test]
		public void Create()
		{
			using (Cairo.Raster image = new Cairo.Bgra(new Geometry2D.Integer.Size(128, 256)))
			using (Draw.Raster.Bgra raster = new Draw.Raster.Bgra(image.Buffer, image.Size, image.CoordinateSystem))
			using (Draw.Raster.Image correct = Draw.Raster.Image.OpenResource("Correct.Bgra.Create.png"))
				Expect(raster.Distance(correct), Is.LessThanOrEqualTo(this.tolerance), this.prefix + "Create.0");
			//raster.Save("Create.png");
		}
		[Test]
		public void Rectangle()
		{
			using (Cairo.Raster image = new Cairo.Bgra(new Geometry2D.Integer.Size(128, 256)))
			{
				image.Canvas.Draw(new Color.Bgra(0, 128, 255, 128), new Color.Bgra(255, 128, 0, 128), 2, Path.CreateRectangle(new Kean.Math.Geometry2D.Single.Box(10, 10, 100, 100)));
				using (Draw.Raster.Bgra raster = new Draw.Raster.Bgra(image.Buffer, image.Size, image.CoordinateSystem))
				using (Draw.Raster.Image correct = Draw.Raster.Image.OpenResource("Correct.Bgra.Rectangle.png"))
					Expect(raster.Distance(correct), Is.LessThanOrEqualTo(this.tolerance), this.prefix + "Rectangle.0");
				//raster.Save("Rectangle.png");
			}
		}
	}
}
