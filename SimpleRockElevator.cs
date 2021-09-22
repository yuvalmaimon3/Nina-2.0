using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRockElevator : MonoBehaviour
{
    [SerializeField]
    private Transform topLimit;
    [SerializeField]
    private Transform botLimit;
    
    private Rigidbody elevatorRigidbody;
    private string direction = "up";
    private float speed = 3f;
    private Vector3 downDirection;


    void Start()
    {
        elevatorRigidbody = GetComponent<Rigidbody>();
        downDirection = new Vector3(0, -1, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        MoveElevator();
    }
    private void MoveElevator()
    {
        if(direction == "up")
        {
            elevatorRigidbody.transform.position += transform.up * Time.deltaTime * speed;
            if (transform.position.y >= topLimit.position.y) //When the elevator rich to the top point it's have to return direction
                direction = "down";

        }
        else if(direction == "down")
        {
            elevatorRigidbody.transform.position += downDirection * Time.deltaTime * speed;
            if (transform.position.y <= botLimit.position.y) //When the elevator rich to the bot point it's have to return direction
                direction = "up";
        }

    }
}
