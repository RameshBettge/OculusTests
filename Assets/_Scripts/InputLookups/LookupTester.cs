using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookupTester : MonoBehaviour
{
    [SerializeField]
    VRInputLookup c;

    void Update()
    {
        //CheckThumbsticks();
        //Test();

        TestVRController();
    }

    private void TestVRController()
    {
        //print(c.Right.Button1.kC.ToString());
        if (c.Right.Button1.OnDown)
        {
            print("button1 down");
        }
    }

    private void Test()
    {
        // r controller buttons
        if (Input.GetKeyDown(c.rButton1))
        {
            print("rButton1 pressed");
        }
        if (Input.GetKeyDown(c.rButton1_Touch))
        {
            print("rButton1 touched");
        }
        if (Input.GetKeyDown(c.rButton2))
        {
            print("rButton2 pressed");
        }
        if (Input.GetKeyDown(c.rButton2_Touch))
        {
            print("rButton2 touched");
        }

        // l controller Buttons
        if (Input.GetKeyDown(c.lButton1))
        {
            print("lButton3 pressed");
        }
        if (Input.GetKeyDown(c.lButton1_Touch))
        {
            print("lButton3 touched");
        }
        if (Input.GetKeyDown(c.lButton2))
        {
            print("lButton4 pressed");
        }
        if (Input.GetKeyDown(c.lButton2_Touch))
        {
            print("lButton4 touched");
        }

        // Grabs
        if (Input.GetAxis(c.rGrab) > 0.05f)
        {
            print("R Grab: " + Input.GetAxis(c.rGrab));
        }
        if (Input.GetAxis(c.lGrab) > 0.05f)
        {
            print("L Grab: " + Input.GetAxis(c.lGrab));
        }

        // Index Squeeze
        if (Input.GetAxis(c.rIndex) > 0.05f)
        {
            print("R Index: " + Input.GetAxis(c.rIndex));
        }
        if (Input.GetAxis(c.lIndex) > 0.05f)
        {
            print("L Index: " + Input.GetAxis(c.lIndex));
        }

        // Index touches
        if (Input.GetKey(c.rIndex_Touch))
        {
            print("R Index touched.");
        }
        if (Input.GetKey(c.lIndex_Touch))
        {
            print("L Index touched.");
        }

        // Thumb touches&presses
        if (Input.GetKey(c.rThumb_Touch))
        {
            print("R Thumb touched.");
        }
        if (Input.GetKey(c.lThumb_Touch))
        {
            print("L Thumb touched.");
        }

        if (Input.GetKey(c.rThumb_Press))
        {
            print("R Thumb Pressed.");
        }
        if (Input.GetKey(c.lThumb_Press))
        {
            print("L Thumb Pressed.");
        }


    }

    private void CheckThumbsticks()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) { return; }

        float x = Input.GetAxisRaw("Oculus_RThumbstickX");
        float y = Input.GetAxisRaw("Oculus_RThumbstickY");

        print("R:" + x + " ; " + y);

        x = Input.GetAxisRaw("Oculus_LThumbstickX");
        y = Input.GetAxisRaw("Oculus_LThumbstickY");

        print("L: " + x + " " + y);

    }
}
