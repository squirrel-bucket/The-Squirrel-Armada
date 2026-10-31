using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelLoader : MonoBehaviour
{
    private GameObject eventSystem;
    private GameObject player;
    private PlayerInput playerInput;
    private PlayerInput UIInput;
    private GameObject UIManager;
    GameObject music;
    AudioSource audioSource;


    public GameManager gameManager;

    private void Awake()
    {
        eventSystem = GameObject.FindGameObjectWithTag("EVENTSYSTEM");
        player = GameObject.FindGameObjectWithTag("Player");
        playerInput = player.GetComponent<PlayerInput>();
        UIManager = GameObject.FindGameObjectWithTag("UIMANAGER");
        UIInput = UIManager.GetComponent<PlayerInput>();
        music = GameObject.Find("Music");
        audioSource = music.GetComponent<AudioSource>();

    }

    public void Off()
    {
        playerInput.DeactivateInput();
        UIInput.DeactivateInput();
        eventSystem.SetActive(false);
    }

    public void On()
    {
        eventSystem.SetActive(true);
        UIInput.ActivateInput();
        playerInput.ActivateInput();
    }

    public static class AudioFadeOut
    {

        public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }
    }

    public void FadeOut()
    {
        StartCoroutine(AudioFadeOut.FadeOut(audioSource, 0.5f));
    }
}
