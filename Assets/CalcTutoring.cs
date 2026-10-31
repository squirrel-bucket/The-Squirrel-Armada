using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CalcTutoring : MonoBehaviour
{
    public int tutorialScene;
    public GameObject chapterSelect;
    public GameObject tutorOption;
    public GameObject mainMenu; // not necessary- could just put it in the button... but this makes it easier to read


    public void CowCowLust()
    {
        if (!(PlayerPrefs.GetInt("levelAt") <= 3))
        {
            mainMenu.SetActive(false);
            chapterSelect.SetActive(true);

        }
        else
        {
            mainMenu.SetActive(false);
            tutorOption.SetActive(true);

        }
    }

    public void HumorTheCalc()
    {
        SceneManager.LoadScene(tutorialScene);
    }

    public void SkipTheCalc()
    {
        tutorOption.SetActive(false);
        chapterSelect.SetActive(true);

    }
}
