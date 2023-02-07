using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaristaMiniGameButtonScript : MonoBehaviour
{
    //Kovu Bentley
    //Purpose: Holds all of the events for buttons being pressed for the barista mini game

    [SerializeField]
    private GameObject tutorialDisplay;

    [SerializeField]
    private Button helpButton;

    [SerializeField]
    private Text scoreLabel;

    [SerializeField]
    private Button redButton;

    [SerializeField]
    private Button greenButton;
    
    [SerializeField]
    private Button blueButton;


    private int score;

    [SerializeField]
    private List<GameObject> customerList;


    [SerializeField]
    private GameObject servingTable;


    private Button servingTableButton;
    private Image servingTableImage;
    void Start()
    {
        score = 0;
        servingTableImage = servingTable.GetComponent<Image>();
        servingTableButton = servingTable.GetComponent<Button>();
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

    public void CustomerPressed(int i)
    { 
        BaristaCustomerScript customerScript = GetCustomerScript(customerList[i]);

        if (SameColors(customerScript.GetCustomerColor(), servingTableImage.color))
        {
            score++;
        }

        else
        {
            score--;
        }

        customerScript.GetNewCustomer();
        servingTableImage.color = new Color(0, 0, 0, 1);
        scoreLabel.text = "Score: " + score;
    }

    public void OpenTutorial()
    {
        //disable r, g, b
        redButton.enabled = false;
        greenButton.enabled = false;
        blueButton.enabled = false;

        //disable trash button
        servingTableButton.enabled = false;

        //disable customers (pause customers if possible)
        foreach (GameObject customer in customerList)
        {
            customer.GetComponent<Button>().enabled = false;
        }

        //disable help button
        helpButton.enabled = false;

        //display tutorial object
        tutorialDisplay.SetActive(true);

    }

    public void CloseTutoiral()
    {
        //enable r, g, b
        redButton.enabled = true;
        greenButton.enabled = true;
        blueButton.enabled = true;

        //enable trash button
        servingTableButton.enabled = true;

        //enable customers (unpause customers if possible)
        foreach (GameObject customer in customerList)
        {
            customer.GetComponent<Button>().enabled = true;
        }

        //enable help button
        helpButton.enabled = true;


        //hide tutorial object
        tutorialDisplay.SetActive(false);
    }

    private bool SameColors(Color c1, Color c2)
    {
        return c1.r == c2.r && c1.g == c2.g && c1.b == c2.b;
    }

    private BaristaCustomerScript GetCustomerScript(GameObject obj)
    {
        return obj.GetComponent<BaristaCustomerScript>();
    }


}
