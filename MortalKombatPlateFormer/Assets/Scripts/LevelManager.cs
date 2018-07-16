using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager m_Instance;
    public static LevelManager Instance
    {
        get { return m_Instance; }
    }

    [SerializeField]
    private float m_LoadingTimer = 5f;
    [SerializeField]
    private GameObject m_LoadingScreen;

    private void Awake()
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        m_LoadingScreen.SetActive(false);
    }

    private void Start()
    {
        ChangeLevel("Menu");
    }

    private void OnLoadingDone(Scene aScene, LoadSceneMode aMode)
    {
        SceneManager.sceneLoaded -= OnLoadingDone;
        m_LoadingScreen.SetActive(false);
    }

    public void ChangeLevel(string aScene)
    {
        StartCoroutine(Loading(aScene));
    }

    private IEnumerator Loading(string aScene)
    {
        /*if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopMusic();
        }*/
        if (aScene != "Menu")
        {
            m_LoadingScreen.SetActive(true); 
            yield return new WaitForSeconds(m_LoadingTimer);         
        }
        else 
        {
            yield return new WaitForSeconds(0f);      
        }
        
        SceneManager.LoadScene(aScene);
        SceneManager.sceneLoaded += OnLoadingDone;
    }

}