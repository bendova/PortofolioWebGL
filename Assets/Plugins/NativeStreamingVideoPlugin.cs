using System;
using System.Collections;
using UnityEngine;

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
namespace Assets.Plugins
{
    public class NativeStreamingVideoPlugin : IStreamingVideoPlugin
    {
        private GameObject m_gameObject;
        private MovieTexture m_movieTexture;
        private AudioClip m_audioClip;
        private WWW m_www;

        private AudioSource m_audioSource;
        private Renderer m_renderer;

        public StreamingVideoStatus Status { get { return m_status; } }
        private StreamingVideoStatus m_status;

        public string Url { get { return m_url; }}
        private string m_url;

        public bool IsReadyToPlay
        {
            get { return Status.CompareTo(StreamingVideoStatus.ReadyToPlay) >= 0; }
        }

        public bool IsPlaying
        {
            get { return Status == StreamingVideoStatus.Playing; }
        }

        public bool IsDone
        {
            get { return Status == StreamingVideoStatus.Done; }
        }

        public string DebugStatus
        {
            get;
            private set;
        }

        public float Progress { get { return m_movieProgress; } }
        private float m_movieProgress;

        public float Duration { get { return m_movieTexture.duration; } }

        public NativeStreamingVideoPlugin(string url, GameObject gameObject)
        {
            m_gameObject = gameObject;

            InitComponents();
            LoadUrl(url);
        }

        private void InitComponents()
        {
            m_audioSource = m_gameObject.GetComponent<AudioSource>();
            if (m_audioSource == null)
            {
                m_audioSource = m_gameObject.AddComponent<AudioSource>();
            }
            m_renderer = m_gameObject.GetComponent<Renderer>();
        }

        private void LoadUrl(string url)
        {
            m_url = url;
            m_status = StreamingVideoStatus.Unknown;

            var fullyQualifiedUrl = "file://" + Application.streamingAssetsPath + "/" + m_url;
            Debug.Log("Creating www object with url: " + fullyQualifiedUrl);
            m_www = new WWW(fullyQualifiedUrl);
        }

        public void Dispose()
        {
            // nothing to do
            if(m_www != null)
            {
                m_www.Dispose();
            }
            m_renderer.material.mainTexture = null;
            m_audioSource.clip = null;
            m_movieTexture = null;
            m_audioSource = null;
        }

        public void Play()
        {
            if (IsReadyToPlay)
            {
                m_movieTexture.Play();
                m_audioSource.Play();

                m_status = StreamingVideoStatus.Playing;
            }
        }

        public void Pause()
        {
            if(IsPlaying)
            {
                m_status = StreamingVideoStatus.Paused;

                m_movieTexture.Pause();
                m_audioSource.Pause();
            }
        }

        public void Stop()
        {
            if(IsReadyToPlay)
            {
                m_status = StreamingVideoStatus.Done;
                m_movieProgress = 0.0f;
                m_movieTexture.Stop();
                m_audioSource.Stop();
            }
        }

        public void Seek(float time)
        {
            // not implemented
        }

        public void Update()
        {
            var prevStatus = m_status;
            switch (prevStatus)
            {
            case StreamingVideoStatus.Unknown:
            case StreamingVideoStatus.Loading:
                HandleLoading();
                break;
            case StreamingVideoStatus.Playing:
                if (m_movieTexture.isPlaying)
                {
                    m_movieProgress += Time.deltaTime;
                }
                if((m_movieProgress >= Duration) || !m_movieTexture.isPlaying)
                {
                    Stop();
                }
                break;
             }
        }

        private void HandleLoading()
        {
            if (m_movieTexture)
            {
                if (m_movieTexture.isReadyToPlay)
                {
                    if (m_audioClip != null)
                    {
                        if (m_audioClip.loadState == AudioDataLoadState.Loaded)
                        {
                            m_renderer.material.mainTexture = m_movieTexture;
                            m_audioSource.clip = m_audioClip;
                            m_status = StreamingVideoStatus.ReadyToPlay;
                        }
                    }
                    else
                    {
                        DebugStatus = "Could not retrieve audio from url: " + Url;
                        m_status = StreamingVideoStatus.Error;
                    }
                }
            }
            else if (m_www.error != null)
            {
                Debug.Log("www.error: " + m_www.error);

                DebugStatus = m_www.error;
                m_status = StreamingVideoStatus.Error;
            }
            else if(m_www.isDone)
            {
                m_movieTexture = m_www.movie;
                if (m_movieTexture)
                {
                    m_movieTexture = m_www.movie;
                    m_audioClip = m_movieTexture.audioClip;
                }
                else
                {
                    DebugStatus = "Could not retrieve video from url: " + Url;
                    m_status = StreamingVideoStatus.Error;
                }
            }
        }
    }
}
#endif
