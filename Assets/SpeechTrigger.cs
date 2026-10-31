using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechTrigger : MonoBehaviour
{
    private BoxCollider2D col;
    public GameObject parser;
    private SubtitleController subAbove;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        subAbove = parser.GetComponent<SubtitleController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            subAbove.PlayDialogue();
            col.enabled = false;
        }
    }
}
