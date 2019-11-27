using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBossController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject boss;
    private RoomSpawner spawnSide;
    private Transform bossTransform;
    void Start()
    {
        //spawns boss and then move his location based on what type 
        //of room he is in so that he is not right in the middle of the room;
        boss = GameObject.FindGameObjectWithTag("Boss");
        boss.SetActive(false);
        spawnSide = GetComponentInChildren<RoomSpawner>();
        bossTransform = boss.GetComponent<Transform>();
        if (spawnSide.openingDirection == 1)
            bossTransform.position += new Vector3(0.0f, -3.0f, 0.0f);
        if (spawnSide.openingDirection == 2)
            bossTransform.position += new Vector3(0.0f, 3.0f, 0.0f);
        if (spawnSide.openingDirection == 3)
            bossTransform.position += new Vector3(-3.0f, 0.0f, 0.0f);
        if (spawnSide.openingDirection == 4)
            bossTransform.position += new Vector3(3.0f, 0.0f, 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            boss.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
