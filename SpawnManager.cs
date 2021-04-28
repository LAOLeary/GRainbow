using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject lazer;
    public GameObject alien;
    public GameObject banana;

    private float zSpawn = 7.0f;
    private float ySpawn = 1f;
    private float xSpawnRange = 10f;
    //private float LazerPosition = 0f;

    private float bananaSpawnTime = 1.0f;
    public float enemySpawnTime = 2.7f;
    private float lazerSpawnTime = 2.0f;
    private float startDelay = 0.0f;

    private GameObject player;
    private PlayerController playerController;
    


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", startDelay, enemySpawnTime);
        InvokeRepeating("SpawnLazer", startDelay, lazerSpawnTime); 
        InvokeRepeating("SpawnBanana", startDelay, bananaSpawnTime);

        //for endgame

        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void SpawnEnemy()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, zSpawn);

        if (playerController.isAlive == true)
        {
            Instantiate(enemy, spawnPos, enemy.gameObject.transform.rotation);
        }
        
    }


    void SpawnLazer()
    {
        if (playerController.isAlive == true)
        {
            Instantiate(lazer, alien.gameObject.transform.position, enemy.gameObject.transform.rotation);
        }
    }

    void SpawnBanana()
    {
        if (playerController.isAlive == true)
        {
            float randomXb = Random.Range(-9, 9);
            float randomYb = 1;
            float randomZb = Random.Range(-4, 4);

            Vector3 bspawnPos = new Vector3(randomXb, randomYb, randomZb);

            float randomX = Random.Range(-xSpawnRange, xSpawnRange);

            Vector3 spawnPos = new Vector3(randomX, ySpawn, zSpawn-1.5f);

            Instantiate(banana, bspawnPos, banana.gameObject.transform.rotation);
        }

    }





}
