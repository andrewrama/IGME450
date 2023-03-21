using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class WishingLogic : MonoBehaviour
{
    public GameObject wishResult;
    public GameObject wishButton;
    public GameObject insufficientFunds;
    private GameObject catImage;
    private GameObject catName;

    private JSONReader jsonScript;


    // Start is called before the first frame update
    void Start()
    {
        jsonScript = transform.Find("/Reader").gameObject.GetComponent<JSONReader>();
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
        if (jsonScript.Currency >= 50)
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
        jsonScript.Currency -= 50;

        int random = Random.Range(0, 100);

        string rarity = random < 50 ? "Common" : random >= 50 && random < 90 ? "Uncommon" : "Rare";

        List<Cat> catPool = jsonScript.catPool.Where(x => x.rarity == rarity).ToList();

        Cat catPulled = catPool[Random.Range(0, catPool.Count)];

        jsonScript.AddCat(catPulled);


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
