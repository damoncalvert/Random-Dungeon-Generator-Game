using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartDrop : MonoBehaviour
{
    private GameObject player;
    public int amountOfHearts;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().health += 1*amountOfHearts;
            if (player.GetComponent<PlayerController>().health > 5)
                player.GetComponent<PlayerController>().health = 5;
            Destroy(gameObject);
        }
    }
}
