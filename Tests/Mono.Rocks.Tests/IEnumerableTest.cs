﻿//
// IEnumerableTest.cs
//
// Author:
//   Jb Evain (jbevain@novell.com)
//
// Copyright (c) 2007 Novell, Inc. (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.IO;

using NUnit.Framework;

using Mono.Rocks;

namespace Mono.Rocks.Tests {

	[TestFixture]
	public class IEnumerableTest : BaseRocksFixture {

		[Test]
		public void Join ()
		{
			var data = new [] { 0, 1, 2, 3, 4, 5 };
			var result = "0, 1, 2, 3, 4, 5";

			Assert.AreEqual (result, data.Join (", "));
		}

		[Test]
		public void JoinEmpty ()
		{
			var data = new int [] {};

			Assert.AreEqual (string.Empty, data.Join ());
		}

		[Test]
		public void Repeat ()
		{
			Assert.AreEqual ("foofoofoo", new [] {"foo"}.Repeat (3).Join ());
			Assert.AreEqual ("foobarfoobar", new [] {"foo", "bar"}.Repeat (2).Join ());
		}

		[Test]
		public void PathCombine ()
		{
			var data = new [] {"a", "b", "c"};
			var result = string.Format ("a{0}b{0}c", Path.DirectorySeparatorChar);
			Assert.AreEqual (result, data.PathCombine ());
		}
	}
}
