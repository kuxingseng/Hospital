namespace Assets.Hospital.Scripts.AI
{
    using System;
    using BehaviorDesigner.Runtime;
    using Modules.Models.Game;
    using Modules.Views.Game;
    using Modules.Views.Map;
    using Muhe.Mjhx.Common;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/3/23 19:13:08 
    /// Desc:
    /// </summary>
    ///
    public class Role:MonoBehaviour
    {
        #region UI
        private AILerp mAILerp;
        private BehaviorTree mBehaviorTree;
        private TileItem mTarget;

        public Text StatusText;
        #endregion

        private Action mFinish;
        public WorkerStatusEnum Status;
        public Task Task;
        public TileItem BornTile;

        public void Start()
        {
            mAILerp = GetComponent<AILerp>();
            mBehaviorTree = GetComponent<BehaviorTree>();
            Status = WorkerStatusEnum.IDLE;
        }

        /// <summary>
        /// 设置任务
        /// </summary>
        /// <param name="task"></param>
        public void SetTask(Task task)
        {
            Task = task;
            mBehaviorTree.Start();
            Task.Status=TaskStatusEnum.IN_PORGRESS;
        }

        /// <summary>
        /// 交还任务
        /// </summary>
        public void FinishTask()
        {
            Task.Status=TaskStatusEnum.FINISH;
        }

        public void MoveToTarget(TileItem target,Action finish)
        {
            if(mTarget==target)
                return;

            mFinish = finish;
            mTarget = target;
            mAILerp.target = mTarget.transform;
            mAILerp.SearchPath();
        }

        private void Update()
        {
            if (Status == WorkerStatusEnum.RUNNING_TO_WORK || Status == WorkerStatusEnum.RUNNING_TO_HOME)
            {
                if (mAILerp.targetReached)
                {
                    if(mFinish!=null)
                        mFinish();
                }
                StatusText.text = "running";
            }else if (Status == WorkerStatusEnum.WORKING)
            {
                StatusText.text = "working";
            }
            else
            {
                StatusText.text = "idle";
            }
        }
    }
}
