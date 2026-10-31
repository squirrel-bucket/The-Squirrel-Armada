using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public SerializableDictionary<string, bool> pickupsCollected;
    public SerializableDictionary<string, bool> knightsSlain;
    public Vector3 enemyPosition;
    public Vector3 lastCheckpointPosition;

    public GameData()
    {
        pickupsCollected = new SerializableDictionary<string, bool>();
        knightsSlain = new SerializableDictionary<string, bool>();
        enemyPosition = Vector3.zero;
        lastCheckpointPosition = new Vector3(256, 180, 0);
    }
}
