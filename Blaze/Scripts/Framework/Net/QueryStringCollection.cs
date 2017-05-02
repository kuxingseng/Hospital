namespace Blaze.Framework.Net
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// 请求参数集合。
    /// </summary>
    public class QueryStringCollection : IDictionary<string, string>
    {
        #region IDictionary<string,string> Members

        /// <summary>
        /// 返回一个循环访问集合的枚举数。
        /// </summary>
        /// <returns>
        /// 可用于循环访问集合的 <see cref="T:System.Collections.Generic.IEnumerator`1"/>。
        /// </returns>
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return mDictionary.GetEnumerator();
        }

        /// <summary>
        /// 返回一个循环访问集合的枚举数。
        /// </summary>
        /// <returns>
        /// 可用于循环访问集合的 <see cref="T:System.Collections.IEnumerator"/> 对象。
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return mDictionary.GetEnumerator();
        }

        /// <summary>
        /// 将某项添加到 <see cref="T:System.Collections.Generic.ICollection`1"/> 中。
        /// </summary>
        /// <param name="item">要添加到 <see cref="T:System.Collections.Generic.ICollection`1"/> 的对象。</param><exception cref="T:System.NotSupportedException"><see cref="T:System.Collections.Generic.ICollection`1"/> 为只读。</exception>
        public void Add(KeyValuePair<string, string> item)
        {
            mDictionary.Add(item.Key.ToLower(), item.Value);
        }

        /// <summary>
        /// 从 <see cref="T:System.Collections.Generic.ICollection`1"/> 中移除所有项。
        /// </summary>
        /// <exception cref="T:System.NotSupportedException"><see cref="T:System.Collections.Generic.ICollection`1"/> 为只读。 </exception>
        public void Clear()
        {
            mDictionary.Clear();
        }

        /// <summary>
        /// 确定 <see cref="T:System.Collections.Generic.ICollection`1"/> 是否包含特定值。
        /// </summary>
        /// <returns>
        /// 如果在 <see cref="T:System.Collections.Generic.ICollection`1"/> 中找到 <paramref name="item"/>，则为 true；否则为 false。
        /// </returns>
        /// <param name="item">要在 <see cref="T:System.Collections.Generic.ICollection`1"/> 中定位的对象。</param>
        public bool Contains(KeyValuePair<string, string> item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 从特定的 <see cref="T:System.Array"/> 索引开始，将 <see cref="T:System.Collections.Generic.ICollection`1"/> 的元素复制到一个 <see cref="T:System.Array"/> 中。
        /// </summary>
        /// <param name="array">作为从 <see cref="T:System.Collections.Generic.ICollection`1"/> 复制的元素的目标位置的一维 <see cref="T:System.Array"/>。<see cref="T:System.Array"/> 必须具有从零开始的索引。</param><param name="arrayIndex"><paramref name="array"/> 中从零开始的索引，从此处开始复制。</param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> 为 null。</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> 小于 0。</exception><exception cref="T:System.ArgumentException"><paramref name="array"/> 是多维的。- 或 -<paramref name="arrayIndex"/> 等于或大于 <paramref name="array"/> 的长度。- 或 -源 <see cref="T:System.Collections.Generic.ICollection`1"/> 中的元素数目大于从 <paramref name="arrayIndex"/> 到目标 <paramref name="array"/> 末尾之间的可用空间。- 或 -无法自动将类型 <paramref name="T"/> 强制转换为目标 <paramref name="array"/> 的类型。</exception>
        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 从 <see cref="T:System.Collections.Generic.ICollection`1"/> 中移除特定对象的第一个匹配项。
        /// </summary>
        /// <returns>
        /// 如果已从 <see cref="T:System.Collections.Generic.ICollection`1"/> 中成功移除 <paramref name="item"/>，则为 true；否则为 false。如果在原始 <see cref="T:System.Collections.Generic.ICollection`1"/> 中没有找到 <paramref name="item"/>，该方法也会返回 false。
        /// </returns>
        /// <param name="item">要从 <see cref="T:System.Collections.Generic.ICollection`1"/> 中移除的对象。</param><exception cref="T:System.NotSupportedException"><see cref="T:System.Collections.Generic.ICollection`1"/> 为只读。</exception>
        public bool Remove(KeyValuePair<string, string> item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取 <see cref="T:System.Collections.Generic.ICollection`1"/> 中包含的元素数。
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Collections.Generic.ICollection`1"/> 中包含的元素数。
        /// </returns>
        public int Count
        {
            get { return mDictionary.Count; }
        }

        /// <summary>
        /// 获取一个值，该值指示 <see cref="T:System.Collections.Generic.ICollection`1"/> 是否为只读。
        /// </summary>
        /// <returns>
        /// 如果 <see cref="T:System.Collections.Generic.ICollection`1"/> 为只读，则为 true；否则为 false。
        /// </returns>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// 确定 <see cref="T:System.Collections.Generic.IDictionary`2"/> 是否包含具有指定键的元素。
        /// </summary>
        /// <returns>
        /// 如果 <see cref="T:System.Collections.Generic.IDictionary`2"/> 包含带有该键的元素，则为 true；否则，为 false。
        /// </returns>
        /// <param name="key">要在 <see cref="T:System.Collections.Generic.IDictionary`2"/> 中定位的键。</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> 为 null。</exception>
        public bool ContainsKey(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            return mDictionary.ContainsKey(key.ToLower());
        }

        /// <summary>
        /// 在 <see cref="T:System.Collections.Generic.IDictionary`2"/> 中添加一个带有所提供的键和值的元素。
        /// </summary>
        /// <param name="key">用作要添加的元素的键的对象。</param><param name="value">用作要添加的元素的值的对象。</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> 为 null。</exception><exception cref="T:System.ArgumentException"><see cref="T:System.Collections.Generic.IDictionary`2"/> 中已存在具有相同键的元素。</exception><exception cref="T:System.NotSupportedException"><see cref="T:System.Collections.Generic.IDictionary`2"/> 为只读。</exception>
        public void Add(string key, string value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            mDictionary.Add(key.ToLower(), value);
        }

        /// <summary>
        /// 从 <see cref="T:System.Collections.Generic.IDictionary`2"/> 中移除带有指定键的元素。
        /// </summary>
        /// <returns>
        /// 如果该元素已成功移除，则为 true；否则为 false。如果在原始 <see cref="T:System.Collections.Generic.IDictionary`2"/> 中没有找到 <paramref name="key"/>，该方法也会返回 false。
        /// </returns>
        /// <param name="key">要移除的元素的键。</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> 为 null。</exception><exception cref="T:System.NotSupportedException"><see cref="T:System.Collections.Generic.IDictionary`2"/> 为只读。</exception>
        public bool Remove(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            return mDictionary.Remove(key);
        }

        /// <summary>
        /// 获取与指定的键相关联的值。
        /// </summary>
        /// <returns>
        /// 如果实现 <see cref="T:System.Collections.Generic.IDictionary`2"/> 的对象包含具有指定键的元素，则为 true；否则，为 false。
        /// </returns>
        /// <param name="key">要获取其值的键。</param><param name="value">当此方法返回时，如果找到指定键，则返回与该键相关联的值；否则，将返回 <paramref name="value"/> 参数的类型的默认值。该参数未经初始化即被传递。</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> 为 null。</exception>
        public bool TryGetValue(string key, out string value)
        {
            return mDictionary.TryGetValue(key.ToLower(), out value);
        }

        /// <summary>
        /// 获取或设置具有指定键的元素。
        /// </summary>
        /// <returns>
        /// 带有指定键的元素。
        /// </returns>
        /// <param name="key">要获取或设置的元素的键。</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> 为 null。</exception><exception cref="T:System.Collections.Generic.KeyNotFoundException">检索了属性但没有找到 <paramref name="key"/>。</exception><exception cref="T:System.NotSupportedException">设置该属性，而且 <see cref="T:System.Collections.Generic.IDictionary`2"/> 为只读。</exception>
        public string this[string key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException("key");
                string result;
                return mDictionary.TryGetValue(key.ToLower(), out result) ? result : null;
            }
            set
            {
                if (key == null)
                    throw new ArgumentNullException("key");
                if (mDictionary.ContainsKey(key.ToLower()))
                    mDictionary[key.ToLower()] = value;
            }
        }

        /// <summary>
        /// 获取包含 <see cref="T:System.Collections.Generic.IDictionary`2"/> 的键的 <see cref="T:System.Collections.Generic.ICollection`1"/>。
        /// </summary>
        /// <returns>
        /// 一个 <see cref="T:System.Collections.Generic.ICollection`1"/>，它包含实现 <see cref="T:System.Collections.Generic.IDictionary`2"/> 的对象的键。
        /// </returns>
        public ICollection<string> Keys
        {
            get { return mDictionary.Keys; }
        }

        /// <summary>
        /// 获取包含 <see cref="T:System.Collections.Generic.IDictionary`2"/> 中的值的 <see cref="T:System.Collections.Generic.ICollection`1"/>。
        /// </summary>
        /// <returns>
        /// 一个 <see cref="T:System.Collections.Generic.ICollection`1"/>，它包含实现 <see cref="T:System.Collections.Generic.IDictionary`2"/> 的对象中的值。
        /// </returns>
        public ICollection<string> Values
        {
            get { return mDictionary.Values; }
        }

        #endregion

        private readonly Dictionary<string, string> mDictionary = new Dictionary<string, string>();
    }
}