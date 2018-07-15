using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    private Action<int> m_DamageEnnemy;
	[SerializeField]
    private float DelayBetweenAttack;    
	[SerializeField]
	private float m_Speed;
	[SerializeField] 
	private Rigidbody2D m_Rigidbody;
	private GameObject m_Player;
	private SpriteRenderer m_Visual;
	private Vector2 m_MoveDir = new Vector2();
	private Animator m_Animator;
    private bool m_IsAttack = false;
    private Vector2 m_RayPos;
    private RaycastHit2D m_Hit;

    private void Start()
    {
        m_Visual = gameObject.GetComponent<SpriteRenderer>();
        m_Animator = gameObject.GetComponent<Animator>();
        if(GameManager.Instance.Player != null)
        {
            m_Player = GameManager.Instance.Player;
            m_DamageEnnemy += m_Player.GetComponent<PlayerController>().OnUpdateHp;
        }
    }

    private void Update()
    {
        if (m_Rigidbody == null)
        {
            Destroy(this);
        }
        else if (m_Player != null)
        {
            if (DelayBetweenAttack > 0f)
            {
                DelayBetweenAttack -= Time.deltaTime;
            }
            if (DelayBetweenAttack <= 0f)
            {
                m_IsAttack = false;
            }

            if (transform.position.x < m_Player.transform.position.x - 1.5f)
            {
                m_Visual.flipX = false;
                m_MoveDir = transform.right;
                m_Animator.SetBool("Walk", true);
            }
            else if (transform.position.x > m_Player.transform.position.x + 1.5f)
            {
                m_Visual.flipX = true;
                m_MoveDir = -transform.right;
                m_Animator.SetBool("Walk", true);
            }
            else if (m_IsAttack == false)
            {
                m_MoveDir = Vector2.zero;
                m_Animator.SetBool("Walk", false);
                m_Animator.SetTrigger("Attack");
                m_IsAttack = true;
                DelayBetweenAttack += 2f;

                Attack();
            }
            else
            {
                m_MoveDir = Vector2.zero;
            }
        }
    }
    	 
	private void FixedUpdate()
	{
        if (m_Rigidbody != null)
        {
            m_MoveDir *= m_Speed;
            m_MoveDir.y = m_Rigidbody.velocity.y;
            m_Rigidbody.velocity = m_MoveDir;
        }
    }

    private void Attack()
    {
        m_RayPos.x = transform.position.x;
        m_RayPos.y = transform.position.y;
        if (transform.position.x > m_Player.transform.position.x)
        {
            m_Hit = Physics2D.Raycast(m_RayPos, -transform.right, 2f, LayerMask.GetMask("Player"));
        }
        else if (transform.position.x < m_Player.transform.position.x)
        {
            m_Hit = Physics2D.Raycast(m_RayPos, transform.right, 2f, LayerMask.GetMask("Player"));
        }
        if (m_Hit.collider != null)
        {
            m_DamageEnnemy(1);
        }
    }

    public void Die()
    {
        m_Animator.SetTrigger("Die");
        StartCoroutine(DestroyMyself());
    }

    private IEnumerator DestroyMyself()
    {
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
