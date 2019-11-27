using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2AttackDrop : MonoBehaviour
{

    private GameObject player;
    public GameObject level2AttackPrefab;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().AttackPrefab = level2AttackPrefab;
            Destroy(gameObject);
        }
    }
}
