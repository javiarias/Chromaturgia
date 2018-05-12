using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfectedAnimalBehaviour : MonoBehaviour
{
	ParticleSystem.MainModule FX;
    Animator animator;
    Color colors;
    Color laserColor;
    bool win;
    static int totalAnimals = 0;
    static int curedAnimals = 0;
    static Text levelText;

    void Awake()
    {
        totalAnimals = curedAnimals = 0;
        levelText = GameObject.FindGameObjectWithTag("LevelInfo").GetComponent<Text>();
    }

    void Start()
    {
        totalAnimals++;
        UpdateText();

		FX = gameObject.GetComponentInChildren<ParticleSystem>().main;
        colors = gameObject.GetComponentInChildren<SpriteRenderer>().color;
        GameManager.instance.puzzleComplete = win = false;

        animator = gameObject.GetComponent<Animator>();
        InvokeRepeating("AnimacionIdle2", 0, 5f);
    }

    void Update()
    {
        if (!win && colors == Color.white)
        {
			FindObjectOfType<AudioManager> ().Play ("AnimalCurado");
            win = true;
            curedAnimals++;
            UpdateText();

            GameManager.instance.SetPuzzleAsCompleted();
        }
    }

    void UpdateText()
    {
        levelText.text = (curedAnimals.ToString()) + "  de  " + (totalAnimals.ToString());
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!win && coll.gameObject.tag == "Laser")
        {
            laserColor = coll.gameObject.GetComponent<SpriteRenderer>().color;
            if (laserColor == Color.red)
            {
                colors.r = 1;
            }
            else if (laserColor == Color.green)
            {
                colors.g = 1;
            }
            else if (laserColor == Color.blue)
            {
                colors.b = 1;
            }
            gameObject.GetComponentInChildren<SpriteRenderer>().color = colors;
			FX.startColor = new ParticleSystem.MinMaxGradient (colors);
            Destroy(coll.gameObject);
        }
    }

    void AnimacionIdle2()
    {
        animator.SetTrigger("Idle2");
    }
}
