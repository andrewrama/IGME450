using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour

{
    public int velocity;

    public Vector2 screenBounds;

    //public Camera gameCamera;

    // Start is called before the first frame update
    void Start()
    {
        //gameCamera = FindObjectOfType<Camera>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)); //gives us half the screen width and half the screen height (but they're negative values!)
    }

    // Update is called once per frame
    void Update()
    {
        // check if player has clicked 'A' or 'D'
        // move left or right
        // + gameScreen.GetComponent<RectTransform>().rect.width
        // + gameScreen.GetComponent<RectTransform>().rect.width
        // transform.position.x >= (gameCamera.transform.position.x - (gameCamera.fieldOfView/2))


        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(velocity, 0, 0);
            //transform.position -= new Vector3(Mathf.Clamp(transform.position.x, gameScreen.GetComponent<RectTransform>().rect.x, gameScreen.GetComponent<RectTransform>().rect.width), 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(velocity, 0, 0);
        }

    }

    private void LateUpdate()
    {
        // clamping to screen
        Vector3 viewPos = transform.position; // used to alter x and y
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x, screenBounds.x - 1);
        transform.position = viewPos;
    }
}