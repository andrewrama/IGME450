using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatInventory : MonoBehaviour
{
    public static CatInventory Instance;

    public List<Cat> ownedCats = new List<Cat>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);


    }

    public void Start()
    {
        if(CatInventory.Instance != null)
        {
            SetInventory(CatInventory.Instance.ownedCats);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            CatInventory.Instance.ownedCats.Clear();
        }
    }

    public void SetInventory(List<Cat> inventory)
    {
        CatInventory.Instance.ownedCats = inventory;
    }

    // Checks if cat is already owned and then adds to inventory if new
    public void AddCatToList(Cat newCat)
    {
        bool unowned = true;

        for(int i=0; i<ownedCats.Count; i++)
        {
            if(CatInventory.Instance.ownedCats[i].catName == newCat.catName)
            {
                unowned = false;
            }
        }

        if (unowned)
        {
            CatInventory.Instance.ownedCats.Add(newCat);
        }
    }
}
