using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "PlayerStatus", order = 51)]
public class PlayerStatus : ScriptableObject
{
    public Vector3 playerLastPos;
}
