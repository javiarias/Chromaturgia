using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class CurrentGameController : MonoBehaviour {

    bool loadLimiter = true;
    GameObject mosaic, r, g, b;
    Text curedAnimals, playtime;
    bool[] completedLevels;

    private void Start()
    {
        mosaic = GameObject.Find("MiniMosaico");
        r = mosaic.transform.GetChild(0).gameObject;
        r.SetActive(false);
        g = mosaic.transform.GetChild(1).gameObject;
        g.SetActive(false);
        b = mosaic.transform.GetChild(2).gameObject;
        b.SetActive(false);
        curedAnimals = GameObject.Find("AnimalesCurados").GetComponent<Text>();
        playtime = GameObject.Find("Tiempo").GetComponent<Text>();
    }

    private void Update()
    {
        if (gameObject.activeSelf && !File.Exists(SaveLoad.instance.SavePath + "/saveData"))
        {
            gameObject.SetActive(false);
        }
        else if (File.Exists(SaveLoad.instance.SavePath + "/saveData") && loadLimiter)
        {
            loadLimiter = false;
            SaveLoad.instance.Load();

            int count = 0;
            completedLevels = GameManager.instance.completedLevels;
            for(int x = 0; x < completedLevels.Length; x++)
            {
                if (completedLevels[x])
                {
                    count++;
                }
            }
            curedAnimals.text = count.ToString();

            int sec = Mathf.RoundToInt(GameManager.instance.playtime);

            int hours = sec / 3600;
            sec = sec % 3600;
            int min = sec / 60;
            sec = sec % 60;

            playtime.text = hours + "  :  " + min + "  :  " + sec;

            r.SetActive(GameManager.instance.redPiecePicked);
            g.SetActive(GameManager.instance.greenPiecePicked);
            b.SetActive(GameManager.instance.bluePiecePicked);
        }
    }
}
