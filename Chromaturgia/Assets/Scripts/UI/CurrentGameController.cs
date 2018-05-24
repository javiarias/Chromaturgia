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


        if (gameObject.activeSelf && !File.Exists("saveData"))
        {
            gameObject.SetActive(false);
        }
        else if (File.Exists("saveData") && loadLimiter)
        {
            loadLimiter = false;
            SaveLoad.instance.Load();

            int count = 0;
            completedLevels = GameManager.instance.completedLevels;
            for (int x = 0; x < completedLevels.Length; x++)
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

            string text = "";

            if (hours < 10)
            {
                text += "0" + hours + ":";
            }
            else { text += hours + ":"; }
            if (min < 10)
            {
                text += "0" + min + ":";
            }
            else { text += min + ":"; }
            if (sec < 10)
            {
                text += "0" + sec;
            }
            else { text += sec; }

            playtime.text = text;

            r.SetActive(GameManager.instance.redPiecePicked);
            g.SetActive(GameManager.instance.greenPiecePicked);
            b.SetActive(GameManager.instance.bluePiecePicked);
        }
    }
}
