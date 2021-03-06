﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_LiuKangPrefab;
    public GameObject LiuKangPrefab
    {
        get { return m_LiuKangPrefab; }
    }
    [SerializeField]
    private GameObject m_ScorpionPrefab;
    public GameObject ScorpionPrefab
    {
        get { return m_ScorpionPrefab; }
    }
    [SerializeField]
    private GameObject m_SubZeroPrefab;
    public GameObject SubZeroPrefab
    {
        get { return m_SubZeroPrefab; }
    }

    private int m_LifeRemains = 3;
    public int LifeRemains
    {
        get { return m_LifeRemains; }
    }
    
    private int m_ZoneReach = 0;
    public int ZoneReach
    {
        get { return m_ZoneReach; }
    }

    private bool m_IsGameOver = false;
    public bool IsGameOver
    {
        get { return m_IsGameOver; }
    }

    private bool m_Win = false;
    public bool Win
    {
        get { return m_Win; }
    }

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

    private bool m_CheckPointReach = false;
    public bool CheckPointReach
    {
        get { return m_CheckPointReach; }
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

    public void SetZoneReach(int zone)
    {
        m_ZoneReach = zone;
        StartCoroutine(ShowCheckPointCoroutine());
    }

    public void SetLife(int damage)
    {
        m_LifeRemains += damage;
    }

    private IEnumerator ShowCheckPointCoroutine()
    {
        m_CheckPointReach = true;
        yield return new WaitForSeconds(3f);
        m_CheckPointReach = false;
    }

}

