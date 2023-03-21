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
    public class JsonCatList
    {
        public List<JsonCat> cats;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(textJSON.text);
        JsonCatList jsonCats = JsonUtility.FromJson<JsonCatList>(textJSON.text);

        List<Cat> catCollection = new List<Cat>();

        for (int i = 0; i < jsonCats.cats.Count; i++)
        {
            Sprite image = LoadImage(i, jsonCats);
            JsonCat c = jsonCats.cats[0];

            catCollection.Add(new Cat(image, c.name, c.rarity));
        }
    }

    private Sprite LoadImage(int index, JsonCatList catPool)
    {
        string imageURL = Application.dataPath + catPool.cats[index].imgPath;

        return (Sprite)AssetDatabase.LoadAssetAtPath(imageURL, typeof(Sprite));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
