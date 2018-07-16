using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private Image m_LifeBar;
    private float m_Life;
    private float m_LifeMax = 10f;

    private void Update()
    {
        if (GameManager.Instance.Player != null)
        {
            m_Life = GameManager.Instance.Player.GetComponent<PlayerController>().Hp;
        }

        m_LifeBar.fillAmount = m_Life / m_LifeMax;
    }
}
