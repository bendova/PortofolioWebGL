using UnityEngine;
using System.Collections;
using System;

public class CharController : MonoBehaviour
{
	public float m_maxSpeed = 10f;
    public BoxCollider2D m_worldBounds;

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
    private float m_maxAngleForInView = 80.0f;

    void Start()
    {
        PlayerDirection = FacingDirection.Forward;
        m_animator = GetComponent<Animator>();
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_renderer = GetComponent<SpriteRenderer>();
    }

	void FixedUpdate () 
	{
		float moveH = Input.GetAxis("Horizontal");
        MoveChar(moveH);
        UpdatePlayerDirection(moveH);
        UpdateLookUp();
        UpdateRendererOrientation();
    }

    private void MoveChar(float moveH)
    {
        float speedX = ClampMoveToWorldBounds(moveH);
        m_animator.SetBool("IsMoving", (speedX != 0.0f));
        m_rigidBody.velocity = new Vector2(speedX, 0);
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
            FacingDirection lookUpDirection = GetLookUpDirection();
            m_renderer.flipX = (lookUpDirection == FacingDirection.Left);
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
            FacingDirection lookupDirection = GetLookUpDirection();
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

    private FacingDirection GetLookUpDirection()
    {
        FacingDirection direction = FacingDirection.Forward;
        if (m_lookupTarget)
        {
            Vector3 directionToTarget = m_lookupTarget.transform.position - transform.position;
            float angle = Vector3.Angle(directionToTarget, Vector3.right);

            if (angle < 90.0f)
            {
                direction = FacingDirection.Right;
            }
            else if (angle > 90.0f)
            {
                direction = FacingDirection.Left;
            }
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
}
