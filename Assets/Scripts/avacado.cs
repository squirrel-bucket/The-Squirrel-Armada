using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class avacado : MonoBehaviour
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


    DataPersistenceManager dataPersistenceManager;
    public GameManager gameManager;

    private void Awake()
    {
        GameObject DPM = GameObject.FindGameObjectWithTag("DPM");
        dataPersistenceManager = DPM.GetComponent<DataPersistenceManager>();
        eventSystem = GameObject.FindGameObjectWithTag("EVENTSYSTEM");
        player = GameObject.FindGameObjectWithTag("Player");
        playerInput = player.GetComponent<PlayerInput>();
        UIManager = GameObject.FindGameObjectWithTag("UIMANAGER");
        UIInput = UIManager.GetComponent<PlayerInput>();
        animator = this.gameObject.GetComponent<Animator>();
    }


    void Start()
    {
        nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
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
        EnemySpecs[] slots = FindObjectsByType(typeof(EnemySpecs)) as EnemySpecs[];

        bool bLoadNextLevel = true;

        foreach (EnemySpecs slot in slots)
        {
            if (slot.living == true)
            {
                bLoadNextLevel = false;
                break;
            }
        }

        if (bLoadNextLevel)
        {
            playerInput.enabled = false;

            Invoke("LOAD", 250/100);
            animator.SetTrigger("minersTouched");
            UIInput.enabled = false;
            eventSystem.SetActive(false);
        }
    }
    



    IEnumerator LoadLevel(int levelIndex)

    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
        dataPersistenceManager.NewGame();
    }

    public void LOAD()
    {

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

        if (nextLevel > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", nextLevel);
        }
    }

}

