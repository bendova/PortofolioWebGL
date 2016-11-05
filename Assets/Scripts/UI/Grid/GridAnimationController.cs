using UnityEngine;
using System.Collections;

public class GridAnimationController : MonoBehaviour
{
    public GridController m_gridController;

    private Animator m_animator;
    private bool m_isFullScreen = false;

	// Use this for initialization
	void Start ()
    {
        m_animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void OnShow()
    {
        m_gridController.OnShow();
    }

    public void OnHide()
    {
        m_gridController.OnHide();
        SetIsFullScreen(false);
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
