//
// Either.cs: Either utility methods.
//
// Author:
//   Jonathan Pryor  <jpryor@novell.com>
//
// Copyright (c) 2008 Novell, Inc. (http://www.novell.com)
//
// Based on ideas from: 
//  http://blogs.msdn.com/wesdyer/archive/2008/01/11/the-marvels-of-monads.aspx
//    (Turns Maybe into a struct, add some helpers, and make 
//    Maybe<T>.SelectMany actually work on current compilers.)
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
using System.ComponentModel;
using System.Linq.Expressions;

namespace Mono.Rocks {

	public static class Either {

		public static Either<T, Exception> TryParse<T> (string value)
		{
			return TryParse<string, T> (value);
		}

		public static Either<TResult, Exception> TryParse<TSource, TResult> (TSource value)
		{
			try {
				TypeConverter c = TypeDescriptor.GetConverter (typeof (TResult));
				if (c.CanConvertFrom (typeof (TSource)))
					return Either<TResult, Exception>.A ((TResult) c.ConvertFrom (value));

				c = TypeDescriptor.GetConverter (typeof (TSource));
				if (c.CanConvertTo (typeof (TResult)))
					return Either<TResult, Exception>.A ((TResult) c.ConvertTo (value, typeof (TResult)));

				// Convert.ChangeType uses IConvertible for type conversions;
				// throws InvalidCastException if type could not be converted.
				return Either<TResult, Exception>.A ((TResult) Convert.ChangeType (value, typeof (TResult)));
			}
			catch (Exception e) {
				return Either<TResult, Exception>.B (new NotSupportedException (
							string.Format ("Conversion from {0} to {1} is not supported.",
								typeof (TSource).FullName, typeof (TResult).FullName), e));
			}
		}
	}
}

