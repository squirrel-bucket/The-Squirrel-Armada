using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrunkleStan : MonoBehaviour
{
    public GameObject bill;
    public void Stanford()
    {
        bill.SetActive(true);
        Invoke("Stanley", 1);
    }

    public void Stanley()
    {
        bill.SetActive(false);
    }
}
