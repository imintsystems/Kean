﻿// 
//  Double.cs
//  
//  Author:
//       Anders Frisk <andersfrisk77@gmail.com>
//  
//  Copyright (c) 2009-2012 Anders Frisk
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
using Kean.Extension;

namespace Kean.Buffer
{
	public class Double :
		Vector<double>
	{
		public Double(int size) : this(size, null) { }
		public Double(int size, Action<IntPtr> free) : base(size, free) { }
		public Double(double[] data) : base(data, null) { }
		public Double(double[] data, Action<IntPtr> free) : base(data, free) { }
	}
}
