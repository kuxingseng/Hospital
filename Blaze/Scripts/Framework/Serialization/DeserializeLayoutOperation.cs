namespace Blaze.Framework.Serialization
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using Loading;
    using OperationQueues;

    /// <summary>
    /// 反序列化Layout配置操作。
    /// </summary>
    public class DeserializeLayoutOperation : SimpleOperation, ILoadOperation
    {
        /// <summary>
        /// 获取错误信息。
        /// </summary>
        public string Error { get; private set; }

        /// <summary>
        /// 获取一个值，表示加载是否完毕。
        /// </summary>
        public bool IsCompleted
        {
            get { return Progress >= 1; }
        }

        /// <summary>
        /// 获取加载进度。
        /// </summary>
        public float Progress
        {
            get
            {
                if (mRequiredOperations.Length == 0)
                    return 1;
                return mRequiredOperations.Sum(operation => operation.Progress)/mRequiredOperations.Length;
            }
        }

        /// <summary>
        /// 获取加载的结果。
        /// </summary>
        public object Result { get; private set; }

        /// <summary>
        /// 构造一个<see cref="DeserializeLayoutOperation"/>。
        /// </summary>
        /// <param name="xml">XML配置</param>
        /// <param name="context">上下文</param>
        public DeserializeLayoutOperation(string xml, DeserializationContext context)
        {
            mXml = xml;
            mContext = context;
        }

        /// <summary>
        /// 当需要执行操作时调用此方法。
        /// </summary>
        protected override void OnExecute()
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(mXml)))
            {
                var reader = XmlReader.Create(stream);

                //Declaration
                reader.Read();

                //GameObject
                reader.Read();
                var resultObject = reader.GetGameObject(mContext, true);
                Result = resultObject;

                mReferenceOperations = mContext.GetDependencyOperations(DependencyLevel.Reference).ToArray();
                mDecorateOperations = mContext.GetDependencyOperations(DependencyLevel.Decorate).ToArray();
                mRequiredOperations = mContext.GetDependencyOperations(DependencyLevel.Required).ToArray();

                var decorateQueue = new MainThreadOperationQueue(BlazeEngine.Instance);
                foreach (var operation in mDecorateOperations)
                    decorateQueue.Enqueue(operation);

                var criticalOperations = new List<ILoadOperation>();
                criticalOperations.AddRange(mRequiredOperations);
                criticalOperations.AddRange(mReferenceOperations);

                var requiredQueue = new MainThreadOperationQueue(BlazeEngine.Instance);
                var requiredOperations = criticalOperations.Select(op => (IOperation) op);
                var requiredQueueOperation = new QueueOperation(requiredQueue, requiredOperations);
                requiredQueueOperation.OnSucceed(() =>
                {
                    mContext.Finish();
                    resultObject.SetActive(true);
                    base.OnExecute();
                }).Execute();
            }
        }

        private ILoadOperation[] mDecorateOperations;
        private ILoadOperation[] mReferenceOperations;
        private ILoadOperation[] mRequiredOperations;
        private readonly DeserializationContext mContext;
        private readonly string mXml;
    }
}