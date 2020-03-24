using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    Player player;
    Vector3 distance;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;
    public bool LookAtPlayer = false;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        distance = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newpos = player.transform.position + distance;
        transform.position = Vector3.Slerp(transform.position, newpos, SmoothFactor);

        if (LookAtPlayer)
            transform.LookAt(player.transform);


    }
}
