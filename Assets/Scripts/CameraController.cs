using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    Transform playerTransform;
    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.x >= transform.position.x + 5.0f)
        {
            transform.position += new Vector3(10.0f, 0.0f, 0.0f);
        }
        if (playerTransform.position.x <= transform.position.x - 5.0f)
        {
            transform.position += new Vector3(-10.0f, 0.0f, 0.0f);
        }
        if (playerTransform.position.y >= transform.position.y + 5.0f)
        {
            transform.position += new Vector3(0.0f, 10.0f, 0.0f);
        }
        if (playerTransform.position.y <= transform.position.y - 5.0f)
        {
            transform.position += new Vector3(0.0f, -10.0f, 0.0f);
        }
    }
}
