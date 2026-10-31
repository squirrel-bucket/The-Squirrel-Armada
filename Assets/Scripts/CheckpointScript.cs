using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            GameManager.lastCheckpointPos = transform.position;
            Debug.Log("IT WORKS!!!!");
            isActivated = true;
        }
    }

    private bool isActivated = false;
    public bool IsActivated
    {
        get
        {
            return isActivated;
        }
        private set
        {
            isActivated = value;
            animator.SetBool(AnimationStrings.isActivated, value);
        }
    }
}
