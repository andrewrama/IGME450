using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject spawnedObject;
    public Vector2 screenBounds;

    // timer variables
    public float spawnRate = 2;
    public float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        // location will be random in between both edges of the screen
        // get bounds of screen, clamp spawn position to x*2 and x*6
        // random number of boxes will be spawned each time: either 2 or 3

        if (timer < spawnRate)
        {

            timer += Time.deltaTime;

        }
        else
        {
            Instantiate(spawnedObject, new Vector3(Random.Range(screenBounds.x * 2, screenBounds.x * 6), transform.position.y, -62.8f), transform.rotation);
            //Instantiate(spawnedObject, new Vector3(Random.Range(10, transform.position.x+10), transform.position.y, 0), transform.rotation);
            timer = 0;
        }

    }
}
