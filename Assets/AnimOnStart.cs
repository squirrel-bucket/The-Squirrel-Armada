using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimOnStart : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.Play("Crossfade_End", 0, 0f);
    }

}
