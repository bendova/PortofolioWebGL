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

    void Start ()
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        m_videoTexture = new NativeStreamingVideoPlugin(m_videoPath, gameObject);
#elif UNITY_WEBGL
        m_videoTexture = new WebGLStreamingVideoPlugin(m_videoPath, gameObject);
#endif
        TogglePlayPauseButton(true);
    }

    private void TogglePlayPauseButton(bool play)
    {
        m_playButton.SetActive(play);
        m_pauseButton.SetActive(!play);
    }

    void Update()
	{
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
        string duration = GetTimeFormatted(m_videoTexture.Duration);
        string time = GetTimeFormatted(m_videoTexture.Progress);
        m_timeText.text = time + " / " + duration;
    }

    private string GetTimeFormatted(float seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        String text = String.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        return text;
    }

    public void TogglePlay()
	{
		if(m_videoTexture.IsReadyToPlay)
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
        TogglePlayPauseButton(false);
    }

    private void Pause()
    {
        m_videoTexture.Pause();
        TogglePlayPauseButton(true);
    }

    public void OnUserSeekBegin()
    {
        if (m_videoTexture.IsReadyToPlay && !m_isUserSeeking)
        {
            m_isUserSeeking = true;
            Pause();
        }
    }

    public void Seek()
    {
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
}
