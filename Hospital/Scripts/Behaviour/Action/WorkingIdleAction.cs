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
    /// Create time:2017/3/30 18:38:49 
    /// Desc:
    /// </summary>
    ///
    [TaskCategory("Custom")]
    [TaskDescription("进入待机状态")]
    public class WorkingIdleAction:Action
    {
        private Role mRole;

        public override void OnAwake()
        {
            //MyLogger.Debug("WorkingIdleAction OnAwake");
            mRole = transform.GetComponent<Role>();
        }

        public override void OnStart()
        {
            //MyLogger.Debug("WorkingIdleAction OnStart");
            mRole.Status = WorkerStatusEnum.IDLE;
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}
