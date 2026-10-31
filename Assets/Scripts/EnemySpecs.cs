using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpecs : MonoBehaviour
{

    Damagable damagable;
    GameManager gameManager;
    public bool living = true;
    SpriteRenderer spriteRenderer;
    Color startColor;

    private void Awake()
    {
        damagable = this.GetComponent<Damagable>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
    }


    public void checkItOut()
    {
        if (this.damagable.IsAlive == false)
        {
            living = false;
        }
        if (this.damagable.IsAlive == true)
        {
            living = true;
            spriteRenderer.color = startColor;
            Debug.Log("MINORS");
        }
    }
}
