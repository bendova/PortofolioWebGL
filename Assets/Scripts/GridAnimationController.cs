using UnityEngine;
using System.Collections;

public class GridAnimationController : MonoBehaviour
{
    public GridController m_gridController;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void OnShow()
    {
        m_gridController.OnShow();
    }

    public void OnHide()
    {
        m_gridController.OnHide();
    }
}
