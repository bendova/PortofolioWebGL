using UnityEngine;
using System.Collections;

public class PortofolioShowcaseController : MonoBehaviour
{
    public Animator m_portofolioAnimator;

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
    }
}
