//
// MutableLookupTest.cs
//
// Author:
//   Eric Maupin  <me@ermau.com>
//
// Copyright (c) 2009 Eric Maupin (http://www.ermau.com)
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
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;

namespace Mono.Rocks.Tests
{
	[TestFixture]
	public class MutableLookupTest
	{
		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void CtorNull()
		{
			new MutableLookup<string, string> (null);
		}

		[Test]
		public void CtorILookup()
		{
			List<int> ints = new List<int> { 10, 20, 21, 30 };
			var lookup = new MutableLookup <int, int> (ints.ToLookup (i => Int32.Parse (i.ToString()[0].ToString())));

			Assert.AreEqual (3, lookup.Count);
			Assert.AreEqual (1, lookup[1].Count());
			Assert.AreEqual (2, lookup[2].Count());
			Assert.AreEqual (1, lookup[3].Count());
		}

		[Test]
		public void CtorILookupWithNulls()
		{
			List<string> strs = new List<string> { "Foo", "Foos", "Foobar", "Monkeys", "Bar", "Ban", "Barfoo" };

			var lookup = new MutableLookup<string, string> (strs.ToLookup (s => (s[0] != 'F' && s[0] != 'B') ? null : s[0].ToString()));
			Assert.AreEqual (3, lookup.Count);
			Assert.AreEqual (3, lookup["F"].Count());
			Assert.AreEqual (3, lookup["B"].Count());
			Assert.AreEqual (1, lookup[null].Count());
		}

		[Test]
		public void Add()
		{
			var lookup = new MutableLookup<string, string>();
			lookup.Add ("F", "Foo");
			Assert.AreEqual (1, lookup.Count);
			Assert.AreEqual ("Foo", lookup["F"].First());
			lookup.Add ("F", "Foobar");
			Assert.AreEqual (1, lookup.Count);
			Assert.AreEqual (2, lookup["F"].Count());
		}

		[Test]
		public void AddNull()
		{
			var lookup = new MutableLookup<string, string>();
			lookup.Add (null, "Foo");
			Assert.AreEqual (1, lookup.Count);
			Assert.AreEqual (1, lookup[null].Count());
			lookup.Add (null, null);
			Assert.AreEqual (1, lookup.Count);
			Assert.AreEqual (2, lookup[null].Count());
		}

		[Test]
		public void CountRefType()
		{
			var lookup = new MutableLookup<string, string>();
			Assert.AreEqual (0, lookup.Count);
			lookup.Add (null, "blah");
			Assert.AreEqual (1, lookup.Count);
			lookup.Add ("F", "Foo");
			Assert.AreEqual (2, lookup.Count);
			lookup.Add ("F", "Foobar");
			Assert.AreEqual (2, lookup.Count);

			lookup.Remove (null, "blah");
			Assert.AreEqual (1, lookup.Count);
		}

		[Test]
		public void CountValueType()
		{
			var lookup = new MutableLookup<int, int>();
			Assert.AreEqual (0, lookup.Count);
			lookup.Add (1, 10);
			Assert.AreEqual (1, lookup.Count);
			lookup.Add (2, 20);
			Assert.AreEqual (2, lookup.Count);
			lookup.Add (2, 21);
			Assert.AreEqual (2, lookup.Count);

			lookup.Remove (1, 10);
			Assert.AreEqual(1, lookup.Count);
		}

		[Test]
		public void RemoveExisting()
		{
			var lookup = new MutableLookup<string, string>();
			lookup.Add (null, "blah");
			lookup.Add (null, "monkeys");
			lookup.Add ("F", "Foo");
			lookup.Add ("F", "Foobar");
			lookup.Add ("B", "Bar");

			Assert.AreEqual (3, lookup.Count);

			Assert.IsTrue (lookup.Remove (null, "blah"));
			Assert.AreEqual (3, lookup.Count);
			Assert.IsTrue (lookup.Remove (null, "monkeys"));
			Assert.AreEqual (2, lookup.Count);

			Assert.IsTrue (lookup.Remove ("F"));
			Assert.AreEqual (1, lookup.Count);
		}

		[Test]
		public void RemoveNonExisting()
		{
			var lookup = new MutableLookup<string, string>();
			lookup.Add(null, "blah");
			lookup.Add(null, "monkeys");
			lookup.Add("F", "Foo");
			lookup.Add("F", "Foobar");
			lookup.Add("B", "Bar");

			Assert.IsFalse (lookup.Remove ("D"));
			Assert.AreEqual(3, lookup.Count);

			Assert.IsFalse (lookup.Remove ("F", "asdf"));
			Assert.AreEqual(3, lookup.Count);

			lookup.Remove (null);
			Assert.IsFalse (lookup.Remove (null));
			Assert.AreEqual (2, lookup.Count);
		}

		[Test]
		public void ClearWithNull()
		{
			var lookup = new MutableLookup<string, string>();
			lookup.Add (null, "blah");
			lookup.Add ("F", "Foo");

			lookup.Clear();

			Assert.AreEqual (0, lookup.Count);
			Assert.IsFalse (lookup.Contains (null));
			Assert.IsFalse (lookup.Contains ("F"));
		}

		[Test]
		public void ClearWithoutNull()
		{
			var lookup = new MutableLookup<string, string>();
			lookup.Add("F", "Foo");
			lookup.Add("F", "Foobar");
			lookup.Add("B", "Bar");

			lookup.Clear();
			Assert.AreEqual (0, lookup.Count);
			Assert.IsFalse (lookup.Contains ("F"));
			Assert.IsFalse (lookup.Contains ("B"));
		}

