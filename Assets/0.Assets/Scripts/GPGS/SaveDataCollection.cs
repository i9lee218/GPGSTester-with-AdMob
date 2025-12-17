
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SaveDataCollection // : MonoBehaviour
{
    [System.Serializable]
    public struct SampleData
    {
        public string _playerName;
        public int _score;
    }

    public SampleData sampleData = new SampleData();

    //=======================================================
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
        
    }

    public void LoadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }

}


public interface ISaveable
{
    void PopulateSaveData(SaveDataCollection saveData);
    void LoadFromSaveData(SaveDataCollection saveData);

}
