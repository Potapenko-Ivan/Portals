using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlSettings : MonoBehaviour
{
    [SerializeField] public float CamSensY = 5, CamSensX = 5;
    [SerializeField] public float CamMaxX = 360, CamMinX = -360, CamMaxY = 40, CamMinY = -40;
    [SerializeField] public float Speed =5,JumpForce=5,Acceleretion=2, ForceCTRL=2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
