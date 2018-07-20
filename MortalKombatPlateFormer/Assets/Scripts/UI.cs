using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private Image m_LifeBar;
    [SerializeField]
    private GameObject m_CheckPointText;
    private float m_Life;
    private float m_LifeMax = 20f;

    private void Update()
    {
        if (GameManager.Instance.Player != null)
        {
            m_Life = GameManager.Instance.Player.GetComponent<PlayerController>().Hp;
        }
        if(GameManager.Instance.CheckPointReach)
        {
            m_CheckPointText.SetActive(true);
        }
        else
        {
            m_CheckPointText.SetActive(false);
        }
        m_LifeBar.fillAmount = m_Life / m_LifeMax;
    }
}
