using UnityEngine;
using System.Collections;

public class PortofolioShowcaseController : MonoBehaviour
{
    public Animator m_portofolioAnimator;
    public CategoriesGrid m_categoriesGrid;
    public Animator m_owlIntroSpeechBubble;

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
        m_owlIntroSpeechBubble.SetBool("isShowing", false);

        m_categoriesGrid.OnShowBegin();
    }

    public void HidePortofolio()
    {
        m_portofolioAnimator.SetBool("isShowing", false);

        m_categoriesGrid.OnHideBegin();
    }

    public void OnPortofolioShowAnimationFinished()
    {
        m_categoriesGrid.OnShowFinished();
    }

    public void OnPortofolioHideAnimationFinished()
    {
        m_categoriesGrid.OnHideFinished();
        m_owlIntroSpeechBubble.SetBool("isShowing", true);
    }
}
