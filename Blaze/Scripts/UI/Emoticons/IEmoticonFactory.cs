namespace Blaze.UI.Emoticons
{
    /// <summary>
    /// 表情图片工厂接口。
    /// </summary>
    public interface IEmoticonFactory
    {
        /// <summary>
        /// 创建指定标识符的表情图片。
        /// </summary>
        /// <param name="id">表情标识符</param>
        /// <returns>表情图片</returns>
        Emoticon Create(string id);
    }
}