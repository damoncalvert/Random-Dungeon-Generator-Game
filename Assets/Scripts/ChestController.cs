using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public Sprite openedSprite;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = openedSprite;
            float probability = Probabilities.RandomExponentialVairable(.1f);
            print(probability);
            if (probability < 10f)
                Instantiate(GameAssets.i.Invincibility, transform.position + new Vector3(2,0,0), Quaternion.Euler(0, 0, 0));
            else if (probability < 15f)
                Instantiate(GameAssets.i.Level2Powerup, transform.position + new Vector3(2, 0, 0), Quaternion.Euler(0, 0, 0));
            else
                Instantiate(GameAssets.i.Level3Powerup, transform.position + new Vector3(2, 0, 0), Quaternion.Euler(0, 0, 0));
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
