using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	[HideInInspector]
	public string m_Player;
	
	private static GameManager m_Instance;
	//variable de moi même(on peux y accéder de partout avec LevelManager.Instance)
	public static GameManager Instance
	{
		//on veux jamais le re-set		
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

	public void SetPlayerCharacter(string aName)
	{
        	m_Player = aName;
	}
}

