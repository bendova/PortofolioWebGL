using UnityEngine;
using System.Collections;

public class ContactPageAnimationController : MonoBehaviour
{
    public ContactPage m_contactPage;

    public void OnMenuPageOpening()
    {
        m_contactPage.OnMenuPageOpening();
    }

    public void OnMenuPageClosing()
    {
        m_contactPage.OnMenuPageClosing();
    }
}
