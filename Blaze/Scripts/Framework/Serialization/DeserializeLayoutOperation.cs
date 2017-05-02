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
    /// �����л�Layout���ò�����
    /// </summary>
    public class DeserializeLayoutOperation : SimpleOperation, ILoadOperation
    {
        /// <summary>
        /// ��ȡ������Ϣ��
        /// </summary>
        public string Error { get; private set; }

        /// <summary>
        /// ��ȡһ��ֵ����ʾ�����Ƿ���ϡ�
        /// </summary>
        public bool IsCompleted
        {
            get { return Progress >= 1; }
        }

        /// <summary>
        /// ��ȡ���ؽ��ȡ�
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
        /// ��ȡ���صĽ����
        /// </summary>
        public object Result { get; private set; }

        /// <summary>
        /// ����һ��<see cref="DeserializeLayoutOperation"/>��
        /// </summary>
        /// <param name="xml">XML����</param>
        /// <param name="context">������</param>
        public DeserializeLayoutOperation(string xml, DeserializationContext context)
        {
            mXml = xml;
            mContext = context;
        }

        /// <summary>
        /// ����Ҫִ�в���ʱ���ô˷�����
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