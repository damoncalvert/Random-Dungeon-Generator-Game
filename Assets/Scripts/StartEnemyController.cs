using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEnemyController : MonoBehaviour
{
    public GameObject[] enemies;
    private bool spawned;
    void Start()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SetActive(false);
        }
        spawned = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !spawned)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null)
                    enemies[i].SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null)
                    enemies[i].SetActive(false);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
