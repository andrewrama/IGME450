using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LogicScript : MonoBehaviour
{
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

    [ContextMenu("Increase Score")]
    public void GetTreat()
    {
        treatsCollected += 1;
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = treatsCollected.ToString();
    }

    public void GameOver()
    {
        // stop the game
        treatSpawn.SetActive(false);
        boxSpawn.SetActive(false);
        player.SetActive(false);

        CleanUp();

        // display game over screen
        gameOverScreen.SetActive(true);

        // set final score
        finalScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = scoreText.GetComponent<TMPro.TextMeshProUGUI>().text;        
    }

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
        player.transform.position = new Vector3(109, 194, 0);
    }

    // helper methods
    private void CleanUp()
    {
        boxesLeft = GameObject.FindGameObjectsWithTag("boxObstacle");
        treatsLeft = GameObject.FindGameObjectsWithTag("treatPickup");

        // clear the screen
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
