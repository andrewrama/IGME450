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

    public List<Cat> ownedCats { get { return allCats.Where(x => x.Owned).ToList(); } }
    public List<Cat> unownedCats { get { return allCats.Where(x => !x.Owned).ToList(); } }


    //if the player will see the long tutoiral for the barista game
    public bool ShowBaristaTutorial;

    //if the player will see the tutoiral for the fishing game
    public bool ShowFishingTutoiral;

    //the percentage of money the player will recieve if they get a duplicate cat
    public int RefundPercentage;

    //the high score for the barista game
    public int BaristaHighScore;


    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;

    public void Print()
    {
        Debug.Log($"Currency: {Currency}\nShowBaristaTutorial: {ShowBaristaTutorial}\nShowFishingTutoiral: {ShowFishingTutoiral}");
    }

    public void AddCat(Cat cat)
    {
        cat.ownedNum++;
    }
}
