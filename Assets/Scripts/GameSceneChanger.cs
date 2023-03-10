using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneChanger : MonoBehaviour
{
    public void CafeScene()
    {
        SceneManager.LoadScene(0);
    }

    public void FishScene()
    {
        SceneManager.LoadScene(5);
    }

    public void WishScene()
    {
        SceneManager.LoadScene(2);
    }
    public void BaristaScene()
    {
        SceneManager.LoadScene(3);
    }

    public void RunnerScene()
    {
        SceneManager.LoadScene(4);
    }
}
