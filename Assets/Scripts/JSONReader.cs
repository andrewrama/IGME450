using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class JSONReader : MonoBehaviour
{
    public TextAsset textJSON;

    //all of the cats in the game
    public List<Cat> catPool;

    //all of the cats the player owns
    public List<Cat> ownedCats;

    [System.Serializable]
    public class JsonCat
    {
        public string name;
        public string imgPath;
        public string rarity;
    }

    [System.Serializable]
    public class JsonData
    {
        public int currency;
        public List<JsonCat> allCats;
        public List<JsonCat> ownedCats;

        public void PrintAllCatsCount()
        {
            Debug.Log(allCats.Count);
        }

        public void PrintOwnedCatsCount()
        {
            Debug.Log(ownedCats.Count);
        }

        public void PrintCurrency()
        {
            Debug.Log(currency);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(textJSON.text);
        JsonData jsonCats = JsonUtility.FromJson<JsonData>(textJSON.text);

        catPool = new List<Cat>();
        ownedCats = new List<Cat>();

        jsonCats.PrintCurrency();
        jsonCats.PrintAllCatsCount();
        jsonCats.PrintOwnedCatsCount();

        //get all the possible cats
        for (int i = 0; i < jsonCats.allCats.Count; i++)
        {
            Sprite image = LoadAllCatImage(i, jsonCats);
            JsonCat a = jsonCats.allCats[i];

            Cat allCat = new Cat(image, a.name, a.rarity);
            catPool.Add(allCat);

        }

        //get a list of all the owned cats
        for (int i = 0; i < jsonCats.ownedCats.Count; i++)
        {
            JsonCat o = jsonCats.ownedCats[i];
            Sprite image = LoaOwnedCatImage(i, jsonCats);

            Cat ownedCat = new Cat(image, o.name, o.rarity);
            ownedCats.Add(ownedCat);
        }
    }

    /// <summary>
    /// Helper method in order to get the images for the cats
    /// </summary>
    /// <param name="index"></param>
    /// <param name="catPool"></param>
    /// <returns></returns>
    private Sprite LoadAllCatImage(int index, JsonData catPool)
    {
        string imageURL = Application.dataPath + catPool.allCats[index].imgPath;

        return (Sprite)AssetDatabase.LoadAssetAtPath(imageURL, typeof(Sprite));
    }

    private Sprite LoaOwnedCatImage(int index, JsonData catPool)
    {
        string imageURL = Application.dataPath + catPool.ownedCats[index].imgPath;

        return (Sprite)AssetDatabase.LoadAssetAtPath(imageURL, typeof(Sprite));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
