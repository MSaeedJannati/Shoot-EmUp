using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    #region Variables
   [SerializeField] float InitX;
    [SerializeField]float FinX;
    [SerializeField]float DeltaX;
    /*  [HideInInspector]*/
    public Transform CameraTransform;

   


    public Vector3 pos;
    bool IsMoving;
    #endregion
    #region Mono CallBacks

    private void Update()
    {
        swipe();
    }
    #endregion
    #region Functions
    public void swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isMouseInSwipeArea())
            {
                IsMoving = true;
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
        }

    }

    bool isMouseInSwipeArea()
    {
        return Input.mousePosition.x > Screen.width / 2 && Input.mousePosition.y > Screen.height / 2;
    }
    #endregion
    #region Coroutines

 
    #endregion
}
