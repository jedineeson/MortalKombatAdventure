using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform m_ToFollow;
    [SerializeField]
    private Transform m_BossCamPos;
    [SerializeField]
    private float m_FollowSpeed = 3f;
    //[SerializeField]
    //private float m_Offset = -2f;
    private Vector3 CamStartPos;
    private Vector3 m_MovePos = new Vector3();
    private bool m_CamFixed = false;

    private void Start()
    {
        CamStartPos = new Vector3(0, 0, -10);
        m_MovePos = transform.position;
    }

    private void LateUpdate()
    {
        if (!m_CamFixed)
        {
            if (GameManager.Instance.Player != null)
            {
                m_ToFollow = GameManager.Instance.Player.transform;

                if (GameManager.Instance.Player.GetComponent<PlayerController>().Visual.flipX == true)
                {
                    m_MovePos.x = Mathf.Lerp(transform.position.x, m_ToFollow.position.x - 3f, m_FollowSpeed * Time.deltaTime);
                }
                else
                {
                    m_MovePos.x = Mathf.Lerp(transform.position.x, m_ToFollow.position.x + 3f, m_FollowSpeed * Time.deltaTime);
                }
                /*
                quand mon perso bouge vers la droite, CamPos = Perso + 3
                */
                m_MovePos.z = -10f;

                //m_MovePos.x = Mathf.Lerp(transform.position.x, m_ToFollow.position.x - m_Offset, m_FollowSpeed * Time.deltaTime);
                transform.position = m_MovePos;
            }

            if (transform.position.x <= 0)
            {
                transform.position = CamStartPos;
            }
        }
        else
        {
            transform.position = m_BossCamPos.position;            
        }
    }

    private void OnTriggerEnter2d(Collider2D aOther)
    {
        if(aOther.tag == "Player")
        {
            Debug.Log("ENTER BOSS ZONE");
            m_CamFixed = true;
        }
    }
}

