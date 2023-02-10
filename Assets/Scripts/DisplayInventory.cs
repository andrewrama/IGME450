using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayInventory : MonoBehaviour
{
    public TMPro.TextMeshProUGUI catNameText;

    // Start is called before the first frame update
    void Start()
    {
        string nameString = "";

        for (int i = 0; i < CatInventory.Instance.ownedCats.Count; i++)
        {
            nameString += CatInventory.Instance.ownedCats[i].catName + "\n";
        }

        catNameText.text = nameString;
    }

}
