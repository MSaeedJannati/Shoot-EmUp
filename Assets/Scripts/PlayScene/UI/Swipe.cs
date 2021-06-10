using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Swipe : MonoBehaviour
{
    #region Variables
   float InitX;
   float FinX;
   float DeltaX;
    /*  [HideInInspector]*/
    [SerializeField] Transform CameraTransform;
     Action<float> playerRotate;
    #endregion
    #region Mono CallBacks
    private void OnEnable()
    {
        PlayerManager.update += _Update;
    }
    private void Start()
    {
        if (TryGetComponent(out PlayerMovement movement))
        {
            playerRotate = movement.getRotateFunction();
        }
    }
    private void OnDisable()
    {
        PlayerManager.update -= _Update;
    }
    //private void Update()
    //{

    //}
    #endregion
    #region Functions
    public void _Update()
    {
        swipe();
        playerRotate?.Invoke(DeltaX);
    }
    public void swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isMouseInSwipeArea())
            {
                InitX = Input.mousePosition.x;
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (isMouseInSwipeArea())
            {
                FinX = Input.mousePosition.x;
                DeltaX = FinX - InitX;
                DeltaX = DeltaX / Screen.width * 20;
                InitX = Input.mousePosition.x;
            }
            else
            {
                DeltaX = 0;
            }
        }
        else
        {
            DeltaX = 0;
        }
    }

    bool isMouseInSwipeArea()
    {
        return /*Input.mousePosition.x > Screen.width / 2 &&*/ Input.mousePosition.y > Screen.height / 2;
    }
    #endregion
}
