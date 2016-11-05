using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public Image m_checkmark;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetSelected(bool isSelected)
    {
        m_checkmark.enabled = isSelected;
    }
}
