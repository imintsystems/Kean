﻿// 
//  ConsoleTerminal.cs
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

namespace Kean.Cli
{
	public class ConsoleTerminal :
		Terminal
	{
		ConsoleDevice device;
		#region Constructors
		public ConsoleTerminal() :
			this(new ConsoleDevice())
		{ }
		ConsoleTerminal(ConsoleDevice device) :
			base(device, device)
		{
			this.device = device;
			this.device.Command += command => this.OnCommand(command);
		}
		#endregion
		public override bool Echo
		{
			get { return this.device.LocalEcho; }
			set { this.device.LocalEcho = value; }
		}
		public override Geometry2D.Integer.Point CursorPosition
		{
			get { return new Geometry2D.Integer.Point(Console.CursorLeft, Console.CursorTop); }
			set { Console.SetCursorPosition(value.X, value.Y); }
		}
		public override bool MoveCursor(Geometry2D.Integer.Size delta)
		{
			this.CursorPosition += delta;
			return true;
		}
		public override bool Clear()
		{
			Console.Clear();
			return true;
		}
	}
}