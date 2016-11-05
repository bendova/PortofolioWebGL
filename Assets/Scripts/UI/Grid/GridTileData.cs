using UnityEngine;
using System.Collections;

public class GridTileData : MonoBehaviour
{
    public string m_tileName;
    public string TileName { get { return m_tileName; } }

    public Sprite m_iconImage;
    public Sprite IconImage { get { return m_iconImage; } }

    public GridSubTileData[] m_datas;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
