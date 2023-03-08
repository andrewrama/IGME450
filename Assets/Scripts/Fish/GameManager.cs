using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text fishText;

    [SerializeField]
    //the image of the bar
    private Image filledProgressBar;

    
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

    //the score of the player
    int score;


    bool fishCaught = false;
    void Start()
    {
        progressBarRectTransform = filledProgressBar.rectTransform;
        progressVal = 100;
        fishCaught = false; 
        currentFishTimer = fishingTimer;
        score = 0;
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {

        currentFishTimer -= Time.deltaTime;

        if(currentFishTimer <= 0)
        {
            PickRandomNumber();
            currentFishTimer = fishingTimer;
        }
        
        /*
        DecreaseProgress();
        Debug.Log(progressVal);
        UpdateBar();
        */
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
        Debug.Log("score");
        if (fishText.text == "" + target)
        {
            score++;

            UpdateScore();
        }

        else
        { 
            //punish player in some way
        }

    }

    private void PickRandomNumber()
    {
        fishText.text = "" + Random.Range(1, 10);
    }

    private void UpdateScore()
    {
        scoreLabel.text = "Score: " + score;
    }
}
