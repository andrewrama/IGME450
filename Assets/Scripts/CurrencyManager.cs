using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public const string FishCurrency = "Currency";
    public static int currency = 0;

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
}
