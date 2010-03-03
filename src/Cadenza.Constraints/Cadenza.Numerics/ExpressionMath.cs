//
// ExpressionMathProvider.cs
//
// Author:
//   Jonathan Pryor <jpryor@novell.com>
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
using System.Linq;
using System.Linq.Expressions;

using Cadenza;

namespace Cadenza.Numerics {

	// from: http://www.yoda.arachsys.com/csharp/genericoperators.html
	public class ExpressionMath<T> : Math<T>
	{
		static Func<T, T, T> add    = CreateBinaryExpression<T> ((a, b) => Expression.Add (a, b));
		static Func<T, T, T> sub    = CreateBinaryExpression<T> ((a, b) =>Expression.SubtractChecked (a, b));
		static Func<T, T, T> divide = CreateBinaryExpression<T> ((a, b) => Expression.Divide (a, b));
		static Func<T, T, T> mod    = CreateBinaryExpression<T> ((a, b) => Expression.Modulo (a, b));
		static Func<T, T, T> mult   = CreateBinaryExpression<T> ((a, b) => Expression.MultiplyChecked (a, b));
		static Func<T, T, T> pow    = CreateBinaryExpression<T>((a, b) => Expression.Power (a, b));
		static Func<T, T> negate    = CreateUnaryExpression (v => Expression.NegateChecked (v));
		static Func<T, T, bool> eq  = CreateBinaryExpression<bool> ((a, b) => Expression.Equal (a, b));
		static Func<T, T, bool> gt  = CreateBinaryExpression<bool> ((a, b) => Expression.GreaterThan (a, b));
		static Func<T, T, bool> gte = CreateBinaryExpression<bool> ((a, b) => Expression.GreaterThanOrEqual (a, b));
		static Func<T, T, bool> lt  = CreateBinaryExpression<bool> ((a, b) => Expression.LessThan (a, b));
		static Func<T, T, bool> lte = CreateBinaryExpression<bool> ((a, b) => Expression.LessThanOrEqual (a, b));

		static Func<T, T, TRet> CreateBinaryExpression<TRet> (Func<ParameterExpression, ParameterExpression, BinaryExpression> operation)
		{
			var a = Expression.Parameter (typeof (T), "a");
			var b = Expression.Parameter (typeof (T), "b");
			var body = operation (a, b);
			return Expression.Lambda<Func<T, T, TRet>> (body, a, b).Compile ();
		}

		static Func<T, T> CreateUnaryExpression (Func<ParameterExpression, UnaryExpression> operation)
		{
			var value = Expression.Parameter (typeof (T), "value");
			var body = operation (value);
			return Expression.Lambda<Func<T, T>> (body, value).Compile ();
		}

		public T Add (T x, T y)
		{
			return add (x, y);
		}
	}
}
