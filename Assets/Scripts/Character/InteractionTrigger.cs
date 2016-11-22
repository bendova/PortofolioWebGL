using UnityEngine;
using System.Collections;
using System;

public class InteractionTrigger : MonoBehaviour
{
    public GameObject m_interactionTarget;
    public GameObject m_interactionMouseButton;
    public GameObject m_interactionMenuPage;

    private GameObject m_player;
    private Animator m_interactionTargetAnimator;
    private Animator m_mouseButtonAnimator;
    private Collider2D m_menuPageOpenCollider;
    private Animator m_menuPageAnimator;
    private CharController m_playerCharController;

    private bool m_isPlayerInsideTrigger = false;
    private bool m_isMouseButtonActive = false;
    private bool m_isMenuPageOpen = false;

    void Start()
    {
        m_interactionTargetAnimator = m_interactionTarget.GetComponent<Animator>();

        m_mouseButtonAnimator = m_interactionMouseButton.GetComponent<Animator>();
        m_menuPageOpenCollider = m_interactionMouseButton.GetComponent<Collider2D>();

        m_menuPageAnimator = m_interactionMenuPage.GetComponent<Animator>();

        m_player = PlayerUtils.GetPlayer();
        if (m_player)
        {
            m_playerCharController = m_player.GetComponent<CharController>();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == Tags.Player)
        {
            m_isPlayerInsideTrigger = true;
            SetTargetAnimation(true);
            SetTargetInteraction(true);
            SetPlayerLookAtTarget(m_interactionTarget);
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
            SetPlayerLookAtTarget(null);
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
        if(!isOpen)
        {
            UnlockPlayerMovement();
            if(m_isPlayerInsideTrigger)
            {
                SetTargetInteraction(true);
            }
        }
    }

    private void SetPlayerLookAtTarget(GameObject lookupTarget)
    {
        m_playerCharController.SetLookUpTarget(lookupTarget);
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
            MovePlayerIntoPosition();
        }
    }

    private void ProcessInputForMenuPage()
    {
        if (Input.GetKeyDown("escape"))
        {
            SetMenuPageOpen(false);
        }
    }

    private void MovePlayerIntoPosition()
    {
        if (m_playerCharController)
        {
            m_playerCharController.ScriptedMoveToPos(m_interactionTarget.transform, m_interactionMenuPage.transform);
            m_playerCharController.SetLookUpTarget(m_interactionMenuPage);
        }
    }

    private void UnlockPlayerMovement()
    {
        if (m_playerCharController)
        {
            m_playerCharController.StopScripteMoveToPos();
            m_playerCharController.SetLookUpTarget(null);
        }
    }

    public void CloseMenuPage()
    {
        SetMenuPageOpen(false);
    }
}
