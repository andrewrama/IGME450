using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int velocity;
    public Vector2 screenBounds;
    public LogicScript logic;
    float centerPoint;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)); //gives us half the screen width and half the screen height (but they're negative values!)
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        centerPoint = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // check if player has clicked 'A' or 'D'
        // move left or right

        if (Input.GetKey(KeyCode.A)) // || gesture
        {
            Debug.Log("A pressed");
            transform.position -= new Vector3(velocity, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("D pressed");
            transform.position += new Vector3(velocity, 0, 0);
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
        Debug.Log("collision");
        if (collision.gameObject.CompareTag("treatPickup"))
        {
            Debug.Log("collided with a treat!");
        }
        else if (collision.gameObject.CompareTag("boxObstacle"))
        {
            Debug.Log("collided with a box!");
        }

        //if (collision.gameobject.layer == 7)
        //{
        //    debug.log("collided with a treat!");
        //    logic.gettreat();
        //}
        //if (collision.gameobject.layer == 6)
        //{
        //    debug.log("collided with a box!");

        //}
    }

}