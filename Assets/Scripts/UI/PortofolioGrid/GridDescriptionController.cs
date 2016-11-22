using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridDescriptionController : MonoBehaviour
{
    public Image m_descriptionImage;
    
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetTileData(GridSubTileData data)
    {
        m_descriptionImage.sprite = data.DescriptionImage;
    }
}
