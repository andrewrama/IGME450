using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCats : MonoBehaviour
{
    public List<GameObject> cats;

    private float zStart = 0;

    // Start is called before the first frame update
    void Start()
    {
        cats = new List<GameObject>();
        foreach (Cat cat in CatInventory.Instance.ownedCats)
        {
            cats.Add(cat.model);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject cat in cats)
        {
            cat.transform.position = new Vector3(80 * Mathf.Sin(Time.realtimeSinceStartup / 5), 0, -160 - 20 * Mathf.Cos(Time.realtimeSinceStartup / 5));
            zStart += 20;
        }
        
    }
}
