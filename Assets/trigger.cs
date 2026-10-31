using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        Activate();
    }

    private void Activate()
    {
        rb.gravityScale = 1;
        StartCoroutine(Kaboom());
    }

    private IEnumerator Kaboom()
    {
        yield return new WaitForSecondsRealtime(5f);
        Debug.Log("works");
        anim.SetTrigger("kaboom");
    }
}
