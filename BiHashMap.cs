using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optomo
{
	public class BiHashMap<TKey, TValue> : IDictionary<TKey, TValue>
	{
		Dictionary<TKey, TValue> ForwardDictionary;
		Dictionary<TValue, TKey> BackwardDictionary;

		public ICollection<TKey> Keys =>( (IDictionary<TKey, TValue>)this.ForwardDictionary ).Keys;
		public ICollection<TValue> Values => ( (IDictionary<TKey, TValue>)this.ForwardDictionary ).Values;
		public int Count => ( (ICollection<KeyValuePair<TKey, TValue>>)this.ForwardDictionary ).Count;
		public bool IsReadOnly => ( (ICollection<KeyValuePair<TKey, TValue>>)this.ForwardDictionary ).IsReadOnly;

		public TValue this [ TKey key ] 
		{ 
			get => ( (IDictionary<TKey, TValue>)this.ForwardDictionary ) [ key ]; 
			set => ( (IDictionary<TKey, TValue>)this.ForwardDictionary ) [ key ]=value; 
		}
		public TKey this [ TValue val ] 
		{ 
			get => ( (IDictionary<TValue, TKey>)this.BackwardDictionary ) [ val ]; 
			set => ( (IDictionary<TValue, TKey>)this.BackwardDictionary ) [ val ] = value; 
		}

		public BiHashMap(TKey key, TValue value) : this()
		{
			ForwardDictionary.Add(key, value);
			BackwardDictionary.Add(value, key);
		}
		public BiHashMap(TValue value, TKey key) : this()
		{
			ForwardDictionary.Add(key, value);
			BackwardDictionary.Add(value, key);
		}
		public BiHashMap()
		{
			ForwardDictionary = new Dictionary<TKey, TValue>();
			BackwardDictionary = new Dictionary<TValue, TKey>();
		}

		public void Add( TKey key, TValue value )
		{
			( (IDictionary<TKey, TValue>)this.ForwardDictionary ).Add( key, value );
			( (IDictionary<TValue, TKey>)this.BackwardDictionary ).Add( value, key );
		}
		public bool ContainsKey( TKey key ) => ( (IDictionary<TKey, TValue>)this.ForwardDictionary ).ContainsKey( key );
		public bool ContainsKey( TValue value ) => ( (IDictionary<TValue, TKey>)this.BackwardDictionary ).ContainsKey( value );
		public bool Remove( TKey key )
		{
			bool found = ( (IDictionary<TKey, TValue>)this.ForwardDictionary ).TryGetValue( key, out TValue value );
			if (found)
			{
				( (IDictionary<TKey, TValue>)this.ForwardDictionary ).Remove(key);
				return ( (IDictionary<TValue, TKey>)this.BackwardDictionary ).Remove(value);
			}
			else { return false; }
		}
		public bool TryGetValue( TKey key, [MaybeNullWhen( false )] out TValue value ) => ( (IDictionary<TKey, TValue>)this.ForwardDictionary ).TryGetValue( key, out value );
		public bool TryGetKey( TValue value, [MaybeNullWhen( false )] out TKey key ) => ( (IDictionary<TValue, TKey>)this.BackwardDictionary ).TryGetValue( value, out key );
		public void Add( KeyValuePair<TKey, TValue> item )
		{
			( (ICollection<KeyValuePair<TKey, TValue>>)this.ForwardDictionary ).Add( item );
			( (ICollection<KeyValuePair<TValue, TKey>>)this.BackwardDictionary ).Add( new KeyValuePair<TValue, TKey>(item.Value, item.Key) );
		}
		public void Clear()
		{
			( (ICollection<KeyValuePair<TKey, TValue>>)this.ForwardDictionary ).Clear();
			( (ICollection<KeyValuePair<TValue, TKey>>)this.BackwardDictionary ).Clear();
		}
		public bool Contains( KeyValuePair<TKey, TValue> item ) => ( (ICollection<KeyValuePair<TKey, TValue>>)this.ForwardDictionary ).Contains( item );
		public bool Contains( KeyValuePair<TValue, TKey> item ) => ( (ICollection<KeyValuePair<TValue, TKey>>)this.BackwardDictionary ).Contains( item );
		public void CopyTo( KeyValuePair<TKey, TValue> [] array, int arrayIndex ) => ( (ICollection<KeyValuePair<TKey, TValue>>)this.ForwardDictionary ).CopyTo( array, arrayIndex );
		public void CopyTo( KeyValuePair<TValue, TKey> [] array, int arrayIndex ) => ( (ICollection<KeyValuePair<TValue, TKey>>)this.BackwardDictionary ).CopyTo( array, arrayIndex );
		public bool Remove( KeyValuePair<TKey, TValue> item ) 
		{
			KeyValuePair<TValue, TKey> pair = new KeyValuePair<TValue, TKey>(item.Value, item.Key);
			( (ICollection<KeyValuePair<TKey, TValue>>)this.ForwardDictionary ).Remove( item );
			
			return ( (ICollection<KeyValuePair<TValue, TKey>>)this.BackwardDictionary ).Remove( pair );
		}
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => ( (IEnumerable<KeyValuePair<TKey, TValue>>)this.ForwardDictionary ).GetEnumerator();
		public IEnumerator<KeyValuePair<TValue, TKey>> GetEnumeratorBackwards() => ( (IEnumerable<KeyValuePair<TValue, TKey>>)this.BackwardDictionary ).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => ( (IEnumerable)this.ForwardDictionary ).GetEnumerator();
	}
}
