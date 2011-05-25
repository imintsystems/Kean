// 
//  Abstract.cs
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
	public abstract class Abstract :
		ISerializer
	{
		protected Abstract()
		{
		}

		protected abstract T Deserialize<T>(Storage storage, Reflect.TypeName type, Data.Node data);
		protected abstract Data.Node Serialize<T> (Storage storage, Reflect.TypeName type, T data);
		#region ISerializer implementation
		public abstract bool Accepts(Type type);
		public Data.Node Serialize<T> (Storage storage, T data)
		{
			Reflect.TypeName type = data.GetType();
			Data.Node result = this.Serialize<T>(storage, type, data);
			if (type != typeof(T))
				result.Type = type;
			return result;
		}
		public T Deserialize<T>(Storage storage, Data.Node data)
		{
			return this.Deserialize<T>(storage, data.Type ?? typeof(T), data);
		}
		#endregion
	}
}
