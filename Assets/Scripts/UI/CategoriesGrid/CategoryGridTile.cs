using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class CategoryGridTile : MonoBehaviour
{
    private const string RESET = "Reset";
    private const string MOUSE_OVER = "MouseOver";
    private const string MOUSE_CLICK = "Clicked";

    [Serializable]
    public class OnClickedEvent : UnityEvent<CategoryGridTile>
    {
        public OnClickedEvent() { }
    }
    public OnClickedEvent OnClickCb { get; set; }

    public int TileIndex { get; set; }

    // Editor properties
    public Collider2D m_tileCollider;
    public Animator m_tileImageAnimator;
    public Animator m_tileNameAnimator;
    public Animator m_tileHoverAnimator;

    // Private fields
    private bool m_handleInput = true;

    public CategoryGridTile()
    {
        OnClickCb = new OnClickedEvent();
    }

    // Use this for initialization
    void Start()
    {

    }
	
	// Update is called once per frame
	void Update()
    {
        if(!m_handleInput)
        {
            return;
        }

        UpdateMouseOver();
        UpdateMouseClick();
	}

    private void UpdateMouseOver()
    {
        bool isMouseOver = InputUtils.IsMouseOverCollider(m_tileCollider);
        SetAnimationMouseOver(isMouseOver);
    }

    private void SetAnimationMouseOver(bool isMouseOver)
    {
        SetAnimatorBool(m_tileImageAnimator, MOUSE_OVER, isMouseOver);
        SetAnimatorBool(m_tileNameAnimator, MOUSE_OVER, isMouseOver);
        SetAnimatorBool(m_tileHoverAnimator, MOUSE_OVER, isMouseOver);
    }

    private void SetAnimatorBool(Animator animator, string key, bool value)
    {
        if (animator.isActiveAndEnabled)
        {
            animator.SetBool(key, value);
        }
    }

    private void UpdateMouseClick()
    {
        if(InputUtils.IsLeftClickOnCollider(m_tileCollider))
        {
            BlockInput();

            m_tileImageAnimator.SetTrigger(MOUSE_CLICK);
            m_tileNameAnimator.SetTrigger(MOUSE_CLICK);
            m_tileHoverAnimator.SetTrigger(MOUSE_CLICK);

            OnClickCb.Invoke(this);
        }
    }

    public void Reset()
    {
        SetAnimationMouseOver(false);
        m_tileImageAnimator.SetTrigger(RESET);
    }

    public void BlockInput()
    {
        m_handleInput = false;
    }

    public void EnableInput()
    {
        m_handleInput = true;
    }
}
