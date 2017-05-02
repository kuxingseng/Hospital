namespace Blaze.Framework.Timers
{
    using System;

    /// <summary>
    /// 提供以指定的时间间隔执行方法的机制。无法继承此类。
    /// </summary>
    public sealed class Timer
    {
        /// <summary>
        /// 当计时器到达指定的时间间隔时触发此事件。
        /// </summary>
        public event EventHandler Tick;

        /// <summary>
        /// 获取或设置一个值，表示计时器触发<see cref="Tick"/>事件之后是否自动调用<see cref="Stop"/>来停止。
        /// </summary>
        public bool AutoReset { get; set; }

        /// <summary>
        /// 获取或设置一个值，表示计时器是否会引发<see cref="E:Tick"/>事件。
        /// </summary>
        public bool Enabled
        {
            get { return mHost.enabled; }
            set { mHost.enabled = value; }
        }

        /// <summary>
        /// 获取计时器的时间间隔。
        /// </summary>
        public float Interval { get; private set; }

        /// <summary>
        /// 构造方法。
        /// </summary>
        /// <param name="interval">时间间隔（单位：秒）</param>
        /// <param name="autoReset">计时器触发<see cref="Tick"/>事件之后是否自动调用<see cref="Stop"/>来停止。</param>
        public Timer(float interval, bool autoReset = false)
        {
            if (interval < 0)
                throw new ArgumentOutOfRangeException("interval");
            Interval = interval;
            AutoReset = autoReset;
            mHost = TimerHost.Create(this);
        }

        /// <summary>
        /// 启动定时器，若<see cref="Enabled"/>为false，则会将其置为true。
        /// </summary>
        public void Start()
        {
            Enabled = true;
        }

        /// <summary>
        /// 停止计时器。若<see cref="Enabled"/>为true，则会将其置为false。
        /// </summary>
        public void Stop()
        {
            Enabled = false;
        }

        /// <summary>
        /// 强制计时器触发<see cref="E:Tick"/>事件，此方法供<see cref="TimerHost"/>调用。
        /// </summary>
        internal void InternalTick()
        {
            if (Tick != null)
                Tick(this, EventArgs.Empty);
            if (AutoReset)
                Stop();
        }

        private readonly TimerHost mHost;
    }
}