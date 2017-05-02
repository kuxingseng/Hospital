using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Hospital.Scripts.Modules.Models.Tips
{
    using Muhe.Mjhx.Modules.Models;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/20 16:17:25 
    /// Desc:
    /// </summary>
    ///
    public class TextTipsModel:ModuleModel
    {
        /// <summary>
        /// 获取或设置当提示结束后的回调。
        /// </summary>
        public Action Callback { get; set; }

        /// <summary>
        /// 获取或设置需要提示的文本。
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 获取或设置是否自动消失类型
        /// </summary>
        public bool IsAutoDisappear { get; set; }
    }
}
