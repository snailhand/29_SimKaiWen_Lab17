using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    public static GameManager gameManager;

    public GameObject coinObject;
    public int coinCount = 2;

    private float timer = 2f;
    void Start()
    {
        if(gameManager == null)
        {
            gameManager = this;
        }

    }

    // Update is called once per frame
    void Update()
    {
        SpawnCoin();
    }
    private void SpawnCoin()
    {
        timer -= Time.deltaTime;
        for (int i = 0; i < 100; i++)
        {
            Vector2 randomPos = new Vector2(Random.Range(-8.4f, 8.4f), Random.Range(-2.3f, 1.6f));
            if (timer <= 0 && coinCount <= 5)
            {
                Instantiate(coinObject, randomPos, Quaternion.identity);
                timer = 2f;
                coinCount++;
            }
        }
    }
}
