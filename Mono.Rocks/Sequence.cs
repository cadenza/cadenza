//
// Sequence.cs
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

namespace Mono.Rocks {

	public static class Sequence {

		public static IEnumerable<TSource> Iterate<TSource> (TSource value, Func<TSource, TSource> selector)
		{
			Check.Selector (selector);

			return CreateIterateIterator (value, selector);
		}

		private static IEnumerable<TSource> CreateIterateIterator<TSource> (TSource value, Func<TSource, TSource> selector)
		{
			yield return value;
			while (true)
				yield return (value = selector (value));
		}

		public static IEnumerable<TSource> Repeat<TSource> (TSource value)
		{
			while (true)
				yield return value;
		}

		public static IEnumerable<TResult> GenerateReverse<TSource, TResult> (TSource value, Func<TSource, Maybe<Tuple<TResult, TSource>>> selector)
		{
			Check.Selector (selector);

			return CreateGenerateReverseIterator (value, selector);
		}

		private static IEnumerable<TResult> CreateGenerateReverseIterator<TSource, TResult> (TSource value, Func<TSource, Maybe<Tuple<TResult, TSource>>> selector)
		{
			Maybe<Tuple<TResult, TSource>> r;
			while ((r = selector (value)).HasValue) {
				Tuple<TResult, TSource> v = r.Value;
				yield return v._1;
				value = v._2;
			}
		}
	}
}
