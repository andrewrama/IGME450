using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int velocity;
    public Vector2 screenBounds;
    public LogicScript logic;

    // center is 108
    // furthest left side is 54 
    // furthest right side is 162

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)); //gives us half the screen width and half the screen height (but they're negative values!)
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        //centerPoint = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // check if player has clicked 'A' or 'D'
        // move left or right

        // left
        if (Input.GetKeyDown(KeyCode.A)) // || gesture
        {
            Debug.Log("A pressed");
            //transform.position -= new Vector3(velocity, 0, 0);
            if (transform.position.x == logic.centerPoint)
            {
                transform.position = new Vector3(logic.leftEdge, 194, 0);
            }
            else if (transform.position.x > logic.centerPoint)
            {
                transform.position = new Vector3(logic.centerPoint, 194, 0);
            }

        }

        // right
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D pressed");
            //transform.position += new Vector3(velocity, 0, 0);
            if (transform.position.x == logic.centerPoint)
            {
                transform.position = new Vector3(logic.rightEdge, 194, 0);
            }
            else if (transform.position.x < logic.centerPoint)
            {
                transform.position = new Vector3(logic.centerPoint, 194, 0);
            }


        }

    }

    private void LateUpdate()
    {
        // clamping to screen
        Vector3 viewPos = transform.position; // used to alter x and y
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x*2, screenBounds.x*6);
        transform.position = viewPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // increase score when treat is collected, remove treat from screen
        if (collision.gameObject.CompareTag("treatPickup"))
        {
            logic.GetTreat();
            Destroy(collision.gameObject);

        }
        // end the game on collision with a box
        else if (collision.gameObject.CompareTag("boxObstacle"))
        {
            logic.GameOver();
        }
    }

}