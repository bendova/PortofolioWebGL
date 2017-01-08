using System;
using UnityEngine;

namespace Assets.Plugins
{
    public class WebGLStreamingVideoPlugin : IStreamingVideoPlugin
    {
        private WebGLMovieTexture m_movieTexture;
        private GameObject m_gameObject;

        private bool m_isStopping = false;

        public WebGLStreamingVideoPlugin(string url, GameObject gameObject)
        {
            Url = "StreamingAssets/" + url;
            _status = StreamingVideoStatus.Unknown;

            m_gameObject = gameObject;
            MeshRenderer meshRenderer = m_gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer)
            {
                m_movieTexture = new WebGLMovieTexture(Url);
                meshRenderer.material.mainTexture = m_movieTexture.GetVideoTexture();
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
            Debug.Log("Play() IsReadyToPlay: " + IsReadyToPlay);

            if (IsReadyToPlay)
            {
                m_movieTexture.Play();
                _status = StreamingVideoStatus.Playing;
            }
        }

        public void Pause()
        {
            Debug.Log("Pause()");

            m_movieTexture.Pause();
            _status = StreamingVideoStatus.Paused;
        }

        public void Stop()
        {
            Debug.Log("Stop()");

            // We can't seek to 0.0f in this case,
            // because for some mysterious reason that
            // causes the video player to get stuck.
            Seek(0.5f);
            m_isStopping = true;
        }

        public void Dispose()
        {
            m_movieTexture.Dispose();
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
            if(m_isStopping)
            {
                if(IsReadyToPlay)
                {
                    Debug.Log("Update() stopping");

                    m_isStopping = false;
                    Pause();
                }
            }
            if (_status < StreamingVideoStatus.ReadyToPlay && IsReadyToPlay)
            {
                _status = StreamingVideoStatus.ReadyToPlay;
            }
            else if (_status == StreamingVideoStatus.Playing && m_movieTexture.hasEnded)
            {
                _status = StreamingVideoStatus.Done;
            }
        }

        public void Seek(float time)
        {
            if(IsReadyToPlay)
            {
                float clampedTime = Mathf.Min(PluginMathUtils.GetSafeFloat(time), m_movieTexture.duration);
                m_movieTexture.Seek(clampedTime);
            }
        }
    }
}
