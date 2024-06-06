using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    public GameObject jellyFish;
    public GameObject obstacle;
    public GameObject shark;
    public float maxTime;
    float timer;
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    float randomX;
    float randomY;

    // Start is called before the first frame update
    void Start()
    {
        //InstantiateObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameOver == false && GameManager.gameStarted == true)
        {
            timer += Time.deltaTime;
            if (timer >= maxTime)
            {
                randomX = Random.Range(minX, maxX);
                randomY = Random.Range(minY, maxY);
                InstantiateObstacle();
                timer = 0;
            }
        }
        
    }
    public void InstantiateObstacle ()
    {
        GameObject newObstacle = Instantiate(obstacle);
        newObstacle.transform.position = new Vector2(transform.position.x, randomY);

        float randomValue = Random.value;

        GameObject newObstacle1;

        if (randomValue < 0.5f)
        {
            newObstacle1 = Instantiate(jellyFish);
        }
        else
        {
            newObstacle1 = Instantiate(shark);
        }

        newObstacle1.transform.position = new Vector2(randomX, randomY);
    }


}

