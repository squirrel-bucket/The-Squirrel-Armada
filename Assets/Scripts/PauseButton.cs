using UnityEngine;
using UnityEngine.InputSystem;

public class PauseButton : MonoBehaviour
{
    public void Play()
    {
        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            PlayerInput playerInput = player.GetComponent<PlayerInput>();

            if (playerInput != null)
                playerInput.ActivateInput();
        }

        GameObject pausePanel = GameObject.Find("PauseMenu");

        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;

        GameObject musicObject = GameObject.Find("Music");

        if (musicObject != null)
        {
            AudioSource audioSource = musicObject.GetComponent<AudioSource>();

            if (audioSource != null)
                audioSource.UnPause();
        }
    }
}