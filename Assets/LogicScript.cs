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

    //text = scoreText.GetComponent<TMPro.TextMeshProUGUI>().text;
    [ContextMenu("Increase Score")]
    public void getTreat()
    {
        treatsCollected += 1;
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = treatsCollected.ToString() ;
    }
}
