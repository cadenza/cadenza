//
// String.cs
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mono.Rocks {

	public static class StringRocks {

		public static TEnum ToEnum<TEnum> (this string self)
		{
			Check.Self (self);

			return (TEnum) Enum.Parse (typeof (TEnum), self);
		}

		public static IEnumerable<string> Lines (this string self)
		{
			Check.Self (self);

			return new StringReader (self).Lines ();
		}

		public static IEnumerable<string> Tokens (this string self, params Func<char?, char, bool>[] categories)
		{
			Check.Self (self);

			return new StringReader (self).Tokens (categories);
		}

		public static IEnumerable<string> Words (this string self)
		{
			Check.Self (self);

			return new StringReader (self).Words ();
		}

		public static IEnumerable<Match> Matches (this string self, string regex)
		{
			return Matches (self, regex, RegexOptions.None);
		}

		public static IEnumerable<Match> Matches (this string self, string regex, RegexOptions options)
		{
			Check.Self (self);

			return new Regex (regex, options).Matches (self).Cast<Match> ();
		}

		public static IEnumerable<string> MatchValues (this string self, string regex, RegexOptions options)
		{
			Check.Self (self);

			return Matches (self, regex, options)
				.Select (m => m.Value);
		}

		public static IEnumerable<string> MatchValues (this string self, string regex)
		{
			Check.Self (self);

			return MatchValues (self, regex, RegexOptions.None);
		}

		public static IEnumerable<string> Captures (this string self, string regex, RegexOptions options)
		{
			Check.Self (self);

			return Matches (self, regex, options)
				.SelectMany (m => m.Groups.Cast<Group> ().Skip (1))
				.Select (g => g.Value);
		}

		public static IEnumerable<string> Captures (this string self, string regex)
		{
			Check.Self (self);

			return Captures (self, regex, RegexOptions.None);
		}

		private static IEnumerable<KeyValuePair<string, string>> CreateCaptureNamedGroupsIterator (this string self, string regex, RegexOptions options)
		{
			Regex r = new Regex (regex, options);
			foreach (Match match in r.Matches (self)) {
				for (int i = 1; i < match.Groups.Count; i++) {
					Group group = match.Groups[i];
					if (r.GroupNameFromNumber(i) != "0" && group.Value != string.Empty)
						yield return new KeyValuePair<string, string> (r.GroupNameFromNumber(i), group.Value);
				}
			}
		}
		
		public static ILookup<string, string> CaptureNamedGroups (this string self, string regex, RegexOptions options)
		{
			Check.Self (self);

			return CreateCaptureNamedGroupsIterator (self, regex, options).ToLookup (s => s.Key, s => s.Value);
		}

		public static ILookup<string, string> CaptureNamedGroups (this string self, string regex)
		{
			Check.Self (self);

			return CaptureNamedGroups (self, regex, RegexOptions.None);
		}

		public static string Slice (this string self, int start, int end)
		{
			Check.Self (self);

			if (start < 0 || start >= self.Length)
				throw new ArgumentOutOfRangeException ("start");

			if (end < 0)
				end += self.Length + 1;

			if (end < start || end > self.Length)
				throw new ArgumentOutOfRangeException ("end");

			return self.Substring (start, end - start);
		}
	}
}
