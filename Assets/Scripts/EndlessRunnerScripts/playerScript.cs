using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour

{
    public int velocity;
    public Vector2 screenBounds;
    public LogicScript logic;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)); //gives us half the screen width and half the screen height (but they're negative values!)
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if player has clicked 'A' or 'D'
        // move left or right


        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(velocity, 0, 0);
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
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x*2, screenBounds.x*6);
        transform.position = viewPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("COLLISION");
        if(collision.gameObject.CompareTag("treat"))
        {
            Debug.Log("Collided with a treat!");
        }
        else if(collision.gameObject.CompareTag("box"))
        {
            Debug.Log("Collided with a box!");
        }

        //if (collision.gameObject.layer == 7)
        //{
        //    Debug.Log("Collided with a treat!");
        //    logic.getTreat();
        //}
        //if (collision.gameObject.layer == 6)
        //{
        //    Debug.Log("Collided with a box!");
        //}
    }
}