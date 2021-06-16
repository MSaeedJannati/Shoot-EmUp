using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    Vector3 movementDirection = new Vector3();
    Vector3 movement;
    Vector3 newPos;
    [SerializeField] Transform mTransform;
    [SerializeField] CharacterController mController;
    [SerializeField] float movementSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] Dpad moveDpad;

    float eulerY;

    #endregion
    #region Monobehaviour callbacks
    private void OnEnable()
    {
        PlayerManager.update += _Update;
    }
    private void OnDisable()
    {
        PlayerManager.update -= _Update;
    }
    private void Start()
    {
        eulerY = mTransform.eulerAngles.y;
    }
    //private void Update()
    //{

   


    //}
    #endregion
    #region Functions
    public void _Update()
    {
        movement = moveDpad.Direction.z * mTransform.forward + moveDpad.Direction.x * mTransform.right;
        Move();
    }
    public Action<float>   getRotateFunction()
    {
        return rotatePlayer;
    }
    void rotatePlayer(float deltaX)
    {
        if (deltaX * deltaX > 0)
        {
            eulerY += deltaX * rotateSpeed * Time.deltaTime;
            mTransform.rotation = Quaternion.Euler(0.0f, eulerY, 0.0f);
        }
    }
    Vector3 getUserInput()
    {
        movementDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            movementDirection.z = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            movementDirection.z = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            movementDirection.x = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            movementDirection.x = -1;
        }
        return movementDirection.normalized;
    }
    void Move()
    {
        //mTransform.localPosition = mTransform.localPosition + movement * Time.deltaTime * movementSpeed;
       mController.Move(movement * Time.deltaTime * movementSpeed);
        //movement = mTransform.TransformPoint(movement);
        //mRigidBody.MovePosition(movement);
    }
    #endregion
}
