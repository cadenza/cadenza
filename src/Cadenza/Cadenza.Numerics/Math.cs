//
// Math.cs
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

using Cadenza;

namespace Cadenza.Numerics {

	//
	// Operations based on Haskell data type interfaces from:
	//    http://www.haskell.org/ghc/docs/latest/html/libraries/base-4.2.0.0/Prelude.html
	//

	// TODO: support Rational?
	// TODO: how should we support Integer?  it's a variable-sized integer type

	public partial class Math<T> : IComparer<T>, IEqualityComparer<T>
	{
		protected Math ()
		{
		}

		static Math ()
		{
			// TODO: set defaultProvider...
		}

		static Math<T> defaultProvider;
		public Math<T> Default {
			get {return defaultProvider;}
		}

		public static void SetDefault (Math<T> provider)
		{
			defaultProvider = provider;
		}

		#region IComparer<T>
		public virtual int Compare (T x, T y)
		{
			return Comparer<T>.Default.Compare (x, y);
		}
		#endregion

		#region IEqualityComparer<T>
		public virtual bool Equals (T x, T y)
		{
			return EqualityComparer<T>.Default.Equals (x, y);
		}

		public virtual int GetHashCode (T obj)
		{
			return EqualityComparer<T>.Default.GetHashCode (obj);
		}
		#endregion

		#region class Eq a => Ord a where
		public virtual bool LessThan (T x, T y)
		{
			var c = Compare (x, y);
			if (c < 0)
				return true;
			return false;
		}

		public virtual bool LessThanOrEqual (T x, T y)
		{
			var c = Compare (x, y);
			if (c <= 0)
				return true;
			return false;
		}

		public virtual bool GreaterThan (T x, T y)
		{
			var c = Compare (x, y);
			if (c > 0)
				return true;
			return false;
		}

		public virtual bool GreaterThanOrEqual (T x, T y)
		{
			var c = Compare (x, y);
			if (c >= 0)
				return true;
			return false;
		}

		public virtual T Max (T x, T y)
		{
			var c = Compare (x, y);
			return c >= 0 ? x : y;
		}

		public virtual T Min (T x, T y)
		{
			var c = Compare (x, y);
			return c <= 0 ? x : y;
		}
		#endregion class Eq a

		#region class Enum a where
		public virtual T Successor (T value)
		{
			return FromInt32 (checked (ToInt32 (value) + 1));
		}

		public virtual T Predecessor (T value)
		{
			return FromInt32 (checked (ToInt32 (value) - 1));
		}

		public virtual T FromInt32 (int value)
		{
			var r = Either.TryConvert<int, T>(value);
			return r.Fold (v => v, e => {throw e;});
		}

		public virtual int ToInt32 (T value)
		{
			var r = Either.TryConvert<T, int> (value);
			return r.Fold (v => v, e => {throw e;});
		}

		public virtual IEnumerable<T> EnumerateFrom (T start)
		{
			return Sequence.Iterate (ToInt32 (start), v => checked (v + 1))
				.Select (v => FromInt32 (v));
		}

		public virtual IEnumerable<T> EnumerateFromThen (T first, T start)
		{
			return new[]{ToInt32 (first)}
				.Concat (Sequence.Iterate (ToInt32 (start), v => checked (v + 1)))
				.Select (v => FromInt32 (v));
		}

		public virtual IEnumerable<T> EnumerateFromTo (T start, T end)
		{
			int s = ToInt32 (start);
			int e = ToInt32 (end);

			return Enumerable.Range (s, e - s).Select (v => FromInt32 (v));
		}

		public virtual IEnumerable<T> EnumerateFromThenTo (T first, T start, T end)
		{
			return new[]{first}.Concat (EnumerateFromTo (start, end));
		}
		#endregion class Enum a

		#region class Bounded a where
		public virtual T MinBound ()
		{
			throw new NotSupportedException ();
		}

		public virtual T MaxBound ()
		{
			throw new NotSupportedException ();
		}
		#endregion class Bounded

		#region class (Eq a, Show a) => Num a where
		public virtual T Add (T x, T y)
		{
			throw new NotSupportedException ();
		}

		public virtual T Multiply (T x, T y)
		{
			throw new NotSupportedException ();
		}

		public virtual T Subtract (T x, T y)
		{
			throw new NotSupportedException ();
		}

		public virtual T Negate (T value)
		{
			throw new NotSupportedException ();
		}

		public virtual T Abs (T value)
		{
			throw new NotSupportedException ();
		}

		public virtual int Sign (T value)
		{
			throw new NotSupportedException ();
		}

		[CLSCompliant (false)]
		public virtual T FromIConvertible (IConvertible value)
		{
			var r = Either.TryConvert<T>((object) value);
			return r.Fold (v => v, e => {throw e;});
		}
		#endregion class Num a

		#region class (Num a, Ord a) => Real a where
		#endregion class Real a

		#region class (Real a, Enum a) => Integral a where
		public virtual T Quotient (T x, T y)
		{
			return QuotientRemainder (x, y).Item1;
		}

