using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CurrencyManager.currency < 0)
        {
            CurrencyManager.currency = 0;
        }
    }

    public void OnButtonClicked()
    {
        CurrencyManager.currency++;
        CurrencyManager.UpdateCurrency();

        Debug.Log(CurrencyManager.currency);
    }
}
