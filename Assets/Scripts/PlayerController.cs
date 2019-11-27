using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/*
 * Controls:
 * Left/Right arrows or A/D to move
 * Up arrow or W to jump
 * left mouse button to attack, right button to block (shield)
 * z to change scenes
*/

public class PlayerController : MonoBehaviour
{
    public enum Facing { LEFT, RIGHT };

    public bool invincible;

    public GameObject AttackPrefab;
    public SpriteRenderer shield;

    public float speed = 0.08f;
    public int health;

    //Hidden publics
    [HideInInspector]
    public Facing facing;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;

    public float attackDelay = 0.3f;
    private float timeToNextAttack;

    public Sprite[] frameArray;
    private int currentFrame;
    private float framerate = 0.4f;
    private float timer;

    private float timeToNextLavaDamage;
    private float lavaDelay = 1.5f;


    //Collision detection
    //Used for handling interactions with collider 2D objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && shield.enabled == false && invincible == false)
        {
            health--;
        }

        if (health <= 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            StartCoroutine(StartCountdown());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lava") && timeToNextLavaDamage <= 0 && shield.enabled == false && invincible == false)
        {
            health--;
            timeToNextLavaDamage = lavaDelay;
        }
        if ((collision.CompareTag("Enemy") || collision.CompareTag("Projectile") && shield.enabled == false && invincible == false))
        {
            health--;
        }
        if (health <= 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            StartCoroutine(StartCountdown());
        }
    }

    public IEnumerator StartCountdown()
    {
        speed = 0.0f;
        yield return new WaitForSecondsRealtime(1.5f);
        var currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync(currentSceneName);
    }

    //Initialization
    void Start()
    {
        facing = Facing.RIGHT;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        health = 5;
        invincible = false;
    }

    //Graphics & instant update, comes from demo code, pther than last line
    void Update()
    {
        if (facing == Facing.LEFT)
        {
            spriteRenderer.flipX = true;
        }
        else if (facing == Facing.RIGHT)
        {
            spriteRenderer.flipX = false;
        }
        if (health > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void walkAnimation()
    {
        if (timer >= framerate)
        {
            timer -= framerate;
            if (currentFrame == 0 || currentFrame == 1 || currentFrame == 3 || currentFrame == 4)
            {
                currentFrame = 2;
                gameObject.GetComponent<SpriteRenderer>().sprite = frameArray[currentFrame];
            }
            else
            {
                currentFrame = 3;
                gameObject.GetComponent<SpriteRenderer>().sprite = frameArray[currentFrame];
            }

        }
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        timeToNextLavaDamage -= Time.deltaTime;
        //Walking
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
        {
            var displacement = Vector3.right * speed;
            transform.position += displacement;
            facing = Facing.RIGHT;
            walkAnimation();
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            var displacement = Vector3.left * speed;
            transform.position += displacement;
            facing = Facing.LEFT;
            walkAnimation();
        }

        //Jumping
        if (((Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.W))))
        {
            transform.position += Vector3.up * speed;
            currentFrame = 0;
            gameObject.GetComponent<SpriteRenderer>().sprite = frameArray[currentFrame];
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            var displacement = Vector3.down * speed;
            transform.position += displacement;
        }

        timeToNextAttack -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && timeToNextAttack <= 0.0f)
        {
            var bullet = Instantiate(AttackPrefab, transform.position, Quaternion.Euler(0, 0, 0));
            timeToNextAttack = attackDelay;
        }

        if (!Input.anyKey)
        {
            currentFrame = 1;
            gameObject.GetComponent<SpriteRenderer>().sprite = frameArray[currentFrame];
        }

    }
}
