using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaristaCustomerScript : MonoBehaviour
{

    #region Images
    private Sprite blackCoffeeImage;
    private Sprite icedCoffeeImage;
    private Sprite icedLatteImage;
    private Sprite latteImage;
    #endregion

    private int startingTime;

    public int penatlyScore;

    private Image customerImage;

    private Image filledProgressBar;

    private float timer;
    private bool gamePaused;
    private RectTransform progressBarRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        customerImage = transform.GetChild(0).gameObject.GetComponent<Image>();

        
        GameObject baseProgressBar = transform.GetChild(1).gameObject;
        filledProgressBar = baseProgressBar.transform.GetChild(0).gameObject.GetComponent<Image>();
        progressBarRectTransform = filledProgressBar.rectTransform;
        
        IniitalizeCustomer();
    }

    public void IniitalizeCustomer()
    {
        gamePaused = false;
        penatlyScore = 0;
        GetNewCustomer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gamePaused)
        { 
            timer -= Time.deltaTime;
            UpdateBar();

            if (timer <= 0)
            {
                penatlyScore++;
                GetNewCustomer();
            }
        }
    }


    public void GetNewCustomer()
    {
        List<Sprite> orderList = new List<Sprite>()
        {
            blackCoffeeImage,
            icedCoffeeImage,
            icedLatteImage,
            latteImage
        };

        int index = Random.Range(0, orderList.Count);
        Sprite order = orderList[index];
        customerImage.sprite = order;

        timer = startingTime;
        UpdateBar();
    }

    public void SetGamePaused(bool b)
    {
        gamePaused = b;
    }

    public void SetStartingTime(int num)
    {
        startingTime = num;
    }

    public void SetImages(Sprite blackCoffeeImage, 
                          Sprite icedCoffeeImage, 
                          Sprite icedLatteImage, 
                          Sprite latteImage)
    {
        this.blackCoffeeImage = blackCoffeeImage;
        this.icedCoffeeImage = icedCoffeeImage;
        this.icedLatteImage = icedLatteImage;
        this.latteImage = latteImage;
    }

    public Image GetCustomerImage()
    {
        return customerImage;
    }


    private void UpdateBar()
    {
        float progressVal = timer / startingTime;
        progressBarRectTransform.anchorMax = new Vector2(progressVal, 1);
        filledProgressBar.color = new Color(1 - progressVal, progressVal, 0);
    }


}
