using System;
using UnityEngine;

namespace Assets.Plugins
{
    public class WebGLStreamingVideoPlugin : IStreamingVideoPlugin
    {
        private WebGLMovieTexture m_movieTexture;
        private GameObject m_gameObject;

        public WebGLStreamingVideoPlugin(string url, GameObject gameObject)
        {
            Url = "StreamingAssets/" + url;
            _status = StreamingVideoStatus.Unknown;

            m_gameObject = gameObject;
            MeshRenderer meshRenderer = m_gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer)
            {
                m_movieTexture = new WebGLMovieTexture(Url);
                meshRenderer.material.mainTexture = m_movieTexture;
            }
        }

        public string Url
        {
            get;
            set;
        }

        public bool IsReadyToPlay
        {
            get { return m_movieTexture.isReady; }
        }

        public bool IsPlaying
        {
            get { return _status == StreamingVideoStatus.Playing; }
        }

        public void Play()
        {
            if (IsReadyToPlay)
            {
                m_movieTexture.Play();
                _status = StreamingVideoStatus.Playing;
            }
        }

        public void Pause()
        {
            m_movieTexture.Pause();
            _status = StreamingVideoStatus.Paused;
        }

        public bool IsDone
        {
            get { return _status == StreamingVideoStatus.Done; }
        }

        private StreamingVideoStatus _status;
        public StreamingVideoStatus Status
        {
            get { return _status; }
        }

        public float Progress { get { return m_movieTexture.time; } }
        public float Duration { get { return m_movieTexture.duration; } }

        public void Update()
        {
            m_movieTexture.Update();
            if (_status < StreamingVideoStatus.ReadyToPlay && IsReadyToPlay)
            {
                _status = StreamingVideoStatus.ReadyToPlay;
            }
            if (_status == StreamingVideoStatus.Playing && m_movieTexture.time == m_movieTexture.duration)
            {
                _status = StreamingVideoStatus.Done;
            }
        }

        public void Seek(float time)
        {
            if(IsReadyToPlay)
            {
                m_movieTexture.Seek(time);
            }
        }
    }
}
