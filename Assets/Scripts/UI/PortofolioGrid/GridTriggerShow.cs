using UnityEngine;
using System.Collections;

public class GridTriggerShow : MonoBehaviour
{
    public Collider2D m_triggerCollider;
    public GridAnimationController m_gridAnimation;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update()
    {
        if (InputUtils.IsLeftClickOnCollider(m_triggerCollider))
        {
            m_gridAnimation.Show_Landing();
        }
    }
}
