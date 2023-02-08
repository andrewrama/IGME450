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
    public Sprite[] catSprites;
    public string[] catNames;

    // Start is called before the first frame update
    void Start()
    {
        wishResult.SetActive(false);
        insufficientFunds.SetActive(false);
        catImage = wishResult.transform.GetChild(0).gameObject;
        catName = wishResult.transform.GetChild(1).gameObject;
    }


    public void Wish()
    {
        wishButton.SetActive(false);
        if (CurrencyManager.currency >= 50)
        {
            CurrencyManager.currency -= 50;
            CurrencyManager.UpdateCurrency();

            int random = Random.Range(0, catSprites.Length);

            Image sprite = catImage.GetComponent<Image>();
            sprite.sprite = catSprites[random];

            catName.GetComponent<TMPro.TextMeshProUGUI>().text = catNames[random];


            Cat newCat = new Cat(sprite.sprite, sprite.sprite, catNames[random]);

            CatInventory.Instance.AddCatToList(newCat);

            wishResult.SetActive(true);
        }
        else
        {
            insufficientFunds.SetActive(true);
        }
        
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
