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
        ReadKeys();
        //ReadJoysticks();
        //ReadButtons();
        //ReadAxes();
    }

    private void ReadButtons()
    {
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick 1 button " + i))
            {
                Debug.Log("joystick " + i + " button pressed");
            }
        }
    }

    private void ReadJoysticks()
    {
        var joysticks = Input.GetJoystickNames();

        foreach(var s in joysticks)
        {
            print(s);
            //float input = Input.GetAxisRaw(s);
            //if (input != 0f)
            //{
            //    print(s + ": " + input);
            //}
        }

    }

    void ReadAxes()
    {
        var inputManager = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];

        SerializedObject obj = new SerializedObject(inputManager);

        SerializedProperty axisArray = obj.FindProperty("m_Axes");

        if (axisArray.arraySize == 0)
            Debug.Log("No Axes");

        for (int i = 0; i < axisArray.arraySize; ++i)
        {
            var axis = axisArray.GetArrayElementAtIndex(i);

            var name = axis.FindPropertyRelative("m_Name").stringValue;
            var axisVal = axis.FindPropertyRelative("axis").intValue;
            //int inputType = (InputType)axis.FindPropertyRelative("type").intValue;

            if (axisVal < 0.01f || axisVal > -0.5f) { return;} //no input

            Debug.Log(name);
            Debug.Log(axisVal);
            //Debug.Log(inputType);
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