		[Test]
		public void ClearValueType()
		{
			var lookup = new MutableLookup<int, int>();
			lookup.Add (1, 10);
			lookup.Add (1, 12);
			lookup.Add (1, 13);
			lookup.Add (2, 21);
			lookup.Add (2, 22);
			lookup.Add (2, 23);

			lookup.Clear();
			Assert.AreEqual (0, lookup.Count);
			Assert.IsFalse (lookup.Contains (1));
			Assert.IsFalse (lookup.Contains (2));
		}

		[Test]
		public void Contains()
		{
			var lookup = new MutableLookup<string, string>();
			lookup.Add(null, "blah");
			lookup.Add(null, "monkeys");
			lookup.Add("F", "Foo");
			lookup.Add("F", "Foobar");
			lookup.Add("B", "Bar");

			Assert.IsTrue (lookup.Contains ("B"));
		}

		[Test]
		public void DoesNotContain()
		{
			var lookup = new MutableLookup<string, string>();
			lookup.Add(null, "blah");
			lookup.Add(null, "monkeys");
			lookup.Add("F", "Foo");
			lookup.Add("F", "Foobar");
			lookup.Add("B", "Bar");

			Assert.IsFalse (lookup.Contains ("D"));
		}

		[Test]
		public void ContainsNull()
		{
			var lookup = new MutableLookup<string, string>();
			lookup.Add(null, "blah");
			lookup.Add(null, "monkeys");
			lookup.Add("F", "Foo");
			lookup.Add("F", "Foobar");
			lookup.Add("B", "Bar");

			Assert.IsTrue (lookup.Contains (null));
		}

		[Test]
		public void DoesNotContainNull()
		{
			var lookup = new MutableLookup<string, string>();
			lookup.Add("F", "Foo");
			lookup.Add("F", "Foobar");
			lookup.Add("B", "Bar");

			Assert.IsFalse (lookup.Contains (null));
		}

		[Test]
		public void Indexer()
		{
			var lookup = new MutableLookup<string, string>();
			lookup.Add(null, "blah");
			lookup.Add(null, "monkeys");
			lookup.Add("F", "Foo");
			lookup.Add("F", "Foobar");
			lookup.Add("B", "Bar");

			Assert.AreEqual (2, lookup["F"].Count());
		}

		[Test]
		public void IndexerNull()
		{
			var lookup = new MutableLookup<string, string>();
			lookup.Add(null, "blah");
			lookup.Add(null, "monkeys");
			lookup.Add("F", "Foo");
			lookup.Add("F", "Foobar");
			lookup.Add("B", "Bar");

			Assert.AreEqual (2, lookup[null].Count());
		}

		[Test]
		public void IndexerNotFound()
		{
			var lookup = new MutableLookup<string, string>();
			lookup.Add (null, "blah");
			lookup.Add (null, "monkeys");
			lookup.Add ("F", "Foo");
			lookup.Add ("F", "Foobar");
			lookup.Add ("B", "Bar");

			Assert.AreEqual (0, lookup["D"].Count());
		}

		[Test]
		public void Enumerator()
		{
			List<int> ints = new List<int> { 10, 20, 21, 30 };
			var lookup = new MutableLookup<int, int>(ints.ToLookup(i => Int32.Parse(i.ToString()[0].ToString())));

			Assert.AreEqual (3, lookup.Count());
			Assert.IsTrue (lookup.Any (g => g.Key == 1));
			Assert.IsTrue (lookup.Any (g => g.Key == 2));
			Assert.IsTrue (lookup.Any (g => g.Key == 3));
		}

		[Test]
		public void EnumeratorNull()
		{
			var lookup = new MutableLookup<string, string>();
			lookup.Add (null, "blah");
			lookup.Add (null, "monkeys");
			lookup.Add ("F", "Foo");
			lookup.Add ("F", "Foobar");
			lookup.Add ("B", "Bar");

			Assert.AreEqual (3, lookup.Count());
			Assert.IsTrue (lookup.Any (g => g.Key == null));
			Assert.IsTrue (lookup.Any (g => g.Key == "F"));
			Assert.IsTrue (lookup.Any (g => g.Key == "B"));
		}

		[Test]
		public void NullGroupingEnumerator()
		{
			var lookup = new MutableLookup<string, string>();
			lookup.Add (null, null);
			lookup.Add (null, "blah");
			lookup.Add (null, "monkeys");
			lookup.Add ("F", "Foo");
			lookup.Add ("F", "Foobar");
			lookup.Add ("B", "Bar");

			Assert.AreEqual (3, lookup[null].Count());
			Assert.IsTrue (lookup[null].Any (s => s == "blah"));
			Assert.IsTrue (lookup[null].Any (s => s == "monkeys"));
			Assert.IsTrue (lookup[null].Any (s => s == null));
		}

		[Test]
		public void GroupingEnumerator()
		{
			List<int> ints = new List<int> { 10, 20, 21, 30 };
			var lookup = new MutableLookup<int, int>(ints.ToLookup(i => Int32.Parse(i.ToString()[0].ToString())));

			Assert.AreEqual (2, lookup[2].Count());
			Assert.IsTrue (lookup[2].Any (i => i == 20));
			Assert.IsTrue (lookup[2].Any(i => i == 21));
		}
	}
}