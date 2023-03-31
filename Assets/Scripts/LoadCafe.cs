using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCafe : MonoBehaviour
{
    public GameObject catPrefab;
    [SerializeField] private SaveDataScriptableObject saveData;

    // Start is called before the first frame update
    void Start()
    {
        int rowCounter = 0;
        float x = -85.0f;
        float y = 71.0f;
        float z = 35.0f;
        for(int i=0; i<saveData.ownedCats.Count; i++)
        {
            GameObject newCat = Instantiate(catPrefab);
            //newCat.GetComponent<MeshRenderer>().material = saveData.ownedCats[i].material;
            newCat.transform.position = new Vector3(x,y,z);
            newCat.transform.Rotate(newCat.transform.up, 180f);

            rowCounter++;
            if(rowCounter > 5)
            {
                rowCounter = 0;
                x = -85.0f;
                z -= 30;
            }
            else
            {
                x += 35;
            }
        }
    }


}
