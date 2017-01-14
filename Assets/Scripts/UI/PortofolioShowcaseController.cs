using UnityEngine;
using System.Collections;

public class PortofolioShowcaseController : MonoBehaviour
{
    public Animator m_showcaseAnimator;

    public CategoriesGrid m_categoriesGrid;

    public Animator m_contactPageAnimator;
    public ContactPage m_contactPage;

    public Animator m_owlIntroSpeechBubble;
    public Animator m_owlContactSpeechBubble;

    private bool m_isShowingHome = true;

    private bool m_isPortfolioOpen = false;
    private bool m_isShowingPortfolio = false;

    private bool m_isContactOpen = false;
    private bool m_isShowingContact = false;

    // Use this for initialization
    void Start()
    {
        m_contactPageAnimator.SetBool("isShowing_Landing", true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToHome()
    {
        if(!m_isShowingHome)
        {
            m_isShowingHome = true;

            ClosePortofolio();
            CloseContact();
        }
    }

    public void OpenPortfolio()
    {
        m_isPortfolioOpen = true;
        m_isShowingHome = false;

        StartShowPortfolioAnimation();
        StartHideContactAnimation();
    }

    private void StartShowPortfolioAnimation()
    {
        if(m_isPortfolioOpen && !m_isShowingPortfolio)
        {
            m_isShowingPortfolio = true;
            m_showcaseAnimator.SetBool("isShowingPortfolio", true);
            m_owlIntroSpeechBubble.SetBool("isShowing", false);
            m_categoriesGrid.OnShowBegin();
        }
    }

    public void OnPortofolioShowAnimationFinished()
    {
        m_categoriesGrid.OnShowFinished();
    }

    private void ClosePortofolio()
    {
        if(m_isPortfolioOpen)
        {
            StartPortfolioHideAnimation();
            m_isPortfolioOpen = false;
        }
    }

    private void StartPortfolioHideAnimation()
    {
        if(m_isPortfolioOpen && m_isShowingPortfolio)
        {
            m_isShowingPortfolio = false;
            m_showcaseAnimator.SetBool("isShowingPortfolio", false);
            m_categoriesGrid.OnHideBegin();
        }
    }

    public void OnPortofolioHideAnimationFinished()
    {
        m_categoriesGrid.OnHideFinished();
        if (m_isShowingHome)
        {
            ShowIntroSpeechBubble();
        }
    }

    public void OpenContact()
    {
        m_isContactOpen = true;
        m_isShowingHome = false;

        StartShowContactAnimation();
        StartPortfolioHideAnimation();
    }

    private void StartShowContactAnimation()
    {
        if(m_isContactOpen && !m_isShowingContact)
        {
            m_isShowingContact = true;
            m_contactPage.OnMenuPageOpening();
            m_showcaseAnimator.SetBool("isShowingContact", true);
            m_owlContactSpeechBubble.SetBool("isShowing", true);
            m_owlIntroSpeechBubble.SetBool("isShowing", false);
        }
    }

    private void CloseContact()
    {
        if(m_isContactOpen)
        {
            StartHideContactAnimation();
            m_isContactOpen = false;
        }
    }

    private void StartHideContactAnimation()
    {
        if(m_isContactOpen && m_isShowingContact)
        {
            m_isShowingContact = false;
            m_contactPage.OnMenuPageClosing();
            m_showcaseAnimator.SetBool("isShowingContact", false);
            m_owlContactSpeechBubble.SetBool("isShowing", false);
        }
    }

    public void OnContactHideAnimationFinished()
    {
        if(m_isShowingHome)
        {
            ShowIntroSpeechBubble();
        }
    }

    private void ShowIntroSpeechBubble()
    {
        m_owlIntroSpeechBubble.SetBool("isShowing", true);
    }
}
