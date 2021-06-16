using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dpad : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    #region Variables

    [SerializeField] float overlapFactor;
    [SerializeField] Image backGround;
    [SerializeField] Transform foreGroundTrnsfrm;
    [SerializeField] RectTransform backGroundRect;
    Vector3 destPos = new Vector3();
    //corners
    Vector3[] backCorners = new Vector3[4];
    Coroutine goBackToCenterRoutine;
    Vector3 direction;
    float radious;
    #endregion
    #region Properties
    public Vector3 Direction => direction;
    #endregion
    #region Monobehaviour callbacks
    private void Start()
    {
        backGroundRect.GetWorldCorners(backCorners);

        radious = (backCorners[0] - backGroundRect.position).magnitude;
        radious *= overlapFactor;
    }

    public void OnDrag(PointerEventData eventData)
    {

        destPos = eventData.position;
        destPos.z = foreGroundTrnsfrm.position.z;

        CalcForeGroundPos(ref destPos);
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
        direction = (foreGroundTrnsfrm.position - backGroundRect.position) / backGroundRect.sizeDelta.x;
        if (direction.sqrMagnitude > 1)
        {
            direction = direction.normalized;
        }

        direction.z = direction.y;
        direction.y = 0;
    }
    public void CalcForeGroundPos(ref Vector3 destPos)
    {
        if ((destPos - backGroundRect.position).sqrMagnitude > radious * radious)
        {
            foreGroundTrnsfrm.position = backGroundRect.position + radious * ((destPos - backGroundRect.position).normalized);
        }
        else
        {
            foreGroundTrnsfrm.position = destPos;
        }
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
