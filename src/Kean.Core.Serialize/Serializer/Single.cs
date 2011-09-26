﻿// 
//  Single.cs
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

namespace Kean.Core.Serialize.Serializer
{
	public class Single :
		ISerializer
	{
		public Single()
		{ }
		#region ISerializer Members
		public ISerializer Find(Reflect.Type type)
		{
			return type == "float" ? this : null;
		}
		public Data.Node Serialize(Storage storage, Reflect.Type type, object data)
		{
			return new Data.Single((float)data);
		}
		public object Deserialize(Storage storage, Reflect.Type type, Data.Node data)
		{
			return data is Data.Single ? (data as Data.Single).Value : 0;
		}
		#endregion
	}
}
