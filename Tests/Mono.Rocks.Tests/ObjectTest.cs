//
// ObjectTest.cs
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
using System.Linq;

using NUnit.Framework;

using Mono.Rocks;

namespace Mono.Rocks.Tests {

	[TestFixture]
	public class ObjectTest : BaseRocksFixture {

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Match_MatchersNull ()
		{
			Func<string, Maybe<int>>[] m = null;
			"foo".Match (m);
		}

		[Test, ExpectedException (typeof (InvalidOperationException))]
		public void Match_NoMatch ()
		{
			var m = new Func<string, Maybe<int>>[] {
				v => Maybe.When (v == "bar", 42),
				v => Maybe.When (v.Length == 5, 54),
			};
			"foo".Match (m);
		}

		[Test]
		public void Match ()
		{
			#region Match
			Assert.AreEqual ("foo",
				"foo".Match (
					s => Maybe.When (s.Length != 3, "bar!"),
					s => s.Just ()));
			Assert.AreEqual ("bar!",
				5.Match (
					v => Maybe.When (v != 3, "bar!"),
					v => v.ToString ().Just()));
			var m = new Func<string, Maybe<int>>[] {
				v => Maybe.When (v == "bar",    1),
				v => Maybe.When (v.Length == 5, 2),
				v => (-1).Just (),
			};
			Assert.AreEqual (1, "bar".Match (m));
			Assert.AreEqual (2, "12345".Match (m));
			Assert.AreEqual (-1, "*default*".Match (m));
			#endregion
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Just_SelfNull ()
		{
			string        s = null;
			Maybe<string> r = s.Just ();
		}

		[Test]
		public void Just ()
		{
			Maybe<string> r = "foo".Just ();
			Assert.AreEqual (typeof(Maybe<string>), r.GetType ());
			Assert.IsTrue (r.HasValue);
			Assert.AreEqual ("foo", r.ToString());
			Assert.AreEqual ("foo", r.Value);
		}

		[Test]
		public void ToMaybe ()
		{
			string        s = null;
			Maybe<string> r = s.ToMaybe ();
			Assert.AreEqual (Maybe<string>.Nothing, r);
			Assert.IsFalse (r.HasValue);

			s = "foo";
			r = s.ToMaybe ();
			Assert.AreEqual ("foo".Just (), r);
			Assert.IsTrue (r.HasValue);
			Assert.AreEqual ("foo", r.Value);
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void With_SelectorNull ()
		{
			string                s = "foo";
			Func<string, string>  f = null;
			s.With (f);
		}

		[Test]
		public void With ()
		{
			#region With
			// sorts the array, then returns the 
			// element in the middle of the array.
			Assert.AreEqual (3,
				new[]{5, 4, 3, 2, 1}.Sort ()
				.With (c => c.ElementAt (c.Count()/2)));
			#endregion
		}
	}
}
