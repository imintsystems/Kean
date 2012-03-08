﻿// 
//  Path.cs
//  
//  Author:
//       Simon Mika <smika@hx.se>
//  
//  Copyright (c) 2010-2011 Simon Mika
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
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Target = Kean.Core.Uri;

namespace Kean.Core.Uri.Test
{
    [TestFixture]
    public class Path :
        Kean.Test.Fixture<Path>
    {
        string prefix = "Kean.Core.Uri.Test.Path.";
        protected override void Run()
        {
            this.Run(
                this.EqualityNull,
                this.Equality,
				this.SpacePlus,
				this.SpaceSpace
                );
        }
        [Test]
        public void EqualityNull()
        {
            Target.Path path = null;
            Expect(path, Is.EqualTo(null), this.prefix + "EqualityNull.0");
            Expect(path == null, "path == null", this.prefix + "EqualityNull.1");
        }
        [Test]
        public void Equality()
        {
            Target.Path path = "folderA/folderB/file.extension";
            Expect(path, Is.Not.EqualTo(null), this.prefix + "Equality.0");
            Expect(path != null, "path != null", this.prefix + "Equality.1");
            Expect((string)path, Is.EqualTo("folderA/folderB/file.extension"), this.prefix + "Equality.2");
            Expect(path == "folderA/folderB/file.extension", "path == \"folderA/folderB/file.extension\"", this.prefix + "Equality.3");
            Expect(path.Head, Is.EqualTo("folderA"), this.prefix + "Equality.4");
            Expect((string)path.Tail, Is.EqualTo("folderB/file.extension"), this.prefix + "Equality.5");
            Expect(path.Tail == "folderB/file.extension", "path.Tail == \"folderB/file.extension\"", this.prefix + "Equality.6");
            Expect(path.Tail.Head, Is.EqualTo("folderB"), this.prefix + "Equality.7");
            Expect((string)path.Tail.Tail, Is.EqualTo("file.extension"), this.prefix + "Equality.8");
            Expect(path.Tail.Tail.Tail, Is.EqualTo(null), this.prefix + "Equality.9");
        }
		[Test]
		public void SpacePlus()
		{
			Target.Path path = "folder+A/folder+B/file.extension";
			Expect(path, Is.Not.EqualTo(null), this.prefix + "SpacePlus.0");
			Expect(path != null, "path != null", this.prefix + "SpacePlus.1");
			Expect((string)path, Is.EqualTo("folder A/folder B/file.extension"), this.prefix + "SpacePlus.2");
			Expect(path == "folder+A/folder+B/file.extension", "path == \"folderA/folderB/file.extension\"", this.prefix + "SpacePlus.3");
			Expect(path.Head, Is.EqualTo("folder A"), this.prefix + "SpacePlus.4");
			Expect((string)path.Tail, Is.EqualTo("folder B/file.extension"), this.prefix + "SpacePlus.5");
			Expect(path.Tail == "folder B/file.extension", "path.Tail == \"folder B/file.extension\"", this.prefix + "SpacePlus.6");
			Expect(path.Tail.Head, Is.EqualTo("folder B"), this.prefix + "SpacePlus.7");
			Expect((string)path.Tail.Tail, Is.EqualTo("file.extension"), this.prefix + "SpacePlus.8");
			Expect(path.Tail.Tail.Tail, Is.EqualTo(null), this.prefix + "SpacePlus.9");
		}
		[Test]
		public void SpaceSpace()
		{
			Target.Path path = "folder A/folder B/file.extension";
			Expect(path, Is.Not.EqualTo(null), this.prefix + "SpaceSpace.0");
			Expect(path != null, "path != null", this.prefix + "SpaceSpace.1");
			Expect((string)path, Is.EqualTo("folder A/folder B/file.extension"), this.prefix + "SpaceSpace.2");
			Expect(path == "folder+A/folder+B/file.extension", "path == \"folderA/folderB/file.extension\"", this.prefix + "SpaceSpace.3");
			Expect(path.Head, Is.EqualTo("folder A"), this.prefix + "SpaceSpace.4");
			Expect((string)path.Tail, Is.EqualTo("folder B/file.extension"), this.prefix + "SpaceSpace.5");
			Expect(path.Tail == "folder B/file.extension", "path.Tail == \"folder B/file.extension\"", this.prefix + "SpaceSpace.6");
			Expect(path.Tail.Head, Is.EqualTo("folder B"), this.prefix + "SpaceSpace.7");
			Expect((string)path.Tail.Tail, Is.EqualTo("file.extension"), this.prefix + "SpaceSpace.8");
			Expect(path.Tail.Tail.Tail, Is.EqualTo(null), this.prefix + "SpaceSpace.9");
		}
	}
}