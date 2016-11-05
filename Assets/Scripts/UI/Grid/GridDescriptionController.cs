using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridDescriptionController : MonoBehaviour
{
    public Text m_descriptionText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetTileData(GridSubTileData data)
    {
        m_descriptionText.text = data.Description;
    }
}
