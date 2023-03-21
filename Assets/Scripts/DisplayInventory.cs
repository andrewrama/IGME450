using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayInventory : MonoBehaviour
{
    public GameObject inventoryDisplay;

    public Transform contentContainer;
    public GameObject inventoryItem;

    
    private JSONReader jsonScript;
    // Start is called before the first frame update
    void Start()
    {
        jsonScript = transform.Find("/Reader").gameObject.GetComponent<JSONReader>();
        //contentContainer.GetComponent<VerticalLayoutGroup>().spacing = 5;

        for (int i = 0; i < jsonScript.ownedCats.Count; i++)
        {
            Cat currentCat = jsonScript.ownedCats[i];
            var listItem = Instantiate(inventoryItem);

            listItem.GetComponentsInChildren<Image>()[1].sprite = currentCat.imageSprite;
            listItem.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = currentCat.catName;
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
