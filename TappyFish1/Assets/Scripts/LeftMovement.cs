using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMovement : MonoBehaviour
{
    public float speed;
    BoxCollider2D box;
    
    float groundWidth;
    float obstacleWidth;
    float jellyFishWidth;
    float sharkWidth;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("Ground"))
        {
            box = GetComponent<BoxCollider2D>();
            groundWidth = box.size.x;
        }
        else if (gameObject.CompareTag("Obstacle")) 
        {
            obstacleWidth = GameObject.FindGameObjectWithTag("Column").GetComponent<BoxCollider2D>().size.x;

        }
        else if (gameObject.CompareTag("JellyFish"))
        {
            jellyFishWidth = GameObject.FindGameObjectWithTag("JellyFish").GetComponent<BoxCollider2D>().size.x;
        }
        else if (gameObject.CompareTag("Shark"))
        {
            sharkWidth = GameObject.FindGameObjectWithTag("Shark").GetComponent<BoxCollider2D>().size.x;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameOver == false)
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);

        }
        if (gameObject.CompareTag("Ground"))
        {
            if (transform.position.x <= -groundWidth)
            {
                transform.position = new Vector2(transform.position.x + 2 * groundWidth, transform.position.y);
            }
        }
        else if (gameObject.CompareTag("Obstacle")) {

            if (transform.position.x < GameManager.bottomLeft.x - obstacleWidth)
            {
                Destroy(gameObject);
            }
        }
        else if (gameObject.CompareTag("JellyFish"))
        {
            if (transform.position.x < GameManager.bottomLeft.x - jellyFishWidth)
            {
                Destroy(gameObject);
            }
        }

        else if (gameObject.CompareTag("Shark"))
        {
            if (transform.position.x < GameManager.bottomLeft.x - sharkWidth)
            {
                Destroy(gameObject);
            }
        }

        
    }
}
