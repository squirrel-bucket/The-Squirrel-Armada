using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class wineFlip : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private Transform playerTrans;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        playerTrans = player.GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.gravityScale = -2f;
            playerTrans.eulerAngles = Vector3.forward * 180;
        }
    }
}
