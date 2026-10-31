using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UIData
{
    public SerializableDictionary<string, bool> isBeat;

    public UIData()
    {
        isBeat = new SerializableDictionary<string, bool>();
    }
}