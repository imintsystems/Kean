﻿// 
//  Output.cs
//  
//  Author:
//       Simon Mika <smika@hx.se>
//  
//  Copyright (c) 2013 Simon Mika
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

using System;

namespace Kean.Html.Dom
{
	public class Output :
		Element
	{
		protected override string TagName { get { return "output"; } }
		public string NameOfOutput { get; set; }
		public string FormIdentifier { get; set; }
		public string For { get; set; }
		#region Constructor
		public Output()
		{
			this.NoLineBreaks = true;
		}
		public Output(Node content) :
			this()
		{
			this.Add(content);
		}
		public Output(params Node[] nodes) :
			this()
		{
			this.Add(nodes);
		}
		#endregion
		protected override string FormatAttributes()
		{
			return
				base.FormatAttributes() +
				this.FormatAttribute("form", this.FormIdentifier) +
				this.FormatAttribute("name", this.NameOfOutput) +
				this.FormatAttribute("for", this.For);
		}
	}
}
