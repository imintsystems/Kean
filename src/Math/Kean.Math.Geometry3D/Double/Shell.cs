﻿// 
//  Shell.cs
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
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.using System;
using System;
using Kean.Core.Extension;

namespace Kean.Math.Geometry3D.Double
{
    public class Shell : Abstract.Shell<Transform, TransformValue, Shell, ShellValue, Box, BoxValue, Point, PointValue, Size, SizeValue, Kean.Math.Double, double>
    {
        public override ShellValue Value { get { return (ShellValue)this; } }
        public Shell() { }
        public Shell(double left, double right, double top, double bottom, double front, double back) : base(left, right, top, bottom, front, back) { }
        public Box Decrease(Size size)
          {
              return new Box(this.Left, this.Top, this.Front, size.Width - this.Left - this.Right, size.Height - this.Top - this.Bottom, size.Depth - this.Front - this.Back);
          }
          public Box Increase(Size size)
          {
              return new Box(-this.Left, -this.Right, -this.Front, size.Width + this.Left + this.Right, size.Height + this.Top + this.Bottom, size.Depth + this.Front + this.Back);
          }
          #region Casts
          public static implicit operator Shell(Single.Shell value)
          {
              return new Shell(value.Left, value.Right, value.Top, value.Bottom, value.Front, value.Back);
          }
          public static implicit operator Shell(Integer.Shell value)
          {
              return new Shell(value.Left, value.Right, value.Top, value.Bottom, value.Front, value.Back);
          }
          public static explicit operator Single.Shell(Shell value)
          {
              return new Single.Shell((Kean.Math.Single)(value.Left), (Kean.Math.Single)(value.Right), (Kean.Math.Single)(value.Top), (Kean.Math.Single)(value.Bottom), (Kean.Math.Single)(value.Front), (Kean.Math.Single)(value.Back));
          }
          public static explicit operator Integer.Shell(Shell value)
          {
              return new Integer.Shell((Kean.Math.Integer)(value.Left), (Kean.Math.Integer)(value.Right), (Kean.Math.Integer)(value.Top), (Kean.Math.Integer)(value.Bottom), (Kean.Math.Integer)(value.Front), (Kean.Math.Integer)(value.Back));
          }
          public static implicit operator Shell(ShellValue value)
          {
              return new Shell(value.Left, value.Right, value.Top, value.Bottom, value.Front, value.Back);
          }
          public static explicit operator ShellValue(Shell value)
          {
              return new ShellValue(value.Left, value.Right, value.Top, value.Bottom, value.Front, value.Back);
          }
      
        public static implicit operator string(Shell value)
        {
            return value.NotNull() ? value.ToString() : null;
        }
        public static implicit operator Shell(string value)
        {
            Shell result = null;
            if (value.NotEmpty())
            {
                try
                {
                    string[] values = value.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length == 6)
                        result = new Shell(Kean.Math.Double.Parse(values[0]), Kean.Math.Double.Parse(values[1]), Kean.Math.Double.Parse(values[2]), Kean.Math.Double.Parse(values[3]), Kean.Math.Double.Parse(values[4]), Kean.Math.Double.Parse(values[5]));
                }
                catch
                {
                    result = null;
                }
            }
            return result;
        }
        #endregion
    }
}