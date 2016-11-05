using UnityEngine;
using System.Collections;
using System;

public class CharController : MonoBehaviour
{
	public float m_maxSpeed = 10f;
    public BoxCollider2D m_worldBounds;
    public bool m_enableInputHandling = true;
    public bool m_scriptedMoveLeft = false;
    public bool m_scriptedMoveRight = false;

    public FacingDirection PlayerDirection
    {
        get;
        private set;
    }

    public enum FacingDirection
    {
        Left,
        Right,
        Forward
    };

    private Animator m_animator;
    private Rigidbody2D m_rigidBody;
    private SpriteRenderer m_renderer;
    private GameObject m_lookupTarget;
    private Collider2D m_playerCollider;
    private float m_maxAngleForInView = 80.0f;

    private ScriptedCameraMovement m_scriptedCameraMovement;
    private bool m_isInScriptedMove = false;
    private Vector3 m_scriptedPlayerMoveTarget;
    private Vector3 m_scriptedCameraMoveTarget;

    void Start()
    {
        PlayerDirection = FacingDirection.Forward;
        m_animator = GetComponent<Animator>();
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_renderer = GetComponent<SpriteRenderer>();
        m_playerCollider = GetComponent<Collider2D>();
        m_scriptedCameraMovement = GetComponent<ScriptedCameraMovement>();
    }

	void FixedUpdate() 
	{
        UpdateScriptedMoveTarget();

        float moveH = CalculateMoveH();
        MoveChar(moveH);
        UpdatePlayerDirection(moveH);
        UpdateLookUp();
        UpdateRendererOrientation();
    }

    private void UpdateScriptedMoveTarget()
    {
        if(m_isInScriptedMove)
        {
            m_scriptedMoveLeft = false;
            m_scriptedMoveRight = false;

            if (HasReachedScriptedMoveTarget())
            {
                // we have reached the target
                m_isInScriptedMove = false;
                OnScriptedMoveTargetReached();
            }
            else
            {
                FacingDirection dir = GetLookUpDirection(m_scriptedPlayerMoveTarget);
                if(dir == FacingDirection.Left)
                {
                    m_scriptedMoveLeft = true;
                }
                else if(dir == FacingDirection.Right)
                {
                    m_scriptedMoveRight = true;
                }
            }
        }
    }

    private bool HasReachedScriptedMoveTarget()
    {
        bool hasReached = false;
        Vector3 currentPos = new Vector3(transform.position.x, 0.0f, 0.0f);
        Vector3 targetPos = new Vector3(m_scriptedPlayerMoveTarget.x, 0.0f, 0.0f);
        float distance = Vector3.Distance(currentPos, targetPos);
        const float epsilon = 0.01f;
        if (distance <= epsilon)
        {
            hasReached = true;
        }
        return hasReached;
    }

    private void OnScriptedMoveTargetReached()
    {
        m_scriptedCameraMovement.SetTarget(m_scriptedCameraMoveTarget);
    }

    private float CalculateMoveH()
    {
        float moveH = 0.0f;
        if (m_enableInputHandling)
        {
            moveH = Input.GetAxis("Horizontal");
        }
        else if(m_scriptedMoveLeft)
        {
            moveH = -1.0f;
        }
        else if(m_scriptedMoveRight)
        {
            moveH = 1.0f;
        }
        return moveH;
    }

    private void MoveChar(float moveH)
    {
        float speedX = 0.0f;
        if(m_isInScriptedMove)
        {
            speedX = ClampMoveToScriptedTarget(moveH);
        }
        else
        {
            speedX = ClampMoveToWorldBounds(moveH);
        }
        m_animator.SetBool("IsMoving", (speedX != 0.0f));
        m_rigidBody.velocity = new Vector2(speedX, 0);
    }

    private float ClampMoveToScriptedTarget(float moveH)
    {
        float speedX = m_maxSpeed * moveH;
        if (speedX != 0.0f)
        {
            float currentX = m_rigidBody.position.x;
            float deltaX = speedX * Time.deltaTime;
            float newX = currentX + deltaX;
            if ((moveH < 0.0f && newX < m_scriptedPlayerMoveTarget.x) ||
                (moveH > 0.0f && newX > m_scriptedPlayerMoveTarget.x))
            {
                float clampedX = m_scriptedPlayerMoveTarget.x;
                float clampedSpeedX = (clampedX - currentX) / Time.deltaTime;
                speedX = clampedSpeedX;
            }
        }
        return speedX;
    }

