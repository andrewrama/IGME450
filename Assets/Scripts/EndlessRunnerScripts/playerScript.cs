using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerScript : MonoBehaviour
{
    private PlayerControls playerControls;
    public LogicScript logic;

    // center is 108
    // furthest left side is 54 
    // furthest right side is 162

    private void Awake()
    {
        playerControls = new PlayerControls();
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
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControls.PlayerMovement.MoveLeft.triggered)
        {
            MoveLeft();
        }
        
        if (playerControls.PlayerMovement.MoveRight.triggered)
        {
            MoveRight();
        }
    }

    public void MoveLeft()
    {
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

    public void MoveRight()
    {
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