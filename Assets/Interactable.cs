using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public Button button;
    public Sprite locked;
    public int valNeeded; //value of PlayerPrefs needed in order to unlock this button

    private void Awake()
    {

        button.interactable = true;

        if (PlayerPrefs.GetInt("levelAt") < valNeeded)
        {
            button.GetComponent<Image>().sprite = locked;
            button.interactable = false;
        }
    }
}
