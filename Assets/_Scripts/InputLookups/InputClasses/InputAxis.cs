using UnityEngine;

[System.Serializable]
public class InputAxis
{
    public string name;
    bool inverted;

    public InputAxis(int axisNum, bool inverted)
    {
        this.inverted = inverted;
        name = "Axis" + axisNum;
    }

    public float Value
    {
        get
        {
            float v = Input.GetAxis(name);
            v = inverted ? -v : v;
            return v;
        }
    }
    public float RawValue
    {
        get
        {
            float v = Input.GetAxisRaw(name);
            v = inverted ? -v : v;
            return v;
        }
    }
}
