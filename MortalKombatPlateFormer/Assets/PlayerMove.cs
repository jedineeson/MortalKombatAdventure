using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 5f;
    [SerializeField]
    private Animator m_Animator;
    [SerializeField]
    private Rigidbody2D m_Player;

	private Vector2 m_MoveDir = new Vector2();
    private SpriteRenderer m_Visual;
    private float m_InputX;

    private void Awake()
    {
        m_Visual = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {        
        m_InputX = Input.GetAxisRaw("Horizontal");
        if (m_InputX > 0f)
        {
            m_MoveDir = transform.right;
			m_Visual.flipX = false;
            m_Animator.SetBool("Walk", true);
        }
		else if(m_InputX < 0f)
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

    private void FixedUpdate()
	{
		m_MoveDir *= m_Speed;
		m_MoveDir.y = m_Player.velocity.y;
		m_Player.velocity = m_MoveDir;
	}
}
