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

	public abstract partial class Math<T> : IComparer<T>, IEqualityComparer<T>
	{
		protected Math ()
		{
		}

		static Math ()
		{
			// TODO: set defaultProvider...
			if (typeof (T) == typeof (int))
				defaultProvider = (Math<T>) (object) new Int32Math ();
		}

		static Math<T> defaultProvider;
		public static Math<T> Default {
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

		public virtual bool IsUnsigned {
			get {return false;}
		}

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
			return Add (value, FromInt32 (1));
		}

		public virtual T Predecessor (T value)
		{
			return Subtract (value, FromInt32 (1));
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
			return Sequence.Iterate (start, v => Successor (v));
		}

		public virtual IEnumerable<T> EnumerateFromThen (T first, T start)
		{
			return new[]{first}.Concat (EnumerateFrom (start));
		}

		public virtual IEnumerable<T> EnumerateFromTo (T start, T end)
		{
			if (GreaterThan (start, end))
				throw new ArgumentException ("Cannot enumerate when end value is greater than start value.");

			return EnumerateFrom (start).TakeWhile (v => LessThanOrEqual (v, end));
		}

		public virtual IEnumerable<T> EnumerateFromThenTo (T first, T start, T end)
		{
			return new[]{first}.Concat (EnumerateFromTo (start, end));
		}
		#endregion class Enum a

		public virtual bool HasBounds {
			get {return false;}
		}

		#region class Bounded a where
		public virtual T MinValue {
			get {throw new NotSupportedException ();}
		}

		public virtual T MaxValue {
			get {throw new NotSupportedException ();}
		}
		#endregion class Bounded

		#region class (Eq a, Show a) => Num a where
		public abstract T Add (T x, T y);
		public abstract T Multiply (T x, T y);

		public abstract T Subtract (T x, T y);  // could be implemented in terms of Negate, but we need to choose one...

		public virtual T Negate (T value)
		{
			return Subtract (FromInt32 (0), value);
		}

		public virtual T Abs (T value)
		{
			if (LessThan (value, FromInt32 (0)))
				return Negate (value);
			return value;
		}

		public virtual T Sign (T value)
		{
			var zero = FromInt32 (0);
			if (Equals (zero, value))
				return zero;
			if (LessThan (value, zero))
				return FromInt32 (-1);
			return FromInt32 (1);
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
			T remainder;
			return QuotientRemainder (x, y, out remainder);
		}

		public virtual T Remainder (T x, T y)
		{
			T remainder;
			QuotientRemainder (x, y, out remainder);
			return remainder;
		}

		public virtual T DivideIntegral (T x, T y)
		{
			T _;
			return DivideIntegralModulus (x, y, out _);
		}

		public virtual T Modulus (T x, T y)
		{
			T modulus;
			DivideIntegralModulus (x, y, out modulus);
			return modulus;
		}

		// returns quotient
		public abstract T QuotientRemainder (T x, T y, out T remainder);

		// returns divide
		public virtual T DivideIntegralModulus (T x, T y, out T modulus)
		{
			var quotient = QuotientRemainder (x, y, out modulus);
			if (Equals (Sign (modulus), Negate (Sign (y)))) {
				quotient  = Predecessor (quotient);
				modulus   = Successor (modulus);
			}
			return quotient;
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
		public virtual T Divide (T x, T y)
		{
			return DivideIntegral (x, y);
		}

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

		public virtual T Pow (T value, T exp)
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

		public virtual bool IsIntegral {
			get {return true;}
		}

		#region class (Real a, Fractional a) => RealFrac a where
		public virtual int FloatRadix (T value)
		{
			throw new NotSupportedException ();
		}

		public virtual int FloatDigits (T value)
		{
			throw new NotSupportedException ();
		}

		public virtual Tuple<int, int> FloatRange (T value)
		{
			throw new NotSupportedException ();
		}

		// skip: decodeFloat, encodeFloat, exponent, significand, scaleFloat

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
			return FromIConvertible (
					Math.Atan2 (
						ToIConvertible (x).ToDouble (null),
						ToIConvertible (y).ToDouble (null)));
		}
		#endregion class RealFrac a

		#region class (RealFrac a, Floating a) => RealFloat a where
		public virtual T Truncate (T value)
		{
			return FromIConvertible (Math.Truncate (ToIConvertible (value).ToDouble (null)));
		}

		public virtual T Round (T value)
		{
			return FromIConvertible (Math.Round (ToIConvertible (value).ToDouble (null)));
		}

		public virtual T Ceiling (T value)
		{
			return FromIConvertible (Math.Ceiling (ToIConvertible (value).ToDouble (null)));
		}

		public virtual T Floor (T value)
		{
			return FromIConvertible (Math.Floor (ToIConvertible (value).ToDouble (null)));
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
		#endregion

		public virtual T IEEERemainder (T x, T y)
		{
			throw new NotSupportedException ();
		}
	}

	internal class Int32Math : Math<int> {

		static void NotZero (int value)
		{
			if (value == 0)
				throw new ArgumentException ("Value must not be zero.", "value");
		}

		public override bool  IsUnsigned                          {get {return false;}}
		public override bool  LessThan            (int x, int y)  {return x < y;}
		public override bool  LessThanOrEqual     (int x, int y)  {return x <= y;}
		public override bool  GreaterThan         (int x, int y)  {return x > y;}
		public override bool  GreaterThanOrEqual  (int x, int y)  {return x >= y;}
		public override int   Max                 (int x, int y)  {return Math.Max (x, y);}
		public override int   Min                 (int x, int y)  {return Math.Min (x, y);}
		public override int   Successor           (int value)     {return checked (value+1);}
		public override int   Predecessor         (int value)     {return checked (value-1);}
		public override int   FromInt32           (int value)     {return value;}
		public override int   ToInt32             (int value)     {return value;}
		public override bool  HasBounds                           {get {return true;}}
		public override int   MinValue                            {get {return int.MinValue;}}
		public override int   MaxValue                            {get {return int.MaxValue;}}
		public override int   Add                 (int x, int y)  {return checked (x + y);}
		public override int   Multiply            (int x, int y)  {return checked (x * y);}
		public override int   Subtract            (int x, int y)  {return checked (x - y);}
		public override int   Negate              (int value)     {return checked (-value);}
		public override int   Abs                 (int value)     {return Math.Abs (value);}
		public override int   Sign                (int value)     {return Math.Sign (value);}
		public override int   FromIConvertible    (IConvertible value)  {Check.Value (value); return value.ToInt32 (null);}
		public override int   Quotient            (int x, int y)  {return x / y;} // truncates toward 0
		public override int   Remainder           (int x, int y)  {return x % y;}
		public override int   DivideIntegral      (int x, int y)  {return ((x >= 0) ? x : checked (x-1))/ y;} // truncates toward -inf
		public override int   Modulus             (int x, int y)  {return x % y;} // TODO?
		public override int   QuotientRemainder   (int x, int y, out int remainder) {remainder = x % y; return x / y;}
		public override int   DivideIntegralModulus (int x, int y, out int modulus)   {modulus = x % y; return Divide (x, y);}
		public override int   Divide              (int x, int y)  {return x / y;}
		public override int   Reciprocal          (int value)     {NotZero (value); return 0;}
		public override bool  IsIntegral                          {get {return true;}}
	}
}
