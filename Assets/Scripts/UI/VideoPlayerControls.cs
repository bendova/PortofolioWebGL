using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VideoPlayerControls : MonoBehaviour
{
    public CanvasGroup m_controlsCanvasGroup;

	// Use this for initialization
	void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update()
    {
	}

    public void Show()
    {
        m_controlsCanvasGroup.alpha = 1.0f;
        m_controlsCanvasGroup.interactable = true;
    }

    public void Hide()
    {
        m_controlsCanvasGroup.alpha = 0.0f;
        m_controlsCanvasGroup.interactable = false;
    }
}
