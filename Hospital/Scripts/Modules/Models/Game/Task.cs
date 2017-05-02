namespace Assets.Hospital.Scripts.Modules.Models.Game
{
    using System.Collections.Generic;
    using Map;
    using Views.Game;
    using Views.Map;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/3/29 16:46:30 
    /// Desc:
    /// </summary>
    ///
    public class Task
    {
        public TaskStatusEnum Status=TaskStatusEnum.AWAIT_DISTRIBUTE;    //任务状态
        public TileItem Target;     //任务目标点(工人到达的地点)
        public BuildCommandType Type;        //任务类型
    }
}
