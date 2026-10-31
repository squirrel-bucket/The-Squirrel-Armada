using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lvlSave : MonoBehaviour
{
    public Button[] lvlButtons;
    public Sprite buttonLocked;

    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 3);

        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 3 > levelAt)
            {
                lvlButtons[i].GetComponent<Image>().sprite = buttonLocked;
                lvlButtons[i].interactable = false;
            }
        }
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log(PlayerPrefs.GetInt("levelAt"));
    }
}
