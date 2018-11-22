
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[System.Serializable]
public class VRController
{
    [Header("Buttons")]
    [SerializeField] int button1 = 0;
    [SerializeField] int button1_Touch = 0;
    [Space(10)]

    [SerializeField] int button2 = 0;
    [SerializeField] int button2_Touch = 0;
    [Space(10)]

    [SerializeField] int index_Touch = 0;
    [Space(10)]

    [SerializeField] int thumb_Touch = 0;
    [SerializeField] int thumb_Press = 0;
    [Space(10)]


    [Header("Axes")]
    [SerializeField] int thumbX = 0;
    [SerializeField] bool thumbXInverted = false;
    [Space(10)]

    [SerializeField] int thumbY = 0;
    [SerializeField] bool thumbYInverted = false;
    [Space(10)]

    [SerializeField] int index = 0;
    [SerializeField] bool indexInverted = false;
    [Space(10)]

    //int rIndex_NearTouch = 0;

    [SerializeField] int grab = 0;
    [SerializeField] bool grabInverted = false;

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
    [Space(10)]
    [HideInInspector] public InputAxis Grab;


    public void Apply()
    {
        Button1 = new InputButton(button1);

        Button1_Touch = new InputButton(button1_Touch);

        Button2 = new InputButton(button2);
        Button2_Touch = new InputButton(button2_Touch);

        ThumbX = new InputAxis(thumbX, thumbXInverted);
        ThumbY = new InputAxis(thumbY, thumbYInverted);
        ThumbAxes = new InputAxisPair(ThumbX, ThumbY);

        Thumb_Touch = new InputButton(thumb_Touch);
        Thumb_Press = new InputButton(thumb_Press);


        Index = new InputAxis(index, indexInverted);
        //int rIndex_NearTouch;
        Index_Touch = new InputButton(index_Touch);
        Grab = new InputAxis(grab, grabInverted);
    }
}




