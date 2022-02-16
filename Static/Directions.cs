using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Directions
{
    public static readonly Dictionary<DirectionState, Vector3> Values
        = new Dictionary<DirectionState, Vector3>
    {
        {DirectionState.Left, new Vector3(0.05f, 0.05f, 0.05f)},
        {DirectionState.Right, new Vector3(-0.05f, 0.05f, 0.05f)}
    };
}
