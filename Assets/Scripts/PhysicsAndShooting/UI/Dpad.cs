using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dpad : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    #region Variables
    [SerializeField] Image backGround;
    [SerializeField] Transform foreGroundTrnsfrm;
    [SerializeField] RectTransform backGroundRect;
    Vector3 destPos = new Vector3();
    [SerializeField] int cornerID;
    //corner
    Vector3[] backCorners = new Vector3[4];
    Coroutine goBackToCenterRoutine;

    [SerializeField] Transform obj;
    [SerializeField] Vector3 direction;
    #endregion
    #region Properties
   public  Vector3 Direction => direction;
    #endregion
    #region Monobehaviour callbacks
    private void Start()
    {

        backGroundRect.GetWorldCorners(backCorners);
    }

    public void OnDrag(PointerEventData eventData)
    {

        destPos = eventData.position;
        destPos.z = foreGroundTrnsfrm.position.z;

        if (CheckIfInsideRect(ref destPos))
        {

            foreGroundTrnsfrm.position = destPos;
        }
        CalcDirection();
       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (goBackToCenterRoutine != null)
            StopCoroutine(goBackToCenterRoutine);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        goBackToCenterRoutine = StartCoroutine(goBackToCenterCoroutine());
    }
    #endregion
    #region Functions
    public void CalcDirection()
    {
        direction = (foreGroundTrnsfrm.position - backGroundRect.position).normalized;
        direction.z = direction.y;
        direction.y = 0;
    }
    public bool CheckIfInsideRect(ref Vector3 destPos)
    {
        if (destPos.x < backCorners[0].x)
        {
            return false;
        }
        if (destPos.y < backCorners[0].y)
        {
            return false;
        }
        if (destPos.x > backCorners[2].x)
        {
            return false;
        }
        if (destPos.y > backCorners[2].y)
        {
            return false;
        }

        return true;
    }
    #endregion
    #region Coroutines
    public IEnumerator goBackToCenterCoroutine()
    {
        direction = Vector3.zero;
        float period = .2f;
        float t = 0.0f;
        Vector3 initPos = foreGroundTrnsfrm.position;
        Vector3 dest = backGroundRect./*transform.*/position;
        while (t < period)
        {
            foreGroundTrnsfrm.position = initPos + (dest - initPos) * (/*t/period*/  Mathf.Lerp(0, 1, t / period));
            t += Time.deltaTime;
            yield return null;
        }
        foreGroundTrnsfrm.position = dest;
    }
    #endregion

}
