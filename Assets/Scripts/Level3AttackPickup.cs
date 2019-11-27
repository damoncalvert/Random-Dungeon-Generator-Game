using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3AttackPickup : MonoBehaviour
{
    private GameObject player;
    public GameObject level3AttackPrefab;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().AttackPrefab = level3AttackPrefab;
            Destroy(gameObject);
        }
    }
}
