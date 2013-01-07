//
//  String.cs
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
using Kean.Core;
using Kean.Core.Extension;
using Uri = Kean.Core.Uri;

namespace Kean.Json.Dom
{
	public class String :
		Primitive<string>,
		IEquatable<String>
	{
		public String (string value) :
			base(value)
		{ }
		public String (string value, Uri.Region region) :
			base(value, region)
		{ }
		#region IEquatable implementation
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}
		public override bool Equals(object other)
		{
			return other is String && this.Equals(other as String);
		}
        public override bool Equals(Item other)
        {
            return this.Equals(other as String);
        }
        public bool Equals(String other)
		{
			return other.NotNull() && this.Value == other.Value;
		}
		public static bool operator ==(String left, String right)
		{
			return left.Same(right) || left.NotNull() && left.Equals(right);
		}
		public static bool operator !=(String left, String right)
		{
			return !left.Same(right) || left.IsNull() || !left.Equals(right);
		}
		#endregion
		#region Casts
		public static implicit operator string(String item)
		{
			return item.NotNull() ? item.Value : null;
		}
		public static implicit operator String(string value)
		{
			return new String(value);
		}
		#endregion
	}
}
