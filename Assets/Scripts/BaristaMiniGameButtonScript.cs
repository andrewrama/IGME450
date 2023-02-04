using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaristaMiniGameButtonScript : MonoBehaviour
{
    //Kovu Bentley
    //Purpose: Holds all of the events for buttons being pressed for the barista mini game

    [SerializeField]
    private Text scoreLabel;

    private int score;

    [SerializeField]
    private List<GameObject> customerList;


    [SerializeField]
    private GameObject servingTable;

    private Image servingTableImage;
    void Start()
    {
        score = 0;
        servingTableImage = servingTable.GetComponent<Image>();
    }

    public void RedButtonPressed()
    {
        Color oldColor = servingTableImage.color;
        servingTableImage.color = new Color(255, oldColor.g, oldColor.b, oldColor.a);
    }

    public void GreenButtonPressed()
    {
        Color oldColor = servingTableImage.color;
        servingTableImage.color = new Color(oldColor.r, 255, oldColor.b, oldColor.a);
    }

    public void BlueButtonPressed()
    {
        Color oldColor = servingTableImage.color;
        servingTableImage.color = new Color(oldColor.r, oldColor.g, 255, oldColor.a);
    }

    public void TrashButtonPressed()
    {
        servingTableImage.color = new Color(0, 0, 0, 255);
    }

    public void CustomerPressed1()
    {
        BaristaCustomerScript customerScript = customerList[0].GetComponent<BaristaCustomerScript>();

        if (SameColors(customerScript.GetCustomerColor(), servingTableImage.color))
        {
            score++;
        }

        else
        {
            score--;
        }

        customerScript.GetNewCustomer();
        servingTableImage.color = new Color(0, 0, 0, 255);
        scoreLabel.text = "Score: " + score;

    }

    public void CustomerPressed2()
    {
        BaristaCustomerScript customerScript = customerList[1].GetComponent<BaristaCustomerScript>();

        if (SameColors(customerScript.GetCustomerColor(), servingTableImage.color))
        {
            score++;
        }

        else
        {
            score--;
        }

        customerScript.GetNewCustomer();
        servingTableImage.color = new Color(0, 0, 0, 255);
        scoreLabel.text = "Score: " + score;

    }

    public void CustomerPressed3()
    {
        BaristaCustomerScript customerScript = customerList[2].GetComponent<BaristaCustomerScript>();

        if (SameColors(customerScript.GetCustomerColor(), servingTableImage.color))
        {
            score++;
        }

        else
        {
            score--;
        }

        customerScript.GetNewCustomer();
        servingTableImage.color = new Color(0, 0, 0, 255);
        scoreLabel.text = "Score: " + score;

    }

    public void CustomerPressed4()
    {
        BaristaCustomerScript customerScript = customerList[3].GetComponent<BaristaCustomerScript>();

        if (SameColors(customerScript.GetCustomerColor(), servingTableImage.color))
        {
            score++;
        }

        else
        {
            score--;
        }

        customerScript.GetNewCustomer();
        servingTableImage.color = new Color(0, 0, 0, 255);
        scoreLabel.text = "Score: " + score;

    }

    private bool SameColors(Color c1, Color c2)
    {
        return c1.r == c2.r && c1.g == c2.g && c1.b == c2.b;
    }
}
