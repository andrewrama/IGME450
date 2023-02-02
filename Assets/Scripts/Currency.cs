using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    private int amount;

    public Text label;

    // Start is called before the first frame update
    void Start()
    {
        amount = 0;
        Debug.Log(amount);
    }

    // Update is called once per frame
    void Update()
    {
        label.text = $"Click Me ({amount})";
        Debug.Log(amount);

        if (amount < 0)
        {
            amount = 0;
        }
    }

    public void OnButtonClicked()
    {
        amount++;
    }
}
