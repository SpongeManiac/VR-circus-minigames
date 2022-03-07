using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataStore : MonoBehaviour
{
    public static DataStore instance;
    static string DATAPATH = "";

    DataList data = new DataList();

    bool exists { get { return File.Exists(DATAPATH); } }

    private void Awake()
    {
        DATAPATH = Application.persistentDataPath + "/USRDAT.bin";
        Debug.Log(DATAPATH);
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("Cannot create multiple instances of DataStore!");
            Destroy(this);
        }
        //check if data store already exists, and, if so, load it
        LoadData();
    }

    //Util functions

    public bool SaveData()
    {
        if(data != null)
        {
            try
            {
                using (var dat = File.Open(DATAPATH, FileMode.Create))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(dat, data);
                }
                return true;
            }
            catch (IOException e)
            {
                Debug.Log("Could not save data!");
                Debug.LogError(e);
            }
        }
        return false;
    }

    public void LoadData()
    {
        if (exists)
        {
            try
            {
                Debug.Log("Loading datastore");
                using (var stream = File.Open(DATAPATH, FileMode.Open))
                {
                    var formatter = new BinaryFormatter();
                    data = (DataList)formatter.Deserialize(stream);
                }
            }
            catch(IOException e)
            {
                Debug.Log("Failed to save datalist");
                Debug.LogError(e);
            }
        }
    }

    public void SetData(object data, string name)
    {
        this.data.SetPersistentData(data, name);
    }
    public void DelData(string field)
    {
        this.data.RemovePersistentData(field);
    }
    public PersistentData GetData(string field)
    {
        return data.GetPersistentData(field);
    }
}


//Data contains a sorted list of PersistentData and provides api for getting and setting data

[System.Serializable]
public class DataList
{
    public Dictionary<string, PersistentData> fields = new Dictionary<string, PersistentData>();
    public DataList(Dictionary<string, PersistentData> list)
    {
        fields = list;
    }
    public DataList(){}

    public void SetPersistentData(object data, string fieldName)
    {
        validName(fieldName);
        fields[fieldName] = new PersistentData(data);
    }

    public void RemovePersistentData(string field)
    {
        fields.Remove(field);
    }

    public PersistentData GetPersistentData(string field)
    {
        try
        {
            var dat = fields[field];
            return dat;
        }
        catch(KeyNotFoundException e)
        {
            Debug.Log("The key '"+field+"' was not present.");
        }
        return null;
    }

    bool validName(string name)
    {
        if (fields.ContainsKey(name))
        {
            return true;
        }
        Debug.Log("Non-unique field name: "+name+"\nUpdating field instead of making new.");
        return false;
    }

    PersistentData GetData(string field)
    {
        return fields[field];
    }
}