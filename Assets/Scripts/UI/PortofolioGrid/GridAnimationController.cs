using UnityEngine;
using System.Collections;

public class GridAnimationController : MonoBehaviour
{
    public GridController m_gridController;

    private Animator m_animator;
    private bool m_isFullScreen = false;
    private bool m_isShowing = false;

	// Use this for initialization
	void Start ()
    {
        m_animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    // TODO Clean this up. Use animation triggers instead of bools.
    public void Show()
    {
        if(m_animator)
        {
            m_animator.SetBool("isHidden", false);
            m_animator.SetBool("isShowing_Default", true);
        }
    }

    public void Show_Landing()
    {
        if(m_animator)
        {
            m_animator.SetBool("isHidden", false);
            m_animator.SetBool("isShowing_Landing", true);
        }
    }

    public void Hide()
    {
        if(m_animator)
        {
            m_animator.SetBool("isHidden", true);
        }
    }

    public void OnShow()
    {
        if(!m_isShowing)
        {
            m_isShowing = true;
            m_gridController.OnShow();
        }
    }

    public void OnHide()
    {
        if (m_isShowing)
        {
            m_isShowing = false;
            m_gridController.OnHide();
            SetIsFullScreen(false);
        }
    }

    public void ToggleFullscreen()
    {
        SetIsFullScreen(!m_isFullScreen);
    }

    private void SetIsFullScreen(bool isFullScreen)
    {
        m_isFullScreen = isFullScreen;
        m_animator.SetBool("isFullscreen", m_isFullScreen);
    }
}
