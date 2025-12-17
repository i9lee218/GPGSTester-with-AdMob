using UnityEngine;

public class SaveLoadManager : MonoBehaviour, ISaveable
{
    private string fileName = "saveData.json";
    private GPGSTester gpgsTester;

    public static SaveLoadManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            //Destroy(transform.root.gameObject);
            //Destroy(this.gameObject);

        }
        else
        {

            Instance = this;
            DontDestroyOnLoad(gameObject);
            //DontDestroyOnLoad(transform.root.gameObject);

        }

        gpgsTester = FindFirstObjectByType<GPGSTester>();  

        GPGS.Instance.InitiateGPGS();
    }

    public void Save()
    {
        Debug.Log("Save at " + this.gameObject.name);
        SaveDataCollection saveDataCollection = new SaveDataCollection();
        PopulateSaveData(saveDataCollection);

        GPGS.Instance.Save(fileName, saveDataCollection);
        
    }

    public void Load()
    {
        Debug.Log("Load at " + this.gameObject.name);
        //SaveDataCollection saveDataCollection = new SaveDataCollection();
        //GPGS.Instance.Load(fileName, saveDataCollection);
        GPGS.Instance.Load(fileName);
    }

    public void LoadFromSaveData(SaveDataCollection saveData)
    {
        gpgsTester.LoadFromSaveData(saveData);
    }

    public void PopulateSaveData(SaveDataCollection saveData)
    {
        gpgsTester.PopulateSaveData(saveData);
        
    }

    
}
