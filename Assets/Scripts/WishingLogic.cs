using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WishingLogic : MonoBehaviour
{
    public GameObject wishResult;
    public GameObject wishButton;
    public GameObject insufficientFunds;
    private GameObject catImage;
    private GameObject catName;

    // Common
    public Sprite[] catSpritesCommon;
    public string[] catNamesCommon;

    // Uncommon
    public Sprite[] catSpritesUncommon;
    public string[] catNamesUncommon;

    // Rare
    public Sprite[] catSpritesRare;
    public string[] catNamesRare;

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
        if (CurrencyManager.currency >= 50)
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
        CurrencyManager.currency -= 50;
        CurrencyManager.UpdateCurrency();

        int random = Random.Range(0, 100);

        if (random < 50)
        {
            GetCatOfRarity(catSpritesCommon, catNamesCommon);
        }
        else if (random >= 50 && random < 90)
        {
            GetCatOfRarity(catSpritesUncommon, catNamesUncommon);
        }
        else
        {
            GetCatOfRarity(catSpritesRare, catNamesRare);
        }


        wishResult.SetActive(true);
    }

    /// <summary>
    /// Gets a cat using the arrays of the random rarity chosen
    /// </summary>
    /// <param name="spriteArr">the array of sprites for the cats of that rarity</param>
    /// <param name="nameArr">the array of names of cats of that rarity</param>
    private void GetCatOfRarity(Sprite[] spriteArr, string[] nameArr)
    {
        int random = Random.Range(0, spriteArr.Length);

        Image sprite = catImage.GetComponent<Image>();
        sprite.sprite = spriteArr[random];

        catName.GetComponent<TMPro.TextMeshProUGUI>().text = nameArr[random];

        CatInventory.Instance.AddCatToList(new Cat(sprite.sprite, sprite.sprite, nameArr[random]));
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
