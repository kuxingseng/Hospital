using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Hospital.Scripts.Behaviour.Action
{
    using System.Collections;
    using AI;
    using BehaviorDesigner.Runtime.Tasks;
    using Modules.Models.Game;
    using Muhe.Mjhx.Common;
    using UnityEngine;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/3/30 18:37:33 
    /// Desc:
    /// </summary>
    ///
    [TaskCategory("Custom")]
    [TaskDescription("开始工作")]
    public class WorkerWorkingAction:Action
    {
        private Role mRole;
        private bool mIsFinish;

        public override void OnAwake()
        {
            //MyLogger.Debug("WorkerWorkingAction OnAwake");
            mRole = transform.GetComponent<Role>();
        }

        public override void OnStart()
        {
            //MyLogger.Debug("WorkerWorkingAction OnStart");
            mIsFinish = false;
            mRole.Status = WorkerStatusEnum.WORKING;
            StartCoroutine(working());
        }

        public override TaskStatus OnUpdate()
        {
            if (!mIsFinish)
                return TaskStatus.Running;
            else
            {
                return TaskStatus.Success;
            }
        }

        private IEnumerator working()
        {
            //开始工作
            if (mRole.Task.Type.ToString().StartsWith("Build"))
                StartCoroutine(mRole.Task.Target.Build(() => mIsFinish = true));
            else
                StartCoroutine(mRole.Task.Target.Remove(mRole.Task.Type,() => mIsFinish = true));
            yield return new WaitUntil(() => mIsFinish);
            //交还任务
            mRole.FinishTask();
        }
    }
}
