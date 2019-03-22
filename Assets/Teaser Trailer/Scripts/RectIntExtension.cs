using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectIntExtension
{
    public static Rect ToRect(this RectInt rect)
    {
        return new Rect(rect.x, rect.y, rect.width, rect.height);
    }
}
