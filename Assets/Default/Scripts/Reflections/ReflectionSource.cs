using UnityEngine;

[System.Serializable]
public class ReflectionSource
{
    public int Integer;
    public float Float;

    public int IntegerProp {get; set;}

    int privateInt;

    public Reference Reference = new Reference("Some Reference");
}
