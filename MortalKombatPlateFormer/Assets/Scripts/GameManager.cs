using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    private GameObject m_Player;
    public GameObject Player
    {
        get { return m_Player; }
    }

    private string m_PlayerName;
    public string PlayerName
    {
        get { return m_PlayerName; }
    }

    private static GameManager m_Instance;
	public static GameManager Instance
	{
		get { return m_Instance; }
	}

	private void Awake()
	{

		if(m_Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			m_Instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void SetPlayerName(string aName)
	{
        	m_PlayerName = aName;
	}

    public void SetPlayerGameObject(GameObject go)
    {
        m_Player = go;
    }
}

