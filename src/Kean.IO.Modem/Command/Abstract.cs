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
using Collection = Kean.Core.Collection;
using Kean.Core.Collection.Extension;
using Kean.Core.Basis.Extension;

namespace Kean.IO.Modem.Command
{
	public class Abstract
	{
		public string Name { get; private set; }
		public bool Succeded { get; private set; }

		protected abstract string Command { get; }

		internal Abstract(string name)
		{
			this.Name = name;
		}
		internal void Run(Serial.IPort port)
		{
			this.port.WriteLine("AT" + this.Command);
			string response = this.port.Read();
			this.Succeded = response.EndsWith("\r\nOK\r\n") && this.Parse(response.Remove(response.Length - 7, 6).Trim());
		}
		protected abstract bool Parse(string response);
	}
}

