
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public int Value;

    // [System.NonSerialized]
    public int RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = Value;
    }

    public void OnBeforeSerialize()
    {

    }
}