﻿// 
//  Editor.cs
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
using Kean.Core;
using Kean.Core.Extension;
using Collection = Kean.Core.Collection;
using Kean.Core.Collection.Extension;
using IO = Kean.IO;

namespace Kean.Cli.LineBuffer
{
	public class Editor :
		IDisposable
	{
		object @lock = new object();
		bool executing;
		ITerminal terminal;
		History history;
		bool help = false;
		bool exit = false;
		int oldmessage = 0;
		public Func<string, bool> Execute { get; set; }
		public Func<string, string> Complete { get; set; }
		public Func<string, string> Help { get; set; }
		public Func<string, string> Error { get; set; }
		public Func<string, bool> RequestType { get; set; }
		public string Prompt { get; set; }
		public Editor(ITerminal terminal)
		{
			this.terminal = terminal;
			this.terminal.Command += this.OnCommand;
			this.history = new History(c => { if (this.terminal.Echo) this.terminal.Out.Write(c); });
		}
		void OnCommand(EditCommand command)
		{
			switch (command)
			{
				case EditCommand.None:
					break;
				case EditCommand.Home:
					this.history.Current.MoveCursorHome();
					break;
				case EditCommand.LeftArrow:
					this.history.Current.MoveCursorLeft();
					break;
				case EditCommand.Copy:
					break;
				case EditCommand.Exit:
					this.exit = true;
					this.Close();
					break;
				case EditCommand.End:
					this.history.Current.MoveCursorEnd();
					break;
				case EditCommand.RightArrow:
					this.history.Current.MoveCursorRight();
					break;
				// 7
				case EditCommand.Backspace:
					this.history.Current.DeletePreviousCharacter();
					break;
				case EditCommand.Tab: // Tab
					if ((this.help || this.Complete.IsNull()) && this.Help.NotNull())
					{
						this.terminal.Out.WriteLine();
						this.terminal.Out.Write(this.Help(this.history.Current.ToString()));
						this.terminal.Out.Write(this.Prompt);
						this.history.Current.Write();
						this.help = false;
					}
					else if (this.Complete.NotNull())
					{
						string old = this.history.Current.ToString();
						string result = this.Complete(old);
						if (result == old)
							this.help = true;
						else
							this.history.Current.Renew(result);
					}
					break;
				case EditCommand.Quit:
				case EditCommand.Enter:
					{
						lock (this.@lock)
							this.executing = true;
						if (this.terminal.Echo)
							this.terminal.Out.WriteLine();

						string line = this.history.Current.ToString();
						if (line.StartsWith("?"))
						{
							if (this.RequestType.NotNull())
								this.RequestType(line.Substring(1));
						}
						else if (this.Execute.NotNull())
								this.Execute(line);

						if (this.history.Current.ToString().NotEmpty())
							this.history.Add();
						else
							this.history.ClearCurrent();
						this.terminal.Out.Write(this.Prompt);
						
						lock (this.@lock)
							this.executing = false;
					}
					break;
				case EditCommand.DownArrow:

					if (!this.history.Empty)
						this.history.Next();
					break;
				case EditCommand.UpArrow:
					if (!this.history.Empty)
						this.history.Previous();
					break;
				case EditCommand.Paste:
					break;
				case EditCommand.Cut:
					break;
				case EditCommand.Redo:
					break;
				case EditCommand.Undo:
					break;
				case EditCommand.Delete:
					this.history.Current.DeleteCurrentCharacter();
					break;
			}
		}
		public void Read()
		{
			this.terminal.Out.Write(this.Prompt);
			while (this.terminal.In.Next() && !this.exit)
			{
				this.history.Current.Insert(this.terminal.In.Last);
				this.help = false;
			}
		}
		public void WriteLine(string value)
		{
			lock (this.@lock)
			{
                if (this.executing)
                {
                    this.terminal.Out.WriteLine(value);
                    this.oldmessage = value.Length;
                }
                else
                {
                    //this.Remove(-this.oldmessage - this.Prompt.Length - this.history.Current.ToString().Length);
                    this.Remove(-this.Prompt.Length);
                    this.terminal.Out.WriteLine(value);
                    this.oldmessage = value.Length;
                    this.terminal.Out.Write(this.Prompt);
                    this.history.Current.Write();
                }
			}
		}
		void Remove(int steps)
		{
			if (steps < 0)
				while (steps++ != 0)
					this.terminal.Out.Write((char)8 + " " + (char)8);
			else
				while (steps-- != 0)
					this.terminal.Out.Write(" ");
		}

		public void Close()
		{
			lock (this.@lock)
				if (this.terminal.NotNull())
				{
					this.terminal.Close();
					this.terminal = null;
				}
		}

		#region IDisposable Members
		void IDisposable.Dispose()
		{
			this.Close();
		}
		#endregion
	}
}
