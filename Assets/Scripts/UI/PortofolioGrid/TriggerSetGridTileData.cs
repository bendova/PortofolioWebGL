using UnityEngine;
using System.Collections;

public class TriggerSetGridTileData : MonoBehaviour
{
    public Collider2D m_collider;
    public GridController m_grid;
    public GridTileDataList m_tileDataList;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update()
    {
        if (InputUtils.IsLeftClickOnCollider(m_collider))
        {
            m_grid.SetGridTileDatas(m_tileDataList);
        }
    }
}
