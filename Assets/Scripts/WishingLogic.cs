using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WishingLogic : MonoBehaviour
{
    public GameObject wishResult;
    private GameObject catImage;
    private GameObject catName;
    public Sprite[] catSprites;
    public string[] catNames;

    // Start is called before the first frame update
    void Start()
    {
        wishResult.SetActive(false);
        catImage = wishResult.transform.GetChild(0).gameObject;
        catName = wishResult.transform.GetChild(1).gameObject;
    }


    public void Wish()
    {
        int random = Random.Range(0, catSprites.Length);

        Image sprite = catImage.GetComponent<Image>();
        sprite.sprite = catSprites[random];

        catName.GetComponent<TMPro.TextMeshProUGUI>().text = catNames[random];

        wishResult.SetActive(true);
    }

    public void WishAccepted()
    {
        wishResult.SetActive(false);
    }
}
