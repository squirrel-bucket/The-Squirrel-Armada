using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextFadeInFadeOut : MonoBehaviour
{
    public GameObject text;
    public Animator anim;

    public IEnumerator TextAnimation()
    {
        text.gameObject.SetActive(true);
        anim.Play("FadeIn");
        yield return new WaitForSecondsRealtime(3f);
        anim.Play("FadeOut");
    }

    public void StartFadeOut()
    {
       StartCoroutine(TextAnimation());
    }
}
