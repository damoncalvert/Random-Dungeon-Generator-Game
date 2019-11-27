using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueHeart : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer sr;
    private BoxCollider2D bc;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.GetComponent<PlayerController>().invincible = true;
        sr.enabled = false;
        bc.enabled = false;
        StartCoroutine(StartCountdown());
    }

    public IEnumerator StartCountdown()
    {
        yield return new WaitForSecondsRealtime(15f);
        player.GetComponent<PlayerController>().health = 5;
        player.GetComponent<PlayerController>().invincible = false;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
