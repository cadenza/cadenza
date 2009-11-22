//
// StringTest.cs
//
// Author:
//   Jb Evain (jbevain@novell.com)
//   Jonathan Pryor  <jpryor@novell.com>
//   Bojan Rajkovic <bojanr@brandeis.edu>
//
// Copyright (c) 2007, 2008 Novell, Inc. (http://www.novell.com)
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using NUnit.Framework;

using Mono.Rocks;

namespace Mono.Rocks.Tests {

	[TestFixture]
	public class StringTest : BaseRocksFixture {

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void Lines_SelfNull ()
		{
			string s = null;
			s.Lines ();
		}

		[Test]
		public void Lines ()
		{
			var data = new string [4];
			var result = new [] { "", "one", "two", "three" };

			int i = 0;
			@"
			one
			two
			three".Lines ().ForEach (line => data [i++] = line.Trim ());

#if false
			foreach (string s in data)
				Console.WriteLine ("data: {0}", s);

			foreach (string s in result)
				Console.WriteLine ("result: {0}", s);
#endif

			AssertAreSame (result, data);

			#region Lines
			Assert.IsTrue (new[]{"one", "two", "three"}
					.SequenceEqual ("one\ntwo\nthree".Lines ()));
			#endregion
		}

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void Tokens_SelfNull ()
		{
			string s = null;
			s.Tokens ();
		}

