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
        public int ownedNum;

        public JsonCat(string name, string imgPath, string rarity, int ownedNum)
        {
            this.name = name;
            this.imgPath = imgPath;
            this.rarity = rarity;
            this.ownedNum = ownedNum;
        }
    }

    [System.Serializable]
    public class JsonData
    {
        public int currency;
        public List<JsonCat> allCats;
        public bool showBaristaTutorial;
        public bool showFishingTutoiral;
        public int refundPercentage;
        public int baristaHighScore;
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
        if (saveData.allCats == null)
        {
            saveData.allCats = new List<Cat>();
            return;
        }

        if (json == null)
        {
            json = JsonUtility.FromJson<JsonData>(textJSON.text);
        }

        //convert all cats
        json.allCats.Clear();

        json.allCats = saveData.allCats.Select(x => ConvertCatToJsonCat(x)).ToList();

        //convert currency
        json.currency = saveData.Currency;

        //tutorials
        json.showBaristaTutorial = saveData.ShowBaristaTutorial;
        json.showFishingTutoiral = saveData.ShowFishingTutoiral;

        //high scores
        json.baristaHighScore = saveData.BaristaHighScore; 

        string s = JsonUtility.ToJson(json);
        File.WriteAllText(Application.dataPath + "/info.json", s);

        Debug.Log("Saving data...\n" + s);
    }

    private void LoadData()
    {
        Debug.Log("Loading data...\n" + textJSON.text);
        json = JsonUtility.FromJson<JsonData>(textJSON.text);

        saveData.allCats = new List<Cat>();
        
        //get all the possible cats
        for (int i = 0; i < json.allCats.Count; i++)
        {
            Sprite image = LoadAllCatImage(i, json);
            JsonCat c = json.allCats[i];

            Cat allCat = new Cat(image, c.imgPath, c.name, c.rarity, c.ownedNum);
            saveData.allCats.Add(allCat);
        }

        //tutorials
        saveData.ShowBaristaTutorial = json.showBaristaTutorial;
        saveData.ShowFishingTutoiral = json.showFishingTutoiral;

        //get the currecny
        saveData.Currency = json.currency;

        //refund amount
        saveData.RefundPercentage = json.refundPercentage;

        //high scores
        saveData.BaristaHighScore = json.baristaHighScore;
    }

    public void ResetData()
    {
        saveData.ShowBaristaTutorial = true;
        saveData.ShowFishingTutoiral = true;
        saveData.Currency = 0;
        saveData.BaristaHighScore = 0;
        SaveData();
    }

    /// <summary>
    /// Helper method in order to get the images for the cats
    /// </summary>
    /// <returns></returns>
    private Sprite LoadAllCatImage(int index, JsonData catPool)
    {
        string imageURL = "Sprites/" + catPool.allCats[index].imgPath;

        return Resources.Load<Sprite>(imageURL);
    }

    private JsonCat ConvertCatToJsonCat(Cat cat)
    {
        return new JsonCat(cat.catName, cat.imgUrl, cat.rarity, cat.ownedNum);
    }
}
