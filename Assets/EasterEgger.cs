using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EasterEgger : MonoBehaviour
{
    public AudioSource audioSource;
    public int mainMenuScene;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => !audioSource.isPlaying);
        yield return new WaitForSecondsRealtime(5f);

        SceneManager.LoadScene(mainMenuScene);
    }
}
