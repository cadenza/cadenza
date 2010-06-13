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
using System.Reflection;

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


		static Exception defaultProviderError;

		static Math ()
		{
			SetDefault (null, e => defaultProviderError = e);
		}

		static Math<T> defaultProvider;
		public static Math<T> Default {
			get {
				if (defaultProvider == null)
					throw new NotSupportedException (
							string.Format ("Could not find an implementation for '{0}'. " +
								"Try calling Cadenza.Numerics.Math<T>.SetDefault() with a Math<T> implementation.",
								typeof (T).FullName),
							defaultProviderError);
				return defaultProvider;
			}
		}

		public static void SetDefault (Math<T> provider)
		{
			if (provider != null) {
				defaultProvider       = provider;
				defaultProviderError  = null;
				return;
			}

			// TODO: set defaultProvider...
			if (typeof (T) == typeof (double))
				defaultProvider = (Math<T>) (object) new DoubleMath ();
			else if (typeof (T) == typeof (int))
				defaultProvider = (Math<T>) (object) new Int32Math ();
			else {
				Assembly  a     = Assembly.Load ("Cadenza.Core");
				Type      gem   = a.GetType ("Cadenza.Numerics.ExpressionMath`1");
				Type      em    = gem.MakeGenericType (typeof (T));
				defaultProvider = (Math<T>) Activator.CreateInstance (em);
			}
		}

		static void SetDefault (Math<T> provider, Action<Exception> handler)
		{
			try {
				SetDefault (provider);
			}
			catch (Exception e) {
				handler (e);
			}
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
				modulus   = Add (modulus, y);
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
			get {return FromIConvertible (Math.E);}
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

		public virtual T Atan2 (T y, T x)
		{
			return FromIConvertible (
					Math.Atan2 (
						ToIConvertible (y).ToDouble (null),
						ToIConvertible (x).ToDouble (null)));
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

	internal class DoubleMath : Math<double> {

		static void NotZero (double value)
		{
			if (value == 0)
				throw new ArgumentException ("Value must not be zero.", "value");
		}

		public override bool    IsUnsigned                                {get {return false;}}
		public override bool    LessThan            (double x, double y)  {return x < y;}
		public override bool    LessThanOrEqual     (double x, double y)  {return x <= y;}
		public override bool    GreaterThan         (double x, double y)  {return x > y;}
		public override bool    GreaterThanOrEqual  (double x, double y)  {return x >= y;}
		public override double  Max                 (double x, double y)  {return Math.Max (x, y);}
		public override double  Min                 (double x, double y)  {return Math.Min (x, y);}
		public override double  Successor           (double value)        {return checked (value+1);}
		public override double  Predecessor         (double value)        {return checked (value-1);}
		public override double  FromInt32           (int value)           {return value;}
		public override int     ToInt32             (double value)        {return (int) value;}
		public override bool    HasBounds                                 {get {return true;}}
		public override double  MinValue                                  {get {return double.MinValue;}}
		public override double  MaxValue                                  {get {return double.MaxValue;}}
		public override double  Add                 (double x, double y)  {return checked (x + y);}
		public override double  Multiply            (double x, double y)  {return checked (x * y);}
		public override double  Subtract            (double x, double y)  {return checked (x - y);}
		public override double  Negate              (double value)        {return checked (-value);}
		public override double  Abs                 (double value)        {return Math.Abs (value);}
		public override double  Sign                (double value)        {return Math.Sign (value);}
		public override double  FromIConvertible    (IConvertible value)  {Check.Value (value); return value.ToDouble (null);}
		public override double  Quotient            (double x, double y)  {return (int) (x / y);}       // truncates toward 0
		public override double  Remainder           (double x, double y)  {return x % y;}
		public override double  DivideIntegral      (double x, double y)  {return Math.Floor (x / y);}  // truncates toward -inf
		public override double  Modulus             (double x, double y)  {return Math.Abs (x % y);}
		public override double  QuotientRemainder   (double x, double y, out double remainder) {remainder = x % y; return (int) (x / y);}
		public override double  DivideIntegralModulus (double x, double y, out double modulus) {modulus = Math.Abs (x % y); return DivideIntegral (x, y);}
		public override IConvertible
		                           ToIConvertible   (double value)        {return value;}
		public override double  Divide              (double x, double y)  {return x / y;}
		public override double  Reciprocal          (double value)        {NotZero (value); return 1.0 / value;}
		public override double  Pi                                        {get {return Math.PI;}}
		public override double  E                                         {get {return Math.E;}}
		public override double  Exp (double value)                        {return Math.Exp (value);}
		public override double  Sqrt (double value)                       {return Math.Sqrt (value);}
		public override double  Log (double value)                        {return Math.Log (value);}
		public override double  Pow (double value, double exp)            {return Math.Pow (value, exp);}
		public override double  Log (double value, double newBase)        {return Math.Log (value, newBase);}
		public override double  Sin (double value)                        {return Math.Sin (value);}
		public override double  Tan (double value)                        {return Math.Tan (value);}
		public override double  Cos (double value)                        {return Math.Cos (value);}
		public override double  Asin (double value)                       {return Math.Asin (value);}
		public override double  Atan (double value)                       {return Math.Atan (value);}
		public override double  Acos (double value)                       {return Math.Acos (value);}
		public override double  Sinh (double value)                       {return Math.Sinh (value);}
		public override double  Tanh (double value)                       {return Math.Tanh (value);}
		public override double  Cosh (double value)                       {return Math.Cosh (value);}
		public override bool    IsIntegral                                {get {return false;}}
		public override int     FloatRadix          (double value)        {return 2;}
		public override int     FloatDigits         (double value)        {return 53;}
		public override Tuple<int, int>
		                           FloatRange       (double value)        {return Tuple.Create (-1022, 1023);}  // TODO: valid?
		public override bool    IsNaN               (double value)        {return double.IsNaN (value);}
		public override bool    IsInfinite          (double value)        {return double.IsInfinity (value);}
		public override bool    IsIEEE              (double value)        {return true;}
		public override double  Atan2               (double y, double x)  {return Math.Atan2 (y, x);}
		public override double  Truncate            (double value)        {return Math.Truncate (value);}
		public override double  Round               (double value)        {return Math.Round (value);}
		public override double  Ceiling             (double value)        {return Math.Ceiling (value);}
		public override double  Floor               (double value)        {return Math.Floor (value);}
		public override double  IEEERemainder       (double x, double y)  {return Math.IEEERemainder (x, y);}
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
		public override int   Modulus             (int x, int y)  {return Math.Abs (x % y);} // TODO?
		public override int   QuotientRemainder   (int x, int y, out int remainder) {remainder = x % y; return x / y;}
		public override int   DivideIntegralModulus (int x, int y, out int modulus) {modulus = Math.Abs (x % y); return DivideIntegral (x, y);}
		public override int   Divide              (int x, int y)  {return x / y;}
		public override int   Reciprocal          (int value)     {NotZero (value); return 0;}
		public override bool  IsIntegral                          {get {return true;}}
	}
}
