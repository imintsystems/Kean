﻿// 
//  Group.cs
//  
//  Author:
//       Simon Mika <smika@hx.se>
//  
//  Copyright (c) 2012 Simon Mika
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
using Kean;
using Kean.Extension;
using Kean.Reflect.Extension;

namespace Kean.Serialize.Rebuilder
{
	public class Group :
		IRebuilder
	{
		IRebuilder[] rebuilders;
		public Group(params IRebuilder[] rebuilders)
		{
			this.rebuilders = rebuilders;
		}
		#region IRebuilder Members
		public Data.Node Store(Storage storage, Data.Node data)
		{
			foreach (IRebuilder rebuilder in this.rebuilders)
				data = rebuilder.Store(storage, data);
			return data;
		}
		public Data.Node Load(Storage storage, Data.Node data)
		{
			foreach (IRebuilder rebuilder in this.rebuilders)
				data = rebuilder.Load(storage, data);
			return data;
		}
		#endregion
	}
}

