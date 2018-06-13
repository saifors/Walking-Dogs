using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookRotation : MonoBehaviour
{

    private Vector2 axisRotation;

    private Transform cameraTransform;
    private Quaternion cameraRot; // Tranform is everything ,this one is included into it but it´s seperate for easier modification of rotation.

    private Transform playerTransform;
    private Quaternion playerRot; //Done seperately because otherwise the camera rotates weirdly.

    public bool smooth;
    public float smoothSpeed;

    public bool limitCameraRot;
    public float minAngle = -60;
    public float maxAngle = 60;

	// Use this for initialization
	void Start ()
    {
        cameraTransform = Camera.main.transform; //This has a lot of processing power needed so don't put in uodate unles through a loophole with a boolean or sth.
        cameraRot = cameraTransform.localRotation;

        playerTransform = transform;
        playerRot = playerTransform.localRotation;
	}
	
	// Update is called once per frame
	void Update ()
    {
        cameraRot *= Quaternion.Euler(-axisRotation.y, 0, 0);
        playerRot *= Quaternion.Euler(0, axisRotation.x, 0);

        if(limitCameraRot) cameraRot = ClampRotationAroundXAxis(cameraRot);

        if(smooth)
        {
            cameraTransform.localRotation = Quaternion.Slerp(cameraTransform.localRotation, cameraRot, Time.deltaTime * smoothSpeed);
            playerTransform.localRotation = Quaternion.Slerp(playerTransform.localRotation, playerRot, Time.deltaTime * smoothSpeed);

        }
        else
        {
            cameraTransform.localRotation = cameraRot;
            playerTransform.localRotation = playerRot;
        }
        
    }

    public void SetMouseAxis(Vector2 mouseAxis)
    {
        axisRotation = mouseAxis;
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, minAngle, maxAngle);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
