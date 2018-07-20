using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private Rigidbody2D m_Rigidbody;
    [SerializeField]
    private SpriteRenderer m_Visual;
    [SerializeField]
    private Animator m_Animator;
    private bool m_CanMoveLeft = true;
    private bool m_CanMoveRight;
    private Vector2 m_MoveDir = new Vector2();
    private bool m_IsVulnerable = false;
    private int m_Hp = 5;
    public bool IsVulnerable
    {
        get { return m_IsVulnerable; }
    }

    private void Update()
    {
        if(gameObject.transform.position.x <= 105.5f && m_CanMoveLeft)
        {
            m_Visual.flipX = false;
            m_MoveDir = Vector2.zero;
            m_Animator.SetBool("Turn", false);
            m_IsVulnerable = true;
            m_Animator.SetTrigger("Pause");
            m_CanMoveLeft = false;
        }
        else if(gameObject.transform.position.x >= 118.5f && m_CanMoveRight)
        {
            m_Visual.flipX = true;
            m_MoveDir = Vector2.zero;
            m_IsVulnerable = true;
            m_Animator.SetTrigger("Pause");
            m_CanMoveRight = false;
        }
        else if (gameObject.transform.position.x >= 105.5f && m_CanMoveLeft)
        {
            m_MoveDir = -transform.right;
        }
        else if (gameObject.transform.position.x <= 118.5f && m_CanMoveRight)
        {
            m_MoveDir = transform.right;
        }
        else
        {
            m_MoveDir = Vector2.zero;
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

    public void SetCanMove()
    {
        if (gameObject.transform.position.x <= 105.5f)
        {
            m_IsVulnerable = false;
            m_CanMoveRight = true;
            m_Animator.SetTrigger("Punch");
        }
        else if (gameObject.transform.position.x >= 118.5f)
        {
            m_IsVulnerable = false;
            m_CanMoveLeft = true;
            m_Animator.SetBool("Turn", true);
        }
    }

    public void LoseLife()
    {
        m_Hp -= 1;
        Debug.Log("GORO HP: " + m_Hp);
    }

    private void OnTriggerEnter2D(Collider2D aOther)
    {
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Player") && !m_IsVulnerable)
        {
            GameManager.Instance.Player.GetComponent<PlayerController>().OnUpdateHp(1);
        }
    }
}
