using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Get2DPos
{
    public static Vector2 Get2DPosition(this Vector3 _v)
    {
        return new Vector2(_v.x, _v.y);
    }
}
