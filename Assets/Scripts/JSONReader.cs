using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    public TextAsset textJSON;
    public CatList catPool = new CatList();
    [System.Serializable]
    public class Cat
    {
        public string name;
        public string imgPath;
        public string rarity;
    }

    [System.Serializable]
    public class CatList
    {
        public List<Cat> cats;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(textJSON.text);
        catPool = JsonUtility.FromJson<CatList>(textJSON.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
