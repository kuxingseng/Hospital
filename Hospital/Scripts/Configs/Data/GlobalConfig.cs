namespace Assets.Mjhx.Scripts.Configs.Data
{
    using Global;
    using Muhe.Mjhx.Configs;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// author ldc
    /// $Id$
    /// Create time:5/24/2016 3:03:52 PM 
    /// </summary>
    ///
    public class GlobalConfig : DataConfig
    {
        /// <summary>
        /// 获取所有的状态模板配置。
        /// </summary>
        [JsonProperty("GlobalConfig")]
        public GlobalEntryInfo Globals;
        
        /// <summary>
        /// 获取配置项标识符。
        /// </summary>
        public override string Key
        {
            get { return "GlobalConfig"; }
        }

        /// <summary>
        /// 初始化配置，默认加载JSON文本并填充当前对象。
        /// </summary>
        /// <param name="data">配置内容</param>
        /// <returns>是否加载成功</returns>
        public override bool Initialize(JContainer data)
        {
            if (!base.Initialize((JContainer)data["GlobalConfig"]))
                return false;
            if (Globals == null)
                return false;
            return true;
        }
    }
}
