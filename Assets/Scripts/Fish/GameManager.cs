using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Image filledProgressBar;

    [SerializeField]
    private float increment;

    RectTransform progressBarRectTransform;

    private float num;
    void Start()
    {
        progressBarRectTransform = filledProgressBar.rectTransform;
        num = 0;
    }

    // Update is called once per frame
    void Update()
    {
        num = (num + increment * Time.deltaTime) % 101;
        progressBarRectTransform.anchorMax = new Vector2(num / 100f, 1);
        Debug.Log(num);
    }
}
