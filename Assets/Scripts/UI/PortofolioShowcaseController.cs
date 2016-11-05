using UnityEngine;
using System.Collections;

public class PortofolioShowcaseController : MonoBehaviour
{
    public Animator m_portofolioAnimator;
    public categoryPressed m_categoryUI;
    public categoryPressed m_categoryDecals;
    public categoryPressed m_categoryPersonal;
    public categoryPressed m_categoryBackground;
    public categoryPressed m_categoryThumbnails;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowPortofolio()
    {
        m_portofolioAnimator.SetBool("isShowing", true);
    }

    public void HidePortofolio()
    {
        m_portofolioAnimator.SetBool("isShowing", false);
        m_categoryUI.Hide();
        m_categoryDecals.Hide();
        m_categoryPersonal.Hide();
        m_categoryBackground.Hide();
        m_categoryThumbnails.Hide();
    }
}
