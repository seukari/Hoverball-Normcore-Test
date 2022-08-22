using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;
    public static SceneLoader Instance { get => instance; }

    private bool loadingScreenEnabled;
    public GameObject loadingScreenObject;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {

        SceneManager.LoadSceneAsync(sceneName);
    }

    public void StartLoadingScreen()
    {
        loadingScreenEnabled = true;
        loadingScreenObject.SetActive(true);
    }

    public void EndLoadingScreen()
    {
        loadingScreenEnabled = false;
        loadingScreenObject.SetActive(false);
    }
}
