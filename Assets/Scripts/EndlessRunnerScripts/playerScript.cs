using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour

{
    public int velocity;
    public Canvas gameScreen;
    float screenHeight;
    float screenWidth;

    // Start is called before the first frame update
    void Start()
    {
        gameScreen = FindObjectOfType<Canvas>();
        screenWidth = gameScreen.GetComponent<RectTransform>().rect.width;
        screenHeight = gameScreen.GetComponent<RectTransform>().rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        // check if player has clicked 'A' or 'D'
        // move left or right

        if (Input.GetKey(KeyCode.A) && transform.position.x >= 0 )
        {
            transform.position -= new Vector3(velocity, 0, 0);
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x <= screenWidth)
        {
            transform.position += new Vector3(velocity, 0, 0);
        }

    }
}
