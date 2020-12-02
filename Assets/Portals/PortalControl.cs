using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControl : MonoBehaviour
{
    public Camera PortalCam_1, PortalCam_2;
    public GameObject Portal_1, Portal_2;
    public GameObject Player;

    void Start()
    {
        
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.transform.position = new Vector3(Portal_2.transform.position.x, gameObject.transform.position.y, Portal_2.transform.position.z);
           // Player.transform.eulerAngles = new Vector3(0,180,0);
            print("");
        }
    }
    private void CamPosition(Transform portal,Transform portal2, Transform player,Transform cam)
    {
        float PortalCamNewPosX = (portal2.position.x + portal.position.x - player.position.x);
        float PortalCamNewPosY = (portal2.position.y + portal.position.y - player.position.y);
        float PortalCamNewPosZ = (portal2.position.z + portal.position.z - player.position.z);
       // cam.position = new Vector3(PortalCamNewPosX,PortalCamNewPosY, PortalCamNewPosZ );

        float DiagXZ = Mathf.Sqrt(Mathf.Pow(portal.position.x - player.position.x, 2) + Mathf.Pow(portal.position.z - player.position.z, 2));
        float aY= Mathf.Sqrt(Mathf.Pow(portal.position.x - player.position.x, 2) + Mathf.Pow(portal.position.y - player.position.y, 2));
        float bY= portal.position.y - player.position.y;
        float DiagAYY = Mathf.Sqrt(Mathf.Pow(aY, 2) + Mathf.Pow(bY, 2));

        float a = portal.position.x - player.position.x;
        float b = portal.position.z - player.position.z;
        float PortalCamNewRotX = Mathf.Acos(Mathf.Abs(b) / DiagXZ)* Mathf.Rad2Deg;// +Mathf.Acos(b/DiagXZ);
      //  float PortalCamNewRotY = Mathf.Acos(aY / DiagAYY) * Mathf.Rad2Deg;// + Mathf.Acos(b / DiagAYY);
     //   float PortalCamNewRotZ = 0;
        cam.eulerAngles = new Vector3(0,ToNormal(180+PortalCamNewRotX)*Mathf.Sign(a), 0);
    }

    public float ToNormal(float angle)
    {
        if (angle > 360f) angle = angle - 360f;
        if (angle < -360f) angle = angle + 360f;

        return angle;
    }


    void Update()
    {
        CamPosition(Portal_1.transform,Portal_2.transform, Player.transform, PortalCam_1.transform);
        CamPosition(Portal_2.transform, Portal_1.transform,  Player.transform, PortalCam_2.transform);


    }
}
