using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControlMove : MonoBehaviour
{
    [SerializeField] private PlayerControlSettings PlayerControlSettings;
    [SerializeField] private Rigidbody Rigidbody;
    [SerializeField] private PlayerCamRotation PlayerCamRotation;
    [SerializeField] private bool Grounded = false;
    [SerializeField] float angle = 0;
    [SerializeField] Animator Animator;
    [SerializeField] bool TrueInCTRL = false;
    [SerializeField] Collider ColOffOnCTRL1, ColOffOnCTRL2, ColOnOnCTRL;



    void Start()
    {
        PlayerControlSettings = GetComponent<PlayerControlSettings>();
        PlayerCamRotation = GetComponent<PlayerCamRotation>();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Ground")
        {
            Grounded = true;
        }
    }


    private void ButtonsInput()
    {
        float camDirection = PlayerCamRotation.DirectionDifferenceX;
        float speed = PlayerControlSettings.Speed;

        
        angle = camDirection;

        bool isMove = false;

        if(Input.GetKeyDown(KeyCode.LeftControl)&&!TrueInCTRL&&Grounded)
        {
            Animator.Play("SlidingPlayerAnimation");
            TrueInCTRL = true;
            Rigidbody.AddForce(new Vector3(Mathf.Sin(Mathf.Deg2Rad * ToNormal(angle)),0, Mathf.Cos(Mathf.Deg2Rad * ToNormal(angle))) * PlayerControlSettings.ForceCTRL, ForceMode.Impulse);
            ColOffOnCTRL1.enabled=false;
            ColOffOnCTRL2.enabled = false;
            ColOnOnCTRL.enabled = true;

        }

        if (!Input.GetKey(KeyCode.LeftControl) || !TrueInCTRL)
        {


            if (Input.GetKey(KeyCode.W))
            {
                isMove = true;
                if (Input.GetKey(KeyCode.D))
                {
                    angle += 45;
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    angle -= 45;
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                angle += 180;
                isMove = true;
                if (Input.GetKey(KeyCode.D))
                {
                    angle -= 45;
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    angle += 45;
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                angle += 90;
                isMove = true;

            }
            else if (Input.GetKey(KeyCode.A))
            {
                angle += 270;
                isMove = true;
            }

        }
        else
        {
            //isMove = true;
            /*  if (Input.GetKey(KeyCode.D))
              {
                  angle += 45;
              }
              else if (Input.GetKey(KeyCode.A))
              {
                  angle -= 45;
              }
              Move(Mathf.Sin(Mathf.Deg2Rad * ToNormal(angle)), Mathf.Cos(Mathf.Deg2Rad * ToNormal(angle)), speed, PlayerControlSettings.Acceleretion);
            */
           // Animator.Play("SlidePlayerAnimation");
        }

        if (Input.GetKeyUp(KeyCode.LeftControl)&&TrueInCTRL)
        {
            Animator.Play("StandUpPlayerAnimation");
            TrueInCTRL = false;
            ColOffOnCTRL1.enabled = true;
            ColOffOnCTRL2.enabled = true;
            ColOnOnCTRL.enabled = false;
        }

        if (isMove)
        {
            Move(Mathf.Sin(Mathf.Deg2Rad * ToNormal(angle)), Mathf.Cos(Mathf.Deg2Rad * ToNormal(angle)), speed,PlayerControlSettings.Acceleretion);

        }

        if (Input.GetKey(KeyCode.Space)&&Grounded&&!TrueInCTRL)
        {
            Rigidbody.AddForce(new Vector3(0,1,0)*PlayerControlSettings.JumpForce,ForceMode.Impulse);
            Grounded = false;
        }



    }


    public void Move(float moveDirectionX, float moveDirectionZ,float speed,float acceleration)
    {
        // Rigidbody.velocity = new Vector3(moveDirectionX*speed, Rigidbody.velocity.y, moveDirectionZ*speed);

        Rigidbody.AddForce(new Vector3(moveDirectionX, 0, moveDirectionZ)*acceleration,ForceMode.Impulse);
        if(Mathf.Abs(Rigidbody.velocity.x)>=speed)
        {
            Rigidbody.velocity = new Vector3(speed*Mathf.Sign(Rigidbody.velocity.x),Rigidbody.velocity.y, Rigidbody.velocity.z);
        }
        if (Mathf.Abs(Rigidbody.velocity.z)>= speed)
        {
            Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, Rigidbody.velocity.y, speed * Mathf.Sign(Rigidbody.velocity.z));
        }
       // print(Rigidbody.velocity.x+" "+Rigidbody.velocity.z);
    }



    public float ToNormal(float angle)
    {
        if (angle > 360f) angle = angle - 360f;
        if (angle < -360f) angle = angle + 360f;

        return angle;
    }



    void Update()
    {
        ButtonsInput();
    }
}
