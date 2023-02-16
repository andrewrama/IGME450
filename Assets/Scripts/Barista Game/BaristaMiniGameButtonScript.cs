using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaristaMiniGameButtonScript : MonoBehaviour
{
    //Kovu Bentley
    //Purpose: Holds all of the events for buttons being pressed for the barista mini game

    private GameSceneChanger sceneChanger;

    [SerializeField]
    private Text recipeTimerLabel;

    [SerializeField]
    private Text recipeScoreLabel;

    [SerializeField]
    private Button trashButton;

    [SerializeField]
    private GameObject gamePanel;

    [SerializeField]
    private GameObject recipePanel;

    [SerializeField]
    private GameObject gameOverScene;

    [SerializeField]
    private int customerStartingTime;

    [SerializeField]
    private int gameStartingTime;

    [SerializeField]
    private Text timeLabel;

    private float timer;

    [SerializeField]
    private Text scoreLabel;

    private int score;

    private bool tutorialActive;

    [SerializeField]
    private GameObject tutorialPanel;

    [SerializeField]
    private Button helpButton;

    [SerializeField]
    private Button redButton;

    [SerializeField]
    private Button greenButton;
    
    [SerializeField]
    private Button blueButton;

    [SerializeField]
    private List<GameObject> customerObjectList;

    private List<BaristaCustomerScript> customerScriptList;

    private bool gameOver;

    [SerializeField]
    private Text gameOverLabel;

    [SerializeField]
    private GameObject servingTable;
    private Image servingTableImage;

    void Start()
    {
        recipePanel.SetActive(false);
        sceneChanger = transform.GetComponent<GameSceneChanger>();
        servingTableImage = servingTable.GetComponent<Image>();
        StartGame();
    }

    private void Update()
    {
        if (!tutorialActive  && !gameOver && timer > 0)
        {
            timer -= Time.deltaTime;
            timeLabel.text = "Time: " + (int)timer;
            recipeTimerLabel.text = "Time: " + (int)timer;

            if (timer <= 0)
            {
                SetGameOver();
            }
        }
    }

    public void RedButtonPressed()
    {
        Color oldColor = servingTableImage.color;
        servingTableImage.color = new Color(1, oldColor.g, oldColor.b, oldColor.a);
    }

    public void GreenButtonPressed()
    {
        Color oldColor = servingTableImage.color;
        servingTableImage.color = new Color(oldColor.r, 1, oldColor.b, oldColor.a);
    }

    public void BlueButtonPressed()
    {
        Color oldColor = servingTableImage.color;
        servingTableImage.color = new Color(oldColor.r, oldColor.g, 1, oldColor.a);
    }

    public void TrashButtonPressed()
    {
        servingTableImage.color = new Color(0, 0, 0, 255);
    }

    public void CustomerPressed(int i)
    { 
        BaristaCustomerScript customerScript = GetCustomerScript(customerObjectList[i]);

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
        DisableGame(false);

        tutorialActive = true;

        gamePanel.SetActive(false);

        //display tutorial object
        tutorialPanel.SetActive(true);
    }

    public void CloseTutoiral()
    {
        EnableGame(false);

        tutorialActive = false;

        //hide tutorial object
        tutorialPanel.SetActive(false);

        gamePanel.SetActive(true);
    }

    private bool SameColors(Color c1, Color c2)
    {
        return c1.r == c2.r && c1.g == c2.g && c1.b == c2.b;
    }

    private BaristaCustomerScript GetCustomerScript(GameObject obj)
    {
        return obj.GetComponent<BaristaCustomerScript>();
    }

    private void EnableGame(bool lookingAtRecipies)
    {
        //enable r, g, b
        redButton.enabled = true;
        greenButton.enabled = true;
        blueButton.enabled = true;

        //enable trash button
        trashButton.enabled = true;

        //enable customers (unpause customers if possible)
        for (int i = 0; i < customerObjectList.Count; i++)
        {
            customerObjectList[i].GetComponent<Button>().enabled = true;
            if (!lookingAtRecipies)
            { 
                customerScriptList[i].SetGamePaused(false);
            }
        }

        //enable help button
        helpButton.enabled = true;
    }

    private void DisableGame(bool lookingAtRecipies)
    {
        //disable r, g, b
        redButton.enabled = false;
        greenButton.enabled = false;
        blueButton.enabled = false;

        //disable trash button
        trashButton.enabled = false;

        //disable customers (pause customers if possible)
        for (int i = 0; i < customerObjectList.Count; i++)
        {
            customerObjectList[i].GetComponent<Button>().enabled = false;
            if (!lookingAtRecipies)
            {
                customerScriptList[i].SetGamePaused(true);
            }
        }

        //disable help button
        helpButton.enabled = false;
    }

    public void ShowRecipie()
    {
        //Have the player not interact with the game, but kee the timer showing and running
        DisableGame(true);

        recipeScoreLabel.text = "Score: " + score;

        gamePanel.SetActive(false);

        recipePanel.SetActive(true);
    }

    public void HideRecipie()
    {
        //Enable interactions of all buttons
        EnableGame(true);

        gamePanel.SetActive(true);

        recipePanel.SetActive(false);
    }


    private void StartGame()
    {
        gameOverScene.SetActive(false);
        tutorialPanel.SetActive(false);

        tutorialActive = false;
        gameOver = false;

        timer = gameStartingTime;

        timeLabel.text = "Time: " + (int)timer;

        score = 0;

        customerScriptList = new List<BaristaCustomerScript>();

        for (int i = 0; i < customerObjectList.Count; i++)
        {
            customerScriptList.Add(customerObjectList[i].GetComponent<BaristaCustomerScript>());
            customerScriptList[i].SetStartingTime(customerStartingTime);
        }
    }

    public void QuitGame()
    {
        score = 0;
        SetGameOver();
    }

    private void SetGameOver()
    {
        gamePanel.SetActive(false);
        tutorialPanel.SetActive(false);
        recipePanel.SetActive(false);

        gameOver = true;
        gameOverScene.SetActive(true);
        if (score < 0)
        {
            score = 0;
        }

        CurrencyManager.currency += score;
        CurrencyManager.UpdateCurrency();

        gameOverLabel.text = $"Game Over\nYou earned {score} fish";
    }

    public void GoToMainMenu()
    { 
        sceneChanger.BaristaScene();
    }


}
