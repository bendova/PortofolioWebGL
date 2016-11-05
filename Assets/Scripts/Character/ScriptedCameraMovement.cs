using UnityEngine;
using System.Collections;

public class ScriptedCameraMovement : MonoBehaviour
{
    public GameObject m_mainCamera;
    public float m_scriptedCameraMoveSpeed = 5.0f;

    private Vector3 m_cameraInitialPosLocalSpace;

    private Vector3 m_startLocalSpace;
    private Vector3 m_targetLocalPos;
    private float m_moveStartTime;
    private float m_moveDistance;

    private bool m_isInScriptedMove = false;

    // Use this for initialization
    void Start ()
    {
        m_cameraInitialPosLocalSpace = m_mainCamera.transform.localPosition;
    }
	
	// Update is called once per frame
	void Update()
    {
        UpdateScriptedCameraTarget();
    }

    private void UpdateScriptedCameraTarget()
    {
        if(m_isInScriptedMove)
        {
            float distanceCovered = m_scriptedCameraMoveSpeed * (Time.time - m_moveStartTime);
            float factor = distanceCovered / m_moveDistance;
            m_mainCamera.transform.localPosition = Vector3.Lerp(m_startLocalSpace, m_targetLocalPos, factor);

            float distance = Vector3.Distance(m_mainCamera.transform.localPosition, m_targetLocalPos);
            if (distance <= Vector3.kEpsilon)
            {
                m_isInScriptedMove = false;
            }
        }
    }

    public void SetTarget(Vector3 m_target)
    {
        Vector3 targetLocalPos = m_cameraInitialPosLocalSpace;
        Vector3 localPos = m_mainCamera.transform.InverseTransformPoint(m_target);
        targetLocalPos.x = localPos.x;
        TweenToLocalPos(targetLocalPos);
    }

    public void ResetToInitialPos()
    {
        TweenToLocalPos(m_cameraInitialPosLocalSpace);
    }

    private void TweenToLocalPos(Vector3 targetLocalPos)
    {
        m_targetLocalPos = targetLocalPos;
        m_moveStartTime = Time.time;
        m_startLocalSpace = m_mainCamera.transform.localPosition;
        m_moveDistance = Vector3.Distance(m_startLocalSpace, m_targetLocalPos);
        m_isInScriptedMove = true;
    }
}
