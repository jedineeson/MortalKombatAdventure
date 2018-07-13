using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 5f;
    [SerializeField]
    private Animator m_Animator;
    [SerializeField]
    private Rigidbody2D m_Player;
    [SerializeField]
	private float m_JumpForce = 650f;
	[SerializeField]
	private float m_FakeGravity = 20f;
    private bool m_IsGrounded = true;
    private bool m_CanMove = true;
    private float m_InputX;
    private Vector2 m_MoveDir = new Vector2();
    private SpriteRenderer m_Visual;

    public SpriteRenderer Visual
    {
        get { return m_Visual; }
    }

    private void Awake()
    {
        m_Visual = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (m_CanMove)
        {
            m_InputX = Input.GetAxisRaw("Horizontal");
            if (m_InputX > 0f)
            {
                m_MoveDir = transform.right;
                m_Visual.flipX = false;
                m_Animator.SetBool("Walk", true);
            }
            else if (m_InputX < 0f)
            {
                m_MoveDir = -transform.right;
                m_Visual.flipX = true;
                m_Animator.SetBool("Walk", true);
            }
            else
            {
                m_MoveDir = Vector2.zero;
                m_Animator.SetBool("Walk", false);
            }
        }
        //INPUT MANAGER
        if (Input.GetKeyDown(KeyCode.K))
        {
            m_MoveDir = Vector2.zero;
            m_Animator.SetTrigger("Kick");
            m_CanMove = false;
            m_Player.velocity = Vector2.zero;
        }
        //INPUT MANAGER
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
	{
        m_MoveDir *= m_Speed;
        m_MoveDir.y = m_Player.velocity.y;

        if(m_Player.velocity.y != 0f)
		{
			m_MoveDir.y -= m_FakeGravity * Time.fixedDeltaTime;
            //m_Animator.SetBool("Jump", true);
		}
        else
        {
            //m_Animator.SetBool("Jump", false);
        }

        m_Player.velocity = m_MoveDir;
    }

    private void Jump()
	{
		if(m_IsGrounded)
		{
            m_Animator.SetTrigger("Jump");                
			m_IsGrounded = false;
			m_Player.velocity = new Vector2(m_Player.velocity.x, 0f);
			m_Player.AddForce(transform.up * m_JumpForce);
		}
	}

    public void SetCanMove()
    {
        m_CanMove = true;
    }

    private void OnTriggerEnter2D(Collider2D aOther)
    {
        if(aOther.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            m_IsGrounded = true;
        }
    }
}
