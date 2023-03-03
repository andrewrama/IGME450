using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LogicScript : MonoBehaviour
{
    public int treatsCollected;
    public TextMeshProUGUI scoreText;
    public string text;

    [ContextMenu("Increase Score")]
    public void GetTreat()
    {
        treatsCollected += 1;
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = treatsCollected.ToString();
    }

    public void GameOver()
    {
        // display final score
        // stop da game

    }
}
