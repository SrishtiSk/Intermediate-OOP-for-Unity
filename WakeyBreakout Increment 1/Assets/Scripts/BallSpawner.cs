using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefabBall;

    // spawn support
    Vector2 spawnloaction = new Vector2(0, -1.5f);
    Timer spawnTimer;
    float spawnRange;

    //collision free support
    bool retrySpawn = false;
    Vector2 spawnLocationMin;
    Vector2 spawnLocationMax;


    // Start is called before the first frame update
    void Start()
    {
        // Spawn and destroy ball to calculate spawn location min and max
        GameObject tempBall = Instantiate<GameObject>(prefabBall);
        BoxCollider2D collider = tempBall.GetComponent<BoxCollider2D>();
        float ballColliderHalfWidth = collider.size.x / 2;
        float ballColliderHalfHeight = collider.size.y / 2;
        spawnLocationMin = new Vector2(
            tempBall.transform.position.x - ballColliderHalfWidth,
            tempBall.transform.position.y - ballColliderHalfHeight);
        spawnLocationMax = new Vector2(
            tempBall.transform.position.x + ballColliderHalfWidth,
            tempBall.transform.position.y + ballColliderHalfHeight);

        Destroy(tempBall);

        //initialize and start spawn timer
        spawnRange = ConfigurationUtils.MaxSpawnSeconds - 
            ConfigurationUtils.MinSpawnSeconds;
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = GetSpawnDelay();
        spawnTimer.Run();

        //Spawn 1st Ball in the game
        SpawnBall();
    }

    // Update is called once per frame
    void Update()
    {
        //spawn ball and restart timer as appropriate
        if (spawnTimer.Finished)
        {
            //dont stack with a spawn still pending
            retrySpawn = false;
            SpawnBall();
            spawnTimer.Duration = GetSpawnDelay();
            spawnTimer.Run();
        }
        //try again if spawn still pending
        if (retrySpawn)
            SpawnBall();

    }

    public void SpawnBall()
    {
        if (Physics2D.OverlapArea(spawnLocationMin, spawnLocationMax) == null)
        {
            retrySpawn = false;
            Instantiate(prefabBall);
        }
        else
            retrySpawn = true;
    }

    float GetSpawnDelay()
    {
        return ConfigurationUtils.MinSpawnSeconds +
            Random.value * spawnRange;
    }
}
