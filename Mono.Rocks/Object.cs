//
// Object.cs: C# extension methods on object.
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
using System.Linq.Expressions;

namespace Mono.Rocks {

	public static class ObjectRocks {

		// Match based in part on:
		// http://blogs.msdn.com/lucabol/archive/2008/07/15/a-c-library-to-write-functional-code-part-v-the-match-operator.aspx
		// I just think this is more readable, useful, and extensible (i.e. not
		// limited to just 2 match arguments).
		public static TResult Match<TSource, TResult> (this TSource self, params Func<TSource, Maybe<TResult>>[] matchers)
		{
			if (matchers == null)
				throw new ArgumentNullException ("matchers");

			foreach (var m in matchers) {
				var r = m (self);
				if (r.HasValue)
					return r.Value;
			}
			throw new InvalidOperationException ("no match");
		}

		public static Maybe<T> Just<T> (this T self)
		{
			return new Maybe<T> (self);
		}

		public static Maybe<T> ToMaybe<T> (this T self)
		{
			if (self == null)
				return Maybe<T>.Nothing;
			return new Maybe<T> (self);
		}

		public static TResult With<TSource, TResult> (this TSource self, Func<TSource, TResult> selector)
		{
			// Permit self to be null
			Check.Selector (selector);

			return selector (self);
		}
	}
}

