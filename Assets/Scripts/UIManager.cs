using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    private GameObject gameCanvas;
    private GameObject pausePanel;
    public AudioSource audioSource;

    private void Start()
    {
        pausePanel.SetActive(false);
    }

    private void Awake()
    {
        foreach (GameObject obj in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (obj.CompareTag("PAUSE MENU"))
            {
                pausePanel = obj;
                break;
            }
        }

        gameCanvas = GameObject.Find("GameCanvas");
    }

    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
    }

    public void CharacterTookDamage(GameObject character, int damageRecieved)
    {
        //create text at character hit
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmpText.text = damageRecieved.ToString();
    }

    public void CharacterHealed(GameObject character, int healthRestored)
    {
        //create text at character heal
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        tmpText.text = healthRestored.ToString();
    }

    public void OnExitGame(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        if (Mathf.Approximately(Time.timeScale, 0f))
            return;

        GameObject player = GameObject.Find("Player");

        if (player == null)
            return;

        PlayerInput playerInput = player.GetComponent<PlayerInput>();

        pausePanel.SetActive(true);
        Time.timeScale = 0f;

        if (playerInput != null)
            playerInput.DeactivateInput();

        if (audioSource != null)
            audioSource.Pause();
    }
}
