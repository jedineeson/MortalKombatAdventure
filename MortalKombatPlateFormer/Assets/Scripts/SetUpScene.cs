using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpScene : MonoBehaviour
{
    [SerializeField]
    private GameObject m_LiuKangPrefab;
    [SerializeField]
    private GameObject m_ScorpionPrefab;
    [SerializeField]
    private GameObject m_SubZeroPrefab;

    //[SerializeField]
    //private Vector3 m_SpawnPos;

    private void Start ()
    {
        if(GameManager.Instance.PlayerName == "LiuKang")
        {
            GameManager.Instance.SetPlayerGameObject(Instantiate(m_LiuKangPrefab, transform.position, Quaternion.identity));
        }
        else if (GameManager.Instance.PlayerName == "Scorpion")
        {
            GameManager.Instance.SetPlayerGameObject(Instantiate(m_ScorpionPrefab, transform.position, Quaternion.identity));
        }
        else if (GameManager.Instance.PlayerName == "SubZero")
        {
            GameManager.Instance.SetPlayerGameObject(Instantiate(m_SubZeroPrefab, transform.position, Quaternion.identity));
        }
    }

}
