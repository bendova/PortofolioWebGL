using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FpsCounter : MonoBehaviour
{
    public float updateInterval = 0.5f;

    private float accum = 0.0f; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeleft = 0.0f; // Left time for current interval

    private Text m_text;

	// Use this for initialization
	void Start ()
    {
        timeleft = updateInterval;
        m_text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            m_text.text = "FPS: " + (accum / frames).ToString("f2");
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }
}
