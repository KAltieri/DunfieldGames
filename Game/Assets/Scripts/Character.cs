using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed;

    private CharacterController controller;
    float horizontalMovement = 0.0f;
    float verticalMovement = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(horizontalMovement, verticalMovement, 0.0f);
        Debug.Log(move);
        move = transform.TransformDirection(move);

        if (horizontalMovement != 0 && verticalMovement != 0) 
        {
            move *= (speed - 1);
        }
        else
        {
            move *= speed;
        }

        controller.Move(move * Time.deltaTime);
    }
}
