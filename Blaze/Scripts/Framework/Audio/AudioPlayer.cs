namespace Blaze.Framework.Audio
{
    using UnityEngine;

    /// <summary>
    /// 声音播放器。
    /// </summary>
    [Singleton(Hierarchy = "Blaze")]
    public class AudioPlayer : MonoBehaviour
    {
        /// <summary>
        /// 获取或设置一个值，表示是否对背景音乐静音。
        /// </summary>
        public bool IsMuteBgm
        {
            get { return mBgmSource.mute; }
            set { mBgmSource.mute = value; }
        }

        /// <summary>
        /// 获取或设置背景音乐的音量
        /// </summary>
        public float BgmVolume
        {
            get { return mBgmSource.volume; }
            set { mBgmSource.volume = value; }
        }

        /// <summary>
        /// 获取或设置一个值，表示是否对音效静音。
        /// </summary>
        public bool IsMuteSfx
        {
            get { return mSfxChannels[0].mute; }
            set
            {
                foreach (var channel in mSfxChannels)
                    channel.mute = value;
            }
        }

        /// <summary>
        /// 获取或设置音效的音量。
        /// </summary>
        public float SfxVolume
        {
            get { return mSfxChannels[0].volume; }
            set
            {
                foreach (var channel in mSfxChannels)
                    channel.volume = value;
            }
        }

        /// <summary>
        /// 获取<see cref="AudioPlayer"/>的唯一实例。
        /// </summary>
        public static AudioPlayer Instance
        {
            get { return Singleton.GetInstance<AudioPlayer>(); }
        }

        /// <summary>
        /// 播放指定的背景音乐。
        /// </summary>
        /// <param name="clip"></param>
        public void PlayBgm(AudioClip clip)
        {
            PlayBgm(clip, false);
        }

        /// <summary>
        /// 播放指定的背景音乐。
        /// </summary>
        /// <param name="clip">需要播放的音频剪辑</param>
        /// <param name="resetToBegin">是否重新开始播放</param>
        public void PlayBgm(AudioClip clip, bool resetToBegin)
        {
            if (!resetToBegin && mBgmSource.clip == clip)
                return;
            mBgmSource.loop = true;
            mBgmSource.clip = clip;
            mBgmSource.Play();
            mLastBgmClip = clip;
        }

        /// <summary>
        /// 播放指定的音效。
        /// </summary>
        /// <param name="clip">音效</param>
        public void PlaySfx(AudioClip clip)
        {
            var channel = mSfxChannels[mSfxChannel];
            channel.clip = clip;
            channel.Play();

            mSfxChannel++;
            if (mSfxChannel >= mSfxChannels.Length)
                mSfxChannel = 0;
        }

        /// <summary>
        /// 重新播放上次播放的背景音乐，若该音乐正在播放则会停止后再开始。
        /// </summary>
        public void ReplayBgm()
        {
            StopBgm();
            PlayBgm(mLastBgmClip, true);
        }

        /// <summary>
        /// 停止播放背景音乐。
        /// </summary>
        public void StopBgm()
        {
            if(mBgmSource!=null)
                mBgmSource.Stop();
        }

        /// <summary>
        /// 停止播放音效。
        /// </summary>
        public void StopSfx()
        {
            foreach (var channel in mSfxChannels)
                channel.Stop();
        }

        protected void Awake()
        {
            var bgm = new GameObject("Bgm");
            bgm.transform.parent = transform;
            mBgmSource = bgm.AddComponent<AudioSource>();
            mBgmSource.playOnAwake = false;

            var sfxRoot = new GameObject("Sfx");
            sfxRoot.transform.parent = transform;
            for (var i = 0; i < mSfxChannels.Length; i++)
            {
                var channel = new GameObject("Channel" + i);
                channel.transform.parent = sfxRoot.transform;
                var source = channel.AddComponent<AudioSource>();
                source.playOnAwake = false;
                mSfxChannels[i] = source;
            }
        }

        private const int mMaxSfxChannel = 5;
        private readonly AudioSource[] mSfxChannels = new AudioSource[mMaxSfxChannel];
        private AudioSource mBgmSource;
        private AudioClip mLastBgmClip;
        private int mSfxChannel;
    }
}