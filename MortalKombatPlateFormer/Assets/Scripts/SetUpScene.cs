using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpScene : MonoBehaviour
{
    [SerializeField]
    private Transform m_Spawn1;
    public Transform Spawn1
    {
        get { return m_Spawn1; }
    }
    [SerializeField]
    private Transform m_Spawn2;
    public Transform Spawn2
    {
        get { return m_Spawn2; }
    }
    [SerializeField]
    private Transform m_Spawn3;
    public Transform Spawn3
    {
        get { return m_Spawn3; }
    }

    private GameObject m_Player;

    private void Start()
    {
        GameObject LiuKang = GameManager.Instance.LiuKangPrefab;
        GameObject Scorpion = GameManager.Instance.ScorpionPrefab;
        GameObject SubZero = GameManager.Instance.SubZeroPrefab;

        if (GameManager.Instance.PlayerName == "LiuKang")
        {
            m_Player = LiuKang;
        }
        else if (GameManager.Instance.PlayerName == "Scorpion")
        {
            m_Player = Scorpion;
        }
        else if (GameManager.Instance.PlayerName == "SubZero")
        {
            m_Player = SubZero;
        }

        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if (GameManager.Instance.ZoneReach == 0)
        {
            GameManager.Instance.SetPlayerGameObject(Instantiate(m_Player, m_Spawn1.position, Quaternion.identity));
        }
        else if (GameManager.Instance.ZoneReach == 1)
        {
            GameManager.Instance.SetPlayerGameObject(Instantiate(m_Player, m_Spawn2.transform.position, Quaternion.identity));
        }
        else if (GameManager.Instance.ZoneReach == 2)
        {
            GameManager.Instance.SetPlayerGameObject(Instantiate(m_Player, m_Spawn3.transform.position, Quaternion.identity));
        }
    }
}
