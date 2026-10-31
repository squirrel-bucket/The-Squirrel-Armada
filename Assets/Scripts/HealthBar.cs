using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthBarText;
    Damagable playerDamagable;
    public GameManager gameManager;
    Player_Controller playerController;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player == null)
        {
            Debug.Log("wha- there's no player??? WHAT DID YOU DO??!");
        }
          
        playerDamagable = player.GetComponent<Damagable>();

    }


    // Start is called before the first frame update
    void Start()
    {
        healthSlider.value = CalculateSliderPercentage(playerDamagable.Health, playerDamagable.MaxHealth);
        healthBarText.text = "HP " + playerDamagable.Health + " / " + playerDamagable.MaxHealth;
        gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        playerDamagable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDamagable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = "HP " + newHealth + " / " + maxHealth;
        if(playerDamagable.Health <= 0)
        {
            Debug.Log("Dead");
            healthBarText.text = "HP " + maxHealth + " / " + maxHealth;
            playerDamagable.Health = 100;
            gameManager.gameOver();
        }
    }

    
}
