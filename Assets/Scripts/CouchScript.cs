using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouchScript : MonoBehaviour
{
    public GameObject speech;

    private void Start()
    {
        speech.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            speech.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            speech.SetActive(false);
        }
    }
}
