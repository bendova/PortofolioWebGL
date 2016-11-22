using UnityEngine;
using System.Collections;
using System;

public class categoryPressed : MonoBehaviour {
	public GameObject imageElement_animateOnClick;
    public GameObject drawnElement_animateonClick;
    public GameObject textElement_animateonClick;

    private Animator imageElement_animator;
    private Animator drawnElement_animator;
    private Animator textElement_animator;
    private Collider2D m_collider;
	
	// Use this for initialization
	void Start ()
    {
        imageElement_animator = imageElement_animateOnClick.GetComponent<Animator>();
        drawnElement_animator = drawnElement_animateonClick.GetComponent<Animator>();
        textElement_animator = textElement_animateonClick.GetComponent<Animator>();

        m_collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (InputUtils.IsLeftClickOnCollider(m_collider))
        {
            SetClicked(true);
        }
    }

    private void SetClicked(bool enable)
    {
        imageElement_animator.SetBool("mouseClick", enable);
        drawnElement_animator.SetBool("mouseClick", enable);
        textElement_animator.SetBool("mouseClick", enable);
    }

    public void Hide()
    {
        SetClicked(false);
    }
}
