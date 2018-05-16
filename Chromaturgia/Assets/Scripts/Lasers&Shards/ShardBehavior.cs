using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardBehavior : MonoBehaviour {

	Color shardColor;
	ParticleSystem.MainModule FX;

	void Start()
	{
		shardColor = gameObject.GetComponent<SpriteRenderer> ().color;
		FX = GetComponent<ParticleSystem> ().main;
		FX.startColor = shardColor;
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
			FindObjectOfType<AudioManager> ().Play ("CollectShard");

			if (shardColor == Color.red)
            {
                GameManager.instance.IncreaseColor(GameManager.Option.Red);
			}
			else if (shardColor == Color.green)
			{
				GameManager.instance.IncreaseColor(GameManager.Option.Green);
			}
			else if (shardColor == Color.blue)
            {
                GameManager.instance.IncreaseColor(GameManager.Option.Blue);
            }
			Destroy(gameObject);
        }
    }
}
