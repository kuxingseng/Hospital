namespace Blaze.Framework.Sensitives
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 敏感词过滤器。
    /// 来源：http://www.cnblogs.com/passer/p/3461052.html
    /// </summary>
    public class WordFilter
    {
        /// <summary>
        /// 敏感词树
        /// </summary>
        private class FilterKeyWordsNode
        {
            public Dictionary<char, FilterKeyWordsNode> Child;
            public bool IsEnd;
        }

        /// <summary>
        /// 自定义过滤方法
        /// </summary>
        /// <param name="text">找到的字符串</param>
        /// <param name="offset">起始位置</param>
        /// <param name="length">字符串长度</param>
        /// <returns>替换后的</returns>
        public delegate string ReplaceDelegate(string text, int offset, int length);

        /// <summary>
        /// 查找含有的关键词
        /// </summary>
        public static bool Find(string text, out string[] keyWords)
        {
            keyWords = find(text).Select(p => text.Substring(p.Key, p.Value)).Distinct().ToArray();
            return keyWords.Length > 0;
        }

        /// <summary>
        /// 初始化 使用前必须调用一次
        /// </summary>
        /// <param name="keyWords">敏感词列表</param>
        public static void Init(string[] keyWords)
        {
            if (mRoot != null)
                return;
            mRoot = new FilterKeyWordsNode();
            var list = keyWords.Distinct().OrderBy(p => p).ThenBy(p => p.Length).ToList();
            for (var i = list.Min(p => p.Length); i <= list.Max(p => p.Length); i++)
            {
                var i1 = i;
                var startList = list.Where(p => p.Length >= i1).Select(p => p.Substring(0, i1)).Distinct();
                foreach (var startWord in startList)
                {
                    var tmp = mRoot;
                    for (var j = 0; j < startWord.Length; j++)
                    {
                        var t = startWord[j];
                        if (tmp.Child == null)
                            tmp.Child = new Dictionary<char, FilterKeyWordsNode>();

                        if (!tmp.Child.ContainsKey(t))
                        {
                            tmp.Child.Add(t, new FilterKeyWordsNode {IsEnd = list.Contains(startWord.Substring(0, 1 + j))});
                        }

                        tmp = tmp.Child[t];
                    }
                }
            }
        }

        /// <summary>
        /// 简单快速替换
        /// </summary>
        public static string Replace(string text)
        {
            var dic = find(text);
            var list = text.ToArray();
            foreach (var i in dic)
            {
                for (var j = i.Key; j < i.Key + i.Value; j++)
                {
                    list[j] = '*';
                }
            }
            return new string(list.ToArray());
        }

        /// <summary>
        /// 自定义过滤
        /// </summary>
        public static string Replace(string text, ReplaceDelegate replaceAction)
        {
            var dic = find(text);
            var list = text.ToList();
            var offset = 0;
            foreach (var i in dic)
            {
                list.RemoveRange(i.Key + offset, i.Value);
                var newText = replaceAction(text.Substring(i.Key, i.Value), i.Key, i.Value);
                list.InsertRange(i.Key + offset, newText);

                offset = offset + newText.Length - i.Value;
            }
            return new string(list.ToArray());
        }

        /// <summary>
        /// 位置查找
        /// </summary>
        private static Dictionary<int, int> find(string src)
        {
            if (mRoot == null)
                throw new InvalidOperationException("未初始化");

            var findResult = new Dictionary<int, int>();
            if (string.IsNullOrEmpty(src))
                return findResult;

            var text = src;
            int start = 0;

            while (start < text.Length)
            {
                int length = 0;
                var firstChar = text[start + length];
                var node = mRoot;

                //不匹配首字符 移动起始位置
                while (!node.Child.ContainsKey(firstChar) && start < text.Length - 1)
                {
                    start++;
                    firstChar = text[start + length];
                }

                //部分匹配 移动结束位置 直到不匹配
                while (node.Child != null && node.Child.ContainsKey(firstChar))
                {
                    node = node.Child[firstChar];
                    length++;

                    if ((start + length) == text.Length)
                        break;

                    firstChar = text[start + length];
                }

                //完整匹配 把起始位置移到结束位置
                if (node.IsEnd)
                {
                    findResult.Add(start, length);
                    start += length - 1;
                }

                start++;
            }

            return findResult;
        }

        private static FilterKeyWordsNode mRoot;
    }
}