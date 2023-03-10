using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject spawnedObject;
    private Vector2 screenBounds;
    public LogicScript logic;

    float xPos;

    // timer variables
    public float spawnRate;
    public float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

    }

    // Update is called once per frame
    void Update()
    {
        // object will spawn in one of three places:
        //  center
        //  leftEdge
        //  rightEdge

        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            xPos = logic.posOptions[Random.Range(0, logic.posOptions.Length)];
            if (spawnedObject.CompareTag("boxObstacle"))
            {
                for (int i = 0; i < Random.Range(1,3); i++)
                {
                    Instantiate(spawnedObject, new Vector3(xPos, transform.position.y, 0), transform.rotation);
                    xPos = logic.posOptions[Random.Range(0, logic.posOptions.Length)];
                }
            }
            else
            {
                Instantiate(spawnedObject, new Vector3(xPos, transform.position.y, 0), transform.rotation);
            }
            timer = 0;
        }

    }
}
