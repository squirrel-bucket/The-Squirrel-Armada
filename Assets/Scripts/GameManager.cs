using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int knightsLiving = 0;
    public static bool isGameOver;
    public static Vector2 lastCheckpointPos = new Vector2(256, 180);
    DataPersistenceManager dataPersistenceManager;
    public Vector2 startPos = new Vector2(256, 180);

    void Awake()
    {
        isGameOver = false;

        GameObject DPM =
            GameObject.FindGameObjectWithTag("DPM");

        if (DPM != null)
        {
            dataPersistenceManager =
                DPM.GetComponent<DataPersistenceManager>();
        }

        if (dataPersistenceManager == null)
        {
            Debug.LogError(
                "DataPersistenceManager not found!");
        }
    }

    public void gameOver()
    {
        GameObject player =
            GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player not found!");
            return;
        }

        player.SetActive(true);
        player.transform.position = lastCheckpointPos;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
