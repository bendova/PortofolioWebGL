using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

public class ContactPage : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern void SendEmailMessage(string gameObjName, string senderEmail, string message);
#else
    private static void SendEmailMessage(string gameObjName, string senderEmail, string message)
    {
        Debug.Log("SendEmailMessage() is not supported in Editor.\n" + 
             "senderEmail: " + senderEmail + ", message: " + message);

        GameObject gameObject = GameObject.Find(gameObjName);
        if(gameObject)
        {
            ContactPage contact = gameObject.GetComponent<ContactPage>();
            if(contact)
            {
                //contact.Invoke("TestMessageFailure", 2);
                contact.Invoke("OnSendEmailMessageSuccess", 2);
            }
        }
    }
#endif

    private const string MatchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
              + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

    public InputField m_textFieldEmail;
    public InputField m_textFieldMessage;
    public int m_minMessageLength = 16;
    public ContactFeedbackController m_feedback;
    public GameObject m_sendButton;
    public GameObject m_closeButton;
    public InteractionTrigger m_interactionTrigger;

    // Use this for initialization
    void Start()
    {
        m_sendButton.SetActive(true);
        EnableSendAndClose(true);
    }

    public void OnMenuPageOpening()
    {
        ClearTextFields();
        m_sendButton.SetActive(true);
        EnableSendAndClose(true);
    }

    public void OnMenuPageClosing()
    {
        m_feedback.ResetMessage();
    }

    private void EnableClose(bool enable)
    {
        SetButtonInteractable(m_closeButton, enable);
        if(m_interactionTrigger)
        {
            m_interactionTrigger.SetCanCloseMenuPage(enable);
        }
    }

    private void EnableSend(bool enable)
    {
        SetButtonInteractable(m_sendButton, enable);
    }

    private void EnableSendAndClose(bool enable)
    {
        EnableSend(enable);
        EnableClose(enable);
    }

    public void SendEmailMessage()
    {
        string senderEmail = m_textFieldEmail.text;
        if (ValidateEmail(senderEmail))
        {
            string message = m_textFieldMessage.text;
            if (ValidateMessage(message))
            {
                SendEmailMessage(gameObject.name, senderEmail, message);
                ShowSendInProgress();
                EnableSendAndClose(false);
            }
        }
    }

    private void OnSendEmailMessageSuccess()
    {
        Debug.Log("OnSendEmailMessageSuccess()");
        ShowSendSuccess();
        EnableClose(true);
    }

    private void TestMessageFailure()
    {
        OnSendEmailMessageFailed("This is a test error");
    }

    private void OnSendEmailMessageFailed(string error)
    {
        Debug.Log("OnSendEmailMessageFailed() error: " + error);
        ShowSendFailed();
        EnableSendAndClose(true);
    }

    private bool ValidateEmail(string senderEmail)
    {
        bool isValid = IsEmailAddressValid(senderEmail);
        if(!isValid)
        {
            m_feedback.ShowInvalidEmail();
        }
        return isValid;
    }

    private bool IsEmailAddressValid(string address)
    {
        try
        {
            return Regex.IsMatch(address,
                  @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                  RegexOptions.IgnoreCase);
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool ValidateMessage(string message)
    {
        bool isValid = IsMessageValid(message);
        if(!isValid)
        {
            m_feedback.ShowInvalidMessage();
        }
        return isValid;
    }

    private bool IsMessageValid(string message)
    {
        return message.Length >= m_minMessageLength;
    }

    private void ShowSendInProgress()
    {
        m_feedback.ShowSendingMessage();
    }

    private void ShowSendSuccess()
    {
        m_feedback.ShowMessageSent();
    }

    private void ShowSendFailed()
    {
        m_feedback.ShowSendMessageFailed();
    }

    private void ClearTextFields()
    {
        m_textFieldEmail.text = "";
        m_textFieldMessage.text = "";
    }

    private void SetButtonInteractable(GameObject button, bool interactable)
    {
        button.GetComponent<Button>().interactable = interactable;
    }
}
