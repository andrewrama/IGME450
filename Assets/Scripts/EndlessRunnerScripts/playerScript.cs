using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerScript : MonoBehaviour
{
    private PlayerControls playerControls;

    private int velocity = 1;
    private Vector2 screenBounds;
    public LogicScript logic;
    private float currentX;
    //public InputAction leftAction;
    //public InputAction rightAction;


    // center is 108
    // furthest left side is 54 
    // furthest right side is 162

    private void Awake()
    {
        playerControls = new PlayerControls();
        //leftAction.performed += MoveLeft;
        //rightAction.performed += MoveRight;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)); //gives us half the screen width and half the screen height (but they're negative values!)
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        //playerControls.PlayerMovement.MoveLeft.performed += MoveLeft;
    }

    // Update is called once per frame
    void Update()
    {
        currentX = transform.position.x;

        if (playerControls.PlayerMovement.MoveLeft.triggered)
        {
            MoveLeft();
            Debug.Log("Left!");
        }
        
        if (playerControls.PlayerMovement.MoveRight.triggered)
        {
            MoveRight();
            Debug.Log("Right!");
        }

        // check if player has clicked 'A' or 'D'
        // move left or right

        //// left
        //if (Input.GetKeyDown(KeyCode.A)) // || gesture
        //{
        //    Debug.Log("A pressed");
        //    if (transform.position.x == logic.centerPoint)
        //    {
        //        transform.position = new Vector3(logic.leftEdge, 194, 0);
        //    }
        //    else if (transform.position.x > logic.centerPoint)
        //    //else if(transform.position.x == logic.rightEdge)
        //    {
        //        transform.position = new Vector3(logic.centerPoint, 194, 0);
        //    }
        //
        //}

        //// right
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    Debug.Log("D pressed");
        //    if (transform.position.x == logic.centerPoint)
        //    {
        //        transform.position = new Vector3(logic.rightEdge, 194, 0);
        //    }
        //    else if (transform.position.x < logic.centerPoint)
        //    //else if (transform.position.x == logic.leftEdge)
        //    {
        //        transform.position = new Vector3(logic.centerPoint, 194, 0);
        //    }
        //
        //
        //}

    }

    //public void MoveLeft(InputAction.CallbackContext context)
    public void MoveLeft()

    {
        //    if (transform.position.x == logic.centerPoint)
        //    {
        //        transform.position = new Vector3(logic.leftEdge, 194, 0);
        //    }
        //    else if (transform.position.x > logic.centerPoint)
        //    //else if(transform.position.x == logic.rightEdge)
        //    {
        //        transform.position = new Vector3(logic.centerPoint, 194, 0);
        //    }

        if (transform.position.x != logic.leftEdge)
        {
            if (transform.position.x == logic.centerPoint)
            {
                transform.position = new Vector3(logic.leftEdge, 194, 0);
            }
            else if (transform.position.x == logic.rightEdge)
            {
                transform.position = new Vector3(logic.centerPoint, 194, 0);
            }
        }
    }

    //public void MoveRight(InputAction.CallbackContext context)
    public void MoveRight()
    {
        //if (transform.position.x == logic.centerPoint)
        //{
        //    transform.position = new Vector3(logic.rightEdge, 194, 0);
        //}
        //else if (transform.position.x < logic.centerPoint)
        ////else if (transform.position.x == logic.leftEdge)
        //{
        //    transform.position = new Vector3(logic.centerPoint, 194, 0);
        //}

        if (transform.position.x != logic.rightEdge)
        {
            if (transform.position.x == logic.centerPoint)
            {
                transform.position = new Vector3(logic.rightEdge, 194, 0);
            }
            else if (transform.position.x == logic.leftEdge)
            {
                transform.position = new Vector3(logic.centerPoint, 194, 0);
            }
        }
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