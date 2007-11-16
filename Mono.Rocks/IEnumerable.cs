//
// IEnumerableRocks.cs
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mono.Rocks {

	public static class IEnumerableRocks {

		public static string Join<TSource> (this IEnumerable<TSource> self, string separator)
		{
			Check.Self (self);

			var coll = self as ICollection<TSource>;
			if (coll != null && coll.Count == 0)
				return string.Empty;

			int i = 0;
			var s = new StringBuilder ();

			foreach (var element in self) {
				if (i > 0 && separator != null)
					s.Append (separator);

				s.Append (element);
				i++;
			}

			return s.ToString ();
		}

		public static string Join<TSource> (this IEnumerable<TSource> self)
		{
			Check.Self (self);

			return self.Join (null);
		}

		public static IEnumerable<TSource> Repeat<TSource> (this IEnumerable<TSource> self, int number)
		{
			Check.Self (self);

			for (int i = 0; i < number; i++) {
				foreach (var element in self)
					yield return element;
			}
		}

		public static string PathCombine (this IEnumerable<string> self)
		{
			Check.Self (self);

			char [] invalid = Path.GetInvalidPathChars ();
			char [] separators = { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar, Path.VolumeSeparatorChar };

			StringBuilder sb = null;
			string previous = null;
			foreach (string s in self) {
				if (s == null)
					throw new ArgumentNullException ("path");
				if (s.Length == 0)
					continue;
				if (s.IndexOfAny (invalid) != -1)
					throw new ArgumentException ("Illegal character in path");

				if (sb == null) {
					sb = new StringBuilder (s);
					previous = s;
				} else {
					if (Path.IsPathRooted (s)) {
						sb = new StringBuilder (s);
						continue;
					}

					char last = ((IEnumerable<char>) previous).Last ();
					if (!separators.Contains (last))
						sb.Append (Path.DirectorySeparatorChar);

					sb.Append (s);
					previous = s;
				}
			}
			return (sb == null) ? String.Empty : sb.ToString ();
		}
	}
}