    private float ClampMoveToWorldBounds(float moveH)
    {
        float speedX = m_maxSpeed * moveH;
        if (m_worldBounds && (speedX != 0.0f))
        {
            float currentX = m_rigidBody.position.x;
            float deltaX = speedX * Time.deltaTime;
            float newX = currentX + deltaX;
            float clampedX = Mathf.Clamp(newX, m_worldBounds.bounds.min.x, m_worldBounds.bounds.max.x);
            float clampedSpeedX = (clampedX - currentX) / Time.deltaTime;
            speedX = clampedSpeedX;
        }
        return speedX;
    }

    private void UpdatePlayerDirection(float moveH)
    {
        if (moveH == 0.0f)
        {
            PlayerDirection = FacingDirection.Forward;
        }
        else
        {
            bool isMovingRight = moveH > 0.0f;
            PlayerDirection = isMovingRight ? FacingDirection.Right : FacingDirection.Left;
        }
    }

    private void UpdateRendererOrientation()
    {
        if(PlayerDirection == FacingDirection.Left)
        {
            m_renderer.flipX = true;
        }
        else if (PlayerDirection == FacingDirection.Forward)
        {
            if(m_lookupTarget)
            {
                FacingDirection lookUpDirection = GetLookUpDirection(m_lookupTarget.transform.position);
                m_renderer.flipX = (lookUpDirection == FacingDirection.Left);
            }
        }
        else
        {
            m_renderer.flipX = false;
        }
    }

    public void SetLookUpTarget(GameObject lookupTarget)
    {
        m_lookupTarget = lookupTarget;
    }

    private void UpdateLookUp()
    {
        if (m_lookupTarget)
        {
            m_animator.SetBool("Lookup", IsLookUpTargetInView());
        }
        else
        {
            m_animator.SetBool("Lookup", false);
        }
    }

    private bool IsLookUpTargetInView()
    {
        bool isInView = false;
        if(PlayerDirection == FacingDirection.Forward)
        {
            FacingDirection lookupDirection = GetLookUpDirection(m_lookupTarget.transform.position);
            if (lookupDirection == FacingDirection.Forward)
            {
                isInView = false;
            }
            else
            {
                isInView = IsLookUpTargetInView(lookupDirection);
            }
        }
        else
        {
            isInView = IsLookUpTargetInView(PlayerDirection);
        }
        
        return isInView;
    }

    private FacingDirection GetLookUpDirection(Vector3 targetPos)
    {
        FacingDirection direction = FacingDirection.Forward;
        Vector3 directionToTarget = targetPos - transform.position;
        float angle = Vector3.Angle(directionToTarget, Vector3.right);

        if (angle < 90.0f)
        {
            direction = FacingDirection.Right;
        }
        else if (angle > 90.0f)
        {
            direction = FacingDirection.Left;
        }
        return direction;
    }

    private bool IsLookUpTargetInView(FacingDirection direction)
    {
        Vector3 directionToTarget = m_lookupTarget.transform.position - transform.position;
        Vector3 forwardDirection = (direction == FacingDirection.Right) ? Vector3.right : -Vector3.right;
        float angle = Vector3.Angle(directionToTarget, forwardDirection);
        bool isInView = (angle < m_maxAngleForInView);
        return isInView;
    }

    public void ScriptedMoveToPos(Transform playerTarget, Transform cameraTarget)
    {
        m_isInScriptedMove = true;
        m_enableInputHandling = false;

        m_scriptedPlayerMoveTarget = playerTarget.position;
        m_scriptedPlayerMoveTarget.y = m_playerCollider.transform.position.y;
        m_scriptedPlayerMoveTarget.z = m_playerCollider.transform.position.z;

        m_scriptedCameraMoveTarget = cameraTarget.position;
    }

    public void StopScripteMoveToPos()
    {
        m_isInScriptedMove = false;
        m_scriptedMoveLeft = false;
        m_scriptedMoveRight = false;
        m_enableInputHandling = true;
        m_scriptedCameraMovement.ResetToInitialPos();
    }
}
