using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public const string FishCurrency = "Currency";
    public static int currency = 0;

    public const string  showBaristaGameTutorial =  "BaristaTutorial";
    public static int showBaristaTuorial = 1;

    // Start is called before the first frame update
    void Start()
    {
        currency = PlayerPrefs.GetInt(FishCurrency);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currency = 0;
            UpdateCurrency();

            showBaristaTuorial = 1;
            UpdateBaristaGameTutoiral();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            currency = 10000;
            UpdateCurrency();
        }
    }

    public static void UpdateCurrency()
    {
        PlayerPrefs.SetInt(FishCurrency, currency);
        currency = PlayerPrefs.GetInt(FishCurrency);
        PlayerPrefs.Save();
    }

    public static void UpdateBaristaGameTutoiral()
    {
        PlayerPrefs.SetInt(showBaristaGameTutorial, showBaristaTuorial);
        showBaristaTuorial = PlayerPrefs.GetInt(showBaristaGameTutorial);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Allows for easy scaling of score to currency after completion of minigames.
    /// Calls CurrencyManager.UpdateCurrency();
    /// Returns fish gained.
    /// </summary>
    /// <param name="score">The score the player earned in the minigame</param>
    /// <param name="multiplier">The game's currency multiplier</param>
    public static int ScoreToCurrency(int score, int multiplier)
    {
        int currencyGain = score * multiplier;
        currency += currencyGain;
        UpdateCurrency();
        return currencyGain;
    }
}
