using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridTile : MonoBehaviour
{
    private GridTileData m_tileData;
    public GridTileData TileData { get { return m_tileData; } }

    public Image m_icon;

    private Animator m_buttonAnimator;

    // Use this for initialization
    void Start()
    {
        m_buttonAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void OnFocusLost()
    {
        if(m_buttonAnimator && m_buttonAnimator.isActiveAndEnabled)
        {
            m_buttonAnimator.SetBool("isSelected", false);
        }
    }

    public void OnFocusGained()
    {
        if (m_buttonAnimator && m_buttonAnimator.isActiveAndEnabled)
        {
            m_buttonAnimator.SetBool("isSelected", true);
        }
    }

    public void SetTileData(GridTileData tileData)
    {
        if(m_tileData != tileData)
        {
            m_tileData = tileData;
            m_icon.sprite = m_tileData.IconImage;
        }
    }
}
