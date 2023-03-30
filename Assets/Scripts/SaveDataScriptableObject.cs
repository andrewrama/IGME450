using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "SaveDataScriptableObject", menuName ="ScriptableObjects/SaveData")]
public class SaveDataScriptableObject : ScriptableObject
{
    //the amount of money the player has
    public int Currency;

    //all the cats in the game
    public List<Cat> allCats;

    //all the cats the player owns
    public List<Cat> ownedCats;

    //if the player will see the long tutoiral for the barista game
    public bool ShowBaristaTutorial;

    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;

    public void Print()
    {
        Debug.Log($"Currency: {Currency}\nShowBaristaTutorial: {ShowBaristaTutorial}\nOwned Cats: {string.Join(", ", ownedCats.Select(x => x.catName))}");
    }
}
