using UnityEngine;
using System.Collections;
using System;

public class GridSubTileData : MonoBehaviour
{
    [Serializable]
    public enum GridContentType
    {
        Video,
        Image
    }

    [Header("Description")]
    [SerializeField]
    private Sprite m_descriptionImage;
    public Sprite DescriptionImage { get { return m_descriptionImage; } }

    [Header("Content")]
    [SerializeField]
    private GridContentType m_contentType;
    public bool HasVideo { get { return (m_contentType == GridContentType.Video); } }
    public bool HasImage { get { return (m_contentType == GridContentType.Image); } }

    [SerializeField]
    private string m_videoPath;
    public string VideoPath { get { return m_videoPath; } }

    [SerializeField]
    public Sprite m_image;
    public Sprite Image { get { return m_image; } }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
