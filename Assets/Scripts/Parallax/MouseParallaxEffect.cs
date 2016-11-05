using UnityEngine;
using System.Collections;

public class MouseParallaxEffect : MonoBehaviour
{
    public Transform m_pivot;
    public Camera m_camera;

    public float m_offsetFactor = 0.01f;
    private Vector3 m_offset = Vector3.zero;
    static private Vector3 m_scaledOffset = Vector3.zero;

    public static Vector3 Offset { get { return m_scaledOffset;  } }

    private Vector3 m_previousMousePos = Vector3.zero;

    // Use this for initialization
    void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update()
    {
        if(Vector3.SqrMagnitude(Input.mousePosition - m_previousMousePos) >= 0.1)
        {
            m_previousMousePos = Input.mousePosition;
            Vector3 mousePosition = m_camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 localMousePosition = m_pivot.InverseTransformVector(mousePosition);

            m_offset = localMousePosition - m_pivot.transform.position;
            m_scaledOffset = m_offset * m_offsetFactor;
            m_scaledOffset.z = 0.0f;
        }
    }
}
