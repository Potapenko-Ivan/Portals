using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamRotation : MonoBehaviour
{
    [SerializeField] public float DirectionDifferenceY=0, DirectionDifferenceX=0;
    [SerializeField] private PlayerControlSettings PlayerControlSettings;
    [SerializeField] private Camera Camera;

    void Start()
    {
        PlayerControlSettings = GetComponent<PlayerControlSettings>();
        Camera =transform.GetComponentInChildren<Camera>();
    }

    private void CamRotate()
    {
        DirectionDifferenceX += Input.GetAxis("Mouse X") * PlayerControlSettings.CamSensX;
        DirectionDifferenceY -= Input.GetAxis("Mouse Y") * PlayerControlSettings.CamSensY;

        DirectionDifferenceX = CamClamp(DirectionDifferenceX,PlayerControlSettings.CamMaxX, PlayerControlSettings.CamMinX);

        DirectionDifferenceY = CamClamp(DirectionDifferenceY, PlayerControlSettings.CamMaxY, PlayerControlSettings.CamMinY);

        Camera.transform.rotation = Quaternion.Euler( DirectionDifferenceY, DirectionDifferenceX, 0);
      

    }

    public float CamClamp(float angle,float max,float min)
    {
        if (angle > 360f) angle = angle - 360f;
        if (angle < -360f) angle = angle + 360f;

        return Mathf.Clamp(angle,min,max);
    }
   

    void Update()
    {
        CamRotate();
    }
}
