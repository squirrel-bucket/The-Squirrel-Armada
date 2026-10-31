using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider2D attackCollider;
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //see if it can be hit
        Damagable damagable = collision.GetComponent<Damagable>();

        if (damagable != null)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            //hit target
            bool getHit = damagable.Hit(attackDamage, deliveredKnockback);

            if (getHit)
            {
            Debug.Log(collision.name + " hit for " + attackDamage);
            }
        }
    }
}
