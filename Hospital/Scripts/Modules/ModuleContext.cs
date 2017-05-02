namespace Muhe.Mjhx.Modules
{
    using System;
    using Configs.Modules;
    using Models;
    using UnityEngine;

    /// <summary>
    /// ģ��ʵ�������ġ�
    /// </summary>
    public class ModuleContext
    {
        /// <summary>
        /// ��ȡ������ģ�����á�
        /// </summary>
        public Module Config { get; set; }

        /// <summary>
        /// ��ȡ������ģ����ʹ�õĿ�������
        /// </summary>
        public ModuleController Controller
        {
            get
            {
                if (mController == null)
                {
                    if (Root != null)
                        mController = Root.GetComponent<ModuleController>();
                }
                return mController;
            }
        }

        /// <summary>
        /// ��ȡ������ģ����ʹ�õ�ģ�͡�
        /// </summary>
        public ModuleModel Model { get; set; }

        /// <summary>
        /// ��ȡ������ģ������Ӧ����ͼ�ĸ���Ϸ����
        /// </summary>
        public GameObject Root
        {
            get { return mRoot; }
            set
            {
                if (mRoot != null)
                    throw new NotSupportedException();
                mRoot = value;
            }
        }

        /// <summary>
        /// ��ȡ������ģ����ʹ�õ���ͼ��
        /// </summary>
        public ModuleView View
        {
            get
            {
                if (mView == null)
                {
                    if (Root != null)
                        mView = Root.GetComponent<ModuleView>();
                }
                return mView;
            }
        }

        private ModuleController mController;
        private ModuleView mView;
        private GameObject mRoot;
    }
}