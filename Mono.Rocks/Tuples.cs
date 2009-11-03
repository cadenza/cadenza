//
// Tuples.cs: Tuple types.
//
// GENERATED CODE: DO NOT EDIT.
//
// To regenerate this code, execute: mktuples -n 4 -o Tuples.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Mono.Rocks {

	/// <summary>
	///   Utility methods to create Tuple instances.
	/// </summary>
	/// <remarks>
	///   <para>
	///    Provides a set of <see cref="M:Mono.Rocks.Tuple.Create"/> methods so that
	///    C# type inferencing can easily be used with tuples.  For example,
	///    instead of:
	///   </para>
	///   <code lang="C#">
	///   Tuple&lt;int, long&gt; a = new Tuple&lt;int, long&gt; (1, 2L);</code>
	///   <para>You can instead write:</para>
	///   <code lang="C#">
	///   Tuple&lt;int, long&gt; b = Tuple.Create (1, 2L);
	///   // or
	///   var              c = Tuple.Create (1, 2L);</code>
	/// </remarks>
	public static partial class Tuple {

		/// <summary>
		///   The maximum number of Tuple types provided.
		/// </summary>
		/// <value>
		///   The maximum number of Tuple types provided.
		/// </value>
		/// <remarks>
		///   <para>
		///    Only tuples up to a certain "arity" are supported; for example,
		///    a <c>Tuple&lt;T1, T2, ..., T100&gt;</c> isn't supported (and won't
		///    likely ever be).
		///   </para>
		///   <para>
		///    <see cref="P:Mono.Rocks.Tuple.MaxValues" /> is the maximum number of
		///    values that the Tuple types support.  If you need to support
		///    more values, then you need to either live with potential boxing
		///    and use a e.g. <see cref="T:System.Collections.Generic.List{System.Object}" />
		///    or nest Tuple instantiations, e.g. 
		///    <c>Tuple&lt;int, Tuple&lt;int, Tuple&lt;int, Tuple&lt;int, int>>>></c>.
		///    The problem with such nesting is that it becomes "unnatural" to access 
		///    later elements -- <c>t._2._2._2._2</c> to access the fifth value for
		///    the previous example.
		///   </para>
		/// </remarks>
		public static readonly int MaxValues = 4;

		/// <typeparam name="T">
		///   The first <see cref="T:Mono.Rocks.Tuple{T}"/> value type.
		/// </typeparam>
		/// <param name="value">
		///   The first <see cref="T:Mono.Rocks.Tuple{T}"/> value.
		/// </param>
		/// <summary>
		///   Creates a <see cref="T:Mono.Rocks.Tuple{T}"/>.
		/// </summary>
		/// <returns>
		///   A <see cref="T:Mono.Rocks.Tuple{T}"/> initialized with the parameter values.
		/// </returns>
		public static Tuple<T>
			Create<T> (T value)
		{
			return new Tuple<T> (value);
		}

		/// <typeparam name="T1">
		///   The first <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> value type.
		/// </typeparam>
		/// <typeparam name="T2">
		///   The second <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> value type.
		/// </typeparam>
		/// <param name="value1">
		///   The first <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> value.
		/// </param>
		/// <param name="value2">
		///   The second <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> value.
		/// </param>
		/// <summary>
		///   Creates a <see cref="T:Mono.Rocks.Tuple{T1, T2}"/>.
		/// </summary>
		/// <returns>
		///   A <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> initialized with the parameter values.
		/// </returns>
		public static Tuple<T1, T2>
			Create<T1, T2> (T1 value1, T2 value2)
		{
			return new Tuple<T1, T2> (value1, value2);
		}

		/// <typeparam name="T1">
		///   The first <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> value type.
		/// </typeparam>
		/// <typeparam name="T2">
		///   The second <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> value type.
		/// </typeparam>
		/// <typeparam name="T3">
		///   The third <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> value type.
		/// </typeparam>
		/// <param name="value1">
		///   The first <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> value.
		/// </param>
		/// <param name="value2">
		///   The second <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> value.
		/// </param>
		/// <param name="value3">
		///   The third <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> value.
		/// </param>
		/// <summary>
		///   Creates a <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/>.
		/// </summary>
		/// <returns>
		///   A <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> initialized with the parameter values.
		/// </returns>
		public static Tuple<T1, T2, T3>
			Create<T1, T2, T3> (T1 value1, T2 value2, T3 value3)
		{
			return new Tuple<T1, T2, T3> (value1, value2, value3);
		}

		/// <typeparam name="T1">
		///   The first <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> value type.
		/// </typeparam>
		/// <typeparam name="T2">
		///   The second <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> value type.
		/// </typeparam>
		/// <typeparam name="T3">
		///   The third <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> value type.
		/// </typeparam>
		/// <typeparam name="T4">
		///   The fourth <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> value type.
		/// </typeparam>
		/// <param name="value1">
		///   The first <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> value.
		/// </param>
		/// <param name="value2">
		///   The second <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> value.
		/// </param>
		/// <param name="value3">
		///   The third <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> value.
		/// </param>
		/// <param name="value4">
		///   The fourth <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> value.
		/// </param>
		/// <summary>
		///   Creates a <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/>.
		/// </summary>
		/// <returns>
		///   A <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> initialized with the parameter values.
		/// </returns>
		public static Tuple<T1, T2, T3, T4>
			Create<T1, T2, T3, T4> (T1 value1, T2 value2, T3 value3, T4 value4)
		{
			return new Tuple<T1, T2, T3, T4> (value1, value2, value3, value4);
		}
	}

	/// <typeparam name="T">
	///   The first value type.
	/// </typeparam>
	/// <summary>
	///   A strongly-typed sequence of 1 variously typed values.
	/// </summary>
	/// <remarks>
	///   <para>
	///    A <c>Tuple</c> is an immutable, strongly typed sequence of variously 
	///    typed values with each value lacking an otherwise meaningful name aside 
	///    from its position.
	///   </para>
	///   <para>
	///    Tuples provide an indexer to access variables by-index in a loosely
	///    typed manner, and provide a set of properties to access variables 
	///    by-index in a strongly typed manner.  Strongly typed properties use
	///    the pattern <c>_N</c>, where <c>N</c> is the ones-based value position.
	///    The indexer, as always, uses 0-based positions.  Thus the value
	///    <see cref="P:Mono.Rocks.Tuple`1._1"/> and <c>tuple[0]</c> refer to the same value,
	///    except <see cref="P:Mono.Rocks.Tuple`1._1"/> is strongly typed, while
	///    <c>tuple[0]</c> is typed as a <see cref="T:System.Object"/> (and thus
	///    potentially boxed).
	///   </para>
	///   <para>
	///    Tuples also implement the common collection interfaces, and all collection
	///    methods that would require mutating the Tuple throw 
	///    <see cref="T:System.NotSupportedException"/>.
	///   </para>
	/// </remarks>
	public struct Tuple<T>
		: IList, IList<object>, IEquatable<Tuple<T>>
	{
		// For .SyncRoot implementation
		private static object syncRoot = new object ();

		private T value;

		/// <param name="value">
		///   A <typeparamref name="T"/> which is used to initialize the <see cref="P:Mono.Rocks.Tuple{T}.1"/> property.
		/// </param>
		/// <summary>
		///   Constructs and initializes a new <see cref="T:Mono.Rocks.Tuple{T}"/> instance.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Constructs and initializes a new <see cref="T:Mono.Rocks.Tuple{T}"/> instance.
		///   </para>
		/// </remarks>
		public Tuple (T value)
		{
			this.value = value;
		}

		/// <summary>
		///   The first tuple value.
		/// </summary>
		/// <value>
		///   A <typeparamref name="T"/> which is the first tuple value.
		/// </value>
		/// <remarks>
		///   The first tuple value.
		/// </remarks>
		[CLSCompliant (false)]
		public T _1 {get{return value;}}

		/// <summary>
		///   Generates a hash code for the current instance.
		/// </summary>
		/// <returns>
		///   A <see cref="T:System.Int32"/> containing the hash code for this instance.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="note">
		///     This method overrides <see cref="M:System.Object.GetHashCode"/>.
		///    </block>
		///   </para>
		/// </remarks>
		public override int GetHashCode ()
		{
			int hc = 0;
			hc ^= _1.GetHashCode ();
			return hc;
		}

		/// <param name="obj">
		///   A <see cref="T:System.Object"/> to compare this instance against.
		/// </param>
		/// <summary>
		///   Determines whether the current instance and the specified object have the same value.
		/// </summary>
		/// <returns>
		///   <para>
		///    <see langword="true"/> if <paramref name="obj"/> is a 
		///    <see cref="T:Mono.Rocks.Tuple{T}"/> and each member of <paramref name="obj"/>
		///    and the current instance have the same value (according to
		///    <see cref="M:System.Object.Equals(System.Object)"/>); otherwise
		///    <see langword="false"/> is returned.
		///   </para>
		/// </returns>
		/// <remarks>
		///   <para>
		///    This method checks for value equality 
		///    (<see cref="M:System.Object.Equals(System.Object)"/>), as defined by each
		///    value type.
		///   </para>
		///   <para>
		///    <block subset="none" type="note">
		///     This method overrides <see cref="M:System.Object.Equals(System.Object)"/>.
		///    </block>
		///   </para>
		/// </remarks>
		public override bool Equals (object obj)
		{
			if (!(obj is Tuple<T>))
				return false;
			return Equals ((Tuple<T>) obj);
		}

		/// <param name="obj">
		///   A <see cref="T:Mono.Rocks.Tuple{T}"/> to compare this instance against.
		/// </param>
		/// <summary>
		///   Determines whether the current instance and the specified <see cref="T:Mono.Rocks.Tuple{T}"/> have the same value.
		/// </summary>
		/// <returns>
		///   <para>
		///    <see langword="true"/> if each member of <paramref name="obj"/>
		///    and the current instance have the same value (according to
		///    <see cref="M:System.Object.Equals(System.Object)"/>); otherwise
		///    <see langword="false"/> is returned.
		///   </para>
		/// </returns>
		/// <remarks>
		///   <para>
		///    This method checks for value equality 
		///    (<see cref="M:System.Object.Equals(System.Object)"/>), as defined by each
		///    value type.
		///   </para>
		/// </remarks>
		public bool Equals (Tuple<T> obj)
		{
			return EqualityComparer<T>.Default.Equals (_1, obj._1)
				;
		}
		#region ICollection

		/// <param name="array">
		///   The one-dimensional
		///   <see cref="T:System.Array"/> that is the destination for the values
		///   to be copied from the current instance.
		/// </param>
		/// <param name="index">
		///   A <see cref="T:System.Int32"/> that specifies
		///   the first index of <paramref name="array"/> to which the elements of the
		///   current <see cref="T:Mono.Rocks.Tuple{T}"/> are copied.
		/// </param>
		/// <summary>
		///   Copies the values of the current 
		///   <see cref="T:Mono.Rocks.Tuple{T}"/> to the specified <see cref="T:System.Array"/>,
		///   starting at the <paramref name="index"/> index of the array.
		/// </summary>
		/// <exception cref="T:System.ArgumentException">
		///   <para>
		///    <paramref name="index" /> is >= 
		///    <paramref name="array"/>'s <see cref="P:System.Array.Length"/> property.
		///   </para>
		///   <para>-or-</para>
		///   <para>
		///    <paramref name="index" /> + <see cref="P:System.Collections.ICollection.Count"/>
		///    of the current instance is >
		///    <paramref name="array"/>'s <see cref="P:System.Array.Length"/> property.
		///   </para>
		/// </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array"/> is <see langword="null"/>.
		/// </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index"/> is negative.
		/// </exception>
		/// <exception cref="T:System.InvalidCastException">
		///   At least one element in the current instance is not
		///   assignment-compatible with the type of <paramref name="array"/>.
		/// </exception>
		void ICollection.CopyTo (Array array, int index)
		{
			if (array == null)
				throw new ArgumentNullException ("array");
			if (index < 0)
				throw new ArgumentOutOfRangeException ("index");
			if (array.Length - index <= 0 ||
					(array.Length - index) < 1)
				throw new ArgumentException ("index");
			array.SetValue (_1, index + 0);
		}

		/// <summary>
		///   The number of values in the <see cref="T:Mono.Rocks.Tuple{T}"/>.
		/// </summary>
		/// <value>
		///   A <see cref="T:System.Int32"/> containing the number 
		///   of values in this <see cref="T:Mono.Rocks.Tuple{T}"/>.
		/// </value>
		/// <remarks>
		///   This values is always equal to 1.
		/// </remarks>
		int ICollection.Count {
			get {return 1;}
		}

		/// <summary>
		///   Gets a value indicating whether access ot the 
		///   current instance is synchronized (thread-safe)
		/// </summary>
		/// <value>
		///   This property always returns <see langword="true"/>.
		/// </value>
		/// <remarks>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T}"/> instance are immutable, and thus the instances
		///    themselves are always thread-safe.  However, this does not mean that 
		///    the values exposed by the tuple are thread safe, so care should be 
		///    taken if necessary.
		///   </para>
		/// </remarks>
		bool ICollection.IsSynchronized  {get {return true;}}

		/// <summary>
		///   Gets an object that can be used to synchronize access
		///   to the current instance.
		/// </summary>
		/// <value>
		///   A <see cref="T:System.Object"/> that can be used to
		///   synchronize access to the current instance.
		/// </value>
		/// <remarks>
		///   <para>
		///    This property shouldn't be used, as <see cref="T:Mono.Rocks.Tuple{T}"/> instances
		///    are immutable, and thus require no locking.
		///   </para>
		///   <para>
		///    The object returned is shared by all <see cref="T:Mono.Rocks.Tuple{T}"/> instances.
		///   </para>
		/// </remarks>
		object ICollection.SyncRoot {get {return syncRoot;}}
		#endregion

		#region ICollection<T>

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void ICollection<object>.Add (object item)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void ICollection<object>.Clear ()
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		bool ICollection<object>.Remove (object item)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Gets a value indicating whether the current instance is read-only
		/// </summary>
		/// <value>
		///   <see langword="true"/>.
		/// </value>
		/// <remarks>
		///   This property always returns <see langword="true"/>.
		/// </remarks>
		bool ICollection<object>.IsReadOnly {get {return true;}}

		/// <param name="value">
		///   The <see cref="T:System.Object"/> to locate in the current instance.
		/// </param>
		/// <summary>
		///   Determines whether <paramref name="value"/>
		///   <see cref="M:System.Object.Equals(System.Object)"/> any value within this
		///   <see cref="T:Mono.Rocks.Tuple{T}"/> instance.
		/// </summary>
		/// <returns>
		///   <see langword="true"/> if <paramref name="value"/> is
		///   contained in the current instance; otherwise, <see langword="false"/>.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="note">
		///     This method determines equality by calling 
		///     <see cref="M:System.Object.Equls(System.Object,System.Object)"/> on
		///     each value contained within this tuple and <paramref name="value"/>.
		///    </block>
		///   </para>
		/// </remarks>
		bool ICollection<object>.Contains (object value)
		{
			return ((IList<object>) this).IndexOf (value) >= 0;
		}

		/// <summary>
		///   The number of values in the <see cref="T:Mono.Rocks.Tuple{T}"/>.
		/// </summary>
		/// <value>
		///   A <see cref="T:System.Int32"/> containing the number 
		///   of values in this <see cref="T:Mono.Rocks.Tuple{T}"/>.
		/// </value>
		/// <remarks>
		///   This values is always equal to 1.
		/// </remarks>
		int ICollection<object>.Count {
			get {return 1;}
		}

		/// <param name="array">
		///   The one-dimensional
		///   <see cref="T:System.Array"/> that is the destination for the values
		///   to be copied from the current instance.
		/// </param>
		/// <param name="arrayIndex">
		///   A <see cref="T:System.Int32"/> that specifies
		///   the first index of <paramref name="array"/> to which the elements of the
		///   current <see cref="T:Mono.Rocks.Tuple{T}"/> are copied.
		/// </param>
		/// <summary>
		///   Copies the values of the current 
		///   <see cref="T:Mono.Rocks.Tuple{T}"/> to the specified <see cref="T:System.Array"/>,
		///   starting at the <paramref name="arrayIndex"/> index of the array.
		/// </summary>
		/// <exception cref="T:System.ArgumentException">
		///   <para>
		///    <paramref name="arrayIndex" /> is >= 
		///    <paramref name="array"/>'s <see cref="P:System.Array.Length"/> property.
		///   </para>
		///   <para>-or-</para>
		///   <para>
		///    <paramref name="arrayIndex" /> + <see cref="P:System.Collections.ICollection.Count"/>
		///    of the current instance is >
		///    <paramref name="array"/>'s <see cref="P:System.Array.Length"/> property.
		///   </para>
		/// </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array"/> is <see langword="null"/>.
		/// </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex"/> is negative.
		/// </exception>
		void ICollection<object>.CopyTo (object[] array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentNullException ("array");
			if (arrayIndex < 0)
				throw new ArgumentOutOfRangeException ("arrayIndex");
			if (array.Length - arrayIndex <= 0 ||
					(array.Length - arrayIndex) < 1)
				throw new ArgumentException ("arrayIndex");
			array [arrayIndex + 0] = _1;
		}
		#endregion
		#region IEnumerable

		/// <summary>
		///   Returns an <see cref="T:System.Collections.IEnumerator"/> for the current instance.
		/// </summary>
		/// <returns>
		///   An <see cref="T:System.Collections.IEnumerator"/> for the current instance.
		/// </returns>
		/// <remarks>
		///   <para>
		///    Returns each value in the <see cref="T:Mono.Rocks.Tuple{T}"/> as an
		///    <see cref="T:System.Object"/>, in the order 
		///    <see cref="P:Mono.Rocks.Tuple`1._1"/>.
		///   </para>
		/// </remarks>
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator ();
		}

		#endregion
		#region IEnumerable<T>

		/// <summary>
		///   Returns an <see cref="T:System.Collections.Generic.IEnumerator{System.Object}"/> for the current instance.
		/// </summary>
		/// <returns>
		///   An <see cref="T:System.Collections.Generic.IEnumerator{System.Object}"/> for the current instance.
		/// </returns>
		/// <remarks>
		///   <para>
		///    Returns each value in the <see cref="T:Mono.Rocks.Tuple{T}"/> as an
		///    <see cref="T:System.Object"/>, in the order 
		///    <see cref="P:Mono.Rocks.Tuple`1._1"/>.
		///   </para>
		/// </remarks>
		public IEnumerator<object> GetEnumerator ()
		{
			yield return _1;
		}

		#endregion
		#region IList

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		int IList.Add (object value)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList.Clear ()
		{
			throw new NotSupportedException ();
		}

		/// <param name="value">
		///   The <see cref="T:System.Object"/> to locate in the current instance.
		/// </param>
		/// <summary>
		///   Determines whether <paramref name="value"/>
		///   <see cref="M:System.Object.Equals(System.Object)"/> any value within this
		///   <see cref="T:Mono.Rocks.Tuple{T}"/> instance.
		/// </summary>
		/// <returns>
		///   <see langword="true"/> if <paramref name="value"/> is
		///   contained in the current instance; otherwise, <see langword="false"/>.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="note">
		///     This method determines equality by calling 
		///     <see cref="M:System.Object.Equls(System.Object,System.Object)"/> on
		///     each value contained within this tuple and <paramref name="value"/>.
		///    </block>
		///   </para>
		/// </remarks>
		bool IList.Contains (object value)
		{
			return ((IList) this).IndexOf (value) >= 0;
		}

		/// <param name="value">
		///   The <see cref="T:System.Object"/> to locate in the current instance.
		/// </param>
		/// <summary>
		///   Searches the current instance, returning the index of
		///   the first occurrence of the specified <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		///   A <see cref="T:System.Int32"/> that specifies the
		///   index of the first occurrence of <paramref name="value"/> in the current
		///   instance, if found; otherwise, -1.
		/// </returns>
		/// <remarks>
		///   This method uses
		///   <see cref="M:System.Object.Equals(System.Object,System.Object)"/> to determine
		///   value equality.
		/// </remarks>
		int IList.IndexOf (object value)
		{
			if (object.Equals (_1, value)) return 0;
			return -1;
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList.Insert (int index, object value)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList.Remove (object value)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList.RemoveAt (int index)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Gets a <see cref="T:System.Boolean"/> indicating
		///   whether the <see cref="P:Mono.Rocks.Tuple`1.System#Collections#ICollection#Count"/>
		///   cannot be changed.
		/// </summary>
		/// <value>
		///   <see langword="true"/>
		/// </value>
		/// <returns>
		///   <see cref="T:Mono.Rocks.Tuple{T}"/> is immutable; its size cannot be changed.
		/// </returns>
		bool IList.IsFixedSize {get {return true;}}

		/// <summary>
		///   Gets a value indicating whether the current instance is read-only
		/// </summary>
		/// <value>
		///   <see langword="true"/>.
		/// </value>
		/// <remarks>
		///   This property always returns <see langword="true"/>.
		/// </remarks>
		bool IList.IsReadOnly {get {return true;}}

		#endregion
		#region IList<T>

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList<object>.Insert (int index, object item)
		{
			throw new NotSupportedException ();
		}

		/// <param name="value">
		///   The <see cref="T:System.Object"/> to locate in the current instance.
		/// </param>
		/// <summary>
		///   Searches the current instance, returning the index of
		///   the first occurrence of the specified <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		///   A <see cref="T:System.Int32"/> that specifies the
		///   index of the first occurrence of <paramref name="value"/> in the current
		///   instance, if found; otherwise, -1.
		/// </returns>
		/// <remarks>
		///   This method uses
		///   <see cref="M:System.Object.Equals(System.Object,System.Object)"/> to determine
		///   value equality.
		/// </remarks>
		int IList<object>.IndexOf (object value)
		{
			if (object.Equals (_1, value)) return 0;
			return -1;
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList<object>.RemoveAt (int index)
		{
			throw new NotSupportedException ();
		}
		/// <param name="index">
		///   An <see cref="T:System.Int32"/> that specifies the zero-based index
		///   of the value in the current instance to get.  This value is >= 0, and 
		///   less than <see cref="P:Mono.Rocks.Tuple`1.System#Collections#Generic#ICollection{System#Object}#Count"/>.
		/// </param>
		/// <summary>
		///   Gets the value at the specified index in the current instance.
		/// </summary>
		/// <value>
		///   The element at the specified <paramref name="index"/> of the current instance.
		/// </value>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index"/> is less than 0 or greater than or equal to 
		///   <see cref="P:Mono.Rocks.Tuple`1.System#Collections#Generic#ICollection{System#Object}#Count"/>.
		/// </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   The setter was called.
		/// </exception>
		/// <remarks>
		///   <see cref="T:Mono.Rocks.Tuple{T}"/> is immutable; the setter cannot be invoked.
		/// </remarks>

		public object this [int index] {
			get {
				switch (index) {
					case 0: return _1;
				}
				throw new ArgumentOutOfRangeException ("index");
			}
			set {throw new NotSupportedException ("Tuple is read-only");}
		}

		#endregion

		/// <typeparam name="TResult">
		///   The return type.
		/// </typeparam>
		/// <param name="func">
		///   A <see cref="T:System.Func{T,TResult}"/> which will be invoked, providing the values
		///   <see cref="P:Mono.Rocks.Tuple`1._1"/> to <paramref name="func"/> and
		///   returning the value returned by <paramref name="func"/>.
		/// </param>
		/// <summary>
		///   Converts the <see cref="T:Mono.Rocks.Tuple{T}"/> into a <typeparamref name="TResult"/>.
		/// </summary>
		/// <returns>
		///   The <typeparamref name="TResult"/> returned by <paramref name="func"/>.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="behaviors">
		///     Passes the values <see cref="P:Mono.Rocks.Tuple`1._1"/> to 
		///     <paramref name="func"/>, returning the value produced by 
		///   	<paramref name="func"/>.
		///    </block>
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="func"/> is <see langword="null"/>.
		/// </exception>
		public TResult Aggregate<TResult> (Func<T, TResult> func)
		{
			if (func == null)
				throw new ArgumentNullException ("func");
			return func (value);
		}

		/// <typeparam name="TResult">
		///   The return type.
		/// </typeparam>
		/// <param name="matchers">
		///   A <see cref="T:System.Func{T,Mono.Rocks.Maybe{TResult}}" /> 
		///   array containing the conversion routines to use to convert 
		///   the current <see cref="T:Mono.Rocks.Tuple{T}" /> instance into a 
		///   <typeparamref name="TResult" /> value.
		/// </param>
		/// <summary>
		///   Converts the current <see cref="T:Mono.Rocks.Tuple{T}"/> instance into a <typeparamref name="TResult"/>.
		/// </summary>
		/// <returns>
		///   The <typeparamref name="TResult"/> returned by one of the <paramref name="matchers"/>.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="behaviors">
		///     <para>
		///      The current <see cref="T:Mono.Rocks.Tuple{T}" /> instance is converted into a 
		///      <typeparamref name="TResult" /> instance by trying each
		///      <see cref="T:System.Func{T,Mono.Rocks.Maybe{TResult}}" />
		///      within <paramref name="matchers" />.
		///     </para>
		///     <para>
		///      This method returns 
		///      <see cref="P:Mono.Rocks.Maybe{TResult}.Value" /> 
		///      for the first delegate to return a
		///      <see cref="T:Mono.Rocks.Maybe{TResult}" /> instance
		///      where <see cref="P:Mono.Rocks.Maybe{TResult}.HasValue" />
		///      is <see langword="true" />.
		///     </para>
		///     <para>
		///      If no 
		///      <see cref="T:System.Func{TSource,Mono.Rocks.Maybe{TResult}}" />
		///      returns a 
		///      <see cref="T:Mono.Rocks.Maybe{TResult}" /> instance
		///      where <see cref="P:Mono.Rocks.Maybe{TResult}.HasValue" />
		///      is <see langword="true" />, then an
		///      <see cref="T:System.InvalidOperationException" /> is thrown.
		///     </para>
		///    </block>
		///    <code lang="C#">
		///   var    a = Tuple.Create (1, 2);
		///   string b = a.Match (
		///       (t, v) =&gt; Match.When ( t + v == 3, "foo!"),
		///       (t, v) =&gt; "*default*".Just ());
		///   Console.WriteLine (b);  // prints "foo!"</code>
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="matchers"/> is <see langword="null"/>.
		/// </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   None of the 
		///   <see cref="T:System.Func{TSource,Mono.Rocks.Maybe{TResult}}" />
		///   delegates within <paramref name="matchers" /> returned a 
		///   <see cref="T:Mono.Rocks.Maybe{TResult}" /> instance where
		///   <see cref="P:Mono.Rocks.Maybe{TResult}.HasValue" /> was
		///   <see langword="true" />.
		/// </exception>
		public TResult Match<TResult> (params Func<T, Maybe<TResult>>[] matchers)
		{
			if (matchers == null)
				throw new ArgumentNullException ("matchers");
			foreach (var m in matchers) {
				var r = m (value);
				if (r.HasValue)
					return r.Value;
			}
			throw new InvalidOperationException ("no match");
		}

		/// <summary>
		///   Returns a <see cref="T:System.String"/> representation of the value of the current instance.
		/// </summary>
		/// <returns>
		///   A <see cref="T:System.String"/> representation of the value of the current instance.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="behaviors">
		///     Returns <c>(</c>, followed by a comma-separated list of the result of
		///     calling <see cref="M:System.Object.ToString"/> on 
		///     <see cref="P:Mono.Rocks.Tuple`1._1"/>, followed by <c>)</c>.
		///    </block>
		///   </para>
		/// </remarks>
		public override string ToString ()
		{
			StringBuilder buf = new StringBuilder ();
			buf.Append ("(");
			buf.Append (_1);
			buf.Append (")");
			return buf.ToString ();
		}

		/// <param name="a">
		///   The first <see cref="T:Mono.Rocks.Tuple{T}"/> to compare.
		/// </param>
		/// <param name="b">
		///   The second <see cref="T:Mono.Rocks.Tuple{T}"/> to compare.
		/// </param>
		/// <summary>
		///   Returns a <see cref="T:System.Boolean"/> value
		///   indicating whether the two specified values are equal to each other.
		/// </summary>
		/// <returns>
		///   <see langword="true"/> if <paramref name="a"/> and
		///   <paramref name="b"/> represent the same value; otherwise, <see langword="false"/>.
		/// </returns>
		/// <seealso cref="M:Mono.Rocks.Tuple`1.Equals(Mono.Rocks.Tuple{`0})"/>
		public static bool operator==  (Tuple<T> a, Tuple<T> b)
		{
			return a.Equals (b);
		}

		/// <param name="a">
		///   The first <see cref="T:Mono.Rocks.Tuple{T}"/> to compare.
		/// </param>
		/// <param name="b">
		///   The second <see cref="T:Mono.Rocks.Tuple{T}"/> to compare.
		/// </param>
		/// <summary>
		///   Returns a <see cref="T:System.Boolean"/> value
		///   indicating whether the two specified values are not equal to each other.
		/// </summary>
		/// <returns>
		///   <see langword="true"/> if <paramref name="a"/> and
		///   <paramref name="b"/> do not represent the same value; otherwise, <see langword="false"/>.
		/// </returns>
		/// <seealso cref="M:Mono.Rocks.Tuple`1.Equals(Mono.Rocks.Tuple{`0})"/>
		public static bool operator!= (Tuple<T> a, Tuple<T> b)
		{
			return !a.Equals (b);
		}
	}

	/// <typeparam name="T1">
	///   The first value type.
	/// </typeparam>
	/// <typeparam name="T2">
	///   The second value type.
	/// </typeparam>
	/// <summary>
	///   A strongly-typed sequence of 2 variously typed values.
	/// </summary>
	/// <remarks>
	///   <para>
	///    A <c>Tuple</c> is an immutable, strongly typed sequence of variously 
	///    typed values with each value lacking an otherwise meaningful name aside 
	///    from its position.
	///   </para>
	///   <para>
	///    Tuples provide an indexer to access variables by-index in a loosely
	///    typed manner, and provide a set of properties to access variables 
	///    by-index in a strongly typed manner.  Strongly typed properties use
	///    the pattern <c>_N</c>, where <c>N</c> is the ones-based value position.
	///    The indexer, as always, uses 0-based positions.  Thus the value
	///    <see cref="P:Mono.Rocks.Tuple`2._1"/> and <c>tuple[0]</c> refer to the same value,
	///    except <see cref="P:Mono.Rocks.Tuple`2._1"/> is strongly typed, while
	///    <c>tuple[0]</c> is typed as a <see cref="T:System.Object"/> (and thus
	///    potentially boxed).
	///   </para>
	///   <para>
	///    Tuples also implement the common collection interfaces, and all collection
	///    methods that would require mutating the Tuple throw 
	///    <see cref="T:System.NotSupportedException"/>.
	///   </para>
	/// </remarks>
	public struct Tuple<T1, T2>
		: IList, IList<object>, IEquatable<Tuple<T1, T2>>
	{
		// For .SyncRoot implementation
		private static object syncRoot = new object ();

		private T1 value1;
		private T2 value2;

		/// <param name="value1">
		///   A <typeparamref name="T1"/> which is used to initialize the <see cref="P:Mono.Rocks.Tuple{T1, T2}.1"/> property.
		/// </param>
		/// <param name="value2">
		///   A <typeparamref name="T2"/> which is used to initialize the <see cref="P:Mono.Rocks.Tuple{T1, T2}.2"/> property.
		/// </param>
		/// <summary>
		///   Constructs and initializes a new <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> instance.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Constructs and initializes a new <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> instance.
		///   </para>
		/// </remarks>
		public Tuple (T1 value1, T2 value2)
		{
			this.value1 = value1;
			this.value2 = value2;
		}

		/// <summary>
		///   The first tuple value.
		/// </summary>
		/// <value>
		///   A <typeparamref name="T1"/> which is the first tuple value.
		/// </value>
		/// <remarks>
		///   The first tuple value.
		/// </remarks>
		[CLSCompliant (false)]
		public T1 _1 {get{return value1;}}

		/// <summary>
		///   The second tuple value.
		/// </summary>
		/// <value>
		///   A <typeparamref name="T2"/> which is the second tuple value.
		/// </value>
		/// <remarks>
		///   The second tuple value.
		/// </remarks>
		[CLSCompliant (false)]
		public T2 _2 {get{return value2;}}

		/// <summary>
		///   Generates a hash code for the current instance.
		/// </summary>
		/// <returns>
		///   A <see cref="T:System.Int32"/> containing the hash code for this instance.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="note">
		///     This method overrides <see cref="M:System.Object.GetHashCode"/>.
		///    </block>
		///   </para>
		/// </remarks>
		public override int GetHashCode ()
		{
			int hc = 0;
			hc ^= _1.GetHashCode ();
			hc ^= _2.GetHashCode ();
			return hc;
		}

		/// <param name="obj">
		///   A <see cref="T:System.Object"/> to compare this instance against.
		/// </param>
		/// <summary>
		///   Determines whether the current instance and the specified object have the same value.
		/// </summary>
		/// <returns>
		///   <para>
		///    <see langword="true"/> if <paramref name="obj"/> is a 
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> and each member of <paramref name="obj"/>
		///    and the current instance have the same value (according to
		///    <see cref="M:System.Object.Equals(System.Object)"/>); otherwise
		///    <see langword="false"/> is returned.
		///   </para>
		/// </returns>
		/// <remarks>
		///   <para>
		///    This method checks for value equality 
		///    (<see cref="M:System.Object.Equals(System.Object)"/>), as defined by each
		///    value type.
		///   </para>
		///   <para>
		///    <block subset="none" type="note">
		///     This method overrides <see cref="M:System.Object.Equals(System.Object)"/>.
		///    </block>
		///   </para>
		/// </remarks>
		public override bool Equals (object obj)
		{
			if (!(obj is Tuple<T1, T2>))
				return false;
			return Equals ((Tuple<T1, T2>) obj);
		}

		/// <param name="obj">
		///   A <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> to compare this instance against.
		/// </param>
		/// <summary>
		///   Determines whether the current instance and the specified <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> have the same value.
		/// </summary>
		/// <returns>
		///   <para>
		///    <see langword="true"/> if each member of <paramref name="obj"/>
		///    and the current instance have the same value (according to
		///    <see cref="M:System.Object.Equals(System.Object)"/>); otherwise
		///    <see langword="false"/> is returned.
		///   </para>
		/// </returns>
		/// <remarks>
		///   <para>
		///    This method checks for value equality 
		///    (<see cref="M:System.Object.Equals(System.Object)"/>), as defined by each
		///    value type.
		///   </para>
		/// </remarks>
		public bool Equals (Tuple<T1, T2> obj)
		{
			return EqualityComparer<T1>.Default.Equals (_1, obj._1)
				&& EqualityComparer<T2>.Default.Equals (_2, obj._2)
				;
		}
		#region ICollection

		/// <param name="array">
		///   The one-dimensional
		///   <see cref="T:System.Array"/> that is the destination for the values
		///   to be copied from the current instance.
		/// </param>
		/// <param name="index">
		///   A <see cref="T:System.Int32"/> that specifies
		///   the first index of <paramref name="array"/> to which the elements of the
		///   current <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> are copied.
		/// </param>
		/// <summary>
		///   Copies the values of the current 
		///   <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> to the specified <see cref="T:System.Array"/>,
		///   starting at the <paramref name="index"/> index of the array.
		/// </summary>
		/// <exception cref="T:System.ArgumentException">
		///   <para>
		///    <paramref name="index" /> is >= 
		///    <paramref name="array"/>'s <see cref="P:System.Array.Length"/> property.
		///   </para>
		///   <para>-or-</para>
		///   <para>
		///    <paramref name="index" /> + <see cref="P:System.Collections.ICollection.Count"/>
		///    of the current instance is >
		///    <paramref name="array"/>'s <see cref="P:System.Array.Length"/> property.
		///   </para>
		/// </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array"/> is <see langword="null"/>.
		/// </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index"/> is negative.
		/// </exception>
		/// <exception cref="T:System.InvalidCastException">
		///   At least one element in the current instance is not
		///   assignment-compatible with the type of <paramref name="array"/>.
		/// </exception>
		void ICollection.CopyTo (Array array, int index)
		{
			if (array == null)
				throw new ArgumentNullException ("array");
			if (index < 0)
				throw new ArgumentOutOfRangeException ("index");
			if (array.Length - index <= 0 ||
					(array.Length - index) < 2)
				throw new ArgumentException ("index");
			array.SetValue (_1, index + 0);
			array.SetValue (_2, index + 1);
		}

		/// <summary>
		///   The number of values in the <see cref="T:Mono.Rocks.Tuple{T1, T2}"/>.
		/// </summary>
		/// <value>
		///   A <see cref="T:System.Int32"/> containing the number 
		///   of values in this <see cref="T:Mono.Rocks.Tuple{T1, T2}"/>.
		/// </value>
		/// <remarks>
		///   This values is always equal to 2.
		/// </remarks>
		int ICollection.Count {
			get {return 2;}
		}

		/// <summary>
		///   Gets a value indicating whether access ot the 
		///   current instance is synchronized (thread-safe)
		/// </summary>
		/// <value>
		///   This property always returns <see langword="true"/>.
		/// </value>
		/// <remarks>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> instance are immutable, and thus the instances
		///    themselves are always thread-safe.  However, this does not mean that 
		///    the values exposed by the tuple are thread safe, so care should be 
		///    taken if necessary.
		///   </para>
		/// </remarks>
		bool ICollection.IsSynchronized  {get {return true;}}

		/// <summary>
		///   Gets an object that can be used to synchronize access
		///   to the current instance.
		/// </summary>
		/// <value>
		///   A <see cref="T:System.Object"/> that can be used to
		///   synchronize access to the current instance.
		/// </value>
		/// <remarks>
		///   <para>
		///    This property shouldn't be used, as <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> instances
		///    are immutable, and thus require no locking.
		///   </para>
		///   <para>
		///    The object returned is shared by all <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> instances.
		///   </para>
		/// </remarks>
		object ICollection.SyncRoot {get {return syncRoot;}}
		#endregion

		#region ICollection<T>

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void ICollection<object>.Add (object item)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void ICollection<object>.Clear ()
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		bool ICollection<object>.Remove (object item)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Gets a value indicating whether the current instance is read-only
		/// </summary>
		/// <value>
		///   <see langword="true"/>.
		/// </value>
		/// <remarks>
		///   This property always returns <see langword="true"/>.
		/// </remarks>
		bool ICollection<object>.IsReadOnly {get {return true;}}

		/// <param name="value">
		///   The <see cref="T:System.Object"/> to locate in the current instance.
		/// </param>
		/// <summary>
		///   Determines whether <paramref name="value"/>
		///   <see cref="M:System.Object.Equals(System.Object)"/> any value within this
		///   <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> instance.
		/// </summary>
		/// <returns>
		///   <see langword="true"/> if <paramref name="value"/> is
		///   contained in the current instance; otherwise, <see langword="false"/>.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="note">
		///     This method determines equality by calling 
		///     <see cref="M:System.Object.Equls(System.Object,System.Object)"/> on
		///     each value contained within this tuple and <paramref name="value"/>.
		///    </block>
		///   </para>
		/// </remarks>
		bool ICollection<object>.Contains (object value)
		{
			return ((IList<object>) this).IndexOf (value) >= 0;
		}

		/// <summary>
		///   The number of values in the <see cref="T:Mono.Rocks.Tuple{T1, T2}"/>.
		/// </summary>
		/// <value>
		///   A <see cref="T:System.Int32"/> containing the number 
		///   of values in this <see cref="T:Mono.Rocks.Tuple{T1, T2}"/>.
		/// </value>
		/// <remarks>
		///   This values is always equal to 2.
		/// </remarks>
		int ICollection<object>.Count {
			get {return 2;}
		}

		/// <param name="array">
		///   The one-dimensional
		///   <see cref="T:System.Array"/> that is the destination for the values
		///   to be copied from the current instance.
		/// </param>
		/// <param name="arrayIndex">
		///   A <see cref="T:System.Int32"/> that specifies
		///   the first index of <paramref name="array"/> to which the elements of the
		///   current <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> are copied.
		/// </param>
		/// <summary>
		///   Copies the values of the current 
		///   <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> to the specified <see cref="T:System.Array"/>,
		///   starting at the <paramref name="arrayIndex"/> index of the array.
		/// </summary>
		/// <exception cref="T:System.ArgumentException">
		///   <para>
		///    <paramref name="arrayIndex" /> is >= 
		///    <paramref name="array"/>'s <see cref="P:System.Array.Length"/> property.
		///   </para>
		///   <para>-or-</para>
		///   <para>
		///    <paramref name="arrayIndex" /> + <see cref="P:System.Collections.ICollection.Count"/>
		///    of the current instance is >
		///    <paramref name="array"/>'s <see cref="P:System.Array.Length"/> property.
		///   </para>
		/// </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array"/> is <see langword="null"/>.
		/// </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex"/> is negative.
		/// </exception>
		void ICollection<object>.CopyTo (object[] array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentNullException ("array");
			if (arrayIndex < 0)
				throw new ArgumentOutOfRangeException ("arrayIndex");
			if (array.Length - arrayIndex <= 0 ||
					(array.Length - arrayIndex) < 2)
				throw new ArgumentException ("arrayIndex");
			array [arrayIndex + 0] = _1;
			array [arrayIndex + 1] = _2;
		}
		#endregion
		#region IEnumerable

		/// <summary>
		///   Returns an <see cref="T:System.Collections.IEnumerator"/> for the current instance.
		/// </summary>
		/// <returns>
		///   An <see cref="T:System.Collections.IEnumerator"/> for the current instance.
		/// </returns>
		/// <remarks>
		///   <para>
		///    Returns each value in the <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> as an
		///    <see cref="T:System.Object"/>, in the order 
		///    <see cref="P:Mono.Rocks.Tuple`2._1"/>, <see cref="P:Mono.Rocks.Tuple`2._2"/>.
		///   </para>
		/// </remarks>
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator ();
		}

		#endregion
		#region IEnumerable<T>

		/// <summary>
		///   Returns an <see cref="T:System.Collections.Generic.IEnumerator{System.Object}"/> for the current instance.
		/// </summary>
		/// <returns>
		///   An <see cref="T:System.Collections.Generic.IEnumerator{System.Object}"/> for the current instance.
		/// </returns>
		/// <remarks>
		///   <para>
		///    Returns each value in the <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> as an
		///    <see cref="T:System.Object"/>, in the order 
		///    <see cref="P:Mono.Rocks.Tuple`2._1"/>, <see cref="P:Mono.Rocks.Tuple`2._2"/>.
		///   </para>
		/// </remarks>
		public IEnumerator<object> GetEnumerator ()
		{
			yield return _1;
			yield return _2;
		}

		#endregion
		#region IList

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		int IList.Add (object value)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList.Clear ()
		{
			throw new NotSupportedException ();
		}

		/// <param name="value">
		///   The <see cref="T:System.Object"/> to locate in the current instance.
		/// </param>
		/// <summary>
		///   Determines whether <paramref name="value"/>
		///   <see cref="M:System.Object.Equals(System.Object)"/> any value within this
		///   <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> instance.
		/// </summary>
		/// <returns>
		///   <see langword="true"/> if <paramref name="value"/> is
		///   contained in the current instance; otherwise, <see langword="false"/>.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="note">
		///     This method determines equality by calling 
		///     <see cref="M:System.Object.Equls(System.Object,System.Object)"/> on
		///     each value contained within this tuple and <paramref name="value"/>.
		///    </block>
		///   </para>
		/// </remarks>
		bool IList.Contains (object value)
		{
			return ((IList) this).IndexOf (value) >= 0;
		}

		/// <param name="value">
		///   The <see cref="T:System.Object"/> to locate in the current instance.
		/// </param>
		/// <summary>
		///   Searches the current instance, returning the index of
		///   the first occurrence of the specified <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		///   A <see cref="T:System.Int32"/> that specifies the
		///   index of the first occurrence of <paramref name="value"/> in the current
		///   instance, if found; otherwise, -1.
		/// </returns>
		/// <remarks>
		///   This method uses
		///   <see cref="M:System.Object.Equals(System.Object,System.Object)"/> to determine
		///   value equality.
		/// </remarks>
		int IList.IndexOf (object value)
		{
			if (object.Equals (_1, value)) return 0;
			if (object.Equals (_2, value)) return 1;
			return -1;
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList.Insert (int index, object value)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList.Remove (object value)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList.RemoveAt (int index)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Gets a <see cref="T:System.Boolean"/> indicating
		///   whether the <see cref="P:Mono.Rocks.Tuple`2.System#Collections#ICollection#Count"/>
		///   cannot be changed.
		/// </summary>
		/// <value>
		///   <see langword="true"/>
		/// </value>
		/// <returns>
		///   <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> is immutable; its size cannot be changed.
		/// </returns>
		bool IList.IsFixedSize {get {return true;}}

		/// <summary>
		///   Gets a value indicating whether the current instance is read-only
		/// </summary>
		/// <value>
		///   <see langword="true"/>.
		/// </value>
		/// <remarks>
		///   This property always returns <see langword="true"/>.
		/// </remarks>
		bool IList.IsReadOnly {get {return true;}}

		#endregion
		#region IList<T>

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList<object>.Insert (int index, object item)
		{
			throw new NotSupportedException ();
		}

		/// <param name="value">
		///   The <see cref="T:System.Object"/> to locate in the current instance.
		/// </param>
		/// <summary>
		///   Searches the current instance, returning the index of
		///   the first occurrence of the specified <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		///   A <see cref="T:System.Int32"/> that specifies the
		///   index of the first occurrence of <paramref name="value"/> in the current
		///   instance, if found; otherwise, -1.
		/// </returns>
		/// <remarks>
		///   This method uses
		///   <see cref="M:System.Object.Equals(System.Object,System.Object)"/> to determine
		///   value equality.
		/// </remarks>
		int IList<object>.IndexOf (object value)
		{
			if (object.Equals (_1, value)) return 0;
			if (object.Equals (_2, value)) return 1;
			return -1;
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList<object>.RemoveAt (int index)
		{
			throw new NotSupportedException ();
		}
		/// <param name="index">
		///   An <see cref="T:System.Int32"/> that specifies the zero-based index
		///   of the value in the current instance to get.  This value is >= 0, and 
		///   less than <see cref="P:Mono.Rocks.Tuple`2.System#Collections#Generic#ICollection{System#Object}#Count"/>.
		/// </param>
		/// <summary>
		///   Gets the value at the specified index in the current instance.
		/// </summary>
		/// <value>
		///   The element at the specified <paramref name="index"/> of the current instance.
		/// </value>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index"/> is less than 0 or greater than or equal to 
		///   <see cref="P:Mono.Rocks.Tuple`2.System#Collections#Generic#ICollection{System#Object}#Count"/>.
		/// </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   The setter was called.
		/// </exception>
		/// <remarks>
		///   <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> is immutable; the setter cannot be invoked.
		/// </remarks>

		public object this [int index] {
			get {
				switch (index) {
					case 0: return _1;
					case 1: return _2;
				}
				throw new ArgumentOutOfRangeException ("index");
			}
			set {throw new NotSupportedException ("Tuple is read-only");}
		}

		#endregion

		/// <typeparam name="TResult">
		///   The return type.
		/// </typeparam>
		/// <param name="func">
		///   A <see cref="T:System.Func{T1,T2,TResult}"/> which will be invoked, providing the values
		///   <see cref="P:Mono.Rocks.Tuple`2._1"/>, <see cref="P:Mono.Rocks.Tuple`2._2"/> to <paramref name="func"/> and
		///   returning the value returned by <paramref name="func"/>.
		/// </param>
		/// <summary>
		///   Converts the <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> into a <typeparamref name="TResult"/>.
		/// </summary>
		/// <returns>
		///   The <typeparamref name="TResult"/> returned by <paramref name="func"/>.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="behaviors">
		///     Passes the values <see cref="P:Mono.Rocks.Tuple`2._1"/>, <see cref="P:Mono.Rocks.Tuple`2._2"/> to 
		///     <paramref name="func"/>, returning the value produced by 
		///   	<paramref name="func"/>.
		///    </block>
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="func"/> is <see langword="null"/>.
		/// </exception>
		public TResult Aggregate<TResult> (Func<T1, T2, TResult> func)
		{
			if (func == null)
				throw new ArgumentNullException ("func");
			return func (value1, value2);
		}

		/// <typeparam name="TResult">
		///   The return type.
		/// </typeparam>
		/// <param name="matchers">
		///   A <see cref="T:System.Func{T1,T2,Mono.Rocks.Maybe{TResult}}" /> 
		///   array containing the conversion routines to use to convert 
		///   the current <see cref="T:Mono.Rocks.Tuple{T1, T2}" /> instance into a 
		///   <typeparamref name="TResult" /> value.
		/// </param>
		/// <summary>
		///   Converts the current <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> instance into a <typeparamref name="TResult"/>.
		/// </summary>
		/// <returns>
		///   The <typeparamref name="TResult"/> returned by one of the <paramref name="matchers"/>.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="behaviors">
		///     <para>
		///      The current <see cref="T:Mono.Rocks.Tuple{T1, T2}" /> instance is converted into a 
		///      <typeparamref name="TResult" /> instance by trying each
		///      <see cref="T:System.Func{T1,T2,Mono.Rocks.Maybe{TResult}}" />
		///      within <paramref name="matchers" />.
		///     </para>
		///     <para>
		///      This method returns 
		///      <see cref="P:Mono.Rocks.Maybe{TResult}.Value" /> 
		///      for the first delegate to return a
		///      <see cref="T:Mono.Rocks.Maybe{TResult}" /> instance
		///      where <see cref="P:Mono.Rocks.Maybe{TResult}.HasValue" />
		///      is <see langword="true" />.
		///     </para>
		///     <para>
		///      If no 
		///      <see cref="T:System.Func{TSource,Mono.Rocks.Maybe{TResult}}" />
		///      returns a 
		///      <see cref="T:Mono.Rocks.Maybe{TResult}" /> instance
		///      where <see cref="P:Mono.Rocks.Maybe{TResult}.HasValue" />
		///      is <see langword="true" />, then an
		///      <see cref="T:System.InvalidOperationException" /> is thrown.
		///     </para>
		///    </block>
		///    <code lang="C#">
		///   var    a = Tuple.Create (1, 2);
		///   string b = a.Match (
		///       (t, v) =&gt; Match.When ( t + v == 3, "foo!"),
		///       (t, v) =&gt; "*default*".Just ());
		///   Console.WriteLine (b);  // prints "foo!"</code>
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="matchers"/> is <see langword="null"/>.
		/// </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   None of the 
		///   <see cref="T:System.Func{TSource,Mono.Rocks.Maybe{TResult}}" />
		///   delegates within <paramref name="matchers" /> returned a 
		///   <see cref="T:Mono.Rocks.Maybe{TResult}" /> instance where
		///   <see cref="P:Mono.Rocks.Maybe{TResult}.HasValue" /> was
		///   <see langword="true" />.
		/// </exception>
		public TResult Match<TResult> (params Func<T1, T2, Maybe<TResult>>[] matchers)
		{
			if (matchers == null)
				throw new ArgumentNullException ("matchers");
			foreach (var m in matchers) {
				var r = m (value1, value2);
				if (r.HasValue)
					return r.Value;
			}
			throw new InvalidOperationException ("no match");
		}

		/// <summary>
		///   Returns a <see cref="T:System.String"/> representation of the value of the current instance.
		/// </summary>
		/// <returns>
		///   A <see cref="T:System.String"/> representation of the value of the current instance.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="behaviors">
		///     Returns <c>(</c>, followed by a comma-separated list of the result of
		///     calling <see cref="M:System.Object.ToString"/> on 
		///     <see cref="P:Mono.Rocks.Tuple`2._1"/>, <see cref="P:Mono.Rocks.Tuple`2._2"/>, followed by <c>)</c>.
		///    </block>
		///   </para>
		/// </remarks>
		public override string ToString ()
		{
			StringBuilder buf = new StringBuilder ();
			buf.Append ("(");
			buf.Append (_1);
			buf.Append (", ");
			buf.Append (_2);
			buf.Append (")");
			return buf.ToString ();
		}

		/// <param name="a">
		///   The first <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> to compare.
		/// </param>
		/// <param name="b">
		///   The second <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> to compare.
		/// </param>
		/// <summary>
		///   Returns a <see cref="T:System.Boolean"/> value
		///   indicating whether the two specified values are equal to each other.
		/// </summary>
		/// <returns>
		///   <see langword="true"/> if <paramref name="a"/> and
		///   <paramref name="b"/> represent the same value; otherwise, <see langword="false"/>.
		/// </returns>
		/// <seealso cref="M:Mono.Rocks.Tuple`2.Equals(Mono.Rocks.Tuple{`0,`1})"/>
		public static bool operator==  (Tuple<T1, T2> a, Tuple<T1, T2> b)
		{
			return a.Equals (b);
		}

		/// <param name="a">
		///   The first <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> to compare.
		/// </param>
		/// <param name="b">
		///   The second <see cref="T:Mono.Rocks.Tuple{T1, T2}"/> to compare.
		/// </param>
		/// <summary>
		///   Returns a <see cref="T:System.Boolean"/> value
		///   indicating whether the two specified values are not equal to each other.
		/// </summary>
		/// <returns>
		///   <see langword="true"/> if <paramref name="a"/> and
		///   <paramref name="b"/> do not represent the same value; otherwise, <see langword="false"/>.
		/// </returns>
		/// <seealso cref="M:Mono.Rocks.Tuple`2.Equals(Mono.Rocks.Tuple{`0,`1})"/>
		public static bool operator!= (Tuple<T1, T2> a, Tuple<T1, T2> b)
		{
			return !a.Equals (b);
		}
	}

	/// <typeparam name="T1">
	///   The first value type.
	/// </typeparam>
	/// <typeparam name="T2">
	///   The second value type.
	/// </typeparam>
	/// <typeparam name="T3">
	///   The third value type.
	/// </typeparam>
	/// <summary>
	///   A strongly-typed sequence of 3 variously typed values.
	/// </summary>
	/// <remarks>
	///   <para>
	///    A <c>Tuple</c> is an immutable, strongly typed sequence of variously 
	///    typed values with each value lacking an otherwise meaningful name aside 
	///    from its position.
	///   </para>
	///   <para>
	///    Tuples provide an indexer to access variables by-index in a loosely
	///    typed manner, and provide a set of properties to access variables 
	///    by-index in a strongly typed manner.  Strongly typed properties use
	///    the pattern <c>_N</c>, where <c>N</c> is the ones-based value position.
	///    The indexer, as always, uses 0-based positions.  Thus the value
	///    <see cref="P:Mono.Rocks.Tuple`3._1"/> and <c>tuple[0]</c> refer to the same value,
	///    except <see cref="P:Mono.Rocks.Tuple`3._1"/> is strongly typed, while
	///    <c>tuple[0]</c> is typed as a <see cref="T:System.Object"/> (and thus
	///    potentially boxed).
	///   </para>
	///   <para>
	///    Tuples also implement the common collection interfaces, and all collection
	///    methods that would require mutating the Tuple throw 
	///    <see cref="T:System.NotSupportedException"/>.
	///   </para>
	/// </remarks>
	public struct Tuple<T1, T2, T3>
		: IList, IList<object>, IEquatable<Tuple<T1, T2, T3>>
	{
		// For .SyncRoot implementation
		private static object syncRoot = new object ();

		private T1 value1;
		private T2 value2;
		private T3 value3;

		/// <param name="value1">
		///   A <typeparamref name="T1"/> which is used to initialize the <see cref="P:Mono.Rocks.Tuple{T1, T2, T3}.1"/> property.
		/// </param>
		/// <param name="value2">
		///   A <typeparamref name="T2"/> which is used to initialize the <see cref="P:Mono.Rocks.Tuple{T1, T2, T3}.2"/> property.
		/// </param>
		/// <param name="value3">
		///   A <typeparamref name="T3"/> which is used to initialize the <see cref="P:Mono.Rocks.Tuple{T1, T2, T3}.3"/> property.
		/// </param>
		/// <summary>
		///   Constructs and initializes a new <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> instance.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Constructs and initializes a new <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> instance.
		///   </para>
		/// </remarks>
		public Tuple (T1 value1, T2 value2, T3 value3)
		{
			this.value1 = value1;
			this.value2 = value2;
			this.value3 = value3;
		}

		/// <summary>
		///   The first tuple value.
		/// </summary>
		/// <value>
		///   A <typeparamref name="T1"/> which is the first tuple value.
		/// </value>
		/// <remarks>
		///   The first tuple value.
		/// </remarks>
		[CLSCompliant (false)]
		public T1 _1 {get{return value1;}}

		/// <summary>
		///   The second tuple value.
		/// </summary>
		/// <value>
		///   A <typeparamref name="T2"/> which is the second tuple value.
		/// </value>
		/// <remarks>
		///   The second tuple value.
		/// </remarks>
		[CLSCompliant (false)]
		public T2 _2 {get{return value2;}}

		/// <summary>
		///   The third tuple value.
		/// </summary>
		/// <value>
		///   A <typeparamref name="T3"/> which is the third tuple value.
		/// </value>
		/// <remarks>
		///   The third tuple value.
		/// </remarks>
		[CLSCompliant (false)]
		public T3 _3 {get{return value3;}}

		/// <summary>
		///   Generates a hash code for the current instance.
		/// </summary>
		/// <returns>
		///   A <see cref="T:System.Int32"/> containing the hash code for this instance.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="note">
		///     This method overrides <see cref="M:System.Object.GetHashCode"/>.
		///    </block>
		///   </para>
		/// </remarks>
		public override int GetHashCode ()
		{
			int hc = 0;
			hc ^= _1.GetHashCode ();
			hc ^= _2.GetHashCode ();
			hc ^= _3.GetHashCode ();
			return hc;
		}

		/// <param name="obj">
		///   A <see cref="T:System.Object"/> to compare this instance against.
		/// </param>
		/// <summary>
		///   Determines whether the current instance and the specified object have the same value.
		/// </summary>
		/// <returns>
		///   <para>
		///    <see langword="true"/> if <paramref name="obj"/> is a 
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> and each member of <paramref name="obj"/>
		///    and the current instance have the same value (according to
		///    <see cref="M:System.Object.Equals(System.Object)"/>); otherwise
		///    <see langword="false"/> is returned.
		///   </para>
		/// </returns>
		/// <remarks>
		///   <para>
		///    This method checks for value equality 
		///    (<see cref="M:System.Object.Equals(System.Object)"/>), as defined by each
		///    value type.
		///   </para>
		///   <para>
		///    <block subset="none" type="note">
		///     This method overrides <see cref="M:System.Object.Equals(System.Object)"/>.
		///    </block>
		///   </para>
		/// </remarks>
		public override bool Equals (object obj)
		{
			if (!(obj is Tuple<T1, T2, T3>))
				return false;
			return Equals ((Tuple<T1, T2, T3>) obj);
		}

		/// <param name="obj">
		///   A <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> to compare this instance against.
		/// </param>
		/// <summary>
		///   Determines whether the current instance and the specified <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> have the same value.
		/// </summary>
		/// <returns>
		///   <para>
		///    <see langword="true"/> if each member of <paramref name="obj"/>
		///    and the current instance have the same value (according to
		///    <see cref="M:System.Object.Equals(System.Object)"/>); otherwise
		///    <see langword="false"/> is returned.
		///   </para>
		/// </returns>
		/// <remarks>
		///   <para>
		///    This method checks for value equality 
		///    (<see cref="M:System.Object.Equals(System.Object)"/>), as defined by each
		///    value type.
		///   </para>
		/// </remarks>
		public bool Equals (Tuple<T1, T2, T3> obj)
		{
			return EqualityComparer<T1>.Default.Equals (_1, obj._1)
				&& EqualityComparer<T2>.Default.Equals (_2, obj._2)
				&& EqualityComparer<T3>.Default.Equals (_3, obj._3)
				;
		}
		#region ICollection

		/// <param name="array">
		///   The one-dimensional
		///   <see cref="T:System.Array"/> that is the destination for the values
		///   to be copied from the current instance.
		/// </param>
		/// <param name="index">
		///   A <see cref="T:System.Int32"/> that specifies
		///   the first index of <paramref name="array"/> to which the elements of the
		///   current <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> are copied.
		/// </param>
		/// <summary>
		///   Copies the values of the current 
		///   <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> to the specified <see cref="T:System.Array"/>,
		///   starting at the <paramref name="index"/> index of the array.
		/// </summary>
		/// <exception cref="T:System.ArgumentException">
		///   <para>
		///    <paramref name="index" /> is >= 
		///    <paramref name="array"/>'s <see cref="P:System.Array.Length"/> property.
		///   </para>
		///   <para>-or-</para>
		///   <para>
		///    <paramref name="index" /> + <see cref="P:System.Collections.ICollection.Count"/>
		///    of the current instance is >
		///    <paramref name="array"/>'s <see cref="P:System.Array.Length"/> property.
		///   </para>
		/// </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array"/> is <see langword="null"/>.
		/// </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index"/> is negative.
		/// </exception>
		/// <exception cref="T:System.InvalidCastException">
		///   At least one element in the current instance is not
		///   assignment-compatible with the type of <paramref name="array"/>.
		/// </exception>
		void ICollection.CopyTo (Array array, int index)
		{
			if (array == null)
				throw new ArgumentNullException ("array");
			if (index < 0)
				throw new ArgumentOutOfRangeException ("index");
			if (array.Length - index <= 0 ||
					(array.Length - index) < 3)
				throw new ArgumentException ("index");
			array.SetValue (_1, index + 0);
			array.SetValue (_2, index + 1);
			array.SetValue (_3, index + 2);
		}

		/// <summary>
		///   The number of values in the <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/>.
		/// </summary>
		/// <value>
		///   A <see cref="T:System.Int32"/> containing the number 
		///   of values in this <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/>.
		/// </value>
		/// <remarks>
		///   This values is always equal to 3.
		/// </remarks>
		int ICollection.Count {
			get {return 3;}
		}

		/// <summary>
		///   Gets a value indicating whether access ot the 
		///   current instance is synchronized (thread-safe)
		/// </summary>
		/// <value>
		///   This property always returns <see langword="true"/>.
		/// </value>
		/// <remarks>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> instance are immutable, and thus the instances
		///    themselves are always thread-safe.  However, this does not mean that 
		///    the values exposed by the tuple are thread safe, so care should be 
		///    taken if necessary.
		///   </para>
		/// </remarks>
		bool ICollection.IsSynchronized  {get {return true;}}

		/// <summary>
		///   Gets an object that can be used to synchronize access
		///   to the current instance.
		/// </summary>
		/// <value>
		///   A <see cref="T:System.Object"/> that can be used to
		///   synchronize access to the current instance.
		/// </value>
		/// <remarks>
		///   <para>
		///    This property shouldn't be used, as <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> instances
		///    are immutable, and thus require no locking.
		///   </para>
		///   <para>
		///    The object returned is shared by all <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> instances.
		///   </para>
		/// </remarks>
		object ICollection.SyncRoot {get {return syncRoot;}}
		#endregion

		#region ICollection<T>

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void ICollection<object>.Add (object item)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void ICollection<object>.Clear ()
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		bool ICollection<object>.Remove (object item)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Gets a value indicating whether the current instance is read-only
		/// </summary>
		/// <value>
		///   <see langword="true"/>.
		/// </value>
		/// <remarks>
		///   This property always returns <see langword="true"/>.
		/// </remarks>
		bool ICollection<object>.IsReadOnly {get {return true;}}

		/// <param name="value">
		///   The <see cref="T:System.Object"/> to locate in the current instance.
		/// </param>
		/// <summary>
		///   Determines whether <paramref name="value"/>
		///   <see cref="M:System.Object.Equals(System.Object)"/> any value within this
		///   <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> instance.
		/// </summary>
		/// <returns>
		///   <see langword="true"/> if <paramref name="value"/> is
		///   contained in the current instance; otherwise, <see langword="false"/>.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="note">
		///     This method determines equality by calling 
		///     <see cref="M:System.Object.Equls(System.Object,System.Object)"/> on
		///     each value contained within this tuple and <paramref name="value"/>.
		///    </block>
		///   </para>
		/// </remarks>
		bool ICollection<object>.Contains (object value)
		{
			return ((IList<object>) this).IndexOf (value) >= 0;
		}

		/// <summary>
		///   The number of values in the <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/>.
		/// </summary>
		/// <value>
		///   A <see cref="T:System.Int32"/> containing the number 
		///   of values in this <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/>.
		/// </value>
		/// <remarks>
		///   This values is always equal to 3.
		/// </remarks>
		int ICollection<object>.Count {
			get {return 3;}
		}

		/// <param name="array">
		///   The one-dimensional
		///   <see cref="T:System.Array"/> that is the destination for the values
		///   to be copied from the current instance.
		/// </param>
		/// <param name="arrayIndex">
		///   A <see cref="T:System.Int32"/> that specifies
		///   the first index of <paramref name="array"/> to which the elements of the
		///   current <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> are copied.
		/// </param>
		/// <summary>
		///   Copies the values of the current 
		///   <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> to the specified <see cref="T:System.Array"/>,
		///   starting at the <paramref name="arrayIndex"/> index of the array.
		/// </summary>
		/// <exception cref="T:System.ArgumentException">
		///   <para>
		///    <paramref name="arrayIndex" /> is >= 
		///    <paramref name="array"/>'s <see cref="P:System.Array.Length"/> property.
		///   </para>
		///   <para>-or-</para>
		///   <para>
		///    <paramref name="arrayIndex" /> + <see cref="P:System.Collections.ICollection.Count"/>
		///    of the current instance is >
		///    <paramref name="array"/>'s <see cref="P:System.Array.Length"/> property.
		///   </para>
		/// </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array"/> is <see langword="null"/>.
		/// </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex"/> is negative.
		/// </exception>
		void ICollection<object>.CopyTo (object[] array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentNullException ("array");
			if (arrayIndex < 0)
				throw new ArgumentOutOfRangeException ("arrayIndex");
			if (array.Length - arrayIndex <= 0 ||
					(array.Length - arrayIndex) < 3)
				throw new ArgumentException ("arrayIndex");
			array [arrayIndex + 0] = _1;
			array [arrayIndex + 1] = _2;
			array [arrayIndex + 2] = _3;
		}
		#endregion
		#region IEnumerable

		/// <summary>
		///   Returns an <see cref="T:System.Collections.IEnumerator"/> for the current instance.
		/// </summary>
		/// <returns>
		///   An <see cref="T:System.Collections.IEnumerator"/> for the current instance.
		/// </returns>
		/// <remarks>
		///   <para>
		///    Returns each value in the <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> as an
		///    <see cref="T:System.Object"/>, in the order 
		///    <see cref="P:Mono.Rocks.Tuple`3._1"/>, <see cref="P:Mono.Rocks.Tuple`3._2"/>, <see cref="P:Mono.Rocks.Tuple`3._3"/>.
		///   </para>
		/// </remarks>
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator ();
		}

		#endregion
		#region IEnumerable<T>

		/// <summary>
		///   Returns an <see cref="T:System.Collections.Generic.IEnumerator{System.Object}"/> for the current instance.
		/// </summary>
		/// <returns>
		///   An <see cref="T:System.Collections.Generic.IEnumerator{System.Object}"/> for the current instance.
		/// </returns>
		/// <remarks>
		///   <para>
		///    Returns each value in the <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> as an
		///    <see cref="T:System.Object"/>, in the order 
		///    <see cref="P:Mono.Rocks.Tuple`3._1"/>, <see cref="P:Mono.Rocks.Tuple`3._2"/>, <see cref="P:Mono.Rocks.Tuple`3._3"/>.
		///   </para>
		/// </remarks>
		public IEnumerator<object> GetEnumerator ()
		{
			yield return _1;
			yield return _2;
			yield return _3;
		}

		#endregion
		#region IList

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		int IList.Add (object value)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList.Clear ()
		{
			throw new NotSupportedException ();
		}

		/// <param name="value">
		///   The <see cref="T:System.Object"/> to locate in the current instance.
		/// </param>
		/// <summary>
		///   Determines whether <paramref name="value"/>
		///   <see cref="M:System.Object.Equals(System.Object)"/> any value within this
		///   <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> instance.
		/// </summary>
		/// <returns>
		///   <see langword="true"/> if <paramref name="value"/> is
		///   contained in the current instance; otherwise, <see langword="false"/>.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="note">
		///     This method determines equality by calling 
		///     <see cref="M:System.Object.Equls(System.Object,System.Object)"/> on
		///     each value contained within this tuple and <paramref name="value"/>.
		///    </block>
		///   </para>
		/// </remarks>
		bool IList.Contains (object value)
		{
			return ((IList) this).IndexOf (value) >= 0;
		}

		/// <param name="value">
		///   The <see cref="T:System.Object"/> to locate in the current instance.
		/// </param>
		/// <summary>
		///   Searches the current instance, returning the index of
		///   the first occurrence of the specified <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		///   A <see cref="T:System.Int32"/> that specifies the
		///   index of the first occurrence of <paramref name="value"/> in the current
		///   instance, if found; otherwise, -1.
		/// </returns>
		/// <remarks>
		///   This method uses
		///   <see cref="M:System.Object.Equals(System.Object,System.Object)"/> to determine
		///   value equality.
		/// </remarks>
		int IList.IndexOf (object value)
		{
			if (object.Equals (_1, value)) return 0;
			if (object.Equals (_2, value)) return 1;
			if (object.Equals (_3, value)) return 2;
			return -1;
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList.Insert (int index, object value)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList.Remove (object value)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList.RemoveAt (int index)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Gets a <see cref="T:System.Boolean"/> indicating
		///   whether the <see cref="P:Mono.Rocks.Tuple`3.System#Collections#ICollection#Count"/>
		///   cannot be changed.
		/// </summary>
		/// <value>
		///   <see langword="true"/>
		/// </value>
		/// <returns>
		///   <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> is immutable; its size cannot be changed.
		/// </returns>
		bool IList.IsFixedSize {get {return true;}}

		/// <summary>
		///   Gets a value indicating whether the current instance is read-only
		/// </summary>
		/// <value>
		///   <see langword="true"/>.
		/// </value>
		/// <remarks>
		///   This property always returns <see langword="true"/>.
		/// </remarks>
		bool IList.IsReadOnly {get {return true;}}

		#endregion
		#region IList<T>

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList<object>.Insert (int index, object item)
		{
			throw new NotSupportedException ();
		}

		/// <param name="value">
		///   The <see cref="T:System.Object"/> to locate in the current instance.
		/// </param>
		/// <summary>
		///   Searches the current instance, returning the index of
		///   the first occurrence of the specified <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		///   A <see cref="T:System.Int32"/> that specifies the
		///   index of the first occurrence of <paramref name="value"/> in the current
		///   instance, if found; otherwise, -1.
		/// </returns>
		/// <remarks>
		///   This method uses
		///   <see cref="M:System.Object.Equals(System.Object,System.Object)"/> to determine
		///   value equality.
		/// </remarks>
		int IList<object>.IndexOf (object value)
		{
			if (object.Equals (_1, value)) return 0;
			if (object.Equals (_2, value)) return 1;
			if (object.Equals (_3, value)) return 2;
			return -1;
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList<object>.RemoveAt (int index)
		{
			throw new NotSupportedException ();
		}
		/// <param name="index">
		///   An <see cref="T:System.Int32"/> that specifies the zero-based index
		///   of the value in the current instance to get.  This value is >= 0, and 
		///   less than <see cref="P:Mono.Rocks.Tuple`3.System#Collections#Generic#ICollection{System#Object}#Count"/>.
		/// </param>
		/// <summary>
		///   Gets the value at the specified index in the current instance.
		/// </summary>
		/// <value>
		///   The element at the specified <paramref name="index"/> of the current instance.
		/// </value>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index"/> is less than 0 or greater than or equal to 
		///   <see cref="P:Mono.Rocks.Tuple`3.System#Collections#Generic#ICollection{System#Object}#Count"/>.
		/// </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   The setter was called.
		/// </exception>
		/// <remarks>
		///   <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> is immutable; the setter cannot be invoked.
		/// </remarks>

		public object this [int index] {
			get {
				switch (index) {
					case 0: return _1;
					case 1: return _2;
					case 2: return _3;
				}
				throw new ArgumentOutOfRangeException ("index");
			}
			set {throw new NotSupportedException ("Tuple is read-only");}
		}

		#endregion

		/// <typeparam name="TResult">
		///   The return type.
		/// </typeparam>
		/// <param name="func">
		///   A <see cref="T:System.Func{T1,T2,T3,TResult}"/> which will be invoked, providing the values
		///   <see cref="P:Mono.Rocks.Tuple`3._1"/>, <see cref="P:Mono.Rocks.Tuple`3._2"/>, <see cref="P:Mono.Rocks.Tuple`3._3"/> to <paramref name="func"/> and
		///   returning the value returned by <paramref name="func"/>.
		/// </param>
		/// <summary>
		///   Converts the <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> into a <typeparamref name="TResult"/>.
		/// </summary>
		/// <returns>
		///   The <typeparamref name="TResult"/> returned by <paramref name="func"/>.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="behaviors">
		///     Passes the values <see cref="P:Mono.Rocks.Tuple`3._1"/>, <see cref="P:Mono.Rocks.Tuple`3._2"/>, <see cref="P:Mono.Rocks.Tuple`3._3"/> to 
		///     <paramref name="func"/>, returning the value produced by 
		///   	<paramref name="func"/>.
		///    </block>
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="func"/> is <see langword="null"/>.
		/// </exception>
		public TResult Aggregate<TResult> (Func<T1, T2, T3, TResult> func)
		{
			if (func == null)
				throw new ArgumentNullException ("func");
			return func (value1, value2, value3);
		}

		/// <typeparam name="TResult">
		///   The return type.
		/// </typeparam>
		/// <param name="matchers">
		///   A <see cref="T:System.Func{T1,T2,T3,Mono.Rocks.Maybe{TResult}}" /> 
		///   array containing the conversion routines to use to convert 
		///   the current <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}" /> instance into a 
		///   <typeparamref name="TResult" /> value.
		/// </param>
		/// <summary>
		///   Converts the current <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> instance into a <typeparamref name="TResult"/>.
		/// </summary>
		/// <returns>
		///   The <typeparamref name="TResult"/> returned by one of the <paramref name="matchers"/>.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="behaviors">
		///     <para>
		///      The current <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}" /> instance is converted into a 
		///      <typeparamref name="TResult" /> instance by trying each
		///      <see cref="T:System.Func{T1,T2,T3,Mono.Rocks.Maybe{TResult}}" />
		///      within <paramref name="matchers" />.
		///     </para>
		///     <para>
		///      This method returns 
		///      <see cref="P:Mono.Rocks.Maybe{TResult}.Value" /> 
		///      for the first delegate to return a
		///      <see cref="T:Mono.Rocks.Maybe{TResult}" /> instance
		///      where <see cref="P:Mono.Rocks.Maybe{TResult}.HasValue" />
		///      is <see langword="true" />.
		///     </para>
		///     <para>
		///      If no 
		///      <see cref="T:System.Func{TSource,Mono.Rocks.Maybe{TResult}}" />
		///      returns a 
		///      <see cref="T:Mono.Rocks.Maybe{TResult}" /> instance
		///      where <see cref="P:Mono.Rocks.Maybe{TResult}.HasValue" />
		///      is <see langword="true" />, then an
		///      <see cref="T:System.InvalidOperationException" /> is thrown.
		///     </para>
		///    </block>
		///    <code lang="C#">
		///   var    a = Tuple.Create (1, 2);
		///   string b = a.Match (
		///       (t, v) =&gt; Match.When ( t + v == 3, "foo!"),
		///       (t, v) =&gt; "*default*".Just ());
		///   Console.WriteLine (b);  // prints "foo!"</code>
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="matchers"/> is <see langword="null"/>.
		/// </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   None of the 
		///   <see cref="T:System.Func{TSource,Mono.Rocks.Maybe{TResult}}" />
		///   delegates within <paramref name="matchers" /> returned a 
		///   <see cref="T:Mono.Rocks.Maybe{TResult}" /> instance where
		///   <see cref="P:Mono.Rocks.Maybe{TResult}.HasValue" /> was
		///   <see langword="true" />.
		/// </exception>
		public TResult Match<TResult> (params Func<T1, T2, T3, Maybe<TResult>>[] matchers)
		{
			if (matchers == null)
				throw new ArgumentNullException ("matchers");
			foreach (var m in matchers) {
				var r = m (value1, value2, value3);
				if (r.HasValue)
					return r.Value;
			}
			throw new InvalidOperationException ("no match");
		}

		/// <summary>
		///   Returns a <see cref="T:System.String"/> representation of the value of the current instance.
		/// </summary>
		/// <returns>
		///   A <see cref="T:System.String"/> representation of the value of the current instance.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="behaviors">
		///     Returns <c>(</c>, followed by a comma-separated list of the result of
		///     calling <see cref="M:System.Object.ToString"/> on 
		///     <see cref="P:Mono.Rocks.Tuple`3._1"/>, <see cref="P:Mono.Rocks.Tuple`3._2"/>, <see cref="P:Mono.Rocks.Tuple`3._3"/>, followed by <c>)</c>.
		///    </block>
		///   </para>
		/// </remarks>
		public override string ToString ()
		{
			StringBuilder buf = new StringBuilder ();
			buf.Append ("(");
			buf.Append (_1);
			buf.Append (", ");
			buf.Append (_2);
			buf.Append (", ");
			buf.Append (_3);
			buf.Append (")");
			return buf.ToString ();
		}

		/// <param name="a">
		///   The first <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> to compare.
		/// </param>
		/// <param name="b">
		///   The second <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> to compare.
		/// </param>
		/// <summary>
		///   Returns a <see cref="T:System.Boolean"/> value
		///   indicating whether the two specified values are equal to each other.
		/// </summary>
		/// <returns>
		///   <see langword="true"/> if <paramref name="a"/> and
		///   <paramref name="b"/> represent the same value; otherwise, <see langword="false"/>.
		/// </returns>
		/// <seealso cref="M:Mono.Rocks.Tuple`3.Equals(Mono.Rocks.Tuple{`0,`1,`2})"/>
		public static bool operator==  (Tuple<T1, T2, T3> a, Tuple<T1, T2, T3> b)
		{
			return a.Equals (b);
		}

		/// <param name="a">
		///   The first <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> to compare.
		/// </param>
		/// <param name="b">
		///   The second <see cref="T:Mono.Rocks.Tuple{T1, T2, T3}"/> to compare.
		/// </param>
		/// <summary>
		///   Returns a <see cref="T:System.Boolean"/> value
		///   indicating whether the two specified values are not equal to each other.
		/// </summary>
		/// <returns>
		///   <see langword="true"/> if <paramref name="a"/> and
		///   <paramref name="b"/> do not represent the same value; otherwise, <see langword="false"/>.
		/// </returns>
		/// <seealso cref="M:Mono.Rocks.Tuple`3.Equals(Mono.Rocks.Tuple{`0,`1,`2})"/>
		public static bool operator!= (Tuple<T1, T2, T3> a, Tuple<T1, T2, T3> b)
		{
			return !a.Equals (b);
		}
	}

	/// <typeparam name="T1">
	///   The first value type.
	/// </typeparam>
	/// <typeparam name="T2">
	///   The second value type.
	/// </typeparam>
	/// <typeparam name="T3">
	///   The third value type.
	/// </typeparam>
	/// <typeparam name="T4">
	///   The fourth value type.
	/// </typeparam>
	/// <summary>
	///   A strongly-typed sequence of 4 variously typed values.
	/// </summary>
	/// <remarks>
	///   <para>
	///    A <c>Tuple</c> is an immutable, strongly typed sequence of variously 
	///    typed values with each value lacking an otherwise meaningful name aside 
	///    from its position.
	///   </para>
	///   <para>
	///    Tuples provide an indexer to access variables by-index in a loosely
	///    typed manner, and provide a set of properties to access variables 
	///    by-index in a strongly typed manner.  Strongly typed properties use
	///    the pattern <c>_N</c>, where <c>N</c> is the ones-based value position.
	///    The indexer, as always, uses 0-based positions.  Thus the value
	///    <see cref="P:Mono.Rocks.Tuple`4._1"/> and <c>tuple[0]</c> refer to the same value,
	///    except <see cref="P:Mono.Rocks.Tuple`4._1"/> is strongly typed, while
	///    <c>tuple[0]</c> is typed as a <see cref="T:System.Object"/> (and thus
	///    potentially boxed).
	///   </para>
	///   <para>
	///    Tuples also implement the common collection interfaces, and all collection
	///    methods that would require mutating the Tuple throw 
	///    <see cref="T:System.NotSupportedException"/>.
	///   </para>
	/// </remarks>
	public struct Tuple<T1, T2, T3, T4>
		: IList, IList<object>, IEquatable<Tuple<T1, T2, T3, T4>>
	{
		// For .SyncRoot implementation
		private static object syncRoot = new object ();

		private T1 value1;
		private T2 value2;
		private T3 value3;
		private T4 value4;

		/// <param name="value1">
		///   A <typeparamref name="T1"/> which is used to initialize the <see cref="P:Mono.Rocks.Tuple{T1, T2, T3, T4}.1"/> property.
		/// </param>
		/// <param name="value2">
		///   A <typeparamref name="T2"/> which is used to initialize the <see cref="P:Mono.Rocks.Tuple{T1, T2, T3, T4}.2"/> property.
		/// </param>
		/// <param name="value3">
		///   A <typeparamref name="T3"/> which is used to initialize the <see cref="P:Mono.Rocks.Tuple{T1, T2, T3, T4}.3"/> property.
		/// </param>
		/// <param name="value4">
		///   A <typeparamref name="T4"/> which is used to initialize the <see cref="P:Mono.Rocks.Tuple{T1, T2, T3, T4}.4"/> property.
		/// </param>
		/// <summary>
		///   Constructs and initializes a new <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> instance.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Constructs and initializes a new <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> instance.
		///   </para>
		/// </remarks>
		public Tuple (T1 value1, T2 value2, T3 value3, T4 value4)
		{
			this.value1 = value1;
			this.value2 = value2;
			this.value3 = value3;
			this.value4 = value4;
		}

		/// <summary>
		///   The first tuple value.
		/// </summary>
		/// <value>
		///   A <typeparamref name="T1"/> which is the first tuple value.
		/// </value>
		/// <remarks>
		///   The first tuple value.
		/// </remarks>
		[CLSCompliant (false)]
		public T1 _1 {get{return value1;}}

		/// <summary>
		///   The second tuple value.
		/// </summary>
		/// <value>
		///   A <typeparamref name="T2"/> which is the second tuple value.
		/// </value>
		/// <remarks>
		///   The second tuple value.
		/// </remarks>
		[CLSCompliant (false)]
		public T2 _2 {get{return value2;}}

		/// <summary>
		///   The third tuple value.
		/// </summary>
		/// <value>
		///   A <typeparamref name="T3"/> which is the third tuple value.
		/// </value>
		/// <remarks>
		///   The third tuple value.
		/// </remarks>
		[CLSCompliant (false)]
		public T3 _3 {get{return value3;}}

		/// <summary>
		///   The fourth tuple value.
		/// </summary>
		/// <value>
		///   A <typeparamref name="T4"/> which is the fourth tuple value.
		/// </value>
		/// <remarks>
		///   The fourth tuple value.
		/// </remarks>
		[CLSCompliant (false)]
		public T4 _4 {get{return value4;}}

		/// <summary>
		///   Generates a hash code for the current instance.
		/// </summary>
		/// <returns>
		///   A <see cref="T:System.Int32"/> containing the hash code for this instance.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="note">
		///     This method overrides <see cref="M:System.Object.GetHashCode"/>.
		///    </block>
		///   </para>
		/// </remarks>
		public override int GetHashCode ()
		{
			int hc = 0;
			hc ^= _1.GetHashCode ();
			hc ^= _2.GetHashCode ();
			hc ^= _3.GetHashCode ();
			hc ^= _4.GetHashCode ();
			return hc;
		}

		/// <param name="obj">
		///   A <see cref="T:System.Object"/> to compare this instance against.
		/// </param>
		/// <summary>
		///   Determines whether the current instance and the specified object have the same value.
		/// </summary>
		/// <returns>
		///   <para>
		///    <see langword="true"/> if <paramref name="obj"/> is a 
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> and each member of <paramref name="obj"/>
		///    and the current instance have the same value (according to
		///    <see cref="M:System.Object.Equals(System.Object)"/>); otherwise
		///    <see langword="false"/> is returned.
		///   </para>
		/// </returns>
		/// <remarks>
		///   <para>
		///    This method checks for value equality 
		///    (<see cref="M:System.Object.Equals(System.Object)"/>), as defined by each
		///    value type.
		///   </para>
		///   <para>
		///    <block subset="none" type="note">
		///     This method overrides <see cref="M:System.Object.Equals(System.Object)"/>.
		///    </block>
		///   </para>
		/// </remarks>
		public override bool Equals (object obj)
		{
			if (!(obj is Tuple<T1, T2, T3, T4>))
				return false;
			return Equals ((Tuple<T1, T2, T3, T4>) obj);
		}

		/// <param name="obj">
		///   A <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> to compare this instance against.
		/// </param>
		/// <summary>
		///   Determines whether the current instance and the specified <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> have the same value.
		/// </summary>
		/// <returns>
		///   <para>
		///    <see langword="true"/> if each member of <paramref name="obj"/>
		///    and the current instance have the same value (according to
		///    <see cref="M:System.Object.Equals(System.Object)"/>); otherwise
		///    <see langword="false"/> is returned.
		///   </para>
		/// </returns>
		/// <remarks>
		///   <para>
		///    This method checks for value equality 
		///    (<see cref="M:System.Object.Equals(System.Object)"/>), as defined by each
		///    value type.
		///   </para>
		/// </remarks>
		public bool Equals (Tuple<T1, T2, T3, T4> obj)
		{
			return EqualityComparer<T1>.Default.Equals (_1, obj._1)
				&& EqualityComparer<T2>.Default.Equals (_2, obj._2)
				&& EqualityComparer<T3>.Default.Equals (_3, obj._3)
				&& EqualityComparer<T4>.Default.Equals (_4, obj._4)
				;
		}
		#region ICollection

		/// <param name="array">
		///   The one-dimensional
		///   <see cref="T:System.Array"/> that is the destination for the values
		///   to be copied from the current instance.
		/// </param>
		/// <param name="index">
		///   A <see cref="T:System.Int32"/> that specifies
		///   the first index of <paramref name="array"/> to which the elements of the
		///   current <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> are copied.
		/// </param>
		/// <summary>
		///   Copies the values of the current 
		///   <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> to the specified <see cref="T:System.Array"/>,
		///   starting at the <paramref name="index"/> index of the array.
		/// </summary>
		/// <exception cref="T:System.ArgumentException">
		///   <para>
		///    <paramref name="index" /> is >= 
		///    <paramref name="array"/>'s <see cref="P:System.Array.Length"/> property.
		///   </para>
		///   <para>-or-</para>
		///   <para>
		///    <paramref name="index" /> + <see cref="P:System.Collections.ICollection.Count"/>
		///    of the current instance is >
		///    <paramref name="array"/>'s <see cref="P:System.Array.Length"/> property.
		///   </para>
		/// </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array"/> is <see langword="null"/>.
		/// </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index"/> is negative.
		/// </exception>
		/// <exception cref="T:System.InvalidCastException">
		///   At least one element in the current instance is not
		///   assignment-compatible with the type of <paramref name="array"/>.
		/// </exception>
		void ICollection.CopyTo (Array array, int index)
		{
			if (array == null)
				throw new ArgumentNullException ("array");
			if (index < 0)
				throw new ArgumentOutOfRangeException ("index");
			if (array.Length - index <= 0 ||
					(array.Length - index) < 4)
				throw new ArgumentException ("index");
			array.SetValue (_1, index + 0);
			array.SetValue (_2, index + 1);
			array.SetValue (_3, index + 2);
			array.SetValue (_4, index + 3);
		}

		/// <summary>
		///   The number of values in the <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/>.
		/// </summary>
		/// <value>
		///   A <see cref="T:System.Int32"/> containing the number 
		///   of values in this <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/>.
		/// </value>
		/// <remarks>
		///   This values is always equal to 4.
		/// </remarks>
		int ICollection.Count {
			get {return 4;}
		}

		/// <summary>
		///   Gets a value indicating whether access ot the 
		///   current instance is synchronized (thread-safe)
		/// </summary>
		/// <value>
		///   This property always returns <see langword="true"/>.
		/// </value>
		/// <remarks>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> instance are immutable, and thus the instances
		///    themselves are always thread-safe.  However, this does not mean that 
		///    the values exposed by the tuple are thread safe, so care should be 
		///    taken if necessary.
		///   </para>
		/// </remarks>
		bool ICollection.IsSynchronized  {get {return true;}}

		/// <summary>
		///   Gets an object that can be used to synchronize access
		///   to the current instance.
		/// </summary>
		/// <value>
		///   A <see cref="T:System.Object"/> that can be used to
		///   synchronize access to the current instance.
		/// </value>
		/// <remarks>
		///   <para>
		///    This property shouldn't be used, as <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> instances
		///    are immutable, and thus require no locking.
		///   </para>
		///   <para>
		///    The object returned is shared by all <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> instances.
		///   </para>
		/// </remarks>
		object ICollection.SyncRoot {get {return syncRoot;}}
		#endregion

		#region ICollection<T>

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void ICollection<object>.Add (object item)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void ICollection<object>.Clear ()
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		bool ICollection<object>.Remove (object item)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Gets a value indicating whether the current instance is read-only
		/// </summary>
		/// <value>
		///   <see langword="true"/>.
		/// </value>
		/// <remarks>
		///   This property always returns <see langword="true"/>.
		/// </remarks>
		bool ICollection<object>.IsReadOnly {get {return true;}}

		/// <param name="value">
		///   The <see cref="T:System.Object"/> to locate in the current instance.
		/// </param>
		/// <summary>
		///   Determines whether <paramref name="value"/>
		///   <see cref="M:System.Object.Equals(System.Object)"/> any value within this
		///   <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> instance.
		/// </summary>
		/// <returns>
		///   <see langword="true"/> if <paramref name="value"/> is
		///   contained in the current instance; otherwise, <see langword="false"/>.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="note">
		///     This method determines equality by calling 
		///     <see cref="M:System.Object.Equls(System.Object,System.Object)"/> on
		///     each value contained within this tuple and <paramref name="value"/>.
		///    </block>
		///   </para>
		/// </remarks>
		bool ICollection<object>.Contains (object value)
		{
			return ((IList<object>) this).IndexOf (value) >= 0;
		}

		/// <summary>
		///   The number of values in the <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/>.
		/// </summary>
		/// <value>
		///   A <see cref="T:System.Int32"/> containing the number 
		///   of values in this <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/>.
		/// </value>
		/// <remarks>
		///   This values is always equal to 4.
		/// </remarks>
		int ICollection<object>.Count {
			get {return 4;}
		}

		/// <param name="array">
		///   The one-dimensional
		///   <see cref="T:System.Array"/> that is the destination for the values
		///   to be copied from the current instance.
		/// </param>
		/// <param name="arrayIndex">
		///   A <see cref="T:System.Int32"/> that specifies
		///   the first index of <paramref name="array"/> to which the elements of the
		///   current <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> are copied.
		/// </param>
		/// <summary>
		///   Copies the values of the current 
		///   <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> to the specified <see cref="T:System.Array"/>,
		///   starting at the <paramref name="arrayIndex"/> index of the array.
		/// </summary>
		/// <exception cref="T:System.ArgumentException">
		///   <para>
		///    <paramref name="arrayIndex" /> is >= 
		///    <paramref name="array"/>'s <see cref="P:System.Array.Length"/> property.
		///   </para>
		///   <para>-or-</para>
		///   <para>
		///    <paramref name="arrayIndex" /> + <see cref="P:System.Collections.ICollection.Count"/>
		///    of the current instance is >
		///    <paramref name="array"/>'s <see cref="P:System.Array.Length"/> property.
		///   </para>
		/// </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array"/> is <see langword="null"/>.
		/// </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex"/> is negative.
		/// </exception>
		void ICollection<object>.CopyTo (object[] array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentNullException ("array");
			if (arrayIndex < 0)
				throw new ArgumentOutOfRangeException ("arrayIndex");
			if (array.Length - arrayIndex <= 0 ||
					(array.Length - arrayIndex) < 4)
				throw new ArgumentException ("arrayIndex");
			array [arrayIndex + 0] = _1;
			array [arrayIndex + 1] = _2;
			array [arrayIndex + 2] = _3;
			array [arrayIndex + 3] = _4;
		}
		#endregion
		#region IEnumerable

		/// <summary>
		///   Returns an <see cref="T:System.Collections.IEnumerator"/> for the current instance.
		/// </summary>
		/// <returns>
		///   An <see cref="T:System.Collections.IEnumerator"/> for the current instance.
		/// </returns>
		/// <remarks>
		///   <para>
		///    Returns each value in the <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> as an
		///    <see cref="T:System.Object"/>, in the order 
		///    <see cref="P:Mono.Rocks.Tuple`4._1"/>, <see cref="P:Mono.Rocks.Tuple`4._2"/>, <see cref="P:Mono.Rocks.Tuple`4._3"/>, <see cref="P:Mono.Rocks.Tuple`4._4"/>.
		///   </para>
		/// </remarks>
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator ();
		}

		#endregion
		#region IEnumerable<T>

		/// <summary>
		///   Returns an <see cref="T:System.Collections.Generic.IEnumerator{System.Object}"/> for the current instance.
		/// </summary>
		/// <returns>
		///   An <see cref="T:System.Collections.Generic.IEnumerator{System.Object}"/> for the current instance.
		/// </returns>
		/// <remarks>
		///   <para>
		///    Returns each value in the <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> as an
		///    <see cref="T:System.Object"/>, in the order 
		///    <see cref="P:Mono.Rocks.Tuple`4._1"/>, <see cref="P:Mono.Rocks.Tuple`4._2"/>, <see cref="P:Mono.Rocks.Tuple`4._3"/>, <see cref="P:Mono.Rocks.Tuple`4._4"/>.
		///   </para>
		/// </remarks>
		public IEnumerator<object> GetEnumerator ()
		{
			yield return _1;
			yield return _2;
			yield return _3;
			yield return _4;
		}

		#endregion
		#region IList

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		int IList.Add (object value)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList.Clear ()
		{
			throw new NotSupportedException ();
		}

		/// <param name="value">
		///   The <see cref="T:System.Object"/> to locate in the current instance.
		/// </param>
		/// <summary>
		///   Determines whether <paramref name="value"/>
		///   <see cref="M:System.Object.Equals(System.Object)"/> any value within this
		///   <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> instance.
		/// </summary>
		/// <returns>
		///   <see langword="true"/> if <paramref name="value"/> is
		///   contained in the current instance; otherwise, <see langword="false"/>.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="note">
		///     This method determines equality by calling 
		///     <see cref="M:System.Object.Equls(System.Object,System.Object)"/> on
		///     each value contained within this tuple and <paramref name="value"/>.
		///    </block>
		///   </para>
		/// </remarks>
		bool IList.Contains (object value)
		{
			return ((IList) this).IndexOf (value) >= 0;
		}

		/// <param name="value">
		///   The <see cref="T:System.Object"/> to locate in the current instance.
		/// </param>
		/// <summary>
		///   Searches the current instance, returning the index of
		///   the first occurrence of the specified <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		///   A <see cref="T:System.Int32"/> that specifies the
		///   index of the first occurrence of <paramref name="value"/> in the current
		///   instance, if found; otherwise, -1.
		/// </returns>
		/// <remarks>
		///   This method uses
		///   <see cref="M:System.Object.Equals(System.Object,System.Object)"/> to determine
		///   value equality.
		/// </remarks>
		int IList.IndexOf (object value)
		{
			if (object.Equals (_1, value)) return 0;
			if (object.Equals (_2, value)) return 1;
			if (object.Equals (_3, value)) return 2;
			if (object.Equals (_4, value)) return 3;
			return -1;
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList.Insert (int index, object value)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList.Remove (object value)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList.RemoveAt (int index)
		{
			throw new NotSupportedException ();
		}

		/// <summary>
		///   Gets a <see cref="T:System.Boolean"/> indicating
		///   whether the <see cref="P:Mono.Rocks.Tuple`4.System#Collections#ICollection#Count"/>
		///   cannot be changed.
		/// </summary>
		/// <value>
		///   <see langword="true"/>
		/// </value>
		/// <returns>
		///   <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> is immutable; its size cannot be changed.
		/// </returns>
		bool IList.IsFixedSize {get {return true;}}

		/// <summary>
		///   Gets a value indicating whether the current instance is read-only
		/// </summary>
		/// <value>
		///   <see langword="true"/>.
		/// </value>
		/// <remarks>
		///   This property always returns <see langword="true"/>.
		/// </remarks>
		bool IList.IsReadOnly {get {return true;}}

		#endregion
		#region IList<T>

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList<object>.Insert (int index, object item)
		{
			throw new NotSupportedException ();
		}

		/// <param name="value">
		///   The <see cref="T:System.Object"/> to locate in the current instance.
		/// </param>
		/// <summary>
		///   Searches the current instance, returning the index of
		///   the first occurrence of the specified <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		///   A <see cref="T:System.Int32"/> that specifies the
		///   index of the first occurrence of <paramref name="value"/> in the current
		///   instance, if found; otherwise, -1.
		/// </returns>
		/// <remarks>
		///   This method uses
		///   <see cref="M:System.Object.Equals(System.Object,System.Object)"/> to determine
		///   value equality.
		/// </remarks>
		int IList<object>.IndexOf (object value)
		{
			if (object.Equals (_1, value)) return 0;
			if (object.Equals (_2, value)) return 1;
			if (object.Equals (_3, value)) return 2;
			if (object.Equals (_4, value)) return 3;
			return -1;
		}

		/// <summary>
		///   Throws <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <remarks>
		///   <para>
		///    Throws <see cref="T:System.NotSupportedException"/>.
		///   </para>
		///   <para>
		///    <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> is immutable; modification is not supported.
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.NotSupportedException">
		///   This member is not supported.
		/// </exception>
		void IList<object>.RemoveAt (int index)
		{
			throw new NotSupportedException ();
		}
		/// <param name="index">
		///   An <see cref="T:System.Int32"/> that specifies the zero-based index
		///   of the value in the current instance to get.  This value is >= 0, and 
		///   less than <see cref="P:Mono.Rocks.Tuple`4.System#Collections#Generic#ICollection{System#Object}#Count"/>.
		/// </param>
		/// <summary>
		///   Gets the value at the specified index in the current instance.
		/// </summary>
		/// <value>
		///   The element at the specified <paramref name="index"/> of the current instance.
		/// </value>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index"/> is less than 0 or greater than or equal to 
		///   <see cref="P:Mono.Rocks.Tuple`4.System#Collections#Generic#ICollection{System#Object}#Count"/>.
		/// </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   The setter was called.
		/// </exception>
		/// <remarks>
		///   <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> is immutable; the setter cannot be invoked.
		/// </remarks>

		public object this [int index] {
			get {
				switch (index) {
					case 0: return _1;
					case 1: return _2;
					case 2: return _3;
					case 3: return _4;
				}
				throw new ArgumentOutOfRangeException ("index");
			}
			set {throw new NotSupportedException ("Tuple is read-only");}
		}

		#endregion

		/// <typeparam name="TResult">
		///   The return type.
		/// </typeparam>
		/// <param name="func">
		///   A <see cref="T:System.Func{T1,T2,T3,T4,TResult}"/> which will be invoked, providing the values
		///   <see cref="P:Mono.Rocks.Tuple`4._1"/>, <see cref="P:Mono.Rocks.Tuple`4._2"/>, <see cref="P:Mono.Rocks.Tuple`4._3"/>, <see cref="P:Mono.Rocks.Tuple`4._4"/> to <paramref name="func"/> and
		///   returning the value returned by <paramref name="func"/>.
		/// </param>
		/// <summary>
		///   Converts the <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> into a <typeparamref name="TResult"/>.
		/// </summary>
		/// <returns>
		///   The <typeparamref name="TResult"/> returned by <paramref name="func"/>.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="behaviors">
		///     Passes the values <see cref="P:Mono.Rocks.Tuple`4._1"/>, <see cref="P:Mono.Rocks.Tuple`4._2"/>, <see cref="P:Mono.Rocks.Tuple`4._3"/>, <see cref="P:Mono.Rocks.Tuple`4._4"/> to 
		///     <paramref name="func"/>, returning the value produced by 
		///   	<paramref name="func"/>.
		///    </block>
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="func"/> is <see langword="null"/>.
		/// </exception>
		public TResult Aggregate<TResult> (Func<T1, T2, T3, T4, TResult> func)
		{
			if (func == null)
				throw new ArgumentNullException ("func");
			return func (value1, value2, value3, value4);
		}

		/// <typeparam name="TResult">
		///   The return type.
		/// </typeparam>
		/// <param name="matchers">
		///   A <see cref="T:System.Func{T1,T2,T3,T4,Mono.Rocks.Maybe{TResult}}" /> 
		///   array containing the conversion routines to use to convert 
		///   the current <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}" /> instance into a 
		///   <typeparamref name="TResult" /> value.
		/// </param>
		/// <summary>
		///   Converts the current <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> instance into a <typeparamref name="TResult"/>.
		/// </summary>
		/// <returns>
		///   The <typeparamref name="TResult"/> returned by one of the <paramref name="matchers"/>.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="behaviors">
		///     <para>
		///      The current <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}" /> instance is converted into a 
		///      <typeparamref name="TResult" /> instance by trying each
		///      <see cref="T:System.Func{T1,T2,T3,T4,Mono.Rocks.Maybe{TResult}}" />
		///      within <paramref name="matchers" />.
		///     </para>
		///     <para>
		///      This method returns 
		///      <see cref="P:Mono.Rocks.Maybe{TResult}.Value" /> 
		///      for the first delegate to return a
		///      <see cref="T:Mono.Rocks.Maybe{TResult}" /> instance
		///      where <see cref="P:Mono.Rocks.Maybe{TResult}.HasValue" />
		///      is <see langword="true" />.
		///     </para>
		///     <para>
		///      If no 
		///      <see cref="T:System.Func{TSource,Mono.Rocks.Maybe{TResult}}" />
		///      returns a 
		///      <see cref="T:Mono.Rocks.Maybe{TResult}" /> instance
		///      where <see cref="P:Mono.Rocks.Maybe{TResult}.HasValue" />
		///      is <see langword="true" />, then an
		///      <see cref="T:System.InvalidOperationException" /> is thrown.
		///     </para>
		///    </block>
		///    <code lang="C#">
		///   var    a = Tuple.Create (1, 2);
		///   string b = a.Match (
		///       (t, v) =&gt; Match.When ( t + v == 3, "foo!"),
		///       (t, v) =&gt; "*default*".Just ());
		///   Console.WriteLine (b);  // prints "foo!"</code>
		///   </para>
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="matchers"/> is <see langword="null"/>.
		/// </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   None of the 
		///   <see cref="T:System.Func{TSource,Mono.Rocks.Maybe{TResult}}" />
		///   delegates within <paramref name="matchers" /> returned a 
		///   <see cref="T:Mono.Rocks.Maybe{TResult}" /> instance where
		///   <see cref="P:Mono.Rocks.Maybe{TResult}.HasValue" /> was
		///   <see langword="true" />.
		/// </exception>
		public TResult Match<TResult> (params Func<T1, T2, T3, T4, Maybe<TResult>>[] matchers)
		{
			if (matchers == null)
				throw new ArgumentNullException ("matchers");
			foreach (var m in matchers) {
				var r = m (value1, value2, value3, value4);
				if (r.HasValue)
					return r.Value;
			}
			throw new InvalidOperationException ("no match");
		}

		/// <summary>
		///   Returns a <see cref="T:System.String"/> representation of the value of the current instance.
		/// </summary>
		/// <returns>
		///   A <see cref="T:System.String"/> representation of the value of the current instance.
		/// </returns>
		/// <remarks>
		///   <para>
		///    <block subset="none" type="behaviors">
		///     Returns <c>(</c>, followed by a comma-separated list of the result of
		///     calling <see cref="M:System.Object.ToString"/> on 
		///     <see cref="P:Mono.Rocks.Tuple`4._1"/>, <see cref="P:Mono.Rocks.Tuple`4._2"/>, <see cref="P:Mono.Rocks.Tuple`4._3"/>, <see cref="P:Mono.Rocks.Tuple`4._4"/>, followed by <c>)</c>.
		///    </block>
		///   </para>
		/// </remarks>
		public override string ToString ()
		{
			StringBuilder buf = new StringBuilder ();
			buf.Append ("(");
			buf.Append (_1);
			buf.Append (", ");
			buf.Append (_2);
			buf.Append (", ");
			buf.Append (_3);
			buf.Append (", ");
			buf.Append (_4);
			buf.Append (")");
			return buf.ToString ();
		}

		/// <param name="a">
		///   The first <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> to compare.
		/// </param>
		/// <param name="b">
		///   The second <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> to compare.
		/// </param>
		/// <summary>
		///   Returns a <see cref="T:System.Boolean"/> value
		///   indicating whether the two specified values are equal to each other.
		/// </summary>
		/// <returns>
		///   <see langword="true"/> if <paramref name="a"/> and
		///   <paramref name="b"/> represent the same value; otherwise, <see langword="false"/>.
		/// </returns>
		/// <seealso cref="M:Mono.Rocks.Tuple`4.Equals(Mono.Rocks.Tuple{`0,`1,`2,`3})"/>
		public static bool operator==  (Tuple<T1, T2, T3, T4> a, Tuple<T1, T2, T3, T4> b)
		{
			return a.Equals (b);
		}

		/// <param name="a">
		///   The first <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> to compare.
		/// </param>
		/// <param name="b">
		///   The second <see cref="T:Mono.Rocks.Tuple{T1, T2, T3, T4}"/> to compare.
		/// </param>
		/// <summary>
		///   Returns a <see cref="T:System.Boolean"/> value
		///   indicating whether the two specified values are not equal to each other.
		/// </summary>
		/// <returns>
		///   <see langword="true"/> if <paramref name="a"/> and
		///   <paramref name="b"/> do not represent the same value; otherwise, <see langword="false"/>.
		/// </returns>
		/// <seealso cref="M:Mono.Rocks.Tuple`4.Equals(Mono.Rocks.Tuple{`0,`1,`2,`3})"/>
		public static bool operator!= (Tuple<T1, T2, T3, T4> a, Tuple<T1, T2, T3, T4> b)
		{
			return !a.Equals (b);
		}
	}
}
