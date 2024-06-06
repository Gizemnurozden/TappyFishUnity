using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField]

    private float _speed;

    int angle;
    int maxAngle = 20;
    int minAngle = -60;

    public Score score;
    bool touchedGround;
    public GameManager gameManager;
    public Sprite fishDied;
    SpriteRenderer sp;
    Animator anim;
    public ObstacleSpawner obstaclespawner;
    [SerializeField] private AudioSource swim, hit, point;
   

    // Start is called before the first frame update
    void Start()
    {
        swim.Play();
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;

        //_rb.gravityScale = -1;
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FishSwim();
        
    }

    private void FixedUpdate()  //balığın hareketini kontrol eden fizik olaylarını daha gerçekçi ve farklı model telefonlarda aynı hız şeklinde kontrol edilmesini sağlar.
        //fizik kontrolleri genellikle fixedupdate altında çağrılır.
    {
        //FishRotation();
    }


    void FishSwim ()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.gameOver == false) 
        {
           
            if (GameManager.gameStarted == false)
            {
                _rb.gravityScale = 4f;
                _rb.velocity = Vector2.zero;
                _rb.velocity = new Vector2(_rb.velocity.x, _speed);
                obstaclespawner.InstantiateObstacle();
              
                gameManager.GameHasStarted();

            }
            else
            {
                _rb.velocity = Vector2.zero; //yerçekimini sıfırlar.
                _rb.velocity = new Vector2(_rb.velocity.x, _speed); // yatayda sabit tutarak dikey de speed değeri kadar hareket eder.
            }
           
        }
    }

    void FishRotation() //balığın hareket açılarını oluşturuyoruz.
    {
        if (_rb.velocity.y > 0)
        {
            if (angle <= maxAngle)
            {
                angle = angle + 4;
            }
        }
        else if (_rb.velocity.y < -1.2)
        {
            if (angle > minAngle)
            {
                angle = angle - 2;
            }
        }
        
        if (touchedGround == false) //yere temasının kontrolü
        {
            transform.rotation = Quaternion.Euler(0, 0, angle); //z ekseninde açısını tanımlıyoruz.

        }
    }


    private void OnTriggerEnter2D(Collider2D collision) //çarpışma üzerinden ne ile çarpışacağımızı kontrol edeceğiz.
    {
        if (collision.CompareTag("Obstacle"))
        {
           // Debug.Log("Scored!..");
            score.Scored();
            point.Play();
      
          

        }
        else if (collision.CompareTag("Column") && GameManager.gameOver == false)
        {
            //game over
            FishDieEffect();
            gameManager.GameOver();
        
        }
        else if (collision.gameObject.CompareTag("JellyFish") && GameManager.gameOver == false)
        {
            //game over
            FishDieEffect();
            gameManager.GameOver();
        }
        else if (collision.gameObject.CompareTag("Shark") && GameManager.gameOver == false )
        {
            //game over
            FishDieEffect();
            gameManager.GameOver();
        }


    }
    
    void FishDieEffect()
    {
        hit.Play();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (GameManager.gameOver == false)
            {
                //game over
                FishDieEffect();
                gameManager.GameOver();
                GameOver();
              
            }
            
          
            else
            {
                //game over (fish)
                GameOver();
            }
        }
       
    }
    void GameOver()
    {
        touchedGround = true;
        transform.rotation = Quaternion.Euler(0, 0, -90);
        sp.sprite = fishDied;
        anim.enabled = false;
    }

}