		[Test]
		public void Tokens ()
		{
			#region Tokens
			string[] expected = {"(", "hello", ",", "world", "!)"};
			string[] actual = "(hello, world!)"
				.Tokens (
						(p, c) => char.IsLetterOrDigit (c), // words
						(p, c) => !char.IsWhiteSpace (c)    // non-space
				).ToArray ();
			AssertAreSame (expected, actual);
			#endregion
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void Words_SelfNull ()
		{
			string s = null;
			s.Words ();
		}

		[Test]
		public void Words ()
		{
			#region Words
			string[] expected = {"skip", "leading", "and", "trailing", "whitespace"};
			string[] actual = 
				"   skip  leading\r\n\tand trailing\vwhitespace   "
				.Words ().ToArray ();
			AssertAreSame (expected, actual);
			#endregion
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void Remove_SelfNull()
		{
			string s = null;
			s.Remove (String.Empty);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void Remove_ParamsNull()
		{
			String.Empty.Remove (null);
		}

		[Test]
		public void Remove()
		{
			Assert.AreEqual (" Bar ", "Foo Bar Monkeys".Remove ("Foo", "Monkeys"));
			Assert.AreEqual (" Bar ", "Foo Bar Foo".Remove ("Foo"));
		}

		[Test]
		public void Slice ()
		{
			#region Slice
			var data = "0123456789";

			Assert.AreEqual ("0",       data.Slice (0, 1));
			Assert.AreEqual ("89",      data.Slice (8, 10));
			Assert.AreEqual ("456789",  data.Slice (4, -1));
			Assert.AreEqual ("8",       data.Slice (8, -2));
			#endregion
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void Matches_SelfNull_NoOptions ()
		{
			string s = null;
			s.Matches (".*");
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void Matches_SelfNull_Options ()
		{
			string s = null;
			s.Matches (".*", RegexOptions.ECMAScript);
		}

		[Test]
		public void Matches_NoOptions ()
		{
			#region Matches
			string match = @"a - b
			c - d
			e - f";

			Match[] matches = match.Matches (@"\w+ - \w+").ToArray();
			Assert.AreEqual (3, matches.Length);
			Assert.AreEqual ("a - b", matches [0].Value);
			Assert.AreEqual ("c - d", matches [1].Value);
			Assert.AreEqual ("e - f", matches [2].Value);
			#endregion
		}

		[Test]
		public void Matches_Options ()
		{
			string match = @"a - b
			c - d
			e - f";

			Match[] matches = match.Matches (@"\w+ - \w+", RegexOptions.Compiled).ToArray();
			Assert.AreEqual (3, matches.Length);
			Assert.AreEqual ("a - b", matches [0].Value);
			Assert.AreEqual ("c - d", matches [1].Value);
			Assert.AreEqual ("e - f", matches [2].Value);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void MatchValues_SelfNull_NoOptions ()
		{
			string s = null;
			s.MatchValues (".*");
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void MatchValues_SelfNull_Options ()
		{
			string s = null;
			s.MatchValues (".*", RegexOptions.ECMAScript);
		}

		[Test]
		public void MatchValues_NoOptions ()
		{
			#region MatchValues
			string match = @"a - b
			c - d
			e - f";

			string[] expected = {"a - b", "c - d", "e - f"};
			string[] actual = match.MatchValues(@"\w+ - \w+").ToArray();

			AssertAreSame (expected, actual);
			#endregion
		}

		[Test]
		public void MatchValues_Options ()
		{
			string match = @"a - b
			c - d
			e - f";

			string[] expected = {"a - b", "c - d", "e - f"};
			string[] actual = match.MatchValues(@"\w+ - \w+", RegexOptions.Compiled).ToArray();

			AssertAreSame (expected, actual);
		}

		[Test]
		public void Captures_NoOptions ()
		{
			string match = "a - b - c - d - e";

			string[] expected = {"a", "b", "c", "d", "e"};
			string[] actual = match.Captures(@"(\w+)").ToArray();

			AssertAreSame (expected, actual);

			#region Captures
			Assert.IsTrue (
					new[]{"a", "b", "c", "d"}.SequenceEqual (
						"a - b - c - d".Captures (@"(\w+)")));
			#endregion
		}

		[Test]
		public void Captures_Options ()
		{
			string match = "a - b - c - d - e";
			
			string[] expected = {"a", "b", "c", "d", "e"};
			string[] actual = match.Captures(@"(\w+)", RegexOptions.IgnoreCase).ToArray();

			AssertAreSame (expected, actual);
		}

		[Test]
		public void CaptureNamedGroups_NoOptions ()
		{
			string match = "a5b6";

			List<KeyValuePair<string, string>> expectedValues = new List<KeyValuePair<string, string>> {
				new KeyValuePair<string, string> ("digita", "5"),
				new KeyValuePair<string, string> ("digitb", "6")
			};

			List<KeyValuePair<string, string>> returnedValues = new List<KeyValuePair<string, string>> ();

			ILookup<string, string> matches = match.CaptureNamedGroups (@"a(?'digita'[0-5])|b(?'digitb'[4-7])");

			foreach (IGrouping<string, string> group in matches)
				foreach (string s in group)
					returnedValues.Add (new KeyValuePair<string, string> (group.Key, s));

			AssertAreSame (returnedValues, expectedValues);

			#region CaptureNamedGroups
			Assert.AreEqual ("flag=--; name=foo; value=bar",
					"--foo=bar"
					.CaptureNamedGroups (@"^(?<flag>--|-|/)(?<name>[^:=]+)((?<sep>[:=])(?<value>.*))?$")
					.With (r => "flag=" + r ["flag"].Implode () + 
						"; name=" + r ["name"].Implode () +
						"; value=" + r ["value"].Implode ()));
			#endregion
		}

		[Test]
		public void CaptureNamedGroups_Options ()
		{
			string match = "A5B6";

			List<KeyValuePair<string, string>> expectedValues = new List<KeyValuePair<string, string>> {
				new KeyValuePair<string, string> ("digita", "5"),
				new KeyValuePair<string, string> ("digitb", "6")
			};

			List<KeyValuePair<string, string>> returnedValues = new List<KeyValuePair<string, string>> ();

			ILookup<string, string> matches = match.CaptureNamedGroups (@"a(?'digita'[0-5])|b(?'digitb'[4-7])", RegexOptions.IgnoreCase);

			foreach (IGrouping<string, string> group in matches)
				foreach (string s in group)
					returnedValues.Add (new KeyValuePair<string, string> (group.Key, s));

			AssertAreSame (returnedValues, expectedValues);
		}

		enum Foo {
			Bar,
			Baz,
			Gazonk
		}

		[Test]
		public void ToEnum ()
		{
			#region ToEnum
			Assert.AreEqual (Foo.Gazonk, "Gazonk".ToEnum<Foo> ());
			Assert.AreEqual (Foo.Bar,    "Bar".ToEnum<Foo> ());
			#endregion
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
