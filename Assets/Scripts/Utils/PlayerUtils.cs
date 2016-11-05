using UnityEngine;
using System.Collections;

public class PlayerUtils
{
    public static GameObject GetPlayer()
    {
        return GameObject.FindGameObjectWithTag(Tags.Player);
    }
}
