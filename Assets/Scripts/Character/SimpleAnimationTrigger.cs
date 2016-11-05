using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Collider2D))]
public class SimpleAnimationTrigger : MonoBehaviour
{
    public Animator m_animationToTrigger;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == Tags.Player)
        {
            SetTargetAnimation(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == Tags.Player)
        {
            SetTargetAnimation(false);
        }
    }

    private void SetTargetAnimation(bool enable)
    {
        m_animationToTrigger.SetBool("animate", enable);
    }
}
