using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;

    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }
    }

    public int sceneToLoad;

    public void OnNewGameClicked()
    {
        DisableMenuButtons();

        Debug.Log("New Game Clicked. Wowza.");

        DataPersistenceManager.instance.NewGame();

        SceneManager.LoadSceneAsync(sceneToLoad);
    }
    public void OnContinueGameClicked()
    {
        DisableMenuButtons();

        Debug.Log("Who's in Paris??");

        SceneManager.LoadSceneAsync(sceneToLoad);
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
}