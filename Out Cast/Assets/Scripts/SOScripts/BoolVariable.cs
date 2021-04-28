using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    public bool Value;

    public void SetValue(bool newBool)
    {
        Value = newBool;
    }

    public void SetValue(BoolVariable newBool)
    {
        Value = newBool.Value;
    }
}