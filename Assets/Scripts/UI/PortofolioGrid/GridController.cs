using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    public GameObject m_tilePrefab;
    public GameObject m_tileContainer;
    public float m_gridSpacingH = 10.0f;

    public GridContent m_content;
    public GridTileDataList m_gridTileDataList;

    private List<GridTile> m_gridTiles;
    private GridTile m_activeTile;

	void Start()
    {
        if(m_gridTileDataList)
        {
            CreateTiles();
            InitTiles();
        }
    }

    public void SetGridTileDatas(GridTileDataList tileDatas)
    {
        m_gridTileDataList = tileDatas;
        int count = Mathf.Min(tileDatas.m_gridTileDatas.Length, m_gridTiles.Count);
        for(int i = 0; i < count; ++i)
        {
            m_gridTiles[i].SetTileData(tileDatas.m_gridTileDatas[i]);
        }
        InitTiles();
    }

    private void CreateTiles()
    {
        float tileX = 0.0f;

        m_gridTiles = new List<GridTile>();
        foreach (GridTileData tileData in m_gridTileDataList.m_gridTileDatas)
        {
            if(tileData != null)
            {
                GameObject newTile = GameObject.Instantiate(m_tilePrefab, m_tileContainer.transform, false) as GameObject;
                newTile.transform.localPosition = new Vector3(tileX, 0.0f, 0.0f);
                RectTransform rectTransform = newTile.transform as RectTransform;
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = new Vector2(tileX, 0.0f);
                    tileX += rectTransform.rect.width + m_gridSpacingH;
                }

                GridTile gridTile = newTile.GetComponent<GridTile>();
                if (gridTile != null)
                {
                    m_gridTiles.Add(gridTile);
                    gridTile.SetTileData(tileData);
                }

                Button gridTileButton = newTile.GetComponent<Button>();
                if(gridTileButton != null)
                {
                    gridTileButton.onClick.AddListener(() =>
                    {
                        OnTileSelected(gridTile);
                    });
                }

                newTile.name = newTile.name + "_" + tileData.name;
            }
        }
    }

    private void InitTiles()
    {
        UnfocusAllTiles();
        FocusFirstTile();
    }

    private void UnfocusAllTiles()
    {
        foreach (GridTile tile in m_gridTiles)
        {
            if (tile != null)
            {
                tile.OnFocusLost();
            }
        }
    }

    private void FocusFirstTile()
    {
        if (m_gridTiles.Count > 0)
        {
            FocusTile(m_gridTiles[0]);
        }
    }

    void Update()
    {
	
	}

    public void OnTileSelected(GridTile tile)
    {
        Debug.Log("OnTileSelected() tile: " + tile.gameObject.name);

        if(m_activeTile != tile)
        {
            UnfocusTile();
            FocusTile(tile);
        }
    }

    private void UnfocusTile()
    {
        if(m_activeTile != null)
        {
            m_activeTile.OnFocusLost();
            m_activeTile = null;
        }
    }

    private void FocusTile(GridTile gridTile)
    {
        m_activeTile = gridTile;
        m_activeTile.OnFocusGained();

        m_content.SetTileData(m_activeTile.TileData);
    }

    public void OnShow()
    {
        m_content.OnShow();
        FocusFirstTile();
    }

    public void OnHide()
    {
        UnfocusAllTiles();
        m_content.OnHide();
    }
}