		public virtual T Remainder (T x, T y)
		{
			return QuotientRemainder (x, y).Item2;
		}

		public virtual T Divide (T x, T y)
		{
			return DivideModulus (x, y).Item1;
		}

		public virtual T Modulus (T x, T y)
		{
			return DivideModulus (x, y).Item2;
		}

		public virtual Tuple<T, T> QuotientRemainder (T x, T y)
		{
			throw new NotSupportedException ();
		}

		public virtual Tuple<T, T> DivideModulus (T x, T y)
		{
			throw new NotSupportedException ();
		}

		[CLSCompliant (false)]
		public virtual IConvertible ToIConvertible (T value)
		{
			var v = value as IConvertible;
			if (v != null)
				return v;
			throw new NotSupportedException ();
		}
		#endregion class Integral a

		#region class Num a => Fractional a where
		// Divide declared in IIntegralNumberProvider<T> region
		// TODO: should float vs. int divide be distinct?

		public virtual T Reciprocal (T value)
		{
			return Divide (FromInt32 (1), value);
		}
		#endregion class Fractional a

		#region class Fractional a => Floating a where
		public virtual T Pi {
			get {return FromIConvertible (Math.PI);}
		}

		public virtual T E {
			get {return FromIConvertible (Math.PI);}
		}

		public virtual T Exp (T value)
		{
			return FromIConvertible (Math.Exp (ToIConvertible (value).ToDouble (null)));
		}

		public virtual T Sqrt (T value)
		{
			return FromIConvertible (Math.Sqrt (ToIConvertible (value).ToDouble (null)));
		}

		public virtual T Log (T value)
		{
			return FromIConvertible (Math.Log (ToIConvertible (value).ToDouble (null)));
		}

		public virtual T Exponentiate (T value, T exp)
		{
			return FromIConvertible (
					Math.Pow (
						ToIConvertible (value).ToDouble (null),
						ToIConvertible (exp).ToDouble (null)));
		}

		public virtual T Log (T value, T newBase)
		{
			return FromIConvertible (
					Math.Log (
						ToIConvertible (value).ToDouble (null),
						ToIConvertible (newBase).ToDouble (null)));
		}

		public virtual T Sin (T value)
		{
			return FromIConvertible (Math.Sin (ToIConvertible (value).ToDouble (null)));
		}

		public virtual T Tan (T value)
		{
			return FromIConvertible (Math.Tan (ToIConvertible (value).ToDouble (null)));
		}

		public virtual T Cos (T value)
		{
			return FromIConvertible (Math.Cos (ToIConvertible (value).ToDouble (null)));
		}

		public virtual T Asin (T value)
		{
			return FromIConvertible (Math.Asin (ToIConvertible (value).ToDouble (null)));
		}

		public virtual T Atan (T value)
		{
			return FromIConvertible (Math.Atan (ToIConvertible (value).ToDouble (null)));
		}

		public virtual T Acos (T value)
		{
			return FromIConvertible (Math.Acos (ToIConvertible (value).ToDouble (null)));
		}

		public virtual T Sinh (T value)
		{
			return FromIConvertible (Math.Sin (ToIConvertible (value).ToDouble (null)));
		}

		public virtual T Tanh (T value)
		{
			return FromIConvertible (Math.Tan (ToIConvertible (value).ToDouble (null)));
		}

		public virtual T Cosh (T value)
		{
			return FromIConvertible (Math.Cos (ToIConvertible (value).ToDouble (null)));
		}
		#endregion classFloating a

		#region class (Real a, Fractional a) => RealFrac a where
		public virtual int Radix (T value)
		{
			throw new NotSupportedException ();
		}

		public virtual int Digits (T value)
		{
			throw new NotSupportedException ();
		}

		public virtual Tuple<int, int> Range (T value)
		{
			throw new NotSupportedException ();
		}

		public virtual bool IsNaN (T value)
		{
			throw new NotSupportedException ();
		}

		public virtual bool IsInfinite (T value)
		{
			throw new NotSupportedException ();
		}

		public virtual bool IsIEEE (T value)
		{
			throw new NotSupportedException ();
		}

		public virtual T Atan2 (T x, T y)
		{
			throw new NotSupportedException ();
		}
		#endregion class RealFrac a

		#region class (RealFrac a, Floating a) => RealFloat a where
		public virtual T Truncate (T value)
		{
			throw new NotSupportedException ();
		}

		public virtual T Round (T value)
		{
			throw new NotSupportedException ();
		}

		public virtual T Ceiling (T value)
		{
			throw new NotSupportedException ();
		}

		public virtual T Floor (T value)
		{
			throw new NotSupportedException ();
		}
		#endregion class RealFloat a

		#region Numeric functions
		// subtract?
		#if false
		public abstract bool Even (T value);
		public abstract bool Odd (T value);
		public abstract T LeastCommonMultiple (T a, T b);
		#endif
		// TODO: ^, ^^, fromIntegral, realToFrac
		public virtual T Pow (T x, T y)
		{
			throw new NotImplementedException ();
		}
		#endregion
	}
}
