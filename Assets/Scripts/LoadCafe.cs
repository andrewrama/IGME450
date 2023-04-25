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

    private GameObject floor;

    //how many seconds the cat can stay "stuck" unti it finds another place to walk
    private int stayStillThreshold = 5;

    //where the cat was 
    private Vector3 oldPosition;

    //the radius of checking if the cat is stuck or not
    private float radius = 5;


    //TODO Fix the bounds so the cat can't go out of view of the camera
    //ToDo Have the cats wait a few seconds before they move to another location

    // Start is called before the first frame update
    void Start()
    {

        //get the floor 

        //int rowCounter = 0;
        //float x = -85.0f;
        //float y = 71.0f;
        //float z = 35.0f;


        floor = GameObject.Find("CafeFloor");

        for (int i=0; i< saveData.ownedCats.Count; i++)
        {
            GameObject newCat = Instantiate(catPrefab);
            //newCat.GetComponent<MeshRenderer>().material = saveData.ownedCats[i].material;
            //newCat.transform.position = new Vector3(x,y,z);
            //newCat.transform.Rotate(newCat.transform.forward, 270f);
            newCat.transform.position = new Vector3(newCat.transform.position.x, 0, newCat.transform.position.z);

            NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();
            int vertexIndex = Random.Range(0, triangulation.vertices.Length);

            NavMeshHit hit;
            
            if(NavMesh.SamplePosition(triangulation.vertices[vertexIndex], out hit, 2f, 1))
            {
                newCat.GetComponent<NavMeshAgent>().Warp(new Vector3(hit.position.x, hit.position.y + 12, hit.position.z));
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

    private IEnumerator WaitSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    private void Move(GameObject cat)
    {
        //if the cat has reaced its destination, find a new destination
        NavMeshAgent navMesh = cat.GetComponent<NavMeshAgent>();

        bool atDestination = cat.transform.position.x == navMesh.destination.x && cat.transform.position.z == navMesh.destination.z;



        if (atDestination)
        {
            StartCoroutine(WaitSeconds(15));
            GetNewDestination(cat);
        }

        else
        { 
            
        }
    }

    /// <summary>
    /// Used to tell the cat where to go
    /// </summary>
    private void GetNewDestination(GameObject cat)
    {

        //Renderer renderScript = floor.GetComponent<Renderer>();
        //NavMeshAgent navMesh = cat.GetComponent<NavMeshAgent>();
        //
        //Vector3 floorMinBounds = renderScript.bounds.min;
        //Vector3 floorMaxBounds = renderScript.bounds.max;
        //
        //float x = Random.Range(floorMinBounds.x, floorMaxBounds.x);
        //float z = Random.Range(floorMinBounds.z, floorMaxBounds.z);
        //
        //navMesh.SetDestination(new Vector3(x, 0, z));

        NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();
        int vertexIndex = Random.Range(0, triangulation.vertices.Length);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(triangulation.vertices[vertexIndex], out hit, 2f, 1))
        {
            cat.GetComponent<NavMeshAgent>().SetDestination(new Vector3(hit.position.x, hit.position.y + 12, hit.position.z));
            cat.GetComponent<NavMeshAgent>().enabled = true;
        }

        //Debug.Log($"Destnation: {navMesh.destination}");
    }

}
