using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour {


    ObjectPooler objectPooler;
    private float spawnX = 0f;
    private Transform playerTransform;
    public float tileLength = 6.38f;
    public GameObject menuScreenTiles;
    public int initialTilesAmount = 5;
    bool tilesInitialized = false;
    bool canSpawnSpike = false;

    void Start () {
        objectPooler = ObjectPooler.instance;
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        spawnX = playerTransform.position.x;
    }

    public void InitializeTiles()
    {
        for (int i = 0; i < initialTilesAmount; i++)
        {
            objectPooler.SpawnFromPool("tile", new Vector3(spawnX, -3.72f, 0f), Quaternion.identity);
            spawnX += tileLength;
        }
    }


    public void DynamicSpawner()
    {
        if (playerTransform != null)
        {
            if (playerTransform.position.x > spawnX - (tileLength * 5))
            {
                int randomNumber = Random.Range(1,11);
                if(randomNumber > 6 && canSpawnSpike)
                {
                    //SpawnSpike
                    objectPooler.SpawnFromPool("spike", new Vector3(spawnX, -3.72f, 0f), Quaternion.identity);
                    canSpawnSpike = false;
                    spawnX += tileLength;
                }
                else
                {
                    //SpawnTile
                    objectPooler.SpawnFromPool("tile", new Vector3(spawnX, -3.72f, 0f), Quaternion.identity);
                    canSpawnSpike = true;
                    float obstacleSpawnX = Random.Range((spawnX - tileLength / 2) + 1, (spawnX + tileLength / 2) - 1);
                    if (randomNumber >= 1 && randomNumber <= 2)
                    {
                        objectPooler.SpawnFromPool("saw", new Vector3(obstacleSpawnX, -2.12f, 0f), Quaternion.identity);
                    }
                    else if(randomNumber == 6)
                    {
                        objectPooler.SpawnFromPool("swing", new Vector3(obstacleSpawnX, -2.12f, 0f), Quaternion.identity);
                    }
                    spawnX += tileLength;
                }
            }
        }
    }


	void Update () {
        if (GameManager.gameStarted && !tilesInitialized)
        {
            Debug.Log("Initialized Tiles");
            Destroy(menuScreenTiles);
            InitializeTiles();
            tilesInitialized = true;
        }
        if (tilesInitialized)
        {
            DynamicSpawner();
        }
    }
}
