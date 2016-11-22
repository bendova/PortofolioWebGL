using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class OpenCvPdf : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern void FileOpenPdf (string url);

#else
    private static void FileOpenPdf(string url)
    {
        Debug.Log("File open is not supported in Editor. Path: " + url);
    }
#endif

    public string m_CVFileName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OpenCvPdfFile()
    {
        string filePath = "StreamingAssets/" + m_CVFileName;
        FileOpenPdf(filePath);
    }
}
