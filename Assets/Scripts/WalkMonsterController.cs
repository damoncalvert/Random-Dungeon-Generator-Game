using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WalkMonsterController : MonoBehaviour
{
    public enum Facing { LEFT, RIGHT };
    public float health;

    //Sprite manipulation
    protected SpriteRenderer spriteRenderer;
    public Facing facing;

    protected GameObject player;
    protected Transform playerTransform;
    protected Transform selfTransform;

    //Misc.
    private float speed;
    Scene currentScene;
    private int sceneNum;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (health <= 0)
        {
            float probability = Probabilities.RandomExponentialVairable(1.0f);
            print(probability);
            if (probability < 1.2f)
                Instantiate(GameAssets.i.OneApple, transform.position, Quaternion.Euler(0, 0, 0));
            else if (probability < 2.2f)
                Instantiate(GameAssets.i.TwoApples, transform.position, Quaternion.Euler(0, 0, 0));
            else if (probability < 3.0f)
                Instantiate(GameAssets.i.ThreeApples, transform.position, Quaternion.Euler(0, 0, 0));
            else
                Instantiate(GameAssets.i.FourApples, transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (health <= 0)
        {
            float probability = Probabilities.RandomExponentialVairable(1.0f);
            print(probability);
            if (probability < 1.2f)
                Instantiate(GameAssets.i.OneApple, transform.position, Quaternion.Euler(0, 0, 0));
            else if (probability < 2.2f)
                Instantiate(GameAssets.i.TwoApples, transform.position, Quaternion.Euler(0, 0, 0));
            else if (probability < 3.0f)
                Instantiate(GameAssets.i.ThreeApples, transform.position, Quaternion.Euler(0, 0, 0));
            else
                Instantiate(GameAssets.i.FourApples, transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(gameObject);
        }
    }
    virtual protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        facing = Facing.LEFT;
        playerTransform = player.GetComponent<Transform>();
        selfTransform = GetComponent<Transform>();
        health = 60.0f;
        speed = 0.03f;
    }

    virtual protected void FixedUpdate()
    {
        AIUpdate();
    }

    //Flips the direction of the sprite and updates the Facing variable, comes from demo code
    protected void FlipDirection()
    {
        var newDirection = (facing == Facing.LEFT) ? Facing.RIGHT : Facing.LEFT;
        var newFlipValue = (spriteRenderer.flipX) ? false : true;

        this.facing = newDirection;
        spriteRenderer.flipX = newFlipValue;
    }

    virtual protected void AIUpdate()
    {
        //-----
        //Sense
        //-----
        var playerPosition = playerTransform.position;
        var selfPosition = selfTransform.position;
        var playerIsOnLeft = (playerPosition.x < selfPosition.x);
        var playerIsAbove = (playerPosition.y > selfPosition.y);


        if (playerIsOnLeft)
        {
            MoveLeft();
        }
        else if (!playerIsOnLeft)
        {
            MoveRight();
        }

        if (playerIsAbove)
        {
            MoveUp();
        }
        else if (!playerIsAbove)
        {
            MoveDown();
        }
    }

    protected void MoveDown()
    {
        var offset = Vector3.down * speed;
        var newPosition = transform.position + offset;

        transform.position = newPosition;
    }
    protected void MoveUp()
    {
        var offset = Vector3.up * speed;
        var newPosition = transform.position + offset;

        transform.position = newPosition;
    }
    protected void MoveLeft()
    {
        var offset = Vector3.left * speed;
        var newPosition = transform.position + offset;

        if (facing == Facing.RIGHT) FlipDirection();
        transform.position = newPosition;
    }
    protected void MoveRight()
    {
        var offset = Vector3.right * speed;
        var newPosition = transform.position + offset;

        if (facing == Facing.LEFT) FlipDirection();
        transform.position = newPosition;
    }
}
