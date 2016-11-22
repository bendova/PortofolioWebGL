using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class CategoriesGrid : MonoBehaviour
{
    // Editor properties
    public CategoryGridTile[] m_tiles;
    public CategoryGridThumbnail[] m_thumbnails;
    public GridAnimationController m_portofolioGrid;
    public Animator m_animator;

    // Use this for initialization
    void Start()
    {
        InitGridTiles();
        InitThumbnails();
        SwitchToTileView();
    }

    private void InitGridTiles()
    {
        foreach(CategoryGridTile tile in m_tiles)
        {
            tile.Reset();
            tile.EnableInput();
            tile.OnClickCb.AddListener((CategoryGridTile gridTile) =>
            {
                OnGridTileClicked(gridTile);
            });
        }
    }

    private void InitThumbnails()
    {
        int count = Mathf.Min(m_tiles.Length, m_thumbnails.Length);
        for(int i = 0; i < count; ++i)
        {
            CategoryGridTile tile = m_tiles[i];
            CategoryGridThumbnail thumbnail = m_thumbnails[i];
            thumbnail.OnClickCb.AddListener((thumbnailParam) =>
            {
                OnGridThumbnailClicked(thumbnailParam);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
	
	}

    private void OnGridTileClicked(CategoryGridTile tile)
    {
        foreach (CategoryGridTile it in m_tiles)
        {
            it.BlockInput();
        }
        SwitchToGridView();
    }

    private void SwitchToGridView()
    {
        SetThumbnailsActive(true);
        SetTilesActive(false);
        m_animator.SetTrigger("ShowGridView");
    }

    private void OnSwitchToGridViewAnimationFinished()
    {
        if (m_thumbnails.Length > 0)
        {
            m_thumbnails[0].SetAsSelected();
        }
        m_portofolioGrid.Show_Landing();
    }

    private void SwitchToTileView()
    {
        SetThumbnailsActive(false);
        SetTilesActive(true);
        m_portofolioGrid.Hide();
        m_animator.SetTrigger("ShowTileView");
    }

    private void OnGridThumbnailClicked(CategoryGridThumbnail thumbnail)
    {
        foreach (CategoryGridThumbnail it in m_thumbnails)
        {
            if(it != thumbnail)
            {
                it.Reset();
            }
        }
        m_portofolioGrid.m_gridController.SetGridTileDatas(thumbnail.m_portofolioGridTileDatas);
    }

    private void SetTilesActive(bool active)
    {
        foreach (CategoryGridTile tile in m_tiles)
        {
            tile.Reset();
            if (active)
            {
                tile.EnableInput();
            }
            else
            {
                tile.BlockInput();
            }
        }
    }

    private void SetThumbnailsActive(bool active)
    {
        foreach (CategoryGridThumbnail thumb in m_thumbnails)
        {
            thumb.Reset();
            if (active)
            {
                thumb.EnableInput();
            }
            else
            {
                thumb.BlockInput();
            }
        }
    }

    public void Reset()
    {
        SwitchToTileView();
    }
}
