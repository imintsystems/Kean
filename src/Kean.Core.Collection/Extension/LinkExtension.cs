// 
//  LinkExtension.cs
//  
//  Author:
//       Simon Mika <smika@hx.se>
//  
//  Copyright (c) 2009 Simon Mika
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
using Kean.Core.Basis.Extension;

namespace Kean.Core.Collection.Extension
{
	public static class LinkExtension
	{
		public static L Add<L, T>(this L me, T item)
			where L : class, ILink<L, T>, new() 
		{
			return new L()
			{
				Head = item,
				Tail = me,
			};
		}
		public static T Get<L, T>(this L me, int index)
			where L : class, ILink<L, T>, new() 
		{
			if (me.IsNull())
				throw new Exception.InvalidIndex();
			return index == 0 ? me.Head : me.Tail.Get<L, T>(index - 1);
		}
		public static void Set<L, T>(this L me, int index, T item)
			where L : class, ILink<L, T>, new() 
		{
			if (me.IsNull())
				throw new Exception.InvalidIndex();
			else if (index == 0)
				me.Head = item;
			else
				me.Tail.Set(index - 1, item);
		}
        public static int Count<L, T>(this L me) 
			where L : class, ILink<L, T>, new() 
        {
            return me.IsNull() ? 0 : (1 + me.Tail.Count<L, T>());
        }
		public static L Insert<L, T>(this L me, int index, T item)
			where L : class, ILink<L, T>, new() 
		{
			if (me.IsNull() && index > 0)
				throw new Exception.InvalidIndex();
			return (index == 0) ? 
				me.Add(item) :
				me.Tail.Insert<L, T>(index - 1, item).Add(me.Head);
		}
		public static L Remove<L, T>(this L me, int index)
			where L : class, ILink<L, T>, new() 
		{
			if (me.IsNull())
				throw new Exception.InvalidIndex();
			return (index == 0) ? 
				me.Tail : 
				me.Tail.Remove<L, T>(index - 1).Add(me.Head);
		}
		public static L Remove<L, T>(this L me, int index, out T element)
			where L : class, ILink<L, T>, new() 
		{
			L result;
			if (me.IsNull())
				throw new Exception.InvalidIndex();
			else if (index == 0)
			{
				element = me.Head;
				result = me.Tail;
			}
			else
				result = me.Tail.Remove<L, T>(index - 1, out element).Add(me.Head);
			return result;
		}
		public static bool Equals<L, T>(this L me, L other)
			where L : class, ILink<L, T>, new() 
		{
			return me.Same(other) || me.NotNull() && other.NotNull() && me.Head.Equals(other.Head) && me.Tail.Equals(other.Tail);
		}
        public static R Fold<L, T, R>(this L me, Func<T, R, R> function) 
			where L : class, ILink<L, T>, new() 
		{
			return me.Fold(function, default(R));
		}
        public static R Fold<L, T, R>(this L me, Func<T, R, R> function, R initial) 
			where L : class, ILink<L, T>, new() 
		{
			return me.IsNull() ? initial : function(me.Head, me.Tail.Fold(function, initial));
		}
        public static R FoldReverse<L, T, R>(this L me, Func<T, R, R> function) 
			where L : class, ILink<L, T>, new() 
        {
            return me.FoldReverse(function, default(R));
        }
        public static R FoldReverse<L, T, R>(this L me, Func<T, R, R> function, R initial) 
			where L : class, ILink<L, T>, new() 
        {
            return me.IsNull() ? initial : me.Tail.Fold(function, function(me.Head, initial));
		}
        public static void Apply<L, T>(this L me, Action<T> function) 
			where L : class, ILink<L, T>, new() 
        {
			if (!me.IsNull())
			{
				function(me.Head);
				me.Tail.Apply(function);
			}
        }
        public static R Map<L, T, R, S>(this L me, Func<T, S> function) 
			where L : class, ILink<L, T>, new() 
			where R : class, ILink<R, S>, new()
        {
			return me.IsNull() ? null : new R() 
			{ 
				Head = function(me.Head),
				Tail = me.Tail.Map<L, T, R, S>(function),
			};
        }
	}
}