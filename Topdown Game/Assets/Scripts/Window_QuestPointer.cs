using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_QuestPointer : MonoBehaviour
{
    [SerializeField] private Camera uiCamera;
    [SerializeField] float borderSizeX = 100f;
    [SerializeField] float borderSizeY = 100f;
    [SerializeField] Transform capturePoint;
    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;
    // Start is called before the first frame update
    private void Awake()
    {
        targetPosition = new Vector3(-35, -24);
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
        bool isOffScreen = targetPositionScreenPoint.x <= borderSizeX || targetPositionScreenPoint.x >= borderSizeX || targetPosition.y >= borderSizeX;
        //Debug.Log(isOffScreen + " " + targetPositionScreenPoint);

        if (isOffScreen)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, borderSizeX, Screen.width - borderSizeX);
            cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.x, borderSizeY, Screen.width - borderSizeY);

            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
