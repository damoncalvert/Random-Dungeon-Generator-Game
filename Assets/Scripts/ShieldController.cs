using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    private float timeToNextUse;
    private float timeToDissapear;

    public float useDelay = 7.0f;
    private float disappearDelay = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        timeToNextUse = 0.0f;
        timeToDissapear = 0.0f;
    }

    void Update()
    {
        //Time update, most of this comes from demo code
        timeToNextUse -= Time.deltaTime;
        timeToDissapear -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (gameObject.activeSelf == true && timeToDissapear <= 0.0f)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
        }

        if (Input.GetMouseButtonDown(1) && timeToNextUse <= 0.0f)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = true;
            timeToNextUse = useDelay;
            timeToDissapear = disappearDelay;
        }
    }
}
