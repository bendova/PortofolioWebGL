using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class VideoPlayerControls : MonoBehaviour
{
    public CanvasGroup m_controlsCanvasGroup;

    private float m_alphaFull = 1.0f;
    private float m_alphaNone = 0.0f;
    private float m_tweenDuration = 0.2f;

    public void Show()
    {
        if(!m_controlsCanvasGroup.interactable)
        {
            m_controlsCanvasGroup.interactable = true;
            StartAlphaTween(m_alphaFull);
        }
    }

    public void Hide()
    {
        if(m_controlsCanvasGroup.interactable)
        {
            m_controlsCanvasGroup.interactable = false;
            StartAlphaTween(m_alphaNone);
        }
    }

    private void StartAlphaTween(float targetAlpha)
    {
        Hashtable tweenParams = new Hashtable();
        tweenParams.Add("from", m_controlsCanvasGroup.alpha);
        tweenParams.Add("to", targetAlpha);
        tweenParams.Add("time", m_tweenDuration);
        tweenParams.Add("onupdate", "OnAlphaTweenUpdate");

        iTween.Stop(m_controlsCanvasGroup.gameObject);
        iTween.ValueTo(m_controlsCanvasGroup.gameObject, tweenParams);
    }

    private void OnAlphaTweenUpdate(float alpha)
    {
        m_controlsCanvasGroup.alpha = alpha;
    }
}
