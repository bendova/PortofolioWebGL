using UnityEngine;
using System.Collections;

public class GridSubTileData : MonoBehaviour
{
    public string m_description;
    public string Description { get { return m_description; } }

    public bool m_hasVideo;
    public bool HasVideo { get { return m_hasVideo; } }

    public string m_videoPath;
    public string VideoPath { get { return m_videoPath; } }

    public bool m_hasImage;
    public bool HasImage { get { return m_hasImage; } }

    public Sprite m_image;
    public Sprite Image { get { return m_image; } }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
