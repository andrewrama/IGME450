using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text gameOverLabel;

    [SerializeField]
    private GameObject gamePanel;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private GameObject tutorialPanel;

    [SerializeField]
    private int easyStrength;

    [SerializeField]
    private int mediumStrength;

    [SerializeField]
    private int hardStrength;

    [SerializeField]
    private int easyFishAmount;

    [SerializeField]
    private int mediumFishAmount;

    [SerializeField]
    private int hardFishAmount;

    private int currentStrength;

    [SerializeField]
    private Text buttonLabel;

    [SerializeField]
    private Text fishText;

    [SerializeField]
    private Image baseProgressBar;

    [SerializeField]
    //the image of the bar
    private Image filledProgressBar;

    int pityCounter;


    [SerializeField]
    //the value of which the bar will increase/decrese
    private float increment;

    [SerializeField]
    private Text scoreLabel;

    private RectTransform progressBarRectTransform;

    //the progress the player has so far to catch to fish
    private float progressVal;

    //the target the player needs to hit in order to catch the fish
    private int target = 5;

    //the timer until the player can catch a fish
    [SerializeField]
    private float fishingTimer;

    private float currentFishTimer;

    private JSONReader jsonScript;

    //the score of the player
    int score;

    bool fishCaught = false;

    bool gameOver;

    void Start()
    {
        progressBarRectTransform = filledProgressBar.rectTransform;

        jsonScript = transform.Find("/Reader").gameObject.GetComponent<JSONReader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!fishCaught)
        {
            currentFishTimer -= Time.deltaTime;

            if (currentFishTimer <= 0)
            {
                PickRandomNumber();
                currentFishTimer = fishingTimer;
                currentStrength = getRandomDifficulty();
            }
        }

        else
        {
            DecreaseProgress();
            UpdateBar();

            if (progressVal <= 0)
            {
                //go back to waiting for a fish
                WaitForFish();
            }
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameOver = true;
        OpenTutoiralPanel();
    }


    private void IncreseProgress()
    { 
        progressVal = (progressVal + increment * Time.deltaTime);
    }

    private void DecreaseProgress()
    {
        progressVal = (progressVal - increment * Time.deltaTime);
    }

    private void UpdateBar()
    { 
        progressBarRectTransform.anchorMax = new Vector2(progressVal / 100f, 1);
    }

    public void PullFish()
    {
        if (!fishCaught)
        {
            if (fishText.text == "" + target)
            {
                CatchFish();
            }

            else
            {
                //punish player in some way
            }
        }

        else
        {
            progressVal += currentStrength;
            UpdateBar();

            if (progressVal >= 100)
            {

                if (currentStrength == easyStrength)
                {
                    score += easyFishAmount;
                }

                else if (currentStrength == mediumStrength)
                {
                    score += mediumFishAmount;
                }

                else
                {
                    score += hardFishAmount;
                }
                UpdateScore();
                WaitForFish();
            }
        }

    }

    private void PickRandomNumber()
    {
        int num = Random.Range(1, 10);

        pityCounter++;

        if (num == 5 || pityCounter == 5)
        {
            pityCounter = 0;
            num = 5;

        }

        fishText.text = "" + num;
    }

    private void UpdateScore()
    {
        scoreLabel.text = "Score: " + score;
    }

    /// <summary>
    /// When the player is wait to get a fish
    /// </summary>
    private void WaitForFish()
    {
        PickRandomNumber();
        fishCaught = false;

        //hide the progress bar
        baseProgressBar.gameObject.SetActive(false);
        filledProgressBar.gameObject.SetActive(false);

        //show the random number generator
        fishText.gameObject.SetActive(true);

        //change the button text to catch
        buttonLabel.text = "Catch";

        currentFishTimer = fishingTimer;
    }

    /// <summary>
    /// When the player gets a bite
    /// </summary>
    private void CatchFish()
    {
        fishCaught = true;
        progressVal = 10;
        UpdateBar();

        //show the progress bar 
        baseProgressBar.gameObject.SetActive(true);
        filledProgressBar.gameObject.SetActive(true);


        //hide the random number generator
        fishText.gameObject.SetActive(false);

        //change the button text to pull
        buttonLabel.text = "Pull";
    }

    
    /// <summary>
    /// this will set the difficulty of the how to get the fish
    /// </summary>
    /// <returns></returns>
    private int getRandomDifficulty()
    {
        int rng = Random.Range(1, 12) / 4;

        return rng == 0 ? easyStrength : rng == 1 ? mediumStrength : hardStrength;
    }

    public void QuitButton()
    {
        OpenGameOverPanel();

        gameOver = true;

        if (score < 0)
        {
            score = 0;
        }

        int fishEarned = score * 3;

        jsonScript.Currency += fishEarned;

        gameOverLabel.text = $"Game Over\nYou earned {fishEarned} fish";
    }

    public void OpenTutoiralPanel()
    {
        tutorialPanel.SetActive(true);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void OpenGamePanel()
    {
        tutorialPanel.SetActive(false);
        gamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    public void OpenGameOverPanel()
    {
        tutorialPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void StartGame()
    {
        OpenGamePanel();
        WaitForFish();
        progressVal = 10;
        fishCaught = false;
        score = 0;
        UpdateScore();
        pityCounter = 0;
    }

    public void GoToCafe()
    {
        SceneManager.LoadScene(0);
    }
}
