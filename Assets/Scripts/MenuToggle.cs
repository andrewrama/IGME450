using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class MenuToggle : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject gamesPanel;

    public void ToggleMenu()
    {
        if (menuPanel.activeInHierarchy)
        {
            menuPanel.SetActive(false);
        }
        else
        {
            menuPanel.SetActive(true);
        }
    }

    public void ToggleGame()
    {
        if (gamesPanel.activeInHierarchy)
        {
            gamesPanel.SetActive(false);
        }
        else
        {
            gamesPanel.SetActive(true);
        }
    }
}
