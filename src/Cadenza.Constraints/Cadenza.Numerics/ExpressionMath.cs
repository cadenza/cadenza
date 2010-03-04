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
			try {
				var body = operation (a, b);
				return Expression.Lambda<Func<T, T, TRet>> (body, a, b).Compile ();
			}
			catch {
				// operation not supported.
				return null;
			}
		}

		static Func<T, T> CreateUnaryExpression (Func<ParameterExpression, UnaryExpression> operation)
		{
			var value = Expression.Parameter (typeof (T), "value");
			try {
				var body = operation (value);
				return Expression.Lambda<Func<T, T>> (body, value).Compile ();
			}
			catch {
				// operation not supported.
				return null;
			}
		}

		public override bool Equals (T x, T y)
		{
			if (eq == null)
				return base.Equals (x, y);
			return eq (x, y);
		}

		public override bool LessThan (T x, T y)
		{
			if (lt == null)
				return base.LessThan (x, y);
			return lt (x, y);
		}

		public override bool LessThanOrEqual (T x, T y)
		{
			if (lte == null)
				return base.LessThan (x, y);
			return lte (x, y);
		}

		public override bool GreaterThan (T x, T y)
		{
			if (gt == null)
				return base.GreaterThan (x, y);
			return gt (x, y);
		}

		public override bool GreaterThanOrEqual (T x, T y)
		{
			if (gte == null)
				return base.GreaterThan (x, y);
			return gte (x, y);
		}

		public override T Add (T x, T y)
		{
			if (add == null)
				throw new NotSupportedException ();
			return add (x, y);
		}

		public override T Multiply (T x, T y)
		{
			if (mult == null)
				throw new NotSupportedException ();
			return mult (x, y);
		}

		public override T Subtract (T x, T y)
		{
			if (sub == null)
				throw new NotSupportedException ();
			return sub (x, y);
		}

		public override T Negate (T value)
		{
			if (negate == null)
				return base.Negate (value);
			return negate (value);
		}

		public override T Divide (T x, T y)
		{
			if (divide == null)
				throw new NotSupportedException ();
			return divide (x, y);
		}

		public override T Modulus (T x, T y)
		{
			if (mod == null)
				return base.Modulus (x, y);
			return mod (x, y);
		}

		public override T QuotientRemainder (T x, T y, out T remainder)
		{
			remainder = Remainder (x, y);
			return Quotient (x, y);
		}

		public override T Pow (T x, T y)
		{
			if (pow == null)
				return base.Pow (x, y);
			return pow (x, y);
		}
	}
}
