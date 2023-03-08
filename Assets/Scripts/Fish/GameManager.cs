using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Image progressBar;

    RectTransform progressBarRectTransform;

    private int num;
    void Start()
    {
        progressBarRectTransform = progressBar.rectTransform;
        num = 0;
    }

    // Update is called once per frame
    void Update()
    {
        num = (num + 1) % 101;
        progressBarRectTransform.anchorMax = new Vector2(num / 100f, 1);
        Debug.Log(num);
    }
}
