using UnityEngine;
using System.Collections;

public class LampHoverAnim : MonoBehaviour {
	public GameObject m_animateOnHover;

    private Animator m_animator;
    private Collider2D m_collider;
	
	// Use this for initialization
	void Start ()
    {
        m_animator = m_animateOnHover.GetComponent<Animator>();
        m_collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (InputUtils.IsMouseOverCollider(m_collider))
        {
            m_animator.SetBool("mouseOver", true);
        }
        else
        {
            m_animator.SetBool("mouseOver", false);
        }
    }
}
