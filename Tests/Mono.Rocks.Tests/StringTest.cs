//
// StringTest.cs
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

using NUnit.Framework;

using Mono.Rocks;

namespace Mono.Rocks.Tests {

	[TestFixture]
	public class StringTest : BaseRocksFixture {

		[Test]
		public void EachLine ()
		{
			var data = new string [3];
			var result = new [] { "one", "two", "three" };

			int i = 0;
			@"
			one
			two
			three".EachLine (line => data [i++] = line.Trim ());

			foreach (string s in data)
				Console.WriteLine ("data: {0}", s);

			foreach (string s in result)
				Console.WriteLine ("result: {0}", s);

			AssertAreSame (result, data);
		}

		[Test]
		public void Slice ()
		{
			var data = "0123456789";

			Assert.AreEqual ("0", data.Slice (0, 1));
			Assert.AreEqual ("89", data.Slice (8, 10));
			Assert.AreEqual ("456789", data.Slice (4, -1));
			Assert.AreEqual ("8", data.Slice (8, -2));
		}

		enum Foo {
			Bar,
			Baz,
			Gazonk
		}

		[Test]
		public void ToEnum ()
		{
			Assert.AreEqual (Foo.Gazonk, "Gazonk".ToEnum<Foo> ());
			Assert.AreEqual (Foo.Bar, "Bar".ToEnum<Foo> ());
		}

		[Test]
		[ExpectedException (typeof (ArgumentException))]
		public void ToEnumNotInEnum ()
		{
			"Gens".ToEnum<Foo> ();
		}

		[Test]
		[ExpectedException (typeof (ArgumentException))]
		public void ToEnumNotEnum ()
		{
			"Bar".ToEnum<string> ();
		}
	}
}
