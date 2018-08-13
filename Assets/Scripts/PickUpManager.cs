using UnityEngine;

public class PickUpManager : MonoBehaviour {

    public GameObject[] pickups;
    private float spawnX = 5;
    private float spawnY;
    public float generateAfter = 5;
    public float generateRangeY = 3;
    public float distanceToSpawnFromPlayer = 20f;
    public float distanceToSpawn = 10f;
    private Transform playerTransform;
    ObjectPooler objectPooler;

    void SpawnPickupFromPool()
    {
        Vector2 spawnPos = new Vector2(spawnX,
            Random.Range(gameObject.transform.position.y, gameObject.transform.position.y + generateRangeY));

        objectPooler.SpawnFromPool("pickup", spawnPos, Quaternion.identity);
        spawnX += Random.Range(3, distanceToSpawnFromPlayer);
    }


    void PickupSpawner()
    {
        if (playerTransform != null)
        {
            if (playerTransform.position.x > spawnX - distanceToSpawn)
            {
                SpawnPickupFromPool();
            }
        }
    }



    void Start () {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        objectPooler = ObjectPooler.instance;
    }
	
	void Update () {
        
	}

    private void FixedUpdate()
    {
        if (GameManager.gameStarted)
        {
            PickupSpawner();
        }
    }


}
