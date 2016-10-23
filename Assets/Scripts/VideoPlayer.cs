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
    public GameObject m_playButton;
    public GameObject m_pauseButton;

    private IStreamingVideoPlugin m_videoTexture;

    private bool m_isUserSeeking = false;
    private bool m_isStarted = false;

    void Start ()
    {
        m_isStarted = true;
        Initialize();
    }

    private void Initialize()
    {
        m_videoTexture = CreateNewVideoTexture(m_videoPath);
        TogglePlayPauseButton(true);
        InitProgressSlider();
        InitVideoTime();
    }

    private IStreamingVideoPlugin CreateNewVideoTexture(string videoPath)
    {
        IStreamingVideoPlugin videoTexture = null;
        if(videoPath.Length > 0)
        {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
            videoTexture = new NativeStreamingVideoPlugin(videoPath, gameObject);
#elif UNITY_WEBGL
            videoTexture = new WebGLStreamingVideoPlugin(videoPath, gameObject);
#endif
        }
        return videoTexture;
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
            TogglePlayPauseButton(true);
        }
        if (m_videoTexture.IsReadyToPlay)
        {
            UpdateProgressSlider();
            UpdateVideoTime();
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

    public void Stop()
    {
        if(m_videoTexture != null)
        {
            m_videoTexture.Stop();
            TogglePlayPauseButton(true);
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

    public void Play()
    {
        if(m_videoTexture != null)
        {
            m_videoTexture.Play();
            TogglePlayPauseButton(false);
        }
    }

    public void Pause()
    {
        if (m_videoTexture != null)
        {
            m_videoTexture.Pause();
            TogglePlayPauseButton(true);
        }
    }

    public void OnUserSeekBegin()
    {
        if (m_videoTexture == null)
        {
            return;
        }

        if (m_videoTexture.IsReadyToPlay && !m_isUserSeeking)
        {
            m_isUserSeeking = true;
            Pause();
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
            Play();
        }
    }

    public void SetVideoPath(string videoPath)
    {
        if(m_videoPath != videoPath)
        {
            m_videoPath = videoPath;
            if(m_isStarted)
            {
                Pause();
                Initialize();
            }
        }
    }
}
