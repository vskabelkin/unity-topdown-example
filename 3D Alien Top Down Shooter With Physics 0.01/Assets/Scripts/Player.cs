using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public float moveSpeed;
    Cursor cursor;
    Shot shot;
    public Transform gunBarrel;
    Vector3 dir = Vector3.zero;
    Animator animator;
    // Rigidbody playerRigidbody;
    float camRayLength = 100f;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        cursor = FindObjectOfType<Cursor>();
        navMeshAgent.updateRotation = false;
        shot = FindObjectOfType<Shot>();
        // playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
            dir.x = -1.0f;
        if (Input.GetKey(KeyCode.D))
            dir.x = 1.0f;
        if (Input.GetKey(KeyCode.W))
            dir.z = 1.0f;
        if (Input.GetKey(KeyCode.S))
            dir.z = -1.0f;
        navMeshAgent.velocity = dir.normalized * moveSpeed;

        if(Input.GetMouseButtonDown(0)) {
            var from = gunBarrel.position;
            var target = cursor.transform.position;
            var to = new Vector3(target.x, from.y, target.z);

            var direction = (to - from).normalized;

            RaycastHit hit;
            if (Physics.Raycast(from, to - from, out hit, camRayLength))
            {

                if (hit.transform != null)
                {
                    var enemy = hit.transform.GetComponent<Enemy>();
                    if (enemy != null)
                        enemy.Kill();
                }

                to = new Vector3(hit.point.x, from.y, hit.point.z);
            }
            else
                to = from + direction * camRayLength;

            shot.Show(from, to);
        }

        Vector3 forward = cursor.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(forward.x, 0, forward.z));

    }
}