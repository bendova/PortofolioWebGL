using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridImageController : MonoBehaviour
{
    public Image m_image;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetImage(Sprite image)
    {
        m_image.sprite = image;
    }
}
