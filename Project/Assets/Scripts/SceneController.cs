using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void NextScene()
    {
        Debug.Log("Loading Next Scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void OnQuit()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
