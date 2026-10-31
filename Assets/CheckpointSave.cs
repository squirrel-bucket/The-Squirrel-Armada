using UnityEngine;

public class CheckpointPersistence : MonoBehaviour, DataPersistence
{
    public void LoadData(GameData data)
    {
        GameManager.lastCheckpointPos =
            data.lastCheckpointPosition;
    }

    public void SaveData(ref GameData data)
    {
        data.lastCheckpointPosition =
            GameManager.lastCheckpointPos;
    }
}