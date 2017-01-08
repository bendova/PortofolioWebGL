using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using Assets.Plugins;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(MeshRenderer))]
public class VideoPlayer : MonoBehaviour
{
    public string m_videoPath;
    public Slider m_progressSlider;
    public Text m_timeText;
    public VideoPlayerControls m_videoPlayerControls;
    public Collider2D m_videoAreaCollider;
    public GameObject m_playButton;
    public GameObject m_pauseButton;
    public Image m_videoImagePoster;

    private IStreamingVideoPlugin m_videoTexture;

    private bool m_isUserSeeking = false;
    private bool m_wasPlayingOnSeekBegin = false;
    private bool m_playWhenReady = false;
    private bool m_isStarted = false;
    private bool m_autoHideControls = false;

    void Start ()
    {
        m_isStarted = true;
        Initialize();
    }

    private void Initialize()
    {
        m_videoImagePoster.enabled = true;
        CreateNewVideoTexture(m_videoPath);
        TogglePlayPauseButton(true);
        InitProgressSlider();
        InitVideoTime();
    }

    private void CreateNewVideoTexture(string videoPath)
    {
        DisposeVideoTexture();

        if(videoPath.Length > 0)
        {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
            m_videoTexture = new NativeStreamingVideoPlugin(videoPath, gameObject);
#elif UNITY_WEBGL
            m_videoTexture = new WebGLStreamingVideoPlugin(videoPath, gameObject);
#endif
        }
    }

    public void OnDestroy()
    {
        DisposeVideoTexture();
    }

    private void DisposeVideoTexture()
    {
        if (m_videoTexture != null)
        {
            m_videoTexture.Dispose();
            m_videoTexture = null;
            Resources.UnloadUnusedAssets();
        }
    }

    private void InitProgressSlider()
    {
        m_progressSlider.maxValue = 1.0f;
        m_progressSlider.value = 0.0f;
    }

    private void InitVideoTime()
    {
        SetTimerText(0.0f, 0.0f);
    }

    private void TogglePlayPauseButton(bool play)
    {
        m_playButton.SetActive(play);
        m_pauseButton.SetActive(!play);
    }

    void Update()
	{
        if(m_videoTexture == null)
        {
            return;
        }

        m_videoTexture.Update();
        if(m_videoTexture.IsDone)
        {
            Stop();
        }
        if (m_videoTexture.IsReadyToPlay)
        {
            UpdateProgressSlider();
            UpdateVideoTime();
            if(m_playWhenReady)
            {
                m_playWhenReady = false;
                Play();
            }
        }
        UpdateVideoPlayerControlsVisibility();
    }

    private void UpdateVideoPlayerControlsVisibility()
    {
        bool showControls = true;
        if (m_autoHideControls)
        {
            if (!(m_isUserSeeking || InputUtils.IsMouseOverCollider(m_videoAreaCollider)))
            {
                showControls = false;
            }
        }

        if(showControls)
        {
            m_videoPlayerControls.Show();
        }
        else
        {
            m_videoPlayerControls.Hide();
        }
    }

    private void UpdateProgressSlider()
    {
        if(!m_isUserSeeking)
        {
            m_progressSlider.maxValue = Mathf.Max(m_videoTexture.Duration, 0.0f);
            m_progressSlider.value = Mathf.Max(m_videoTexture.Progress, 0.0f);
        }
    }

    private void UpdateVideoTime()
    {
        SetTimerText(m_videoTexture.Progress, m_videoTexture.Duration);
    }

    private void SetTimerText(float time, float duration)
    {
        string durationText = GetTimeFormatted(duration);
        string timeText = GetTimeFormatted(time);
        m_timeText.text = timeText + " / " + durationText;
    }

    private string GetTimeFormatted(float seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        String text = String.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        return text;
    }

    private void Stop()
    {
        if(m_videoTexture != null)
        {
            m_videoTexture.Stop();
            m_videoImagePoster.enabled = true;
            TogglePlayPauseButton(true);
            m_autoHideControls = false;
            m_playWhenReady = false;
        }
    }

    public void TogglePlay()
	{
		if((m_videoTexture != null) && m_videoTexture.IsReadyToPlay)
        {
            if (m_videoTexture.IsPlaying)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }
	}

    private void Play()
    {
        m_videoTexture.Play();
        m_videoImagePoster.enabled = false;
        TogglePlayPauseButton(false);
        m_autoHideControls = true;
    }

    private void Pause()
    {
        if (m_videoTexture != null)
        {
            m_videoTexture.Pause();
            TogglePlayPauseButton(true);
        }
    }

    public void OnUserSeekBegin()
    {
        if(!m_isUserSeeking)
        {
            if ((m_videoTexture != null) && m_videoTexture.IsReadyToPlay)
            {
                m_isUserSeeking = true;
                m_playWhenReady = false;
                m_wasPlayingOnSeekBegin = m_videoTexture.IsPlaying;
                Pause();
            }
        }
    }

    public void Seek()
    {
        if (m_videoTexture == null)
        {
            return;
        }

        if (m_isUserSeeking)
        {
            float seekTime = Mathf.Clamp(m_progressSlider.value, 0.0f, m_progressSlider.maxValue);
            m_videoTexture.Seek(seekTime);
        }
    }

    public void OnUserSeekEnd()
    {
        if (m_isUserSeeking)
        {
            m_isUserSeeking = false;
            if (m_videoTexture != null)
            {
                m_playWhenReady = m_wasPlayingOnSeekBegin;
            }
        }
    }

    public void SetVideo(string videoPath, Sprite videoPoster)
    {
        m_videoPath = videoPath;
        m_videoImagePoster.sprite = videoPoster;
        if (m_isStarted)
        {
            Pause();
            Initialize();
        }
    }

    public void ClearVideo()
    {
        DisposeVideoTexture();
    }
}
