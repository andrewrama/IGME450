using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaristaCustomerScript : MonoBehaviour
{
    private int startingTime;

    private Button customerButton;
    private Image customerImage;
    private Text timerText;
    private float timer;
    private bool customerEnabled;
    private bool gamePaused;

    
    // Start is called before the first frame update
    void Start()
    {
        customerImage = transform.gameObject.GetComponent<Image>();
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
        bool red = Random.Range(0, 2) == 1;
        bool green = Random.Range(0, 2) == 1;
        bool blue = Random.Range(0, 2) == 1;

        customerImage.color = new Color(ActivateColor(red), ActivateColor(green), ActivateColor(blue));
    }

    private int ActivateColor(bool activate)
    {
        return activate ? 1 : 0;
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


}
