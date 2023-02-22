using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaristaCustomerScript : MonoBehaviour
{

    #region Images
    private Sprite coffeeImage;
    private Sprite coldCupWithMilkImage;
    private Sprite coldCupImage;
    private Sprite hotCupWithMilkImage;
    private Sprite hotCupImage;
    private Sprite icedCoffeeImage;
    private Sprite icedLatteImage;
    private Sprite latteImage;
    #endregion

    private int startingTime;

    private Image customerImage;

    private Button customerButton;
    private Text timerText;
    private float timer;
    private bool customerEnabled;
    private bool gamePaused;

    // Start is called before the first frame update
    void Start()
    {
        customerImage = transform.GetChild(1).gameObject.GetComponent<Image>();
        customerButton = transform.gameObject.GetComponent<Button>();
        timerText = transform.GetChild(0).GetComponent<Text>();

        IniitalizeCustomer();
    }

    public void IniitalizeCustomer()
    {
        customerEnabled = true;
        gamePaused = false;

        timerText.color = Color.white;
        timerText.gameObject.SetActive(false);

        GetRandomOrder();
    }

    // Update is called once per frame
    void Update()
    {
        if (!customerEnabled && !gamePaused)
        {
            timerText.text = "" + (int)timer;
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                //get a new customer
                GetRandomOrder();

                //enable button agian
                customerButton.enabled = true;
                customerEnabled = true;
                timerText.gameObject.SetActive(false);
            }
        }
    }

    private void GetRandomOrder()
    {
        List<Sprite> orderList = new List<Sprite>()
        {
            coffeeImage,
            coldCupWithMilkImage,
            coldCupImage,
            hotCupWithMilkImage,
            hotCupImage,
            icedCoffeeImage,
            icedLatteImage,
            latteImage
        };

        int index = Random.Range(0, orderList.Count);
        Sprite order = orderList[index];
        customerImage.sprite = order;
    }

    public void GetNewCustomer()
    {
        //disable the customer's button
        timer = startingTime;
        customerButton.enabled = false;
        customerImage.color = Color.gray;
        customerEnabled = false;
        timerText.gameObject.SetActive(true);
    }

    public Color GetCustomerColor()
    {
        return customerImage.color;
    }

    public void SetGamePaused(bool b)
    {
        gamePaused = b;
    }

    public void SetStartingTime(int num)
    {
        startingTime = num;
    }

    public void SetImages(Sprite coffeeImage, 
                          Sprite coldCupWithMilkImage, 
                          Sprite coldCupImage, 
                          Sprite hotCupWithMilkImage, 
                          Sprite hotCupImage, 
                          Sprite icedCoffeeImage, 
                          Sprite icedLatteImage, 
                          Sprite latteImage)
    {
        this.coffeeImage = coffeeImage;
        this.coldCupWithMilkImage = coldCupWithMilkImage;
        this.coldCupImage = coldCupImage;
        this.hotCupWithMilkImage = hotCupWithMilkImage;
        this.hotCupImage = hotCupImage;
        this.icedCoffeeImage = icedCoffeeImage;
        this.icedLatteImage = icedLatteImage;
        this.latteImage = latteImage;
    }


}
