using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public Vector2 moveSpeed = new Vector2(3f, 0);
    public Vector2 knockback = new Vector2(0, 0);
    
    int degreesPerSec = 4000;

    Rigidbody2D rb;

    public void Rotate()
    {
        float rotAmount = degreesPerSec * Time.deltaTime;
        float curRot = transform.localRotation.eulerAngles.z;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot + rotAmount));
        InvokeRepeating("Rotate", 0, 1 / 16);
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Rotate();
    }

    // Start is called before the first frame update
    void Start()
    {
        // project effect by gravity = dynamic mode rgdbdy
        rb.linearVelocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.GetComponent<Damagable>();

        if (damagable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            //hit target
            bool getHit = damagable.Hit(damage, deliveredKnockback);

            if (getHit)
            {
                Debug.Log(collision.name + " hit for " + damage);

                Destroy(gameObject);
            }
        }
    }
}
