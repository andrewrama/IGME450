using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaristaCustomerScript : MonoBehaviour
{
    [SerializeField]
    private int startingTime;

    private Button customerButton;
    private Image customerImage;
    private Text timerText;
    private float timer;
    private bool customerDisabled;
    
    // Start is called before the first frame update
    void Start()
    {
        customerImage = transform.gameObject.GetComponent<Image>();
        customerButton = transform.gameObject.GetComponent<Button>();
        timerText = transform.GetChild(0).GetComponent<Text>();
        timerText.color = Color.white;
        customerDisabled = false;
        timerText.gameObject.SetActive(false);
        GetRandomOrder();
    }

    // Update is called once per frame
    void Update()
    {
        if (customerDisabled)
        {
            timerText.text = "" + (int)timer;
            timer -= Time.deltaTime;
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
        return activate ? 255 : 0;
    }

    public void GetNewCustomer()
    {
        StartCoroutine(NewCustomer());
    }

    private IEnumerator NewCustomer()
    {
        //disable the customer's button
        timer = 5;
        customerButton.enabled = false;
        customerImage.color = Color.gray;
        customerDisabled = true;
        timerText.gameObject.SetActive(true);

        //wait for 5 sectionds
        yield return new WaitForSeconds(startingTime);

        //get a new customer
        GetRandomOrder();

        //enable button agian
        customerButton.enabled = true;
        customerDisabled = false;
        timerText.gameObject.SetActive(false);
    }

    public Color GetCustomerColor()
    {
        return customerImage.color;
    }
}
