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
    public string SavePath = "";

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
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file;

        if (File.Exists("saveData"))
        {
            File.Delete("saveData");
        }

        file = File.Create("saveData");

        SaveData data = new SaveData();

        bool[] aux = new bool[GameManager.MAX_LEVELS];
        for (int i = 0; i < aux.Length; i++)
            aux[i] = false;

        data.completedLevels = aux;
        data.redPicked = false;
        data.greenPicked = false;
        data.bluePicked = false;
        data.playtime = 0;

        binaryFormatter.Serialize(file, data);
        file.Close();
    }

    public void SaveLevels()
    {
        anim.SetTrigger ("Saving");

		BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file;

        if (!File.Exists("saveData"))
        {
            file = File.Create("saveData");
        }

        else
        {
            file = File.Open("saveData", FileMode.Open);
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
        if (GameManager.instance != null) //al hacer un poquito de troubleshooting vi que cuando cierras el juego de golpe, algo llama a SaveConfig() como una instancia vacía, guardando datos erróneos. Esto debería remediarlo
        {
            anim.SetTrigger("Saving");

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file;

            file = File.Open("configData", FileMode.Open);
            ConfigData data = new ConfigData();

            data.brightness = GameManager.instance.brightness;
            data.saturation = GameManager.instance.saturation;
            data.soundVolume = GameManager.instance.soundVolume;
            data.musicVolume = GameManager.instance.musicVolume;

            binaryFormatter.Serialize(file, data);
            file.Close();
        }
    }

    public bool Load()
    {
        bool exists = File.Exists("saveData");

        if (exists)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open("saveData", FileMode.Open);

            SaveData data = (SaveData)binaryFormatter.Deserialize(file);
            file.Close();
            
            GameManager.instance.completedLevels = data.completedLevels;
            GameManager.instance.redPiecePicked = data.redPicked;
            GameManager.instance.greenPiecePicked = data.greenPicked;
            GameManager.instance.bluePiecePicked = data.bluePicked;
            GameManager.instance.playtime = Mathf.Round(data.playtime);
            GameManager.instance.sceneToLoad = "Hub";
        }

        return exists;
    }

    public bool LoadConfig()
    {
        bool exists = File.Exists("configData");
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file;

        if (!exists)
        {
            file = File.Create("configData");
            ConfigData dataA = new ConfigData();

            dataA.brightness = 0;
            dataA.saturation = 0;
            dataA.soundVolume = -6.4f;
            dataA.musicVolume = -16;

            binaryFormatter.Serialize(file, dataA);
            file.Close();
        }

        binaryFormatter = new BinaryFormatter();
        file = File.Open("configData", FileMode.Open);

        ConfigData data = (ConfigData)binaryFormatter.Deserialize(file);
        file.Close();
        
        GameManager.instance.brightness = data.brightness;
        GameManager.instance.ChangeBrightness();

        GameManager.instance.saturation = data.saturation;
        GameManager.instance.ChangeSaturation();
        
        GameManager.instance.musicVolume = data.musicVolume;

        GameManager.instance.soundVolume = data.soundVolume;

        return exists;
    }

    public bool saveDataExists()
    {
        return File.Exists("saveData");
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
