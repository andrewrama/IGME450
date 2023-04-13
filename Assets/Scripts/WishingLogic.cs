using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WishingLogic : MonoBehaviour
{
    public GameObject wishResult;
    public GameObject wishButton;
    public GameObject insufficientFunds;
    private GameObject catImage;
    private GameObject catName;

    [SerializeField]
    public JSONReader jsonScript;

    [SerializeField]
    private SaveDataScriptableObject saveData;



    // Start is called before the first frame update
    void Start()
    {

        wishResult.SetActive(false);
        insufficientFunds.SetActive(false);
        catImage = wishResult.transform.GetChild(0).gameObject;
        catName = wishResult.transform.GetChild(1).gameObject;
    }

    /// <summary>
    /// Check if player has enough currency to wish. If they do
    /// then wish for cat, if not then display a notice.
    /// </summary>
    public void WishButton()
    {
        wishButton.SetActive(false);
        
        if (saveData.Currency >= 50)
        {
            Wish();
        }
        else
        {
            insufficientFunds.SetActive(true);
        }
        

    }

    /// <summary>
    /// Wishing logic
    /// 
    /// Includes randomization and rarity
    /// </summary>
    private void Wish()
    {
        saveData.Currency -= 50;

        int random = Random.Range(0, 100);

        string rarity = random < 50 ? "Common" : random >= 50 && random < 90 ? "Uncommon" : "Rare";

       List<Cat> catPool = saveData.allCats.Where(x => x.rarity == rarity).ToList();

        Cat catPulled = catPool[Random.Range(0, catPool.Count)];

        if(catPulled.Owned)
        {
            int refund = Mathf.FloorToInt(50f * (saveData.RefundPercentage * .01f));
            saveData.Currency += refund;
        }

        catPulled.ownedNum++;


        jsonScript.SaveData();

        catImage.GetComponent<Image>().sprite = catPulled.imageSprite;
        catName.GetComponent<TMP_Text>().text = catPulled.catName;

        wishResult.SetActive(true);
    }

    public void WishAccepted()
    {
        wishResult.SetActive(false);
        wishButton.SetActive(true);
    }

    public void NoticeAccepted()
    {
        insufficientFunds.SetActive(false);
        wishButton.SetActive(true);
    }
}
