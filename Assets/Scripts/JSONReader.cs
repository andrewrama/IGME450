using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class JSONReader : MonoBehaviour
{
    public TextAsset textJSON;

    //all of the cats in the game
    public List<global::Cat> catPool;

    //all of the cats the player owns
    public List<global::Cat> ownedCats;

    [System.Serializable]
    public class Cat
    {
        public string name;
        public string imgPath;
        public string rarity;
    }

    [System.Serializable]
    public class JsonCatList
    {
        public List<Cat> allCats;

        public void PrintCatCount()
        {
            Debug.Log(allCats.Count);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(textJSON.text);
        JsonCatList jsonCats = JsonUtility.FromJson<JsonCatList>(textJSON.text);

        jsonCats.PrintCatCount();

        List<global::Cat> catCollection = new List<global::Cat>();

        for (int i = 0; i < jsonCats.allCats.Count; i++)
        {
            Sprite image = LoadImage(i, jsonCats);
            Cat c = jsonCats.allCats[i];

            global::Cat cat = new global::Cat(image, c.name, c.rarity);
            cat.Print();
            catCollection.Add(cat);
        }
    }

    private Sprite LoadImage(int index, JsonCatList catPool)
    {
        string imageURL = Application.dataPath + catPool.allCats[index].imgPath;

        return (Sprite)AssetDatabase.LoadAssetAtPath(imageURL, typeof(Sprite));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
