using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public float jumpforce;

    public Text score;

    public Text lives;

    private int scoreValue = 0;

    private int livesValue = 3;

    private int levelCounter = 0;

    private int musicCounter = 0;

    public AudioSource musicSource, musicSource2;
    
    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;
    Animator anim;


    public GameObject winTextObject, deathTextObject, level2, player;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        score.text = scoreValue.ToString();

        lives.text = livesValue.ToString();        
        winTextObject.SetActive(false);
        deathTextObject.SetActive(false);

    }

    void Update()
    {
        if (scoreValue >= 8)
        {
            winTextObject.SetActive(true);
            musicSource.Stop();
            player.SetActive(false);
        if (musicCounter == 0)
            {
                musicSource2.Play();
                musicCounter += 1;
            }
        }
        if (livesValue <= 0)
        {
            deathTextObject.SetActive(true);
            player.SetActive(false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);

        }
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);

        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            anim.SetInteger("State", 2);
        }
                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            anim.SetInteger("State", 2);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        if (Input.GetAxis("Horizontal") > 0.01f)
            {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        else if (Input.GetAxis("Horizontal") < -0.01f)
            {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (scoreValue == 4)
        {
            if (levelCounter == 0)
        {
            player.transform.position = level2.transform.position;
            livesValue = 3;
            lives.text = livesValue.ToString();
            levelCounter += 1;
        }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }
}
