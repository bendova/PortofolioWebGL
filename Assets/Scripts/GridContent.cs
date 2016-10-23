using UnityEngine;
using System.Collections;
using System;

public class GridContent : MonoBehaviour
{
    public GameObject m_descriptionContainer;
    public GameObject m_videoContainer;
    public GameObject m_imageContainer;

    private GridDescriptionController m_descriptionController;
    private VideoPlayer m_videoPlayer;
    private GridImageController m_imageController;

    private GridTileData m_tileData;

    // Use this for initialization
    void Start ()
    {
        m_descriptionController = m_descriptionContainer.GetComponentInChildren<GridDescriptionController>();
        m_videoPlayer = m_videoContainer.GetComponentInChildren<VideoPlayer>();
        m_imageController = m_imageContainer.GetComponentInChildren<GridImageController>();

        HideAll();
    }

    private void HideAll()
    {
        m_descriptionContainer.SetActive(false);
        m_videoContainer.SetActive(false);
        m_imageContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
	
	}

    public void SetTileData(GridTileData tileData)
    {
        m_tileData = tileData;

        HideAll();
        RefreshDescription(tileData);
        if(tileData.HasVideo)
        {
            RefreshVideo(tileData);
        }
        else if(tileData.HasImage)
        {
            RefreshImage(tileData);
        }
    }

    private void RefreshDescription(GridTileData tileData)
    {
        m_descriptionContainer.SetActive(true);
        m_descriptionController.SetTileData(tileData);
    }

    private void RefreshVideo(GridTileData tileData)
    {
        m_videoContainer.SetActive(true);
        m_videoPlayer.SetVideoPath(tileData.VideoPath);
    }

    private void RefreshImage(GridTileData tileData)
    {
        m_imageContainer.SetActive(true);
        m_imageController.SetImage(tileData.Image);
    }

    public void OnShow()
    {

    }

    public void OnHide()
    {
        m_videoPlayer.Stop();
    }
}
