//
// MathProvider.cs
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
	// Interfaced based on Haskell data type interfaces from:
	//    http://www.haskell.org/ghc/docs/latest/html/libraries/base-4.2.0.0/Prelude.html
	//

	public interface IOrderedTypeProvider<T> : IComparer<T>
	{
		#region class Eq a => Ord a where
		// compare is in IComparer<T>
		bool LessThan (T x, T y);
		bool LessThanOrEqual (T x, T y);
		bool GreaterThan (T x, T y);
		bool GreaterThanOrEqual (T x, T y);
		T Max (T x, T y);
		T Min (T x, T y);
		#endregion
	}

	public static class OrderedTypeProviderCoda
	{
		public static bool DefaultLessThan<T>(this IComparer<T> self, T x, T y)
		{
			Check.Self (self);

			var c = self.Compare (x, y);
			if (c < 0)
				return true;
			return false;
		}

		public static bool DefaultLessThanOrEqual<T>(this IComparer<T> self, T x, T y)
		{
			Check.Self (self);

			var c = self.Compare (x, y);
			if (c <= 0)
				return true;
			return false;
		}

		public static bool DefaultGreaterThan<T>(this IComparer<T> self, T x, T y)
		{
			Check.Self (self);

			var c = self.Compare (x, y);
			if (c > 0)
				return true;
			return false;
		}

		public static bool DefaultGreaterThanOrEqual<T>(this IComparer<T> self, T x, T y)
		{
			Check.Self (self);

			var c = self.Compare (x, y);
			if (c >= 0)
				return true;
			return false;
		}

		public static T DefaultMax<T> (this IComparer<T> self, T x, T y)
		{
			Check.Self (self);

			var c = self.Compare (x, y);
			return c >= 0 ? x : y;
		}

		public static T DefaultMin<T> (this IComparer<T> self, T x, T y)
		{
			Check.Self (self);

			var c = self.Compare (x, y);
			return c <= 0 ? x : y;
		}
	}


	public interface ISequentiallyOrderedTypeProvider<T>
	{
		#region class Enum a where
		T Successor (T value);
		T Predecessor (T value);
		T Parse (int value);
		int ToInt32 (T value);
		IEnumerable<T> EnumerateFrom (T value);
		IEnumerable<T> EnumerateFromThen (T first, T start);
		IEnumerable<T> EnumerateFromTo (T start, T end);
		IEnumerable<T> EnumerateFromThenTo (T first, T start, T end);
		#endregion
	}

	public static class SequentiallyOrderedTypeProviderCoda
	{
		// Default implmentations
		public static T DefaultSuccessor<T> (ISequentiallyOrderedTypeProvider<T> self, T value)
		{
			Check.Self (self);

			return self.Parse (checked (self.ToInt32 (value) + 1));
		}

		public static T DefaultPredecessor<T> (ISequentiallyOrderedTypeProvider<T> self, T value)
		{
			Check.Self (self);

			return self.Parse (checked (self.ToInt32 (value) - 1));
		}

		public static IEnumerable<T> DefaultEnumFrom<T> (ISequentiallyOrderedTypeProvider<T> self, T value)
		{
			Check.Self (self);

			return Sequence.Iterate (self.ToInt32 (value), v => checked (v + 1))
				.Select (v => self.Parse (v));
		}

		public static IEnumerable<T> DefaultEnumFromThen<T> (ISequentiallyOrderedTypeProvider<T> self, T first, T start)
		{
			Check.Self (self);

			return new[]{self.ToInt32 (first)}
				.Concat (Sequence.Iterate (self.ToInt32 (start), v => checked (v + 1)))
				.Select (v => self.Parse (v));
		}

		public static IEnumerable<T> DefaultEnumFromTo<T> (ISequentiallyOrderedTypeProvider<T> self, T start, T end)
		{
			Check.Self (self);

			int s = self.ToInt32 (start);
			int e = self.ToInt32 (end);

			return Enumerable.Range (s, e - s).Select (v => self.Parse (v));
		}

		public static IEnumerable<T> DefaultEnumFromThenTo<T> (ISequentiallyOrderedTypeProvider<T> self, T first, T start, T end)
		{
			Check.Self (self);

			int s = self.ToInt32 (start);
			int e = self.ToInt32 (end);

			return new[]{first}.Concat (Enumerable.Range (s, e - s).Select (v => self.Parse (v)));
		}
	}

	public interface IBoundedProvider<T>
	{
		#region class Bounded a where
		T MinBound ();
		T MaxBound ();
		#endregion
	}

	// TODO: support Rational?
	// TODO: how should we support Integer?  it's a variable-sized integer type

	public interface INumericProvider<T> : IEqualityComparer<T>
	{
		#region class (Eq a, Show a) => Num a where
		T Add (T x, T y);
		T Multiply (T x, T y);
		T Subtract (T x, T y);
		T Negate (T x);
		T Abs (T value);
		int Sign (T value);

		T Parse (int value);
		#endregion
	}

	public interface IRealProvider<T> : INumericProvider<T>, IOrderedTypeProvider<T>
	{
		// TODO? toRational :: a -> Rational
	}

	public interface IIntegralNumberProvider<T> : IRealProvider<T>, ISequentiallyOrderedTypeProvider<T>
	{
		#region class (Real a, Enum a) => Integral a where
		T Quotient (T x, T y);  // integer division truncating toward 0
		T Remainder (T x, T y); // integer remainder; satisfies: (x `quot` y)*y + (x `rem` y) == x
		T Divide (T x, T y);     // integer division truncated toward negative infinity
		T Modulus (T x, T y);   // integer moduleus, satisfying: (x `div` y)*y + (x `mod` y) == x
		Tuple<T, T> QuotientRemainder (T x, T y);
		Tuple<T, T> DivideModulus (T x, T y);
		long ToInt64 (T value); // toInteger
		#endregion
	}

	public interface IFractionalProvider<T> : INumericProvider<T>
	{
		#region class Num a => Fractional a where
		T Divide (T x, T y);
		T Reciprocal (T value);
		// TODO: fromRational
		#endregion
	}

	public interface IFloatingProvider<T> : IFractionalProvider<T>
	{
		#region class Fractional a => Floating a where
		T Pi {get;}
		T E {get;}
		T Exp (T value);
		T Sqrt (T value);
		T Log (T value);
		T Exponentiate (T value, T exp);
		T Log (T value, T newBase);
		T Sin (T value);
		T Tan (T value);
		T Cos (T value);
		T Asin (T value);
		T Atan (T value);
		T Acos (T value);
		T Sinh (T value);
		T Tanh (T value);
		T Cosh (T value);
		// TODO: asinh, atanh, acosh
		#endregion
	}

	public interface IRealFracProvider<T> : IRealProvider<T>, IFractionalProvider<T>
	{
		#region class (Real a, Fractional a) => RealFrac a where
		// TODO: properFraction :: Integral b => a ->(b, a)
		// TODO: Haskell introduces an 'Integral b' parameter; don't understand its use.
		T Truncate (T value);
		T Round (T value);
		T Ceiling (T value);
		T Floor (T value);
		#endregion
	}

	public interface IRealFloatProvider<T> : IRealFracProvider<T>, IFloatingProvider<T>
	{
		#region class (RealFrac a, Floating a) => RealFloat a where
		int Radix (T value);
		int Digits (T value);
		Tuple<int, int> Range (T value);
		// TODO: decodeFloat, encodeFloat, exponent, significand, scaleFloat
		bool IsNaN (T value);
		bool IsInfinite (T value);
		// TODO: isDenormalized, isNegativeZero
		bool IsIEEE (T value);
		T Atan2 (T y, T x);
		#endregion
	}

	public abstract partial class MathProvider<T> : IComparer<T>, IEqualityComparer<T> {

		protected MathProvider ()
		{
		}

		static MathProvider ()
		{
			// set defaultProvider...
		}

		static MathProvider<T> defaultProvider;
		public MathProvider<T> Default {
			get {return defaultProvider;}
		}

		public static void SetDefault (MathProvider<T> provider)
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

		#region Numeric functions
		// subtract?
		#if false
		public abstract bool Even (T value);
		public abstract bool Odd (T value);
		public abstract T LeastCommonMultiple (T a, T b);
		#endif
		// TODO: ^, ^^, fromIntegral, realToFrac
		#endregion
	}
}
