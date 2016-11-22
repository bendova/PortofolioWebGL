using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContactFeedbackController : MonoBehaviour
{
    private static string TriggerReset                  = "TriggerReset";
    private static string TriggerSendingMessage         = "TriggerSendingMessage";
    private static string TriggerMessageSent            = "TriggerMessageSent";
    private static string TriggerErrorSendMessageFailed = "TriggerErrorSendMessageFailed";
    private static string TriggerErrorInvalidEmail      = "TriggerErrorInvalidEmail";
    private static string TriggerErrorInvalidMessage    = "TriggerErrorInvalidMessage";

    public Animator m_animator;

	// Use this for initialization
	void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update()
    {
	
	}

    public void ShowSendingMessage()
    {
        SetAnimationTrigger(TriggerSendingMessage);
    }

    public void ShowMessageSent()
    {
        SetAnimationTrigger(TriggerMessageSent);
    }

    public void ShowSendMessageFailed()
    {
        SetAnimationTrigger(TriggerErrorSendMessageFailed);
    }

    public void ShowInvalidEmail()
    {
        SetAnimationTrigger(TriggerErrorInvalidEmail);
    }

    public void ShowInvalidMessage()
    {
        SetAnimationTrigger(TriggerErrorInvalidMessage);
    }

    public void ResetMessage()
    {
        m_animator.SetTrigger(TriggerReset);
    }

    private void SetAnimationTrigger(string trigger)
    {
        ResetMessage();
        m_animator.SetTrigger(trigger);
    }
}
