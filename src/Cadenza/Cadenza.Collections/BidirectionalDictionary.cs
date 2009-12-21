using System;
using System.Collections;
using System.Collections.Generic;

namespace Cadenza.Collections
{
	public class BidirectionalDictionary<TKey, TValue> : IDictionary<TKey, TValue>
	{
		private readonly IEqualityComparer<TKey> keyComparer;
		private readonly IEqualityComparer<TValue> valueComparer;
		private readonly Dictionary<TKey, TValue> items1;
		private readonly Dictionary<TValue, TKey> items2;
		private readonly BidirectionalDictionary<TValue, TKey> inverse;


		public BidirectionalDictionary() : this(10, null, null) {}

		public BidirectionalDictionary(int capacity) : this(capacity, null, null) {}

		public BidirectionalDictionary(int capacity, IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			if (capacity < 0) throw new ArgumentOutOfRangeException("capacity", capacity, "capacity cannot be less than 0");

			this.keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;
			this.valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;

			items1 = new Dictionary<TKey, TValue>(capacity, this.keyComparer);
			items2 = new Dictionary<TValue, TKey>(capacity, this.valueComparer);

			inverse = new BidirectionalDictionary<TValue, TKey>(this);
		}

		private BidirectionalDictionary(BidirectionalDictionary<TValue, TKey> inverse)
		{
			this.inverse = inverse;
			keyComparer = inverse.valueComparer;
			valueComparer = inverse.keyComparer;
			items2 = inverse.items1;
			items1 = inverse.items2;
		}


		public BidirectionalDictionary<TValue, TKey> Inverse
		{
			get { return inverse; }
		}


		public ICollection<TKey> Keys
		{
			get { return items1.Keys; }
		}

		public ICollection<TValue> Values
		{
			get { return items2.Keys; }
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return items1.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<TKey, TValue>>) items1).CopyTo(array, arrayIndex);
		}


		public bool ContainsKey(TKey key)
		{
			if (key == null) throw new ArgumentNullException("key");
			return items1.ContainsKey(key);
		}

		public bool ContainsValue(TValue value)
		{
			if (value == null) throw new ArgumentNullException("value");
			return items2.ContainsKey(value);
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			return ((ICollection<KeyValuePair<TKey, TValue>>) items1).Contains(item);
		}

		public bool TryGetKey(TValue value, out TKey key)
		{
			if (value == null) throw new ArgumentNullException("value");
			return items2.TryGetValue(value, out key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null) throw new ArgumentNullException("key");
			return items1.TryGetValue(key, out value);
		}

		public TValue this[TKey key]
		{
			get { return items1[key]; }
			set
			{
				if (key == null) throw new ArgumentNullException("key");
				if (value == null) throw new ArgumentNullException("value");

				//no check if key exists as it is expected that an existing key should be overwrote.
				//not sure if an existing value should be overwrote or not, perhaps it should be an option when creating? Currently defaulting to not.
				if (ValueBelongsToOtherKey(key, value)) throw new ArgumentException("Value already exists", "value");

				items1[key] = value;
				items2[value] = key;
			}
		}

		public int Count
		{
			get { return items1.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}


		public void Add(TKey key, TValue value)
		{
			if (key == null) throw new ArgumentNullException("key");
			if (value == null) throw new ArgumentNullException("value");

			if (items1.ContainsKey(key)) throw new ArgumentException("Key already exists", "key");
			if (items2.ContainsKey(value)) throw new ArgumentException("Value already exists", "value");

			items1.Add(key, value);
			items2.Add(value, key);
		}

		public void Replace(TKey key, TValue value)
		{
			if (key == null) throw new ArgumentNullException("key");
			if (value == null) throw new ArgumentNullException("value");

			// replaces a key value pair, if the key or value already exists those mappings will be replaced.
			// e.g. you have; a -> b, b -> a; c -> d, d -> c
			// you add the mapping; a -> d, d -> a
			// this will remove both of the original mappings
			Remove(key);
			inverse.Remove(value);
			Add(key, value);
		}

		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			Add(item.Key, item.Value);
		}

		public bool Remove(TKey key)
		{
			if (key == null) throw new ArgumentNullException("key");

			TValue value;
			if (items1.TryGetValue(key, out value))
			{
				items1.Remove(key);
				items2.Remove(value);
				return true;
			}
			else
			{
				return false;
			}
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			bool removed = ((ICollection<KeyValuePair<TKey, TValue>>) items1).Remove(item);
			if (removed) items2.Remove(item.Value);
			return removed;
		}

		public void Clear()
		{
			items1.Clear();
			items2.Clear();
		}


		private bool ValueBelongsToOtherKey(TKey key, TValue value) 
		{
			TKey otherKey;
			if (items2.TryGetValue(value, out otherKey))
			{
				// if the keys are not equal the value belongs to another key
				return !keyComparer.Equals(key, otherKey);
			}
			else
			{
				// value doesn't exist in map, thus it cannot belong to another key
				return false;
			}
		}
	}
}
