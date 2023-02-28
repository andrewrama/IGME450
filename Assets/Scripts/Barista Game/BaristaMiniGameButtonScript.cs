using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaristaMiniGameButtonScript : MonoBehaviour
{
    //Kovu Bentley
    //Purpose: Holds all of the events for buttons being pressed for the barista mini game

    #region Buttons
    [SerializeField]
    private Button hotCupButton;

    [SerializeField]
    private Button coldCupButton;

    [SerializeField]
    private Button coffeeButton;

    [SerializeField]
    private Button milkButton;

    [SerializeField]
    private Button trashButton;

    [SerializeField]
    private Button helpButton;
    #endregion

    #region Images

    [SerializeField]
    private Sprite defaultImage;

    [SerializeField]
    private Sprite blackCoffeeImage;

    [SerializeField]
    private Sprite coffeeImage;

    [SerializeField]
    private Sprite coldCupWithMilkImage;

    [SerializeField]
    private Sprite coldCupImage;

    [SerializeField]
    private Sprite hotCupWithMilkImage;

    [SerializeField]
    private Sprite hotCupImage;

    [SerializeField]
    private Sprite icedCoffeeImage;

    [SerializeField]
    private Sprite icedLatteImage;

    [SerializeField]
    private Sprite latteImage;

    [SerializeField]
    private Sprite milkImage;
    #endregion

    #region Panels

    [SerializeField]
    private GameObject cupButtonPanal;

    [SerializeField]
    private GameObject drinkButtonPanal;

    [SerializeField]
    private GameObject gamePanel;

    [SerializeField]
    private GameObject recipePanel;

    [SerializeField]
    private GameObject tutorialPanel;

    [SerializeField]
    private GameObject gameOverPanel;
    #endregion

    private GameSceneChanger sceneChanger;

    public enum Ingrediant
    { 
        HotCup,
        ColdCup,
        Coffee,
        Milk
    }

    private List<Ingrediant> ingrediantList;

    [SerializeField]
    private Text recipeTimerLabel;

    [SerializeField]
    private Text recipeScoreLabel;

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
    private List<GameObject> customerObjectList;

    private List<BaristaCustomerScript> customerScriptList;

    private bool gameOver;

    [SerializeField]
    private Text gameOverLabel;

    [SerializeField]
    private GameObject servingTable;
    private Image servingTableImage;

    void Awake()
    {
        gamePanel.SetActive(true);
        recipePanel.SetActive(false);
        sceneChanger = transform.GetComponent<GameSceneChanger>();
        servingTableImage = servingTable.GetComponent<Image>();
        StartGame();
    }

    void Start()
    {
        
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (CurrencyManager.showBaristaTuorial == 1)
        {
            CurrencyManager.showBaristaTuorial = 0;
            CurrencyManager.UpdateBaristaGameTutoiral();
            
            OpenTutorial();
        }
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

   

    public void HandleServingTable(Ingrediant ingrediant)
    {
        bool addIngrediant = false;

        int ingrediantCount = ingrediantList.Count;
        if (ingrediantCount == 0 && (ingrediant == Ingrediant.ColdCup || ingrediant == Ingrediant.HotCup))
        {
            addIngrediant = true;
        }

        else if (!ingrediantList.Contains(ingrediant))
        {

            if (ingrediantCount == 1)
            {

                Ingrediant firstIngrediant = ingrediantList[0];


                //Hot cup + coffee = black coffee
                //Hot cup + coffee = hot cup with milk
                if (firstIngrediant == Ingrediant.HotCup && ingrediant != Ingrediant.ColdCup)
                {
                    addIngrediant = true;
                }

                //coldCup + coffee = iced coffee
                //coldCup + milk = cold cup with milk
                else if (firstIngrediant == Ingrediant.ColdCup && ingrediant != Ingrediant.HotCup)
                {
                    addIngrediant = true;
                }
            }

            else if (ingrediantCount == 2)
            {
                //black coffee + milk = latte
                if (ingrediantList.Contains(Ingrediant.HotCup) &&
                    ingrediantList.Contains(Ingrediant.Coffee) &&
                    ingrediant == Ingrediant.Milk)
                {
                    addIngrediant = true;

                }

                //hot cup with milk + coffee = latte
                else if (ingrediantList.Contains(Ingrediant.HotCup) &&
                         ingrediantList.Contains(Ingrediant.Milk) &&
                         ingrediant == Ingrediant.Coffee)
                { 
                    addIngrediant = true;
                }

                //iced black coffee + milk = iced latte
                else if (ingrediantList.Contains(Ingrediant.ColdCup) &&
                    ingrediantList.Contains(Ingrediant.Coffee) &&
                    ingrediant == Ingrediant.Milk)
                {
                    addIngrediant = true;
                }

                //cold cup with milk + coffee = iced latte
                else if (ingrediantList.Contains(Ingrediant.ColdCup) &&
                         ingrediantList.Contains(Ingrediant.Milk) &&
                         ingrediant == Ingrediant.Coffee)
                {
                    addIngrediant = true;
                }
            }

           
        }

        if (addIngrediant)
        {
            ingrediantList.Add(ingrediant);
            UpdateTableDrawing();
        }
    }

    private void UpdateTableDrawing()
    {
        switch (ingrediantList.Count)
        {
            //0 ingrediants
            //-nothing
            case 0:
                servingTableImage.sprite = defaultImage;
                break;

            //1 ingrediant
            //- hot cup
            //- cold cup
            case 1:
                if (ingrediantList.Contains(Ingrediant.HotCup))
                {
                    servingTableImage.sprite = hotCupImage;
                }
                else
                { 
                    servingTableImage.sprite = coldCupImage;
                }

                ShowDrinks();
                break;

            case 2:

                //- hot cup + coffee = black coffee
                if (ingrediantList.Contains(Ingrediant.HotCup) &&
                   ingrediantList.Contains(Ingrediant.Coffee))
                { 
                    servingTableImage.sprite = blackCoffeeImage;
                }

                //- hot cup + milk = hot cup with milk
                else if (
                   ingrediantList.Contains(Ingrediant.HotCup) &&
                   ingrediantList.Contains(Ingrediant.Milk))
                {
                    servingTableImage.sprite = hotCupWithMilkImage;
                }

                //- cold cup + coffee = iced coffee
                else if (ingrediantList.Contains(Ingrediant.ColdCup) &&
                  ingrediantList.Contains(Ingrediant.Coffee))
                {
                    servingTableImage.sprite = icedCoffeeImage;
                }

                //- cold cup + milk = cold cup with milk
                else if (
                   ingrediantList.Contains(Ingrediant.ColdCup) &&
                   ingrediantList.Contains(Ingrediant.Milk))
                {
                    servingTableImage.sprite = coldCupWithMilkImage;
                }

                break;

            case 3:
                //3 ingrediants

                //- hot cup + coffee + milk = latte
                if (ingrediantList.Contains(Ingrediant.HotCup) &&
                    ingrediantList.Contains(Ingrediant.Coffee) &&
                    ingrediantList.Contains(Ingrediant.Milk))
                { 
                    servingTableImage.sprite = latteImage;
                }

                //- cold cup + coffee + milk = iced latte
                else
                {
                    servingTableImage.sprite = icedLatteImage;
                }
                break;
        }

    }

    #region Button Press Events
    public void TrashButtonPressed()
    {
        ShowCups();
        ingrediantList.Clear();
        UpdateTableDrawing();
    }

    public void HotCupButtonPressed()
    {
        HandleServingTable(Ingrediant.HotCup);
    }

    public void ColdCupButtonPressed()
    {
        HandleServingTable(Ingrediant.ColdCup);
    }

    public void CoffeeButtonPressed()
    {
        HandleServingTable(Ingrediant.Coffee);
    }

    public void MilkButtonPressed()
    {
        HandleServingTable(Ingrediant.Milk);
    }

    public void CustomerPressed(int i)
    {
        BaristaCustomerScript customerScript = GetCustomerScript(customerObjectList[i]);

        //coffee
        //iced coffee
        //latte
        //iced latte

        Sprite customerImageSprite = customerScript.GetCustomerImage().sprite;

        if (customerImageSprite == servingTableImage.sprite)
        {
            score++;
        }
        else
        {
            score--;
        }

        ShowCups();
        ingrediantList.Clear();
        UpdateTableDrawing();

        customerScript.GetNewCustomer();
        scoreLabel.text = "Score: " + score;
    }
    #endregion

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

    private BaristaCustomerScript GetCustomerScript(GameObject obj)
    {
        return obj.GetComponent<BaristaCustomerScript>();
    }

    private void EnableGame(bool lookingAtRecipies)
    {
        //enable ingrediants buttons
        hotCupButton.enabled = true;
        coldCupButton.enabled = true;
        coffeeButton.enabled = true;
        milkButton.enabled = true;

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
        //disable ingrediants buttons
        hotCupButton.enabled = true;
        coldCupButton.enabled = true;
        coffeeButton.enabled = true;
        milkButton.enabled = true;

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
        gameOverPanel.SetActive(false);
        tutorialPanel.SetActive(false);

        ShowCups();

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
            customerScriptList[i].SetImages(blackCoffeeImage, icedCoffeeImage, icedLatteImage, latteImage);
        }

        ingrediantList = new List<Ingrediant>();
        UpdateTableDrawing();
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
        gameOverPanel.SetActive(true);
        if (score < 0)
        {
            score = 0;
        }

        int fishEarned = CurrencyManager.ScoreToCurrency(score, 3);

        gameOverLabel.text = $"Game Over\nYou earned {fishEarned} fish";
    }

    public void GoToMainMenu()
    { 
        sceneChanger.BaristaScene();
    }

    private void ShowCups()
    {
        cupButtonPanal.SetActive(true);
        drinkButtonPanal.SetActive(false);
    }

    private void ShowDrinks()
    {
        cupButtonPanal.SetActive(false);
        drinkButtonPanal.SetActive(true);
    }


}
