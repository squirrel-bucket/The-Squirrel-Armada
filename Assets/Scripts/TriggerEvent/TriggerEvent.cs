using System;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public int eventToTrigger = 0;
    private Action functionToTrigger;

    public TemporaryDarkness temporaryDarkness;
    public PermanentDarkness permanentDarkness;
    public FlashEvent flashEvent;
    public AudioEvent audioEvent;

    private void Start()
    {
        switch(eventToTrigger)
        {
            case 0:
                functionToTrigger = temporaryDarknessMethod;
                break;
            case 1:
                functionToTrigger = permanentDarknessMethod;
                break;
            case 2:
                functionToTrigger = flashEventMethod; 
                break;
            case 3:
                functionToTrigger = audioEventMethod;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            functionToTrigger();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void temporaryDarknessMethod() // Case 0, Change textures to dark pallete for a second
    {

    }

    private void permanentDarknessMethod() // Case 1, Change textures for the rest of the level to be the dark pallete
    {

    }

    private void flashEventMethod() // Case 2, Invert screen colors (or other colors idk)
    {
        flashEvent.screenColorInverter.SetInverted();

    }

    private void audioEventMethod() // Case 3, Play random sound for no apparent reason
    {
        audioEvent.audio.Play();
    }
}
