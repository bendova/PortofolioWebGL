using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class CategoryGridThumbnail : MonoBehaviour
{
    private const string RESET = "Reset";
    private const string MOUSE_OVER = "MouseOver";
    private const string MOUSE_CLICK = "Clicked";

    [Serializable]
    public class OnClickedEvent : UnityEvent<CategoryGridThumbnail>
    {
        public OnClickedEvent() { }
    }
    public OnClickedEvent OnClickCb { get; set; }

    public Collider2D m_collider;
    public SpriteRenderer m_spriteRenderer;
    public Animator m_imageAnimator;

    private bool m_handleInput = true;
    public GridTileDataList m_portofolioGridTileDatas;
    public GridTileDataList PortofolioGridTileDatas
    {
        get { return m_portofolioGridTileDatas; }
        set { m_portofolioGridTileDatas = value; }
    }

    public CategoryGridThumbnail()
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
        bool isMouseOver = InputUtils.IsMouseOverCollider(m_collider);
        m_imageAnimator.SetBool(MOUSE_OVER, isMouseOver);
    }

    private void UpdateMouseClick()
    {
        if (InputUtils.IsLeftClickOnCollider(m_collider))
        {
            OnSelected();
        }
    }

    private void OnSelected()
    {
        m_imageAnimator.SetTrigger(MOUSE_CLICK);
        OnClickCb.Invoke(this);
    }

    public void Reset()
    {
        if(m_imageAnimator.isActiveAndEnabled)
        {
            m_imageAnimator.SetBool(MOUSE_OVER, false);
            m_imageAnimator.SetTrigger(RESET);
        }
    }

    public void BlockInput()
    {
        m_handleInput = false;
    }

    public void EnableInput()
    {
        m_handleInput = true;
    }

    public void SetAsSelected()
    {
        OnSelected();
    }
}
