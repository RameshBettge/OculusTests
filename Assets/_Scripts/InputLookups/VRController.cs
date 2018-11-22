
using UnityEngine;

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

    public int Button1 = 0;
    public int Button1_Touch = 0;
    [Space(10)]

    public int Button2 = 0;
    public int Button2_Touch = 0;
    [Space(10)]

    public string ThumbX = "Horizontal";
    public string ThumbY = "Horizontal";
    public int Thumb_Touch = 0;
    public int Thumb_Press = 0;
    [Space(10)]


    public string Index = "Horizontal";
    //string rIndex_NearTouch = "Horizontal";
    public int Index_Touch = 0;
    [Space(5)]
    public string Grab = "Horizontal";
}
