using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    public static SaveLoad instance = null;

    public float brightness;
    public float saturation;
    public float musicVolume;
    static public float soundVolume;
    

	Animator anim;

    [HideInInspector]
    public string SavePath = "..";

    void Awake()
    { 
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);

            //saveDataExists = File.Exists(SavePath + "/saveData");
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

	void Start()
	{
		anim = GetComponent<Animator>();
	}

    public void Reset()
    {
        File.Delete(SavePath + "/saveData");
    }

    public void SaveLevels()
    {
        anim.SetTrigger ("Saving");

		BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file;

        if (!File.Exists(SavePath + "/saveData"))
        {
            file = File.Create(SavePath + "/saveData");
        }

        else
        {
            file = File.Open(SavePath + "/saveData", FileMode.Open);
        }

        SaveData data = new SaveData();
        
        data.completedLevels = GameManager.instance.completedLevels;
        data.redPicked = GameManager.instance.redPiecePicked;
        data.greenPicked = GameManager.instance.greenPiecePicked;
        data.bluePicked = GameManager.instance.bluePiecePicked;
        data.playtime = Mathf.Round(GameManager.instance.playtime);

        binaryFormatter.Serialize(file, data);
        file.Close();
    }

    public void SaveConfig()
    {
        anim.SetTrigger("Saving");

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file;

        if (!File.Exists(SavePath + "/configData"))
        {
            file = File.Create(SavePath + "/configData");
        }

        else
        {
            file = File.Open(SavePath + "/configData", FileMode.Open);
        }

        ConfigData data = new ConfigData();

        data.brightness = brightness;
        data.saturation = saturation;
        data.soundVolume = soundVolume;
        data.musicVolume = musicVolume;

        binaryFormatter.Serialize(file, data);
        file.Close();
    }

    public bool Load()
    {
        bool exists = File.Exists(SavePath + "/saveData");

        if (exists)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(SavePath + "/saveData", FileMode.Open);

            SaveData data = (SaveData)binaryFormatter.Deserialize(file);
            file.Close();
            
            GameManager.instance.completedLevels = data.completedLevels;
            GameManager.instance.redPiecePicked = data.redPicked;
            GameManager.instance.greenPiecePicked = data.greenPicked;
            GameManager.instance.bluePiecePicked = data.bluePicked;
            GameManager.instance.playtime = Mathf.Round(data.playtime);
        }

        return exists;
    }

    public bool LoadConfig()
    {
        bool exists = File.Exists(SavePath + "/configData");

        if (exists)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(SavePath + "/configData", FileMode.Open);

            ConfigData data = (ConfigData)binaryFormatter.Deserialize(file);
            file.Close();

            GameManager.instance.brightness = data.brightness;
            GameManager.instance.ChangeBrightness();

            GameManager.instance.saturation = data.saturation;
            GameManager.instance.ChangeSaturation();

            GameManager.instance.musicVolume = data.musicVolume;

            GameManager.instance.soundVolume = data.soundVolume;

        }

        return exists;
    }

    public bool saveDataExists()
    {
        return File.Exists(SavePath + "/saveData");
    }
}

[Serializable]
class SaveData
{
    public bool[] completedLevels;
    public bool redPicked, greenPicked, bluePicked;
    public float playtime;
}

[Serializable]
class ConfigData
{
    public float brightness;
    public float saturation;
    public float musicVolume;
    public float soundVolume;
}
