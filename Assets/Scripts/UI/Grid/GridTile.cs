using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridTile : MonoBehaviour
{
    private GridTileData m_tileData;
    public GridTileData TileData { get { return m_tileData; } }

    public Text m_nameText;
    public Image m_icon;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Start() tile: " + gameObject.name);
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void OnFocusLost()
    {
        
    }

    public void OnFocusGained()
    {
        
    }

    public void SetTileData(GridTileData tileData)
    {
        if(m_tileData != tileData)
        {
            m_tileData = tileData;
            m_nameText.text = m_tileData.TileName;
            m_icon.sprite = m_tileData.IconImage;
        }
    }
}
