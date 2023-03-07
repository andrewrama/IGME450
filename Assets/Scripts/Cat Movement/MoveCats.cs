using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCats : MonoBehaviour
{
    public List<GameObject> cats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cats[0].transform.position = new Vector3(80 * Mathf.Sin(Time.realtimeSinceStartup/5), 0, -108);
    }
}
