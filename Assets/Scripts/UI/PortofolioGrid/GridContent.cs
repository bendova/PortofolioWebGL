using UnityEngine;
using System.Collections;
using System;

public class GridContent : MonoBehaviour
{
    public GameObject m_descriptionContainer;
    public GameObject m_videoContainer;
    public GameObject m_imageContainer;
    public BulletScroll m_bulletScroll;

    private GridDescriptionController m_descriptionController;
    private VideoPlayer m_videoPlayer;
    private GridImageController m_imageController;

    private GridTileData m_tileData;
    private int m_selectedDataIndex = -1;

    // Use this for initialization
    void Start ()
    {
        m_descriptionController = m_descriptionContainer.GetComponentInChildren<GridDescriptionController>();
        m_videoPlayer = m_videoContainer.GetComponentInChildren<VideoPlayer>();
        m_imageController = m_imageContainer.GetComponentInChildren<GridImageController>();
        m_bulletScroll.onBulletSelected.AddListener((int index) =>
        {
            RefreshContent(index);
        });

        HideAll();
    }

    private void HideAll()
    {
        m_descriptionContainer.SetActive(false);
        m_videoContainer.SetActive(false);
        m_imageContainer.SetActive(false);
        if (m_videoPlayer)
        {
            m_videoPlayer.ClearVideo();
        }
    }

    // Update is called once per frame
    void Update()
    {
	
	}

    public void SetTileData(GridTileData tileData)
    {
        m_tileData = tileData;
        m_bulletScroll.SetBulletsCount(m_tileData.m_datas.Length);

        m_selectedDataIndex = -1;
        RefreshContent(0);
    }

    private void RefreshContent(int dataIndex)
    {
        if(m_selectedDataIndex != dataIndex)
        {
            if (m_tileData.m_datas.Length > 0 && dataIndex < m_tileData.m_datas.Length)
            {
                HideAll();

                m_selectedDataIndex = dataIndex;
                GridSubTileData data = m_tileData.m_datas[dataIndex];
                RefreshDescription(data);
                if (data.HasVideo)
                {
                    RefreshVideo(data);
                }
                else if (data.HasImage)
                {
                    RefreshImage(data);
                }
            }
        }
    }

    private void RefreshDescription(GridSubTileData tileData)
    {
        m_descriptionContainer.SetActive(true);
        if(m_descriptionController)
        {
            m_descriptionController.SetTileData(tileData);
        }
    }

    private void RefreshVideo(GridSubTileData tileData)
    {
        m_videoContainer.SetActive(true);
        if(m_videoPlayer)
        {
            m_videoPlayer.SetVideo(tileData.VideoPath, tileData.m_videoImagePoster);
        }
    }

    private void RefreshImage(GridSubTileData tileData)
    {
        m_imageContainer.SetActive(true);
        if(m_imageController)
        {
            m_imageController.SetImage(tileData.Image);
        }
    }

    public void OnShow()
    {

    }

    public void OnHide()
    {
        m_videoPlayer.ClearVideo();
    }
}
