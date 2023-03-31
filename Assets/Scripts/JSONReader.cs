using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class JSONReader : MonoBehaviour
{

    [SerializeField]
    private SaveDataScriptableObject saveData;

    private PlayerControls inputActions;

    //object used in order to read/write data from json

    public TextAsset textJSON;

    private JsonData json;

    private static bool init = true;

    [System.Serializable]
    public class JsonCat
    {
        public string name;
        public string imgPath;
        public string rarity;

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

    private void Update()
    {
        if (inputActions.DevTools.GiveCurrency.triggered)
        {
            saveData.Currency = 1000;
            SaveData();
        }

        if (inputActions.DevTools.Reset.triggered)
        {
            ResetData();
            
        }
    }

    void Awake()
    {
        if (init)
        {
            init = false;
            LoadData();
        }

        else
        { 
            SaveData();
        }
    }

    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
        }

        inputActions.Enable();

    }

    private void OnDisable()
    {
        inputActions.Disable();
    }



    public void SaveData()
    {
        if (saveData.allCats == null || saveData.ownedCats == null)
        {
            saveData.allCats = new List<Cat>();
            saveData.ownedCats = new List<Cat>();
            return;
        }

        if (json == null)
        {
            json = JsonUtility.FromJson<JsonData>(textJSON.text);
        }

        //convert all cats
        json.allCats.Clear();

        json.allCats = saveData.allCats.Select(x => ConvertCatToJsonCat(x)).ToList();

        //convert owned cats
        json.ownedCats.Clear();

        json.ownedCats = saveData.ownedCats.Select(x => ConvertCatToJsonCat(x)).ToList();

        //convert currency
        json.currency = saveData.Currency;

        //show barista tutorial
        json.showBaristaTutorial = saveData.ShowBaristaTutorial;

        string s = JsonUtility.ToJson(json);
        File.WriteAllText(Application.dataPath + "/info.json", s);

        Debug.Log("Saving data...\n" + s);
    }

    private void LoadData()
    {
        Debug.Log("Loading data...\n" + textJSON.text);
        json = JsonUtility.FromJson<JsonData>(textJSON.text);

        saveData.allCats = new List<Cat>();
        saveData.ownedCats = new List<Cat>();

        //get all the possible cats
        for (int i = 0; i < json.allCats.Count; i++)
        {
            Sprite image = LoadAllCatImage(i, json);
            JsonCat a = json.allCats[i];

            Cat allCat = new Cat(image, a.imgPath, a.name, a.rarity);
            saveData.allCats.Add(allCat);
        }

        //get a list of all the owned cats
        for (int i = 0; i < json.ownedCats.Count; i++)
        {
            JsonCat o = json.ownedCats[i];
            Sprite image = LoadOwnedCatImage(i, json);

            Cat ownedCat = new Cat(image, o.imgPath, o.name, o.rarity);
            saveData.ownedCats.Add(ownedCat);
        }

        //showBarsitaTutorial
        saveData.ShowBaristaTutorial = json.showBaristaTutorial;

        //get the currecny
        saveData.Currency = json.currency;
    }

    public void ResetData()
    {
        saveData.ShowBaristaTutorial = true;
        saveData.ownedCats.Clear();
        saveData.Currency = 0;
        SaveData();
    }

    /// <summary>
    /// Helper method in order to get the images for the cats
    /// </summary>
    /// <returns></returns>
    private Sprite LoadAllCatImage(int index, JsonData catPool)
    {
        string imageURL = "Assets/Sprites/" + catPool.allCats[index].imgPath;

        return (Sprite)AssetDatabase.LoadAssetAtPath(imageURL, typeof(Sprite));
    }

    private Sprite LoadOwnedCatImage(int index, JsonData catPool)
    {
        //string imageURL = Application.dataPath + catPool.ownedCats[index].imgPath;

        string imageURL = "Assets/Sprites/" + catPool.ownedCats[index].imgPath;

        Debug.Log(imageURL);

        return (Sprite)AssetDatabase.LoadAssetAtPath(imageURL, typeof(Sprite));
    }

    private JsonCat ConvertCatToJsonCat(Cat cat)
    {
        return new JsonCat(cat.catName, cat.imgUrl, cat.rarity);
    }
}
