
using UnityEngine;
using UnityEditor;

// TODO: Make sure the variables reflect the actual state even when set by VRInputSetup.cs!
[ExecuteInEditMode]
[System.Serializable]
public class VRController
{
    [Header("Buttons")]
    [SerializeField] int button1_Touch = -1;
    [SerializeField] int button2_Touch = -1;
    [Space(10)]

    [SerializeField] int index_Touch = -1;
    [SerializeField] int thumb_Touch = -1;
    [Space(10)]

    [SerializeField] int button1 = -1;
    [SerializeField] int button2 = -1;
    [SerializeField] int thumb_Press = -1;
    [Space(10)]


    [Header("Axes")]
    [SerializeField] int thumbX = -1;
    [SerializeField] bool thumbXInverted = false;
    [Space(10)]

    [SerializeField] int thumbY = -1;
    [SerializeField] bool thumbYInverted = false;
    [Space(10)]

    [SerializeField] int index = -1;
    [SerializeField] bool indexInverted = false;
    [Space(10)]

    //int rIndex_NearTouch = -1;

    [SerializeField] int grab = -1;
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

    public void CopyFromSetup(VRInputSetup s)
    {
        button1 = s.button1;

        button1_Touch = s.button1_Touch;

        button2 = s.button2;
        button2_Touch = s.button2_Touch;

        index_Touch = s.index_Touch;

        thumb_Touch = s.thumb_Touch;
        thumb_Press = s.thumb_Press;


        thumbX = s.thumbX;
        thumbXInverted = s.thumbXInverted;

        thumbY = s.thumbY;
        thumbYInverted = s.thumbYInverted;

        index = s.index;
        indexInverted = s.indexInverted;

        grab = s.grab;
        grabInverted = s.grabInverted;

        s.AddKeyInt(button1);
        s.AddKeyInt(button1_Touch);
        s.AddKeyInt(button2);
        s.AddKeyInt(button2_Touch);
        s.AddKeyInt(index_Touch);
        s.AddKeyInt(thumb_Touch);
        s.AddKeyInt(thumb_Press);

        Apply();
    }

    public void WriteToSetup(VRInputSetup s)
    {
        s.button1 = button1;
        s.button1_Touch = button1_Touch;

        s.button2 = button2;
        s.button2_Touch = button2_Touch;

        s.index_Touch = index_Touch;

        s.thumb_Touch = thumb_Touch;
        s.thumb_Press = thumb_Press;


        s.thumbX = thumbX;
        s.thumbXInverted = thumbXInverted;

        s.thumbY = thumbY;
        s.thumbYInverted = thumbYInverted;

        s.index = index;
        s.indexInverted = indexInverted;

        s.grab = grab;
        s.grabInverted = grabInverted;
    }


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




