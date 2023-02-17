using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveSpeed = 50;
    public Vector2 screenBounds;


    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3.down * moveSpeed) * Time.deltaTime;

        // destroying when offscreen
        if (transform.position.y < -(screenBounds.y*2))
        {
            Destroy(gameObject);
        }
    }
}
