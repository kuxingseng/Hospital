using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Hospital.Scripts.Behaviour.Action
{
    using AI;
    using BehaviorDesigner.Runtime.Tasks;
    using Muhe.Mjhx.Common;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/3/30 18:38:06 
    /// Desc:
    /// </summary>
    ///
    [TaskCategory("Custom")]
    [TaskDescription("移动到工作目标格子")]
    public class WorkerRunningAction:Action
    {
        private Role mRole;
        private bool mIsFinish;

        public override void OnAwake()
        {
            //MyLogger.Debug("WorkerRunningAction OnAwake");
            mRole = transform.GetComponent<Role>();
        }

        public override void OnStart()
        {
            //MyLogger.Debug("WorkerRunningAction OnStart");
            mIsFinish = false;
            mRole.Status = WorkerStatusEnum.RUNNING_TO_WORK;
            mRole.MoveToTarget(mRole.Task.Target, () => mIsFinish = true);
        }

        public override TaskStatus OnUpdate()
        {
            if (!mIsFinish)
                return TaskStatus.Running;
            else
                return TaskStatus.Success;
        }
    }
}
