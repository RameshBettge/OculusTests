using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookupTester : MonoBehaviour
{
    [SerializeField]
    VRInputLookup lookup;

    private void Start()
    {

    }

  

    void Update()
    {
        TestVRControllerButtons("R: ", lookup.Right);
        TestVRControllerButtons("L: ", lookup.Left);

        TestVRControllerAxes("R: ", lookup.Right);
        TestVRControllerAxes("L: ", lookup.Left);

        //LogUsedAxis();
    }

    private void LogUsedAxis()
    {
        for (int i = 1; i < 29; i++)
        {
            string axis = "Axis" + i;
            float v = Input.GetAxis(axis);

            if(v > 0.1f && v < 0.9f)
            {
                print(axis + ": " + v);
            }
        }
    }

    private void TestVRControllerAxes(string s, VRController c)
    {
        if (c.Grab.Value > 0.05f)
        {
            print(s + "Grab = " + c.Grab.Value);
        }
        if (c.Index.Value > 0.05f)
        {
            print(s + "Index = " + c.Index.Value);
        }

        Vector2 thumb = c.ThumbAxes.Vec2;

        if(thumb.sqrMagnitude > 0.01f)
        {
            print(s + thumb);
        }
    }

    private void TestVRControllerButtons(string s, VRController c)
    {
        // Buttons
        if (c.Button1.OnDown)
        {
            print(s + "button1 pressed");
        }
        if (c.Button1_Touch.OnDown)
        {
            print(s + "Button1 touched");
        }
        if (c.Button2.OnDown)
        {
            print(s + "Button2 pressed");
        }
        if (c.Button2_Touch.OnDown)
        {
            print(s + "Button2 touched");
        }

        if (c.Index_Touch.OnDown)
        {
            print(s + "Index touched");
        }
        if (c.Thumb_Touch.OnDown)
        {
            print(s + "Thumb touched.");
        }
        if (c.Thumb_Press.OnDown)
        {
            print(s + "Thumb pressed.");
        }


   
    }
}
