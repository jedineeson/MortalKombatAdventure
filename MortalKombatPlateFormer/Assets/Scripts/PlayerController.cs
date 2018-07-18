using System;
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
    [SerializeField]
    private int m_Hp = 10;
    private bool m_IsGrounded = true;
    private bool m_CanMove = true;
    private float m_InputX;
    private bool m_IsDead = false;

    private Vector2 m_MoveDir = new Vector2();
    private SpriteRenderer m_Visual;
    private Vector2 m_RayPos;
    private RaycastHit2D m_Hit;

    public int Hp
    {
        get { return m_Hp; }
    }

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
        if (m_CanMove && !m_IsDead)
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
        if (Input.GetKeyDown(KeyCode.K) && !m_IsDead)
        {
            m_MoveDir = Vector2.zero;
            m_Animator.SetTrigger("Kick");
            m_CanMove = false;
            m_Player.velocity = Vector2.zero;
            StartCoroutine(KickResult());

        }
        //INPUT MANAGER
        if (Input.GetKeyDown(KeyCode.Space) && !m_IsDead)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        m_MoveDir *= m_Speed;
        m_MoveDir.y = m_Player.velocity.y;

        if (m_Player.velocity.y != 0f)
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
        if (m_IsGrounded && !m_IsDead)
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
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Floor") || aOther.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            m_IsGrounded = true;
        }
    }

    private IEnumerator KickResult()
    {
        yield return new WaitForSeconds(1f);
        m_RayPos.x = transform.position.x;
        m_RayPos.y = transform.position.y;
        if (m_Visual.flipX == false)
        {
            m_Hit = Physics2D.Raycast(m_RayPos, transform.right, 2f, LayerMask.GetMask("Enemy"));
        }
        else
        {
            m_Hit = Physics2D.Raycast(m_RayPos, -transform.right, 2f, LayerMask.GetMask("Enemy"));
        }

        if (m_Hit.collider != null)
        {
            if(m_Hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                m_Hit.collider.gameObject.GetComponent<Ennemy>().Die();
            }
            else if(m_Hit.collider.gameObject.layer == LayerMask.NameToLayer("Goro") && !m_Hit.collider.gameObject.GetComponent<Boss>().IsVulnerable)
            {
                m_Hit.collider.gameObject.GetComponent<Boss>().LoseLife();
            }
        }
    }

    public void OnUpdateHp(int damage = 1)
    {
        m_Hp -= damage;
        if (m_Hp <= 0 && !m_IsDead)
        {
            m_Animator.SetTrigger("Die");
            StartCoroutine(DestroyMyself());
            m_IsDead = true;
        }
        else
        {
            m_Animator.SetTrigger("GotHit");
        }
    }

    private IEnumerator DestroyMyself()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.SetLife(-1);
        LevelManager.Instance.ChangeLevel("End");
        //Destroy(gameObject);
    }
}
