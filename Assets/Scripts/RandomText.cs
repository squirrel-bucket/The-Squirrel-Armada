using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;

public class RandomText1 : MonoBehaviour
{
    public string[] words = { "XA", "XB", "XC", "XD", "XE", "XF" };
    public TMP_Text text;

    private void Start()
    {
        // log whatever comes out of the RandomWord string.
        string wordToDisplay = RandomWord();

        text.text = wordToDisplay;
    }

    public void buttonStuff()
    {
        string wordToDisplay = RandomWord();
        Debug.Log("EH");
        text.text = wordToDisplay;
    }

    // when you see a string function,
    // it will return a string that
    // you can use anywhere!
    private string RandomWord()
    {
        // grab a random string from the words array
        string randomWord = words[Random.Range(0, words.Length)];

        // return it (this will be the string that the script will use)
        return randomWord;
    }

    public void RESPAWN()
    {
        RandomWord();
        string wordtodisplay = RandomWord();
        text.text = wordtodisplay;
    }

}