
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[System.Serializable]
public class VRController
{
    [SerializeField] int button1 = 0;
    [SerializeField] int button1_Touch = 0;
    [Space(10)]

    [SerializeField] int button2 = 0;
    [SerializeField] int button2_Touch = 0;
    [Space(10)]

    [SerializeField] string thumbX = "Horizontal";
    [SerializeField] string thumbY = "Horizontal";
    [SerializeField] int thumb_Touch = 0;
    [SerializeField] int thumb_Press = 0;
    [Space(10)]


    [SerializeField] string index = "Horizontal";
    //string rIndex_NearTouch = "Horizontal";
    [SerializeField] int index_Touch = 0;
    [Space(5)]
    [SerializeField] string grab = "Horizontal";

    // ----

    [HideInInspector] public InputButton Button1;
    [HideInInspector] public InputButton Button1_Touch;
    [Space(10)]

    [HideInInspector] public InputButton Button2;
    [HideInInspector] public InputButton Button2_Touch;
    [Space(10)]

    InputAxis ThumbX;
    InputAxis ThumbY;
    [HideInInspector] public InputAxisPair ThumbAxes;

    [HideInInspector] public InputButton Thumb_Touch;
    [HideInInspector] public InputButton Thumb_Press;
    [Space(10)]


    [HideInInspector] public InputAxis Index;
    //InputAxis rIndex_NearTouch;
    [HideInInspector] public InputButton Index_Touch;
    [Space(5)]
    [HideInInspector] public InputAxis Grab;


    public void Apply()
    {
        Button1 = new InputButton(button1);

        Button1_Touch = new InputButton(button1_Touch);

        Button2 = new InputButton(button2);
        Button2_Touch = new InputButton(button2_Touch);

        ThumbX = new InputAxis(thumbX);
        ThumbY = new InputAxis(thumbY);
        ThumbAxes = new InputAxisPair(ThumbX, ThumbY);

        Thumb_Touch = new InputButton(thumb_Touch);
        Thumb_Press = new InputButton(thumb_Press);


        Index = new InputAxis(index);
        //string rIndex_NearTouch;
        Index_Touch = new InputButton(index_Touch);
        Grab = new InputAxis(grab);
    }
}




