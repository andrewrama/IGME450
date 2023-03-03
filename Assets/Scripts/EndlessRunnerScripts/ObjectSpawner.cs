using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject boxPrefab;
    public GameObject treatPrefab;
    public Vector2 screenBounds;
    public LogicScript logic;
    public float[] posOptions;

    float xPos;

    // timer variables
    public float boxSpawnRate = 1.5f;
    public float treatSpawnRate = 3f;
    public float timer = 0;
    public float boxTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        posOptions = new float[3] { logic.centerPoint, logic.leftEdge, logic.rightEdge };
        Debug.Log(posOptions[0]);
        Debug.Log(posOptions[1]);
        Debug.Log(posOptions[2]);
    }

    // Update is called once per frame
    void Update()
    {
        // object will spawn in one of three places:
        //  center
        //  leftEdge
        //  rightEdge

        xPos = posOptions[Random.Range(0, posOptions.Length)];

        if (boxTime < boxSpawnRate)
        {
            boxTime += Time.deltaTime;
        }
        else
        {
            Instantiate(boxPrefab, new Vector3(xPos, transform.position.y, 0), transform.rotation);
            Debug.Log("Spawn a box!");
            boxTime = 0;
        }

        if (timer < treatSpawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Instantiate(treatPrefab, new Vector3(xPos, transform.position.y, 0), transform.rotation);
            Debug.Log("Spawn a treat!");
            timer = 0;
        }

    }
}
