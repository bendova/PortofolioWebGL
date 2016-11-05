using UnityEngine;
using System.Collections;

public class MouseParallaxLayer : MonoBehaviour
{
    public float m_speedX;
    public float m_speedY;
    public bool m_moveInOppositeDirection;

    private Vector3 m_initialPosition;

    // Use this for initialization
    void Start ()
    {
        m_initialPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update()
    {
        float direction = (m_moveInOppositeDirection) ? -1f : 1f;
        Vector3 distance = MouseParallaxEffect.Offset * direction;
        distance.x *= m_speedX;
        distance.y *= m_speedY;
        transform.position = m_initialPosition + distance;
    }
}
