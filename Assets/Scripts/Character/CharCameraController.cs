using UnityEngine;
using System.Collections;
using System;

public class CharCameraController : MonoBehaviour
{
    public GameObject m_mainCamera;
    public float m_scriptedCameraMoveSpeed = 5.0f;
    public BoxCollider2D m_worldBounds;

    private Camera m_cameraComponent;
    private Vector3 m_cameraInitialPosLocalSpace;

    private Vector3 m_startLocalSpace;
    private Vector3 m_targetLocalSpace;
    private float m_moveStartTime;
    private float m_moveDistance;
    
    private CharController.OnScriptedTargetReached m_onScriptedTargetReachedCb;

    private bool m_isInScriptedMove = false;

    private bool m_keepCameraClamped = false;
    private Vector3 m_cameraClampPosition;

    class CameraBounds
    {
        public bool m_isInsideLeftBound = false;
        public bool m_isInsideRightBound = false;

        public bool IsInsideBounds()
        {
            return m_isInsideLeftBound && m_isInsideRightBound;
        }
    }

    // Use this for initialization
    void Start ()
    {
        m_cameraComponent = m_mainCamera.GetComponent<Camera>();
        m_cameraInitialPosLocalSpace = m_mainCamera.transform.localPosition;
    }
	
	// Update is called once per frame
	void Update()
    {
        UpdateScriptedCameraTarget();
        ClampCameraToWorldBounds();
    }

    private void UpdateScriptedCameraTarget()
    {
        if(m_isInScriptedMove)
        {
            float distanceCovered = m_scriptedCameraMoveSpeed * (Time.time - m_moveStartTime);
            float factor = distanceCovered / m_moveDistance;
            m_mainCamera.transform.localPosition = Vector3.Lerp(m_startLocalSpace, m_targetLocalSpace, factor);

            float distance = Vector3.Distance(m_mainCamera.transform.localPosition, m_targetLocalSpace);
            if (distance <= Vector3.kEpsilon)
            {
                m_isInScriptedMove = false;
                if(m_onScriptedTargetReachedCb != null)
                {
                    m_onScriptedTargetReachedCb.Invoke();
                    m_onScriptedTargetReachedCb = null;
                }
            }
        }
    }

    public void SetTarget(Vector3 target, CharController.OnScriptedTargetReached cb)
    {
        Vector3 targetLocalPos = m_cameraInitialPosLocalSpace;
        Vector3 localPos = m_mainCamera.transform.parent.InverseTransformPoint(target);
        targetLocalPos.x = localPos.x;
        TweenToLocalPos(targetLocalPos);
        m_onScriptedTargetReachedCb = cb;
    }

    public void ResetToInitialPos()
    {
        TweenToLocalPos(m_cameraInitialPosLocalSpace);
    }

    private void TweenToLocalPos(Vector3 targetLocalPos)
    {
        m_moveStartTime = Time.time;
        m_startLocalSpace = m_mainCamera.transform.localPosition;
        m_targetLocalSpace = targetLocalPos;
        m_moveDistance = Vector3.Distance(m_startLocalSpace, m_targetLocalSpace);
        m_isInScriptedMove = true;
    }

    private void ClampCameraToWorldBounds()
    {
        if (m_worldBounds)
        {
            CameraBounds cameraBounds = CheckCurrentCameraBoundsAt(m_mainCamera.transform.position);
            if (!cameraBounds.m_isInsideLeftBound)
            {
                float clampedX = m_worldBounds.bounds.min.x + GetCameraOrthoSizeX();
                ClampCameraToX(clampedX);
            }
            else if (!cameraBounds.m_isInsideRightBound)
            {
                float clampedX = m_worldBounds.bounds.max.x - GetCameraOrthoSizeX();
                ClampCameraToX(clampedX);
            }
            else if (m_keepCameraClamped && IsCameraInsideBoundsAt(m_cameraInitialPosLocalSpace))
            {
                m_keepCameraClamped = false;
                ResetToInitialPos();
            }
            else if (m_keepCameraClamped)
            {
                m_mainCamera.transform.position = m_cameraClampPosition;
            }
        }
    }

    private bool IsCameraInsideBoundsAt(Vector3 posInLocalSpace)
    {
        Vector3 posInGlobalSpace = m_mainCamera.transform.parent.TransformPoint(posInLocalSpace);
        CameraBounds bounds = CheckCurrentCameraBoundsAt(posInGlobalSpace);
        return bounds.IsInsideBounds();
    }

    private CameraBounds CheckCurrentCameraBoundsAt(Vector3 posInGlobalSpace)
    {
        CameraBounds bounds = new CameraBounds();

        float orthoSizeX = GetCameraOrthoSizeX();
        float leftBound = posInGlobalSpace.x - orthoSizeX;
        float rightBound = posInGlobalSpace.x + orthoSizeX;
        if (leftBound >= m_worldBounds.bounds.min.x)
        {
            bounds.m_isInsideLeftBound = true;
        }
        if (rightBound <= m_worldBounds.bounds.max.x)
        {
            bounds.m_isInsideRightBound = true;
        }

        return bounds;
    }

    private void ClampCameraToX(float clampedX)
    {
        m_cameraClampPosition = new Vector3(clampedX, m_mainCamera.transform.position.y, m_mainCamera.transform.position.z);
        m_mainCamera.transform.position = m_cameraClampPosition;
        m_keepCameraClamped = true;
    }

    private float GetCameraOrthoSizeX()
    {
        float orthoSizeX = m_cameraComponent.orthographicSize * Screen.width / Screen.height;
        return orthoSizeX;
    }
}
