using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    private void Update()
    {
        var inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
        }

        inputVector = inputVector.normalized;

        var moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        var playerTransform = transform;
        playerTransform.position += moveDir * (Time.deltaTime * speed);
        playerTransform.forward = Vector3.Slerp(playerTransform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }
}
