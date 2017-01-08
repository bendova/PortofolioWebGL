using UnityEngine;
using System.Collections;

public class PluginMathUtils
{
    public static float GetSafeFloat(float value)
    {
        if (float.IsNaN(value) || float.IsInfinity(value))
        {
            return 0.0f;
        }
        return value;
    }
}
