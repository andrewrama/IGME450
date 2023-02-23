using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayInventory : MonoBehaviour
{
    public Transform contentContainer;
    public GameObject inventoryItem;

    // Start is called before the first frame update
    void Start()
    {
        //contentContainer.GetComponent<VerticalLayoutGroup>().spacing = 5;
        for (int i = 0; i < CatInventory.Instance.ownedCats.Count; i++)
        {
            Cat currentCat = CatInventory.Instance.ownedCats[i];
            var listItem = Instantiate(inventoryItem);

            listItem.GetComponentsInChildren<Image>()[2].sprite = currentCat.imageSprite;
            listItem.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = currentCat.catName;
            listItem.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = currentCat.rarity;

            listItem.transform.SetParent(contentContainer);
            //listItem.GetComponent<LayoutElement>().preferredWidth = Screen.width / 2;
            listItem.transform.position = new Vector3(0, contentContainer.position.y, 0);

            //listItem.transform.localScale = Vector2.one;
        }



    }

}
