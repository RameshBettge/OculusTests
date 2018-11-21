using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class CheckInput : MonoBehaviour
{
    private void Awake()
    {
        //ReadAxes();
    }

    void Update()
    {
        //ReadKeys();


        //CheckThumbsticks();
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

    private void ReadButtons()
    {
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("Thumbstick 1 button " + i))
            {
                Debug.Log("Thumbstick " + i + " button pressed");
            }
        }
    }

    private void ReadThumbsticks()
    {
        var Joysticks = Input.GetJoystickNames();

        foreach(var s in Joysticks)
        {
            print(s);
        }

    }

    void ReadKeys()
    {
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                Debug.Log(key.ToString());
            }
        }

        
    }
}
