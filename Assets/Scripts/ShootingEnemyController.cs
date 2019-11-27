using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyController : MonoBehaviour
{
    public GameObject projectilePrefab;
    private float attackDelay = 0.9f;
    private float timeToNextAttack;

    public enum Facing { LEFT, RIGHT };
    public float health;

    protected SpriteRenderer spriteRenderer;
    public Facing facing;

    protected GameObject player;
    protected Transform playerTransform;
    protected Transform selfTransform;
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
                Instantiate(GameAssets.i.TwoApples, transform.position, Quaternion.Euler(0, 0, 0));
            else
                Instantiate(GameAssets.i.TwoApples, transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(gameObject);
        }
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        facing = Facing.LEFT;
        playerTransform = player.GetComponent<Transform>();
        selfTransform = GetComponent<Transform>();
        health = 60.0f;
        timeToNextAttack = .4f;
    }

    void Update()
    {
        timeToNextAttack -= Time.deltaTime;
        if (timeToNextAttack <= 0.0f)
        {
            var blast = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, 0));
            timeToNextAttack = attackDelay;
        }
        AIUpdate();
    }

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
            if (facing == Facing.RIGHT) FlipDirection();
        }
        else if (!playerIsOnLeft)
        {
            if (facing == Facing.LEFT) FlipDirection();
        }
    }
}