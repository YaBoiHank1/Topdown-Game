using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_QuestPointer : MonoBehaviour
{
    [SerializeField] private Camera uiCamera;
    [SerializeField] float borderSize = 100f;
    [SerializeField] Vector3 capturePoint;
    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;
    // Start is called before the first frame update
    private void Awake()
    {
        targetPosition = new Vector3(capturePoint.x, capturePoint.y);
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = Vector3.Angle(dir, transform.up);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

        
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= borderSize || targetPosition.y >= borderSize;
        //Debug.Log(isOffScreen + " " + targetPositionScreenPoint);

        if (isOffScreen)
        {
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize;
            if (cappedTargetScreenPosition.x >= borderSize) cappedTargetScreenPosition.x = borderSize;
            if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = borderSize;
            if (cappedTargetScreenPosition.y >= borderSize) cappedTargetScreenPosition.y = borderSize;

            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }
        else
        {
            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }
    }
}
