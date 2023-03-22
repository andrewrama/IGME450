using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class JSONReader : MonoBehaviour
{
    public int Currency;

    public TextAsset textJSON;

    //all of the cats in the game
    public List<Cat> catPool;

    //all of the cats the player owns
    public List<Cat> ownedCats;

    public bool ShowBaristaTutorial;

    private JsonData json;

    [System.Serializable]
    public class JsonCat
    {
        public string name;
        public string imgPath;
        public string rarity;
        public bool showBaristaTuorial;

        public JsonCat(string name, string imgPath, string rarity)
        {
            this.name = name;
            this.imgPath = imgPath;
            this.rarity = rarity;
        }
    }

    [System.Serializable]
    public class JsonData
    {
        public int currency;
        public List<JsonCat> allCats;
        public List<JsonCat> ownedCats;
        public bool showBaristaTutorial;

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

        public void PrintShowBaristaTutorial()
        {
            Debug.Log(showBaristaTutorial);
        }
    }

    void Awake()
    {
        LoadData();

        DontDestroyOnLoad(this.gameObject);

    }

    public void AddCat(Cat cat)
    {
        ownedCats.Add(cat);
    }

    public void SaveData()
    {
        json.currency = Currency;

        //convert all cats
        json.allCats.Clear();

        json.allCats = catPool.Select(x => ConvertCatToJsonCat(x)).ToList();

        //convert owned cats
        json.ownedCats.Clear();

        json.ownedCats = ownedCats.Select(x => ConvertCatToJsonCat(x)).ToList();

        //convert currency
        json.currency = Currency;

        //show barista tutorial
        json.showBaristaTutorial = ShowBaristaTutorial;

        string s = JsonUtility.ToJson(json);
        File.WriteAllText(Application.dataPath + "/info.json", s);
    }

    private void LoadData()
    {
        Debug.Log(textJSON.text);
        json = JsonUtility.FromJson<JsonData>(textJSON.text);

        catPool = new List<Cat>();
        ownedCats = new List<Cat>();

        json.PrintCurrency();
        json.PrintAllCatsCount();
        json.PrintOwnedCatsCount();
        json.PrintShowBaristaTutorial();

        //get all the possible cats
        for (int i = 0; i < json.allCats.Count; i++)
        {
            Sprite image = LoadAllCatImage(i, json);
            JsonCat a = json.allCats[i];

            Cat allCat = new Cat(image, a.imgPath, a.name, a.rarity);
            catPool.Add(allCat);

        }

        //get a list of all the owned cats
        for (int i = 0; i < json.ownedCats.Count; i++)
        {
            JsonCat o = json.ownedCats[i];
            Sprite image = LoaOwnedCatImage(i, json);

            Cat ownedCat = new Cat(image, o.imgPath, o.name, o.rarity);
            ownedCats.Add(ownedCat);
        }

        //showBarsitaTutorial
        ShowBaristaTutorial = json.showBaristaTutorial;

        //get the currecny
        Currency = json.currency;
    }

    /// <summary>
    /// Helper method in order to get the images for the cats
    /// </summary>
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

    private JsonCat ConvertCatToJsonCat(Cat cat)
    {
        return new JsonCat(cat.catName, cat.imgURl, cat.rarity);
    }
}
