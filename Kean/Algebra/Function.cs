﻿//
//  Function.cs
//
//  Author:
//       Simon Mika <simon@mika.se>
//
//  Copyright (c) 2014 Simon Mika
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

namespace Kean.Algebra
{
	public abstract class Function :
	Expression
	{
		public override int Precedence { get { return 0; } }
		protected abstract string Symbol { get; }
		public Expression Argument { get; set; }
		protected Function(Expression argument)
		{
			this.Argument = argument;
		}
		public override string ToString()
		{
			return this.Symbol + "(" + this.Argument.ToString(this.Precedence) + ")";
		}
		public override bool Equals(Expression other)
		{
			return other is Function && this.Symbol == ((Function)other).Symbol && this.Argument == ((Function)other).Argument;
		}
	}
}

