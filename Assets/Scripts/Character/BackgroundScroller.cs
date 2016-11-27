using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeedX = 1.0f;
    public float backgroundLengthX = 1.0f;

    public float scrollSpeedY = 1.0f;
    public float backgroundLengthY = 1.0f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newX = Mathf.PingPong(Time.time * scrollSpeedX, backgroundLengthX);
        float newY = Mathf.PingPong(Time.time * scrollSpeedY, backgroundLengthY);
        transform.position = startPosition + (Vector3.right * newX) + (Vector3.up * newY);
        //transform.localPosition = new Vector3(Mathf.Round(transform.localPosition.x), transform.localPosition.y, transform.localPosition.z);
    }
}
