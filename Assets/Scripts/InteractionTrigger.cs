using UnityEngine;
using System.Collections;
using System;

public class InteractionTrigger : MonoBehaviour
{
    public GameObject m_interactionTarget;
    public GameObject m_playerLookupTarget;
    public GameObject m_interactionMouseButton;
    public GameObject m_interactionMenuPage;

    private Animator m_interactionTargetAnimator;
    private Animator m_mouseButtonAnimator;
    private Collider2D m_menuPageOpenCollider;
    private Animator m_menuPageAnimator;
    private Collider2D m_menuPageCloseCollider;

    private bool m_isPlayerInsideTrigger = false;
    private bool m_isMouseButtonActive = false;
    private bool m_isMenuPageOpen = false;
    private GameObject m_player = null;

    void Start()
    {
        m_interactionTargetAnimator = m_interactionTarget.GetComponent<Animator>();

        m_mouseButtonAnimator = m_interactionMouseButton.GetComponent<Animator>();
        m_menuPageOpenCollider = m_interactionMouseButton.GetComponent<Collider2D>();

        m_menuPageAnimator = m_interactionMenuPage.GetComponent<Animator>();
        m_menuPageCloseCollider = m_interactionMenuPage.GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == Tags.Player)
        {
            m_isPlayerInsideTrigger = true;
            m_player = collider.gameObject;
            SetTargetAnimation(true);
            SetTargetInteraction(true);
            SetPlayerLookAtTarget(collider.gameObject, m_playerLookupTarget);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == Tags.Player)
        {
            m_isPlayerInsideTrigger = false;
            SetTargetAnimation(false);
            SetTargetInteraction(false);
            SetMenuPageOpen(false);
            SetPlayerLookAtTarget(collider.gameObject, null);
        }
    }

    private void SetTargetAnimation(bool enable)
    {
        m_interactionTargetAnimator.SetBool("animate", enable);
    }

    private void SetTargetInteraction(bool enable)
    {
        m_mouseButtonAnimator.SetBool("mouseappear", enable);
        m_isMouseButtonActive = enable;
    }

    private void SetMenuPageOpen(bool isOpen)
    {
        m_menuPageAnimator.SetBool("isShowing", isOpen);
        m_isMenuPageOpen = isOpen;
        if(!isOpen && m_isPlayerInsideTrigger)
        {
            SetTargetInteraction(true);
        }
    }

    private void SetPlayerLookAtTarget(GameObject player, GameObject lookupTarget)
    {
        CharController controller = player.GetComponent<CharController>();
        if(controller)
        {
            controller.SetLookUpTarget(lookupTarget);
        }
    }

    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (m_isMouseButtonActive)
        {
            ProcessInputForMouseButton();
        }
        else if (m_isMenuPageOpen)
        {
            ProcessInputForMenuPage();
        }
    }

    private void ProcessInputForMouseButton()
    {
        if(InputUtils.IsLeftClickOnCollider(m_menuPageOpenCollider))
        {
            SetTargetInteraction(false);
            SetMenuPageOpen(true);
        }
    }

    private void ProcessInputForMenuPage()
    {
        if(InputUtils.IsLeftClickOnCollider(m_menuPageCloseCollider))
        {
            SetMenuPageOpen(false);
        }
        else if (Input.GetKeyDown("escape"))
        {
            SetMenuPageOpen(false);
        }
    }
}
