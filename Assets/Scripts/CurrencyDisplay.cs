using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyDisplay : MonoBehaviour
{
    private Text text;

    private JSONReader jsonScript;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        jsonScript = transform.Find("/Reader").gameObject.GetComponent<JSONReader>();
    }

    // Update is called once per frame
    void Update()
    {
        string[] temp = text.text.Split(' ');
        text.text = temp[0] + "  " + jsonScript.Currency;
    }
}
