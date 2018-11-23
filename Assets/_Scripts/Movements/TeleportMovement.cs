using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMovement : MonoBehaviour
{
    [SerializeField]
    Renderer line;

    [SerializeField]
    Transform rightHand;

    [SerializeField]
    VRInputLookup VRInput;

    [SerializeField]
    float turnDegrees = 15f;

    [SerializeField]
    float turnArea = 0.75f;

    [SerializeField]
    float teleportationOffset;

    bool turnedRight;
    bool turnedLeft;
    bool teleported;

    

    private void Update()
    {
        Teleportation();
        Turn();
    }

    private void Teleportation()
    {
        if (VRInput.Right.Button2_Touch.IsPressed)
        {
            line.enabled = true;
        }
        else
        {
            line.enabled = false;
        }

        if (VRInput.Right.Button2.IsPressed)
        {
            if (!teleported)
            {
                Debug.DrawRay(rightHand.position, rightHand.forward * 100f, Color.cyan);

                RaycastHit hit;

                print("attempting tele");
                if(Physics.Raycast(rightHand.position, rightHand.forward * 100f, out hit))
                {
                    Vector3 dir = hit.point - transform.position;
                    dir.y = 0f;
                    dir = dir.normalized * teleportationOffset;

                    Vector3 target = hit.point + dir;
                    target.y = transform.position.y;

                    transform.position = target;

                    teleported = true;

                    print("tele to: " + target);

                }
                else
                {
                    print("tele failed");
                }
            }
        }
        else if (teleported)
        {
            print("tele reset");
            teleported = false;
        }
    }

    private void Turn()
    {
        float input = VRInput.Right.ThumbAxes.X;

        if (turnedRight)
        {
            if(input < turnArea)
            {
                turnedRight = false;
            }
        }
        else
        {
            if (input > turnArea)
            {
                turnedRight = true;
                transform.eulerAngles += Vector3.up * turnDegrees;
            }
        }

        if(turnedLeft)
        {
            if (input > -turnArea)
            {
                turnedLeft = false;
            }
        }
        else
        {
            if(input < -turnArea)
            {
                turnedLeft = true;
                transform.eulerAngles -= Vector3.up * turnDegrees;
            }
        }
    }
}
