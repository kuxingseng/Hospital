namespace Assets.Hospital.Scripts.Managers
{
    using System.Collections;
    using System.Collections.Generic;
    using AI;
    using Modules.Models.Game;
    using Modules.Views.Game;
    using Modules.Views.Map;
    using Muhe.Mjhx.Common;
    using Muhe.Mjhx.Managers;
    using UnityEngine;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/3/29 16:39:50 
    /// Desc:
    /// </summary>
    ///
    public static class WorkerManager
    {
        /// <summary>
        /// 工人列表
        /// </summary>
        public static List<Role> Workers=new List<Role>();

        /// <summary>
        /// 任务列表
        /// </summary>
        public static List<Task> Tasks=new List<Task>();

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            CoroutineManager.Start(taskDistributor());
        }

        /// <summary>
        /// 创建工人
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="bornTile"></param>
        /// <returns></returns>
        public static Role CreateWorker(Transform parent, TileItem bornTile)
        {
            var role = Resources.Load("Prefabs/UI/Game/Worker");
            var go = (GameObject)Object.Instantiate(role);
            go.transform.SetParent(parent);
            go.transform.localPosition = bornTile.transform.position;
            go.transform.localScale = Vector3.one;
            var worker = go.GetComponent<Role>();
            worker.BornTile = bornTile;
            Workers.Add(worker);
            return worker;
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="tasks"></param>
        public static void AddTask(IEnumerable<Task> tasks)
        {
            Tasks.AddRange(tasks);
        }

        //自动分配、整理任务
        private static IEnumerator taskDistributor()
        {
            while (true)
            {
                if (Tasks != null && Tasks.Count > 0 && Workers.Count>0)
                {
                    for (var i = 0; i < Tasks.Count; i++)
                    {
                        if (Tasks[i].Status == TaskStatusEnum.AWAIT_DISTRIBUTE)
                        {
                            for (var j = 0; j < Workers.Count; j++)
                            {
                                if (Workers[j].Status == WorkerStatusEnum.IDLE)
                                {
                                    if (Workers[j].Task == null)
                                    {
                                        Workers[j].SetTask(Tasks[i]);
                                        break;
                                    }
                                    else
                                    {
                                        if (Workers[j].Task.Status == TaskStatusEnum.FINISH)
                                        {
                                            Workers[j].SetTask(Tasks[i]);
                                            break;
                                        }
                                    }
                                }
                            }
                        }else if (Tasks[i].Status == TaskStatusEnum.FINISH)
                        {
                            Tasks.Remove(Tasks[i]);
                        }
                    }
                }
                yield return null;
                yield return null;
            }
        }
    }
}
