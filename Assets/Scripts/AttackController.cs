using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private Vector3 mousePosition;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float moveSpeed = 7.0f;
    public Probabilities probabilities;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        moveDirection = (mousePosition - transform.position).normalized;
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        probabilities = GetComponent<Probabilities>();
    }

    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
            float damage = Probabilities.RandomNormalVariable(10f, 3f, 5, 20);
            DamagePopup.Create(transform.position, Convert.ToInt32(damage));
            if (collision.GetComponent<WalkMonsterController>())
                collision.GetComponent<WalkMonsterController>().health -= damage;
            if (collision.GetComponent<ShootingEnemyController>())
                collision.GetComponent<ShootingEnemyController>().health -= damage;
            if (collision.GetComponent<BossController>())
                collision.GetComponent<BossController>().health -= damage;

            Destroy(gameObject);
        }
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
