using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    Vector3 movementDirection = new Vector3();
    Vector3 movement;
    Vector3 newPos;
    [SerializeField] Transform mTransform;
    [SerializeReference] Rigidbody mRigidBody;
    [SerializeReference] float movementSpeed;
    [SerializeReference] Dpad moveDpad;
    [SerializeReference] Dpad rotateDpad;
    #endregion
    #region Monobehaviour callbacks
    private void Update()
    {

        movement = moveDpad.Direction;
     
          

    }
    private void FixedUpdate()
    {
        Move();
    }
    #endregion
    #region Functions
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
        newPos = mTransform.position + movement * Time.deltaTime*movementSpeed;
        mRigidBody.MovePosition(newPos);
    }
    #endregion
}
