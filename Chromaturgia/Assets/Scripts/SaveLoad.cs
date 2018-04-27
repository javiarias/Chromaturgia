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
    public float soundVolume;

	public bool saveDataExists;

	Animator anim;

    void Awake()
    { 
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);

            saveDataExists = File.Exists(Application.persistentDataPath + "/saveData");
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
        File.Delete(Application.persistentDataPath + "/saveData");
    }

    public void Save()
    {
		anim.SetTrigger ("Saving");

		BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saveData");

        SaveData data = new SaveData();

        data.completedLevels = GameManager.instance.completedLevels;
        data.brightness = brightness;
        data.saturation = saturation;
        data.soundVolume = soundVolume;
        data.musicVolume = musicVolume;

        binaryFormatter.Serialize(file, data);
        file.Close();
    }

	public void Load()
    {
		if (File.Exists(Application.persistentDataPath + "/saveData"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveData", FileMode.Open);

            SaveData data = (SaveData)binaryFormatter.Deserialize(file);
            file.Close();

            SceneManager.LoadScene("Hub");

            GameManager.instance.completedLevels = data.completedLevels;

            GameManager.instance.brightness = data.brightness;
            GameManager.instance.ChangeBrightness();

            GameManager.instance.saturation = data.saturation;
            GameManager.instance.ChangeSaturation();

            for (int i = 0; i < GameManager.MAX_LEVELS; ++i)
            {
                //Debug.Log(GameManager.instance.completedLevels[i]);
            }
        }
    }
}

[Serializable]
class SaveData
{
    public bool[] completedLevels;
    public float brightness;
    public float saturation;
    public float musicVolume;
    public float soundVolume;
}
