using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject m_WinScreen;
    [SerializeField]
    private GameObject m_LoseScreen;
    [SerializeField]
    private TextMeshProUGUI m_MainText;
    [SerializeField]
    private TextMeshProUGUI m_SubText;

    private void Start()
    {
        if (GameManager.Instance.Win)
        {
            m_WinScreen.SetActive(true);
            m_LoseScreen.SetActive(false);
            m_SubText.gameObject.SetActive(false);
            m_MainText.text = "YOU WIN!";
        }
        else
        {
            if (GameManager.Instance.LifeRemains > 0)
            {
                m_WinScreen.SetActive(false);
                m_LoseScreen.SetActive(true);
                m_SubText.gameObject.SetActive(true);
                m_MainText.text = "YOU DIED!";

                switch (GameManager.Instance.LifeRemains)
                {
                    case 1:
                        m_SubText.text = "1 LIFES REMAINING";
                        break;
                    case 2:
                        m_SubText.text = "2 LIFES REMAINING";
                        break;
						default:
						m_SubText.text = "A GAME BY JimGV";
						break;
                }
            }
            else
            {
                m_MainText.text = "GAME OVER!";
                GameManager.Instance.SetLife(3);
                GameManager.Instance.SetZoneReach(0);
                m_WinScreen.SetActive(false);
                m_LoseScreen.SetActive(true);
                m_SubText.gameObject.SetActive(false);
            }
        }
    }

	public void Quit()
	{
		LevelManager.Instance.ChangeLevel("Menu");
	}

	public void Replay()
	{
		LevelManager.Instance.ChangeLevel("Game");
	}

}
