using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class LogicScript : MonoBehaviour
{
    #region Variables
    public int treatsCollected;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public string text;

    public GameObject gameOverScreen;
    public GameObject treatSpawn;
    public GameObject boxSpawn;
    public GameObject player;

    private GameObject[] boxesLeft;
    private GameObject[] treatsLeft;

    public float centerPoint;
    public float leftEdge;
    public float rightEdge;

    [SerializeField]
    private SaveDataScriptableObject saveData;

    public float[] posOptions;

    #endregion

    void Start()
    {
        centerPoint = player.transform.position.x;
        leftEdge = centerPoint - 54;
        rightEdge = centerPoint + 54;
        posOptions = new float[3] { centerPoint, leftEdge, rightEdge };
    }

    /// <summary>
    /// increases number of treats collected, updates text on screen
    /// </summary>
    public void GetTreat()
    {
        treatsCollected += 1;
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = treatsCollected.ToString();
    }

    /// <summary>
    /// clears game screen and moves to game over screen
    /// </summary>
    public void GameOver()
    {
        // stop the game
        treatSpawn.SetActive(false);
        boxSpawn.SetActive(false);
        player.SetActive(false);

        // clear the screen
        CleanUp();

        // display game over screen
        gameOverScreen.SetActive(true);

        // set final score
        finalScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = (treatsCollected * 2).ToString(); ;

        // add to currency
        treatsCollected *= 2;
        saveData.Currency += treatsCollected;
    }

    /// <summary>
    /// clears game over screen and moves to game, restarts game
    /// </summary>
    public void StartGame()
    {
        // reset the score
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "0";
        treatsCollected = 0;

        // turn off game over screen
        gameOverScreen.SetActive(false);

        // turn on spawners
        treatSpawn.SetActive(true);
        boxSpawn.SetActive(true);

        // restart player
        player.SetActive(true);
        player.transform.position = new Vector3(108, 194, 0);
    }

    // helper methods

    /// <summary>
    /// cleans up leftover GameObjects on screen
    /// </summary>
    private void CleanUp()
    {
        boxesLeft = GameObject.FindGameObjectsWithTag("boxObstacle");
        treatsLeft = GameObject.FindGameObjectsWithTag("treatPickup");

        foreach (GameObject item in boxesLeft)
        {
            Destroy(item);
        }

        foreach (GameObject item in treatsLeft)
        {
            Destroy(item);
        }
    }
}
