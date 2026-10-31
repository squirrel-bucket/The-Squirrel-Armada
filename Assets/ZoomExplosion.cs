using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class ZoomExplosion : MonoBehaviour
{
    public Camera vcam;
    public GameObject diabetes;
    public GameObject music;

    AudioSource audioSource;
    AudioSource audioSourceCake;

    public TMP_Text text;

    Vector2 knockback = Vector2.zero;

    GameObject amongTheHidden;


    Color32 myColor32 = new Color32(156, 156, 156, 255);

    GameObject lighting;
    Light2D light2D;

    GameObject light2D2;
    Light2D light2D3;

    public GameObject player;
    PlayerInput playerInput;
    Animator animator;

    Damagable damagable;

    private void Awake()
    {
        lighting = GameObject.Find("Global Light 2D");
        light2D = lighting.GetComponent<Light2D>();
        playerInput = player.GetComponent<PlayerInput>();
        animator = player.GetComponent<Animator>();
        damagable = player.GetComponent<Damagable>();

        amongTheHidden = GameObject.Find("GameCanvas");

        audioSource = music.GetComponent<AudioSource>();
        audioSourceCake = this.gameObject.GetComponent<AudioSource>();

        light2D2 = GameObject.Find("Light 2D");
        light2D3 = light2D2.GetComponent<Light2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            vcam.orthographicSize = 1;
            amongTheHidden.SetActive(false);

            audioSource.Pause();
            audioSourceCake.Pause();

            playerInput.DeactivateInput();
            animator.SetTrigger(AnimationStrings.combustibleFoldingChair);

            Invoke("BLACK", 2);
            Invoke("WHITE", 4);
        }
    }

    private void BLACK()
    {
        light2D.color = Color.black;
        light2D3.color = Color.black;  
    }

    private void WHITE()
    {
        SceneManager.LoadScene(30);
    }
}