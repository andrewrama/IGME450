using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("boxObstacle"))
        {
            transform.position = new Vector3(transform.position.x - 10, transform.position.y, 0);
        }
    }
}
