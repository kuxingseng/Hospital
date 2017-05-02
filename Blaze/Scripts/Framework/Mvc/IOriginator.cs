namespace Blaze.Framework.Mvc
{
    /// <summary>
    /// 备忘录模式发起者接口。
    /// <remarks>http://en.wikipedia.org/wiki/Memento_pattern</remarks>
    /// </summary>
    public interface IOriginator
    {
        /// <summary>
        /// 捕获当前备忘录状态。
        /// </summary>
        /// <returns></returns>
        IMemento GetMemento();

        /// <summary>
        /// 还原到指定的备忘录状态。
        /// </summary>
        /// <param name="memento">备忘录</param>
        void SetMemento(IMemento memento);
    }
}