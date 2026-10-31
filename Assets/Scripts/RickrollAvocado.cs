using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RickrollAvocado : MonoBehaviour
{
    Animator animator;
    public Animator transition;
    public int nextLevel;
    public float transitionTime = 1f;
    private GameObject eventSystem;
    private GameObject player;
    private PlayerInput playerInput;
    private PlayerInput UIInput;
    private GameObject UIManager;
    public GameManager gameManager;
    private lvlSave lvlSave;

    private void Awake()
    {
        lvlSave = GetComponent<lvlSave>();
        eventSystem = GameObject.FindGameObjectWithTag("EVENTSYSTEM");
        player = GameObject.FindGameObjectWithTag("Player");
        playerInput = player.GetComponent<PlayerInput>();
        UIManager = GameObject.FindGameObjectWithTag("UIMANAGER");
        UIInput = UIManager.GetComponent<PlayerInput>();
        animator = this.gameObject.GetComponent<Animator>();
    }


    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        playerInput.enabled = true;
        UIInput.enabled = true;
        eventSystem.SetActive(true);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            checking();
        }
    }

    public void checking()
    {

            Invoke("LOAD", 250/100);
            animator.SetTrigger("minersTouched");
            UIInput.enabled = false;
            eventSystem.SetActive(false);
        
    }
    



    IEnumerator LoadLevel(int levelIndex)

    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(0);
    }

    public void LOAD()
    {

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 9));
        lvlSave.ResetGame();
        playerInput.enabled = false;
    }

}

