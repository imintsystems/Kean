﻿using System;
using Collection = Kean.Core.Collection;
using Kean.Core.Extension;
using Kean.Core.Collection.Extension;

namespace Kean.IO.Text
{
	public class WriteBuffer :
		Abstract.CharacterWriter
	{
		System.Timers.Timer timer;
		Collection.IQueue<System.Collections.Generic.IEnumerable<char>> queue; 

		public override Core.Uri.Locator Resource { get { return "console:///"; } }
		public override bool Opened { get { return this.timer.NotNull(); } }
		
		WriteBuffer()
		{
			this.queue = new Collection.Synchronized.Queue<System.Collections.Generic.IEnumerable<char>>();
			this.timer = new System.Timers.Timer();
			this.timer.Interval = 500;
			this.timer.Elapsed += (sender, e) => this.Flush();
			this.timer.Enabled = true;
			this.timer.Start();
		}
		void Flush()
		{
			Collection.List<char> accumulator = new Collection.List<char>();
			while (!this.queue.Empty)
				foreach (char c in this.queue.Dequeue())
					accumulator.Add(c);
			System.Diagnostics.Debug.Write(new string(accumulator.ToArray()));
		}
		public override bool Close()
		{
			bool result;
			if (result = this.timer.NotNull())
			{
				this.timer.Dispose();
				this.timer = null;
				this.Flush();
			}
			return result;
		}

		public override bool Write(System.Collections.Generic.IEnumerable<char> buffer)
		{
			this.queue.Enqueue(buffer);
			return true;
		}
		public static WriteBuffer Create()
		{
			return new WriteBuffer();
		}
	}
}
