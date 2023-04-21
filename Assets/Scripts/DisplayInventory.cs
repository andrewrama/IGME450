using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class DisplayInventory : MonoBehaviour
{

    public GameObject inventoryDisplay;

    public Transform contentContainer;
    public GameObject inventoryItem;

    [SerializeField]
    private SaveDataScriptableObject saveData;


    // Start is called before the first frame update
    void Start()
    {
        //contentContainer.GetComponent<VerticalLayoutGroup>().spacing = 5;

        
        for (int i = 0; i < saveData.ownedCats.Count; i++)
        {
            Cat currentCat = saveData.ownedCats[i];
            var listItem = Instantiate(inventoryItem);

            listItem.GetComponentsInChildren<Image>()[1].sprite = currentCat.imageSprite;
            listItem.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = currentCat.catName;
            listItem.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = currentCat.rarity;

            listItem.transform.SetParent(contentContainer);
            //listItem.GetComponent<LayoutElement>().preferredWidth = Screen.width / 2;

            //listItem.transform.localScale = Vector2.one;
        }

        string imageURL = "Sprites/questionmark";

        for (int i = 0; i < saveData.unownedCats.Count; i++)
        {
            Cat currentCat = saveData.unownedCats[i];
            var listItem = Instantiate(inventoryItem);

            listItem.GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>(imageURL);
            listItem.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "???";
            listItem.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = currentCat.rarity;

            listItem.transform.SetParent(contentContainer);
            //listItem.GetComponent<LayoutElement>().preferredWidth = Screen.width / 2;

            //listItem.transform.localScale = Vector2.one;
        }
    }

    public void ToggleVisibility()
    {
        if (inventoryDisplay.activeInHierarchy)
        {
            inventoryDisplay.SetActive(false);
        }
        else
        {
            inventoryDisplay.SetActive(true);
        }
    }

}
