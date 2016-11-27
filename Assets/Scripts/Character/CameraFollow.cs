using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public GameObject m_target;
    public float m_smoothTime = 0.15f;

    private Vector3 m_velocity = Vector3.zero;

	// Use this for initialization
	void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update()
    {
	    if(m_target)
        {
            Vector3 smoothPos = Vector3.SmoothDamp(transform.position, m_target.transform.position, ref m_velocity, m_smoothTime);
            transform.position = new Vector3(smoothPos.x, transform.position.y, transform.position.z);
        }
	}
}
