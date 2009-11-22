//
// TuplesTest.cs
//
// Author:
//   Jonathan Pryor  <jpryor@novell.com>
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

namespace Mono.Rocks.Tests {

	[TestFixture]
	public class Tuple2EquatableContract : EquatableContract<Tuple<char, int>>
	{
		protected override Tuple<char, int> CreateValueX ()
		{
			return Tuple.Create ('x', 1);
		}

		protected override Tuple<char, int> CreateValueY ()
		{
			return Tuple.Create ('y', 2);
		}

		protected override Tuple<char, int> CreateValueZ ()
		{
			return Tuple.Create ('z', 3);
		}
	}

	[TestFixture]
	public class TuplesTest : BaseRocksFixture {

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void ToTuple_ValuesNull ()
		{
			IEnumerable<object> v = null;
			v.ToTuple ();
		}

		[Test]
		[ExpectedException (typeof (NotSupportedException))]
		public void ToTuple_TooManyValues ()
		{
			Enumerable.Range (0, Tuple.MaxValues+1).ToTuple ();
		}

		[Test]
		public void ToTuple ()
		{
			var a = Tuple.Create (1U, 2L, '\x3', (byte) 4);
			Assert.AreEqual (true,
					a.Equals (new object[]{1U, 2L, '\x3', (byte) 4}.ToTuple ()));
			Assert.AreEqual (a,
					a.AsEnumerable ().ToTuple ());
		}

		[Test]
		public void ToKeyValuePair ()
		{
			var t = Tuple.Create (42, "42");
			var k = t.ToKeyValuePair ();
			Assert.AreEqual (t._1,  k.Key);
			Assert.AreEqual (t._2,  k.Value);
		}
	}
}
