using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    private int m_Zone;

    private void OnTriggerEnter2D(Collider2D aOther)
    {
        if(aOther.tag == "Player")
        {
            GameManager.Instance.SetZoneReach(m_Zone);
        }
    }
}
