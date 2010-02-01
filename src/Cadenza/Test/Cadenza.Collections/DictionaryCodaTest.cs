//
// IEnumerableTest.cs
//
// Author:
//   Jonathan Pryor (jpryor@novell.com)
//
// Copyright (c) 2010 Novell, Inc. (http://www.novell.com)
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
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Cadenza.Collections;
using Cadenza.Tests;

namespace Cadenza.Collections.Tests {

	[TestFixture]
	public class DictionaryCodaTest : BaseRocksFixture {

		[Test]
		public void GetValueOrDefault_Self ()
		{
			IDictionary<string, int> s = null;
			Assert.Throws<ArgumentNullException>(() => s.GetValueOrDefault ("foo"));
			Assert.Throws<ArgumentNullException>(() => s.GetValueOrDefault ("foo", 42));
		}

		[Test]
		public void GetValueOrDefault ()
		{
			#region GetValueOrDefault
			var dict = new Dictionary<string, int> () {
				{ "a", 1 },
			};
			Assert.AreEqual (1, dict.GetValueOrDefault ("a"));
			Assert.AreEqual (0, dict.GetValueOrDefault ("c"));
			Assert.AreEqual (3, dict.GetValueOrDefault ("c", 3));
			#endregion
		}

		[Test]
		public void UpdateValue_Arguments ()
		{
			IDictionary<string, int> s = null;
			Assert.Throws<ArgumentNullException>(() => s.UpdateValue ("key", v => v));
			s = new Dictionary<string, int> ();
			Assert.Throws<ArgumentNullException>(() => s.UpdateValue ("key", null));
		}

		[Test]
		public void UpdateValue ()
		{
			#region UpdateValue
			var words = new[]{
				"Count",
				"the",
				"the",
				"repeated",
				"words",
			};
			var wordCounts = new Dictionary<string, int> ();
			foreach (var word in words)
				wordCounts.UpdateValue (word, v => v + 1);
			Assert.AreEqual (4, wordCounts.Count);
			Assert.AreEqual (1, wordCounts ["Count"]);
			Assert.AreEqual (2, wordCounts ["the"]);
			Assert.AreEqual (1, wordCounts ["repeated"]);
			Assert.AreEqual (1, wordCounts ["words"]);
			#endregion
		}
	}
}
