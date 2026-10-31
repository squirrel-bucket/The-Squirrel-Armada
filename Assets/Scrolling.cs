using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public GameObject BUTTONS;
    public Vector2 moveSpeed = new Vector2(3f, 0);
    public Vector2 distance = new Vector2(-2500, 0);
    public void OnClick()
    {
        BUTTONS.transform.position = new Vector2(-2500, 0);
    }
}
