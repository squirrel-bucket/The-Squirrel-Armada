using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource introSource;

    // Start is called before the first frame update
    void Start()
    {
        introSource.Play();
    }
}
