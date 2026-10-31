using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public int gameStartScene;

    public void StartGame()
    {
        Invoke("TestSTUFF", 1);
    }

    public void TestSTUFF()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(gameStartScene);
    }
}
