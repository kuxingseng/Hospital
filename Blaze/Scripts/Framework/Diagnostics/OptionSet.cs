namespace Blaze.Framework.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// 命令行可选参数集合。
    /// </summary>
    public class OptionSet
    {
        /// <summary>
        /// 获取参数的数量。
        /// </summary>
        public int Count
        {
            get { return mOptionDictionary.Count; }
        }

        /// <summary>
        /// 获取或设置指定参数的值。
        /// </summary>
        /// <param name="key">参数</param>
        /// <returns>参数的值</returns>
        public string this[string key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException("key");
                key = key.ToLower();
                Option option;
                return mOptionDictionary.TryGetValue(key, out option) ? option.Value : null;
            }
            set
            {
                if (key == null)
                    throw new ArgumentNullException("key");
                key = key.ToLower();
                if (mOptionDictionary.ContainsKey(key))
                {
                    mOptionDictionary[key].Value = value;
                }
                else
                {
                    var option = new Option(key, value);
                    mOptionDictionary.Add(key, option);
                }
            }
        }

        /// <summary>
        /// 获取指定索引位置的参数设置。
        /// </summary>
        /// <param name="index">索引值</param>
        /// <returns>参数值</returns>
        public string this[int index]
        {
            get
            {
                if (index < 0 || mArguments.Length <= index)
                    return null;
                return mArguments[index];
            }
        }

        /// <summary>
        /// 构造一个不含任何参数配置的集合。
        /// </summary>
        public OptionSet(params string[] args)
        {
            mArguments = args;
        }

        /// <summary>
        /// 获取一个值，表示指定的参数是否存在。
        /// </summary>
        /// <param name="key">参数名</param>
        /// <returns>是否存在</returns>
        public bool HasKey(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            key = key.ToLower();
            return mOptionDictionary.ContainsKey(key);
        }

        /// <summary>
        /// 返回表示当前 <see cref="T:System.Object"/> 的 <see cref="T:System.String"/>。
        /// </summary>
        /// <returns>
        /// <see cref="T:System.String"/>，表示当前的 <see cref="T:System.Object"/>。
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var arg in mArguments)
                builder.AppendFormat("{0} ", arg);
            foreach (var pair in mOptionDictionary)
                builder.AppendFormat("{0}={1} ", pair.Key, pair.Value.Value);
            return builder.ToString();
        }

        private readonly string[] mArguments;
        private readonly Dictionary<string, Option> mOptionDictionary = new Dictionary<string, Option>();
    }
}