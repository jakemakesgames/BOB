using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothTime = .15f;

    Vector3 velocity = Vector3.zero;

    #region CAM CLAMP
    [Header("CamPos Clamp")]
    [Header("Y Max Values")]
    // Enable and set the max Y values //
    public bool yMaxEnabled = false;
    public float yMaxValue = 0;

    [Header("Y Min Values")]
    // Enable and set the min Y values //
    public bool yMinEnabled = false;
    public float yMinValue = 0;

    [Header("X Max Values")]
    // Enable and set the max X values //
    public bool xMaxEnabled = false;
    public float xMaxValue = 0;

    [Header("X Min Values")]
    // Enable and set the min X values //
    public bool xMinEnabled = false;
    public float xMinValue = 0;
    #endregion

    void FixedUpdate()
    {
        
        Vector3 targetPos = target.position;

        // Vertical
        if (yMinEnabled && yMaxEnabled)
        {
            targetPos.y = Mathf.Clamp(target.position.y, yMinValue, yMaxValue);
        }
        else
        if (yMinEnabled)
        {
            targetPos.y = Mathf.Clamp(target.position.y, yMinValue, target.position.y);
        }
        else
        if (yMaxEnabled)
        {
            targetPos.y = Mathf.Clamp(target.position.y, target.position.y, yMaxValue);
        }

        // Horizontal
        if (xMinEnabled && xMaxEnabled)
        {
            targetPos.x = Mathf.Clamp(target.position.x, xMinValue, xMaxValue);
        }
        else
        if (xMinEnabled)
        {
            targetPos.x = Mathf.Clamp(target.position.x, xMinValue, target.position.x);
        }
        else
        if (xMaxEnabled)
        {
            targetPos.x = Mathf.Clamp(target.position.x, target.position.x, xMaxValue);
        }








        targetPos.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}

