using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class LoadCafe : MonoBehaviour
{
    public GameObject catPrefab;
    [SerializeField] private SaveDataScriptableObject saveData;
    private List<GameObject> cats = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        int rowCounter = 0;
        float x = -85.0f;
        float y = 71.0f;
        float z = 35.0f;

        for (int i=0; i< saveData.ownedCats.Count; i++)
        {
            GameObject newCat = Instantiate(catPrefab);
            //newCat.GetComponent<MeshRenderer>().material = saveData.ownedCats[i].material;
            //newCat.transform.position = new Vector3(x,y,z);
            newCat.transform.Rotate(newCat.transform.forward, 270f);
            newCat.transform.position = new Vector3(newCat.transform.position.x, 0, newCat.transform.position.z);

            NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();
            int vertexIndex = Random.Range(0, triangulation.vertices.Length);

            NavMeshHit hit;
            if(NavMesh.SamplePosition(triangulation.vertices[vertexIndex], out hit, 2f, 1))
            {
                newCat.GetComponent<NavMeshAgent>().Warp(new Vector3(hit.position.x, -50, hit.position.z));
                newCat.GetComponent<NavMeshAgent>().enabled = true;
            }

            //rowCounter++;
            //if(rowCounter > 5)
            //{
            //    rowCounter = 0;
            //    x = -85.0f;
            //    z -= 60;
            //}
            //else
            //{
            //    x += 35;
            //}
            cats.Add(newCat);
        }
    }

    private void Update()
    {
        foreach(GameObject cat in cats)
        {
            
            Move(cat);
        }
    }


    private void Move(GameObject cat)
    {
        cat.GetComponent<NavMeshAgent>().SetDestination(new Vector3(0, 0, 0));
    }

}
