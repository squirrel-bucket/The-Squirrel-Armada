using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class rotScript : MonoBehaviour
{
    public Transform RotationCenter;

    public float rotSpeed, rotRad;

    private float posX, posY;

    public float angle = 0f;

    private void Update()
    {
        posX = RotationCenter.position.x + Mathf.Cos(angle) * rotRad;
        posY = RotationCenter.position.y + Mathf.Sin(angle) * rotRad;
        transform.position = new Vector2(posX, posY);
        angle = angle + Time.deltaTime * rotSpeed;

        if (angle >= 360)
        {
            angle = 0;
        }
    }
}
