//
// JaggedArrayTest.cs
//
// Author:
//   Jonathan Pryor
//
// Copyright (c) 2008 Novell, Inc. (http://www.novell.com)
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
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using Mono.Rocks;

// "The variable `r' is assigned but it's value is never used."
// It's value isn't supposed to be used; it's purpose is as a manual check the
// the generated .Curry() methods generate the correct return type.
#pragma warning disable 0219

namespace Mono.Rocks.Tests {

	[TestFixture]
	public class JaggedArrayTest : BaseRocksFixture {

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Rows_SelfNull ()
		{
			int[][] s = null;
			IEnumerable<IEnumerable<int>> r = s.Rows ();
		}

		[Test]
		public void Rows ()
		{
			int[][] s = new []{
				new[]{1, 2, 3},
				new[]{4, 5, 6},
				new[]{7, 8, 9},
			};
			IEnumerable<IEnumerable<int>> r = s.Rows();
			Assert.AreEqual (3, r.Count ());

			Assert.IsTrue (new[]{1,2,3}.SequenceEqual (r.ElementAt (0)));
			Assert.IsTrue (new[]{4,5,6}.SequenceEqual (r.ElementAt (1)));
			Assert.IsTrue (new[]{7,8,9}.SequenceEqual (r.ElementAt (2)));
		}
	}
}
