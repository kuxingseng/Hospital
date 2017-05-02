using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Hospital.Scripts.Behaviour.Condition
{
    using AI;
    using BehaviorDesigner.Runtime.Tasks;
    using Modules.Models.Game;
    using Muhe.Mjhx.Common;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/3/30 18:37:08 
    /// Desc:
    /// </summary>
    ///
    [TaskCategory("Custom")]
    [TaskDescription("判断当前是否有新的任务")]
    public class WorkerGetTaskCondition:Conditional
    {
        private Role mRole;

        public override void OnAwake()
        {
            //MyLogger.Debug("WorkerGetTaskCondition OnAwake");
            mRole = transform.GetComponent<Role>();
        }

        public override void OnStart()
        {
            base.OnStart();
            //MyLogger.Debug("WorkerGetTaskCondition OnStart");
        }

        public override TaskStatus OnUpdate()
        {
            //MyLogger.Debug("WorkerGetTaskCondition OnUpdate");
            if (mRole.Task != null)
            {
                if(mRole.Task.Status!=TaskStatusEnum.FINISH)
                    return TaskStatus.Success;
                else
                    return TaskStatus.Running;
            }   
            else
                return TaskStatus.Running;
        }
    }
}
