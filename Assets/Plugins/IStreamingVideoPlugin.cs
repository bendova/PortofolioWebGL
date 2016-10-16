using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Plugins
{
    public interface IStreamingVideoPlugin
    {
        string Url { get; }
        bool IsReadyToPlay { get; }
        bool IsPlaying { get; }
        StreamingVideoStatus Status { get; }
        bool IsDone { get; }
        float Progress { get; }
        float Duration { get; }

        void Play();
        void Pause();
        void Update();
        void Seek(float time);
    }

    public enum StreamingVideoStatus
    {
        Unknown,
        Error,
        Loading,
        ReadyToPlay,
        Playing,
        Paused,
        Done,
        Stopped
    }
}
